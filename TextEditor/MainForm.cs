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
using System.Text;
using System.Text.RegularExpressions;
using ScintillaNET_FindReplaceDialog;

namespace ScintillaNET.Demo
{
    public partial class MainForm : Form
    {
        FindReplace myFindReplace;
        IncrementalSearcher incrementalSearcher;
        Scintilla curTextArea;
        FileInf curFileName;
        Dictionary<int, FileInf> files;
        public MainForm()
        {
            InitializeComponent();
            myFindReplace = new FindReplace();
            incrementalSearcher = new IncrementalSearcher(true);
            incrementalSearcher.FindReplace = myFindReplace;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

            files = new Dictionary<int, FileInf>();
            // CREATE CONTROL
            Logger.InitLogger();

        }

        private void InitTextArea(Scintilla curTextArea)
        {
            curTextArea.Dock = DockStyle.Fill;
            // INITIAL VIEW CONFIG
            curTextArea.WrapMode = wordWrapItem.Checked ? WrapMode.Word : WrapMode.None;
            curTextArea.IndentationGuides = indentGuidesItem.Checked ? IndentView.LookBoth : IndentView.None;
            curTextArea.ViewWhitespace = hiddenCharactersItem.Checked ? WhitespaceMode.VisibleAlways : WhitespaceMode.Invisible;

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
            HotKeyManager.AddHotKey(this, ShowIncrementalSearch, Keys.F, true);
            HotKeyManager.AddHotKey(this, ShowSearch, Keys.F, true, false, true);
            HotKeyManager.AddHotKey(this, ShowReplace, Keys.H, true);
            HotKeyManager.AddHotKey(this, ShowGoTo, Keys.I, true);
            HotKeyManager.AddHotKey(this, Uppercase, Keys.U, true);
            HotKeyManager.AddHotKey(this, Lowercase, Keys.L, true);
            HotKeyManager.AddHotKey(this, ShowGoTo, Keys.G, true);
            HotKeyManager.AddHotKey(this, ZoomIn, Keys.Oemplus, true);
            HotKeyManager.AddHotKey(this, ZoomOut, Keys.OemMinus, true);
            HotKeyManager.AddHotKey(this, ZoomDefault, Keys.D0, true);
            HotKeyManager.AddHotKey(this, FindNext, Keys.F3, true);
            HotKeyManager.AddHotKey(this, FindPrevious, Keys.F3, shift: true);

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

        private void AddNewDoc(string file)
        {
            FileInf finf = new FileInf();
            finf.isNew = string.IsNullOrEmpty(file);
            sttc.AddDocument(file, -1);
            finf.page = sttc.LastAddedDocument;
            finf.ID = sttc.LastAddedDocument.ID;
            InitTextArea(sttc.LastAddedDocument.Scintilla);
            curFileName = finf;
            finf.fullPath = finf.page.FileName;
            finf.filename = finf.page.FileNameNotPath;
            curTextArea = finf.page.Scintilla;
            myFindReplace.Scintilla = finf.page.Scintilla;
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

            /*var nums = curTextArea.Margins[NUMBER_MARGIN];
            nums.Width = 30;
            nums.Type = MarginType.Number;
            nums.Sensitive = true;
            nums.Mask = 0;*/
        }

        private void InitBookmarkMargin(Scintilla curTextArea)
        {

            var margin = curTextArea.Margins[BOOKMARK_MARGIN];
            //margin.Width = 20;
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

        #region file
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewDoc(string.Empty);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                AddNewDoc(openFileDialog.FileName);
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
                sttc.AddDocument(curFileName.fullPath, -1);
                curFileName.page = sttc.LastAddedDocument;
                SaveDataToFile(saveFileDialog.FileName);
            }
        }
        #endregion

        #region project
        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(curFileName.filename, ".as$"))
            {
                MessageBox.Show("Project build/run only available for '.as'/'.mc' files!");
            }
            var filename = Path.ChangeExtension(curFileName.fullPath, ".mc");
            var project = new ASol();
            List<string> lst = new List<string>
            {
                curFileName.fullPath,
                filename
            };
            Project(project, lst);
        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(curFileName.filename, ".mc$"))
            {
                MessageBox.Show("Project build/run only available for '.as'/'.mc' files!");
            }
            var filename = Path.ChangeExtension(curFileName.fullPath, ".txt");
            var project = new SSol();
            List<string> lst = new List<string>
            {
                curFileName.fullPath,
                filename
            };
            Project(project, lst);
        }

        private void compileRunToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Regex.IsMatch(curFileName.filename, ".as$"))
            {
                MessageBox.Show("Project build/run only available for '.as'/'.mc' files!");
            }
            var compile = Path.ChangeExtension(curFileName.fullPath, ".mc");
            var report = Path.ChangeExtension(curFileName.fullPath, ".txt");
            var project = new ASSol();
            List<string> lst = new List<string>
            {
                curFileName.fullPath,
                compile,
                report
            };
            Project(project, lst);
        }
        #endregion

        #region search
        private void incrementalSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowIncrementalSearch();
        }

        private void findDialogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowSearch();
        }

        private void findAndReplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowReplace();
        }
        private void goToLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowGoTo();
        }
        #endregion

        #region edit
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
        #endregion

        #region view
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

        #region Find & Replace Dialog

        private void ShowGoTo()
        {
            if (curTextArea != null)
            {
                GoTo MyGoTo = new GoTo(curTextArea);
                MyGoTo.ShowGoToDialog();
            }
        }

        private void ShowReplace()
        {
            if (curTextArea != null)
            {
                myFindReplace.ShowReplace();
            }
        }
        private void ShowSearch()
        {
            if (curTextArea != null)
            {
                myFindReplace.ShowFind();
            }
        }
        private void ShowIncrementalSearch()
        {
            if(curTextArea != null)
            {
                myFindReplace.ShowIncrementalSearch();
            }
        }
        private void FindNext()
        {
            if (myFindReplace.Window != null)
            {
                myFindReplace.Window.FindNext();
            }
        }
        private void FindPrevious()
        {
            if (myFindReplace.Window != null)
            {
                myFindReplace.Window.FindPrevious();
            }
        }

        #endregion
        
        #region Utils

        public static Color IntToColor(int rgb)
        {
            return Color.FromArgb(255, (byte)(rgb >> 16), (byte)(rgb >> 8), (byte)rgb);
        }

        #endregion

        private void Project(Sol sol, List<string> argv)
        {
            try
            {
                sol.Run(argv);
            }
            catch (MessageException me)
            {
                Logger.Log.Debug(me.Message);
            }
            catch (Exception ex)
            {
                Logger.Log.Error(ex.StackTrace + " " + ex.Message);
            }
        }

        private void sttc_TabClosing(object sender, VPKSoft.ScintillaTabbedTextControl.TabClosingEventArgsExt e)
        {
            files.Remove(e.ScintillaTabbedDocument.ID);
        }

    }
}
