using System;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context() //Extracted context from Wagner and Within paper (1958)
                {
                    horizon = 12,
                    demand = new int[] { 69, 29, 36, 61, 61, 26, 34, 67, 45, 67, 79, 56 },
                    inventoryCosts = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                    setupCosts = new int[] { 85, 102, 102, 101, 98, 114, 105, 86, 119, 110, 98, 114 }
                };

            WagnerWithin w = new WagnerWithin(context);

            for (int i = 1; i <= 12; i++) Console.WriteLine("Minimum costs from period 1 to " + i + " : " + w.MinimalCosts[i - 1]);
            Console.WriteLine();
            for (int i = 1; i <= 12; i++) Console.WriteLine("Production at period " + i + " : " + w.OptimalProductionQuantity[i - 1]);
            Console.WriteLine();
            for (int i = 1; i <= 12; i++) Console.WriteLine("Inventory at period " + i + " : " + w.OptimalInventory[i - 1]);

            Console.Read();
        }
    }
}
