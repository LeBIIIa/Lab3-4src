using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using ScintillaNET.Demo.Utils;
using Assembler;
using ScintillaNET;
using System.Text;
using System.Text.RegularExpressions;

namespace ScintillaNET.Demo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        ScintillaNET.Scintilla curTextArea = new Scintilla();
        private int newFiles = 1;
        FileInf curFileName;
        Dictionary<string, FileInf> files;
        readonly string ProgramName = "TextEditor";
        Sol project;

        private void MainForm_Load(object sender, EventArgs e)
        {

            files = new Dictionary<string, FileInf>();
            // CREATE CONTROL
            curTextArea = new Scintilla();
            TextPanel.Controls.Add(curTextArea);
            InitTextArea(curTextArea);
            Logger.InitLogger();
        }

        private void InitTextArea(Scintilla curTextArea)
        {
            // BASIC CONFIG
            curTextArea.Dock = DockStyle.Fill;

            // INITIAL VIEW CONFIG
            curTextArea.WrapMode = WrapMode.None;
            curTextArea.IndentationGuides = IndentView.LookBoth;

            // STYLING
            InitColors(curTextArea);
            InitSyntaxColoring(curTextArea);

            // NUMBER MARGIN
            InitNumberMargin(curTextArea);

            // BOOKMARK MARGIN
            InitBookmarkMargin(curTextArea);

            // CODE FOLDING MARGIN
            InitCodeFolding(curTextArea);

            // DRAG DROP
            InitDragDropFile(curTextArea);

            // INIT HOTKEYS
            InitHotkeys(curTextArea);
        }

        private void InitColors(Scintilla curTextArea)
        {
            curTextArea.SetSelectionBackColor(true, IntToColor(0x114D9C));
        }

        private void InitHotkeys(Scintilla curTextArea)
        {

            // register the hotkeys with the form
            HotKeyManager.AddHotKey(this, OpenSearch, Keys.F, true);
            HotKeyManager.AddHotKey(this, OpenFindDialog, Keys.F, true, false, true);
            HotKeyManager.AddHotKey(this, OpenReplaceDialog, Keys.R, true);
            HotKeyManager.AddHotKey(this, OpenReplaceDialog, Keys.H, true);
            HotKeyManager.AddHotKey(this, Uppercase, Keys.U, true);
            HotKeyManager.AddHotKey(this, Lowercase, Keys.L, true);
            HotKeyManager.AddHotKey(this, ZoomIn, Keys.Oemplus, true);
            HotKeyManager.AddHotKey(this, ZoomOut, Keys.OemMinus, true);
            HotKeyManager.AddHotKey(this, ZoomDefault, Keys.D0, true);
            HotKeyManager.AddHotKey(this, CloseSearch, Keys.Escape);

            // remove conflicting hotkeys from scintilla
            curTextArea.ClearCmdKey(Keys.Control | Keys.F);
            curTextArea.ClearCmdKey(Keys.Control | Keys.R);
            curTextArea.ClearCmdKey(Keys.Control | Keys.H);
            curTextArea.ClearCmdKey(Keys.Control | Keys.L);
            curTextArea.ClearCmdKey(Keys.Control | Keys.U);

        }

        private void InitSyntaxColoring(Scintilla curTextArea)
        {

            // Configure the default style
            curTextArea.StyleResetDefault();
            curTextArea.Styles[Style.Default].Font = "Consolas";
            curTextArea.Styles[Style.Default].Size = 10;
            curTextArea.Styles[Style.Default].BackColor = IntToColor(0x212121);
            curTextArea.Styles[Style.Default].ForeColor = IntToColor(0xFFFFFF);
            curTextArea.StyleClearAll();

            // Configure the CPP (C#) lexer styles
            curTextArea.Styles[Style.Cpp.Identifier].ForeColor = IntToColor(0xD0DAE2);
            curTextArea.Styles[Style.Cpp.Comment].ForeColor = IntToColor(0xBD758B);
            curTextArea.Styles[Style.Cpp.CommentLine].ForeColor = IntToColor(0x40BF57);
            curTextArea.Styles[Style.Cpp.CommentDoc].ForeColor = IntToColor(0x2FAE35);
            curTextArea.Styles[Style.Cpp.Number].ForeColor = IntToColor(0xFFFF00);
            curTextArea.Styles[Style.Cpp.String].ForeColor = IntToColor(0xFFFF00);
            curTextArea.Styles[Style.Cpp.Character].ForeColor = IntToColor(0xE95454);
            curTextArea.Styles[Style.Cpp.Preprocessor].ForeColor = IntToColor(0x8AAFEE);
            curTextArea.Styles[Style.Cpp.Operator].ForeColor = IntToColor(0xE0E0E0);
            curTextArea.Styles[Style.Cpp.Regex].ForeColor = IntToColor(0xff00ff);
            curTextArea.Styles[Style.Cpp.CommentLineDoc].ForeColor = IntToColor(0x77A7DB);
            curTextArea.Styles[Style.Cpp.Word].ForeColor = IntToColor(0x48A8EE);
            curTextArea.Styles[Style.Cpp.Word2].ForeColor = IntToColor(0xF98906);
            curTextArea.Styles[Style.Cpp.CommentDocKeyword].ForeColor = IntToColor(0xB3D991);
            curTextArea.Styles[Style.Cpp.CommentDocKeywordError].ForeColor = IntToColor(0xFF0000);
            curTextArea.Styles[Style.Cpp.GlobalClass].ForeColor = IntToColor(0x48A8EE);

            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            foreach (var c in (Assembler.Command[])Enum.GetValues(typeof(Assembler.Command)))
            {
                if ((int)c < CommonUtil.startMemCommand)
                {
                    sb1.Append(c.ToString() + " ");
                }
                else
                {
                    sb2.Append(c.ToString() + " ");
                }

            }
            curTextArea.Lexer = Lexer.Cpp;

            curTextArea.SetKeywords(0, sb1.ToString());
            curTextArea.SetKeywords(1, sb2.ToString());
            //curTextArea.SetKeywords(0, "class extends implements import interface new case do while else if for in switch throw get set function var try catch finally while with default break continue delete return each const namespace package include use is as instanceof typeof author copy default deprecated eventType example exampleText exception haxe inheritDoc internal link mtasc mxmlc param private return see serial serialData serialField since throws usage version langversion playerversion productversion dynamic private public partial static intrinsic internal native override protected AS3 final super this arguments null Infinity NaN undefined true false abstract as base bool break by byte case catch char checked class const continue decimal default delegate do double descending explicit event extern else enum false finally fixed float for foreach from goto group if implicit in int interface internal into is lock long new null namespace object operator out override orderby params private protected public readonly ref return switch struct sbyte sealed short sizeof stackalloc static string select this throw true try typeof uint ulong unchecked unsafe ushort using var virtual volatile void while where yield");
            //curTextArea.SetKeywords(1, "void Null ArgumentError arguments Array Boolean Class Date DefinitionError Error EvalError Function int Math Namespace Number Object RangeError ReferenceError RegExp SecurityError String SyntaxError TypeError uint XML XMLList Boolean Byte Char DateTime Decimal Double Int16 Int32 Int64 IntPtr SByte Single UInt16 UInt32 UInt64 UIntPtr Void Path File System Windows Forms ScintillaNET");

        }

        private void CloneTab(FileInf file)
        {
            // create new tab
            TabPage tp = new TabPage();
            tp.Text = file.filename;

            // iterate through each control and clone it
            foreach (Control c in tabControl.TabPages[0].Controls)
            {
                // clone control (this references the code project download ControlFactory.cs)
                Control ctrl = ControlFactory.CloneCtrl(c);
                GetAllControls(c, ctrl);
                // now add it to the new tab
                tp.Controls.Add(ctrl);
                // set bounds to size and position
                ctrl.SetBounds(c.Bounds.X, c.Bounds.Y, c.Bounds.Width, c.Bounds.Height);
            }

            // now add tab page
            tabControl.TabPages.Add(tp);
            tabControl.SelectedTab = tp;
            file.page = tp;
        }

        private void GetAllControls(Control container, Control dest)
        {
            foreach (Control c in container.Controls)
            {
                Control ctrl;
                if (!(c is Scintilla))
                {
                    ctrl = ControlFactory.CloneCtrl(c);
                }
                else
                {
                    curTextArea = new Scintilla();
                    InitTextArea(curTextArea);
                    ctrl = curTextArea;
                }
                dest.Controls.Add(ctrl);
                GetAllControls(c, ctrl);
            }
        }

        private string GetNextFileName()
        {
            return $"new {newFiles++}";
        }


        #region Numbers, Bookmarks, Code Folding

        /// <summary>
        /// the background color of the text area
        /// </summary>
        private const int BACK_COLOR = 0x2A211C;

        /// <summary>
        /// default text color of the text area
        /// </summary>
        private const int FORE_COLOR = 0xB7B7B7;

        /// <summary>
        /// change this to whatever margin you want the line numbers to show in
        /// </summary>
        private const int NUMBER_MARGIN = 1;

        /// <summary>
        /// change this to whatever margin you want the bookmarks/breakpoints to show in
        /// </summary>
        private const int BOOKMARK_MARGIN = 2;
        private const int BOOKMARK_MARKER = 2;

        /// <summary>
        /// change this to whatever margin you want the code folding tree (+/-) to show in
        /// </summary>
        private const int FOLDING_MARGIN = 3;

        /// <summary>
        /// set this true to show circular buttons for code folding (the [+] and [-] buttons on the margin)
        /// </summary>
        private const bool CODEFOLDING_CIRCULAR = true;

        private void InitNumberMargin(Scintilla curTextArea)
        {

            curTextArea.Styles[Style.LineNumber].BackColor = IntToColor(BACK_COLOR);
            curTextArea.Styles[Style.LineNumber].ForeColor = IntToColor(FORE_COLOR);
            curTextArea.Styles[Style.IndentGuide].ForeColor = IntToColor(FORE_COLOR);
            curTextArea.Styles[Style.IndentGuide].BackColor = IntToColor(BACK_COLOR);

            var nums = curTextArea.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;

            curTextArea.MarginClick += TextArea_MarginClick;
        }

        private void InitBookmarkMargin(Scintilla curTextArea)
        {

            var margin = curTextArea.Margins[BOOKMARK_MARGIN];
            margin.Width = 20;
            margin.Sensitive = true;
            margin.Type = MarginType.Symbol;
            margin.Mask = (1 << BOOKMARK_MARKER);

            var marker = curTextArea.Markers[BOOKMARK_MARKER];
            marker.Symbol = MarkerSymbol.Circle;
            marker.SetBackColor(IntToColor(0xFF003B));
            marker.SetForeColor(IntToColor(0x000000));
            marker.SetAlpha(100);

        }

        private void InitCodeFolding(Scintilla curTextArea)
        {

            curTextArea.SetFoldMarginColor(true, IntToColor(BACK_COLOR));
            curTextArea.SetFoldMarginHighlightColor(true, IntToColor(BACK_COLOR));

            // Enable code folding
            curTextArea.SetProperty("fold", "1");
            curTextArea.SetProperty("fold.compact", "1");

            // Configure a margin to display folding symbols
            curTextArea.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
            curTextArea.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
            curTextArea.Margins[FOLDING_MARGIN].Sensitive = true;
            curTextArea.Margins[FOLDING_MARGIN].Width = 20;

            // Set colors for all folding markers
            for (int i = 25; i <= 31; i++)
            {
                curTextArea.Markers[i].SetForeColor(IntToColor(BACK_COLOR)); // styles for [+] and [-]
                curTextArea.Markers[i].SetBackColor(IntToColor(FORE_COLOR)); // styles for [+] and [-]
            }

            // Configure folding markers with respective symbols
            curTextArea.Markers[Marker.Folder].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
            curTextArea.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
            curTextArea.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
            curTextArea.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
            curTextArea.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CIRCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
            curTextArea.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
            curTextArea.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

            // Enable automatic folding
            curTextArea.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

        }

        private void TextArea_MarginClick(object sender, MarginClickEventArgs e)
        {
            if (e.Margin == BOOKMARK_MARGIN)
            {
                // Do we have a marker for this line?
                const uint mask = (1 << BOOKMARK_MARKER);
                var line = curTextArea.Lines[curTextArea.LineFromPosition(e.Position)];
                if ((line.MarkerGet() & mask) > 0)
                {
                    // Remove existing bookmark
                    line.MarkerDelete(BOOKMARK_MARKER);
                }
                else
                {
                    // Add bookmark
                    line.MarkerAdd(BOOKMARK_MARKER);
                }
            }
        }

        #endregion

        #region Drag & Drop File

        public void InitDragDropFile(Scintilla curTextArea)
        {

            curTextArea.AllowDrop = true;
            curTextArea.DragEnter += delegate (object sender, DragEventArgs e) {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            };
            curTextArea.DragDrop += delegate (object sender, DragEventArgs e) {

                // get file drop
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {

                    Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
                    if (a != null)
                    {

                        string path = a.GetValue(0).ToString();

                        LoadDataFromFile(path);

                    }
                }
            };

        }

        private void LoadDataFromFile(string path)
        {
            if (File.Exists(path))
            {
                curTextArea.Text = File.ReadAllText(path);
            }
        }
        private void SaveDataToFile(string path)
        {
            File.WriteAllText(path, curTextArea.Text);
        }
        #endregion

        #region Main Menu Commands

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (files.Count == 0)
            {
                string file = GetNextFileName();
                var t = new FileInf(file, file, true);
                files.Add(file, t);
                curFileName = t;
                t.page = tabControl.SelectedTab;
                tabControl.SelectedTab.Text = t.filename;
            }
            else
            {
                string file = GetNextFileName();
                var t = new FileInf(file, file, true);
                files.Add(file, t);
                CloneTab(t);
            }
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var t = new FileInf(Path.GetFileName(openFileDialog.FileName), openFileDialog.FileName, false);
                if (files.Count == 0)
                {
                    files.Add(t.filename, t);
                    curFileName = t;
                    tabControl.SelectedTab.Text = t.filename;
                    LoadDataFromFile(openFileDialog.FileName);
                }
                else
                {
                    if (files.Any(f => f.Value.fullPath == t.fullPath))
                    {
                        tabControl.SelectedTab = files[t.fullPath].page;
                    }
                    else
                    {
                        files.Add(Path.GetFileName(openFileDialog.FileName), t);
                        CloneTab(t);
                        LoadDataFromFile(openFileDialog.FileName);
                    }
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (curFileName.isNew)
            {
                saveWithDialog();
            }
            else
            {
                SaveDataToFile(curFileName.fullPath);
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveWithDialog();
        }

        private void saveWithDialog()
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                curFileName.fullPath = saveFileDialog.FileName;
                curFileName.filename = Path.GetFileName(saveFileDialog.FileName);
                tabControl.SelectedTab.Text = curFileName.filename;
                SaveDataToFile(saveFileDialog.FileName);
            }
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(curFileName.filename, ".as$"))
            {
                MessageBox.Show("Project build/run only available for '.as'/'.mc' files!");
            }
            try
            {
                project = new ASol();
                project.Run();
            }
            catch (MessageException me)
            {
                if (me is MessageException)
                {

                }
                else
                {
                    Logger.Log
                }

            }
            finally
            {
                Logger.MemoryAppender.Clear();
            }
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(curFileName.filename, ".mc$"))
            {
                MessageBox.Show("Project build/run only available for '.as'/'.mc' files!");
            }
            try
            {
                project = new SSol();
                project.Run();
            }
            catch (MessageException me)
            {
                if (me is MessageException)
                {

                }
                else
                {
                    Logger.Log
                }
            }
            finally
            {
                Logger.MemoryAppender.Clear();
            }
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenSearch();
        }

        private void findDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFindDialog();
        }

        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenReplaceDialog();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            curTextArea.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            curTextArea.Copy();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            curTextArea.Paste();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            curTextArea.SelectAll();
        }

        private void selectLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Line line = curTextArea.Lines[curTextArea.CurrentLine];
            curTextArea.SetSelection(line.Position + line.Length, line.Position);
        }

        private void clearSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            curTextArea.SetEmptySelection(0);
        }

        private void indentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Indent();
        }

        private void outdentSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Outdent();
        }

        private void uppercaseSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uppercase();
        }

        private void lowercaseSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Lowercase();
        }

        private void wordWrapToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            // toggle word wrap
            wordWrapItem.Checked = !wordWrapItem.Checked;
            curTextArea.WrapMode = wordWrapItem.Checked ? WrapMode.Word : WrapMode.None;
        }

        private void indentGuidesToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // toggle indent guides
            indentGuidesItem.Checked = !indentGuidesItem.Checked;
            curTextArea.IndentationGuides = indentGuidesItem.Checked ? IndentView.LookBoth : IndentView.None;
        }

        private void hiddenCharactersToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // toggle view whitespace
            hiddenCharactersItem.Checked = !hiddenCharactersItem.Checked;
            curTextArea.ViewWhitespace = hiddenCharactersItem.Checked ? WhitespaceMode.VisibleAlways : WhitespaceMode.Invisible;
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomIn();
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomOut();
        }

        private void zoom100ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ZoomDefault();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            curTextArea.FoldAll(FoldAction.Contract);
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            curTextArea.FoldAll(FoldAction.Expand);
        }


        #endregion

        #region Uppercase / Lowercase

        private void Lowercase()
        {

            // save the selection
            int start = curTextArea.SelectionStart;
            int end = curTextArea.SelectionEnd;

            // modify the selected text
            curTextArea.ReplaceSelection(curTextArea.GetTextRange(start, end - start).ToLower());

            // preserve the original selection
            curTextArea.SetSelection(start, end);
        }

        private void Uppercase()
        {

            // save the selection
            int start = curTextArea.SelectionStart;
            int end = curTextArea.SelectionEnd;

            // modify the selected text
            curTextArea.ReplaceSelection(curTextArea.GetTextRange(start, end - start).ToUpper());

            // preserve the original selection
            curTextArea.SetSelection(start, end);
        }

        #endregion

        #region Indent / Outdent

        private void Indent()
        {
            // we use this hack to send "Shift+Tab" to scintilla, since there is no known API to indent,
            // although the indentation function exists. Pressing TAB with the editor focused confirms this.
            GenerateKeystrokes("{TAB}");
        }

        private void Outdent()
        {
            // we use this hack to send "Shift+Tab" to scintilla, since there is no known API to outdent,
            // although the indentation function exists. Pressing Shift+Tab with the editor focused confirms this.
            GenerateKeystrokes("+{TAB}");
        }

        private void GenerateKeystrokes(string keys)
        {
            HotKeyManager.Enable = false;
            curTextArea.Focus();
            SendKeys.Send(keys);
            HotKeyManager.Enable = true;
        }

        #endregion

        #region Zoom

        private void ZoomIn()
        {
            curTextArea.ZoomIn();
        }

        private void ZoomOut()
        {
            curTextArea.ZoomOut();
        }

        private void ZoomDefault()
        {
            curTextArea.Zoom = 0;
        }


        #endregion

        #region Quick Search Bar

        bool SearchIsOpen = false;

        private void OpenSearch()
        {

            SearchManager.SearchBox = TxtSearch;
            SearchManager.TextArea = curTextArea;

            if (!SearchIsOpen)
            {
                SearchIsOpen = true;
                InvokeIfNeeded(delegate () {
                    PanelSearch.Visible = true;
                    TxtSearch.Text = SearchManager.LastSearch;
                    TxtSearch.Focus();
                    TxtSearch.SelectAll();
                });
            }
            else
            {
                InvokeIfNeeded(delegate () {
                    TxtSearch.Focus();
                    TxtSearch.SelectAll();
                });
            }
        }
        private void CloseSearch()
        {
            if (SearchIsOpen)
            {
                SearchIsOpen = false;
                InvokeIfNeeded(delegate () {
                    PanelSearch.Visible = false;
                    //CurBrowser.GetBrowser().StopFinding(true);
                });
            }
        }

        private void BtnClearSearch_Click(object sender, EventArgs e)
        {
            CloseSearch();
        }

        private void BtnPrevSearch_Click(object sender, EventArgs e)
        {
            SearchManager.Find(false, false);
        }
        private void BtnNextSearch_Click(object sender, EventArgs e)
        {
            SearchManager.Find(true, false);
        }
        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            SearchManager.Find(true, true);
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (HotKeyManager.IsHotkey(e, Keys.Enter))
            {
                SearchManager.Find(true, false);
            }
            if (HotKeyManager.IsHotkey(e, Keys.Enter, true) || HotKeyManager.IsHotkey(e, Keys.Enter, false, true))
            {
                SearchManager.Find(false, false);
            }
        }

        #endregion

        #region Find & Replace Dialog

        private void OpenFindDialog()
        {

        }
        private void OpenReplaceDialog()
        {


        }

        #endregion

        #region Utils

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        public void InvokeIfNeeded(Action action)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }




        #endregion

        private void tabControl_Selecting(object sender, TabControlCancelEventArgs e)
        {
            curFileName = files.First(f => f.Key == ((TabControl)sender).SelectedTab.Text).Value;
            Text = curFileName?.fullPath ?? "" + ProgramName;
            curTextArea = ((TabControl)sender).SelectedTab.Controls[0].Controls.OfType<Scintilla>().Single();
        }
    }
}
