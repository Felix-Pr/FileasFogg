using System;
using System.Collections.Generic;
using System.IO;

namespace Algorithms
{
    public class YuanContext
    {
        public int T { get; set; } //horizon length
        public List<int> demand { get; set; } //demand
        public int sellingPrice { get; set; } //product price
        public List<int> inventoryCost { get; set; } //inventory cost
        public int productionCost { get; set; } //production cost
        public List<int> setupCost { get; set; } //setup cost
        public int unitMaterialCost { get; set; } //unit raw material cost
        public int delayInPaymentFromClient { get; set; } //delay in payment from client
        public int delayInPaymentToSupplier { get; set; } //delay in payment to supplier
        public int alpha { get; set; } //discount rate per period
        public int beta { get; set; } //interest rate for financing OWCR per period

        public YuanContext(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                string[] columns = reader.ReadLine().Split(',');
                string[] values = reader.ReadLine().Split(',');
                int inventoryCostColumnIndex=0;
                int demandColumnIndex=0;
                int setupCostColumnIndex = 0;

                for(int i = 0; i<columns.Length; i++)
                {
                    switch (columns[i])
                    {
                        case ("Horizon"):
                            T = Int32.Parse(values[i]);
                            break;
                        case ("Selling Price"):
                            sellingPrice = Int32.Parse(values[i]);
                            break;
                        case ("Inventory Cost"): //DETERMINER SI TABLEAU OU VALEUR UNIQUE
                            inventoryCostColumnIndex = i;
                            inventoryCost = new List<int>() { Int32.Parse(values[i]) };
                            break;
                        case ("Production Cost"):
                            productionCost = Int32.Parse(values[i]);
                            break;
                        case ("Setup Cost"):
                            setupCostColumnIndex = i;
                            setupCost = new List<int> { Int32.Parse(values[i]) };
                            break;
                        case ("Unit Material Cost"):
                            unitMaterialCost = Int32.Parse(values[i]);
                            break;
                        case ("Delay in payment to Supplier"):
                            delayInPaymentToSupplier = Int32.Parse(values[i]);
                            break;
                        case ("Delay in payment from Client"):
                            delayInPaymentFromClient = Int32.Parse(values[i]);
                            break;
                        case ("Discount Rate"): //alpha
                            alpha = Int32.Parse(values[i]);
                            break;
                        case ("Interest Rate"): //beta
                            beta = Int32.Parse(values[i]);
                            break;
                        case ("Demand"): 
                            demandColumnIndex = i;
                            demand = new List<int>() { Int32.Parse(values[i])};
                            break;
                    }
                }

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(',');
                    var numbers = line.Length;
                    demand.Add(Int32.Parse(line[demandColumnIndex]));
                    setupCost.Add(Int32.Parse(line[setupCostColumnIndex]));
                    inventoryCost.Add(Int32.Parse(line[inventoryCostColumnIndex]));
                }
            }

        }



    }
}
