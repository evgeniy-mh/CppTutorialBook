using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TutorialBook
{
    public partial class Form1 : Form
    {        
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
            TreeNode[] lectures = {
                new TreeNode("Лекция 1. Объектно-ориентированное программирование",new TreeNode[] {
                    new TreeNode("1.1.	Введение"),
                    new TreeNode("1.2.	Понятие класса"),
                    new TreeNode("1.3.	Принципы и свойства объектно-ориентированного программирования")
                }),

                new TreeNode("Лекция 2. Общие сведения о классах")
            };

            //lectures[0].Nodes.AddRange( {new TreeNode("sa") });

            LecturesTreeView.Nodes.AddRange(lectures);

        }
    }
}
