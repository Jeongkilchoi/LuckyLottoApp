namespace LuckyLottoApp.Forms
{
    partial class SamOhDataForm
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
            this.components = new System.ComponentModel.Container();
            this.ChangeButton = new System.Windows.Forms.Button();
            this.SaveTextBox = new System.Windows.Forms.TextBox();
            this.BeforeCheckBox = new System.Windows.Forms.CheckBox();
            this.ResultListBox = new System.Windows.Forms.CheckedListBox();
            this.RemoveContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.RemoveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.CancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SectionComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SelectedNumberTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FiveRadioButton = new System.Windows.Forms.RadioButton();
            this.ThreeRadioButton = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.LimitNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.StopButton = new System.Windows.Forms.Button();
            this.WriteButton = new System.Windows.Forms.Button();
            this.ThreeShownButton = new System.Windows.Forms.Button();
            this.NonThreeButton = new System.Windows.Forms.Button();
            this.NonTwoButton = new System.Windows.Forms.Button();
            this.DicListView = new System.Windows.Forms.ListView();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CheckButton = new System.Windows.Forms.Button();
            this.Limit3NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.Limit3CheckBox = new System.Windows.Forms.CheckBox();
            this.Limit2NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.Limit2CheckBox = new System.Windows.Forms.CheckBox();
            this.Limit1NumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.Limit1CheckBox = new System.Windows.Forms.CheckBox();
            this.NoneCheckedButton = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.InitialButton = new System.Windows.Forms.Button();
            this.RemoveContextMenuStrip.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LimitNumericUpDown)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Limit3NumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Limit2NumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Limit1NumericUpDown)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // ChangeButton
            // 
            this.ChangeButton.Enabled = false;
            this.ChangeButton.Location = new System.Drawing.Point(184, 500);
            this.ChangeButton.Name = "ChangeButton";
            this.ChangeButton.Size = new System.Drawing.Size(119, 30);
            this.ChangeButton.TabIndex = 40;
            this.ChangeButton.Text = "변경하기";
            this.ChangeButton.UseVisualStyleBackColor = true;
            this.ChangeButton.Click += new System.EventHandler(this.ChangeButton_Click);
            // 
            // SaveTextBox
            // 
            this.SaveTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SaveTextBox.Location = new System.Drawing.Point(309, 458);
            this.SaveTextBox.Multiline = true;
            this.SaveTextBox.Name = "SaveTextBox";
            this.SaveTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.SaveTextBox.Size = new System.Drawing.Size(195, 292);
            this.SaveTextBox.TabIndex = 39;
            // 
            // BeforeCheckBox
            // 
            this.BeforeCheckBox.AutoSize = true;
            this.BeforeCheckBox.Location = new System.Drawing.Point(161, 698);
            this.BeforeCheckBox.Name = "BeforeCheckBox";
            this.BeforeCheckBox.Size = new System.Drawing.Size(142, 19);
            this.BeforeCheckBox.TabIndex = 38;
            this.BeforeCheckBox.Text = "리스트박스 선택 반전";
            this.BeforeCheckBox.UseVisualStyleBackColor = true;
            this.BeforeCheckBox.CheckedChanged += new System.EventHandler(this.BeforeCheckBox_CheckedChanged);
            // 
            // ResultListBox
            // 
            this.ResultListBox.ContextMenuStrip = this.RemoveContextMenuStrip;
            this.ResultListBox.FormattingEnabled = true;
            this.ResultListBox.Location = new System.Drawing.Point(510, 458);
            this.ResultListBox.Name = "ResultListBox";
            this.ResultListBox.Size = new System.Drawing.Size(201, 292);
            this.ResultListBox.TabIndex = 35;
            this.ResultListBox.SelectedIndexChanged += new System.EventHandler(this.ResultListBox_SelectedIndexChanged);
            // 
            // RemoveContextMenuStrip
            // 
            this.RemoveContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RemoveToolStripMenuItem,
            this.toolStripSeparator1,
            this.CancelToolStripMenuItem});
            this.RemoveContextMenuStrip.Name = "RemoveContextMenuStrip";
            this.RemoveContextMenuStrip.Size = new System.Drawing.Size(151, 54);
            // 
            // RemoveToolStripMenuItem
            // 
            this.RemoveToolStripMenuItem.Name = "RemoveToolStripMenuItem";
            this.RemoveToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.RemoveToolStripMenuItem.Text = "선택한것 삭제";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(147, 6);
            // 
            // CancelToolStripMenuItem
            // 
            this.CancelToolStripMenuItem.Name = "CancelToolStripMenuItem";
            this.CancelToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.CancelToolStripMenuItem.Text = "취소";
            // 
            // SectionComboBox
            // 
            this.SectionComboBox.FormattingEnabled = true;
            this.SectionComboBox.Location = new System.Drawing.Point(84, 503);
            this.SectionComboBox.Name = "SectionComboBox";
            this.SectionComboBox.Size = new System.Drawing.Size(88, 23);
            this.SectionComboBox.TabIndex = 34;
            this.SectionComboBox.SelectionChangeCommitted += new System.EventHandler(this.SectionComboBox_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 506);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 15);
            this.label2.TabIndex = 33;
            this.label2.Text = "검사구간: ";
            // 
            // SelectedNumberTextBox
            // 
            this.SelectedNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SelectedNumberTextBox.Location = new System.Drawing.Point(13, 541);
            this.SelectedNumberTextBox.Multiline = true;
            this.SelectedNumberTextBox.Name = "SelectedNumberTextBox";
            this.SelectedNumberTextBox.Size = new System.Drawing.Size(290, 40);
            this.SelectedNumberTextBox.TabIndex = 32;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.FiveRadioButton);
            this.panel1.Controls.Add(this.ThreeRadioButton);
            this.panel1.Location = new System.Drawing.Point(13, 448);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 41);
            this.panel1.TabIndex = 31;
            // 
            // FiveRadioButton
            // 
            this.FiveRadioButton.AutoSize = true;
            this.FiveRadioButton.Location = new System.Drawing.Point(170, 10);
            this.FiveRadioButton.Name = "FiveRadioButton";
            this.FiveRadioButton.Size = new System.Drawing.Size(112, 19);
            this.FiveRadioButton.TabIndex = 1;
            this.FiveRadioButton.Text = "무중복 5 데이터";
            this.FiveRadioButton.UseVisualStyleBackColor = true;
            // 
            // ThreeRadioButton
            // 
            this.ThreeRadioButton.AutoSize = true;
            this.ThreeRadioButton.Checked = true;
            this.ThreeRadioButton.Location = new System.Drawing.Point(27, 11);
            this.ThreeRadioButton.Name = "ThreeRadioButton";
            this.ThreeRadioButton.Size = new System.Drawing.Size(112, 19);
            this.ThreeRadioButton.TabIndex = 0;
            this.ThreeRadioButton.TabStop = true;
            this.ThreeRadioButton.Text = "무중복 3 데이터";
            this.ThreeRadioButton.UseVisualStyleBackColor = true;
            this.ThreeRadioButton.CheckedChanged += new System.EventHandler(this.ThreeRadioButton_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(803, 546);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 30;
            this.label1.Text = "한계값:";
            // 
            // LimitNumericUpDown
            // 
            this.LimitNumericUpDown.Location = new System.Drawing.Point(803, 566);
            this.LimitNumericUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.LimitNumericUpDown.Name = "LimitNumericUpDown";
            this.LimitNumericUpDown.Size = new System.Drawing.Size(69, 23);
            this.LimitNumericUpDown.TabIndex = 29;
            this.LimitNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LimitNumericUpDown.ValueChanged += new System.EventHandler(this.LimitNumericUpDown_ValueChanged);
            // 
            // StopButton
            // 
            this.StopButton.Enabled = false;
            this.StopButton.Location = new System.Drawing.Point(717, 548);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(69, 41);
            this.StopButton.TabIndex = 28;
            this.StopButton.Text = "작업중지";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // WriteButton
            // 
            this.WriteButton.Enabled = false;
            this.WriteButton.Location = new System.Drawing.Point(803, 605);
            this.WriteButton.Name = "WriteButton";
            this.WriteButton.Size = new System.Drawing.Size(69, 41);
            this.WriteButton.TabIndex = 27;
            this.WriteButton.Text = "파일작성";
            this.WriteButton.UseVisualStyleBackColor = true;
            this.WriteButton.Click += new System.EventHandler(this.WriteButton_Click);
            // 
            // ThreeShownButton
            // 
            this.ThreeShownButton.Location = new System.Drawing.Point(717, 605);
            this.ThreeShownButton.Name = "ThreeShownButton";
            this.ThreeShownButton.Size = new System.Drawing.Size(69, 41);
            this.ThreeShownButton.TabIndex = 26;
            this.ThreeShownButton.Text = "3조합 1출";
            this.ThreeShownButton.UseVisualStyleBackColor = true;
            this.ThreeShownButton.Click += new System.EventHandler(this.ThreeShownButton_Click);
            // 
            // NonThreeButton
            // 
            this.NonThreeButton.Location = new System.Drawing.Point(717, 656);
            this.NonThreeButton.Name = "NonThreeButton";
            this.NonThreeButton.Size = new System.Drawing.Size(155, 41);
            this.NonThreeButton.TabIndex = 25;
            this.NonThreeButton.Text = "3조합 무출 검사";
            this.NonThreeButton.UseVisualStyleBackColor = true;
            this.NonThreeButton.Click += new System.EventHandler(this.NonThreeButton_Click);
            // 
            // NonTwoButton
            // 
            this.NonTwoButton.Location = new System.Drawing.Point(717, 708);
            this.NonTwoButton.Name = "NonTwoButton";
            this.NonTwoButton.Size = new System.Drawing.Size(155, 41);
            this.NonTwoButton.TabIndex = 24;
            this.NonTwoButton.Text = "2조합 무출 검사";
            this.NonTwoButton.UseVisualStyleBackColor = true;
            this.NonTwoButton.Click += new System.EventHandler(this.NonTwoButton_Click);
            // 
            // DicListView
            // 
            this.DicListView.GridLines = true;
            this.DicListView.Location = new System.Drawing.Point(13, 17);
            this.DicListView.MultiSelect = false;
            this.DicListView.Name = "DicListView";
            this.DicListView.Size = new System.Drawing.Size(859, 419);
            this.DicListView.TabIndex = 23;
            this.DicListView.UseCompatibleStateImageBehavior = false;
            this.DicListView.View = System.Windows.Forms.View.Details;
            this.DicListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.DicListView_ColumnClick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 726);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(290, 23);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 41;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.CheckButton);
            this.panel2.Controls.Add(this.Limit3NumericUpDown);
            this.panel2.Controls.Add(this.Limit3CheckBox);
            this.panel2.Controls.Add(this.Limit2NumericUpDown);
            this.panel2.Controls.Add(this.Limit2CheckBox);
            this.panel2.Controls.Add(this.Limit1NumericUpDown);
            this.panel2.Controls.Add(this.Limit1CheckBox);
            this.panel2.Enabled = false;
            this.panel2.Location = new System.Drawing.Point(12, 590);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(291, 100);
            this.panel2.TabIndex = 42;
            // 
            // CheckButton
            // 
            this.CheckButton.Location = new System.Drawing.Point(194, 8);
            this.CheckButton.Name = "CheckButton";
            this.CheckButton.Size = new System.Drawing.Size(84, 82);
            this.CheckButton.TabIndex = 7;
            this.CheckButton.Text = "검사하기";
            this.CheckButton.UseVisualStyleBackColor = true;
            this.CheckButton.Click += new System.EventHandler(this.CheckButton_Click);
            // 
            // Limit3NumericUpDown
            // 
            this.Limit3NumericUpDown.Location = new System.Drawing.Point(126, 67);
            this.Limit3NumericUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Limit3NumericUpDown.Name = "Limit3NumericUpDown";
            this.Limit3NumericUpDown.Size = new System.Drawing.Size(51, 23);
            this.Limit3NumericUpDown.TabIndex = 6;
            this.Limit3NumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Limit3NumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Limit3CheckBox
            // 
            this.Limit3CheckBox.AutoSize = true;
            this.Limit3CheckBox.Location = new System.Drawing.Point(16, 71);
            this.Limit3CheckBox.Name = "Limit3CheckBox";
            this.Limit3CheckBox.Size = new System.Drawing.Size(101, 19);
            this.Limit3CheckBox.TabIndex = 5;
            this.Limit3CheckBox.Tag = "3";
            this.Limit3CheckBox.Text = "한계값 3전 값";
            this.Limit3CheckBox.UseVisualStyleBackColor = true;
            // 
            // Limit2NumericUpDown
            // 
            this.Limit2NumericUpDown.Location = new System.Drawing.Point(126, 37);
            this.Limit2NumericUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Limit2NumericUpDown.Name = "Limit2NumericUpDown";
            this.Limit2NumericUpDown.Size = new System.Drawing.Size(51, 23);
            this.Limit2NumericUpDown.TabIndex = 4;
            this.Limit2NumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Limit2NumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Limit2CheckBox
            // 
            this.Limit2CheckBox.AutoSize = true;
            this.Limit2CheckBox.Location = new System.Drawing.Point(16, 41);
            this.Limit2CheckBox.Name = "Limit2CheckBox";
            this.Limit2CheckBox.Size = new System.Drawing.Size(101, 19);
            this.Limit2CheckBox.TabIndex = 3;
            this.Limit2CheckBox.Tag = "2";
            this.Limit2CheckBox.Text = "한계값 2전 값";
            this.Limit2CheckBox.UseVisualStyleBackColor = true;
            // 
            // Limit1NumericUpDown
            // 
            this.Limit1NumericUpDown.Location = new System.Drawing.Point(126, 8);
            this.Limit1NumericUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.Limit1NumericUpDown.Name = "Limit1NumericUpDown";
            this.Limit1NumericUpDown.Size = new System.Drawing.Size(51, 23);
            this.Limit1NumericUpDown.TabIndex = 2;
            this.Limit1NumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Limit1NumericUpDown.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // Limit1CheckBox
            // 
            this.Limit1CheckBox.AutoSize = true;
            this.Limit1CheckBox.Location = new System.Drawing.Point(16, 12);
            this.Limit1CheckBox.Name = "Limit1CheckBox";
            this.Limit1CheckBox.Size = new System.Drawing.Size(101, 19);
            this.Limit1CheckBox.TabIndex = 1;
            this.Limit1CheckBox.Tag = "1";
            this.Limit1CheckBox.Text = "한계값 1전 값";
            this.Limit1CheckBox.UseVisualStyleBackColor = true;
            // 
            // NoneCheckedButton
            // 
            this.NoneCheckedButton.Location = new System.Drawing.Point(17, 42);
            this.NoneCheckedButton.Name = "NoneCheckedButton";
            this.NoneCheckedButton.Size = new System.Drawing.Size(121, 30);
            this.NoneCheckedButton.TabIndex = 43;
            this.NoneCheckedButton.Text = "전체 선택해제";
            this.NoneCheckedButton.UseVisualStyleBackColor = true;
            this.NoneCheckedButton.Click += new System.EventHandler(this.NoneCheckedButton_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.InitialButton);
            this.panel3.Controls.Add(this.NoneCheckedButton);
            this.panel3.Enabled = false;
            this.panel3.Location = new System.Drawing.Point(717, 459);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(155, 78);
            this.panel3.TabIndex = 44;
            // 
            // InitialButton
            // 
            this.InitialButton.Location = new System.Drawing.Point(17, 6);
            this.InitialButton.Name = "InitialButton";
            this.InitialButton.Size = new System.Drawing.Size(121, 30);
            this.InitialButton.TabIndex = 44;
            this.InitialButton.Text = "초기로 되돌리기";
            this.InitialButton.UseVisualStyleBackColor = true;
            this.InitialButton.Click += new System.EventHandler(this.InitialButton_Click);
            // 
            // SamOhDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 761);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.ChangeButton);
            this.Controls.Add(this.SaveTextBox);
            this.Controls.Add(this.BeforeCheckBox);
            this.Controls.Add(this.ResultListBox);
            this.Controls.Add(this.SectionComboBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SelectedNumberTextBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LimitNumericUpDown);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.WriteButton);
            this.Controls.Add(this.ThreeShownButton);
            this.Controls.Add(this.NonThreeButton);
            this.Controls.Add(this.NonTwoButton);
            this.Controls.Add(this.DicListView);
            this.MaximizeBox = false;
            this.Name = "SamOhDataForm";
            this.Text = "무중복 3열, 5열 데이터 검사";
            this.Load += new System.EventHandler(this.SamOhDataForm_Load);
            this.RemoveContextMenuStrip.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LimitNumericUpDown)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Limit3NumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Limit2NumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Limit1NumericUpDown)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button ChangeButton;
        private TextBox SaveTextBox;
        private CheckBox BeforeCheckBox;
        private CheckedListBox ResultListBox;
        private ComboBox SectionComboBox;
        private Label label2;
        private TextBox SelectedNumberTextBox;
        private Panel panel1;
        private RadioButton FiveRadioButton;
        private RadioButton ThreeRadioButton;
        private Label label1;
        private NumericUpDown LimitNumericUpDown;
        private Button StopButton;
        private Button WriteButton;
        private Button ThreeShownButton;
        private Button NonThreeButton;
        private Button NonTwoButton;
        private ListView DicListView;
        private ContextMenuStrip RemoveContextMenuStrip;
        private ToolStripMenuItem RemoveToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem CancelToolStripMenuItem;
        private ProgressBar progressBar1;
        private Panel panel2;
        private NumericUpDown Limit2NumericUpDown;
        private CheckBox Limit2CheckBox;
        private NumericUpDown Limit1NumericUpDown;
        private CheckBox Limit1CheckBox;
        private NumericUpDown Limit3NumericUpDown;
        private CheckBox Limit3CheckBox;
        private Button NoneCheckedButton;
        private Panel panel3;
        private Button InitialButton;
        private Button CheckButton;
    }
}