namespace LuckyLottoApp.Forms
{
    partial class MarkovProcessForm
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
            this.NumberTextBox = new System.Windows.Forms.TextBox();
            this.ChulNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.JeoniNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.ItemComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChulNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.JeoniNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 12);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(860, 666);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListView1_ColumnClick);
            this.listView1.Click += new System.EventHandler(this.ListView1_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.NumberTextBox);
            this.panel1.Controls.Add(this.ChulNumericUpDown);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.JeoniNumericUpDown);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.ItemComboBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 697);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 52);
            this.panel1.TabIndex = 1;
            // 
            // NumberTextBox
            // 
            this.NumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumberTextBox.Location = new System.Drawing.Point(532, 13);
            this.NumberTextBox.Name = "NumberTextBox";
            this.NumberTextBox.Size = new System.Drawing.Size(315, 23);
            this.NumberTextBox.TabIndex = 6;
            // 
            // ChulNumericUpDown
            // 
            this.ChulNumericUpDown.Location = new System.Drawing.Point(462, 14);
            this.ChulNumericUpDown.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.ChulNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ChulNumericUpDown.Name = "ChulNumericUpDown";
            this.ChulNumericUpDown.Size = new System.Drawing.Size(43, 23);
            this.ChulNumericUpDown.TabIndex = 5;
            this.ChulNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ChulNumericUpDown.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.ChulNumericUpDown.ValueChanged += new System.EventHandler(this.ChulNumericUpDown_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(386, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "행렬의길이:";
            // 
            // JeoniNumericUpDown
            // 
            this.JeoniNumericUpDown.Location = new System.Drawing.Point(320, 14);
            this.JeoniNumericUpDown.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.JeoniNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.JeoniNumericUpDown.Name = "JeoniNumericUpDown";
            this.JeoniNumericUpDown.Size = new System.Drawing.Size(43, 23);
            this.JeoniNumericUpDown.TabIndex = 3;
            this.JeoniNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.JeoniNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.JeoniNumericUpDown.ValueChanged += new System.EventHandler(this.JeoniNumericUpDown_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(244, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "전이행렬수:";
            // 
            // ItemComboBox
            // 
            this.ItemComboBox.FormattingEnabled = true;
            this.ItemComboBox.Location = new System.Drawing.Point(77, 14);
            this.ItemComboBox.Name = "ItemComboBox";
            this.ItemComboBox.Size = new System.Drawing.Size(147, 23);
            this.ItemComboBox.TabIndex = 1;
            this.ItemComboBox.SelectedIndexChanged += new System.EventHandler(this.ItemComboBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "검사항목:";
            // 
            // MarkovProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 761);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listView1);
            this.MaximizeBox = false;
            this.Name = "MarkovProcessForm";
            this.Text = "마코프체인 데이터 검사";
            this.Load += new System.EventHandler(this.MarkovProcessForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ChulNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.JeoniNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ListView listView1;
        private Panel panel1;
        private TextBox NumberTextBox;
        private NumericUpDown ChulNumericUpDown;
        private Label label3;
        private NumericUpDown JeoniNumericUpDown;
        private Label label2;
        private ComboBox ItemComboBox;
        private Label label1;
    }
}