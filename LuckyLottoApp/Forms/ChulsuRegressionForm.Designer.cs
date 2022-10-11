namespace LuckyLottoApp.Forms
{
    partial class ChulsuRegressionForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MultipleRadioButton = new System.Windows.Forms.RadioButton();
            this.SimpleRadioButton = new System.Windows.Forms.RadioButton();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.NumberLabel = new System.Windows.Forms.Label();
            this.LastchulLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.SectionNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ChulNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PredictTextBox = new System.Windows.Forms.TextBox();
            this.LimitNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.IndexComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ItemsComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.PerfectRadioButton = new System.Windows.Forms.RadioButton();
            this.BadRadioButton = new System.Windows.Forms.RadioButton();
            this.GoodRadioButton = new System.Windows.Forms.RadioButton();
            this.AllRadioButton = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.MultitextBox = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SectionNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChulNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LimitNumericUpDown)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.NumberLabel);
            this.panel1.Controls.Add(this.LastchulLabel);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.SectionNumericUpDown);
            this.panel1.Controls.Add(this.ChulNumericUpDown);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.PredictTextBox);
            this.panel1.Controls.Add(this.LimitNumericUpDown);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.IndexComboBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ItemsComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1067, 95);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MultipleRadioButton);
            this.groupBox1.Controls.Add(this.SimpleRadioButton);
            this.groupBox1.Controls.Add(this.ExecuteButton);
            this.groupBox1.Location = new System.Drawing.Point(874, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(188, 85);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "검사방법";
            // 
            // MultipleRadioButton
            // 
            this.MultipleRadioButton.AutoSize = true;
            this.MultipleRadioButton.Location = new System.Drawing.Point(99, 19);
            this.MultipleRadioButton.Name = "MultipleRadioButton";
            this.MultipleRadioButton.Size = new System.Drawing.Size(73, 19);
            this.MultipleRadioButton.TabIndex = 6;
            this.MultipleRadioButton.Text = "다중회귀";
            this.MultipleRadioButton.UseVisualStyleBackColor = true;
            // 
            // SimpleRadioButton
            // 
            this.SimpleRadioButton.AutoSize = true;
            this.SimpleRadioButton.Checked = true;
            this.SimpleRadioButton.Location = new System.Drawing.Point(17, 19);
            this.SimpleRadioButton.Name = "SimpleRadioButton";
            this.SimpleRadioButton.Size = new System.Drawing.Size(73, 19);
            this.SimpleRadioButton.TabIndex = 5;
            this.SimpleRadioButton.TabStop = true;
            this.SimpleRadioButton.Text = "단순회귀";
            this.SimpleRadioButton.UseVisualStyleBackColor = true;
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Location = new System.Drawing.Point(14, 45);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(162, 34);
            this.ExecuteButton.TabIndex = 4;
            this.ExecuteButton.Text = "검사하기";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // NumberLabel
            // 
            this.NumberLabel.AutoSize = true;
            this.NumberLabel.Location = new System.Drawing.Point(563, 73);
            this.NumberLabel.Name = "NumberLabel";
            this.NumberLabel.Size = new System.Drawing.Size(58, 15);
            this.NumberLabel.TabIndex = 19;
            this.NumberLabel.Text = "해당번호:";
            // 
            // LastchulLabel
            // 
            this.LastchulLabel.AutoSize = true;
            this.LastchulLabel.Location = new System.Drawing.Point(14, 73);
            this.LastchulLabel.Name = "LastchulLabel";
            this.LastchulLabel.Size = new System.Drawing.Size(34, 15);
            this.LastchulLabel.TabIndex = 18;
            this.LastchulLabel.Text = "종출:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(562, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 15);
            this.label6.TabIndex = 17;
            this.label6.Text = "예상치:";
            // 
            // SectionNumericUpDown
            // 
            this.SectionNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.SectionNumericUpDown.Location = new System.Drawing.Point(272, 13);
            this.SectionNumericUpDown.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.SectionNumericUpDown.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.SectionNumericUpDown.Name = "SectionNumericUpDown";
            this.SectionNumericUpDown.Size = new System.Drawing.Size(84, 23);
            this.SectionNumericUpDown.TabIndex = 16;
            this.SectionNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.SectionNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.SectionNumericUpDown.ValueChanged += new System.EventHandler(this.SectionNumericUpDown_ValueChanged);
            // 
            // ChulNumericUpDown
            // 
            this.ChulNumericUpDown.Location = new System.Drawing.Point(616, 13);
            this.ChulNumericUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.ChulNumericUpDown.Name = "ChulNumericUpDown";
            this.ChulNumericUpDown.Size = new System.Drawing.Size(62, 23);
            this.ChulNumericUpDown.TabIndex = 15;
            this.ChulNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ChulNumericUpDown.ValueChanged += new System.EventHandler(this.ChulNumericUpDown_ValueChanged_1);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(563, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 15);
            this.label5.TabIndex = 14;
            this.label5.Text = "출수값:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(211, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "검사구간";
            // 
            // PredictTextBox
            // 
            this.PredictTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PredictTextBox.Location = new System.Drawing.Point(616, 41);
            this.PredictTextBox.Name = "PredictTextBox";
            this.PredictTextBox.Size = new System.Drawing.Size(252, 23);
            this.PredictTextBox.TabIndex = 11;
            // 
            // LimitNumericUpDown
            // 
            this.LimitNumericUpDown.DecimalPlaces = 2;
            this.LimitNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            this.LimitNumericUpDown.Location = new System.Drawing.Point(272, 42);
            this.LimitNumericUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LimitNumericUpDown.Name = "LimitNumericUpDown";
            this.LimitNumericUpDown.Size = new System.Drawing.Size(84, 23);
            this.LimitNumericUpDown.TabIndex = 10;
            this.LimitNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.LimitNumericUpDown.Value = new decimal(new int[] {
            95,
            0,
            0,
            131072});
            this.LimitNumericUpDown.ValueChanged += new System.EventHandler(this.LimitNumericUpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(211, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 15);
            this.label3.TabIndex = 9;
            this.label3.Text = "검사한계";
            // 
            // IndexComboBox
            // 
            this.IndexComboBox.FormattingEnabled = true;
            this.IndexComboBox.Location = new System.Drawing.Point(75, 41);
            this.IndexComboBox.Name = "IndexComboBox";
            this.IndexComboBox.Size = new System.Drawing.Size(121, 23);
            this.IndexComboBox.TabIndex = 8;
            this.IndexComboBox.SelectedIndexChanged += new System.EventHandler(this.IndexComboBox_SelectedIndexChanged_1);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "인덱스";
            // 
            // ItemsComboBox
            // 
            this.ItemsComboBox.FormattingEnabled = true;
            this.ItemsComboBox.Location = new System.Drawing.Point(75, 12);
            this.ItemsComboBox.Name = "ItemsComboBox";
            this.ItemsComboBox.Size = new System.Drawing.Size(121, 23);
            this.ItemsComboBox.TabIndex = 6;
            this.ItemsComboBox.SelectedIndexChanged += new System.EventHandler(this.ItemsComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "검사항목";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.PerfectRadioButton);
            this.groupBox4.Controls.Add(this.BadRadioButton);
            this.groupBox4.Controls.Add(this.GoodRadioButton);
            this.groupBox4.Controls.Add(this.AllRadioButton);
            this.groupBox4.Location = new System.Drawing.Point(387, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(162, 75);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "결과표시 방법";
            // 
            // PerfectRadioButton
            // 
            this.PerfectRadioButton.AutoSize = true;
            this.PerfectRadioButton.Location = new System.Drawing.Point(85, 47);
            this.PerfectRadioButton.Name = "PerfectRadioButton";
            this.PerfectRadioButton.Size = new System.Drawing.Size(61, 19);
            this.PerfectRadioButton.TabIndex = 3;
            this.PerfectRadioButton.Text = "최적만";
            this.PerfectRadioButton.UseVisualStyleBackColor = true;
            this.PerfectRadioButton.CheckedChanged += new System.EventHandler(this.PerfectRadioButton_CheckedChanged);
            // 
            // BadRadioButton
            // 
            this.BadRadioButton.AutoSize = true;
            this.BadRadioButton.Location = new System.Drawing.Point(14, 47);
            this.BadRadioButton.Name = "BadRadioButton";
            this.BadRadioButton.Size = new System.Drawing.Size(61, 19);
            this.BadRadioButton.TabIndex = 2;
            this.BadRadioButton.Text = "악번만";
            this.BadRadioButton.UseVisualStyleBackColor = true;
            this.BadRadioButton.CheckedChanged += new System.EventHandler(this.BadRadioButton_CheckedChanged);
            // 
            // GoodRadioButton
            // 
            this.GoodRadioButton.AutoSize = true;
            this.GoodRadioButton.Location = new System.Drawing.Point(85, 22);
            this.GoodRadioButton.Name = "GoodRadioButton";
            this.GoodRadioButton.Size = new System.Drawing.Size(61, 19);
            this.GoodRadioButton.TabIndex = 1;
            this.GoodRadioButton.Text = "호번만";
            this.GoodRadioButton.UseVisualStyleBackColor = true;
            this.GoodRadioButton.CheckedChanged += new System.EventHandler(this.GoodRadioButton_CheckedChanged);
            // 
            // AllRadioButton
            // 
            this.AllRadioButton.AutoSize = true;
            this.AllRadioButton.Checked = true;
            this.AllRadioButton.Location = new System.Drawing.Point(14, 22);
            this.AllRadioButton.Name = "AllRadioButton";
            this.AllRadioButton.Size = new System.Drawing.Size(49, 19);
            this.AllRadioButton.TabIndex = 0;
            this.AllRadioButton.TabStop = true;
            this.AllRadioButton.Text = "전체";
            this.AllRadioButton.UseVisualStyleBackColor = true;
            this.AllRadioButton.CheckedChanged += new System.EventHandler(this.AllRadioButton_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 113);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(775, 620);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(793, 113);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(286, 424);
            this.listBox1.TabIndex = 2;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.ListBox_SelectedIndexChanged);
            // 
            // MultitextBox
            // 
            this.MultitextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MultitextBox.Location = new System.Drawing.Point(793, 543);
            this.MultitextBox.Multiline = true;
            this.MultitextBox.Name = "MultitextBox";
            this.MultitextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MultitextBox.Size = new System.Drawing.Size(286, 190);
            this.MultitextBox.TabIndex = 3;
            // 
            // ChulsuRegressionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 745);
            this.Controls.Add(this.MultitextBox);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Name = "ChulsuRegressionForm";
            this.Text = "여러 출수의 회귀 검사";
            this.Load += new System.EventHandler(this.ChulsuRegressionForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SectionNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChulNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LimitNumericUpDown)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private GroupBox groupBox4;
        private RadioButton PerfectRadioButton;
        private RadioButton BadRadioButton;
        private RadioButton GoodRadioButton;
        private RadioButton AllRadioButton;
        private Label label4;
        private TextBox PredictTextBox;
        private NumericUpDown LimitNumericUpDown;
        private Label label3;
        private ComboBox IndexComboBox;
        private Label label2;
        private ComboBox ItemsComboBox;
        private Label label1;
        private Button ExecuteButton;
        private PictureBox pictureBox1;
        private ListBox listBox1;
        private Label label5;
        private NumericUpDown ChulNumericUpDown;
        private NumericUpDown SectionNumericUpDown;
        private Label LastchulLabel;
        private Label label6;
        private Label NumberLabel;
        private GroupBox groupBox1;
        private RadioButton MultipleRadioButton;
        private RadioButton SimpleRadioButton;
        private TextBox MultitextBox;
    }
}