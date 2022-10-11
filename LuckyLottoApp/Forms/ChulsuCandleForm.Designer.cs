namespace LuckyLottoApp.Forms
{
    partial class ChulsuCandleForm
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
            this.PositionTextBox = new System.Windows.Forms.TextBox();
            this.LastArrayLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BeforeLabel = new System.Windows.Forms.Label();
            this.SelectItemComboBox = new System.Windows.Forms.ComboBox();
            this.NumberTextBox = new System.Windows.Forms.TextBox();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.ResultListBox = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CandlePictureBox = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CandlePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.PositionTextBox);
            this.panel1.Controls.Add(this.LastArrayLabel);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.BeforeLabel);
            this.panel1.Controls.Add(this.SelectItemComboBox);
            this.panel1.Controls.Add(this.NumberTextBox);
            this.panel1.Controls.Add(this.ExecuteButton);
            this.panel1.Controls.Add(this.ResultListBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 462);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(960, 182);
            this.panel1.TabIndex = 3;
            // 
            // PositionTextBox
            // 
            this.PositionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PositionTextBox.Location = new System.Drawing.Point(740, 10);
            this.PositionTextBox.Multiline = true;
            this.PositionTextBox.Name = "PositionTextBox";
            this.PositionTextBox.Size = new System.Drawing.Size(203, 40);
            this.PositionTextBox.TabIndex = 12;
            // 
            // LastArrayLabel
            // 
            this.LastArrayLabel.AutoSize = true;
            this.LastArrayLabel.Location = new System.Drawing.Point(13, 58);
            this.LastArrayLabel.Name = "LastArrayLabel";
            this.LastArrayLabel.Size = new System.Drawing.Size(62, 15);
            this.LastArrayLabel.TabIndex = 11;
            this.LastArrayLabel.Text = "최종출수: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "해당번호: ";
            // 
            // BeforeLabel
            // 
            this.BeforeLabel.AutoSize = true;
            this.BeforeLabel.Location = new System.Drawing.Point(13, 82);
            this.BeforeLabel.Name = "BeforeLabel";
            this.BeforeLabel.Size = new System.Drawing.Size(14, 15);
            this.BeforeLabel.TabIndex = 9;
            this.BeforeLabel.Text = ": ";
            // 
            // SelectItemComboBox
            // 
            this.SelectItemComboBox.FormattingEnabled = true;
            this.SelectItemComboBox.Location = new System.Drawing.Point(81, 10);
            this.SelectItemComboBox.Name = "SelectItemComboBox";
            this.SelectItemComboBox.Size = new System.Drawing.Size(181, 23);
            this.SelectItemComboBox.TabIndex = 6;
            this.SelectItemComboBox.SelectedIndexChanged += new System.EventHandler(this.SelectItemComboBox_SelectedIndexChanged);
            // 
            // NumberTextBox
            // 
            this.NumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumberTextBox.Location = new System.Drawing.Point(13, 141);
            this.NumberTextBox.Name = "NumberTextBox";
            this.NumberTextBox.Size = new System.Drawing.Size(515, 23);
            this.NumberTextBox.TabIndex = 5;
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Location = new System.Drawing.Point(740, 101);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(203, 63);
            this.ExecuteButton.TabIndex = 2;
            this.ExecuteButton.Text = "검사하기";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // ResultListBox
            // 
            this.ResultListBox.Enabled = false;
            this.ResultListBox.FormattingEnabled = true;
            this.ResultListBox.ItemHeight = 15;
            this.ResultListBox.Location = new System.Drawing.Point(541, 10);
            this.ResultListBox.Name = "ResultListBox";
            this.ResultListBox.Size = new System.Drawing.Size(183, 154);
            this.ResultListBox.TabIndex = 1;
            this.ResultListBox.SelectedIndexChanged += new System.EventHandler(this.ResultListBox_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "검사항목: ";
            // 
            // CandlePictureBox
            // 
            this.CandlePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CandlePictureBox.Location = new System.Drawing.Point(12, 12);
            this.CandlePictureBox.Name = "CandlePictureBox";
            this.CandlePictureBox.Size = new System.Drawing.Size(960, 444);
            this.CandlePictureBox.TabIndex = 2;
            this.CandlePictureBox.TabStop = false;
            this.CandlePictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.CandlePictureBox_Paint);
            this.CandlePictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CandlePictureBox_MouseMove);
            // 
            // ChulsuCandleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 660);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CandlePictureBox);
            this.MaximizeBox = false;
            this.Name = "ChulsuCandleForm";
            this.Text = "출수데이터 캔틀검사";
            this.Load += new System.EventHandler(this.ChulsuCandleForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CandlePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private Label label4;
        private ComboBox SelectItemComboBox;
        private TextBox NumberTextBox;
        private Button ExecuteButton;
        private ListBox ResultListBox;
        private Label label1;
        private PictureBox CandlePictureBox;
        private Label LastArrayLabel;
        private Label BeforeLabel;
        private TextBox PositionTextBox;
    }
}