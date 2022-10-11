namespace LuckyLottoApp.Forms
{
    partial class MultyCheckForm
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
            this.LimitNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.BeforeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.SectionNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.ColumnComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.UpperTextBox = new System.Windows.Forms.TextBox();
            this.DownTextBox = new System.Windows.Forms.TextBox();
            this.WriteButton = new System.Windows.Forms.Button();
            this.ViewListView = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LimitNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeforeNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SectionNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.LimitNumericUpDown);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.BeforeNumericUpDown);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.SectionNumericUpDown);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ColumnComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 51);
            this.panel1.TabIndex = 0;
            // 
            // LimitNumericUpDown
            // 
            this.LimitNumericUpDown.DecimalPlaces = 3;
            this.LimitNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.LimitNumericUpDown.Location = new System.Drawing.Point(818, 14);
            this.LimitNumericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LimitNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.LimitNumericUpDown.Name = "LimitNumericUpDown";
            this.LimitNumericUpDown.Size = new System.Drawing.Size(87, 23);
            this.LimitNumericUpDown.TabIndex = 7;
            this.LimitNumericUpDown.Value = new decimal(new int[] {
            11,
            0,
            0,
            196608});
            this.LimitNumericUpDown.ValueChanged += new System.EventHandler(this.LimitNumericUpDown_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(754, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "상극한계:";
            // 
            // BeforeNumericUpDown
            // 
            this.BeforeNumericUpDown.Location = new System.Drawing.Point(626, 13);
            this.BeforeNumericUpDown.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.BeforeNumericUpDown.Name = "BeforeNumericUpDown";
            this.BeforeNumericUpDown.Size = new System.Drawing.Size(61, 23);
            this.BeforeNumericUpDown.TabIndex = 5;
            this.BeforeNumericUpDown.ValueChanged += new System.EventHandler(this.BeforeNumericUpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(562, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "이전결과:";
            // 
            // SectionNumericUpDown
            // 
            this.SectionNumericUpDown.Location = new System.Drawing.Point(407, 13);
            this.SectionNumericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.SectionNumericUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.SectionNumericUpDown.Name = "SectionNumericUpDown";
            this.SectionNumericUpDown.Size = new System.Drawing.Size(82, 23);
            this.SectionNumericUpDown.TabIndex = 3;
            this.SectionNumericUpDown.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(343, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "검사구간:";
            // 
            // ColumnComboBox
            // 
            this.ColumnComboBox.FormattingEnabled = true;
            this.ColumnComboBox.Location = new System.Drawing.Point(84, 13);
            this.ColumnComboBox.Name = "ColumnComboBox";
            this.ColumnComboBox.Size = new System.Drawing.Size(191, 23);
            this.ColumnComboBox.TabIndex = 1;
            this.ColumnComboBox.SelectedIndexChanged += new System.EventHandler(this.ColumnComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "검사항목: ";
            // 
            // UpperTextBox
            // 
            this.UpperTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UpperTextBox.Location = new System.Drawing.Point(685, 69);
            this.UpperTextBox.Multiline = true;
            this.UpperTextBox.Name = "UpperTextBox";
            this.UpperTextBox.Size = new System.Drawing.Size(287, 300);
            this.UpperTextBox.TabIndex = 1;
            // 
            // DownTextBox
            // 
            this.DownTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DownTextBox.Location = new System.Drawing.Point(685, 449);
            this.DownTextBox.Multiline = true;
            this.DownTextBox.Name = "DownTextBox";
            this.DownTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DownTextBox.Size = new System.Drawing.Size(287, 300);
            this.DownTextBox.TabIndex = 2;
            // 
            // WriteButton
            // 
            this.WriteButton.Location = new System.Drawing.Point(685, 380);
            this.WriteButton.Name = "WriteButton";
            this.WriteButton.Size = new System.Drawing.Size(287, 56);
            this.WriteButton.TabIndex = 3;
            this.WriteButton.Text = "파일 작성";
            this.WriteButton.UseVisualStyleBackColor = true;
            this.WriteButton.Click += new System.EventHandler(this.WriteButton_Click);
            // 
            // ViewListView
            // 
            this.ViewListView.GridLines = true;
            this.ViewListView.Location = new System.Drawing.Point(12, 69);
            this.ViewListView.MultiSelect = false;
            this.ViewListView.Name = "ViewListView";
            this.ViewListView.Size = new System.Drawing.Size(667, 680);
            this.ViewListView.TabIndex = 4;
            this.ViewListView.UseCompatibleStateImageBehavior = false;
            this.ViewListView.View = System.Windows.Forms.View.Details;
            // 
            // MultyCheckForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.ViewListView);
            this.Controls.Add(this.WriteButton);
            this.Controls.Add(this.DownTextBox);
            this.Controls.Add(this.UpperTextBox);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "MultyCheckForm";
            this.Text = "데이터 복합검사";
            this.Load += new System.EventHandler(this.MultyCheckForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LimitNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BeforeNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SectionNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private NumericUpDown LimitNumericUpDown;
        private Label label4;
        private NumericUpDown BeforeNumericUpDown;
        private Label label3;
        private NumericUpDown SectionNumericUpDown;
        private Label label2;
        private ComboBox ColumnComboBox;
        private Label label1;
        private TextBox UpperTextBox;
        private TextBox DownTextBox;
        private Button WriteButton;
        private ListView ViewListView;
    }
}