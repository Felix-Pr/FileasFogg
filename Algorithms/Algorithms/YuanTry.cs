using System;

namespace Algorithms
{
    public class YuanTry
    {
        public YuanContext c;
        //k = periode a laquelle il faut répondre a la demande
        //t = periode on produit pour satisfaire la demande de la periode k

        public double[] minimalFinancingCosts;
        private double[][] Ekt;
        public int[][] Q; //production
        private int[][] Y; //setup , =0 ou =1, a revoir
        public int[][] I; //inventaire a la periode t
        private int[][] lastXtk;
        private double[] owcr;

        public YuanTry(string fileName)
        {
            this.c = new YuanContext(fileName);
        }

        public YuanTry(YuanContext yuanContext)
        {
            this.c = yuanContext;
        }

        public static void Main(string[] args)
        {
            YuanTry algo = new YuanTry(@"C:\Users\poiri\Downloads\exemplefabrice.csv");

            algo.ComputeProductionPlan();
            int sumOfDemand = 0;
            int sumOfProduction = 0;
            foreach (int d in algo.c.demand) sumOfDemand += d;
            foreach (int q in algo.Q[algo.c.horizon - 1]) sumOfProduction += q;
            Console.WriteLine("Sum of demand : " + sumOfDemand);
            Console.WriteLine("Sum of production : " + sumOfProduction);
            Console.WriteLine("Q :");
            foreach (int q in algo.Q[algo.c.horizon-1])Console.WriteLine(q);
            Console.WriteLine("Y :");
            foreach(int y in algo.Y[algo.c.horizon-1])Console.WriteLine(y);
            Console.WriteLine();
            Console.WriteLine("Demand:");
            foreach (int d in algo.c.demand) Console.WriteLine(d);
            Console.WriteLine();

            Console.WriteLine("Alpha"+algo.c.alpha);            
            Console.WriteLine("Beta"+algo.c.beta);
            int k = 2;
            for (int t=0; t<=k; t++) Console.WriteLine("LC " + t + "," + k + " : " +algo.LC(t,k));

            for(int i = 0; i<algo.c.horizon; i++) Console.WriteLine("Cost "+i+ " : " + algo.minimalFinancingCosts[i]);

            Console.Read();
        }

        public void InitializeArrays()
        {
            minimalFinancingCosts = new double[c.horizon];
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
            minimalFinancingCosts[0] = ComputeEtk(0, 0);
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

            int periodOfLatestSetup = GetPeriodOfLatestSetup(k);
            ComputeEk(k, periodOfLatestSetup);

            double[] possibleFinancingCosts = new double[k + 1];
            possibleFinancingCosts[0] = Ekt[k][0];
            for (int t = Math.Max(periodOfLatestSetup,1); t <= k; t++) possibleFinancingCosts[t] = minimalFinancingCosts[t-1] + Ekt[k][t];

            int periodOfProduction = IndexOfMinimum(possibleFinancingCosts, periodOfLatestSetup);

            minimalFinancingCosts[k] = possibleFinancingCosts[periodOfProduction];

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
                Q[k][i] = Q[periodOfProduction-1][i];
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
            //for (int l = t; l <= k-1; l++) res += WcrPur(t, k) + WcrSet(t, k) + WcrProd(t, k) + WcrInv(t, k);
            res += WcrPur(t, k) + WcrSet(t, k) + WcrProd(t, k) + WcrInv(t, k);
            res *= c.beta;
            res += LC(t, k);
            return res;
        }

        private double WcrPur(int t, int k) //OWCR for purchasing Xtk //VALIDE
        {
            double res = (CalculateSum(t + 1 + c.delayInPaymentToSupplier, c.horizon) - CalculateSum(k + 1 + c.delayInPaymentFromClient, c.horizon)) * c.unitMaterialCost * c.demand[k];
            Console.WriteLine("WcrPur " + t + "," + k + " : " + res);

            return res ;
        }
        private double WcrSet(int t, int k) //
        {
            int[] y;
            int[] q;

            if (k == 0)
            {
                q = new int[] { c.demand[0] };
                if (c.demand[0] > 0) y = new int[] { 1 };
                else y = new int[] { 0 };
            }
            else
            {
                y = InducedY(t, Y[k - 1]);
                q = InducedProductionPlanning(t, Q[k - 1]);
            }
            if (q[t] == 0) return 0;
            double res = CalculateSum(t+1,k+1+c.delayInPaymentFromClient-1) * c.demand[k] * c.setupCosts[0] * y[t] / (q[t] + 1 - y[t]);
            Console.WriteLine("WcrSet " + t + "," + k + " : " + res);
            return res;
        }
        private double WcrProd(int t, int k)
        {
            Console.WriteLine("WcrProd " + t + "," + k + " : " + CalculateSum(t + 1, k + 1 + c.delayInPaymentFromClient - 1) * c.productionCost * c.demand[k]);
            return CalculateSum(t+1,k+1+c.delayInPaymentFromClient-1)*c.productionCost*c.demand[k];
        }
        private double WcrInv(int t, int k)
        {
            double res = 0;
            for (int l = t+1; l <= k ; l++)
            {
                res += CalculateSum(l, k + 1 + c.delayInPaymentFromClient - 1);
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
            if(t - c.delayInPaymentToSupplier>=0) res += c.unitMaterialCost * production[t - c.delayInPaymentToSupplier];
            res += c.productionCost * production[t];
            /*if (t<k && production[t] > 0 || t==k)*/ res += c.setupCosts[t]; //* Y[k][t]; //ATTENTION SETUPCOSTS CONSTANTS
            for (int l = t; l<k; l++) res += c.inventoryCosts[0] * inventory[l];
            res *= Math.Pow(1 + c.alpha, -t);
            //Console.WriteLine("LC " + t + "," + k + " : " + res);

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
        private int[] InducedY(int t, int[] lastY)
        {
            int k = lastY.Length;
            int[] y = new int[k + 1];
            for (int i = 0; i < k; i++) y[i] = Y[k - 1][i];
            y[t] = 1;
            return y;
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
            double res = 0;
            for(int i = beginIndex; i<=endIndex; i++) res+= Math.Pow(1/(1 + c.alpha), i);


            //
            return res;
        }
        private double CalculateSum(int endIndex)
        {
            double res = 0;
            for (int i = 1; i <= endIndex; i++) res += Math.Pow(1 + c.alpha, -i);
            return res;
        }
    }
}
