namespace LuckyLottoApp.Forms
{
    partial class MoveAvgForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.CheckItemComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SelectNumberTextBox = new System.Windows.Forms.TextBox();
            this.ResultListBox = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(860, 551);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ExecuteButton);
            this.panel1.Controls.Add(this.CheckItemComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.SelectNumberTextBox);
            this.panel1.Controls.Add(this.ResultListBox);
            this.panel1.Location = new System.Drawing.Point(12, 569);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 180);
            this.panel1.TabIndex = 1;
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Location = new System.Drawing.Point(25, 110);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(288, 56);
            this.ExecuteButton.TabIndex = 8;
            this.ExecuteButton.Text = "검사하기";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // CheckItemComboBox
            // 
            this.CheckItemComboBox.FormattingEnabled = true;
            this.CheckItemComboBox.Location = new System.Drawing.Point(89, 12);
            this.CheckItemComboBox.Name = "CheckItemComboBox";
            this.CheckItemComboBox.Size = new System.Drawing.Size(224, 23);
            this.CheckItemComboBox.TabIndex = 3;
            this.CheckItemComboBox.SelectionChangeCommitted += new System.EventHandler(this.CheckItemComboBox_SelectionChangeCommitted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "검사항목:";
            // 
            // SelectNumberTextBox
            // 
            this.SelectNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SelectNumberTextBox.Location = new System.Drawing.Point(524, 12);
            this.SelectNumberTextBox.Multiline = true;
            this.SelectNumberTextBox.Name = "SelectNumberTextBox";
            this.SelectNumberTextBox.Size = new System.Drawing.Size(322, 154);
            this.SelectNumberTextBox.TabIndex = 1;
            // 
            // ResultListBox
            // 
            this.ResultListBox.Enabled = false;
            this.ResultListBox.FormattingEnabled = true;
            this.ResultListBox.ItemHeight = 15;
            this.ResultListBox.Location = new System.Drawing.Point(337, 12);
            this.ResultListBox.Name = "ResultListBox";
            this.ResultListBox.Size = new System.Drawing.Size(173, 154);
            this.ResultListBox.TabIndex = 0;
            this.ResultListBox.SelectedIndexChanged += new System.EventHandler(this.ResultListBox_SelectedIndexChanged);
            // 
            // MoveAvgForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 761);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listView1);
            this.MaximizeBox = false;
            this.Name = "MoveAvgForm";
            this.Text = "이동평균 검사";
            this.Load += new System.EventHandler(this.MoveAvgForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ListView listView1;
        private Panel panel1;
        private Button ExecuteButton;
        private ComboBox CheckItemComboBox;
        private Label label1;
        private TextBox SelectNumberTextBox;
        private ListBox ResultListBox;
    }
}