namespace test
{
    partial class ExamForm
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
            this.TextQuestionPanel = new System.Windows.Forms.Panel();
            this.TrueFalsePanel = new System.Windows.Forms.Panel();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.TextQuestionAnswer = new System.Windows.Forms.RichTextBox();
            this.McqPanel = new System.Windows.Forms.Panel();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.QuestionLabel = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.previous_button = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.TextQuestionPanel.SuspendLayout();
            this.TrueFalsePanel.SuspendLayout();
            this.McqPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // TextQuestionPanel
            // 
            this.TextQuestionPanel.Controls.Add(this.TrueFalsePanel);
            this.TextQuestionPanel.Controls.Add(this.TextQuestionAnswer);
            this.TextQuestionPanel.Location = new System.Drawing.Point(12, 155);
            this.TextQuestionPanel.Name = "TextQuestionPanel";
            this.TextQuestionPanel.Size = new System.Drawing.Size(776, 120);
            this.TextQuestionPanel.TabIndex = 0;
            // 
            // TrueFalsePanel
            // 
            this.TrueFalsePanel.Controls.Add(this.radioButton2);
            this.TrueFalsePanel.Controls.Add(this.radioButton1);
            this.TrueFalsePanel.Location = new System.Drawing.Point(0, 0);
            this.TrueFalsePanel.Name = "TrueFalsePanel";
            this.TrueFalsePanel.Size = new System.Drawing.Size(776, 120);
            this.TrueFalsePanel.TabIndex = 3;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(566, 49);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(50, 17);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "False";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(146, 49);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(47, 17);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "True";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // TextQuestionAnswer
            // 
            this.TextQuestionAnswer.Location = new System.Drawing.Point(0, 0);
            this.TextQuestionAnswer.Name = "TextQuestionAnswer";
            this.TextQuestionAnswer.Size = new System.Drawing.Size(776, 117);
            this.TextQuestionAnswer.TabIndex = 1;
            this.TextQuestionAnswer.Text = "";
            // 
            // McqPanel
            // 
            this.McqPanel.BackColor = System.Drawing.SystemColors.Control;
            this.McqPanel.Controls.Add(this.checkedListBox1);
            this.McqPanel.Location = new System.Drawing.Point(12, 155);
            this.McqPanel.Name = "McqPanel";
            this.McqPanel.Size = new System.Drawing.Size(776, 120);
            this.McqPanel.TabIndex = 4;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(199, 11);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(395, 109);
            this.checkedListBox1.TabIndex = 0;
            // 
            // QuestionLabel
            // 
            this.QuestionLabel.AutoSize = true;
            this.QuestionLabel.Location = new System.Drawing.Point(39, 20);
            this.QuestionLabel.Name = "QuestionLabel";
            this.QuestionLabel.Size = new System.Drawing.Size(35, 13);
            this.QuestionLabel.TabIndex = 0;
            this.QuestionLabel.Text = "label1";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(578, 308);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 31);
            this.button1.TabIndex = 1;
            this.button1.Text = "Next";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // previous_button
            // 
            this.previous_button.Location = new System.Drawing.Point(158, 308);
            this.previous_button.Name = "previous_button";
            this.previous_button.Size = new System.Drawing.Size(90, 31);
            this.previous_button.TabIndex = 2;
            this.previous_button.Text = "Previous";
            this.previous_button.UseVisualStyleBackColor = true;
            this.previous_button.Click += new System.EventHandler(this.previous_button_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(578, 308);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(84, 31);
            this.button3.TabIndex = 5;
            this.button3.Text = "Submit";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Submit);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(677, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "label1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ExamForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 363);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.McqPanel);
            this.Controls.Add(this.previous_button);
            this.Controls.Add(this.QuestionLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.TextQuestionPanel);
            this.Name = "ExamForm";
            this.Text = "ExamForm";
            this.Load += new System.EventHandler(this.ExamForm_Load);
            this.TextQuestionPanel.ResumeLayout(false);
            this.TrueFalsePanel.ResumeLayout(false);
            this.TrueFalsePanel.PerformLayout();
            this.McqPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel TextQuestionPanel;
        private System.Windows.Forms.RichTextBox TextQuestionAnswer;
        private System.Windows.Forms.Label QuestionLabel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button previous_button;
        private System.Windows.Forms.Panel TrueFalsePanel;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Panel McqPanel;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
    }
}