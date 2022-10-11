namespace LuckyLottoApp.Forms
{
    partial class LinearForm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.AllCheckButton = new System.Windows.Forms.Button();
            this.SectionNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.ItemComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ColumnComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.EachColumnButton = new System.Windows.Forms.Button();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.SelectListView = new System.Windows.Forms.ListView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.NextTextBox = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.MaxTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.RealTextBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ResultListBox = new System.Windows.Forms.ListBox();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.LastTextBox = new System.Windows.Forms.TextBox();
            this.PredictTextBox = new System.Windows.Forms.TextBox();
            this.GapTextBox = new System.Windows.Forms.TextBox();
            this.NumberTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SectionNumericUpDown)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.AllCheckButton);
            this.panel1.Controls.Add(this.SectionNumericUpDown);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.ItemComboBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ColumnComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.EachColumnButton);
            this.panel1.Controls.Add(this.ExecuteButton);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 61);
            this.panel1.TabIndex = 0;
            // 
            // AllCheckButton
            // 
            this.AllCheckButton.Location = new System.Drawing.Point(553, 11);
            this.AllCheckButton.Name = "AllCheckButton";
            this.AllCheckButton.Size = new System.Drawing.Size(98, 38);
            this.AllCheckButton.TabIndex = 8;
            this.AllCheckButton.Text = "구간전체 검사";
            this.AllCheckButton.UseVisualStyleBackColor = true;
            this.AllCheckButton.Click += new System.EventHandler(this.AllCheckButton_Click);
            // 
            // SectionNumericUpDown
            // 
            this.SectionNumericUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.SectionNumericUpDown.Location = new System.Drawing.Point(460, 18);
            this.SectionNumericUpDown.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.SectionNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SectionNumericUpDown.Name = "SectionNumericUpDown";
            this.SectionNumericUpDown.Size = new System.Drawing.Size(55, 23);
            this.SectionNumericUpDown.TabIndex = 7;
            this.SectionNumericUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.SectionNumericUpDown.ValueChanged += new System.EventHandler(this.SectionNumericUpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(396, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "이동구간:";
            // 
            // ItemComboBox
            // 
            this.ItemComboBox.FormattingEnabled = true;
            this.ItemComboBox.Location = new System.Drawing.Point(296, 18);
            this.ItemComboBox.Name = "ItemComboBox";
            this.ItemComboBox.Size = new System.Drawing.Size(79, 23);
            this.ItemComboBox.TabIndex = 5;
            this.ItemComboBox.SelectionChangeCommitted += new System.EventHandler(this.ItemComboBox_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(252, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "항 목:";
            // 
            // ColumnComboBox
            // 
            this.ColumnComboBox.FormattingEnabled = true;
            this.ColumnComboBox.Location = new System.Drawing.Point(69, 18);
            this.ColumnComboBox.Name = "ColumnComboBox";
            this.ColumnComboBox.Size = new System.Drawing.Size(162, 23);
            this.ColumnComboBox.TabIndex = 3;
            this.ColumnComboBox.SelectedIndexChanged += new System.EventHandler(this.ColumnComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "열 이름:";
            // 
            // EachColumnButton
            // 
            this.EachColumnButton.Location = new System.Drawing.Point(761, 11);
            this.EachColumnButton.Name = "EachColumnButton";
            this.EachColumnButton.Size = new System.Drawing.Size(84, 38);
            this.EachColumnButton.TabIndex = 1;
            this.EachColumnButton.Text = "열별검사";
            this.EachColumnButton.UseVisualStyleBackColor = true;
            this.EachColumnButton.Click += new System.EventHandler(this.EachColumnButton_Click);
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Enabled = false;
            this.ExecuteButton.Location = new System.Drawing.Point(657, 11);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(98, 38);
            this.ExecuteButton.TabIndex = 0;
            this.ExecuteButton.Text = "구간사전 검사";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // SelectListView
            // 
            this.SelectListView.FullRowSelect = true;
            this.SelectListView.GridLines = true;
            this.SelectListView.Location = new System.Drawing.Point(12, 79);
            this.SelectListView.MultiSelect = false;
            this.SelectListView.Name = "SelectListView";
            this.SelectListView.Size = new System.Drawing.Size(155, 670);
            this.SelectListView.TabIndex = 1;
            this.SelectListView.UseCompatibleStateImageBehavior = false;
            this.SelectListView.View = System.Windows.Forms.View.Details;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.NextTextBox);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.MaxTextBox);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.RealTextBox);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Location = new System.Drawing.Point(173, 79);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(369, 250);
            this.panel2.TabIndex = 2;
            // 
            // NextTextBox
            // 
            this.NextTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NextTextBox.Location = new System.Drawing.Point(79, 108);
            this.NextTextBox.Multiline = true;
            this.NextTextBox.Name = "NextTextBox";
            this.NextTextBox.Size = new System.Drawing.Size(275, 126);
            this.NextTextBox.TabIndex = 13;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 111);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(58, 15);
            this.label10.TabIndex = 12;
            this.label10.Text = "최종다음:";
            // 
            // MaxTextBox
            // 
            this.MaxTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MaxTextBox.Location = new System.Drawing.Point(79, 67);
            this.MaxTextBox.Name = "MaxTextBox";
            this.MaxTextBox.Size = new System.Drawing.Size(275, 23);
            this.MaxTextBox.TabIndex = 11;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 15);
            this.label9.TabIndex = 10;
            this.label9.Text = "연속최대:";
            // 
            // RealTextBox
            // 
            this.RealTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RealTextBox.Location = new System.Drawing.Point(79, 30);
            this.RealTextBox.Name = "RealTextBox";
            this.RealTextBox.Size = new System.Drawing.Size(275, 23);
            this.RealTextBox.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 33);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(58, 15);
            this.label8.TabIndex = 8;
            this.label8.Text = "후방연속:";
            // 
            // ResultListBox
            // 
            this.ResultListBox.FormattingEnabled = true;
            this.ResultListBox.ItemHeight = 15;
            this.ResultListBox.Location = new System.Drawing.Point(173, 340);
            this.ResultListBox.Name = "ResultListBox";
            this.ResultListBox.Size = new System.Drawing.Size(369, 409);
            this.ResultListBox.TabIndex = 3;
            this.toolTip1.SetToolTip(this.ResultListBox, "음수면 악번, 양수면 호번");
            this.ResultListBox.SelectedIndexChanged += new System.EventHandler(this.ResultListBox_SelectedIndexChanged);
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResultTextBox.Location = new System.Drawing.Point(548, 340);
            this.ResultTextBox.Multiline = true;
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultTextBox.Size = new System.Drawing.Size(324, 409);
            this.ResultTextBox.TabIndex = 4;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.LastTextBox);
            this.panel3.Controls.Add(this.PredictTextBox);
            this.panel3.Controls.Add(this.GapTextBox);
            this.panel3.Controls.Add(this.NumberTextBox);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(548, 79);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(324, 250);
            this.panel3.TabIndex = 5;
            // 
            // LastTextBox
            // 
            this.LastTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LastTextBox.Location = new System.Drawing.Point(81, 20);
            this.LastTextBox.Multiline = true;
            this.LastTextBox.Name = "LastTextBox";
            this.LastTextBox.Size = new System.Drawing.Size(228, 65);
            this.LastTextBox.TabIndex = 7;
            // 
            // PredictTextBox
            // 
            this.PredictTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PredictTextBox.Location = new System.Drawing.Point(81, 106);
            this.PredictTextBox.Name = "PredictTextBox";
            this.PredictTextBox.Size = new System.Drawing.Size(228, 23);
            this.PredictTextBox.TabIndex = 6;
            // 
            // GapTextBox
            // 
            this.GapTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GapTextBox.Location = new System.Drawing.Point(81, 151);
            this.GapTextBox.Name = "GapTextBox";
            this.GapTextBox.Size = new System.Drawing.Size(228, 23);
            this.GapTextBox.TabIndex = 5;
            // 
            // NumberTextBox
            // 
            this.NumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumberTextBox.Location = new System.Drawing.Point(81, 196);
            this.NumberTextBox.Multiline = true;
            this.NumberTextBox.Name = "NumberTextBox";
            this.NumberTextBox.Size = new System.Drawing.Size(228, 38);
            this.NumberTextBox.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(46, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "최종출:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 109);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "예상치:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 154);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "차이값:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 209);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "해당번호:";
            // 
            // LinearForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 761);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.ResultListBox);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.SelectListView);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "LinearForm";
            this.Text = "단순 회귀검사";
            this.Load += new System.EventHandler(this.LinearForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SectionNumericUpDown)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private NumericUpDown SectionNumericUpDown;
        private Label label3;
        private ComboBox ItemComboBox;
        private Label label2;
        private ComboBox ColumnComboBox;
        private Label label1;
        private Button EachColumnButton;
        private Button ExecuteButton;
        private ListView SelectListView;
        private Panel panel2;
        private ListBox ResultListBox;
        private TextBox ResultTextBox;
        private Panel panel3;
        private TextBox NextTextBox;
        private Label label10;
        private TextBox MaxTextBox;
        private Label label9;
        private TextBox RealTextBox;
        private Label label8;
        private TextBox LastTextBox;
        private TextBox PredictTextBox;
        private TextBox GapTextBox;
        private TextBox NumberTextBox;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Button AllCheckButton;
        private ToolTip toolTip1;
    }
}