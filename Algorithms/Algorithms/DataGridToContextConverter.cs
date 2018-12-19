using System.Collections.Generic;
using System.Windows.Forms;

namespace Algorithms
{
    static class DataGridToContextConverter
    {
        public static YuanContext ConvertIntoContext(DataGridView constants, DataGridView variables)
        {
            int horizon = (int)constants.Rows[0].Cells[0].Value;
            List<int> demand = new List<int>(horizon);
            List<double> inventoryCosts = new List<double>(horizon);
            List<double> setupCosts = new List<double>(horizon);

            for(int i = 0; i<horizon; i++)
            {
                demand.Add((int)variables.Rows[i].Cells[0].Value);
                inventoryCosts.Add((int)variables.Rows[i].Cells[1].Value);
                setupCosts.Add((int)variables.Rows[i].Cells[2].Value);
            }

            return new YuanContext()
            {
                horizon = horizon,
                productionCost = (int)constants.Rows[0].Cells[1].Value,
                sellingPrice = (int)constants.Rows[0].Cells[2].Value,
                unitMaterialCost = (int)constants.Rows[0].Cells[3].Value,
                delayInPaymentFromClient = (int)constants.Rows[0].Cells[4].Value,
                delayInPaymentToSupplier = (int)constants.Rows[0].Cells[5].Value,
                alpha = (int)constants.Rows[0].Cells[6].Value,
                beta = (int)constants.Rows[0].Cells[7].Value,
                demand = demand,
                inventoryCosts = inventoryCosts,
                setupCosts = setupCosts
            };



            
        }
    }
}
