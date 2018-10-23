using System.Collections.Generic;

namespace Algorithms
{
    class WagnerWithin
    {
        //Paramètres de l'exemple du papier de Wagner Within (1958)
        public readonly int[] demand; //demande
        public readonly int[] setupCosts; //setup costs
        public readonly int[] inventoryCosts; //inventory costs
        public readonly int numberOfPeriods; //nombre de périodes

        private int[] minimalCosts;//couts minimaux des plages 1 à i pour i allant de 1 à N
        public int[] MinimalCosts {
            get {
                if (minimalCosts == null) {
                    minimalCosts = ComputeMinimalCosts();
                }
                return minimalCosts;
            }
        } 

        public WagnerWithin() {
            //Crée la classe par défaut avec les paramètres de l'exemple du papier de Wagner et Within (1958)
            this.demand = new int[] { 69, 29, 36, 61, 61, 26, 34, 67, 45, 67, 79, 56 };
            this.setupCosts = new int[] { 85, 102, 102, 101, 98, 114, 105, 86, 119, 110, 98, 114 };
            this.inventoryCosts = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
            this.numberOfPeriods = 12;
        }

        public WagnerWithin(int N, int[] d, int[] s, int[] ic) {
            this.demand = d;
            this.setupCosts = s;
            this.inventoryCosts = ic;
            this.numberOfPeriods = N;
        }

        public int[] ComputeMinimalCosts()
        {
            minimalCosts = new int[numberOfPeriods];
            for (int i = 0; i < numberOfPeriods; i++) //Détermination du cout minimum pour les périodes i allant de 1 à N
            {
                minimalCosts[i] = ComputeMinimalCostForPeriods1toI(i);
            }
            return minimalCosts;
        }

        private int ComputeMinimalCostForPeriods1toI(int i)
        {
            if (i == 0 && demand[0] != 0) return setupCosts[0]; //pour la première période le coût minimum est le setup cost de la période 1 si la demande est non nulle
            else if (i == 0 && demand[0] == 0) return 0; //si la demande à la première période est nulle, on ne produit rien et il n'y a pas de coûts

            List<int> costs = new List<int>(); //liste des i coûts si on satisfait la demande de la période i en commandant aux périodes 1<=j<=i
            int cost = 0;
            for (int j = 0; j < i; j++)
            {
                if (j > 0) cost = minimalCosts[j - 1]; //cout en commandant à la période j<i => cout minimum entre 1 et j-1 + setup costs j + coûts de stock de j à i
                cost += setupCosts[j];

                //calcul des couts de stock
                int totalUnitsOrderedAtPeriodj = 0;
                for (int k = j; k <= i; k++) totalUnitsOrderedAtPeriodj += demand[k];

                int inventory = totalUnitsOrderedAtPeriodj;
                for (int k = j + 1; k <= i; k++)
                {
                    inventory = inventory - demand[k - 1];
                    cost += inventory * inventoryCosts[k - 1];
                }
                costs.Add(cost);
            }

            costs.Add(minimalCosts[i - 1] + setupCosts[i]); //prise en compte du cas : on commande à la période i pour satisfaire la demande de la période i (pas de coûts de stock supplémentaires)
            return Minimum(costs);
        }

        private static int Minimum(List<int> liste)
        {
            if (liste.Count == 0 || liste == null) return 0;
            int min = liste[0];
            foreach (int k in liste) if (k < min) min = k;
            return min;
        }

    }
}
