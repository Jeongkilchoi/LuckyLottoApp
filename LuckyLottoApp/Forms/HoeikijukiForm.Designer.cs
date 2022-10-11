namespace LuckyLottoApp.Forms
{
    partial class HoeikijukiForm
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
            this.JukiRadioButton = new System.Windows.Forms.RadioButton();
            this.HokiRadioButton = new System.Windows.Forms.RadioButton();
            this.CountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.LimitNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.IntervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.OrderNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.MaxLabel = new System.Windows.Forms.Label();
            this.RealLabel = new System.Windows.Forms.Label();
            this.WinningTextBox = new System.Windows.Forms.TextBox();
            this.GoodButton = new System.Windows.Forms.Button();
            this.PercentTextBox = new System.Windows.Forms.TextBox();
            this.ResultListBox = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DiagonalButton = new System.Windows.Forms.Button();
            this.DirectButton = new System.Windows.Forms.Button();
            this.WayComboBox = new System.Windows.Forms.ComboBox();
            this.ContinueNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.HokiTextBox = new System.Windows.Forms.TextBox();
            this.ResultListView = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.CountNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LimitNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderNumericUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContinueNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // JukiRadioButton
            // 
            this.JukiRadioButton.AutoSize = true;
            this.JukiRadioButton.Location = new System.Drawing.Point(158, 673);
            this.JukiRadioButton.Name = "JukiRadioButton";
            this.JukiRadioButton.Size = new System.Drawing.Size(89, 19);
            this.JukiRadioButton.TabIndex = 34;
            this.JukiRadioButton.Text = "주기로 검사";
            this.JukiRadioButton.UseVisualStyleBackColor = true;
            // 
            // HokiRadioButton
            // 
            this.HokiRadioButton.AutoSize = true;
            this.HokiRadioButton.Checked = true;
            this.HokiRadioButton.Location = new System.Drawing.Point(24, 673);
            this.HokiRadioButton.Name = "HokiRadioButton";
            this.HokiRadioButton.Size = new System.Drawing.Size(89, 19);
            this.HokiRadioButton.TabIndex = 33;
            this.HokiRadioButton.TabStop = true;
            this.HokiRadioButton.Text = "회기로 검사";
            this.HokiRadioButton.UseVisualStyleBackColor = true;
            this.HokiRadioButton.CheckedChanged += new System.EventHandler(this.HokiRadioButton_CheckedChanged);
            // 
            // CountNumericUpDown
            // 
            this.CountNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CountNumericUpDown.Location = new System.Drawing.Point(227, 638);
            this.CountNumericUpDown.Maximum = new decimal(new int[] {
            9900,
            0,
            0,
            0});
            this.CountNumericUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CountNumericUpDown.Name = "CountNumericUpDown";
            this.CountNumericUpDown.Size = new System.Drawing.Size(54, 23);
            this.CountNumericUpDown.TabIndex = 32;
            this.CountNumericUpDown.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.CountNumericUpDown.ValueChanged += new System.EventHandler(this.CountNumericUpDown_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(166, 640);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 15);
            this.label6.TabIndex = 31;
            this.label6.Text = "검사갯수";
            // 
            // LimitNumericUpDown
            // 
            this.LimitNumericUpDown.Location = new System.Drawing.Point(85, 638);
            this.LimitNumericUpDown.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.LimitNumericUpDown.Name = "LimitNumericUpDown";
            this.LimitNumericUpDown.Size = new System.Drawing.Size(54, 23);
            this.LimitNumericUpDown.TabIndex = 30;
            this.LimitNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 640);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 15);
            this.label7.TabIndex = 29;
            this.label7.Text = "한계값";
            // 
            // IntervalNumericUpDown
            // 
            this.IntervalNumericUpDown.Location = new System.Drawing.Point(227, 607);
            this.IntervalNumericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.IntervalNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.IntervalNumericUpDown.Name = "IntervalNumericUpDown";
            this.IntervalNumericUpDown.Size = new System.Drawing.Size(54, 23);
            this.IntervalNumericUpDown.TabIndex = 28;
            this.IntervalNumericUpDown.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.IntervalNumericUpDown.ValueChanged += new System.EventHandler(this.IntervalNumericUpDown_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(166, 609);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 15);
            this.label5.TabIndex = 27;
            this.label5.Text = "검사회기";
            // 
            // OrderNumericUpDown
            // 
            this.OrderNumericUpDown.Location = new System.Drawing.Point(85, 607);
            this.OrderNumericUpDown.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.OrderNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.OrderNumericUpDown.Name = "OrderNumericUpDown";
            this.OrderNumericUpDown.Size = new System.Drawing.Size(54, 23);
            this.OrderNumericUpDown.TabIndex = 26;
            this.OrderNumericUpDown.Value = new decimal(new int[] {
            950,
            0,
            0,
            0});
            this.OrderNumericUpDown.ValueChanged += new System.EventHandler(this.OrderNumericUpDown_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 609);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 15);
            this.label4.TabIndex = 25;
            this.label4.Text = "검사회차";
            // 
            // MaxLabel
            // 
            this.MaxLabel.AutoSize = true;
            this.MaxLabel.Location = new System.Drawing.Point(154, 704);
            this.MaxLabel.Name = "MaxLabel";
            this.MaxLabel.Size = new System.Drawing.Size(58, 15);
            this.MaxLabel.TabIndex = 24;
            this.MaxLabel.Text = "연속최대:";
            // 
            // RealLabel
            // 
            this.RealLabel.AutoSize = true;
            this.RealLabel.Location = new System.Drawing.Point(24, 704);
            this.RealLabel.Name = "RealLabel";
            this.RealLabel.Size = new System.Drawing.Size(58, 15);
            this.RealLabel.TabIndex = 23;
            this.RealLabel.Text = "후방연속:";
            // 
            // WinningTextBox
            // 
            this.WinningTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WinningTextBox.Location = new System.Drawing.Point(23, 726);
            this.WinningTextBox.Name = "WinningTextBox";
            this.WinningTextBox.Size = new System.Drawing.Size(189, 23);
            this.WinningTextBox.TabIndex = 22;
            // 
            // GoodButton
            // 
            this.GoodButton.Location = new System.Drawing.Point(287, 595);
            this.GoodButton.Name = "GoodButton";
            this.GoodButton.Size = new System.Drawing.Size(72, 154);
            this.GoodButton.TabIndex = 21;
            this.GoodButton.Text = "회기및 주기호번   자동검사";
            this.GoodButton.UseVisualStyleBackColor = true;
            this.GoodButton.Click += new System.EventHandler(this.GoodButton_Click);
            // 
            // PercentTextBox
            // 
            this.PercentTextBox.Location = new System.Drawing.Point(23, 565);
            this.PercentTextBox.Name = "PercentTextBox";
            this.PercentTextBox.ReadOnly = true;
            this.PercentTextBox.Size = new System.Drawing.Size(336, 23);
            this.PercentTextBox.TabIndex = 20;
            // 
            // ResultListBox
            // 
            this.ResultListBox.Enabled = false;
            this.ResultListBox.FormattingEnabled = true;
            this.ResultListBox.ItemHeight = 15;
            this.ResultListBox.Location = new System.Drawing.Point(367, 565);
            this.ResultListBox.Name = "ResultListBox";
            this.ResultListBox.Size = new System.Drawing.Size(65, 184);
            this.ResultListBox.TabIndex = 19;
            this.ResultListBox.SelectedIndexChanged += new System.EventHandler(this.ResultListBox_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.DiagonalButton);
            this.panel1.Controls.Add(this.DirectButton);
            this.panel1.Controls.Add(this.WayComboBox);
            this.panel1.Controls.Add(this.ContinueNumericUpDown);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.HokiTextBox);
            this.panel1.Location = new System.Drawing.Point(439, 565);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(442, 184);
            this.panel1.TabIndex = 35;
            // 
            // DiagonalButton
            // 
            this.DiagonalButton.Location = new System.Drawing.Point(277, 135);
            this.DiagonalButton.Name = "DiagonalButton";
            this.DiagonalButton.Size = new System.Drawing.Size(150, 37);
            this.DiagonalButton.TabIndex = 5;
            this.DiagonalButton.Text = "회기 사선악번";
            this.DiagonalButton.UseVisualStyleBackColor = true;
            this.DiagonalButton.Click += new System.EventHandler(this.DiagonalButton_Click);
            // 
            // DirectButton
            // 
            this.DirectButton.Location = new System.Drawing.Point(277, 82);
            this.DirectButton.Name = "DirectButton";
            this.DirectButton.Size = new System.Drawing.Size(150, 37);
            this.DirectButton.TabIndex = 4;
            this.DirectButton.Text = "회기 직선악번";
            this.DirectButton.UseVisualStyleBackColor = true;
            this.DirectButton.Click += new System.EventHandler(this.DirectButton_Click);
            // 
            // WayComboBox
            // 
            this.WayComboBox.FormattingEnabled = true;
            this.WayComboBox.Items.AddRange(new object[] {
            "같음",
            "이상",
            "이하"});
            this.WayComboBox.Location = new System.Drawing.Point(373, 43);
            this.WayComboBox.Name = "WayComboBox";
            this.WayComboBox.Size = new System.Drawing.Size(54, 23);
            this.WayComboBox.TabIndex = 3;
            // 
            // ContinueNumericUpDown
            // 
            this.ContinueNumericUpDown.Location = new System.Drawing.Point(331, 44);
            this.ContinueNumericUpDown.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.ContinueNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ContinueNumericUpDown.Name = "ContinueNumericUpDown";
            this.ContinueNumericUpDown.Size = new System.Drawing.Size(35, 23);
            this.ContinueNumericUpDown.TabIndex = 2;
            this.ContinueNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(267, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "검사조건:";
            // 
            // HokiTextBox
            // 
            this.HokiTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HokiTextBox.Enabled = false;
            this.HokiTextBox.Location = new System.Drawing.Point(9, 9);
            this.HokiTextBox.Multiline = true;
            this.HokiTextBox.Name = "HokiTextBox";
            this.HokiTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HokiTextBox.Size = new System.Drawing.Size(252, 163);
            this.HokiTextBox.TabIndex = 0;
            // 
            // ResultListView
            // 
            this.ResultListView.GridLines = true;
            this.ResultListView.Location = new System.Drawing.Point(23, 12);
            this.ResultListView.MultiSelect = false;
            this.ResultListView.Name = "ResultListView";
            this.ResultListView.Size = new System.Drawing.Size(858, 547);
            this.ResultListView.TabIndex = 36;
            this.ResultListView.UseCompatibleStateImageBehavior = false;
            this.ResultListView.View = System.Windows.Forms.View.Details;
            // 
            // HoeikijukiForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 761);
            this.Controls.Add(this.ResultListView);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.JukiRadioButton);
            this.Controls.Add(this.HokiRadioButton);
            this.Controls.Add(this.CountNumericUpDown);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.LimitNumericUpDown);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.IntervalNumericUpDown);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.OrderNumericUpDown);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.MaxLabel);
            this.Controls.Add(this.RealLabel);
            this.Controls.Add(this.WinningTextBox);
            this.Controls.Add(this.GoodButton);
            this.Controls.Add(this.PercentTextBox);
            this.Controls.Add(this.ResultListBox);
            this.MaximizeBox = false;
            this.Name = "HoeikijukiForm";
            this.Text = "회기및 주기검사";
            this.Load += new System.EventHandler(this.HoeikijukiForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CountNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LimitNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderNumericUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ContinueNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private RadioButton JukiRadioButton;
        private RadioButton HokiRadioButton;
        private NumericUpDown CountNumericUpDown;
        private Label label6;
        private NumericUpDown LimitNumericUpDown;
        private Label label7;
        private NumericUpDown IntervalNumericUpDown;
        private Label label5;
        private NumericUpDown OrderNumericUpDown;
        private Label label4;
        private Label MaxLabel;
        private Label RealLabel;
        private TextBox WinningTextBox;
        private Button GoodButton;
        private TextBox PercentTextBox;
        private ListBox ResultListBox;
        private Panel panel1;
        private Button DiagonalButton;
        private Button DirectButton;
        private ComboBox WayComboBox;
        private NumericUpDown ContinueNumericUpDown;
        private Label label1;
        private TextBox HokiTextBox;
        private ListView ResultListView;
    }
}