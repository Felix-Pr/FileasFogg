using System.Collections.Generic;

namespace Algorithms
{
    public class WagnerWithin
    {
        public readonly int[] demand;
        public readonly double[] setupCosts;
        public readonly double[] inventoryCosts;
        public readonly int horizon;

        //output variables
        private double[] minimalCosts;//minimum production costs for plannings from period 1 to i
        private int[][] optimalProductionQuantity;//production plans for plannings from period 1 to i
        private int[][] optimalInventory;//inventory for plannings from periods 1 to i

        public double[] MinimalCosts {
            get {
                if (minimalCosts == null) {
                    ComputeProductionPlan();
                }
                return minimalCosts;
            }
        }
        public int[] OptimalProductionQuantity
        {
            get
            {
                if (optimalProductionQuantity == null)
                {
                    ComputeProductionPlan();
                }
                return optimalProductionQuantity[horizon-1];
            }
        }
        public int[] OptimalInventory
        {
            get
            {
                if (optimalInventory == null)
                {
                    ComputeProductionPlan();
                }
                return optimalInventory[horizon-1];
            }
        }

        private void InitializeArrays()
        {
            minimalCosts = new double[horizon];
            optimalProductionQuantity = new int[horizon][];
            optimalInventory = new int[horizon][];

            for (int i = 0; i <horizon; i++)
            {
                optimalProductionQuantity[i] = new int[i + 1];
                optimalInventory[i] = new int[i + 1];
            }
        }

        public WagnerWithin() {
            this.demand = new int[] { 69, 29, 36, 61, 61, 26, 34, 67, 45, 67, 79, 56 };
            this.setupCosts = new double[] { 85, 102, 102, 101, 98, 114, 105, 86, 119, 110, 98, 114 };
            this.inventoryCosts = new double[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            this.horizon = 12;
        }

        public WagnerWithin(YuanContext context)
        {
            this.demand = context.demand.ToArray();
            this.setupCosts = context.setupCosts.ToArray();
            this.inventoryCosts = context.inventoryCosts.ToArray();
            this.horizon = context.horizon;
        }

        public WagnerWithin(int numberOfPeriods, int[] demand, double[] setupCosts, double[] inventoryCosts) {
            this.demand = demand;
            this.setupCosts = setupCosts;
            this.inventoryCosts = inventoryCosts;
            this.horizon = numberOfPeriods;
        }

        public void ComputeProductionPlan()
        {
            InitializeArrays();
            for (int i = 0; i < horizon; i++) { 
                ComputeProductionPlanForPeriods1ToI(i);
            }
        }

        private void ComputeProductionPlanForPeriods1ToI(int i)
        {
            if (i == 0) {
                //first period particular case
                optimalInventory[0][0] = 0;
                optimalProductionQuantity[0][0] = demand[0];
                if (demand[0] > 0) minimalCosts[0] = setupCosts[0];
                return;
            }

            else if (demand[i] == 0) {
                //null demand particular case
                int[] tempInventory = new int[i + 1];
                int[] tempProductionQuantity = new int[i + 1];
                double minimumCost = 0;
                for(int j = 0; j < i; j++)
                {
                    tempInventory[j] = optimalInventory[i-1][j];
                    tempProductionQuantity[j] = optimalProductionQuantity[i-1][j];
                }
                tempInventory[i] = tempInventory[i - 1];
                tempProductionQuantity[i] = 0;
                minimumCost = minimalCosts[i - 1] + tempInventory[i - 1] * inventoryCosts[i - 1];

                optimalInventory[i] = tempInventory;
                optimalProductionQuantity[i] = tempProductionQuantity;
                minimalCosts[i] = minimumCost;
                return;
            }


            List<double> possibleCosts = new List<double>(); //i possible costs for satisfying demand by launching production at period j, 1<=j<=i
            List<int[]> possibleInventories = new List<int[]>();
            List<int[]> possibleProductionQuantities = new List<int[]>();
            
            //cost and production plan evaluation if production is launched at period j to satisfy demand at period i
            for (int j = 0; j <= i; j++) {
                EvaluateCostOfProducingAtPeriodJforI(i, j, ref possibleCosts, ref possibleInventories, ref possibleProductionQuantities);
            }

            int indexOfMinimumCostCase = IndexOfMinimum(possibleCosts);

            minimalCosts[i] = possibleCosts[indexOfMinimumCostCase];
            optimalProductionQuantity[i] = possibleProductionQuantities[indexOfMinimumCostCase];
            optimalInventory[i] = possibleInventories[indexOfMinimumCostCase];
        }

        private void EvaluateCostOfProducingAtPeriodJforI(int i, int j, ref List<double> possibleCosts, ref List<int[]> possibleInventories, ref List<int[]> possibleProductionQuantities)
        {
            double tempCost = 0;
            int[] tempInventory = new int[i + 1];
            int[] tempProductionQuantity = new int[i + 1];

            if (j > 0)
            {
                tempCost = minimalCosts[j - 1]; //cost of lauching production at j = minimal cost between 1 and j-1 + setup costs j + inventory costs from j to i
                for (int k = 0; k < j; k++)
                {
                    tempInventory[k] = optimalInventory[j - 1][k];
                    tempProductionQuantity[k] = optimalProductionQuantity[j - 1][k];
                }
            }
            if (demand[i] > 0) tempCost += setupCosts[j];

            //inventory costs evaluation
            int totalUnitsOrderedAtPeriodj = 0;
            for (int k = j; k <= i; k++) totalUnitsOrderedAtPeriodj += demand[k];
            int currentInventory = totalUnitsOrderedAtPeriodj;
            tempProductionQuantity[j] = totalUnitsOrderedAtPeriodj;
            tempInventory[j] = currentInventory - demand[j];

            for (int k = j + 1; k <= i; k++)
            {
                currentInventory = currentInventory - demand[k - 1];
                tempInventory[k] = currentInventory - demand[k];
                tempCost += currentInventory * inventoryCosts[k - 1];
            }

            possibleCosts.Add(tempCost);
            possibleInventories.Add(tempInventory);
            possibleProductionQuantities.Add(tempProductionQuantity);
        }

        private static int IndexOfMinimum(List<double> liste)
        {
            if (liste.Count == 0 || liste == null) return 0;
            double min = liste[0];
            int index = 0;
            for(int i = 0; i < liste.Count; i++)
            {
                if (liste[i] < min)
                {
                    min = liste[i];
                    index = i;
                }
            }
            return index;
        }
    }
}
