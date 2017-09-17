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
            this.LecturesTreeView = new System.Windows.Forms.TreeView();
            this.ExercisesTab = new System.Windows.Forms.TabPage();
            this.LectureRichTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.CppBookTab.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(656, 486);
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
            this.CppBookTab.Size = new System.Drawing.Size(648, 460);
            this.CppBookTab.TabIndex = 0;
            this.CppBookTab.Text = "Учебные материалы";
            this.CppBookTab.UseVisualStyleBackColor = true;
            // 
            // LecturesTreeView
            // 
            this.LecturesTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.LecturesTreeView.Location = new System.Drawing.Point(7, 23);
            this.LecturesTreeView.Name = "LecturesTreeView";
            this.LecturesTreeView.Size = new System.Drawing.Size(305, 431);
            this.LecturesTreeView.TabIndex = 0;
            this.LecturesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.LecturesTreeView_AfterSelect);
            // 
            // ExercisesTab
            // 
            this.ExercisesTab.Location = new System.Drawing.Point(4, 22);
            this.ExercisesTab.Name = "ExercisesTab";
            this.ExercisesTab.Padding = new System.Windows.Forms.Padding(3);
            this.ExercisesTab.Size = new System.Drawing.Size(648, 460);
            this.ExercisesTab.TabIndex = 1;
            this.ExercisesTab.Text = "Задания";
            this.ExercisesTab.UseVisualStyleBackColor = true;
            // 
            // LectureRichTextBox
            // 
            this.LectureRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LectureRichTextBox.Location = new System.Drawing.Point(318, 23);
            this.LectureRichTextBox.Name = "LectureRichTextBox";
            this.LectureRichTextBox.Size = new System.Drawing.Size(324, 431);
            this.LectureRichTextBox.TabIndex = 1;
            this.LectureRichTextBox.Text = "";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(315, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Содержание";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 510);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Учебник C++";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.CppBookTab.ResumeLayout(false);
            this.CppBookTab.PerformLayout();
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
    }
}

