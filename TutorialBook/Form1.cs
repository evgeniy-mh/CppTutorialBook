using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TutorialBook
{
    public partial class Form1 : Form
    {

        public DirectoryInfo AppDirectory;
        DirectoryInfo LecturesDirectory;
        DirectoryInfo ExercisesDirectory;
        DirectoryInfo currentExerciseDirectory;

        //хранится соответствие между названием файла лекции в левом меню и самим файлом на диске
        private Dictionary<string, FileInfo> lectureFilesDictionary = new Dictionary<string, FileInfo>();
        private Dictionary<string, DirectoryInfo> exerciseDirectoriesDictionary = new Dictionary<string, DirectoryInfo>();

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

            while (!AppDirectory.EnumerateDirectories().Any((dir) => { return dir.Name == "Lectures"; }))
            {
                AppDirectory = AppDirectory.Parent;
            }
        }

        private void initLecuresTreeView()
        {
            //переход в папку Lectures
            LecturesDirectory = new DirectoryInfo(AppDirectory.FullName + @"\Lectures\");

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
            ExercisesDirectory = new DirectoryInfo(AppDirectory.FullName + @"\Exercises\");
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

                FileInfo exerciseTextFile = exerciseFiles.FirstOrDefault(file => { return file.Name == "text.txt"; });
                if (exerciseTextFile != null)
                {
                    ExerciseTextTextBox.Text = System.IO.File.ReadAllText(exerciseTextFile.FullName, Encoding.Default);
                }
                else
                {
                    ExerciseTextTextBox.Clear();
                }

                FileInfo exerciseTemplateFile = exerciseFiles.FirstOrDefault(file => { return file.Name == "template.cpp"; });
                if (exerciseTemplateFile != null)
                {
                    UserCodeTextBox.Text = System.IO.File.ReadAllText(exerciseTemplateFile.FullName, Encoding.Default);
                }
                else
                {
                    UserCodeTextBox.Clear();
                }
            }
        }

        //TODO: REFACTOR!
        private void loadWordFile(FileInfo wordFile)
        {
            object filename = wordFile.FullName;
            /*WordApplication = new Microsoft.Office.Interop.Word.Application();
            document = new Microsoft.Office.Interop.Word.Document();*/

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
            if (currentExerciseDirectory != null)
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
                        pWriter.WriteLine(@"path=%path%;C:\MinGW\bin");
                        pWriter.WriteLine(@"mingw32-g++.exe -Wall -fexceptions -g -c template.cpp -o template.o");
                        pWriter.WriteLine(@"mingw32-g++.exe  -o template.exe template.o");
                        pWriter.WriteLine(@"template.exe");
                    }
                }

                while (!process.StandardOutput.EndOfStream)
                {
                    string line = process.StandardOutput.ReadLine();
                    Console.WriteLine(line);
                }

                while (!process.StandardError.EndOfStream)
                {
                    string line = process.StandardError.ReadLine();
                    Console.WriteLine(line);
                }

                process.WaitForExit();
            }
        }
    }
}
