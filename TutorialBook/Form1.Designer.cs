namespace TutorialBook
{
    partial class Form1
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.CppBookTab = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.LectureRichTextBox = new System.Windows.Forms.RichTextBox();
            this.LecturesTreeView = new System.Windows.Forms.TreeView();
            this.ExercisesTab = new System.Windows.Forms.TabPage();
            this.OpenFileButton = new System.Windows.Forms.Button();
            this.SaveUserCodeButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.UserCodeOutputTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ExerciseTextTextBox = new System.Windows.Forms.TextBox();
            this.ExercisesTreeView = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.RunCodeButton = new System.Windows.Forms.Button();
            this.UserCodeTextBox = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.CppBookTab.SuspendLayout();
            this.ExercisesTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.CppBookTab);
            this.tabControl1.Controls.Add(this.ExercisesTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(986, 516);
            this.tabControl1.TabIndex = 0;
            // 
            // CppBookTab
            // 
            this.CppBookTab.Controls.Add(this.label2);
            this.CppBookTab.Controls.Add(this.label1);
            this.CppBookTab.Controls.Add(this.LectureRichTextBox);
            this.CppBookTab.Controls.Add(this.LecturesTreeView);
            this.CppBookTab.Location = new System.Drawing.Point(4, 22);
            this.CppBookTab.Name = "CppBookTab";
            this.CppBookTab.Padding = new System.Windows.Forms.Padding(3);
            this.CppBookTab.Size = new System.Drawing.Size(978, 490);
            this.CppBookTab.TabIndex = 0;
            this.CppBookTab.Text = "Учебные материалы";
            this.CppBookTab.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(315, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Содержание";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Список лекций";
            // 
            // LectureRichTextBox
            // 
            this.LectureRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LectureRichTextBox.Location = new System.Drawing.Point(318, 23);
            this.LectureRichTextBox.Name = "LectureRichTextBox";
            this.LectureRichTextBox.ReadOnly = true;
            this.LectureRichTextBox.Size = new System.Drawing.Size(654, 461);
            this.LectureRichTextBox.TabIndex = 1;
            this.LectureRichTextBox.Text = "";
            // 
            // LecturesTreeView
            // 
            this.LecturesTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LecturesTreeView.Location = new System.Drawing.Point(7, 23);
            this.LecturesTreeView.Name = "LecturesTreeView";
            this.LecturesTreeView.Size = new System.Drawing.Size(305, 461);
            this.LecturesTreeView.TabIndex = 0;
            this.LecturesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.LecturesTreeView_AfterSelect);
            // 
            // ExercisesTab
            // 
            this.ExercisesTab.Controls.Add(this.OpenFileButton);
            this.ExercisesTab.Controls.Add(this.SaveUserCodeButton);
            this.ExercisesTab.Controls.Add(this.label6);
            this.ExercisesTab.Controls.Add(this.UserCodeOutputTextBox);
            this.ExercisesTab.Controls.Add(this.label5);
            this.ExercisesTab.Controls.Add(this.label4);
            this.ExercisesTab.Controls.Add(this.ExerciseTextTextBox);
            this.ExercisesTab.Controls.Add(this.ExercisesTreeView);
            this.ExercisesTab.Controls.Add(this.label3);
            this.ExercisesTab.Controls.Add(this.RunCodeButton);
            this.ExercisesTab.Controls.Add(this.UserCodeTextBox);
            this.ExercisesTab.Location = new System.Drawing.Point(4, 22);
            this.ExercisesTab.Name = "ExercisesTab";
            this.ExercisesTab.Padding = new System.Windows.Forms.Padding(3);
            this.ExercisesTab.Size = new System.Drawing.Size(978, 490);
            this.ExercisesTab.TabIndex = 1;
            this.ExercisesTab.Text = "Задачи";
            this.ExercisesTab.UseVisualStyleBackColor = true;
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.Location = new System.Drawing.Point(248, 461);
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(75, 23);
            this.OpenFileButton.TabIndex = 11;
            this.OpenFileButton.Text = "Открыть";
            this.OpenFileButton.UseVisualStyleBackColor = true;
            this.OpenFileButton.Click += new System.EventHandler(this.OpenFileButton_Click);
            // 
            // SaveUserCodeButton
            // 
            this.SaveUserCodeButton.Location = new System.Drawing.Point(370, 461);
            this.SaveUserCodeButton.Name = "SaveUserCodeButton";
            this.SaveUserCodeButton.Size = new System.Drawing.Size(90, 23);
            this.SaveUserCodeButton.TabIndex = 10;
            this.SaveUserCodeButton.Text = "Сохранить";
            this.SaveUserCodeButton.UseVisualStyleBackColor = true;
            this.SaveUserCodeButton.Click += new System.EventHandler(this.SaveUserCodeButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(583, 153);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Вывод";
            // 
            // UserCodeOutputTextBox
            // 
            this.UserCodeOutputTextBox.Location = new System.Drawing.Point(583, 169);
            this.UserCodeOutputTextBox.Multiline = true;
            this.UserCodeOutputTextBox.Name = "UserCodeOutputTextBox";
            this.UserCodeOutputTextBox.Size = new System.Drawing.Size(389, 104);
            this.UserCodeOutputTextBox.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(252, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Ваш код";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(249, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Текст задачи";
            // 
            // ExerciseTextTextBox
            // 
            this.ExerciseTextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ExerciseTextTextBox.Location = new System.Drawing.Point(249, 24);
            this.ExerciseTextTextBox.Multiline = true;
            this.ExerciseTextTextBox.Name = "ExerciseTextTextBox";
            this.ExerciseTextTextBox.ReadOnly = true;
            this.ExerciseTextTextBox.Size = new System.Drawing.Size(723, 122);
            this.ExerciseTextTextBox.TabIndex = 5;
            // 
            // ExercisesTreeView
            // 
            this.ExercisesTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ExercisesTreeView.Location = new System.Drawing.Point(7, 24);
            this.ExercisesTreeView.Name = "ExercisesTreeView";
            this.ExercisesTreeView.Size = new System.Drawing.Size(235, 460);
            this.ExercisesTreeView.TabIndex = 4;
            this.ExercisesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.ExercisesTreeView_AfterSelect);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Задачи";
            // 
            // RunCodeButton
            // 
            this.RunCodeButton.Location = new System.Drawing.Point(486, 462);
            this.RunCodeButton.Name = "RunCodeButton";
            this.RunCodeButton.Size = new System.Drawing.Size(90, 23);
            this.RunCodeButton.TabIndex = 2;
            this.RunCodeButton.Text = "Запуск";
            this.RunCodeButton.UseVisualStyleBackColor = true;
            this.RunCodeButton.Click += new System.EventHandler(this.RunCodeButton_Click);
            // 
            // UserCodeTextBox
            // 
            this.UserCodeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UserCodeTextBox.Location = new System.Drawing.Point(249, 169);
            this.UserCodeTextBox.Multiline = true;
            this.UserCodeTextBox.Name = "UserCodeTextBox";
            this.UserCodeTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.UserCodeTextBox.Size = new System.Drawing.Size(327, 287);
            this.UserCodeTextBox.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1010, 540);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Учебник C++";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.CppBookTab.ResumeLayout(false);
            this.CppBookTab.PerformLayout();
            this.ExercisesTab.ResumeLayout(false);
            this.ExercisesTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage CppBookTab;
        private System.Windows.Forms.TabPage ExercisesTab;
        private System.Windows.Forms.TreeView LecturesTreeView;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox LectureRichTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button RunCodeButton;
        private System.Windows.Forms.TextBox UserCodeTextBox;
        private System.Windows.Forms.TreeView ExercisesTreeView;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ExerciseTextTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox UserCodeOutputTextBox;
        private System.Windows.Forms.Button SaveUserCodeButton;
        private System.Windows.Forms.Button OpenFileButton;
    }
}

