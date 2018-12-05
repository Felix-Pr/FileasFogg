using Algorithms;
using System;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace GUI
{
    public partial class WagnerWithinVisualisation : Form
    {
        private int[] minimalCosts;
        private int[] optimalProductionQuantity;
        private int[] optimalInventory;

        private YuanContext context ;

        public WagnerWithinVisualisation(int[] minimalCosts, int[] optimalProductionQuantity, int[] optimalInventory)
        {
            InitializeComponent();
            this.minimalCosts = minimalCosts;
            this.optimalInventory = optimalInventory;
            this.optimalProductionQuantity = optimalProductionQuantity;
        }
        public WagnerWithinVisualisation(WagnerWithin w)
        {
            InitializeComponent();
            this.minimalCosts = w.MinimalCosts;
            this.optimalInventory = w.OptimalInventory;
            this.optimalProductionQuantity = w.OptimalProductionQuantity;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void ShowWagnerWhitinResults()
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

            for (int i = 1; i <= minimalCosts.Length; i++)
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
        private void loadFromFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            context = new YuanContext(openFileDialog1.FileName);

            wagnerWhitinButton.Enabled = true;

            richTextBox.AppendText("Horizon : " + context.T);
            richTextBox.AppendText("\n");
            richTextBox.AppendText("Inventory Cost : ");
            foreach (int i in context.inventoryCost) richTextBox.AppendText(i + ", ");
            richTextBox.AppendText("\n");
            richTextBox.AppendText("Setup Cost : ");
            foreach (int s in context.setupCost) richTextBox.AppendText(s + ", ");
            richTextBox.AppendText("\n");
            richTextBox.AppendText("Production Cost : " + context.productionCost);
            richTextBox.AppendText("\n");
            richTextBox.AppendText("Unit Material Cost : " + context.productionCost);
            richTextBox.AppendText("\n");
            richTextBox.AppendText("Discount Rate : " + context.alpha);
            richTextBox.AppendText("\n");
            richTextBox.AppendText("Interest Rate : " + context.beta);
            richTextBox.AppendText("\n");
            richTextBox.AppendText("Selling Price : " + context.sellingPrice);
            richTextBox.AppendText("\n");
            richTextBox.AppendText("Delay in payment from Client : " + context.delayInPaymentFromClient);
            richTextBox.AppendText("\n");
            richTextBox.AppendText("Delay in payment to Supplier : " + context.delayInPaymentToSupplier);
            richTextBox.AppendText("\n");
            richTextBox.AppendText("Demand : ");
            foreach (int d in context.demand) richTextBox.AppendText(d + ", ");


        }

        private void wagnerWhitinButton_Click(object sender, EventArgs e)
        {
            WagnerWithin w = new WagnerWithin(context);

            richTextBox.AppendText("\n");
            richTextBox.AppendText("\n");
            richTextBox.AppendText("Wagner Whitin Algorithm results :");
            richTextBox.AppendText("\n");
            richTextBox.AppendText("\n");
            for (int i = 1; i <= context.T; i++) richTextBox.AppendText("Minimum costs from period 1 to " + i + " : " + w.MinimalCosts[i - 1]+"\n");
            richTextBox.AppendText("\n");
            for (int i = 1; i <= context.T; i++) richTextBox.AppendText("Production at period " + i + " : " + w.OptimalProductionQuantity[i - 1]+"\n");
            richTextBox.AppendText("\n");
            for (int i = 1; i <= context.T; i++) richTextBox.AppendText("Inventory at period " + i + " : " + w.OptimalInventory[i - 1]+ "\n");

            ShowWagnerWhitinResults();
            exportResultsButton.Enabled = true;
        }

        private void exportResultsButton_Click(object sender, EventArgs e)
        {
            exportResultsDialog.ShowDialog();
        }

        private string ConvertResultsIntoCsvString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("Period");
            for (int i = 1; i <= context.T; i++) s.Append("," + i);
            s.Append("\n");
            s.Append("Production");
            for (int i = 1; i <= context.T; i++) s.Append("," + optimalProductionQuantity[i - 1]);
            s.Append("\n");
            s.Append("Costs");
            for (int i = 1; i <= context.T; i++) s.Append("," + minimalCosts[i - 1]);
            s.Append("\n");
            s.Append("Inventory");
            for (int i = 1; i <= context.T; i++) s.Append("," + optimalInventory[i - 1]);

            return s.ToString();
        }

        private void exportResultsDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(exportResultsDialog.FileName))
            {
                file.Write(ConvertResultsIntoCsvString());
            }
        }

        private void generateCsvTemplateButton_Click(object sender, EventArgs e)
        {
            generateCsvTemplateDialog.ShowDialog();
        }

        private void generateCsvTemplateDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(generateCsvTemplateDialog.FileName))
            {
                file.WriteLine("Horizon,Demand,Inventory Cost,Setup Cost,Production Cost,Unit Material Cost,Delay in payment to Supplier,Delay in payment from Client,Discount Rate,Interest Rate");
                file.WriteLine("12,69,1,85,1,3,1,1,1,1");
                file.WriteLine(",29,1,102,,,,,,");
                file.WriteLine(",36,1,102,,,,,,");
                file.WriteLine(",61,1,101,,,,,,");
                file.WriteLine(",61,1,98,,,,,,");
                file.WriteLine(",26,1,114,,,,,,");
                file.WriteLine(",34,1,105,,,,,,");
                file.WriteLine(",67,1,86,,,,,,");
                file.WriteLine(",45,1,119,,,,,,");
                file.WriteLine(",67,1,110,,,,,,");
                file.WriteLine(",79,1,98,,,,,,");
                file.WriteLine(",56,1,114,,,,,,");
            }
        }
    }
}
