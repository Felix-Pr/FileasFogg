using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI
{
    public partial class WagnerWithinVisualisation : Form
    {
        private int[] minimalCosts;
        private int[] optimalProductionQuantity;
        private int[] optimalInventory;

        public WagnerWithinVisualisation(int[] minimalCosts, int[] optimalProductionQuantity, int[] optimalInventory)
        {
            InitializeComponent();
            this.minimalCosts = minimalCosts;
            this.optimalInventory = optimalInventory;
            this.optimalProductionQuantity = optimalProductionQuantity;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Titles.Add("Wagner Within Algorithm Result");
            
            var Costs = new Series
            {
                Name = "Costs",
                ChartType = SeriesChartType.Column,
                ChartArea = "ChartArea1"
            };
            var Production = new Series
            {
                Name = "Production",
                ChartType = SeriesChartType.Column,
                ChartArea = "ChartArea2"
            };
            var Inventory = new Series
            {
                Name = "Inventory",
                ChartType = SeriesChartType.Column,
                ChartArea = "ChartArea3",

            };

            for(int i = 1; i<=minimalCosts.Length; i++)
            {
                Costs.Points.AddXY(i, minimalCosts[i - 1]);
                Production.Points.AddXY(i, optimalProductionQuantity[i - 1]);
                Inventory.Points.AddXY(i, optimalInventory[i - 1]);
            }

            chart1.Series.Add(Costs);
            chart1.Series.Add(Production);
            chart1.Series.Add(Inventory);

            chart1.Invalidate();
            chart1.Visible = true;
        }
    }
}
