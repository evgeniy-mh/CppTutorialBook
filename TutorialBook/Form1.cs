﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TutorialBook
{
    public partial class Form1 : Form
    {

        DirectoryInfo AppDirectory;
        DirectoryInfo LecturesDirectory;
        DirectoryInfo ExercisesDirectory;
        DirectoryInfo currentExerciseDirectory;

        FileInfo currentExerciseCPPFile;
        FileInfo currentExerciseTestFile;

        //хранится соответствие между названием файла лекции в левом меню и самим файлом на диске
        Dictionary<string, FileInfo> lectureFilesDictionary = new Dictionary<string, FileInfo>();
        Dictionary<string, DirectoryInfo> exerciseDirectoriesDictionary = new Dictionary<string, DirectoryInfo>();

        Microsoft.Office.Interop.Word.Application WordApplication;
        Microsoft.Office.Interop.Word.Document WordDocument;

        public Form1()
        {
            InitializeComponent();
            findAppDirectory();
            initLecuresTreeView();
            initExercisesListView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WordApplication = new Microsoft.Office.Interop.Word.Application();
            WordDocument = new Microsoft.Office.Interop.Word.Document();
        }

        private void findAppDirectory()
        {
            //идет вверх по папкам пока не попадет в главную папку программы где есть папка Lectures
            AppDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (!AppDirectory.EnumerateDirectories().Any((dir) => { return dir.Name == AppConstants.LECTURES_DIR_NAME; }))
            {
                AppDirectory = AppDirectory.Parent;
            }
        }

        private void initLecuresTreeView()
        {
            //переход в папку Lectures
            LecturesDirectory = new DirectoryInfo(String.Format(@"{0} \{1}\",AppDirectory.FullName,AppConstants.LECTURES_DIR_NAME));

            //добавление папок с содержанием каждой лекции в список лекций
            foreach (DirectoryInfo dir in LecturesDirectory.GetDirectories())
            {
                //добавление частей лекций
                FileInfo[] lectureFiles = dir.GetFiles();
                List<TreeNode> childNodes = new List<TreeNode>();
                foreach (FileInfo file in lectureFiles)
                {
                    childNodes.Add(new TreeNode(file.Name));
                    lectureFilesDictionary.Add(file.Name, file);
                }
                LecturesTreeView.Nodes.Add(new TreeNode(dir.Name, childNodes.ToArray()));
            }
        }

        private void initExercisesListView()
        {
            ExercisesDirectory = new DirectoryInfo(String.Format(@"{0} \{1}\", AppDirectory.FullName, AppConstants.EXERCISES_DIR_NAME));
            foreach (DirectoryInfo dir in ExercisesDirectory.GetDirectories())
            {
                ExercisesTreeView.Nodes.Add(new TreeNode(dir.Name));
                exerciseDirectoriesDictionary.Add(dir.Name, dir);
            }
        }

        private void LecturesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (lectureFilesDictionary.ContainsKey(e.Node.Text))
            {
                FileInfo file = lectureFilesDictionary[e.Node.Text];
                loadWordFile(file);
            }
        }

        private void ExercisesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (exerciseDirectoriesDictionary.ContainsKey(e.Node.Text))
            {
                DirectoryInfo exerciseDirectory = exerciseDirectoriesDictionary[e.Node.Text];
                currentExerciseDirectory = exerciseDirectory;

                FileInfo[] exerciseFiles = exerciseDirectory.GetFiles();

                //TODO: refactor
                FileInfo exerciseTextFile = exerciseFiles.FirstOrDefault(file => { return file.Name == AppConstants.DEFAULT_EXERCISE_TEXT_FILE_NAME; });
                if (exerciseTextFile != null)
                {
                    ExerciseTextTextBox.Text = System.IO.File.ReadAllText(exerciseTextFile.FullName, Encoding.Default);
                }
                else
                {
                    ExerciseTextTextBox.Clear();
                }

                FileInfo exerciseTemplateFile = exerciseFiles.FirstOrDefault(file => { return file.Name == AppConstants.DEFAULT_EXERCISE_TEMPLATE_FILE_NAME; });
                if (exerciseTemplateFile != null)
                {
                    UserCodeTextBox.Text = File.ReadAllText(exerciseTemplateFile.FullName, Encoding.Default);
                }
                else
                {
                    UserCodeTextBox.Clear();
                }

                currentExerciseTestFile = exerciseFiles.FirstOrDefault(file=> { return file.Name == AppConstants.DEFAULT_EXERCISE_TEST_FILE_NAME; });
                if (currentExerciseTestFile != null)
                {

                }

                /*foreach(FileInfo file in exerciseFiles)
                {
                    switch (file.Name)
                    {
                        case AppConstants.DEFAULT_EXERCISE_TEXT_FILE_NAME:
                            break;

                        case AppConstants.DEFAULT_EXERCISE_TEMPLATE_FILE_NAME:
                            break;
                    }
                }*/
            }
        }

        //TODO: REFACTOR!
        private void loadWordFile(FileInfo wordFile)
        {
            object filename = wordFile.FullName;
            object readOnly = false;
            object isVisible = true;
            object missing = System.Reflection.Missing.Value;

            try
            {
                LectureRichTextBox.ReadOnly = false;
                LectureRichTextBox.Clear();
                LectureRichTextBox.Text = "...loading file";

                WordDocument = WordApplication.Documents.Open(ref filename, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible);
                WordDocument.Content.Select();
                WordDocument.Content.Copy();

                LectureRichTextBox.Clear();
                LectureRichTextBox.Paste();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
            finally
            {
                WordDocument.Close(ref missing, ref missing, ref missing);
                LectureRichTextBox.ReadOnly = true;
            }
        }

        private void RunCodeButton_Click(object sender, EventArgs e)
        {
            if (currentExerciseDirectory != null && currentExerciseCPPFile != null)
            {
                Process process = new Process();
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    WorkingDirectory = currentExerciseDirectory.FullName,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                };

                process.Start();

                using (StreamWriter pWriter = process.StandardInput)
                {
                    if (pWriter.BaseStream.CanWrite)
                    {
                        pWriter.WriteLine(@"path=C:\MinGW\bin");
                        pWriter.WriteLine(String.Format(@"mingw32-g++.exe -Wall -fexceptions -g -c {0} -o ex.o",currentExerciseCPPFile));
                        pWriter.WriteLine(String.Format(@"mingw32-g++.exe  -o {0} ex.o",AppConstants.DEFAULT_COMPILED_FILE_NAME));
                        //pWriter.WriteLine(AppConstants.DEFAULT_COMPILED_FILE_NAME);                        
                    }
                }

                /*while (!process.StandardOutput.EndOfStream)
                {
                    string line = process.StandardOutput.ReadLine();
                    Console.WriteLine(line);
                }*/
                UserCodeOutputTextBox.Text = process.StandardOutput.ReadToEnd();

                while (!process.StandardError.EndOfStream)
                {
                    string line = process.StandardError.ReadLine();
                }

                process.WaitForExit();

                /*if (currentExerciseTestFile != null)
                {
                    RunExerciseTest(currentExerciseTestFile);
                }*/
            }
        }

        void RunExerciseTest(FileInfo testFile)
        {
            string[] testString = File.ReadAllLines(testFile.FullName, Encoding.Default);

            Console.WriteLine( currentExerciseDirectory.FullName + @"\" + AppConstants.DEFAULT_COMPILED_FILE_NAME);

            Process process = new Process();
            process.StartInfo = new ProcessStartInfo
            {
                FileName = currentExerciseDirectory.FullName+@"\"+ AppConstants.DEFAULT_COMPILED_FILE_NAME,
                WorkingDirectory = currentExerciseDirectory.FullName,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            };

            process.Start();

            //UserCodeOutputTextBox.Text = UserCodeOutputTextBox.Text+ process.StandardOutput.ReadToEnd();

            process.WaitForExit();

            /*if (currentExerciseTestFile != null)
                        {
                            foreach (string s in testString)
                            {
                                if (s[0] == '=')
                                {

                                }
                                else
                                {
                                    pWriter.WriteLine(s);
                                }
                            }
                        }*/
        }

        private void SaveUserCodeButton_Click(object sender, EventArgs e)
        {
            if (currentExerciseDirectory != null)
            {
                String filePath;
                if (currentExerciseCPPFile == null)
                {
                    filePath = String.Format(@"{0}\{1}", currentExerciseDirectory.FullName, AppConstants.DEFAULT_EXERCISE_FILE_NAME);
                }
                else
                {
                    filePath = currentExerciseCPPFile.FullName;
                }
                File.WriteAllLines(filePath, UserCodeTextBox.Lines, Encoding.Default);
            }
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            if (currentExerciseDirectory != null)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = currentExerciseDirectory.FullName;
                openFileDialog.Filter = "Cpp Files|*.cpp";
                openFileDialog.Title = "Выберите файл с вашим кодом";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    currentExerciseCPPFile =new FileInfo(openFileDialog.FileName);
                    UserCodeTextBox.Text = File.ReadAllText(currentExerciseCPPFile.FullName);
                }
            }
        }
    }
}
