using System;
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

        DirectoryInfo AppDirectory; //главная директория к которой находится приложение
        DirectoryInfo LecturesDirectory; //директория с лекциями
        DirectoryInfo ExercisesDirectory; //директория с упражнениями 
        DirectoryInfo currentExerciseDirectory; //директория текущего упражнения

        FileInfo currentExerciseCPPFile; //текущий cpp файл с которым работает пользователь 
        FileInfo currentExerciseTestFile; //файл содержащий тестовую последовательность для текущего упражнения

        //словарь соответствия между названиями файлов лекций в левом меню и самими файлами на диске
        Dictionary<string, FileInfo> lectureFilesDictionary = new Dictionary<string, FileInfo>();

        //словарь соответствия между названиями упражнений и директориями содержащими упражнения
        Dictionary<string, DirectoryInfo> exerciseDirectoriesDictionary = new Dictionary<string, DirectoryInfo>();

        //объекты для работы с файлами *.docx
        Microsoft.Office.Interop.Word.Application WordApplication;
        Microsoft.Office.Interop.Word.Document WordDocument;

        public Form1()
        {
            InitializeComponent();
            findAppDirectory();
            initLecuresTreeView();
            initExercisesListView();
            MinGWFolderTextBox.Text = AppConstants.DEFAULT_COMPILER_FOLDER;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //инициализация объектов для работы с файлами *.docx
            WordApplication = new Microsoft.Office.Interop.Word.Application();
            WordDocument = new Microsoft.Office.Interop.Word.Document();
        }

        private void findAppDirectory()
        {
            //инициализация объекта главной директории приложения
            AppDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());

            //если приложение было запущено не в главной директории приложения, а в некой её поддиректории, 
            //то происходит последовательный подъем вверх пока не будет найдена главная директория
            while (!AppDirectory.EnumerateDirectories().Any((dir) => { return dir.Name == AppConstants.LECTURES_DIR_NAME; }))
            {
                AppDirectory = AppDirectory.Parent;
            }
        }

        private void initLecuresTreeView()
        {
            //инициализация объекта директории содержащей папки лекций
            LecturesDirectory = new DirectoryInfo(String.Format(@"{0} \{1}\", AppDirectory.FullName, AppConstants.LECTURES_DIR_NAME));

            //добавление папок с содержанием каждой лекции в список лекций в левом меню
            foreach (DirectoryInfo dir in LecturesDirectory.GetDirectories())
            {
                FileInfo[] lectureFiles = dir.GetFiles();
                List<TreeNode> childNodes = new List<TreeNode>();

                //добавление файлов лекций в подпункты с номерами лекций 
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
            //инициализация объекта директории содержащей папки упражнений
            ExercisesDirectory = new DirectoryInfo(String.Format(@"{0} \{1}\", AppDirectory.FullName, AppConstants.EXERCISES_DIR_NAME));

            //добавление папок с содержанием файлов каждого упражнения в список упражнений в левом меню
            foreach (DirectoryInfo dir in ExercisesDirectory.GetDirectories())
            {
                ExercisesTreeView.Nodes.Add(new TreeNode(dir.Name));
                exerciseDirectoriesDictionary.Add(dir.Name, dir);
            }
        }

        private void LecturesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //загрузка содержимого файла лекции после нажатия на название лекции в левом меню
            if (lectureFilesDictionary.ContainsKey(e.Node.Text))
            {
                FileInfo file = lectureFilesDictionary[e.Node.Text];
                loadWordFile(file);
            }
        }

        private void loadWordFile(FileInfo wordFile)
        {
            //загрузка содержимого *.docx файла лекции в приложение
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

        private void ExercisesTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (exerciseDirectoriesDictionary.ContainsKey(e.Node.Text))
            {
                DirectoryInfo exerciseDirectory = exerciseDirectoriesDictionary[e.Node.Text];
                currentExerciseDirectory = exerciseDirectory;

                //массив файлов содержащихся в директории текущего упражнения
                FileInfo[] exerciseFiles = exerciseDirectory.GetFiles();

                //поиск файла с текстом задания упражнения
                FileInfo exerciseTextFile = exerciseFiles.FirstOrDefault(file => { return file.Name == AppConstants.DEFAULT_EXERCISE_TEXT_FILE_NAME; });
                if (exerciseTextFile != null)
                {
                    ExerciseTextTextBox.Text = System.IO.File.ReadAllText(exerciseTextFile.FullName, Encoding.Default);
                }
                else
                {
                    ExerciseTextTextBox.Clear();
                }

                //поиск файла с шаблоном кода упражнения
                FileInfo exerciseTemplateFile = exerciseFiles.FirstOrDefault(file => { return file.Name == AppConstants.DEFAULT_EXERCISE_TEMPLATE_FILE_NAME; });
                if (exerciseTemplateFile != null)
                {
                    UserCodeTextBox.Text = File.ReadAllText(exerciseTemplateFile.FullName, Encoding.Default);
                }
                else
                {
                    UserCodeTextBox.Clear();
                }

                //поиск тестового файла с тестовой последовательностью для упражнения
                currentExerciseTestFile = exerciseFiles.FirstOrDefault(file => { return file.Name == AppConstants.DEFAULT_EXERCISE_TEST_FILE_NAME; });
            }
        }

        private void RunCodeButton_Click(object sender, EventArgs e)
        {
            if (currentExerciseDirectory != null && currentExerciseCPPFile != null)
            {
                //запуск командной строки
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
                        //компиляция пользовательского кода
                        pWriter.WriteLine(@"path=" + MinGWFolderTextBox.Text);
                        pWriter.WriteLine(String.Format(@"mingw32-g++.exe -Wall -fexceptions -g -c {0} -o ex.o", currentExerciseCPPFile));
                        pWriter.WriteLine(String.Format(@"mingw32-g++.exe  -o {0} ex.o", AppConstants.DEFAULT_COMPILED_FILE_NAME));
                    }
                }

                //считывание сообщений из командной строки в окно приложения
                UserCodeOutputTextBox.Text = process.StandardOutput.ReadToEnd();

                //считывание сообщений об ошибках в окно приложения
                while (!process.StandardError.EndOfStream)
                {
                    string line = process.StandardError.ReadLine();
                }

                //ожидание завершения компиляции
                process.WaitForExit();

                //запуск теста упражнения
                if (currentExerciseTestFile != null)
                {
                    RunExerciseTest(currentExerciseTestFile);
                }
            }
            else
            {
                MessageBox.Show("Сначала сохраните ваш файл или откройте существующий");
            }
        }

        void RunExerciseTest(FileInfo testFile)
        {
            //считывание тестовой последовательности из тестового файла
            string[] testString = File.ReadAllLines(testFile.FullName, Encoding.Default);

            //запуск командной строки
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

            //правильный ответ из тестового файла
            string testAnswerFromFileStr = "";
            double testAnswerFromFile = 0;

            using (StreamWriter pWriter = process.StandardInput)
            {
                if (pWriter.BaseStream.CanWrite)
                {
                    //запуск ранее скомпилированного приложения
                    pWriter.WriteLine(@"path=" + MinGWFolderTextBox.Text);
                    pWriter.WriteLine(AppConstants.DEFAULT_COMPILED_FILE_NAME);
                }

                if (currentExerciseTestFile != null)
                {
                    foreach (string s in testString)
                    {
                        //если в тестовом файле найдена строка начинающаяся с знака "=" - это строка с правильным ответом
                        if (s[0] == '=')
                        {
                            //считывание правильного ответа из тестового файла
                            testAnswerFromFileStr = s.Remove(0, 1); //удаление символа "="
                            testAnswerFromFile = Double.Parse(testAnswerFromFileStr, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
                        }
                        else
                        {
                            //передача в приложение тестовых параметров(аргументов)
                            pWriter.WriteLine(s);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Файла для проведения тестирования не обнаружено!");
                }
            }

            //ответ который возвращает пользовательское приложение после передачи в него тестовых аргументов из тестового файла
            string testAnswerStr = "";
            double testAnswer = 0;
            bool programOutputContainsAnswer = false;

            //вывод сообщения о тестировании
            UserCodeOutputTextBox.Text = UserCodeOutputTextBox.Text +
                System.Environment.NewLine +
                System.Environment.NewLine +
                "::::Начало тестирования::::" +
                System.Environment.NewLine +
                System.Environment.NewLine;

            while (!process.StandardOutput.EndOfStream)
            {
                //считывание сообщений из пользовательского приложения
                string line = process.StandardOutput.ReadLine() + Environment.NewLine;
                UserCodeOutputTextBox.Text = UserCodeOutputTextBox.Text + line;

                //если очередное сообщения из пользовательского приложения содержит подстроку "Answer:"
                if (line.StartsWith("Answer:"))
                {
                    //считывание символов ответа из пользовательского приложения (символов являющихся цифрами или содержащих ".")
                    testAnswerStr = String.Join("", line.ToCharArray().Where(c => { return Char.IsDigit(c) || c == '.'; }));
                    //перевод строки в число
                    testAnswer = Double.Parse(testAnswerStr, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
                    programOutputContainsAnswer = true;
                }
            }

            //ожидание завершения тестирования
            process.WaitForExit();

            //вывод сообщения если пользовательское приложение не возвращало строки начинающейся с "Answer:"
            if (programOutputContainsAnswer == false)
            {
                MessageBox.Show("Внимание! Программа не может быть протестирована, не обнаруженно ключевое слово \"Answer: \"");
            }

            //если были полученны ответ из пользовательского приложения и ответ из тестового файла
            if (testAnswerStr.Length > 0 && testAnswerFromFileStr.Length > 0)
            {
                //сравнение ответов
                if (testAnswer == testAnswerFromFile)
                {
                    MessageBox.Show("Тест пройден :)");
                }
                else
                {
                    MessageBox.Show(String.Format("Правильный ответ: {0}, Результат тестирования: {1}", testAnswerFromFile, testAnswer), "Тест не пройден");
                }
            }

        }

        private void SaveUserCodeButton_Click(object sender, EventArgs e)
        {
            //сохранение .cpp файла с пользовательским кодом
            if (currentExerciseDirectory != null)
            {
                String filePath;
                //если еще не открыто .cpp файла
                if (currentExerciseCPPFile == null)
                {
                    //сохранение .cpp файла в директории текущего упражнения с стандартным именем "exercise.cpp"
                    filePath = String.Format(@"{0}\{1}", currentExerciseDirectory.FullName, AppConstants.DEFAULT_EXERCISE_FILE_NAME);
                }
                else
                {
                    //перезапись открытого .cpp файла 
                    filePath = currentExerciseCPPFile.FullName;
                }
                File.WriteAllLines(filePath, UserCodeTextBox.Lines, Encoding.Default);

                MessageBox.Show("Ваш код сохранен в файл " + filePath);

                //обновление переменной currentExerciseCPPFile содержащей ссылку на текущий .cpp файл
                currentExerciseCPPFile = new FileInfo(filePath);

                //обновление интерфейса приложения
                UserCodeTextBox.Text = File.ReadAllText(currentExerciseCPPFile.FullName);
            }
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            //открытие пользовательского .cpp файла
            if (currentExerciseDirectory != null)
            {
                //запуск диалога открытия файла
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.InitialDirectory = currentExerciseDirectory.FullName;
                openFileDialog.Filter = "Cpp Files|*.cpp";
                openFileDialog.Title = "Выберите файл с вашим кодом";

                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    //обновление переменной currentExerciseCPPFile содержащей ссылку на текущий .cpp файл
                    currentExerciseCPPFile = new FileInfo(openFileDialog.FileName);

                    //обновление интерфейса приложения
                    UserCodeTextBox.Text = File.ReadAllText(currentExerciseCPPFile.FullName);
                }
            }
        }

        private void OpenMinGWFolder_Click(object sender, EventArgs e)
        {
            //задание директории компилятора MinGW
            using (var fbd = new FolderBrowserDialog())
            {
                //открытие диалога выбора директории
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    //обновление пути к директории компилятора
                    MinGWFolderTextBox.Text = fbd.SelectedPath;
                }
            }
        }
    }
}
