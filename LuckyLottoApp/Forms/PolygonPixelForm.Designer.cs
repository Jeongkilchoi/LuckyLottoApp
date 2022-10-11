namespace LuckyLottoApp.Forms
{
    partial class PolygonPixelForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ItemComboBox = new System.Windows.Forms.ComboBox();
            this.OrderNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.GapComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.QueryTextBox = new System.Windows.Forms.TextBox();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.CheckButton = new System.Windows.Forms.Button();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.AllCheckButton = new System.Windows.Forms.Button();
            this.AllCheckListBox = new System.Windows.Forms.ListBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(400, 400);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox_Paint);
            // 
            // ItemComboBox
            // 
            this.ItemComboBox.FormattingEnabled = true;
            this.ItemComboBox.Location = new System.Drawing.Point(575, 12);
            this.ItemComboBox.Name = "ItemComboBox";
            this.ItemComboBox.Size = new System.Drawing.Size(87, 23);
            this.ItemComboBox.TabIndex = 1;
            this.ItemComboBox.SelectedIndexChanged += new System.EventHandler(this.ItemComboBox_SelectedIndexChanged);
            // 
            // OrderNumericUpDown
            // 
            this.OrderNumericUpDown.Location = new System.Drawing.Point(575, 76);
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
            this.OrderNumericUpDown.Size = new System.Drawing.Size(87, 23);
            this.OrderNumericUpDown.TabIndex = 2;
            this.OrderNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OrderNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.OrderNumericUpDown.ValueChanged += new System.EventHandler(this.OrderNumericUpDown_ValueChanged);
            // 
            // GapComboBox
            // 
            this.GapComboBox.FormattingEnabled = true;
            this.GapComboBox.Location = new System.Drawing.Point(575, 44);
            this.GapComboBox.Name = "GapComboBox";
            this.GapComboBox.Size = new System.Drawing.Size(87, 23);
            this.GapComboBox.TabIndex = 3;
            this.GapComboBox.SelectedIndexChanged += new System.EventHandler(this.GapComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(502, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "시퀸스이격";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(502, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "검사 항목:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(502, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "검사 회차:";
            // 
            // QueryTextBox
            // 
            this.QueryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.QueryTextBox.Location = new System.Drawing.Point(12, 435);
            this.QueryTextBox.Multiline = true;
            this.QueryTextBox.Name = "QueryTextBox";
            this.QueryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.QueryTextBox.Size = new System.Drawing.Size(190, 82);
            this.QueryTextBox.TabIndex = 7;
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Location = new System.Drawing.Point(285, 435);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(127, 51);
            this.ExecuteButton.TabIndex = 8;
            this.ExecuteButton.Text = "검사하기";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 15;
            this.listBox1.Location = new System.Drawing.Point(208, 435);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(71, 214);
            this.listBox1.TabIndex = 9;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.FindOrderListBox_SelectedIndexChanged);
            // 
            // CheckButton
            // 
            this.CheckButton.Enabled = false;
            this.CheckButton.Location = new System.Drawing.Point(285, 533);
            this.CheckButton.Name = "CheckButton";
            this.CheckButton.Size = new System.Drawing.Size(127, 51);
            this.CheckButton.TabIndex = 10;
            this.CheckButton.Text = "결과회차위치검사";
            this.CheckButton.UseVisualStyleBackColor = true;
            this.CheckButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResultTextBox.Location = new System.Drawing.Point(12, 533);
            this.ResultTextBox.Multiline = true;
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultTextBox.Size = new System.Drawing.Size(190, 116);
            this.ResultTextBox.TabIndex = 11;
            // 
            // AllCheckButton
            // 
            this.AllCheckButton.Location = new System.Drawing.Point(427, 111);
            this.AllCheckButton.Name = "AllCheckButton";
            this.AllCheckButton.Size = new System.Drawing.Size(235, 51);
            this.AllCheckButton.TabIndex = 12;
            this.AllCheckButton.Text = "전체 자동검사";
            this.AllCheckButton.UseVisualStyleBackColor = true;
            this.AllCheckButton.Click += new System.EventHandler(this.AllCheckButton_Click);
            // 
            // AllCheckListBox
            // 
            this.AllCheckListBox.FormattingEnabled = true;
            this.AllCheckListBox.ItemHeight = 15;
            this.AllCheckListBox.Location = new System.Drawing.Point(427, 174);
            this.AllCheckListBox.Name = "AllCheckListBox";
            this.AllCheckListBox.Size = new System.Drawing.Size(235, 469);
            this.AllCheckListBox.TabIndex = 13;
            // 
            // PolygonPixelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(674, 661);
            this.Controls.Add(this.AllCheckListBox);
            this.Controls.Add(this.AllCheckButton);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.CheckButton);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.ExecuteButton);
            this.Controls.Add(this.QueryTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GapComboBox);
            this.Controls.Add(this.OrderNumericUpDown);
            this.Controls.Add(this.ItemComboBox);
            this.Controls.Add(this.pictureBox1);
            this.MaximizeBox = false;
            this.Name = "PolygonPixelForm";
            this.Text = "폴리곤 픽셀 검사";
            this.Load += new System.EventHandler(this.DotRegionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureBox1;
        private ComboBox ItemComboBox;
        private NumericUpDown OrderNumericUpDown;
        private ComboBox GapComboBox;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox QueryTextBox;
        private Button ExecuteButton;
        private ListBox listBox1;
        private Button CheckButton;
        private TextBox ResultTextBox;
        private Button AllCheckButton;
        private ListBox AllCheckListBox;
    }
}