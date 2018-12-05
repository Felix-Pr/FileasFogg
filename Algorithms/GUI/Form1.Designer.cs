namespace GUI
{
    partial class WagnerWithinVisualisation
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea7 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea8 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.loadFromFileButton = new System.Windows.Forms.Button();
            this.wagnerWhitinButton = new System.Windows.Forms.Button();
            this.exportResultsButton = new System.Windows.Forms.Button();
            this.exportResultsDialog = new System.Windows.Forms.SaveFileDialog();
            this.generateCsvTemplateButton = new System.Windows.Forms.Button();
            this.generateCsvTemplateDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            this.chart1.AccessibleName = "Chart";
            chartArea7.Name = "ChartArea1";
            chartArea8.Name = "ChartArea2";
            chartArea9.Name = "ChartArea3";
            this.chart1.ChartAreas.Add(chartArea7);
            this.chart1.ChartAreas.Add(chartArea8);
            this.chart1.ChartAreas.Add(chartArea9);
            legend3.Name = "Legend1";
            this.chart1.Legends.Add(legend3);
            this.chart1.Location = new System.Drawing.Point(28, 109);
            this.chart1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.chart1.Name = "chart1";
            this.chart1.Size = new System.Drawing.Size(498, 577);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(533, 109);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.ReadOnly = true;
            this.richTextBox.Size = new System.Drawing.Size(512, 577);
            this.richTextBox.TabIndex = 1;
            this.richTextBox.Text = "";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // loadFromFileButton
            // 
            this.loadFromFileButton.Location = new System.Drawing.Point(28, 14);
            this.loadFromFileButton.Name = "loadFromFileButton";
            this.loadFromFileButton.Size = new System.Drawing.Size(265, 56);
            this.loadFromFileButton.TabIndex = 2;
            this.loadFromFileButton.Text = "Load from File";
            this.loadFromFileButton.UseVisualStyleBackColor = true;
            this.loadFromFileButton.Click += new System.EventHandler(this.loadFromFileButton_Click);
            // 
            // wagnerWhitinButton
            // 
            this.wagnerWhitinButton.Enabled = false;
            this.wagnerWhitinButton.Location = new System.Drawing.Point(533, 14);
            this.wagnerWhitinButton.Name = "wagnerWhitinButton";
            this.wagnerWhitinButton.Size = new System.Drawing.Size(210, 56);
            this.wagnerWhitinButton.TabIndex = 4;
            this.wagnerWhitinButton.Text = "Calculate Planning with Wagner Whitin";
            this.wagnerWhitinButton.UseVisualStyleBackColor = true;
            this.wagnerWhitinButton.Click += new System.EventHandler(this.wagnerWhitinButton_Click);
            // 
            // exportResultsButton
            // 
            this.exportResultsButton.Enabled = false;
            this.exportResultsButton.Location = new System.Drawing.Point(773, 14);
            this.exportResultsButton.Name = "exportResultsButton";
            this.exportResultsButton.Size = new System.Drawing.Size(164, 56);
            this.exportResultsButton.TabIndex = 5;
            this.exportResultsButton.Text = "Export Results";
            this.exportResultsButton.UseVisualStyleBackColor = true;
            this.exportResultsButton.Click += new System.EventHandler(this.exportResultsButton_Click);
            // 
            // exportResultsDialog
            // 
            this.exportResultsDialog.DefaultExt = "csv";
            this.exportResultsDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.exportResultsDialog_FileOk);
            // 
            // generateCsvTemplateButton
            // 
            this.generateCsvTemplateButton.Location = new System.Drawing.Point(320, 15);
            this.generateCsvTemplateButton.Name = "generateCsvTemplateButton";
            this.generateCsvTemplateButton.Size = new System.Drawing.Size(188, 55);
            this.generateCsvTemplateButton.TabIndex = 7;
            this.generateCsvTemplateButton.Text = "Generate Csv Template";
            this.generateCsvTemplateButton.UseVisualStyleBackColor = true;
            this.generateCsvTemplateButton.Click += new System.EventHandler(this.generateCsvTemplateButton_Click);
            // 
            // generateCsvTemplateDialog
            // 
            this.generateCsvTemplateDialog.DefaultExt = "csv";
            this.generateCsvTemplateDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.generateCsvTemplateDialog_FileOk);
            // 
            // WagnerWithinVisualisation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 750);
            this.Controls.Add(this.generateCsvTemplateButton);
            this.Controls.Add(this.exportResultsButton);
            this.Controls.Add(this.wagnerWhitinButton);
            this.Controls.Add(this.loadFromFileButton);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.chart1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "WagnerWithinVisualisation";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button loadFromFileButton;
        private System.Windows.Forms.Button wagnerWhitinButton;
        private System.Windows.Forms.Button exportResultsButton;
        private System.Windows.Forms.SaveFileDialog exportResultsDialog;
        private System.Windows.Forms.Button generateCsvTemplateButton;
        private System.Windows.Forms.SaveFileDialog generateCsvTemplateDialog;
    }
}

