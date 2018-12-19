using System;

namespace Algorithms
{
    class Yuan
    {
        public YuanContext c;
        //k = periode a laquelle il faut répondre a la demande
        //t = periode on produit pour staisfaire a demande a la periode k


        private double[] minimalCosts;
        private double[][] Ekt;
        private double[][] P; //production
        private double[][] Y; //setup , =0 ou =1, a revoir
        private int[][][] X; //prod a la periode t pour satisfaire demande periode k
        private double[][] I; //inventaire a la periode t
        private int[][] Q; //Qté produite à t
        
        private void InitializeArrays()
        {
            minimalCosts = new double[c.horizon];
            Q = new int[c.horizon][];
            Y = new double[c.horizon][];
            I = new double[c.horizon][];
            P = new double[c.horizon][];
            Ekt = new double[c.horizon][];
            X = new int[c.horizon][][];
            for (int i = 0; i < c.horizon; i++) {
                Q[i] = new int[i + 1];
                P[i] = new double[i + 1];
                Ekt[i] = new double[i + 1];
                X[i] = new int[i + 1][];
                for (int j = 0; j <= i; j++) X[i][j] = new int[j + 1];
            }
            foreach (int[][] x in X ) x[0][0] = c.demand[0];


        }
        public void ComputeProductionPlan() {
            InitializeArrays();
            for (int k = 0; k < c.horizon; k++) {
                ComputeProductionPlanFomPeriods1tok(k);
            }
        }
        private void ComputeProductionPlanFomPeriods1tok(int k) {
            ComputeEtk(k);
            double[] possiblecosts = new double[k + 1];
            possiblecosts[0] = Ekt[k][0];
            for (int t = 1; t <= k; t++) possiblecosts[t] = minimalCosts[t - 1] + Ekt[k][t];

            int periodOfProduction = IndexOfMinimum(possiblecosts);
            minimalCosts[k] = possiblecosts[periodOfProduction];
            //for (int i = 0; i < periodOfProduction; i++) optimalProductionQuantity[k][i] = optimalProductionQuantity[periodOfProduction - 1][i];
            //for (int i = periodOfProduction; i <= k; i++) optimalProductionQuantity[k][periodOfProduction] += context.demand[i];


        }

        private void computeXtk(int k)
        {
            if (k == 0) return;
        }

        private void ComputeEtk() {
            for (int k = 0; k < c.horizon; k++) {
                computeXtk(k);
                ComputeEtk(k);
                //ComputeProductionPlanToPeriodK(k);
            }
        }
        private void ComputeEtk(int k)
        {
            for (int t = 0; t <= k; t++) {
                Ekt[k][t] = ProductionCosts(t, k) + c.beta * (WcrPur(t, k) + WcrSet(t, k) + WcrProd(t, k) + WcrInv(t, k));
            } 
        }

        private double ProductionCosts(int t, int k) {
            //Approche dynamique à rajouter
            //COMPUTE Q
            Q[k][t] = 0;
            for (int j = t; j < c.horizon; j++) Q[k][t] += X[k][t][j];
            //COMPUTE Y
            Y[k][t] = 0;
            if (Q[k][t] > 0) Y[k][t] = 1;
            //COMPUTE I
            I[k][t] = 0;
            for(int l=1;l<=t; l++) {
                for (int K = t + 1; K <= c.horizon; K++) I[k][t] += X[k][l][K];
            }

            double res = 0;
            res += c.unitMaterialCost * Q[k][t - c.delayInPaymentToSupplier]; //raw material cost
            res += c.productionCost * Q[k][t]; //production cost
            //res += c.setupCost * Y[k][t]; //setup cost
            //res += c.inventoryCost * I[k][t]; //inventory cost
            res *= 1 / Math.Pow(1 + c.alpha, t);

            return res;
        }
        private double WcrPur(int t, int k)
        {
            double res = 0;
            for (int j = t + c.delayInPaymentToSupplier; j <= c.horizon; j++) res += 1 / Math.Pow(1 + c.alpha, j);
            for (int j = k + c.delayInPaymentFromClient; j <= c.horizon; j++) res -= 1 / Math.Pow(1 + c.alpha, j);
            res *= c.unitMaterialCost * X[k][t][k];
            return res;
        }
        private double WcrSet(int t, int k)
        {
            double res = 0;
            for (int j = t; j <= k + c.delayInPaymentFromClient - 1; j++) res += 1 / Math.Pow(1 + c.alpha, j);
            //res = res * X[k][t];
            //res = res * (c.setupCost * Y[t] / (Q[t] + 1 - Y[t]));
            return res;
        }
        private double WcrProd(int t, int k)
        {
            double res = 0;
            for (int j = t; j <= k + c.delayInPaymentFromClient - 1; j++) res += 1 / Math.Pow(1 + c.alpha, j);
            //res *= c.productionCost * X[t][k];
            return res;
        }
        private double WcrInv(int t, int k)
        {
            double res = 0;
            for (int l = t;l<= k - 1;l++)
            {
                for(int j = l; j <= k + c.delayInPaymentFromClient - 1; j++)
                {
                    res+= 1 / Math.Pow(1 + c.alpha, j);
                }
            }
            //res *= c.inventoryCosts * X[t][k];
            return res;
        }
        private static int IndexOfMinimum(double[] arr) //duplicate WagnerWhitin
        {
            if (arr.Length == 0 || arr == null) return 0;
            double min = arr[0];
            int index = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] < min)
                {
                    min = arr[i];
                    index = i;
                }
            }
            return index;
        }

    }
}
