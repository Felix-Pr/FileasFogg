using System;

namespace Algorithms
{
    class YuanTry
    {

        public YuanContext c;
        //k = periode a laquelle il faut répondre a la demande
        //t = periode on produit pour satisfaire la demande de la periode k


        private double[] minimalCosts;
        private double[][] Ekt;
        private int[][] Q; //production
        private int[][] Y; //setup , =0 ou =1, a revoir
        private int[][] I; //inventaire a la periode t
        private int[][] lastXtk;
        private double[] owcr;

        public YuanTry(string fileName)
        {
            this.c = new YuanContext(fileName);
        }

        public static void Main(string[] args)
        {
            YuanTry algo = new YuanTry(@"C:\Users\poiri\Downloads\COntextTemplate.csv");
            algo.ComputeProductionPlan();
            foreach(int q in algo.Q[algo.c.horizon-1])Console.WriteLine(q);
            Console.WriteLine();
            Console.WriteLine("Alpha"+algo.c.alpha);            
            Console.WriteLine("Beta"+algo.c.beta);            
            Console.Read();
        }

        public void InitializeArrays()
        {
            minimalCosts = new double[c.horizon];
            Q = new int[c.horizon][];
            Y = new int[c.horizon][];
            I = new int[c.horizon][];
            Ekt = new double[c.horizon][];
            owcr = new double[c.horizon];
            for (int i = 0; i < c.horizon; i++)
            {
                Q[i] = new int[i + 1];
                Ekt[i] = new double[i + 1];
                I[i] = new int[i + 1];
                Y[i] = new int[i + 1];
            }
            Q[0][0] = c.demand[0];
            minimalCosts[0] = ComputeEtk(0, 0);
        }

        public void ComputeProductionPlan()
        {
            InitializeArrays();
            for (int k = 0; k < c.horizon; k++)
            {
                ComputeProductionPlanFomPeriods1toK(k);
            }
        }
        private void ComputeProductionPlanFomPeriods1toK(int k)
        {
            if (k == 0) return;

            int periodOfLatestSetup = GetPeriodOfLatestSetup(k-1);
            ComputeEk(k, periodOfLatestSetup);

            double[] possiblecosts = new double[k + 1];
            possiblecosts[0] = Ekt[k][0];
            for (int t = Math.Max(periodOfLatestSetup,1); t <= k; t++) possiblecosts[t] = minimalCosts[t-1] + Ekt[k][t];

            int periodOfProduction = IndexOfMinimum(possiblecosts, periodOfLatestSetup);

            minimalCosts[k] = possiblecosts[periodOfProduction];

            for (int i = 0; i < periodOfProduction; i++) Q[k][i] = Q[periodOfProduction - 1][i];
            for (int i = periodOfProduction; i <= k; i++) Q[k][periodOfProduction] += c.demand[i];
            
            ComputeQ(periodOfProduction, k);
            lastXtk = ComputeXtk(k);
            ComputeY(k);
            ComputeI(k);
        }

        private void ComputeQ(int periodOfProduction, int k)
        {
            Q[k] = new int[k + 1];
            for (int i = 0; i < Math.Max(periodOfProduction, 0); i++)
            {
                Q[k][i] = Q[periodOfProduction - 1][i];
            }
            int remainingToProduce = 0;
            for (int i = periodOfProduction; i < k+1; i++)
            {
                remainingToProduce += c.demand[i];
            }
            Q[k][periodOfProduction] += remainingToProduce;
        }
        private void ComputeY(int k)
        {
            int[] y = new int[k+1];
            for(int i = 0; i<= k; i++)
            {
                if (Q[k][i] > 0) y[i] = 1;
                else y[i] = 0;
            }
            Y[k] = y;
        }
        private int[][] ComputeXtk(int k)
        {
            int[][] Xtk = new int[k+1][];
            for (int t = 0; t <= k; t++)
            {
                double remainingInventory = Q[k][t];
                Xtk[t] = new int[t+1];
                for(int j = 0; j<=t; j++)
                {
                    if (remainingInventory <= 0) break;
                    Xtk[t][j] = c.demand[j];
                    remainingInventory = remainingInventory - c.demand[j];
                }
            }
            return Xtk;
        }
        private void ComputeI(int k)
        {
            int[] i = new int[k+1];
            int remaining = 0;

            for(int j = 0; j< k+1; j++)
            {
                i[j] = remaining + Q[k][j] - c.demand[j];
                remaining = i[j];
            }
            I[k] = i;
        }

        private void ComputeEk(int k)
        {
            for (int t = 0; t <= k; t++)
            {
                Ekt[k][t] = ComputeEtk(t, k);
            }
        }
        private void ComputeEk(int k, int tmin)
        {
            for (int t = tmin; t <= k; t++)
            {
                Ekt[k][t] = ComputeEtk(t, k);
            }
        }
        private double ComputeEtk(int t, int k)
        {
            double res = 0;
            //ATTENTION determiner la somme
            for (int l = t; l <= k - 1; l++) res += WcrPur(l, k) + WcrSet(l, k) + WcrProd(l, k) + WcrInv(l, k);
            res *= c.beta;
            res += LC(t, k);
            return res;
        }

