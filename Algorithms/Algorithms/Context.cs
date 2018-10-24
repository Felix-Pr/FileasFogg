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
        public int[] setupCosts { get; set; }
        public int[] inventoryCosts { get; set; }
        public int unitProductPrice{ get; set; }
        public int unitProductionCost{ get; set; }
        public int delayOfPaymentFromClient{ get; set; }
        public int delayOfPaymentToSupplier{ get; set; }
        public int discountRatePerPeriod{ get; set; }
        public int interestRatePerPeriod{ get; set; }
        public int productionCapacity { get; set; } //0 = infinite capacity

        public Context(){ }


    }
}
