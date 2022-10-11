namespace LuckyLottoApp.Forms
{
    partial class HaengByeolForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.LimitNumUpDown = new System.Windows.Forms.NumericUpDown();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.UpdwRadioButton = new System.Windows.Forms.RadioButton();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.SelectTextBox = new System.Windows.Forms.TextBox();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LimitNumUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.LimitNumUpDown);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.UpdwRadioButton);
            this.panel1.Controls.Add(this.ExecuteButton);
            this.panel1.Controls.Add(this.SelectTextBox);
            this.panel1.Controls.Add(this.ResultTextBox);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 161);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "사선연속";
            // 
            // LimitNumUpDown
            // 
            this.LimitNumUpDown.Location = new System.Drawing.Point(87, 75);
            this.LimitNumUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.LimitNumUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LimitNumUpDown.Name = "LimitNumUpDown";
            this.LimitNumUpDown.Size = new System.Drawing.Size(47, 23);
            this.LimitNumUpDown.TabIndex = 5;
            this.LimitNumUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.LimitNumUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(28, 36);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(97, 19);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.Text = "좌우사선검사";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // UpdwRadioButton
            // 
            this.UpdwRadioButton.AutoSize = true;
            this.UpdwRadioButton.Checked = true;
            this.UpdwRadioButton.Location = new System.Drawing.Point(28, 11);
            this.UpdwRadioButton.Name = "UpdwRadioButton";
            this.UpdwRadioButton.Size = new System.Drawing.Size(85, 19);
            this.UpdwRadioButton.TabIndex = 3;
            this.UpdwRadioButton.TabStop = true;
            this.UpdwRadioButton.Text = "상하선검사";
            this.UpdwRadioButton.UseVisualStyleBackColor = true;
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Location = new System.Drawing.Point(26, 104);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(108, 47);
            this.ExecuteButton.TabIndex = 2;
            this.ExecuteButton.Text = "검사하기";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // SelectTextBox
            // 
            this.SelectTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SelectTextBox.Location = new System.Drawing.Point(603, 10);
            this.SelectTextBox.Multiline = true;
            this.SelectTextBox.Name = "SelectTextBox";
            this.SelectTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.SelectTextBox.Size = new System.Drawing.Size(346, 141);
            this.SelectTextBox.TabIndex = 1;
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ResultTextBox.Location = new System.Drawing.Point(158, 10);
            this.ResultTextBox.Multiline = true;
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultTextBox.Size = new System.Drawing.Size(437, 141);
            this.ResultTextBox.TabIndex = 0;
            // 
            // listView1
            // 
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 179);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(960, 530);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView_ColumnClick);
            // 
            // HaengByeolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 721);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "HaengByeolForm";
            this.Text = "출현위치의 행출검사";
            this.Load += new System.EventHandler(this.HaengByeolForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LimitNumUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private RadioButton radioButton2;
        private RadioButton UpdwRadioButton;
        private Button ExecuteButton;
        private TextBox SelectTextBox;
        private TextBox ResultTextBox;
        private ListView listView1;
        private Label label1;
        private NumericUpDown LimitNumUpDown;
    }
}