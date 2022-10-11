namespace LuckyLottoApp.Forms
{
    partial class ChulsuDisitalForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.SequenceComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TermNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.listView1 = new System.Windows.Forms.ListView();
            this.CheckButton = new System.Windows.Forms.Button();
            this.QueryTextBox = new System.Windows.Forms.TextBox();
            this.SelectedButton = new System.Windows.Forms.Button();
            this.TermButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FindNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.TermNumericUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FindNumericUpDown)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(753, 449);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "시퀸스:";
            // 
            // SequenceComboBox
            // 
            this.SequenceComboBox.FormattingEnabled = true;
            this.SequenceComboBox.Location = new System.Drawing.Point(805, 446);
            this.SequenceComboBox.Name = "SequenceComboBox";
            this.SequenceComboBox.Size = new System.Drawing.Size(67, 23);
            this.SequenceComboBox.TabIndex = 2;
            this.SequenceComboBox.SelectedIndexChanged += new System.EventHandler(this.SequenceComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(741, 484);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "검사회귀:";
            // 
            // TermNumericUpDown
            // 
            this.TermNumericUpDown.Location = new System.Drawing.Point(805, 481);
            this.TermNumericUpDown.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.TermNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TermNumericUpDown.Name = "TermNumericUpDown";
            this.TermNumericUpDown.Size = new System.Drawing.Size(67, 23);
            this.TermNumericUpDown.TabIndex = 4;
            this.TermNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.TermNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TermNumericUpDown.ValueChanged += new System.EventHandler(this.TermNumericUpDown_ValueChanged);
            // 
            // listView1
            // 
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(860, 409);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // CheckButton
            // 
            this.CheckButton.Location = new System.Drawing.Point(10, 8);
            this.CheckButton.Name = "CheckButton";
            this.CheckButton.Size = new System.Drawing.Size(122, 36);
            this.CheckButton.TabIndex = 8;
            this.CheckButton.Text = "도트 검사하기";
            this.CheckButton.UseVisualStyleBackColor = true;
            this.CheckButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // QueryTextBox
            // 
            this.QueryTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.QueryTextBox.Location = new System.Drawing.Point(12, 427);
            this.QueryTextBox.Multiline = true;
            this.QueryTextBox.Name = "QueryTextBox";
            this.QueryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.QueryTextBox.Size = new System.Drawing.Size(702, 322);
            this.QueryTextBox.TabIndex = 9;
            // 
            // SelectedButton
            // 
            this.SelectedButton.Enabled = false;
            this.SelectedButton.Location = new System.Drawing.Point(10, 56);
            this.SelectedButton.Name = "SelectedButton";
            this.SelectedButton.Size = new System.Drawing.Size(122, 36);
            this.SelectedButton.TabIndex = 10;
            this.SelectedButton.Text = "전체선택";
            this.SelectedButton.UseVisualStyleBackColor = true;
            this.SelectedButton.Click += new System.EventHandler(this.SelectedButton_Click);
            // 
            // TermButton
            // 
            this.TermButton.Location = new System.Drawing.Point(9, 13);
            this.TermButton.Name = "TermButton";
            this.TermButton.Size = new System.Drawing.Size(122, 36);
            this.TermButton.TabIndex = 11;
            this.TermButton.Text = "회기 검사하기";
            this.TermButton.UseVisualStyleBackColor = true;
            this.TermButton.Click += new System.EventHandler(this.TermButton_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.FindNumericUpDown);
            this.panel1.Controls.Add(this.TermButton);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(731, 527);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(141, 99);
            this.panel1.TabIndex = 12;
            // 
            // FindNumericUpDown
            // 
            this.FindNumericUpDown.Enabled = false;
            this.FindNumericUpDown.Location = new System.Drawing.Point(73, 61);
            this.FindNumericUpDown.Maximum = new decimal(new int[] {
            45,
            0,
            0,
            0});
            this.FindNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FindNumericUpDown.Name = "FindNumericUpDown";
            this.FindNumericUpDown.Size = new System.Drawing.Size(56, 23);
            this.FindNumericUpDown.TabIndex = 6;
            this.FindNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.FindNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.FindNumericUpDown.ValueChanged += new System.EventHandler(this.FindNumericUpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "검사번호:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.SelectedButton);
            this.panel2.Controls.Add(this.CheckButton);
            this.panel2.Location = new System.Drawing.Point(731, 645);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(141, 104);
            this.panel2.TabIndex = 13;
            // 
            // ChulsuDisitalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 761);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.QueryTextBox);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.TermNumericUpDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SequenceComboBox);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "ChulsuDisitalForm";
            this.Text = "출수의 디지탈 변환 검사";
            this.Load += new System.EventHandler(this.ChulsuDisitalForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.TermNumericUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FindNumericUpDown)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label1;
        private ComboBox SequenceComboBox;
        private Label label2;
        private NumericUpDown TermNumericUpDown;
        private ListView listView1;
        private Button CheckButton;
        private TextBox QueryTextBox;
        private Button SelectedButton;
        private Button TermButton;
        private Panel panel1;
        private NumericUpDown FindNumericUpDown;
        private Label label3;
        private Panel panel2;
    }
}