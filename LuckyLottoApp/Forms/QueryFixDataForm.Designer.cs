namespace LuckyLottoApp.Forms
{
    partial class QueryFixDataForm
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
            this.HorFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.HorCheckBox = new System.Windows.Forms.CheckBox();
            this.VerCheckBox = new System.Windows.Forms.CheckBox();
            this.VerFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.GuCheckBox = new System.Windows.Forms.CheckBox();
            this.RowCountComboBox = new System.Windows.Forms.ComboBox();
            this.GuFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ATextBox = new System.Windows.Forms.TextBox();
            this.BTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CombineTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.ResultTextBox = new System.Windows.Forms.TextBox();
            this.ChangeButton = new System.Windows.Forms.Button();
            this.TypeCheckBox = new System.Windows.Forms.CheckBox();
            this.ETextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.TypeFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.ShowLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(382, 265);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "행갯수:";
            // 
            // HorFlowLayoutPanel
            // 
            this.HorFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HorFlowLayoutPanel.Location = new System.Drawing.Point(354, 84);
            this.HorFlowLayoutPanel.Name = "HorFlowLayoutPanel";
            this.HorFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(10, 5, 0, 0);
            this.HorFlowLayoutPanel.Size = new System.Drawing.Size(191, 160);
            this.HorFlowLayoutPanel.TabIndex = 2;
            // 
            // HorCheckBox
            // 
            this.HorCheckBox.AutoSize = true;
            this.HorCheckBox.Location = new System.Drawing.Point(374, 59);
            this.HorCheckBox.Name = "HorCheckBox";
            this.HorCheckBox.Size = new System.Drawing.Size(98, 19);
            this.HorCheckBox.TabIndex = 3;
            this.HorCheckBox.Text = "Hor chulData";
            this.HorCheckBox.UseVisualStyleBackColor = true;
            this.HorCheckBox.CheckedChanged += new System.EventHandler(this.HorCheckBox_CheckedChanged);
            // 
            // VerCheckBox
            // 
            this.VerCheckBox.AutoSize = true;
            this.VerCheckBox.Location = new System.Drawing.Point(583, 59);
            this.VerCheckBox.Name = "VerCheckBox";
            this.VerCheckBox.Size = new System.Drawing.Size(96, 19);
            this.VerCheckBox.TabIndex = 5;
            this.VerCheckBox.Text = "Ver chulData";
            this.VerCheckBox.UseVisualStyleBackColor = true;
            this.VerCheckBox.CheckedChanged += new System.EventHandler(this.VerCheckBox_CheckedChanged);
            // 
            // VerFlowLayoutPanel
            // 
            this.VerFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.VerFlowLayoutPanel.Location = new System.Drawing.Point(563, 84);
            this.VerFlowLayoutPanel.Name = "VerFlowLayoutPanel";
            this.VerFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(10, 5, 0, 0);
            this.VerFlowLayoutPanel.Size = new System.Drawing.Size(191, 160);
            this.VerFlowLayoutPanel.TabIndex = 4;
            // 
            // GuCheckBox
            // 
            this.GuCheckBox.AutoSize = true;
            this.GuCheckBox.Checked = true;
            this.GuCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.GuCheckBox.Location = new System.Drawing.Point(271, 59);
            this.GuCheckBox.Name = "GuCheckBox";
            this.GuCheckBox.Size = new System.Drawing.Size(60, 19);
            this.GuCheckBox.TabIndex = 7;
            this.GuCheckBox.Text = "Gu1-6";
            this.GuCheckBox.UseVisualStyleBackColor = true;
            this.GuCheckBox.CheckedChanged += new System.EventHandler(this.GuCheckBox_CheckedChanged);
            // 
            // RowCountComboBox
            // 
            this.RowCountComboBox.FormattingEnabled = true;
            this.RowCountComboBox.Items.AddRange(new object[] {
            "5",
            "7",
            "9"});
            this.RowCountComboBox.Location = new System.Drawing.Point(434, 262);
            this.RowCountComboBox.Name = "RowCountComboBox";
            this.RowCountComboBox.Size = new System.Drawing.Size(73, 23);
            this.RowCountComboBox.TabIndex = 8;
            this.RowCountComboBox.SelectedIndexChanged += new System.EventHandler(this.RowCountComboBox_SelectedIndexChanged);
            // 
            // GuFlowLayoutPanel
            // 
            this.GuFlowLayoutPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GuFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.GuFlowLayoutPanel.Location = new System.Drawing.Point(266, 84);
            this.GuFlowLayoutPanel.Name = "GuFlowLayoutPanel";
            this.GuFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(5, 3, 0, 0);
            this.GuFlowLayoutPanel.Size = new System.Drawing.Size(65, 160);
            this.GuFlowLayoutPanel.TabIndex = 9;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(230, 230);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.PictureBox1_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 265);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 15);
            this.label2.TabIndex = 11;
            this.label2.Text = "A(번호):";
            // 
            // ATextBox
            // 
            this.ATextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ATextBox.Location = new System.Drawing.Point(73, 262);
            this.ATextBox.Name = "ATextBox";
            this.ATextBox.Size = new System.Drawing.Size(227, 23);
            this.ATextBox.TabIndex = 12;
            this.ATextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ATextBox_KeyDown);
            this.ATextBox.Leave += new System.EventHandler(this.ATextBox_Leave);
            // 
            // BTextBox
            // 
            this.BTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.BTextBox.Location = new System.Drawing.Point(73, 291);
            this.BTextBox.Name = "BTextBox";
            this.BTextBox.Size = new System.Drawing.Size(434, 23);
            this.BTextBox.TabIndex = 14;
            this.BTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BTextBox_KeyDown);
            this.BTextBox.Leave += new System.EventHandler(this.BTextBox_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 294);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 15);
            this.label3.TabIndex = 13;
            this.label3.Text = "B(Gu): ";
            // 
            // CTextBox
            // 
            this.CTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CTextBox.Location = new System.Drawing.Point(73, 320);
            this.CTextBox.Name = "CTextBox";
            this.CTextBox.Size = new System.Drawing.Size(434, 23);
            this.CTextBox.TabIndex = 16;
            this.CTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CTextBox_KeyDown);
            this.CTextBox.Leave += new System.EventHandler(this.CTextBox_Leave);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 323);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 15);
            this.label4.TabIndex = 15;
            this.label4.Text = "C(Hor): ";
            // 
            // DTextBox
            // 
            this.DTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DTextBox.Location = new System.Drawing.Point(73, 349);
            this.DTextBox.Name = "DTextBox";
            this.DTextBox.Size = new System.Drawing.Size(434, 23);
            this.DTextBox.TabIndex = 18;
            this.DTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DTextBox_KeyDown);
            this.DTextBox.Leave += new System.EventHandler(this.DTextBox_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 352);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "D(Ver): ";
            // 
            // CombineTextBox
            // 
            this.CombineTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CombineTextBox.Location = new System.Drawing.Point(609, 262);
            this.CombineTextBox.Name = "CombineTextBox";
            this.CombineTextBox.Size = new System.Drawing.Size(262, 23);
            this.CombineTextBox.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(551, 265);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 15);
            this.label6.TabIndex = 19;
            this.label6.Text = "조건식: ";
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Location = new System.Drawing.Point(778, 361);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(93, 40);
            this.ExecuteButton.TabIndex = 23;
            this.ExecuteButton.Text = "쿼리 실행하기";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // listView1
            // 
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(12, 419);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(859, 244);
            this.listView1.TabIndex = 24;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // ResultTextBox
            // 
            this.ResultTextBox.Location = new System.Drawing.Point(12, 669);
            this.ResultTextBox.Multiline = true;
            this.ResultTextBox.Name = "ResultTextBox";
            this.ResultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ResultTextBox.Size = new System.Drawing.Size(859, 202);
            this.ResultTextBox.TabIndex = 25;
            // 
            // ChangeButton
            // 
            this.ChangeButton.Location = new System.Drawing.Point(666, 361);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(95, 40);
            this.ChangeButton.TabIndex = 26;
            this.ChangeButton.Text = "항목 채우기";
            this.ChangeButton.UseVisualStyleBackColor = true;
            this.ChangeButton.Click += new System.EventHandler(this.ChangeButton_Click);
            // 
            // TypeCheckBox
            // 
            this.TypeCheckBox.AutoSize = true;
            this.TypeCheckBox.Location = new System.Drawing.Point(782, 59);
            this.TypeCheckBox.Name = "TypeCheckBox";
            this.TypeCheckBox.Size = new System.Drawing.Size(76, 19);
            this.TypeCheckBox.TabIndex = 27;
            this.TypeCheckBox.Text = "TypeData";
            this.TypeCheckBox.UseVisualStyleBackColor = true;
            this.TypeCheckBox.CheckedChanged += new System.EventHandler(this.TypeCheckBox_CheckedChanged);
            // 
            // ETextBox
            // 
            this.ETextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ETextBox.Location = new System.Drawing.Point(73, 378);
            this.ETextBox.Name = "ETextBox";
            this.ETextBox.Size = new System.Drawing.Size(434, 23);
            this.ETextBox.TabIndex = 30;
            this.ETextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ETextBox_KeyDown);
            this.ETextBox.Leave += new System.EventHandler(this.ETextBox_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 381);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 15);
            this.label7.TabIndex = 29;
            this.label7.Text = "E(Type): ";
            // 
            // TypeFlowLayout
            // 
            this.TypeFlowLayout.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TypeFlowLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.TypeFlowLayout.Location = new System.Drawing.Point(771, 84);
            this.TypeFlowLayout.Name = "TypeFlowLayout";
            this.TypeFlowLayout.Size = new System.Drawing.Size(100, 160);
            this.TypeFlowLayout.TabIndex = 31;
            // 
            // ShowLabel
            // 
            this.ShowLabel.AutoSize = true;
            this.ShowLabel.Location = new System.Drawing.Point(524, 309);
            this.ShowLabel.Name = "ShowLabel";
            this.ShowLabel.Size = new System.Drawing.Size(31, 15);
            this.ShowLabel.TabIndex = 35;
            this.ShowLabel.Text = "내용";
            // 
            // QueryFixDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 883);
            this.Controls.Add(this.ShowLabel);
            this.Controls.Add(this.TypeFlowLayout);
            this.Controls.Add(this.ETextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.TypeCheckBox);
            this.Controls.Add(this.ChangeButton);
            this.Controls.Add(this.ResultTextBox);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.ExecuteButton);
            this.Controls.Add(this.CombineTextBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.DTextBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CTextBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.BTextBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ATextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.GuFlowLayoutPanel);
            this.Controls.Add(this.RowCountComboBox);
            this.Controls.Add(this.GuCheckBox);
            this.Controls.Add(this.VerCheckBox);
            this.Controls.Add(this.VerFlowLayoutPanel);
            this.Controls.Add(this.HorCheckBox);
            this.Controls.Add(this.HorFlowLayoutPanel);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "QueryFixDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "데이터 쿼리검사";
            this.Load += new System.EventHandler(this.QueryFixDataForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label1;
        private FlowLayoutPanel HorFlowLayoutPanel;
        private CheckBox HorCheckBox;
        private CheckBox VerCheckBox;
        private FlowLayoutPanel VerFlowLayoutPanel;
        private CheckBox GuCheckBox;
        private ComboBox RowCountComboBox;
        private FlowLayoutPanel GuFlowLayoutPanel;
        private PictureBox pictureBox1;
        private Label label2;
        private TextBox ATextBox;
        private TextBox BTextBox;
        private Label label3;
        private TextBox CTextBox;
        private Label label4;
        private TextBox DTextBox;
        private Label label5;
        private TextBox CombineTextBox;
        private Label label6;
        private Button ExecuteButton;
        private ListView listView1;
        private TextBox ResultTextBox;
        private Button ChangeButton;
        private CheckBox TypeCheckBox;
        private TextBox ETextBox;
        private Label label7;
        private FlowLayoutPanel TypeFlowLayout;
        private Label ShowLabel;
    }
}