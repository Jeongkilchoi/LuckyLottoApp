namespace LuckyLottoApp.Forms
{
    partial class OutlineFrameForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.RowCountComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.OrderNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.MinGapButton = new System.Windows.Forms.Button();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.MinMaxTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(960, 386);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 415);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(334, 334);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(703, 427);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "행의 수:";
            // 
            // RowCountComboBox
            // 
            this.RowCountComboBox.FormattingEnabled = true;
            this.RowCountComboBox.Items.AddRange(new object[] {
            "5",
            "7",
            "9"});
            this.RowCountComboBox.Location = new System.Drawing.Point(759, 424);
            this.RowCountComboBox.Name = "RowCountComboBox";
            this.RowCountComboBox.Size = new System.Drawing.Size(61, 23);
            this.RowCountComboBox.TabIndex = 3;
            this.RowCountComboBox.SelectedIndexChanged += new System.EventHandler(this.RowCountComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(840, 427);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "검사회차:";
            // 
            // OrderNumericUpDown
            // 
            this.OrderNumericUpDown.Location = new System.Drawing.Point(904, 425);
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
            this.OrderNumericUpDown.Size = new System.Drawing.Size(68, 23);
            this.OrderNumericUpDown.TabIndex = 5;
            this.OrderNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.OrderNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.OrderNumericUpDown.ValueChanged += new System.EventHandler(this.OrderNumericUpDown_ValueChanged);
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Location = new System.Drawing.Point(369, 417);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(131, 34);
            this.ExecuteButton.TabIndex = 6;
            this.ExecuteButton.Text = "검사하기";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // MinGapButton
            // 
            this.MinGapButton.Location = new System.Drawing.Point(525, 417);
            this.MinGapButton.Name = "MinGapButton";
            this.MinGapButton.Size = new System.Drawing.Size(131, 34);
            this.MinGapButton.TabIndex = 7;
            this.MinGapButton.Text = "최소간격찾기";
            this.MinGapButton.UseVisualStyleBackColor = true;
            this.MinGapButton.Click += new System.EventHandler(this.MinGapButton_Click);
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResultTextBox.Location = new System.Drawing.Point(369, 465);
            this.ResultTextBox.Multiline = true;
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultTextBox.Size = new System.Drawing.Size(384, 284);
            this.ResultTextBox.TabIndex = 8;
            // 
            // MinMaxTextBox
            // 
            this.MinMaxTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MinMaxTextBox.Location = new System.Drawing.Point(769, 465);
            this.MinMaxTextBox.Multiline = true;
            this.MinMaxTextBox.Name = "MinMaxTextBox";
            this.MinMaxTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MinMaxTextBox.Size = new System.Drawing.Size(203, 284);
            this.MinMaxTextBox.TabIndex = 9;
            // 
            // OutlineFrameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 761);
            this.Controls.Add(this.MinMaxTextBox);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.MinGapButton);
            this.Controls.Add(this.ExecuteButton);
            this.Controls.Add(this.OrderNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.RowCountComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.listView1);
            this.MaximizeBox = false;
            this.Name = "OutlineFrameForm";
            this.Text = "외곽선 프레임 검사";
            this.Load += new System.EventHandler(this.OutlineFrameForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrderNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListView listView1;
        private PictureBox pictureBox1;
        private Label label1;
        private ComboBox RowCountComboBox;
        private Label label2;
        private NumericUpDown OrderNumericUpDown;
        private Button ExecuteButton;
        private Button MinGapButton;
        private TextBox ResultTextBox;
        private TextBox MinMaxTextBox;
    }
}