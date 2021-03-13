using Assembler;

using FastColoredTextBoxNS;

using NotepadSharp.Utils;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NotepadSharp
{
    public partial class MainForm : Form
    {
        private readonly string partialFormName = "NotepadSharp";
        private readonly string historyFileName = "DataHistoryEditor.dat";
        private readonly string themeFileName = "ThemeCollector.dat";
        private readonly string newFileName = "new";

        public readonly Style BlueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        public readonly Style MagentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);

        private Regex KeywordRegex { get; set; }
        private Regex NumberRegex { get; set; }
        private Style NumberStyle { get; set; }
        private Style KeywordStyle { get; set; }

        private TypeOfTheme currentTheme = TypeOfTheme.Light;

        private readonly List<string> openedFiles = new List<string>();

        // Шлях файлу який присвоюється в filepath.
        private string currentFile = string.Empty;
        // Скільки нових файлів відкрито.
        private int _filesNew;
        // Цифри в секундах для timeInterval.
        private int _timeLeft = 300;
        // Для збереження файлів журнал.
        private int _count;
        // Потрібен для автозбереження.
        private int _newTime;

        public MainForm()
        {
            InitializeComponent();
        }

        private void CreateNewTab(string tabPageName, string fullPath, string text)
        {
            // Створюємо нову вкладку і текстбокс.
            TabPage tabPage = new TabPage
            {
                BackColor = Color.Black,
                BorderStyle = BorderStyle.None,
                Text = tabPageName,
                AccessibleDescription = fullPath
            };
            FastColoredTextBox fctText = new FastColoredTextBox
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Text = text,
                Font = new Font("Consolas", 10)
            };
            ColorTheme.ColorChangerTextBox(fctText, currentTheme);
            tabPage.Controls.Add(fctText);

            // Додаємо в нову вкладку текст-бокс.
            tabOption.TabPages.Add(tabPage); // В головний параметр вкладок додаємо новий з усім функціоналом.
            // Створюємо нову вкладку і текст-бокс.
            tabOption.SelectedTab = tabPage;
            fctText.Select();
            // Додамо контекстне меню в нову вкладку.
            tabPage.ContextMenuStrip = contextOptions;
            fctText.TextChanged += FcbTextBox_TextChanged;
            fctText.TextChangedDelayed += FcbTextBox_TextChanged;
            Text = tabPageName + " " + partialFormName;
        }
        private void OpenOrSelectTab(string fileName, string safeFileName)
        {
            if (openedFiles.Contains(fileName))
            {
                TabPage selected = null;
                foreach (TabPage tab in tabOption.TabPages)
                {
                    if (tab.AccessibleDescription == fileName)
                    {
                        selected = tab;
                        break;
                    }
                }
                tabOption.SelectedTab = selected;
                SelectedPageTextBox().Text = File.ReadAllText(fileName);
            }
            else
            {
                CreateNewTab(safeFileName, fileName, File.ReadAllText(fileName));
                currentFile = fileName;
                // Додаю в історію збережених.
                openedFiles.Add(currentFile);
                File.AppendAllText(historyFileName, currentFile + Environment.NewLine);
            }
        }
        private void DeveloperEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Збираю інфу про вкладку.
                TabControl.TabPageCollection tabcoll1 = tabOption.TabPages;

                if (tabOption.TabCount > 0)
                {
                    TabControl.TabPageCollection tabcoll = tabOption.TabPages;
                    // Перебираємо вкладки.
                    foreach (TabPage tabpage in tabcoll)
                    {
                        tabOption.SelectedTab = tabpage;
                        // Якщо не збереглося, то виконуємо умову.
                        if (!openedFiles.Contains(tabpage.AccessibleDescription))
                        {
                            SaveFile_Click(sender, e);
                            openedFiles.Add(tabpage.AccessibleDescription);
                        }

                        tabOption.TabPages.Remove(tabpage);

                        break;
                    }
                    // Додаємо посилання в файл.
                    File.WriteAllLines(historyFileName, openedFiles);
                }
                LoggingHelper.LogEntry(SystemCategories.GeneralDebug, "Closing " + partialFormName);

            }
            catch (Exception exception)
            {
                LoggingHelper.LogEntry(SystemCategories.GeneralError, exception.Message + " " + exception.StackTrace);
                MessageBox.Show(exception.Message);
            }
        }
        private void DeveloperEditor_Load(object sender, EventArgs e)
        {
            try
            {
                InitStyleSchema();
                InitRegex();

                // Додаємо параметри
                tabOption.ContextMenuStrip = contextTabMenuOptions;
                // Перевірка на існування.
                if (!File.Exists(themeFileName))
                {
                    File.WriteAllText(themeFileName, TypeOfTheme.Light.ToString());
                }

                if (!File.Exists(historyFileName))
                {
                    File.Create(historyFileName);
                }
                else
                {
                    string[] arrPages = File.ReadAllLines(historyFileName);
                    // Якщо посилань більше ніж або дорівнює 1, то виконуємо умову.
                    string[] colors = File.ReadAllLines(themeFileName);
                    if (colors.Length > 0 && Enum.TryParse(colors[0], out TypeOfTheme theme))
                    {
                        currentTheme = theme;
                    }

                    ApplyTheme(currentTheme);
                    if (arrPages.Length > 0)
                    {
                        for (int i = 0; i < arrPages.Length; ++i)
                        {
                            openFile.FileName = arrPages[i].Trim();
                            // Перевіряємо, чи існує файл
                            if (!string.IsNullOrEmpty(openFile.FileName) && openFile.CheckFileExists)
                            {
                                CreateNewTab(openFile.SafeFileName, openFile.FileName, File.ReadAllText(openFile.FileName));
                                openedFiles.Add(openFile.FileName);
                            }
                        }
                    }
                    if (tabOption.TabCount == 0)
                    {
                        NewFile_Click(sender, e);
                    }

                    LoggingHelper.LogEntry(SystemCategories.GeneralDebug, "Opening " + partialFormName);
                }
            }
            catch (Exception exception)
            {
                LoggingHelper.LogEntry(SystemCategories.GeneralError, exception.Message + " " + exception.StackTrace);
                MessageBox.Show(exception.Message);
            }
        }

        #region Timer
        public void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                --_timeLeft;
                // Якщо час дорівнює нулю, то виконується ця дія.
                if (_timeLeft <= 0)
                {
                    timerInterval.Stop();
                    // Потрібен для журналу, файл зберігається в шляху exe-шника
                    string path = $@"backup/BackupText {_count++}.rtf";
                    //
                    TabPage tb = tabOption.SelectedTab;
                    TabControl.TabPageCollection tabcoll = tabOption.TabPages;
                    foreach (TabPage tabpage in tabcoll)
                    {
                        SelectedPageTextBox().SaveToFile(path, Encoding.UTF8);
                        SelectedPageTextBox().SaveToFile(tb.AccessibleDescription, Encoding.UTF8);

                        File.AppendAllText(historyFileName, tb.AccessibleDescription + Environment.NewLine);
                    }

                    _timeLeft = _newTime;
                    timerInterval.Start();
                }
            }
            catch (Exception exception)
            {
                LoggingHelper.LogEntry(SystemCategories.GeneralError, exception.Message + " " + exception.StackTrace);
                MessageBox.Show(exception.Message);
            }
        }

        private void TimerStart()
        {
            _newTime = _timeLeft;
            timerInterval.Interval = 1000;
            timerInterval.Start();
            saveIntervalMenuItem.ShortcutKeyDisplayString = "ON";
            timerInterval.Tick += Timer_Tick;
            timerInterval.Start();
        }
        #endregion

        #region Файл
        private void NewFile_Click(object sender, EventArgs e)
        {
            LoggingHelper.LogEntry(SystemCategories.GeneralUIDebug, "TEMP");
            LoggingHelper.LogEntry(SystemCategories.GeneralUIError, "TEMP");
            // Якщо вкладок більше або дорівнює нуля, то створюємо новий файл.
            if (tabOption.TabCount >= 0)
            {
                CreateNewTab(newFileName + (++_filesNew), null, null);
            }
        }
        private void OpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                // Якщо користувач після вибору файлу для відкриття натиснув "ОК". Відбувається це дія.
                if (openFile.ShowDialog() == DialogResult.OK)
                    OpenOrSelectTab(openFile.FileName, openFile.SafeFileName);
            }
            catch (Exception exception)
            {
                LoggingHelper.LogEntry(SystemCategories.GeneralError, exception.Message + " " + exception.StackTrace);
                MessageBox.Show(exception.Message);
            }
        }
        private void FileSaveFunction(string path)
        {
            using (StreamWriter streamWriter = new StreamWriter(path, false, Encoding.Default))
            {
                // Перевірка на шлях файлу, якщо розширення rtf, то виконуємо цю дію.
                if (path.Contains(".rtf"))
                {
                    streamWriter.WriteLine(SelectedPageTextBox().Rtf);
                }
                else
                {
                    streamWriter.WriteLine(SelectedPageTextBox().Text);
                }
            }
        }
        private void SaveFile_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage tb = tabOption.SelectedTab;
                if (tb != null && tb.Text.Contains(newFileName))
                {
                    // Перебираю вкладки для збереження і перевірки на шлях.
                    TabControl.TabPageCollection tabcoll = tabOption.TabPages;
                    foreach (TabPage tabpage in tabcoll)
                    {
                        tabOption.SelectedTab = tabpage;
                        DialogResult dg = MessageBox.Show(
                            "Do you want to save file " + tabpage.Text,
                            @"Save file",
                            MessageBoxButtons.YesNoCancel);

                        if (dg == DialogResult.Yes)
                        {
                            // Якщо це не створений файл то виконую швидке збереження.
                            if (!openedFiles.Contains(tabpage.AccessibleDescription))
                            {
                                // Метод запису.
                                FileSaveFunction(tabpage.AccessibleDescription);
                                currentFile = tabpage.AccessibleDescription;
                                // Міняю назву редактора.
                                Text = currentFile + " " + partialFormName;
                            }
                            else if (saveFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                // Метод запису.
                                FileSaveFunction(saveFileDialog.FileName);
                                currentFile = saveFileDialog.FileName;
                                // Міняю назву редактора.
                                Text = currentFile + " " + partialFormName;
                                openFile.FileName = saveFileDialog.FileName;
                                tabpage.Text = openFile.SafeFileName;
                                tabpage.AccessibleDescription = openFile.FileName;
                                // Додаю в історію збережених.                        
                                File.AppendAllText(historyFileName, currentFile + Environment.NewLine);
                            }
                        }
                        else if (dg == DialogResult.Cancel)
                        {
                            tabOption.Select();
                            break;
                        }
                    }
                }
                else
                {
                    // Перевірка на збереження.
                    if (!tb.Text.Contains(newFileName))
                    {
                        FileSaveFunction(tb.AccessibleDescription); // Метод збереження.
                    }
                    else if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileSaveFunction(saveFileDialog.FileName); // Метод збереження.

                        // Додаю в змінні шляху збереження файлу.
                        openFile.FileName = saveFileDialog.FileName;
                        tb.Text = openFile.SafeFileName;
                        currentFile = openFile.FileName;
                        // Додаю в історію збережених.
                        File.AppendAllText(historyFileName, currentFile + Environment.NewLine);
                        tb.AccessibleDescription = openFile.FileName;
                    }
                }
            }
            catch (Exception exception)
            {
                LoggingHelper.LogEntry(SystemCategories.GeneralError, exception.Message + " " + exception.StackTrace);
                MessageBox.Show(exception.Message);
            }
        }
        private void SaveAsFile_Click(object sender, EventArgs e)
        {
            try
            {
                // Якщо вкладок більше або дорівнює 1 то виконуємо збереження аналогічне з SaveFile.
                if (tabOption.TabCount >= 1)
                {
                    TabPage tabPage = tabOption.SelectedTab;
                    if (!openedFiles.Contains(tabPage.AccessibleDescription))
                    {
                        if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            // Метод збереження.
                            FileSaveFunction(saveFileDialog.FileName);
                            Text = saveFileDialog.FileName + " " + partialFormName;
                            openFile.FileName = saveFileDialog.FileName;
                            tabPage.Text = openFile.SafeFileName;
                            string prevName = tabPage.AccessibleDescription;
                            tabPage.AccessibleDescription = saveFileDialog.FileName;
                        }
                    }
                    else
                    {
                        // Метод збереження.
                        FileSaveFunction(tabPage.AccessibleDescription);
                        Text = tabPage.AccessibleDescription + " " + partialFormName;
                        openFile.FileName = tabPage.AccessibleDescription;
                        tabPage.Text = openFile.SafeFileName;
                        // Запис в історію.
                        File.WriteAllText(historyFileName, File.ReadAllText(historyFileName).Replace(tabPage.AccessibleDescription, saveFileDialog.FileName));
                    }
                }
            }
            catch (Exception exception)
            {
                LoggingHelper.LogEntry(SystemCategories.GeneralError, exception.Message + " " + exception.StackTrace);
                MessageBox.Show(exception.Message);
            }
        }
        #endregion

        #region Вкладка і компіляція
        private FastColoredTextBox SelectedPageTextBox()
        {
            // Присвоїти обрану вкладку в новий TabPage.
            TabPage tabpage = tabOption.SelectedTab;
            if (tabpage != null)
            {
                FastColoredTextBox box = tabpage.Controls.OfType<FastColoredTextBox>().FirstOrDefault();
                if (box != null)
                {
                    documentMap.Target = box;
                }

                return box;
            }
            return null;
        }
        private void CompileToolMenuItem_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(currentFile, ".as$", RegexOptions.Compiled))
            {
                MessageBox.Show("Compile is available only for '.as' files!");
                return;
            }
            string filename = Path.ChangeExtension(currentFile, ".mc");
            ASol project = new ASol();
            List<string> lst = new List<string>
            {
                currentFile,
                filename
            };
            if (Project(project, lst))
            {
                openFile.FileName = filename;
                OpenOrSelectTab(openFile.FileName, openFile.SafeFileName);
            }
        }
        private void RunToolMenuItem_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(currentFile, ".mc$", RegexOptions.Compiled))
            {
                MessageBox.Show("Run is available only for '.mc' files!");
                return;
            }
            string filename = Path.ChangeExtension(currentFile, ".txt");
            SSol project = new SSol();
            List<string> lst = new List<string>
            {
                currentFile,
                filename
            };
            if(Project(project, lst))
            {
                openFile.FileName = filename;
                OpenOrSelectTab(openFile.FileName, openFile.SafeFileName);
            }
        }
        private void CompileRunToolMenuItem_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(currentFile, ".as$", RegexOptions.Compiled))
            {
                MessageBox.Show("Compile is available only for '.as' files!");
                return;
            }
            string compile = Path.ChangeExtension(currentFile, ".mc");
            string report = Path.ChangeExtension(currentFile, ".txt");
            ASSol project = new ASSol();
            List<string> lst = new List<string>
            {
                currentFile,
                compile,
                report
            };
            if(Project(project, lst))
            {
                openFile.FileName = compile;
                OpenOrSelectTab(openFile.FileName, openFile.SafeFileName);
                openFile.FileName = report;
                OpenOrSelectTab(openFile.FileName, openFile.SafeFileName);
            }
        }
        private static bool Project(ISol sol, List<string> argv)
        {
            try
            {
                sol.Run(argv);
                LoggingHelper.LogEntry(SystemCategories.GeneralUIDebug, sol.Result());
                return true;
            }
            catch (MessageException me)
            {
                LoggingHelper.LogEntry(SystemCategories.GeneralUIError, me.Message);
            }
            catch (Exception ex)
            {
                LoggingHelper.LogEntry(SystemCategories.GeneralUIError, ex.Message + ex.StackTrace);
            }
            return false;
        }
        #endregion

        #region Themes
        private void LightTheme_Click(object sender, EventArgs e)
        {
            ApplyTheme(TypeOfTheme.Light);
            File.WriteAllText(themeFileName, TypeOfTheme.Light.ToString());
        }
        private void DarkTheme_Click(object sender, EventArgs e)
        {
            ApplyTheme(TypeOfTheme.Dark);
            File.WriteAllText(themeFileName, TypeOfTheme.Dark.ToString());
        }
        private void HackerTheme_Click(object sender, EventArgs e)
        {
            ApplyTheme(TypeOfTheme.Special);
            File.WriteAllText(themeFileName, TypeOfTheme.Special.ToString());
        }
        private void ApplyTheme(TypeOfTheme theme)
        {
            ColorTheme.ColorChangerDeveloperForm(this, mainMenu, toolButtons, statusStrip, tabOption, SelectedPageTextBox(), theme);

        }
        private void ForeColorTheme_Click(object sender, EventArgs e)
        {
            colorOption.ShowDialog();
            SelectedPageTextBox().ForeColor = colorOption.Color;
        }
        #endregion

        #region Update/Change
        private void FcbTextBox_TextChanged(object sender, EventArgs e)
        {
            ((FastColoredTextBox)sender).Range.ClearStyle(KeywordStyle, NumberStyle);
            ((FastColoredTextBox)sender).Range.SetStyle(NumberStyle, NumberRegex);
            ((FastColoredTextBox)sender).Range.SetStyle(KeywordStyle, KeywordRegex);

        }
        private void UpdateWindow()
        {
            TabControl.TabPageCollection tabcoll = tabOption.TabPages;

            foreach (TabPage tabpage in tabcoll)
            {
                ToolStripMenuItem menuitem = new ToolStripMenuItem();
                string s = tabpage.Text;
                menuitem.Text = s;
                if (tabOption.SelectedTab == tabpage)
                {
                    menuitem.Checked = true;
                }
                else
                {
                    menuitem.Checked = false;
                }

                Invalidate();

                menuitem.Click += WindowList;
            }
        }
        private void WindowList(object sender, EventArgs e)
        {
            ToolStripItem toolstripitem = (ToolStripItem)sender;
            TabControl.TabPageCollection tabcoll = tabOption.TabPages;
            foreach (TabPage tb in tabcoll)
            {
                if (toolstripitem.Text == tb.Text)
                {
                    // Перебираємо циклом і оновлюємо кожну вкладку.
                    tabOption.SelectedTab = tb;
                    Control fcTextBox = tabOption.TabPages[tabOption.SelectedIndex].Controls[0];
                    fcTextBox.Select();
                    UpdateWindow();
                }
            }
        }
        protected void InitRegex()
        {
            NumberRegex = new Regex(@"\b\d+\b", RegexOptions.Compiled);
            string commands = string.Format(@"\b({0})\b|[{2}]\b{1}\b", 
                string.Join("|", CommonUtil.Commands.Where(c => (int)c.Key < CommonUtil.startMemCommand).Select(c => c.Value)),
                string.Join("|", CommonUtil.Commands.Where(c => (int)c.Key >= CommonUtil.startMemCommand).Select(c => c.Value.Replace(CommonUtil.memSpecialCommandSymbol, ""))),
                CommonUtil.memSpecialCommandSymbol);
            KeywordRegex = new Regex(commands, RegexOptions.Compiled);
        }
        public void InitStyleSchema()
        {
            NumberStyle = MagentaStyle;
            KeywordStyle = BlueStyle;
        }
        #endregion

        #region Кнопки меню и быстрого доступа
        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int newIndex = tabOption.TabPages.IndexOf(tabOption.SelectedTab) - 1;

                openedFiles.Remove(tabOption.SelectedTab.AccessibleDescription);

                tabOption.TabPages.Remove(tabOption.SelectedTab);
                if (tabOption.TabPages.Count != 0)
                {
                    tabOption.SelectedTab = tabOption.TabPages[Math.Max(newIndex, 0)];
                }
            }
            catch (Exception ex)
            {
                LoggingHelper.LogEntry(SystemCategories.GeneralError, ex.Message + " " + ex.StackTrace);
            }

        }
        private void BackgroundTextTheme(object sender, EventArgs e)
        {
            colorOption.ShowDialog();
            SelectedPageTextBox().BackColor = colorOption.Color;
        }
        private void SelectMenu_Click(object sender, EventArgs e)
        {
            if (SelectedPageTextBox().TextLength > 0)
            {
                SelectedPageTextBox().SelectAll();
            }
        }
        private void CopyMenu_Click(object sender, EventArgs e)
        {
            if (SelectedPageTextBox().TextLength > 0)
            {
                SelectedPageTextBox().Copy();
            }
        }
        private void PasteMenu_Click(object sender, EventArgs e)
        {
            if (SelectedPageTextBox().TextLength > 0)
            {
                SelectedPageTextBox().Paste();
            }
        }
        private void SelectAllMenu_Click(object sender, EventArgs e)
        {
            if (SelectedPageTextBox().TextLength > 0)
            {
                SelectedPageTextBox().SelectAll();
            }
        }
        private void CutMenu_Click(object sender, EventArgs e)
        {
            if (SelectedPageTextBox().TextLength > 0)
            {
                SelectedPageTextBox().Cut();
            }
        }
        private void UpdateWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateWindow();
        }

        private void CutContextMenu_Click(object sender, EventArgs e)
        {
            CutButton_Click(sender, e);
        }

        private void SelectContextMenu_Click(object sender, EventArgs e)
        {
            SelectMenu_Click(sender, e);
        }

        private void RedoContextMenu_Click(object sender, EventArgs e)
        {
            RedoMenu_Click(sender, e);
        }

        private void UndoContextMenu_Click(object sender, EventArgs e)
        {
            UndoMenu_Click(sender, e);
        }

        private void DeleteToolStripMenu_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabOption.TabPages.Count >= 1)
                {
                    if (SelectedPageTextBox().TextLength > 0)
                    {
                        SelectedPageTextBox().SelectedText = string.Empty;
                    }
                }
            }
            catch (Exception exception)
            {
                LoggingHelper.LogEntry(SystemCategories.GeneralError, exception.Message + " " + exception.StackTrace);
                MessageBox.Show(exception.Message);
            }

        }
        private void FullWindowedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
        }

        private void FirstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _timeLeft = 300;
            TimerStart();
        }
        private void SecondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _timeLeft = 600;
            TimerStart();
        }
        private void ThirdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _timeLeft = 1200;
            TimerStart();
        }
        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerInterval.Stop();
            saveIntervalMenuItem.ShortcutKeyDisplayString = "OFF";
            _timeLeft = 0;
        }
        private void CloseTabPageButton_Click(object sender, EventArgs e)
        {
            tabOption.TabPages.Remove(tabOption.SelectedTab);
        }

        private void DeleteToolStripMenu_Click_1(object sender, EventArgs e)
        {
            DeleteToolStripMenu_Click(sender, e);
        }

        private void NewDeveloperWindow_Click(object sender, EventArgs e)
        {
            MainForm newNotepad = new MainForm();
            newNotepad.Show();
        }
        // Інформація про прогу.
        private void AboutPanel_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Build version: " + ProductVersion);
        }

        // Скасовуємо змінений текст.
        private void UndoMenu_Click(object sender, EventArgs e)
        {
            BackButton_Click(sender, e);
        }

        // Повернемо змінений текст.
        private void RedoMenu_Click(object sender, EventArgs e)
        {
            ReturnButton_Click(sender, e);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            SelectedPageTextBox()?.Undo();
        }

        // Закриття програми.
        private void ExitTool_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ReturnButton_Click(object sender, EventArgs e)
        {
            SelectedPageTextBox()?.Redo();
        }

        // Переходить в подія створення нового файлу.
        private void NewFile_Button(object sender, EventArgs e)
        {
            NewFile_Click(sender, e);
        }

        // Переходить в подія відкриття нового файлу.
        private void OpenFile_Button(object sender, EventArgs e)
        {
            OpenFile_Click(sender, e);
        }

        // Переходить в подія збереження файлу з вибором розширення.
        private void SaveAs_Button(object sender, EventArgs e)
        {
            SaveAsFile_Click(sender, e);
        }

        // Переходить в події копії тексту.
        private void CopyText(object sender, EventArgs e)
        {
            CopyMenu_Click(sender, e);
        }

        // Вставити текст.
        private void PasteText(object sender, EventArgs e)
        {
            PasteMenu_Click(sender, e);
        }

        // Вирізати текст.
        private void CutText(object sender, EventArgs e)
        {
            CutMenu_Click(sender, e);
        }

        // Вирізати текст
        private void CutButton_Click(object sender, EventArgs e)
        {
            CutMenu_Click(sender, e);
        }

        // Копіювати текст
        private void CopyButton_Click(object sender, EventArgs e)
        {
            CopyMenu_Click(sender, e);
        }

        // Збереження
        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            SaveFile_Click(sender, e);
        }

        // Виділити все
        private void SelectAll_Click(object sender, EventArgs e)
        {
            SelectAllMenu_Click(sender, e);
        }

        // Збільшення текстбокса
        private void ZoomIn_Click(object sender, EventArgs e)
        {
            if (SelectedPageTextBox().Zoom < 500)
            {
                SelectedPageTextBox().Zoom += 25;
            }
            else
            {
                MessageBox.Show($@"The zoom value is greater than {SelectedPageTextBox().Zoom}!");
            }
        }
        // Зменшення текстбокса
        private void ZoomOut_Click(object sender, EventArgs e)
        {
            if (SelectedPageTextBox().Zoom <= 0)
            {
                MessageBox.Show($@"The zoom value is less than {SelectedPageTextBox().Zoom}!");
            }
            else
            {
                SelectedPageTextBox().Zoom -= 15;
            }
        }
        private void TabOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            FastColoredTextBox tb = SelectedPageTextBox();
            if (tb != null)
            {
                string text = tb.Text;
                string[] lines = text.Split('\n');
                statusLabelStrip.Text = $@"Symbols: {text.Length}";
                rowsInfoStrip.Text = $@"Rows: {lines.Length}";
                currentFile = tabOption.SelectedTab.AccessibleDescription;
            }
        }
        #endregion
    }
}
