namespace LuckyLottoApp.Forms
{
    partial class DangbeonRegressionForm
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
            this.MultipleRadioButton = new System.Windows.Forms.RadioButton();
            this.SimpleRadioButton = new System.Windows.Forms.RadioButton();
            this.PredicTextBox = new System.Windows.Forms.TextBox();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.PerfectRadioButton = new System.Windows.Forms.RadioButton();
            this.BadRadioButton = new System.Windows.Forms.RadioButton();
            this.GoodRadioButton = new System.Windows.Forms.RadioButton();
            this.AllRadioButton = new System.Windows.Forms.RadioButton();
            this.NumNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.LimitNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.SectionComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LimitNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.MultipleRadioButton);
            this.panel1.Controls.Add(this.SimpleRadioButton);
            this.panel1.Controls.Add(this.PredicTextBox);
            this.panel1.Controls.Add(this.ExecuteButton);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.NumNumericUpDown);
            this.panel1.Controls.Add(this.LimitNumericUpDown);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.SectionComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1077, 66);
            this.panel1.TabIndex = 0;
            // 
            // MultipleRadioButton
            // 
            this.MultipleRadioButton.AutoSize = true;
            this.MultipleRadioButton.Location = new System.Drawing.Point(862, 36);
            this.MultipleRadioButton.Name = "MultipleRadioButton";
            this.MultipleRadioButton.Size = new System.Drawing.Size(73, 19);
            this.MultipleRadioButton.TabIndex = 15;
            this.MultipleRadioButton.Text = "다중회귀";
            this.MultipleRadioButton.UseVisualStyleBackColor = true;
            // 
            // SimpleRadioButton
            // 
            this.SimpleRadioButton.AutoSize = true;
            this.SimpleRadioButton.Checked = true;
            this.SimpleRadioButton.Location = new System.Drawing.Point(862, 11);
            this.SimpleRadioButton.Name = "SimpleRadioButton";
            this.SimpleRadioButton.Size = new System.Drawing.Size(73, 19);
            this.SimpleRadioButton.TabIndex = 14;
            this.SimpleRadioButton.TabStop = true;
            this.SimpleRadioButton.Text = "단순회귀";
            this.SimpleRadioButton.UseVisualStyleBackColor = true;
            // 
            // PredicTextBox
            // 
            this.PredicTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PredicTextBox.Location = new System.Drawing.Point(164, 32);
            this.PredicTextBox.Name = "PredicTextBox";
            this.PredicTextBox.Size = new System.Drawing.Size(201, 23);
            this.PredicTextBox.TabIndex = 13;
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Location = new System.Drawing.Point(946, 8);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(122, 48);
            this.ExecuteButton.TabIndex = 5;
            this.ExecuteButton.Text = "전체검사";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(183, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "다음회차 예상값:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(727, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "한계:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.PerfectRadioButton);
            this.panel2.Controls.Add(this.BadRadioButton);
            this.panel2.Controls.Add(this.GoodRadioButton);
            this.panel2.Controls.Add(this.AllRadioButton);
            this.panel2.Location = new System.Drawing.Point(385, 8);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(330, 48);
            this.panel2.TabIndex = 11;
            // 
            // PerfectRadioButton
            // 
            this.PerfectRadioButton.AutoSize = true;
            this.PerfectRadioButton.Location = new System.Drawing.Point(252, 14);
            this.PerfectRadioButton.Name = "PerfectRadioButton";
            this.PerfectRadioButton.Size = new System.Drawing.Size(61, 19);
            this.PerfectRadioButton.TabIndex = 11;
            this.PerfectRadioButton.Text = "최적만";
            this.PerfectRadioButton.UseVisualStyleBackColor = true;
            this.PerfectRadioButton.CheckedChanged += new System.EventHandler(this.PerfectRadioButton_CheckedChanged);
            // 
            // BadRadioButton
            // 
            this.BadRadioButton.AutoSize = true;
            this.BadRadioButton.Location = new System.Drawing.Point(167, 14);
            this.BadRadioButton.Name = "BadRadioButton";
            this.BadRadioButton.Size = new System.Drawing.Size(61, 19);
            this.BadRadioButton.TabIndex = 10;
            this.BadRadioButton.Text = "악번만";
            this.BadRadioButton.UseVisualStyleBackColor = true;
            this.BadRadioButton.CheckedChanged += new System.EventHandler(this.BadRadioButton_CheckedChanged);
            // 
            // GoodRadioButton
            // 
            this.GoodRadioButton.AutoSize = true;
            this.GoodRadioButton.Location = new System.Drawing.Point(82, 13);
            this.GoodRadioButton.Name = "GoodRadioButton";
            this.GoodRadioButton.Size = new System.Drawing.Size(61, 19);
            this.GoodRadioButton.TabIndex = 9;
            this.GoodRadioButton.Text = "호번만";
            this.GoodRadioButton.UseVisualStyleBackColor = true;
            this.GoodRadioButton.CheckedChanged += new System.EventHandler(this.GoodRadioButton_CheckedChanged);
            // 
            // AllRadioButton
            // 
            this.AllRadioButton.AutoSize = true;
            this.AllRadioButton.Checked = true;
            this.AllRadioButton.Location = new System.Drawing.Point(9, 13);
            this.AllRadioButton.Name = "AllRadioButton";
            this.AllRadioButton.Size = new System.Drawing.Size(49, 19);
            this.AllRadioButton.TabIndex = 8;
            this.AllRadioButton.TabStop = true;
            this.AllRadioButton.Text = "전체";
            this.AllRadioButton.UseVisualStyleBackColor = true;
            this.AllRadioButton.CheckedChanged += new System.EventHandler(this.AllRadioButton_CheckedChanged);
            // 
            // NumNumericUpDown
            // 
            this.NumNumericUpDown.Location = new System.Drawing.Point(94, 32);
            this.NumNumericUpDown.Maximum = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.NumNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumNumericUpDown.Name = "NumNumericUpDown";
            this.NumNumericUpDown.Size = new System.Drawing.Size(53, 23);
            this.NumNumericUpDown.TabIndex = 3;
            this.NumNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.NumNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumNumericUpDown.ValueChanged += new System.EventHandler(this.NumNumericUpDown_ValueChanged);
            // 
            // LimitNumericUpDown
            // 
            this.LimitNumericUpDown.DecimalPlaces = 2;
            this.LimitNumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.LimitNumericUpDown.Location = new System.Drawing.Point(768, 23);
            this.LimitNumericUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            65536});
            this.LimitNumericUpDown.Name = "LimitNumericUpDown";
            this.LimitNumericUpDown.Size = new System.Drawing.Size(72, 23);
            this.LimitNumericUpDown.TabIndex = 7;
            this.LimitNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.LimitNumericUpDown.Value = new decimal(new int[] {
            95,
            0,
            0,
            131072});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(89, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "검사번호:";
            // 
            // SectionComboBox
            // 
            this.SectionComboBox.FormattingEnabled = true;
            this.SectionComboBox.Location = new System.Drawing.Point(18, 32);
            this.SectionComboBox.Name = "SectionComboBox";
            this.SectionComboBox.Size = new System.Drawing.Size(53, 23);
            this.SectionComboBox.TabIndex = 1;
            this.SectionComboBox.SelectedIndexChanged += new System.EventHandler(this.SectionComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "검사구간:";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 84);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(817, 604);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(835, 84);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(254, 604);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox_SelectedIndexChanged);
            // 
            // DangbeonRegressionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 702);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "DangbeonRegressionForm";
            this.Text = "당첨회차 회귀검사";
            this.Load += new System.EventHandler(this.DangbeonRegressionForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LimitNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private NumericUpDown NumNumericUpDown;
        private Label label2;
        private ComboBox SectionComboBox;
        private Label label1;
        private PictureBox pictureBox1;
        private NumericUpDown LimitNumericUpDown;
        private Label label3;
        private Button ExecuteButton;
        private RadioButton BadRadioButton;
        private RadioButton GoodRadioButton;
        private RadioButton AllRadioButton;
        private ListBox listBox1;
        private TextBox PredicTextBox;
        private Label label4;
        private Panel panel2;
        private RadioButton PerfectRadioButton;
        private RadioButton MultipleRadioButton;
        private RadioButton SimpleRadioButton;
    }
}