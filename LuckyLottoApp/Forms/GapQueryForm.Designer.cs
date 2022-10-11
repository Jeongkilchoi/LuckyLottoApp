namespace LuckyLottoApp.Forms
{
    partial class GapQueryForm
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
            this.ViewPictureBox = new System.Windows.Forms.PictureBox();
            this.HorFlowTextBox = new System.Windows.Forms.TextBox();
            this.VerFlowTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RowCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.WayComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.VerRadioButton = new System.Windows.Forms.RadioButton();
            this.HorRadioButton = new System.Windows.Forms.RadioButton();
            this.GapComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.OrderNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ViewPictureBox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RowCountNumericUpDown)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OrderNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ViewPictureBox
            // 
            this.ViewPictureBox.Location = new System.Drawing.Point(12, 91);
            this.ViewPictureBox.Name = "ViewPictureBox";
            this.ViewPictureBox.Size = new System.Drawing.Size(600, 600);
            this.ViewPictureBox.TabIndex = 0;
            this.ViewPictureBox.TabStop = false;
            this.ViewPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(this.ViewPictureBox_Paint);
            // 
            // HorFlowTextBox
            // 
            this.HorFlowTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HorFlowTextBox.Location = new System.Drawing.Point(627, 91);
            this.HorFlowTextBox.Multiline = true;
            this.HorFlowTextBox.Name = "HorFlowTextBox";
            this.HorFlowTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HorFlowTextBox.Size = new System.Drawing.Size(245, 280);
            this.HorFlowTextBox.TabIndex = 1;
            // 
            // VerFlowTextBox
            // 
            this.VerFlowTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VerFlowTextBox.Location = new System.Drawing.Point(627, 411);
            this.VerFlowTextBox.Multiline = true;
            this.VerFlowTextBox.Name = "VerFlowTextBox";
            this.VerFlowTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.VerFlowTextBox.Size = new System.Drawing.Size(245, 280);
            this.VerFlowTextBox.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.RowCountNumericUpDown);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.WayComboBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.GapComboBox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.OrderNumericUpDown);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 57);
            this.panel1.TabIndex = 3;
            // 
            // RowCountNumericUpDown
            // 
            this.RowCountNumericUpDown.Location = new System.Drawing.Point(771, 17);
            this.RowCountNumericUpDown.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.RowCountNumericUpDown.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.RowCountNumericUpDown.Name = "RowCountNumericUpDown";
            this.RowCountNumericUpDown.Size = new System.Drawing.Size(58, 23);
            this.RowCountNumericUpDown.TabIndex = 8;
            this.RowCountNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.RowCountNumericUpDown.ValueChanged += new System.EventHandler(this.RowCountNumericUpDown_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(715, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "행 갯수:";
            // 
            // WayComboBox
            // 
            this.WayComboBox.FormattingEnabled = true;
            this.WayComboBox.Location = new System.Drawing.Point(593, 16);
            this.WayComboBox.Name = "WayComboBox";
            this.WayComboBox.Size = new System.Drawing.Size(86, 23);
            this.WayComboBox.TabIndex = 6;
            this.WayComboBox.SelectedIndexChanged += new System.EventHandler(this.WayComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(529, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "검사방법:";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.VerRadioButton);
            this.panel2.Controls.Add(this.HorRadioButton);
            this.panel2.Location = new System.Drawing.Point(9, 9);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(182, 37);
            this.panel2.TabIndex = 4;
            // 
            // VerRadioButton
            // 
            this.VerRadioButton.AutoSize = true;
            this.VerRadioButton.Location = new System.Drawing.Point(104, 8);
            this.VerRadioButton.Name = "VerRadioButton";
            this.VerRadioButton.Size = new System.Drawing.Size(73, 19);
            this.VerRadioButton.TabIndex = 1;
            this.VerRadioButton.Text = "세로방향";
            this.VerRadioButton.UseVisualStyleBackColor = true;
            // 
            // HorRadioButton
            // 
            this.HorRadioButton.AutoSize = true;
            this.HorRadioButton.Checked = true;
            this.HorRadioButton.Location = new System.Drawing.Point(13, 8);
            this.HorRadioButton.Name = "HorRadioButton";
            this.HorRadioButton.Size = new System.Drawing.Size(73, 19);
            this.HorRadioButton.TabIndex = 0;
            this.HorRadioButton.TabStop = true;
            this.HorRadioButton.Text = "가로방향";
            this.HorRadioButton.UseVisualStyleBackColor = true;
            this.HorRadioButton.CheckedChanged += new System.EventHandler(this.HorRadioButton_CheckedChanged);
            // 
            // GapComboBox
            // 
            this.GapComboBox.FormattingEnabled = true;
            this.GapComboBox.Location = new System.Drawing.Point(430, 16);
            this.GapComboBox.Name = "GapComboBox";
            this.GapComboBox.Size = new System.Drawing.Size(62, 23);
            this.GapComboBox.TabIndex = 3;
            this.GapComboBox.SelectedIndexChanged += new System.EventHandler(this.GapComboBox_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(366, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "이격간격:";
            // 
            // OrderNumericUpDown
            // 
            this.OrderNumericUpDown.Location = new System.Drawing.Point(276, 17);
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
            this.OrderNumericUpDown.Size = new System.Drawing.Size(58, 23);
            this.OrderNumericUpDown.TabIndex = 1;
            this.OrderNumericUpDown.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.OrderNumericUpDown.ValueChanged += new System.EventHandler(this.OrderNumericUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "검사회차:";
            // 
            // GapQueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 703);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.VerFlowTextBox);
            this.Controls.Add(this.HorFlowTextBox);
            this.Controls.Add(this.ViewPictureBox);
            this.MaximizeBox = false;
            this.Name = "GapQueryForm";
            this.Text = "이격간격으로 데이터 조회";
            this.Load += new System.EventHandler(this.GapQueryForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ViewPictureBox)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RowCountNumericUpDown)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OrderNumericUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox ViewPictureBox;
        private TextBox HorFlowTextBox;
        private TextBox VerFlowTextBox;
        private Panel panel1;
        private NumericUpDown RowCountNumericUpDown;
        private Label label4;
        private ComboBox WayComboBox;
        private Label label3;
        private Panel panel2;
        private RadioButton VerRadioButton;
        private RadioButton HorRadioButton;
        private ComboBox GapComboBox;
        private Label label2;
        private NumericUpDown OrderNumericUpDown;
        private Label label1;
    }
}