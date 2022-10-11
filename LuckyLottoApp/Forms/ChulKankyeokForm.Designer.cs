namespace LuckyLottoApp.Forms
{
    partial class ChulKankyeokForm
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
            this.listView1 = new System.Windows.Forms.ListView();
            this.SumaryTextBox = new System.Windows.Forms.TextBox();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.OrderNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.SectionComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OrderNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 174);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(960, 575);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // SumaryTextBox
            // 
            this.SumaryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SumaryTextBox.Location = new System.Drawing.Point(12, 12);
            this.SumaryTextBox.Multiline = true;
            this.SumaryTextBox.Name = "SumaryTextBox";
            this.SumaryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SumaryTextBox.Size = new System.Drawing.Size(422, 156);
            this.SumaryTextBox.TabIndex = 1;
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResultTextBox.Location = new System.Drawing.Point(637, 12);
            this.ResultTextBox.Multiline = true;
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.Size = new System.Drawing.Size(335, 156);
            this.ResultTextBox.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ExecuteButton);
            this.panel1.Controls.Add(this.OrderNumericUpDown);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.SectionComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(440, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(191, 156);
            this.panel1.TabIndex = 3;
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Location = new System.Drawing.Point(19, 91);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(154, 45);
            this.ExecuteButton.TabIndex = 4;
            this.ExecuteButton.Text = "검사하기";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // OrderNumericUpDown
            // 
            this.OrderNumericUpDown.Location = new System.Drawing.Point(83, 56);
            this.OrderNumericUpDown.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.OrderNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.OrderNumericUpDown.Name = "OrderNumericUpDown";
            this.OrderNumericUpDown.Size = new System.Drawing.Size(90, 23);
            this.OrderNumericUpDown.TabIndex = 3;
            this.OrderNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.OrderNumericUpDown.ValueChanged += new System.EventHandler(this.OrderNumericUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "시작회차:";
            // 
            // SectionComboBox
            // 
            this.SectionComboBox.FormattingEnabled = true;
            this.SectionComboBox.Location = new System.Drawing.Point(83, 19);
            this.SectionComboBox.Name = "SectionComboBox";
            this.SectionComboBox.Size = new System.Drawing.Size(90, 23);
            this.SectionComboBox.TabIndex = 1;
            this.SectionComboBox.SelectionChangeCommitted += new System.EventHandler(this.SectionComboBox_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "검사구간:";
            // 
            // ChulKankyeokForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.SumaryTextBox);
            this.Controls.Add(this.listView1);
            this.MaximizeBox = false;
            this.Name = "ChulKankyeokForm";
            this.Text = "출현간격 검사";
            this.Load += new System.EventHandler(this.ChulKankyeokForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OrderNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView listView1;
        private TextBox SumaryTextBox;
        private TextBox ResultTextBox;
        private Panel panel1;
        private Button ExecuteButton;
        private NumericUpDown OrderNumericUpDown;
        private Label label2;
        private ComboBox SectionComboBox;
        private Label label1;
    }
}