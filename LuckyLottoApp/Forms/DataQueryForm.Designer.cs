namespace LuckyLottoApp.Forms
{
    partial class DataQueryForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.AscRadioButton = new System.Windows.Forms.RadioButton();
            this.DescRadioButton = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.TableNameComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.QueryDataGridView = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QueryDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.AscRadioButton);
            this.panel1.Controls.Add(this.DescRadioButton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.TableNameComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 53);
            this.panel1.TabIndex = 0;
            // 
            // AscRadioButton
            // 
            this.AscRadioButton.AutoSize = true;
            this.AscRadioButton.Location = new System.Drawing.Point(482, 16);
            this.AscRadioButton.Name = "AscRadioButton";
            this.AscRadioButton.Size = new System.Drawing.Size(73, 19);
            this.AscRadioButton.TabIndex = 4;
            this.AscRadioButton.Text = "오름차순";
            this.AscRadioButton.UseVisualStyleBackColor = true;
            // 
            // DescRadioButton
            // 
            this.DescRadioButton.AutoSize = true;
            this.DescRadioButton.Checked = true;
            this.DescRadioButton.Location = new System.Drawing.Point(393, 16);
            this.DescRadioButton.Name = "DescRadioButton";
            this.DescRadioButton.Size = new System.Drawing.Size(73, 19);
            this.DescRadioButton.TabIndex = 3;
            this.DescRadioButton.TabStop = true;
            this.DescRadioButton.Text = "내림차순";
            this.DescRadioButton.UseVisualStyleBackColor = true;
            this.DescRadioButton.CheckedChanged += new System.EventHandler(this.DescRadioButton_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(328, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "정렬방법:";
            // 
            // TableNameComboBox
            // 
            this.TableNameComboBox.FormattingEnabled = true;
            this.TableNameComboBox.Items.AddRange(new object[] {
            "BasicTbl",
            "ForeignTbl",
            "AppearTbl",
            "ChulsuTbl",
            "TypeTbl",
            "SingleTbl",
            "FixChulsuTbl",
            "NonChulsuTbl"});
            this.TableNameComboBox.Location = new System.Drawing.Point(84, 15);
            this.TableNameComboBox.Name = "TableNameComboBox";
            this.TableNameComboBox.Size = new System.Drawing.Size(191, 23);
            this.TableNameComboBox.TabIndex = 1;
            this.TableNameComboBox.SelectedIndexChanged += new System.EventHandler(this.TableNameComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "테이블명:";
            // 
            // QueryDataGridView
            // 
            this.QueryDataGridView.AllowUserToAddRows = false;
            this.QueryDataGridView.AllowUserToDeleteRows = false;
            this.QueryDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.QueryDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.QueryDataGridView.Location = new System.Drawing.Point(12, 71);
            this.QueryDataGridView.Name = "QueryDataGridView";
            this.QueryDataGridView.ReadOnly = true;
            this.QueryDataGridView.RowHeadersVisible = false;
            this.QueryDataGridView.RowTemplate.Height = 25;
            this.QueryDataGridView.Size = new System.Drawing.Size(860, 578);
            this.QueryDataGridView.TabIndex = 1;
            // 
            // DataQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 661);
            this.Controls.Add(this.QueryDataGridView);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 700);
            this.Name = "DataQueryForm";
            this.Text = "데이터 조회";
            this.Load += new System.EventHandler(this.DataQueryForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.QueryDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private RadioButton AscRadioButton;
        private RadioButton DescRadioButton;
        private Label label2;
        private ComboBox TableNameComboBox;
        private Label label1;
        private DataGridView QueryDataGridView;
    }
}