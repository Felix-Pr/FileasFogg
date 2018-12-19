using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms
{
    public class Context
    {
        public int horizon { get; set; }
        public int[] demand { get; set; }
        public double[] setupCosts { get; set; }
        public double[] inventoryCosts { get; set; }
        public double unitProductPrice { get; set; }
        public double unitProductionCost { get; set; }
        public int delayOfPaymentFromClient{ get; set; }
        public int delayOfPaymentToSupplier{ get; set; }
        public double discountRatePerPeriod { get; set; }
        public double interestRatePerPeriod { get; set; }
        public int productionCapacity { get; set; } //0 = infinite capacity

        public Context(){ }


    }
}
