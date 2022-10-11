namespace LuckyLottoApp.Forms
{
    partial class InnerBoxForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.VerFlowRadioButton = new System.Windows.Forms.RadioButton();
            this.HorFlowRadioButton = new System.Windows.Forms.RadioButton();
            this.KeyComboBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ItemListBox = new System.Windows.Forms.ListBox();
            this.ChulsuListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Max5Label = new System.Windows.Forms.Label();
            this.Real5Label = new System.Windows.Forms.Label();
            this.Max4Label = new System.Windows.Forms.Label();
            this.Real4Label = new System.Windows.Forms.Label();
            this.Max3Label = new System.Windows.Forms.Label();
            this.Real3Label = new System.Windows.Forms.Label();
            this.Max2Label = new System.Windows.Forms.Label();
            this.Real2Label = new System.Windows.Forms.Label();
            this.Max1Label = new System.Windows.Forms.Label();
            this.Real1Label = new System.Windows.Forms.Label();
            this.MaxNextLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.Next5TextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.Next4TextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.Next3TextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Next2TextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Next1TextBox = new System.Windows.Forms.TextBox();
            this.RealNextLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.NextTextBox = new System.Windows.Forms.TextBox();
            this.SameLabel = new System.Windows.Forms.Label();
            this.ThreeLabel = new System.Windows.Forms.Label();
            this.TwoLabel = new System.Windows.Forms.Label();
            this.OneLabel = new System.Windows.Forms.Label();
            this.ZeroLabel = new System.Windows.Forms.Label();
            this.NumberTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BadListBox = new System.Windows.Forms.ListBox();
            this.GoodButton = new System.Windows.Forms.Button();
            this.GoodListBox = new System.Windows.Forms.ListBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.PassLabel = new System.Windows.Forms.Label();
            this.MaxLabel = new System.Windows.Forms.Label();
            this.RealLabel = new System.Windows.Forms.Label();
            this.PercentLabel = new System.Windows.Forms.Label();
            this.ShownLabel = new System.Windows.Forms.Label();
            this.NoneLabel = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.VerFlowRadioButton);
            this.groupBox1.Controls.Add(this.HorFlowRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(160, 84);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "검사 방향";
            // 
            // VerFlowRadioButton
            // 
            this.VerFlowRadioButton.AutoSize = true;
            this.VerFlowRadioButton.Location = new System.Drawing.Point(26, 52);
            this.VerFlowRadioButton.Name = "VerFlowRadioButton";
            this.VerFlowRadioButton.Size = new System.Drawing.Size(89, 19);
            this.VerFlowRadioButton.TabIndex = 1;
            this.VerFlowRadioButton.Text = "세로 데이터";
            this.VerFlowRadioButton.UseVisualStyleBackColor = true;
            // 
            // HorFlowRadioButton
            // 
            this.HorFlowRadioButton.AutoSize = true;
            this.HorFlowRadioButton.Checked = true;
            this.HorFlowRadioButton.Location = new System.Drawing.Point(26, 27);
            this.HorFlowRadioButton.Name = "HorFlowRadioButton";
            this.HorFlowRadioButton.Size = new System.Drawing.Size(89, 19);
            this.HorFlowRadioButton.TabIndex = 0;
            this.HorFlowRadioButton.TabStop = true;
            this.HorFlowRadioButton.Text = "가로 데이터";
            this.HorFlowRadioButton.UseVisualStyleBackColor = true;
            // 
            // KeyComboBox
            // 
            this.KeyComboBox.FormattingEnabled = true;
            this.KeyComboBox.Location = new System.Drawing.Point(15, 22);
            this.KeyComboBox.Name = "KeyComboBox";
            this.KeyComboBox.Size = new System.Drawing.Size(130, 23);
            this.KeyComboBox.TabIndex = 1;
            this.KeyComboBox.SelectedIndexChanged += new System.EventHandler(this.KeyComboBox_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.KeyComboBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(160, 58);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "검사 항목";
            // 
            // ItemListBox
            // 
            this.ItemListBox.FormattingEnabled = true;
            this.ItemListBox.ItemHeight = 15;
            this.ItemListBox.Location = new System.Drawing.Point(12, 168);
            this.ItemListBox.Name = "ItemListBox";
            this.ItemListBox.Size = new System.Drawing.Size(160, 244);
            this.ItemListBox.TabIndex = 3;
            this.ItemListBox.SelectedIndexChanged += new System.EventHandler(this.ItemListBox_SelectedIndexChanged);
            // 
            // ChulsuListView
            // 
            this.ChulsuListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.ChulsuListView.GridLines = true;
            this.ChulsuListView.Location = new System.Drawing.Point(191, 12);
            this.ChulsuListView.MultiSelect = false;
            this.ChulsuListView.Name = "ChulsuListView";
            this.ChulsuListView.Size = new System.Drawing.Size(125, 754);
            this.ChulsuListView.TabIndex = 4;
            this.ChulsuListView.UseCompatibleStateImageBehavior = false;
            this.ChulsuListView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "회 차";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "출수";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 40;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Max5Label);
            this.panel1.Controls.Add(this.Real5Label);
            this.panel1.Controls.Add(this.Max4Label);
            this.panel1.Controls.Add(this.Real4Label);
            this.panel1.Controls.Add(this.Max3Label);
            this.panel1.Controls.Add(this.Real3Label);
            this.panel1.Controls.Add(this.Max2Label);
            this.panel1.Controls.Add(this.Real2Label);
            this.panel1.Controls.Add(this.Max1Label);
            this.panel1.Controls.Add(this.Real1Label);
            this.panel1.Controls.Add(this.MaxNextLabel);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.Next5TextBox);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.Next4TextBox);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.Next3TextBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.Next2TextBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.Next1TextBox);
            this.panel1.Controls.Add(this.RealNextLabel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.NextTextBox);
            this.panel1.Location = new System.Drawing.Point(341, 127);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(634, 639);
            this.panel1.TabIndex = 5;
            // 
            // Max5Label
            // 
            this.Max5Label.AutoSize = true;
            this.Max5Label.Location = new System.Drawing.Point(312, 568);
            this.Max5Label.Name = "Max5Label";
            this.Max5Label.Size = new System.Drawing.Size(62, 15);
            this.Max5Label.TabIndex = 27;
            this.Max5Label.Text = "연속최대: ";
            // 
            // Real5Label
            // 
            this.Real5Label.AutoSize = true;
            this.Real5Label.Location = new System.Drawing.Point(166, 568);
            this.Real5Label.Name = "Real5Label";
            this.Real5Label.Size = new System.Drawing.Size(62, 15);
            this.Real5Label.TabIndex = 26;
            this.Real5Label.Text = "후방연속: ";
            // 
            // Max4Label
            // 
            this.Max4Label.AutoSize = true;
            this.Max4Label.Location = new System.Drawing.Point(312, 494);
            this.Max4Label.Name = "Max4Label";
            this.Max4Label.Size = new System.Drawing.Size(62, 15);
            this.Max4Label.TabIndex = 25;
            this.Max4Label.Text = "연속최대: ";
            // 
            // Real4Label
            // 
            this.Real4Label.AutoSize = true;
            this.Real4Label.Location = new System.Drawing.Point(166, 494);
            this.Real4Label.Name = "Real4Label";
            this.Real4Label.Size = new System.Drawing.Size(62, 15);
            this.Real4Label.TabIndex = 24;
            this.Real4Label.Text = "후방연속: ";
            // 
            // Max3Label
            // 
            this.Max3Label.AutoSize = true;
            this.Max3Label.Location = new System.Drawing.Point(312, 420);
            this.Max3Label.Name = "Max3Label";
            this.Max3Label.Size = new System.Drawing.Size(62, 15);
            this.Max3Label.TabIndex = 23;
            this.Max3Label.Text = "연속최대: ";
            // 
            // Real3Label
            // 
            this.Real3Label.AutoSize = true;
            this.Real3Label.Location = new System.Drawing.Point(166, 420);
            this.Real3Label.Name = "Real3Label";
            this.Real3Label.Size = new System.Drawing.Size(62, 15);
            this.Real3Label.TabIndex = 22;
            this.Real3Label.Text = "후방연속: ";
            // 
            // Max2Label
            // 
            this.Max2Label.AutoSize = true;
            this.Max2Label.Location = new System.Drawing.Point(312, 289);
            this.Max2Label.Name = "Max2Label";
            this.Max2Label.Size = new System.Drawing.Size(62, 15);
            this.Max2Label.TabIndex = 21;
            this.Max2Label.Text = "연속최대: ";
            // 
            // Real2Label
            // 
            this.Real2Label.AutoSize = true;
            this.Real2Label.Location = new System.Drawing.Point(166, 289);
            this.Real2Label.Name = "Real2Label";
            this.Real2Label.Size = new System.Drawing.Size(62, 15);
            this.Real2Label.TabIndex = 20;
            this.Real2Label.Text = "후방연속: ";
            // 
            // Max1Label
            // 
            this.Max1Label.AutoSize = true;
            this.Max1Label.Location = new System.Drawing.Point(312, 153);
            this.Max1Label.Name = "Max1Label";
            this.Max1Label.Size = new System.Drawing.Size(62, 15);
            this.Max1Label.TabIndex = 19;
            this.Max1Label.Text = "연속최대: ";
            // 
            // Real1Label
            // 
            this.Real1Label.AutoSize = true;
            this.Real1Label.Location = new System.Drawing.Point(166, 153);
            this.Real1Label.Name = "Real1Label";
            this.Real1Label.Size = new System.Drawing.Size(62, 15);
            this.Real1Label.TabIndex = 18;
            this.Real1Label.Text = "후방연속: ";
            // 
            // MaxNextLabel
            // 
            this.MaxNextLabel.AutoSize = true;
            this.MaxNextLabel.Location = new System.Drawing.Point(312, 17);
            this.MaxNextLabel.Name = "MaxNextLabel";
            this.MaxNextLabel.Size = new System.Drawing.Size(62, 15);
            this.MaxNextLabel.TabIndex = 17;
            this.MaxNextLabel.Text = "연속최대: ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 568);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 15);
            this.label10.TabIndex = 16;
            this.label10.Text = "후방5구간";
            // 
            // Next5TextBox
            // 
            this.Next5TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Next5TextBox.Location = new System.Drawing.Point(20, 589);
            this.Next5TextBox.Multiline = true;
            this.Next5TextBox.Name = "Next5TextBox";
            this.Next5TextBox.Size = new System.Drawing.Size(598, 37);
            this.Next5TextBox.TabIndex = 15;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(20, 494);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 15);
            this.label9.TabIndex = 14;
            this.label9.Text = "후방4구간";
            // 
            // Next4TextBox
            // 
            this.Next4TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Next4TextBox.Location = new System.Drawing.Point(20, 515);
            this.Next4TextBox.Multiline = true;
            this.Next4TextBox.Name = "Next4TextBox";
            this.Next4TextBox.Size = new System.Drawing.Size(598, 37);
            this.Next4TextBox.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 420);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "후방3구간";
            // 
            // Next3TextBox
            // 
            this.Next3TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Next3TextBox.Location = new System.Drawing.Point(20, 441);
            this.Next3TextBox.Multiline = true;
            this.Next3TextBox.Name = "Next3TextBox";
            this.Next3TextBox.Size = new System.Drawing.Size(598, 37);
            this.Next3TextBox.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 289);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "후방2구간";
            // 
            // Next2TextBox
            // 
            this.Next2TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Next2TextBox.Location = new System.Drawing.Point(20, 311);
            this.Next2TextBox.Multiline = true;
            this.Next2TextBox.Name = "Next2TextBox";
            this.Next2TextBox.Size = new System.Drawing.Size(598, 92);
            this.Next2TextBox.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 153);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "후방1구간";
            // 
            // Next1TextBox
            // 
            this.Next1TextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Next1TextBox.Location = new System.Drawing.Point(20, 175);
            this.Next1TextBox.Multiline = true;
            this.Next1TextBox.Name = "Next1TextBox";
            this.Next1TextBox.Size = new System.Drawing.Size(598, 92);
            this.Next1TextBox.TabIndex = 7;
            // 
            // RealNextLabel
            // 
            this.RealNextLabel.AutoSize = true;
            this.RealNextLabel.Location = new System.Drawing.Point(166, 17);
            this.RealNextLabel.Name = "RealNextLabel";
            this.RealNextLabel.Size = new System.Drawing.Size(62, 15);
            this.RealNextLabel.TabIndex = 2;
            this.RealNextLabel.Text = "후방연속: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "최종다음";
            // 
            // NextTextBox
            // 
            this.NextTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NextTextBox.Location = new System.Drawing.Point(20, 39);
            this.NextTextBox.Multiline = true;
            this.NextTextBox.Name = "NextTextBox";
            this.NextTextBox.Size = new System.Drawing.Size(598, 92);
            this.NextTextBox.TabIndex = 5;
            // 
            // SameLabel
            // 
            this.SameLabel.AutoSize = true;
            this.SameLabel.Location = new System.Drawing.Point(20, 61);
            this.SameLabel.Name = "SameLabel";
            this.SameLabel.Size = new System.Drawing.Size(50, 15);
            this.SameLabel.TabIndex = 13;
            this.SameLabel.Text = "동출수: ";
            // 
            // ThreeLabel
            // 
            this.ThreeLabel.AutoSize = true;
            this.ThreeLabel.Location = new System.Drawing.Point(494, 85);
            this.ThreeLabel.Name = "ThreeLabel";
            this.ThreeLabel.Size = new System.Drawing.Size(26, 15);
            this.ThreeLabel.TabIndex = 3;
            this.ThreeLabel.Text = "3출";
            // 
            // TwoLabel
            // 
            this.TwoLabel.AutoSize = true;
            this.TwoLabel.Location = new System.Drawing.Point(336, 85);
            this.TwoLabel.Name = "TwoLabel";
            this.TwoLabel.Size = new System.Drawing.Size(26, 15);
            this.TwoLabel.TabIndex = 2;
            this.TwoLabel.Text = "2출";
            // 
            // OneLabel
            // 
            this.OneLabel.AutoSize = true;
            this.OneLabel.Location = new System.Drawing.Point(178, 85);
            this.OneLabel.Name = "OneLabel";
            this.OneLabel.Size = new System.Drawing.Size(26, 15);
            this.OneLabel.TabIndex = 1;
            this.OneLabel.Text = "1출";
            // 
            // ZeroLabel
            // 
            this.ZeroLabel.AutoSize = true;
            this.ZeroLabel.Location = new System.Drawing.Point(20, 85);
            this.ZeroLabel.Name = "ZeroLabel";
            this.ZeroLabel.Size = new System.Drawing.Size(26, 15);
            this.ZeroLabel.TabIndex = 0;
            this.ZeroLabel.Text = "0출";
            // 
            // NumberTextBox
            // 
            this.NumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumberTextBox.Location = new System.Drawing.Point(84, 12);
            this.NumberTextBox.Name = "NumberTextBox";
            this.NumberTextBox.Size = new System.Drawing.Size(82, 23);
            this.NumberTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "해당번호:";
            // 
            // BadListBox
            // 
            this.BadListBox.FormattingEnabled = true;
            this.BadListBox.ItemHeight = 15;
            this.BadListBox.Location = new System.Drawing.Point(12, 627);
            this.BadListBox.Name = "BadListBox";
            this.BadListBox.Size = new System.Drawing.Size(160, 139);
            this.BadListBox.TabIndex = 6;
            this.BadListBox.SelectedIndexChanged += new System.EventHandler(this.ResultListBox_SelectedIndexChanged);
            // 
            // GoodButton
            // 
            this.GoodButton.Location = new System.Drawing.Point(12, 427);
            this.GoodButton.Name = "GoodButton";
            this.GoodButton.Size = new System.Drawing.Size(160, 40);
            this.GoodButton.TabIndex = 8;
            this.GoodButton.Text = "호번, 악번검사";
            this.GoodButton.UseVisualStyleBackColor = true;
            this.GoodButton.Click += new System.EventHandler(this.GoodButton_Click);
            // 
            // GoodListBox
            // 
            this.GoodListBox.FormattingEnabled = true;
            this.GoodListBox.ItemHeight = 15;
            this.GoodListBox.Location = new System.Drawing.Point(12, 481);
            this.GoodListBox.Name = "GoodListBox";
            this.GoodListBox.Size = new System.Drawing.Size(160, 139);
            this.GoodListBox.TabIndex = 9;
            this.GoodListBox.SelectedIndexChanged += new System.EventHandler(this.GoodListBox_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.PassLabel);
            this.panel3.Controls.Add(this.MaxLabel);
            this.panel3.Controls.Add(this.RealLabel);
            this.panel3.Controls.Add(this.PercentLabel);
            this.panel3.Controls.Add(this.ShownLabel);
            this.panel3.Controls.Add(this.NoneLabel);
            this.panel3.Controls.Add(this.ThreeLabel);
            this.panel3.Controls.Add(this.SameLabel);
            this.panel3.Controls.Add(this.TwoLabel);
            this.panel3.Controls.Add(this.NumberTextBox);
            this.panel3.Controls.Add(this.OneLabel);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.ZeroLabel);
            this.panel3.Location = new System.Drawing.Point(341, 12);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(634, 109);
            this.panel3.TabIndex = 10;
            // 
            // PassLabel
            // 
            this.PassLabel.AutoSize = true;
            this.PassLabel.Location = new System.Drawing.Point(178, 15);
            this.PassLabel.Name = "PassLabel";
            this.PassLabel.Size = new System.Drawing.Size(62, 15);
            this.PassLabel.TabIndex = 19;
            this.PassLabel.Text = "검사결과: ";
            // 
            // MaxLabel
            // 
            this.MaxLabel.AutoSize = true;
            this.MaxLabel.Location = new System.Drawing.Point(494, 37);
            this.MaxLabel.Name = "MaxLabel";
            this.MaxLabel.Size = new System.Drawing.Size(62, 15);
            this.MaxLabel.TabIndex = 18;
            this.MaxLabel.Text = "연속최대: ";
            // 
            // RealLabel
            // 
            this.RealLabel.AutoSize = true;
            this.RealLabel.Location = new System.Drawing.Point(336, 37);
            this.RealLabel.Name = "RealLabel";
            this.RealLabel.Size = new System.Drawing.Size(62, 15);
            this.RealLabel.TabIndex = 17;
            this.RealLabel.Text = "후방연속: ";
            // 
            // PercentLabel
            // 
            this.PercentLabel.AutoSize = true;
            this.PercentLabel.Location = new System.Drawing.Point(494, 61);
            this.PercentLabel.Name = "PercentLabel";
            this.PercentLabel.Size = new System.Drawing.Size(50, 15);
            this.PercentLabel.TabIndex = 16;
            this.PercentLabel.Text = "출수율: ";
            // 
            // ShownLabel
            // 
            this.ShownLabel.AutoSize = true;
            this.ShownLabel.Location = new System.Drawing.Point(336, 61);
            this.ShownLabel.Name = "ShownLabel";
            this.ShownLabel.Size = new System.Drawing.Size(50, 15);
            this.ShownLabel.TabIndex = 15;
            this.ShownLabel.Text = "유출수: ";
            // 
            // NoneLabel
            // 
            this.NoneLabel.AutoSize = true;
            this.NoneLabel.Location = new System.Drawing.Point(178, 61);
            this.NoneLabel.Name = "NoneLabel";
            this.NoneLabel.Size = new System.Drawing.Size(50, 15);
            this.NoneLabel.TabIndex = 14;
            this.NoneLabel.Text = "무출수: ";
            // 
            // InnerBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(987, 778);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.GoodListBox);
            this.Controls.Add(this.GoodButton);
            this.Controls.Add(this.BadListBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ChulsuListView);
            this.Controls.Add(this.ItemListBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.Name = "InnerBoxForm";
            this.Text = "3데이터 내부검사";
            this.Load += new System.EventHandler(this.InnerBoxForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private GroupBox groupBox1;
        private RadioButton VerFlowRadioButton;
        private RadioButton HorFlowRadioButton;
        private ComboBox KeyComboBox;
        private GroupBox groupBox2;
        private ListBox ItemListBox;
        private ListView ChulsuListView;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private Panel panel1;
        private TextBox NumberTextBox;
        private Label label1;
        private Label label5;
        private TextBox Next3TextBox;
        private Label label4;
        private TextBox Next2TextBox;
        private Label label3;
        private TextBox Next1TextBox;
        private Label label2;
        private TextBox NextTextBox;
        private Label ThreeLabel;
        private Label TwoLabel;
        private Label OneLabel;
        private Label ZeroLabel;
        private Label RealNextLabel;
        private Label SameLabel;
        private ListBox BadListBox;
        private Button GoodButton;
        private ListBox GoodListBox;
        private Label Max5Label;
        private Label Real5Label;
        private Label Max4Label;
        private Label Real4Label;
        private Label Max3Label;
        private Label Real3Label;
        private Label Max2Label;
        private Label Real2Label;
        private Label Max1Label;
        private Label Real1Label;
        private Label MaxNextLabel;
        private Label label10;
        private TextBox Next5TextBox;
        private Label label9;
        private TextBox Next4TextBox;
        private Panel panel3;
        private Label PercentLabel;
        private Label ShownLabel;
        private Label NoneLabel;
        private Label MaxLabel;
        private Label RealLabel;
        private Label PassLabel;
    }
}