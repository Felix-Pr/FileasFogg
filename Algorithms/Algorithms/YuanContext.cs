using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Algorithms
{
    public class YuanContext
    {
        public int horizon { get; set; } //horizon length
        public List<int> demand { get; set; } //demand
        public double sellingPrice { get; set; } //product price
        public List<double> inventoryCosts { get; set; } //inventory cost
        public double p { get; set; } //production cost
        public List<double> setupCosts { get; set; } //setup cost
        public double a { get; set; } //unit raw material cost
        public int rc { get; set; } //delay in payment from client
        public int rf { get; set; } //delay in payment to supplier
        public double alpha { get; set; } //discount rate per period
        public double beta { get; set; } //interest rate for financing OWCR per period

        public YuanContext() { }

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
                            horizon = int.Parse(values[i]);
                            break;
                        case ("Selling Price"):
                            sellingPrice = int.Parse(values[i]);
                            break;
                        case ("Inventory Cost"): //DETERMINER SI TABLEAU OU VALEUR UNIQUE
                            inventoryCostColumnIndex = i;
                            inventoryCosts = new List<double>() { double.Parse(values[i]) };
                            break;
                        case ("Production Cost"):
                            p = int.Parse(values[i]);
                            break;
                        case ("Setup Cost"):
                            setupCostColumnIndex = i;
                            setupCosts = new List<double> { double.Parse(values[i]) };
                            break;
                        case ("Unit Material Cost"):
                            a = int.Parse(values[i]);
                            break;
                        case ("Delay in payment to Supplier"):
                            rf = int.Parse(values[i]);
                            break;
                        case ("Delay in payment from Client"):
                            rc = int.Parse(values[i]);
                            break;
                        case ("Discount Rate"): //alpha
                            alpha = double.Parse(values[i]);
                            break;
                        case ("Interest Rate"): //beta
                            beta = double.Parse(values[i]);
                            break;
                        case ("Demand"): 
                            demandColumnIndex = i;
                            demand = new List<int>() { int.Parse(values[i])};
                            break;
                    }
                }

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(',');
                    var numbers = line.Length;
                    demand.Add(int.Parse(line[demandColumnIndex]));
                    setupCosts.Add(int.Parse(line[setupCostColumnIndex]));
                    inventoryCosts.Add(int.Parse(line[inventoryCostColumnIndex]));
                }
            }
        }

        public YuanContext(int horizonLength, DataGridView constantsDataGridView, DataGridView variablesDataGridView)
        {
            alpha = double.Parse(constantsDataGridView.Rows[0].Cells["Alpha"].Value.ToString());
            beta = double.Parse(constantsDataGridView.Rows[0].Cells["Beta"].Value.ToString());
            rc = int.Parse(constantsDataGridView.Rows[0].Cells["Delay in Payment from Client"].Value.ToString());
            rf = int.Parse(constantsDataGridView.Rows[0].Cells["Delay in Payment to Supplier"].Value.ToString());
            a = double.Parse(constantsDataGridView.Rows[0].Cells["Unit Material Cost"].Value.ToString());
            p = double.Parse(constantsDataGridView.Rows[0].Cells["Production Cost"].Value.ToString());
            sellingPrice = double.Parse(constantsDataGridView.Rows[0].Cells["Selling Price"].Value.ToString());
            horizon = horizonLength;
            setupCosts = new List<double>(horizon);
            inventoryCosts = new List<double>(horizon);
            demand = new List<int>(horizon);

            for (int i = 1; i<=horizon; i++)
            {
                inventoryCosts.Add(double.Parse(variablesDataGridView.Rows[i-1].Cells["Inventory Costs"].Value.ToString()));
                demand.Add(int.Parse(variablesDataGridView.Rows[i-1].Cells["Demand"].Value.ToString()));
                setupCosts.Add(double.Parse(variablesDataGridView.Rows[i-1].Cells["Setup Costs"].Value.ToString()));
            };

        }
    }
}
