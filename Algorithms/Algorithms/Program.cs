using System;

namespace Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            WagnerWithin w = new WagnerWithin();
            for (int i = 1; i <= w.numberOfPeriods; i++) Console.WriteLine("Coûts minimums sur la plage 1 à " + i + " : " + w.MinimalCosts[i - 1]);
            Console.Read();
        }
    }
}
