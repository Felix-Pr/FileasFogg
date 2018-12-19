namespace GUI
{
    partial class PhileasFogg
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhileasFogg));
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.loadFromFileButton = new System.Windows.Forms.Button();
            this.wagnerWhitinButton = new System.Windows.Forms.Button();
            this.exportResultsButton = new System.Windows.Forms.Button();
            this.exportResultsDialog = new System.Windows.Forms.SaveFileDialog();
            this.generateCsvTemplateButton = new System.Windows.Forms.Button();
            this.generateCsvTemplateDialog = new System.Windows.Forms.SaveFileDialog();
            this.variablesDataGrid = new System.Windows.Forms.DataGridView();
            this.constantsDataGrid = new System.Windows.Forms.DataGridView();
            this.resultsDataGrid = new System.Windows.Forms.DataGridView();
            this.resultsLabel = new System.Windows.Forms.Label();
            this.dataLabel = new System.Windows.Forms.Label();
            this.settingsLabel = new System.Windows.Forms.Label();
            this.saveSettingsButton = new System.Windows.Forms.Button();
            this.saveSettingsDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.variablesDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.constantsDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // loadFromFileButton
            // 
            this.loadFromFileButton.Location = new System.Drawing.Point(12, 12);
            this.loadFromFileButton.Name = "loadFromFileButton";
            this.loadFromFileButton.Size = new System.Drawing.Size(265, 56);
            this.loadFromFileButton.TabIndex = 2;
            this.loadFromFileButton.Text = "Load from File";
            this.loadFromFileButton.UseVisualStyleBackColor = true;
            this.loadFromFileButton.Click += new System.EventHandler(this.loadFromFileButton_Click);
            // 
            // wagnerWhitinButton
            // 
            this.wagnerWhitinButton.Location = new System.Drawing.Point(677, 12);
            this.wagnerWhitinButton.Name = "wagnerWhitinButton";
            this.wagnerWhitinButton.Size = new System.Drawing.Size(303, 55);
            this.wagnerWhitinButton.TabIndex = 4;
            this.wagnerWhitinButton.Text = "Compute Planning with Wagner Whitin";
            this.wagnerWhitinButton.UseVisualStyleBackColor = true;
            this.wagnerWhitinButton.Click += new System.EventHandler(this.wagnerWhitinButton_Click);
            // 
            // exportResultsButton
            // 
            this.exportResultsButton.Enabled = false;
            this.exportResultsButton.Location = new System.Drawing.Point(986, 12);
            this.exportResultsButton.Name = "exportResultsButton";
            this.exportResultsButton.Size = new System.Drawing.Size(409, 56);
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
            this.generateCsvTemplateButton.Location = new System.Drawing.Point(283, 12);
            this.generateCsvTemplateButton.Name = "generateCsvTemplateButton";
            this.generateCsvTemplateButton.Size = new System.Drawing.Size(191, 55);
            this.generateCsvTemplateButton.TabIndex = 7;
            this.generateCsvTemplateButton.Text = "Generate Settings Template";
            this.generateCsvTemplateButton.UseVisualStyleBackColor = true;
            this.generateCsvTemplateButton.Click += new System.EventHandler(this.generateCsvTemplateButton_Click);
            // 
            // generateCsvTemplateDialog
            // 
            this.generateCsvTemplateDialog.DefaultExt = "csv";
            this.generateCsvTemplateDialog.FileName = "ContextTemplate.csv";
            this.generateCsvTemplateDialog.Filter = "csv File|*.csv";
            this.generateCsvTemplateDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.generateCsvTemplateDialog_FileOk);
            // 
            // variablesDataGrid
            // 
            this.variablesDataGrid.AllowUserToAddRows = false;
            this.variablesDataGrid.AllowUserToDeleteRows = false;
            this.variablesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.variablesDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.variablesDataGrid.Location = new System.Drawing.Point(3, 260);
            this.variablesDataGrid.MultiSelect = false;
            this.variablesDataGrid.Name = "variablesDataGrid";
            this.variablesDataGrid.RowHeadersVisible = false;
            this.variablesDataGrid.RowTemplate.Height = 28;
            this.variablesDataGrid.Size = new System.Drawing.Size(667, 408);
            this.variablesDataGrid.TabIndex = 8;
            this.variablesDataGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_EditingControlShowing);
            // 
            // constantsDataGrid
            // 
            this.constantsDataGrid.AllowUserToAddRows = false;
            this.constantsDataGrid.AllowUserToDeleteRows = false;
            this.constantsDataGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.ColumnHeader;
            this.constantsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.constantsDataGrid.DefaultCellStyle = dataGridViewCellStyle1;
            this.constantsDataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.constantsDataGrid.Location = new System.Drawing.Point(3, 121);
            this.constantsDataGrid.MultiSelect = false;
            this.constantsDataGrid.Name = "constantsDataGrid";
            this.constantsDataGrid.RowHeadersVisible = false;
            this.constantsDataGrid.RowTemplate.Height = 28;
            this.constantsDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.constantsDataGrid.Size = new System.Drawing.Size(1392, 91);
            this.constantsDataGrid.TabIndex = 9;
            this.constantsDataGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.Horizon_CellValueChanged);
            this.constantsDataGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_EditingControlShowing);
            // 
            // resultsDataGrid
            // 
            this.resultsDataGrid.AllowUserToAddRows = false;
            this.resultsDataGrid.AllowUserToDeleteRows = false;
            this.resultsDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.resultsDataGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.resultsDataGrid.Location = new System.Drawing.Point(677, 260);
            this.resultsDataGrid.Name = "resultsDataGrid";
            this.resultsDataGrid.ReadOnly = true;
            this.resultsDataGrid.RowHeadersVisible = false;
            this.resultsDataGrid.RowTemplate.Height = 28;
            this.resultsDataGrid.Size = new System.Drawing.Size(718, 408);
            this.resultsDataGrid.TabIndex = 10;
            // 
            // resultsLabel
            // 
            this.resultsLabel.AutoSize = true;
            this.resultsLabel.Location = new System.Drawing.Point(673, 225);
            this.resultsLabel.Name = "resultsLabel";
            this.resultsLabel.Size = new System.Drawing.Size(63, 20);
            this.resultsLabel.TabIndex = 11;
            this.resultsLabel.Text = "Results";
            // 
            // dataLabel
            // 
            this.dataLabel.AutoSize = true;
            this.dataLabel.Location = new System.Drawing.Point(3, 225);
            this.dataLabel.Name = "dataLabel";
            this.dataLabel.Size = new System.Drawing.Size(48, 20);
            this.dataLabel.TabIndex = 12;
            this.dataLabel.Text = "Data ";
            // 
            // settingsLabel
            // 
            this.settingsLabel.AutoSize = true;
            this.settingsLabel.Location = new System.Drawing.Point(3, 86);
            this.settingsLabel.Name = "settingsLabel";
            this.settingsLabel.Size = new System.Drawing.Size(68, 20);
            this.settingsLabel.TabIndex = 13;
            this.settingsLabel.Text = "Settings";
            // 
            // saveSettingsButton
            // 
            this.saveSettingsButton.Location = new System.Drawing.Point(480, 12);
            this.saveSettingsButton.Name = "saveSettingsButton";
            this.saveSettingsButton.Size = new System.Drawing.Size(190, 55);
            this.saveSettingsButton.TabIndex = 15;
            this.saveSettingsButton.Text = "Save Settings";
            this.saveSettingsButton.UseVisualStyleBackColor = true;
            this.saveSettingsButton.Click += new System.EventHandler(this.saveSettingsButton_Click);
            // 
            // saveSettingsDialog
            // 
            this.saveSettingsDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.saveSettingsDialog_FileOk);
            // 
            // PhileasFogg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1893, 648);
            this.Controls.Add(this.saveSettingsButton);
            this.Controls.Add(this.settingsLabel);
            this.Controls.Add(this.dataLabel);
            this.Controls.Add(this.resultsLabel);
            this.Controls.Add(this.resultsDataGrid);
            this.Controls.Add(this.constantsDataGrid);
            this.Controls.Add(this.variablesDataGrid);
            this.Controls.Add(this.generateCsvTemplateButton);
            this.Controls.Add(this.exportResultsButton);
            this.Controls.Add(this.wagnerWhitinButton);
            this.Controls.Add(this.loadFromFileButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "PhileasFogg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phileas Fogg";
            this.Load += new System.EventHandler(this.PhileasFogg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.variablesDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.constantsDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.resultsDataGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button loadFromFileButton;
        private System.Windows.Forms.Button wagnerWhitinButton;
        private System.Windows.Forms.Button exportResultsButton;
        private System.Windows.Forms.SaveFileDialog exportResultsDialog;
        private System.Windows.Forms.Button generateCsvTemplateButton;
        private System.Windows.Forms.SaveFileDialog generateCsvTemplateDialog;
        private System.Windows.Forms.DataGridView variablesDataGrid;
        private System.Windows.Forms.DataGridView constantsDataGrid;
        private System.Windows.Forms.DataGridView resultsDataGrid;
        private System.Windows.Forms.Label resultsLabel;
        private System.Windows.Forms.Label dataLabel;
        private System.Windows.Forms.Label settingsLabel;
        private System.Windows.Forms.Button saveSettingsButton;
        private System.Windows.Forms.SaveFileDialog saveSettingsDialog;
    }
}

