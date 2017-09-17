using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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

        //хранится соответствие между названием файла в левом меню и самим файлом на диске
        private Dictionary<string,FileInfo> lectureFilesDictionary=new Dictionary<string, FileInfo>();

        public Form1()
        {
            InitializeComponent();

            initLecuresTreeView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void initLecuresTreeView()
        {
            //идет вверх по папкам пока не попадет в главную папку программы где есть папка Lectures
            DirectoryInfo AppDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

            while (!AppDirectory.EnumerateDirectories().Any((dir)=> { return dir.Name == "Lectures"; }))
            {
                AppDirectory = AppDirectory.Parent;
            }

            //переход в папку Lectures
            DirectoryInfo LecturesDirectory = new DirectoryInfo(AppDirectory.FullName + @"\Lectures\");
            
            //добавление папок с содержанием каждой лекции в список лекций
            foreach (DirectoryInfo dir in LecturesDirectory.GetDirectories())
            {
                //добавление частей лекций
                FileInfo[] lectureFiles = dir.GetFiles();
                List<TreeNode> childNodes=new List<TreeNode>();
                foreach(FileInfo file in lectureFiles)
                {
                    childNodes.Add(new TreeNode(file.Name));
                    lectureFilesDictionary.Add(file.Name,file);
                }

                LecturesTreeView.Nodes.Add(new TreeNode(dir.Name,childNodes.ToArray()));

            }

        }

        private void LecturesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {            
            Console.WriteLine("selected "+e.Node.Text);
            if (lectureFilesDictionary.ContainsKey(e.Node.Text))
            {
                FileInfo file = lectureFilesDictionary[e.Node.Text];
                Console.WriteLine(file.FullName);

                showFile(file);
            }          
        }


        private void showFile(FileInfo file)
        {
            LectureRichTextBox.Clear();

            object filename = file.FullName;
            Microsoft.Office.Interop.Word.Application AC = new Microsoft.Office.Interop.Word.Application();
            Microsoft.Office.Interop.Word.Document doc = new Microsoft.Office.Interop.Word.Document();

            object readOnly = false;
            object isVisible = true;
            object missing = System.Reflection.Missing.Value;

            try
            {
                doc = AC.Documents.Open(ref filename, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref isVisible);
                doc.Content.Select();
                doc.Content.Copy();
                LectureRichTextBox.Paste();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
            finally
            {
                doc.Close(ref missing, ref missing, ref missing);
            }
        }
    }
}
