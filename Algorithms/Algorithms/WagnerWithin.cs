using System;
using System.Collections.Generic;

namespace Algorithms
{
    public class WagnerWithin
    {
        public readonly int[] demand;
        public readonly int[] setupCosts;
        public readonly int[] inventoryCosts;
        public readonly int horizon;

        //output variables
        private int[] minimalCosts;//minimum production costs for plannings from period 1 to i
        private int[][] optimalProductionQuantity;//production plans for plannings from period 1 to i
        private int[][] optimalInventory;//inventory for plannings from periods 1 to i

        public int[] MinimalCosts {
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

        //lol
        private void InitializeArrays()
        {
            minimalCosts = new int[horizon];
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
            this.setupCosts = new int[] { 85, 102, 102, 101, 98, 114, 105, 86, 119, 110, 98, 114 };
            this.inventoryCosts = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            this.horizon = 12;
        }

        public WagnerWithin(int numberOfPeriods, int[] demand, int[] setupCosts, int[] inventoryCosts) {
            this.demand = demand;
            this.setupCosts = setupCosts;
            this.inventoryCosts = inventoryCosts;
            this.horizon = numberOfPeriods;
        }

        public WagnerWithin(Context context) {
            this.demand = context.demand;
            this.setupCosts = context.setupCosts;
            this.inventoryCosts = context.inventoryCosts;
            this.horizon = context.horizon;
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
            }

            List<int> possibleCosts = new List<int>(); //i possible costs for satisfying demand by launching production at period j, 1<=j<=i
            List<int[]> possibleInventories = new List<int[]>();
            List<int[]> possibleProductionQuantities = new List<int[]>();


            //cost and production plan evaluation if production is launched at period j to satisfy demand at period i
            for (int j = 0; j <= i; j++) {

                int tempCost = 0; 
                int[] tempInventory = new int[i + 1]; 
                int[] tempProductionQuantity = new int[i + 1]; 

                if (j > 0)
                {
                    tempCost = minimalCosts[j - 1]; //cost of lauching production at j = minimal cost between 1 and j-1 + setup costs j + inventory costs from j to i
                    for (int k = 0; k < j; k++) {
                        tempInventory[k] = optimalInventory[j - 1][k];
                        tempProductionQuantity[k] = optimalProductionQuantity[j - 1][k];
                    }
                }
                tempCost += setupCosts[j];

                //inventory costs evaluation
                int totalUnitsOrderedAtPeriodj = 0;
                for (int k = j; k <= i; k++) totalUnitsOrderedAtPeriodj += demand[k];
                int currentInventory = totalUnitsOrderedAtPeriodj;
                tempProductionQuantity[j] = totalUnitsOrderedAtPeriodj;
                tempInventory[j] = currentInventory - demand[j];

                for (int k = j + 1; k <= i; k++) {
                    currentInventory = currentInventory - demand[k - 1];
                    tempInventory[k] = currentInventory - demand[k];
                    tempCost += currentInventory * inventoryCosts[k - 1];
                }

                possibleCosts.Add(tempCost);
                possibleInventories.Add(tempInventory);
                possibleProductionQuantities.Add(tempProductionQuantity);
            }

            int indexOfMinimumCostCase = IndexOfMinimum(possibleCosts);

            minimalCosts[i] = possibleCosts[indexOfMinimumCostCase];
            optimalProductionQuantity[i] = possibleProductionQuantities[indexOfMinimumCostCase];
            optimalInventory[i] = possibleInventories[indexOfMinimumCostCase];
        }

        private static int IndexOfMinimum(List<int> liste)
        {
            if (liste.Count == 0 || liste == null) return 0;
            int min = liste[0];
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
