namespace LuckyLottoApp.Forms
{
    partial class ChulsuFilterForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.listView1 = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.PercentLabel = new System.Windows.Forms.Label();
            this.ShownCountLabel = new System.Windows.Forms.Label();
            this.NonCountLabel = new System.Windows.Forms.Label();
            this.SameCountLabel = new System.Windows.Forms.Label();
            this.NowMaxLabel = new System.Windows.Forms.Label();
            this.NowRealLabel = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.PassLabel = new System.Windows.Forms.Label();
            this.NumberTextBox = new System.Windows.Forms.TextBox();
            this.SectionNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.DataCountLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BadListBox = new System.Windows.Forms.CheckedListBox();
            this.GoodListBox = new System.Windows.Forms.ListBox();
            this.AddFilterButton = new System.Windows.Forms.Button();
            this.UseGridButton = new System.Windows.Forms.Button();
            this.GoodBadButton = new System.Windows.Forms.Button();
            this.LimitWriteButton = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.MinMaxRadioButton = new System.Windows.Forms.RadioButton();
            this.PerfectRadioButton = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Next5TextBox = new System.Windows.Forms.TextBox();
            this.Max5Label = new System.Windows.Forms.Label();
            this.Real5Label = new System.Windows.Forms.Label();
            this.Next4TextBox = new System.Windows.Forms.TextBox();
            this.Max4Label = new System.Windows.Forms.Label();
            this.Real4Label = new System.Windows.Forms.Label();
            this.Next3TextBox = new System.Windows.Forms.TextBox();
            this.Max3Label = new System.Windows.Forms.Label();
            this.Real3Label = new System.Windows.Forms.Label();
            this.Next2TextBox = new System.Windows.Forms.TextBox();
            this.Max2Label = new System.Windows.Forms.Label();
            this.Real2Label = new System.Windows.Forms.Label();
            this.Next1TextBox = new System.Windows.Forms.TextBox();
            this.Max1Label = new System.Windows.Forms.Label();
            this.Real1Label = new System.Windows.Forms.Label();
            this.NextChulsuTextBox = new System.Windows.Forms.TextBox();
            this.NextMaxLabel = new System.Windows.Forms.Label();
            this.NextRealLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SectionNumericUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(480, 837);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridView1_DataError);
            // 
            // listView1
            // 
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(503, 12);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(130, 837);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RemoveToolStripMenuItem,
            this.toolStripSeparator1,
            this.CancelToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(127, 54);
            // 
            // RemoveToolStripMenuItem
            // 
            this.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem";
            this.RemoveToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.RemoveToolStripMenuItem.Text = "체크 삭제";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(123, 6);
            // 
            // CancelToolStripMenuItem
            // 
            this.CancelToolStripMenuItem.Name = "CancelToolStripMenuItem";
            this.CancelToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.CancelToolStripMenuItem.Text = "작업 취소";
            // 
            // PercentLabel
            // 
            this.PercentLabel.AutoSize = true;
            this.PercentLabel.Location = new System.Drawing.Point(336, 100);
            this.PercentLabel.Name = "PercentLabel";
            this.PercentLabel.Size = new System.Drawing.Size(50, 15);
            this.PercentLabel.TabIndex = 9;
            this.PercentLabel.Text = "출수률: ";
            // 
            // ShownCountLabel
            // 
            this.ShownCountLabel.AutoSize = true;
            this.ShownCountLabel.Location = new System.Drawing.Point(228, 100);
            this.ShownCountLabel.Name = "ShownCountLabel";
            this.ShownCountLabel.Size = new System.Drawing.Size(50, 15);
            this.ShownCountLabel.TabIndex = 8;
            this.ShownCountLabel.Text = "유출수: ";
            // 
            // NonCountLabel
            // 
            this.NonCountLabel.AutoSize = true;
            this.NonCountLabel.Location = new System.Drawing.Point(120, 100);
            this.NonCountLabel.Name = "NonCountLabel";
            this.NonCountLabel.Size = new System.Drawing.Size(50, 15);
            this.NonCountLabel.TabIndex = 7;
            this.NonCountLabel.Text = "무출수: ";
            // 
            // SameCountLabel
            // 
            this.SameCountLabel.AutoSize = true;
            this.SameCountLabel.Location = new System.Drawing.Point(12, 100);
            this.SameCountLabel.Name = "SameCountLabel";
            this.SameCountLabel.Size = new System.Drawing.Size(50, 15);
            this.SameCountLabel.TabIndex = 6;
            this.SameCountLabel.Text = "동수출: ";
            // 
            // NowMaxLabel
            // 
            this.NowMaxLabel.AutoSize = true;
            this.NowMaxLabel.Location = new System.Drawing.Point(120, 72);
            this.NowMaxLabel.Name = "NowMaxLabel";
            this.NowMaxLabel.Size = new System.Drawing.Size(62, 15);
            this.NowMaxLabel.TabIndex = 5;
            this.NowMaxLabel.Text = "연속최대: ";
            // 
            // NowRealLabel
            // 
            this.NowRealLabel.AutoSize = true;
            this.NowRealLabel.Location = new System.Drawing.Point(12, 72);
            this.NowRealLabel.Name = "NowRealLabel";
            this.NowRealLabel.Size = new System.Drawing.Size(62, 15);
            this.NowRealLabel.TabIndex = 4;
            this.NowRealLabel.Text = "후방연속: ";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.PassLabel);
            this.panel4.Controls.Add(this.NumberTextBox);
            this.panel4.Controls.Add(this.SectionNumericUpDown);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.DataCountLabel);
            this.panel4.Controls.Add(this.NowRealLabel);
            this.panel4.Controls.Add(this.NowMaxLabel);
            this.panel4.Controls.Add(this.SameCountLabel);
            this.panel4.Controls.Add(this.NonCountLabel);
            this.panel4.Controls.Add(this.ShownCountLabel);
            this.panel4.Controls.Add(this.PercentLabel);
            this.panel4.Location = new System.Drawing.Point(644, 12);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(451, 126);
            this.panel4.TabIndex = 36;
            // 
            // PassLabel
            // 
            this.PassLabel.AutoSize = true;
            this.PassLabel.Location = new System.Drawing.Point(228, 72);
            this.PassLabel.Name = "PassLabel";
            this.PassLabel.Size = new System.Drawing.Size(62, 15);
            this.PassLabel.TabIndex = 14;
            this.PassLabel.Text = "검사결과: ";
            // 
            // NumberTextBox
            // 
            this.NumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumberTextBox.Location = new System.Drawing.Point(12, 40);
            this.NumberTextBox.Name = "NumberTextBox";
            this.NumberTextBox.Size = new System.Drawing.Size(425, 23);
            this.NumberTextBox.TabIndex = 13;
            // 
            // SectionNumericUpDown
            // 
            this.SectionNumericUpDown.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.SectionNumericUpDown.Location = new System.Drawing.Point(242, 8);
            this.SectionNumericUpDown.Maximum = new decimal(new int[] {
            9000,
            0,
            0,
            0});
            this.SectionNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SectionNumericUpDown.Name = "SectionNumericUpDown";
            this.SectionNumericUpDown.Size = new System.Drawing.Size(53, 23);
            this.SectionNumericUpDown.TabIndex = 12;
            this.SectionNumericUpDown.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(178, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "검사구간:";
            // 
            // DataCountLabel
            // 
            this.DataCountLabel.AutoSize = true;
            this.DataCountLabel.Location = new System.Drawing.Point(12, 10);
            this.DataCountLabel.Name = "DataCountLabel";
            this.DataCountLabel.Size = new System.Drawing.Size(98, 15);
            this.DataCountLabel.TabIndex = 10;
            this.DataCountLabel.Text = "데이터 번호갯수:";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Next5TextBox);
            this.panel1.Controls.Add(this.Max5Label);
            this.panel1.Controls.Add(this.Real5Label);
            this.panel1.Controls.Add(this.Next4TextBox);
            this.panel1.Controls.Add(this.Max4Label);
            this.panel1.Controls.Add(this.Real4Label);
            this.panel1.Controls.Add(this.Next3TextBox);
            this.panel1.Controls.Add(this.Max3Label);
            this.panel1.Controls.Add(this.Real3Label);
            this.panel1.Controls.Add(this.Next2TextBox);
            this.panel1.Controls.Add(this.Max2Label);
            this.panel1.Controls.Add(this.Real2Label);
            this.panel1.Controls.Add(this.Next1TextBox);
            this.panel1.Controls.Add(this.Max1Label);
            this.panel1.Controls.Add(this.Real1Label);
            this.panel1.Controls.Add(this.NextChulsuTextBox);
            this.panel1.Controls.Add(this.NextMaxLabel);
            this.panel1.Controls.Add(this.NextRealLabel);
            this.panel1.Location = new System.Drawing.Point(644, 144);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(451, 705);
            this.panel1.TabIndex = 40;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.BadListBox);
            this.panel3.Controls.Add(this.GoodListBox);
            this.panel3.Controls.Add(this.AddFilterButton);
            this.panel3.Controls.Add(this.UseGridButton);
            this.panel3.Controls.Add(this.GoodBadButton);
            this.panel3.Controls.Add(this.LimitWriteButton);
            this.panel3.Controls.Add(this.numericUpDown1);
            this.panel3.Controls.Add(this.label8);
            this.panel3.Location = new System.Drawing.Point(12, 473);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(434, 222);
            this.panel3.TabIndex = 35;
            // 
            // BadListBox
            // 
            this.BadListBox.ContextMenuStrip = this.contextMenuStrip1;
            this.BadListBox.FormattingEnabled = true;
            this.BadListBox.Location = new System.Drawing.Point(115, 97);
            this.BadListBox.Name = "BadListBox";
            this.BadListBox.Size = new System.Drawing.Size(189, 112);
            this.BadListBox.TabIndex = 7;
            this.BadListBox.SelectedIndexChanged += new System.EventHandler(this.BadListBox_SelectedIndexChanged);
            // 
            // GoodListBox
            // 
            this.GoodListBox.FormattingEnabled = true;
            this.GoodListBox.ItemHeight = 15;
            this.GoodListBox.Location = new System.Drawing.Point(115, 8);
            this.GoodListBox.Name = "GoodListBox";
            this.GoodListBox.Size = new System.Drawing.Size(189, 79);
            this.GoodListBox.TabIndex = 6;
            this.GoodListBox.SelectedIndexChanged += new System.EventHandler(this.GoodistBox_SelectedIndexChanged);
            // 
            // AddFilterButton
            // 
            this.AddFilterButton.Enabled = false;
            this.AddFilterButton.Location = new System.Drawing.Point(312, 170);
            this.AddFilterButton.Name = "AddFilterButton";
            this.AddFilterButton.Size = new System.Drawing.Size(112, 39);
            this.AddFilterButton.TabIndex = 5;
            this.AddFilterButton.Text = "제외필터에 추가";
            this.AddFilterButton.UseVisualStyleBackColor = true;
            this.AddFilterButton.Click += new System.EventHandler(this.AddFilterButton_Click);
            // 
            // UseGridButton
            // 
            this.UseGridButton.Enabled = false;
            this.UseGridButton.Location = new System.Drawing.Point(312, 97);
            this.UseGridButton.Name = "UseGridButton";
            this.UseGridButton.Size = new System.Drawing.Size(112, 39);
            this.UseGridButton.TabIndex = 4;
            this.UseGridButton.Text = "그리드뷰 사용";
            this.UseGridButton.UseVisualStyleBackColor = true;
            this.UseGridButton.Click += new System.EventHandler(this.UseGridButton_Click);
            // 
            // GoodBadButton
            // 
            this.GoodBadButton.Location = new System.Drawing.Point(312, 8);
            this.GoodBadButton.Name = "GoodBadButton";
            this.GoodBadButton.Size = new System.Drawing.Size(112, 39);
            this.GoodBadButton.TabIndex = 3;
            this.GoodBadButton.Text = "호번악번 검사";
            this.GoodBadButton.UseVisualStyleBackColor = true;
            this.GoodBadButton.Click += new System.EventHandler(this.GoodBadButton_Click);
            // 
            // LimitWriteButton
            // 
            this.LimitWriteButton.Location = new System.Drawing.Point(10, 55);
            this.LimitWriteButton.Name = "LimitWriteButton";
            this.LimitWriteButton.Size = new System.Drawing.Size(99, 154);
            this.LimitWriteButton.TabIndex = 2;
            this.LimitWriteButton.Text = "최저, 최고, 최적값 기록하기";
            this.LimitWriteButton.UseVisualStyleBackColor = true;
            this.LimitWriteButton.Click += new System.EventHandler(this.LimitWriteButton_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 3;
            this.numericUpDown1.Increment = new decimal(new int[] {
            5,
            0,
            0,
            196608});
            this.numericUpDown1.Location = new System.Drawing.Point(50, 16);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            9,
            0,
            0,
            65536});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(59, 23);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 15);
            this.label8.TabIndex = 0;
            this.label8.Text = "한계:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.radioButton3);
            this.panel2.Controls.Add(this.MinMaxRadioButton);
            this.panel2.Controls.Add(this.PerfectRadioButton);
            this.panel2.Location = new System.Drawing.Point(12, 429);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(425, 38);
            this.panel2.TabIndex = 34;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(310, 8);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(89, 19);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.Text = "한계 초기화";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // MinMaxRadioButton
            // 
            this.MinMaxRadioButton.AutoSize = true;
            this.MinMaxRadioButton.Location = new System.Drawing.Point(150, 8);
            this.MinMaxRadioButton.Name = "MinMaxRadioButton";
            this.MinMaxRadioButton.Size = new System.Drawing.Size(120, 19);
            this.MinMaxRadioButton.TabIndex = 1;
            this.MinMaxRadioButton.Text = "최저, 최고값 적용";
            this.MinMaxRadioButton.UseVisualStyleBackColor = true;
            // 
            // PerfectRadioButton
            // 
            this.PerfectRadioButton.AutoSize = true;
            this.PerfectRadioButton.Checked = true;
            this.PerfectRadioButton.Location = new System.Drawing.Point(18, 8);
            this.PerfectRadioButton.Name = "PerfectRadioButton";
            this.PerfectRadioButton.Size = new System.Drawing.Size(89, 19);
            this.PerfectRadioButton.TabIndex = 0;
            this.PerfectRadioButton.TabStop = true;
            this.PerfectRadioButton.Text = "최적값 적용";
            this.PerfectRadioButton.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 7);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 15);
            this.label7.TabIndex = 33;
            this.label7.Text = "최종다음";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 367);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 15);
            this.label6.TabIndex = 32;
            this.label6.Text = "후방5구간";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 303);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 15);
            this.label5.TabIndex = 31;
            this.label5.Text = "후방4구간";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 237);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 15);
            this.label4.TabIndex = 30;
            this.label4.Text = "후방3구간";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 171);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 29;
            this.label3.Text = "후방2구간";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 28;
            this.label1.Text = "후방1구간";
            // 
            // Next5TextBox
            // 
            this.Next5TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Next5TextBox.Location = new System.Drawing.Point(12, 387);
            this.Next5TextBox.Multiline = true;
            this.Next5TextBox.Name = "Next5TextBox";
            this.Next5TextBox.Size = new System.Drawing.Size(425, 37);
            this.Next5TextBox.TabIndex = 27;
            // 
            // Max5Label
            // 
            this.Max5Label.AutoSize = true;
            this.Max5Label.Location = new System.Drawing.Point(235, 367);
            this.Max5Label.Name = "Max5Label";
            this.Max5Label.Size = new System.Drawing.Size(62, 15);
            this.Max5Label.TabIndex = 26;
            this.Max5Label.Text = "연속최대: ";
            // 
            // Real5Label
            // 
            this.Real5Label.AutoSize = true;
            this.Real5Label.Location = new System.Drawing.Point(91, 367);
            this.Real5Label.Name = "Real5Label";
            this.Real5Label.Size = new System.Drawing.Size(62, 15);
            this.Real5Label.TabIndex = 25;
            this.Real5Label.Text = "후방연속: ";
            // 
            // Next4TextBox
            // 
            this.Next4TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Next4TextBox.Location = new System.Drawing.Point(12, 323);
            this.Next4TextBox.Multiline = true;
            this.Next4TextBox.Name = "Next4TextBox";
            this.Next4TextBox.Size = new System.Drawing.Size(425, 37);
            this.Next4TextBox.TabIndex = 24;
            // 
            // Max4Label
            // 
            this.Max4Label.AutoSize = true;
            this.Max4Label.Location = new System.Drawing.Point(235, 303);
            this.Max4Label.Name = "Max4Label";
            this.Max4Label.Size = new System.Drawing.Size(62, 15);
            this.Max4Label.TabIndex = 23;
            this.Max4Label.Text = "연속최대: ";
            // 
            // Real4Label
            // 
            this.Real4Label.AutoSize = true;
            this.Real4Label.Location = new System.Drawing.Point(91, 303);
            this.Real4Label.Name = "Real4Label";
            this.Real4Label.Size = new System.Drawing.Size(62, 15);
            this.Real4Label.TabIndex = 22;
            this.Real4Label.Text = "후방연속: ";
            // 
            // Next3TextBox
            // 
            this.Next3TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Next3TextBox.Location = new System.Drawing.Point(12, 257);
            this.Next3TextBox.Multiline = true;
            this.Next3TextBox.Name = "Next3TextBox";
            this.Next3TextBox.Size = new System.Drawing.Size(425, 37);
            this.Next3TextBox.TabIndex = 21;
            // 
            // Max3Label
            // 
            this.Max3Label.AutoSize = true;
            this.Max3Label.Location = new System.Drawing.Point(235, 237);
            this.Max3Label.Name = "Max3Label";
            this.Max3Label.Size = new System.Drawing.Size(62, 15);
            this.Max3Label.TabIndex = 20;
            this.Max3Label.Text = "연속최대: ";
            // 
            // Real3Label
            // 
            this.Real3Label.AutoSize = true;
            this.Real3Label.Location = new System.Drawing.Point(91, 237);
            this.Real3Label.Name = "Real3Label";
            this.Real3Label.Size = new System.Drawing.Size(62, 15);
            this.Real3Label.TabIndex = 19;
            this.Real3Label.Text = "후방연속: ";
            // 
            // Next2TextBox
            // 
            this.Next2TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Next2TextBox.Location = new System.Drawing.Point(12, 191);
            this.Next2TextBox.Multiline = true;
            this.Next2TextBox.Name = "Next2TextBox";
            this.Next2TextBox.Size = new System.Drawing.Size(425, 37);
            this.Next2TextBox.TabIndex = 18;
            // 
            // Max2Label
            // 
            this.Max2Label.AutoSize = true;
            this.Max2Label.Location = new System.Drawing.Point(235, 171);
            this.Max2Label.Name = "Max2Label";
            this.Max2Label.Size = new System.Drawing.Size(62, 15);
            this.Max2Label.TabIndex = 17;
            this.Max2Label.Text = "연속최대: ";
            // 
            // Real2Label
            // 
            this.Real2Label.AutoSize = true;
            this.Real2Label.Location = new System.Drawing.Point(91, 171);
            this.Real2Label.Name = "Real2Label";
            this.Real2Label.Size = new System.Drawing.Size(62, 15);
            this.Real2Label.TabIndex = 16;
            this.Real2Label.Text = "후방연속: ";
            // 
            // Next1TextBox
            // 
            this.Next1TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Next1TextBox.Location = new System.Drawing.Point(12, 125);
            this.Next1TextBox.Multiline = true;
            this.Next1TextBox.Name = "Next1TextBox";
            this.Next1TextBox.Size = new System.Drawing.Size(425, 37);
            this.Next1TextBox.TabIndex = 15;
            // 
            // Max1Label
            // 
            this.Max1Label.AutoSize = true;
            this.Max1Label.Location = new System.Drawing.Point(235, 105);
            this.Max1Label.Name = "Max1Label";
            this.Max1Label.Size = new System.Drawing.Size(62, 15);
            this.Max1Label.TabIndex = 14;
            this.Max1Label.Text = "연속최대: ";
            // 
            // Real1Label
            // 
            this.Real1Label.AutoSize = true;
            this.Real1Label.Location = new System.Drawing.Point(91, 105);
            this.Real1Label.Name = "Real1Label";
            this.Real1Label.Size = new System.Drawing.Size(62, 15);
            this.Real1Label.TabIndex = 13;
            this.Real1Label.Text = "후방연속: ";
            // 
            // NextChulsuTextBox
            // 
            this.NextChulsuTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NextChulsuTextBox.Location = new System.Drawing.Point(12, 27);
            this.NextChulsuTextBox.Multiline = true;
            this.NextChulsuTextBox.Name = "NextChulsuTextBox";
            this.NextChulsuTextBox.Size = new System.Drawing.Size(425, 66);
            this.NextChulsuTextBox.TabIndex = 12;
            // 
            // NextMaxLabel
            // 
            this.NextMaxLabel.AutoSize = true;
            this.NextMaxLabel.Location = new System.Drawing.Point(235, 7);
            this.NextMaxLabel.Name = "NextMaxLabel";
            this.NextMaxLabel.Size = new System.Drawing.Size(62, 15);
            this.NextMaxLabel.TabIndex = 11;
            this.NextMaxLabel.Text = "연속최대: ";
            // 
            // NextRealLabel
            // 
            this.NextRealLabel.AutoSize = true;
            this.NextRealLabel.Location = new System.Drawing.Point(91, 7);
            this.NextRealLabel.Name = "NextRealLabel";
            this.NextRealLabel.Size = new System.Drawing.Size(62, 15);
            this.NextRealLabel.TabIndex = 10;
            this.NextRealLabel.Text = "후방연속: ";
            // 
            // ChulsuFilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 861);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.Name = "ChulsuFilterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "고정데이터 필터";
            this.Load += new System.EventHandler(this.KojeongFilterForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SectionNumericUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dataGridView1;
        private ListView listView1;
        private Label NowRealLabel;
        private Label NowMaxLabel;
        private Label SameCountLabel;
        private Label ShownCountLabel;
        private Label NonCountLabel;
        private Label PercentLabel;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem RemoveToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem CancelToolStripMenuItem;
        private Panel panel4;
        private TextBox NumberTextBox;
        private NumericUpDown SectionNumericUpDown;
        private Label label2;
        private Label DataCountLabel;
        private Panel panel1;
        private Panel panel3;
        private CheckedListBox BadListBox;
        private ListBox GoodListBox;
        private Button AddFilterButton;
        private Button UseGridButton;
        private Button GoodBadButton;
        private Button LimitWriteButton;
        private NumericUpDown numericUpDown1;
        private Label label8;
        private Panel panel2;
        private RadioButton radioButton3;
        private RadioButton MinMaxRadioButton;
        private RadioButton PerfectRadioButton;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label1;
        private TextBox Next5TextBox;
        private Label Max5Label;
        private Label Real5Label;
        private TextBox Next4TextBox;
        private Label Max4Label;
        private Label Real4Label;
        private TextBox Next3TextBox;
        private Label Max3Label;
        private Label Real3Label;
        private TextBox Next2TextBox;
        private Label Max2Label;
        private Label Real2Label;
        private TextBox Next1TextBox;
        private Label Max1Label;
        private Label Real1Label;
        private TextBox NextChulsuTextBox;
        private Label NextMaxLabel;
        private Label NextRealLabel;
        private Label PassLabel;
    }
}