using Algorithms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GUI
{
    public partial class PhileasFogg : Form
    {
        private static int initialHorizonLength = 5;

        private double[] minimalCosts = new double[initialHorizonLength];
        private int[] optimalProductionQuantity = new int[initialHorizonLength];
        private int[] optimalInventory = new int[initialHorizonLength];

        private Queue<DataGridViewRow> deletedRows = new Queue<DataGridViewRow>();

        private readonly DataGridViewRow firstRow = new DataGridViewRow();

        private YuanContext context;

        public PhileasFogg() { InitializeComponent(); }


        private void PhileasFogg_Load(object sender, EventArgs e)
        {
            GenerateColumns();
            constantsDataGrid.Rows.Add(new DataGridViewRow());
            constantsDataGrid.Rows[0].SetValues(initialHorizonLength,0,0,0,0,0,0,0);
        }

        private void GenerateColumns()
        {
            DataGridViewTextBoxColumn horizon = new DataGridViewTextBoxColumn()
            {
                ValueType = typeof(int),
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Name = "Horizon",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            DataGridViewTextBoxColumn sellingPrice = new DataGridViewTextBoxColumn()
            {
                ValueType = typeof(double),
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Name = "Selling Price"
            };
            DataGridViewTextBoxColumn productionCost = new DataGridViewTextBoxColumn()
            {
                ValueType = typeof(double),
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Name = "Production Cost"
            };
            DataGridViewTextBoxColumn unitMaterialCost = new DataGridViewTextBoxColumn()
            {
                ValueType = typeof(double),
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Name = "Unit Material Cost"
            };
            DataGridViewTextBoxColumn delayInPaymentFromClient = new DataGridViewTextBoxColumn()
            {
                ValueType = typeof(int),
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Name = "Delay in Payment from Client"
            };
            DataGridViewTextBoxColumn delayInPaymentToSupplier = new DataGridViewTextBoxColumn()
            {
                ValueType = typeof(int),
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Name = "Delay in Payment to Supplier"
            };
            DataGridViewTextBoxColumn alpha = new DataGridViewTextBoxColumn()
            {
                ValueType = typeof(double),
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Name = "Alpha"
            };
            DataGridViewTextBoxColumn beta = new DataGridViewTextBoxColumn()
            {
                ValueType = typeof(double),
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Name = "Beta"
            };

            constantsDataGrid.Columns.AddRange(
               horizon,
               productionCost,
               sellingPrice,
               unitMaterialCost,
               delayInPaymentFromClient,
               delayInPaymentToSupplier,
               alpha,
               beta);

            DataGridViewTextBoxColumn period = new DataGridViewTextBoxColumn()
            {
                ValueType = typeof(int),
                Name = "Period",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                ReadOnly = true,


            };
            DataGridViewTextBoxColumn demand = new DataGridViewTextBoxColumn()
            {
                ValueType = typeof(int),
                Name = "Demand",
                SortMode = DataGridViewColumnSortMode.NotSortable,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            DataGridViewTextBoxColumn inventoryCosts = new DataGridViewTextBoxColumn()
            {
                ValueType = typeof(double),
                SortMode = DataGridViewColumnSortMode.NotSortable,
                Name = "Inventory Costs"
            };
            DataGridViewTextBoxColumn setupCosts = new DataGridViewTextBoxColumn()
            {
                ValueType = typeof(double),
                Name = "Setup Costs",
                SortMode = DataGridViewColumnSortMode.NotSortable
            };

            


            variablesDataGrid.Columns.AddRange(period, demand, inventoryCosts, setupCosts);

            DataGridViewTextBoxColumn resultPeriod = new DataGridViewTextBoxColumn()
            {
                ValueType = typeof(int),
                Name = "Period"
            };
            DataGridViewTextBoxColumn productionQuantity = new DataGridViewTextBoxColumn()
            {
                ValueType = typeof(int),
                Name = "Production Quantity",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            DataGridViewTextBoxColumn inventory = new DataGridViewTextBoxColumn()
            {
                ValueType = typeof(int),
                Name = "Inventory"
            };
            DataGridViewTextBoxColumn costs = new DataGridViewTextBoxColumn()
            {
                ValueType = typeof(double),
                Name = "Costs"
            };
            resultsDataGrid.Columns.AddRange(resultPeriod, productionQuantity, inventory, costs);
            foreach (DataGridViewRow row in variablesDataGrid.Rows) row.SetValues(row.Cells[0].Value, 0, 0, 0);
        }

        private void LoadDataGrid(object[] constantRow, List<object[]> variableRows)
        {
            constantsDataGrid.Rows.Clear();
            variablesDataGrid.Rows.Clear();
            constantsDataGrid.Rows.Add(CreateConstantsRow(constantRow));
            foreach (object[] row in variableRows) variablesDataGrid.Rows.Add(CreateVariableRow(row));
        }

        private void LoadDataGrid(string filePath)
        {
            object[] constantRow = new object[8];
            List<object[]> variableRows = new List<object[]>();

            using (var reader = new StreamReader(filePath))
            {
                reader.ReadLine(); //Header row
                int period = 1;
                object[] firstRow = reader.ReadLine().Split(',');

                constantRow = SubArray(firstRow, 3, 8);
                object[] variableRow = new object[4];
                variableRow[0] = period;
                SubArray(firstRow, 0, 3).CopyTo(variableRow, 1);
                variableRows.Add(variableRow);

                while (!reader.EndOfStream)
                {
                    period++;
                    variableRow = new object[4];
                    variableRow[0] = period;
                    SubArray(reader.ReadLine().Split(','), 0, 3).CopyTo(variableRow, 1);
                    variableRows.Add(variableRow);
                }
            }

            LoadDataGrid(constantRow, variableRows);

        }

        private static T[] SubArray<T>(T[] data, int index, int length)
        {
            T[] result = new T[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        private DataGridViewRow CreateConstantsRow(object[] row)
        {
            DataGridViewRow dataRow = new DataGridViewRow();
            dataRow.CreateCells(constantsDataGrid);
            dataRow.SetValues(row);

            return dataRow;
        }
        private DataGridViewRow CreateResultsRow(object[] row)
        {
            DataGridViewRow dataRow = new DataGridViewRow();
            dataRow.CreateCells(resultsDataGrid);
            dataRow.SetValues(row);

            return dataRow;
        }
        private DataGridViewRow CreateVariableRow(object[] row)
        {
            DataGridViewRow dataRow = new DataGridViewRow();
            dataRow.CreateCells(variablesDataGrid);
            dataRow.SetValues(row);

            return dataRow;
        }

        private string ConvertResultsIntoCsvString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("Period");
            for (int i = 1; i <= context.horizon; i++) s.Append("," + i);
            s.Append("\n");
            s.Append("Production");
            for (int i = 1; i <= context.horizon; i++) s.Append("," + optimalProductionQuantity[i - 1]);
            s.Append("\n");
            s.Append("Costs");
            for (int i = 1; i <= context.horizon; i++) s.Append("," + minimalCosts[i - 1]);
            s.Append("\n");
            s.Append("Inventory");
            for (int i = 1; i <= context.horizon; i++) s.Append("," + optimalInventory[i - 1]);

            return s.ToString();
        }

        private void exportResultsDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (HasWriteAccessToFolder(Path.GetDirectoryName(exportResultsDialog.FileName)))
            {
                using (StreamWriter file = new StreamWriter(exportResultsDialog.FileName))
                {
                    file.Write(ConvertResultsIntoCsvString());
                }
            }
            else MessageBox.Show("Write access to folder denied");

        }

        private void loadFromFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                context = new YuanContext(openFileDialog.FileName);
            }
            catch
            {
                openFileDialog.Dispose();
                MessageBox.Show("Incorrect file completion : please make sure the template is correctly filled");
                return;
            }
            deletedRows = new Queue<DataGridViewRow>();
            LoadDataGrid(openFileDialog.FileName);
        }

        private void wagnerWhitinButton_Click(object sender, EventArgs e)
        {
            try
            {
                WagnerWithin w = new WagnerWithin(new YuanContext(constantsDataGrid, variablesDataGrid));
                minimalCosts = w.MinimalCosts;
                optimalInventory = w.OptimalInventory;
                optimalProductionQuantity = w.OptimalProductionQuantity;
                ShowResults();
                exportResultsButton.Enabled = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ShowResults()
        {
            resultsDataGrid.Rows.Clear();
            for (int i = 0; i < minimalCosts.Length; i++)
            {
                resultsDataGrid.Rows.Add(CreateResultsRow(new object[] {
                    i+1,
                    optimalProductionQuantity[i],
                    optimalInventory[i],
                    minimalCosts[i]}));
            }
        }

        private void exportResultsButton_Click(object sender, EventArgs e)
        {
            exportResultsDialog.ShowDialog();
        }
        private void generateCsvTemplateButton_Click(object sender, EventArgs e)
        {
            generateCsvTemplateDialog.ShowDialog();
        }

        private void generateCsvTemplateDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            using (StreamWriter file =
            new StreamWriter(generateCsvTemplateDialog.FileName))
            {
                file.WriteLine("Demand,Inventory Cost,Setup Cost,Horizon,Production Cost,Selling Price,Unit Material Cost,Delay in payment to Supplier,Delay in payment from Client,Discount Rate,Interest Rate");
                file.WriteLine("69,1,85,12,3,10,1,1,1,1,1");
                file.WriteLine("29,1,102,,,,,,,,");
                file.WriteLine("36,1,102,,,,,,,,");
                file.WriteLine("61,1,101,,,,,,,,");
                file.WriteLine("61,1,98,,,,,,,,");
                file.WriteLine("26,1,114,,,,,,,,");
                file.WriteLine("34,1,105,,,,,,,,");
                file.WriteLine("67,1,86,,,,,,,,");
                file.WriteLine("45,1,119,,,,,,,,");
                file.WriteLine("67,1,110,,,,,,,,");
                file.WriteLine("79,1,98,,,,,,,,");
                file.WriteLine("56,1,114,,,,,,,,");
            }
        }


        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyPress -= new KeyPressEventHandler(Column_KeyPress);
            TextBox tb = e.Control as TextBox;
            if (tb != null)
            {
                tb.KeyPress += new KeyPressEventHandler(Column_KeyPress);
            }
        }

        private void Column_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void IntegerColumn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Settings_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                Horizon_CellValueChanged(sender, e);
                return;
            }
            if (constantsDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "") constantsDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;



        }
        private void Horizon_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

            if (constantsDataGrid.CurrentCell.ColumnIndex == 0 && constantsDataGrid.CurrentCell.Value != null)
            {
                int newHorizon = int.Parse(constantsDataGrid.Rows[0].Cells["Horizon"].Value.ToString());
                int latestHorizon = variablesDataGrid.Rows.Count;

                if (newHorizon > latestHorizon)
                {
                    for (int i = 0; i < newHorizon - latestHorizon; i++)
                    {
                        if (deletedRows.Count > 0)
                        {
                            variablesDataGrid.Rows.Add(deletedRows.Dequeue());
                            variablesDataGrid.Rows[variablesDataGrid.Rows.Count - 1].Cells[0].Value = variablesDataGrid.Rows.Count;
                        }
                        else
                        {
                            variablesDataGrid.Rows.Add(CreateVariableRow(new object[] { latestHorizon + i + 1, 0, 0, 0 }));
                        }
                    }
                }
                else if (newHorizon < latestHorizon)
                {
                    for (int i = 0; i < latestHorizon - newHorizon; i++)
                    {
                        if (VariableRowIsNotEmpty(newHorizon)) deletedRows.Enqueue(variablesDataGrid.Rows[newHorizon]);
                        variablesDataGrid.Rows.RemoveAt(newHorizon);
                    }
                }
            }

        }

        private bool VariableRowIsNotEmpty(int rowIndex)
        {
            return !(variablesDataGrid.Rows[rowIndex].Cells["Demand"].Value.ToString() == "0"
                            && variablesDataGrid.Rows[rowIndex].Cells["Setup Costs"].Value.ToString() == "0"
                            && variablesDataGrid.Rows[rowIndex].Cells["Inventory Costs"].Value.ToString() == "0");
        }

        private bool HasWriteAccessToFolder(string folderPath)
        {
            try
            {
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }

        }

        private void saveSettingsButton_Click(object sender, EventArgs e)
        {
            saveSettingsDialog.ShowDialog();
        }

        private void saveSettingsDialog_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveSettings(saveSettingsDialog.FileName);
        }

        private void SaveSettings(string filePath)
        {
            string[] headerRow;
            string[] firstRow;
            List<string[]> rows;
            try
            {
                headerRow = "Demand,Inventory Cost,Setup Cost,Horizon,Production Cost,Selling Price,Unit Material Cost,Delay in payment to Supplier,Delay in payment from Client,Discount Rate,Interest Rate".Split(',');
                firstRow = new string[11];
                firstRow[0] = variablesDataGrid.Rows[0].Cells["Demand"].Value.ToString();
                firstRow[1] = variablesDataGrid.Rows[0].Cells["Inventory Costs"].Value.ToString();
                firstRow[2] = variablesDataGrid.Rows[0].Cells["Setup Costs"].Value.ToString();
                firstRow[3] = constantsDataGrid.Rows[0].Cells["Horizon"].Value.ToString();
                firstRow[4] = constantsDataGrid.Rows[0].Cells["Production Cost"].Value.ToString();
                firstRow[5] = constantsDataGrid.Rows[0].Cells["Selling Price"].Value.ToString();
                firstRow[6] = constantsDataGrid.Rows[0].Cells["Unit Material Cost"].Value.ToString();
                firstRow[7] = constantsDataGrid.Rows[0].Cells["Delay in Payment from Client"].Value.ToString();
                firstRow[8] = constantsDataGrid.Rows[0].Cells["Delay in Payment to Supplier"].Value.ToString();
                firstRow[9] = constantsDataGrid.Rows[0].Cells["Alpha"].Value.ToString();
                firstRow[10] = constantsDataGrid.Rows[0].Cells["Beta"].Value.ToString();
                rows = new List<string[]>();
                for (int i = 1; i < variablesDataGrid.Rows.Count; i++)
                {
                    rows.Add(new string[11]);
                    rows[i - 1][0] = variablesDataGrid.Rows[i].Cells["Demand"].Value.ToString();
                    rows[i - 1][1] = variablesDataGrid.Rows[i].Cells["Inventory Costs"].Value.ToString();
                    rows[i - 1][2] = variablesDataGrid.Rows[i].Cells["Setup Costs"].Value.ToString();
                }
            }
            catch { MessageBox.Show("Please fill settings before saving");
                return;
            }
            

            try
            {
                using (StreamWriter file = new StreamWriter(filePath))
                {
                    file.WriteLine(string.Join(",", headerRow));
                    file.WriteLine(string.Join(",", firstRow));
                    foreach (string[] row in rows) file.WriteLine(string.Join(",", row));
                }

            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Write access denied");
            }
           
        }

        private void yuanButton_click(object sender, EventArgs e)
        {
            {
               
                try
                {
                    YuanTry y = new YuanTry(new YuanContext(constantsDataGrid, variablesDataGrid));
                    y.InitializeArrays();
                    y.ComputeProductionPlan();
                    minimalCosts = y.minimalFinancingCosts;
                    optimalInventory = y.I[y.I.Length-1];
                    optimalProductionQuantity = y.Q[y.Q.Length-1];
                    ShowResults();
                    exportResultsButton.Enabled = true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void variablesDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (variablesDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "") variablesDataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
        }
    }
}