        private double WcrPur(int t, int k) //OWCR for purchasing Xtk
        {
            Console.WriteLine("WcrPur " + t + "," + k + " : " + CalculateSum(t + c.delayInPaymentToSupplier, k + c.delayInPaymentFromClient) * c.unitMaterialCost * c.demand[k]);

            return CalculateSum(t + c.delayInPaymentToSupplier, k + c.delayInPaymentFromClient)*c.unitMaterialCost*c.demand[k];
            return 0;
            double res = 0;
            for (int j = t + c.delayInPaymentToSupplier; j <= c.horizon; j++) res += 1 / Math.Pow(1 + c.alpha, j);
            for (int j = k + c.delayInPaymentFromClient; j <= c.horizon; j++) res -= 1 / Math.Pow(1 + c.alpha, j);
            res *= c.unitMaterialCost * c.demand[k];
            return res;
        }
        private double WcrSet(int t, int k)
        {
            Console.WriteLine("WcrSet " + t + "," + k + " : " + CalculateSum(t, k + c.delayInPaymentFromClient - 1) * c.demand[k] * (c.setupCosts[0] * Y[k][t] / (Q[k][t] + 1 - Y[k][t])));

            return CalculateSum(t,k+c.delayInPaymentFromClient-1)*c.demand[k] * (c.setupCosts[0] * Y[k][t] / (Q[k][t] + 1 - Y[k][t]));
            double res = 0;
            for (int j = t; j <= k + c.delayInPaymentFromClient - 1; j++) res += 1 / Math.Pow(1 + c.alpha, j);
            res = res * c.demand[k];
            res = res * (c.setupCosts[0] * Y[k][t] / (Q[k][t] + 1 - Y[k][t])); //fix setupcosts
            return res;
        }
        private double WcrProd(int t, int k)
        {
            Console.WriteLine("WcrProd " + t + "," + k + " : " + CalculateSum(t, k + c.delayInPaymentFromClient - 1) * c.productionCost * c.demand[k]);

            return CalculateSum(t,k+c.delayInPaymentFromClient-1)*c.productionCost*c.demand[k];
            double res = 0;
            for (int j = t; j <= k + c.delayInPaymentFromClient - 1; j++) res += 1 / Math.Pow(1 + c.alpha, j);
            res *= c.productionCost * c.demand[k];
            return res;
        }
        private double WcrInv(int t, int k)
        {
            double res = 0;
            for (int l = t; l <= k - 1; l++)
            {
                res += CalculateSum(l, k + c.delayInPaymentFromClient - 1);
            }
            res *= c.inventoryCosts[0] * c.demand[k]; //fix couts inventaire

            Console.WriteLine("WcrInv " + t + "," + k + " : " + res);
            return res;
        }
        private double LC(int t, int k)
        {
            if (k == 0)
            {
                return c.setupCosts[0]+ c.demand[0]*c.unitMaterialCost + c.productionCost;
            }

            int[] production =InducedProductionPlanning(t, Q[k-1]);
            int[] inventory = InducedInventoryLevels(t, I[k-1]);

            double res = 0;
            res += c.unitMaterialCost * production[t - c.delayInPaymentToSupplier+1];
            res += c.productionCost * production[t];
            /*if (t<k && production[t] > 0 || t==k)*/ res += c.setupCosts[t]; //* Y[k][t]; //ATTENTION SETUPCOSTS CONSTANTS
            for (int l = t; l<k; l++) res += c.inventoryCosts[0] * inventory[l];
            res *= Math.Pow(1 + c.alpha, -t);
            Console.WriteLine("LC " + t + "," + k + " : " + res);

            return res;
        }
        
        private int[] InducedProductionPlanning(int t, int[] lastIterationPlanning)
        {
            int k = lastIterationPlanning.Length;
            int[] production = new int[k + 1];
            for (int i = 0; i < k; i++) production[i] = Q[k - 1][i];
            production[t] += c.demand[k];
            return production;
        }
        private int[] InducedInventoryLevels(int t, int[] lastIterationInventoryLevel)
        {
            int k = lastIterationInventoryLevel.Length;
            int[] inventory = new int[k + 1];
            for (int j = 0; j < k; j++) inventory[j] = lastIterationInventoryLevel[j];
            for (int j = t; j < k; j++) inventory[j] += c.demand[k];
            return inventory;
        }

        private int GetPeriodOfLatestSetup(int k)
        {
            for (int i = k-1; i >= 0; i--)
            {
                if (Q[k-1][i] > 0)
                {
                    return i;
                }
            }
            return 0;
        }
        private static int IndexOfMinimum(double[] arr, int startIndex) //duplicate WagnerWhitin
        {
            if (arr.Length == 0 || arr == null) return 0;
            double min = arr[startIndex];
            int index = startIndex;
            for (int i = startIndex+1; i < arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                    index = i;
                }
            }
            return index;
        }

        private double CalculateSum (int beginIndex, int endIndex)
        {
            return CalculateSum(endIndex) - CalculateSum(beginIndex);
        }
        private double CalculateSum(int endIndex)
        {
            double res = 0;
            for (int i = 1; i <= endIndex; i++) res += Math.Pow(1 + c.alpha, -i);
            return res;
        }
    }
}
