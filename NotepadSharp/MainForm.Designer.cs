
using System.ComponentModel;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace NotepadSharp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newWindowToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.saveIntervalMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.firstToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.secondToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thirdToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.stopToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.themeToolMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.backgroundThemeToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.foreColorThemeToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalThemeToolMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.lightThemeToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.darkThemeToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.hackerThemeToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.fullWindowedToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FullWindowedToolStrip = new System.Windows.Forms.ToolStripSeparator();
            this.updateWindowToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.projectToolMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.compileCodeToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.runToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compileAndRunToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Help = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolButtons = new System.Windows.Forms.ToolStrip();
            this.newButton = new System.Windows.Forms.ToolStripButton();
            this.openButton = new System.Windows.Forms.ToolStripButton();
            this.saveAs = new System.Windows.Forms.ToolStripButton();
            this.saveButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripSeparator();
            this.backButton = new System.Windows.Forms.ToolStripButton();
            this.returnButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cutButton = new System.Windows.Forms.ToolStripButton();
            this.copyButton = new System.Windows.Forms.ToolStripButton();
            this.selectAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.zoomIn = new System.Windows.Forms.ToolStripButton();
            this.zoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.closeTabPageButton = new System.Windows.Forms.ToolStripButton();
            this.colorOption = new System.Windows.Forms.ColorDialog();
            this.contextOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabOption = new System.Windows.Forms.TabControl();
            this.contextTabMenuOptions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteTabMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.fileStatusStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.fileNameStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLabelStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.symbolsCountStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.rowsInfoStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.rowsValueStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.nameAppLabelStrip = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerInterval = new System.Windows.Forms.Timer(this.components);
            this.documentMap = new FastColoredTextBoxNS.DocumentMap();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.txtInfo = new FastColoredTextBoxNS.FastColoredTextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.mainMenu.SuspendLayout();
            this.toolButtons.SuspendLayout();
            this.contextOptions.SuspendLayout();
            this.contextTabMenuOptions.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtInfo)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFile
            // 
            this.openFile.FileName = "openFile";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Assembler code|*.as|Compile code|*.mc|All files|*.*";
            // 
            // mainMenu
            // 
            this.mainMenu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.mainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolMenu,
            this.editToolMenu,
            this.themeToolMenu,
            this.projectToolMenu,
            this.Help});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1064, 31);
            this.mainMenu.TabIndex = 1;
            this.mainMenu.Text = "Main Menu";
            // 
            // fileToolMenu
            // 
            this.fileToolMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolMenuItem,
            this.newWindowToolMenuItem,
            this.openToolMenuItem,
            this.saveAsToolMenuItem,
            this.saveToolMenuItem,
            this.toolStripSeparator6,
            this.saveIntervalMenuItem,
            this.toolStripMenuItem1,
            this.exitToolMenuItem});
            this.fileToolMenu.Name = "fileToolMenu";
            this.fileToolMenu.Size = new System.Drawing.Size(49, 27);
            this.fileToolMenu.Text = "File";
            // 
            // newToolMenuItem
            // 
            this.newToolMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolMenuItem.Image")));
            this.newToolMenuItem.Name = "newToolMenuItem";
            this.newToolMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newToolMenuItem.Size = new System.Drawing.Size(266, 28);
            this.newToolMenuItem.Text = "New";
            this.newToolMenuItem.Click += new System.EventHandler(this.NewFile_Click);
            // 
            // newWindowToolMenuItem
            // 
            this.newWindowToolMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newWindowToolMenuItem.Image")));
            this.newWindowToolMenuItem.Name = "newWindowToolMenuItem";
            this.newWindowToolMenuItem.Size = new System.Drawing.Size(266, 28);
            this.newWindowToolMenuItem.Text = "In a new window...";
            this.newWindowToolMenuItem.Click += new System.EventHandler(this.NewDeveloperWindow_Click);
            // 
            // openToolMenuItem
            // 
            this.openToolMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openToolMenuItem.Image")));
            this.openToolMenuItem.Name = "openToolMenuItem";
            this.openToolMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openToolMenuItem.Size = new System.Drawing.Size(266, 28);
            this.openToolMenuItem.Text = "Open...";
            this.openToolMenuItem.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // saveAsToolMenuItem
            // 
            this.saveAsToolMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveAsToolMenuItem.Image")));
            this.saveAsToolMenuItem.Name = "saveAsToolMenuItem";
            this.saveAsToolMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveAsToolMenuItem.Size = new System.Drawing.Size(266, 28);
            this.saveAsToolMenuItem.Text = "Save as...";
            this.saveAsToolMenuItem.Click += new System.EventHandler(this.SaveAsFile_Click);
            // 
            // saveToolMenuItem
            // 
            this.saveToolMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveToolMenuItem.Image")));
            this.saveToolMenuItem.Name = "saveToolMenuItem";
            this.saveToolMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveToolMenuItem.Size = new System.Drawing.Size(266, 28);
            this.saveToolMenuItem.Text = "Save";
            this.saveToolMenuItem.Click += new System.EventHandler(this.SaveFile_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(263, 6);
            // 
            // saveIntervalMenuItem
            // 
            this.saveIntervalMenuItem.AccessibleDescription = "";
            this.saveIntervalMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.firstToolMenuItem,
            this.secondToolMenuItem,
            this.thirdToolMenuItem,
            this.toolStripSeparator7,
            this.stopToolMenuItem});
            this.saveIntervalMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveIntervalMenuItem.Image")));
            this.saveIntervalMenuItem.Name = "saveIntervalMenuItem";
            this.saveIntervalMenuItem.ShortcutKeyDisplayString = "OFF";
            this.saveIntervalMenuItem.Size = new System.Drawing.Size(266, 28);
            this.saveIntervalMenuItem.Text = "Autosaving...";
            // 
            // firstToolMenuItem
            // 
            this.firstToolMenuItem.Name = "firstToolMenuItem";
            this.firstToolMenuItem.Size = new System.Drawing.Size(178, 28);
            this.firstToolMenuItem.Text = "5 minutes";
            this.firstToolMenuItem.Click += new System.EventHandler(this.FirstToolStripMenuItem_Click);
            // 
            // secondToolMenuItem
            // 
            this.secondToolMenuItem.Name = "secondToolMenuItem";
            this.secondToolMenuItem.Size = new System.Drawing.Size(178, 28);
            this.secondToolMenuItem.Text = "10 minutes";
            this.secondToolMenuItem.Click += new System.EventHandler(this.SecondToolStripMenuItem_Click);
            // 
            // thirdToolMenuItem
            // 
            this.thirdToolMenuItem.Name = "thirdToolMenuItem";
            this.thirdToolMenuItem.Size = new System.Drawing.Size(178, 28);
            this.thirdToolMenuItem.Text = "20 minutes";
            this.thirdToolMenuItem.Click += new System.EventHandler(this.ThirdToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(175, 6);
            // 
            // stopToolMenuItem
            // 
            this.stopToolMenuItem.Name = "stopToolMenuItem";
            this.stopToolMenuItem.Size = new System.Drawing.Size(178, 28);
            this.stopToolMenuItem.Text = "OFF";
            this.stopToolMenuItem.Click += new System.EventHandler(this.StopToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(263, 6);
            // 
            // exitToolMenuItem
            // 
            this.exitToolMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("exitToolMenuItem.Image")));
            this.exitToolMenuItem.Name = "exitToolMenuItem";
            this.exitToolMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.exitToolMenuItem.Size = new System.Drawing.Size(266, 28);
            this.exitToolMenuItem.Text = "Close";
            this.exitToolMenuItem.Click += new System.EventHandler(this.ExitTool_Click);
            // 
            // editToolMenu
            // 
            this.editToolMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolMenuItem,
            this.pasteToolMenuItem,
            this.cutToolMenuItem,
            this.toolStripSeparator8,
            this.selectAllToolMenuItem,
            this.toolStripSeparator9,
            this.undoToolMenuItem,
            this.redoToolMenuItem,
            this.toolStripMenuItem2,
            this.deleteToolMenuItem});
            this.editToolMenu.Name = "editToolMenu";
            this.editToolMenu.Size = new System.Drawing.Size(53, 27);
            this.editToolMenu.Text = "Edit";
            // 
            // copyToolMenuItem
            // 
            this.copyToolMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyToolMenuItem.Image")));
            this.copyToolMenuItem.Name = "copyToolMenuItem";
            this.copyToolMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.copyToolMenuItem.Size = new System.Drawing.Size(241, 28);
            this.copyToolMenuItem.Text = "Copy";
            this.copyToolMenuItem.Click += new System.EventHandler(this.CopyText);
            // 
            // pasteToolMenuItem
            // 
            this.pasteToolMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteToolMenuItem.Image")));
            this.pasteToolMenuItem.Name = "pasteToolMenuItem";
            this.pasteToolMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.pasteToolMenuItem.Size = new System.Drawing.Size(241, 28);
            this.pasteToolMenuItem.Text = "Paste";
            this.pasteToolMenuItem.Click += new System.EventHandler(this.PasteText);
            // 
            // cutToolMenuItem
            // 
            this.cutToolMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutToolMenuItem.Image")));
            this.cutToolMenuItem.Name = "cutToolMenuItem";
            this.cutToolMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.cutToolMenuItem.Size = new System.Drawing.Size(241, 28);
            this.cutToolMenuItem.Text = "Cut";
            this.cutToolMenuItem.Click += new System.EventHandler(this.CutText);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(238, 6);
            // 
            // selectAllToolMenuItem
            // 
            this.selectAllToolMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("selectAllToolMenuItem.Image")));
            this.selectAllToolMenuItem.Name = "selectAllToolMenuItem";
            this.selectAllToolMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolMenuItem.Size = new System.Drawing.Size(241, 28);
            this.selectAllToolMenuItem.Text = "Select All";
            this.selectAllToolMenuItem.Click += new System.EventHandler(this.SelectMenu_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(238, 6);
            // 
            // undoToolMenuItem
            // 
            this.undoToolMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoToolMenuItem.Image")));
            this.undoToolMenuItem.Name = "undoToolMenuItem";
            this.undoToolMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Z)));
            this.undoToolMenuItem.Size = new System.Drawing.Size(241, 28);
            this.undoToolMenuItem.Text = "Undo";
            this.undoToolMenuItem.Click += new System.EventHandler(this.UndoMenu_Click);
            // 
            // redoToolMenuItem
            // 
            this.redoToolMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoToolMenuItem.Image")));
            this.redoToolMenuItem.Name = "redoToolMenuItem";
            this.redoToolMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.redoToolMenuItem.Size = new System.Drawing.Size(241, 28);
            this.redoToolMenuItem.Text = "Redo";
            this.redoToolMenuItem.Click += new System.EventHandler(this.RedoMenu_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(238, 6);
            // 
            // deleteToolMenuItem
            // 
            this.deleteToolMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolMenuItem.Image")));
            this.deleteToolMenuItem.Name = "deleteToolMenuItem";
            this.deleteToolMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.deleteToolMenuItem.Size = new System.Drawing.Size(241, 28);
            this.deleteToolMenuItem.Text = "Delete";
            this.deleteToolMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenu_Click_1);
            // 
            // themeToolMenu
            // 
            this.themeToolMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backgroundThemeToolMenuItem,
            this.foreColorThemeToolMenuItem,
            this.generalThemeToolMenu,
            this.toolStripMenuItem8,
            this.fullWindowedToolMenuItem,
            this.FullWindowedToolStrip,
            this.updateWindowToolMenuItem});
            this.themeToolMenu.Name = "themeToolMenu";
            this.themeToolMenu.Size = new System.Drawing.Size(85, 27);
            this.themeToolMenu.Text = "Settings";
            // 
            // backgroundThemeToolMenuItem
            // 
            this.backgroundThemeToolMenuItem.Image = global::NotepadSharp.Properties.Resources.AzureReservedIPAddress_color_16x;
            this.backgroundThemeToolMenuItem.Name = "backgroundThemeToolMenuItem";
            this.backgroundThemeToolMenuItem.Size = new System.Drawing.Size(226, 28);
            this.backgroundThemeToolMenuItem.Text = "Background";
            this.backgroundThemeToolMenuItem.Click += new System.EventHandler(this.BackgroundTextTheme);
            // 
            // foreColorThemeToolMenuItem
            // 
            this.foreColorThemeToolMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("foreColorThemeToolMenuItem.Image")));
            this.foreColorThemeToolMenuItem.Name = "foreColorThemeToolMenuItem";
            this.foreColorThemeToolMenuItem.Size = new System.Drawing.Size(226, 28);
            this.foreColorThemeToolMenuItem.Text = "Foreground color";
            this.foreColorThemeToolMenuItem.Click += new System.EventHandler(this.ForeColorTheme_Click);
            // 
            // generalThemeToolMenu
            // 
            this.generalThemeToolMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lightThemeToolMenuItem,
            this.darkThemeToolMenuItem,
            this.toolStripMenuItem4,
            this.hackerThemeToolMenuItem});
            this.generalThemeToolMenu.Image = ((System.Drawing.Image)(resources.GetObject("generalThemeToolMenu.Image")));
            this.generalThemeToolMenu.Name = "generalThemeToolMenu";
            this.generalThemeToolMenu.Size = new System.Drawing.Size(226, 28);
            this.generalThemeToolMenu.Text = "Theme";
            // 
            // lightThemeToolMenuItem
            // 
            this.lightThemeToolMenuItem.Name = "lightThemeToolMenuItem";
            this.lightThemeToolMenuItem.Size = new System.Drawing.Size(147, 28);
            this.lightThemeToolMenuItem.Text = "Light";
            this.lightThemeToolMenuItem.Click += new System.EventHandler(this.LightTheme_Click);
            // 
            // darkThemeToolMenuItem
            // 
            this.darkThemeToolMenuItem.Name = "darkThemeToolMenuItem";
            this.darkThemeToolMenuItem.Size = new System.Drawing.Size(147, 28);
            this.darkThemeToolMenuItem.Text = "Dark";
            this.darkThemeToolMenuItem.Click += new System.EventHandler(this.DarkTheme_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(144, 6);
            // 
            // hackerThemeToolMenuItem
            // 
            this.hackerThemeToolMenuItem.Name = "hackerThemeToolMenuItem";
            this.hackerThemeToolMenuItem.Size = new System.Drawing.Size(147, 28);
            this.hackerThemeToolMenuItem.Text = "Special";
            this.hackerThemeToolMenuItem.Click += new System.EventHandler(this.HackerTheme_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(223, 6);
            // 
            // fullWindowedToolMenuItem
            // 
            this.fullWindowedToolMenuItem.Image = global::NotepadSharp.Properties.Resources.WindowsService_16x;
            this.fullWindowedToolMenuItem.Name = "fullWindowedToolMenuItem";
            this.fullWindowedToolMenuItem.Size = new System.Drawing.Size(226, 28);
            this.fullWindowedToolMenuItem.Text = "Fullscreen";
            this.fullWindowedToolMenuItem.Click += new System.EventHandler(this.FullWindowedToolStripMenuItem_Click);
            // 
            // FullWindowedToolStrip
            // 
            this.FullWindowedToolStrip.Name = "FullWindowedToolStrip";
            this.FullWindowedToolStrip.Size = new System.Drawing.Size(223, 6);
            // 
            // updateWindowToolMenuItem
            // 
            this.updateWindowToolMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("updateWindowToolMenuItem.Image")));
            this.updateWindowToolMenuItem.Name = "updateWindowToolMenuItem";
            this.updateWindowToolMenuItem.Size = new System.Drawing.Size(226, 28);
            this.updateWindowToolMenuItem.Text = "Update tab";
            this.updateWindowToolMenuItem.Click += new System.EventHandler(this.UpdateWindowToolStripMenuItem_Click);
            // 
            // projectToolMenu
            // 
            this.projectToolMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.compileCodeToolMenuItem,
            this.runToolMenuItem,
            this.compileAndRunToolMenuItem});
            this.projectToolMenu.Name = "projectToolMenu";
            this.projectToolMenu.Size = new System.Drawing.Size(77, 27);
            this.projectToolMenu.Text = "Project";
            // 
            // compileCodeToolMenuItem
            // 
            this.compileCodeToolMenuItem.Name = "compileCodeToolMenuItem";
            this.compileCodeToolMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.compileCodeToolMenuItem.Size = new System.Drawing.Size(253, 28);
            this.compileCodeToolMenuItem.Text = "Compile";
            this.compileCodeToolMenuItem.Click += new System.EventHandler(this.CompileToolMenuItem_Click);
            // 
            // runToolMenuItem
            // 
            this.runToolMenuItem.Name = "runToolMenuItem";
            this.runToolMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.runToolMenuItem.Size = new System.Drawing.Size(253, 28);
            this.runToolMenuItem.Text = "Run";
            this.runToolMenuItem.Click += new System.EventHandler(this.RunToolMenuItem_Click);
            // 
            // compileAndRunToolMenuItem
            // 
            this.compileAndRunToolMenuItem.Name = "compileAndRunToolMenuItem";
            this.compileAndRunToolMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.compileAndRunToolMenuItem.Size = new System.Drawing.Size(253, 28);
            this.compileAndRunToolMenuItem.Text = "Compile and Run";
            this.compileAndRunToolMenuItem.Click += new System.EventHandler(this.CompileRunToolMenuItem_Click);
            // 
            // Help
            // 
            this.Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolMenuItem});
            this.Help.Name = "Help";
            this.Help.Size = new System.Drawing.Size(32, 27);
            this.Help.Text = "?";
            // 
            // aboutToolMenuItem
            // 
            this.aboutToolMenuItem.Name = "aboutToolMenuItem";
            this.aboutToolMenuItem.Size = new System.Drawing.Size(141, 28);
            this.aboutToolMenuItem.Text = "About";
            this.aboutToolMenuItem.Click += new System.EventHandler(this.AboutPanel_Click);
            // 
            // toolButtons
            // 
            this.toolButtons.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolButtons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newButton,
            this.openButton,
            this.saveAs,
            this.saveButton,
            this.toolStripButton5,
            this.backButton,
            this.returnButton,
            this.toolStripSeparator1,
            this.cutButton,
            this.copyButton,
            this.selectAll,
            this.toolStripSeparator2,
            this.zoomIn,
            this.zoomOut,
            this.toolStripSeparator3,
            this.toolStripSeparator4,
            this.closeTabPageButton});
            this.toolButtons.Location = new System.Drawing.Point(0, 31);
            this.toolButtons.Name = "toolButtons";
            this.toolButtons.Size = new System.Drawing.Size(1064, 27);
            this.toolButtons.TabIndex = 2;
            this.toolButtons.Text = "toolStrip1";
            // 
            // newButton
            // 
            this.newButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newButton.Image = ((System.Drawing.Image)(resources.GetObject("newButton.Image")));
            this.newButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newButton.Name = "newButton";
            this.newButton.Size = new System.Drawing.Size(29, 24);
            this.newButton.Text = "New";
            this.newButton.ToolTipText = "New";
            this.newButton.Click += new System.EventHandler(this.NewFile_Button);
            // 
            // openButton
            // 
            this.openButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openButton.Image = ((System.Drawing.Image)(resources.GetObject("openButton.Image")));
            this.openButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size(29, 24);
            this.openButton.Text = "Open";
            this.openButton.Click += new System.EventHandler(this.OpenFile_Button);
            // 
            // saveAs
            // 
            this.saveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveAs.Image = ((System.Drawing.Image)(resources.GetObject("saveAs.Image")));
            this.saveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveAs.Name = "saveAs";
            this.saveAs.Size = new System.Drawing.Size(29, 24);
            this.saveAs.Text = "Save As";
            this.saveAs.Click += new System.EventHandler(this.SaveAs_Button);
            // 
            // saveButton
            // 
            this.saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.saveButton.Image = ((System.Drawing.Image)(resources.GetObject("saveButton.Image")));
            this.saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(29, 24);
            this.saveButton.Text = "Save";
            this.saveButton.ToolTipText = "Save";
            this.saveButton.Click += new System.EventHandler(this.SaveFileButton_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(6, 27);
            // 
            // backButton
            // 
            this.backButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.backButton.Image = ((System.Drawing.Image)(resources.GetObject("backButton.Image")));
            this.backButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(29, 24);
            this.backButton.Text = "Back";
            this.backButton.Click += new System.EventHandler(this.BackButton_Click);
            // 
            // returnButton
            // 
            this.returnButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.returnButton.Image = ((System.Drawing.Image)(resources.GetObject("returnButton.Image")));
            this.returnButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(29, 24);
            this.returnButton.Text = "Return";
            this.returnButton.Click += new System.EventHandler(this.ReturnButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // cutButton
            // 
            this.cutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutButton.Image = ((System.Drawing.Image)(resources.GetObject("cutButton.Image")));
            this.cutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutButton.Name = "cutButton";
            this.cutButton.Size = new System.Drawing.Size(29, 24);
            this.cutButton.Text = "Cut";
            this.cutButton.Click += new System.EventHandler(this.CutButton_Click);
            // 
            // copyButton
            // 
            this.copyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.copyButton.Image = ((System.Drawing.Image)(resources.GetObject("copyButton.Image")));
            this.copyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(29, 24);
            this.copyButton.Text = "Copy";
            this.copyButton.Click += new System.EventHandler(this.CopyButton_Click);
            // 
            // selectAll
            // 
            this.selectAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.selectAll.Image = ((System.Drawing.Image)(resources.GetObject("selectAll.Image")));
            this.selectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.selectAll.Name = "selectAll";
            this.selectAll.Size = new System.Drawing.Size(29, 24);
            this.selectAll.Text = "Select All";
            this.selectAll.Click += new System.EventHandler(this.SelectAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // zoomIn
            // 
            this.zoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomIn.Image = ((System.Drawing.Image)(resources.GetObject("zoomIn.Image")));
            this.zoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomIn.Name = "zoomIn";
            this.zoomIn.Size = new System.Drawing.Size(29, 24);
            this.zoomIn.Text = "Zoom in";
            this.zoomIn.Click += new System.EventHandler(this.ZoomIn_Click);
            // 
            // zoomOut
            // 
            this.zoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOut.Image = ((System.Drawing.Image)(resources.GetObject("zoomOut.Image")));
            this.zoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOut.Name = "zoomOut";
            this.zoomOut.Size = new System.Drawing.Size(29, 24);
            this.zoomOut.Text = "Zoom Out";
            this.zoomOut.Click += new System.EventHandler(this.ZoomOut_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 27);
            // 
            // closeTabPageButton
            // 
            this.closeTabPageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.closeTabPageButton.Image = global::NotepadSharp.Properties.Resources.CloseDocumentGroup_16x_32;
            this.closeTabPageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.closeTabPageButton.Name = "closeTabPageButton";
            this.closeTabPageButton.Size = new System.Drawing.Size(29, 24);
            this.closeTabPageButton.Text = "Закрыть";
            this.closeTabPageButton.Click += new System.EventHandler(this.CloseTabPageButton_Click);
            // 
            // colorOption
            // 
            this.colorOption.AnyColor = true;
            // 
            // contextOptions
            // 
            this.contextOptions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyContextMenuItem,
            this.pasteContextMenuItem,
            this.selectAllContextMenuItem,
            this.selectContextMenuItem,
            this.cutContextMenuItem,
            this.undoContextMenuItem,
            this.redoContextMenuItem,
            this.toolStripSeparator5,
            this.deleteContextMenuItem});
            this.contextOptions.Name = "contextMenuStrip1";
            this.contextOptions.Size = new System.Drawing.Size(145, 218);
            // 
            // copyContextMenuItem
            // 
            this.copyContextMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("copyContextMenuItem.Image")));
            this.copyContextMenuItem.Name = "copyContextMenuItem";
            this.copyContextMenuItem.Size = new System.Drawing.Size(144, 26);
            this.copyContextMenuItem.Text = "Copy";
            this.copyContextMenuItem.Click += new System.EventHandler(this.CopyMenu_Click);
            // 
            // pasteContextMenuItem
            // 
            this.pasteContextMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("pasteContextMenuItem.Image")));
            this.pasteContextMenuItem.Name = "pasteContextMenuItem";
            this.pasteContextMenuItem.Size = new System.Drawing.Size(144, 26);
            this.pasteContextMenuItem.Text = "Paste";
            this.pasteContextMenuItem.Click += new System.EventHandler(this.PasteMenu_Click);
            // 
            // selectAllContextMenuItem
            // 
            this.selectAllContextMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("selectAllContextMenuItem.Image")));
            this.selectAllContextMenuItem.Name = "selectAllContextMenuItem";
            this.selectAllContextMenuItem.Size = new System.Drawing.Size(144, 26);
            this.selectAllContextMenuItem.Text = "Select All";
            this.selectAllContextMenuItem.Click += new System.EventHandler(this.SelectAllMenu_Click);
            // 
            // selectContextMenuItem
            // 
            this.selectContextMenuItem.Name = "selectContextMenuItem";
            this.selectContextMenuItem.Size = new System.Drawing.Size(144, 26);
            this.selectContextMenuItem.Text = "Select";
            this.selectContextMenuItem.Click += new System.EventHandler(this.SelectContextMenu_Click);
            // 
            // cutContextMenuItem
            // 
            this.cutContextMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("cutContextMenuItem.Image")));
            this.cutContextMenuItem.Name = "cutContextMenuItem";
            this.cutContextMenuItem.Size = new System.Drawing.Size(144, 26);
            this.cutContextMenuItem.Text = "Cut";
            this.cutContextMenuItem.Click += new System.EventHandler(this.CutContextMenu_Click);
            // 
            // undoContextMenuItem
            // 
            this.undoContextMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("undoContextMenuItem.Image")));
            this.undoContextMenuItem.Name = "undoContextMenuItem";
            this.undoContextMenuItem.Size = new System.Drawing.Size(144, 26);
            this.undoContextMenuItem.Text = "Undo";
            this.undoContextMenuItem.Click += new System.EventHandler(this.UndoContextMenu_Click);
            // 
            // redoContextMenuItem
            // 
            this.redoContextMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("redoContextMenuItem.Image")));
            this.redoContextMenuItem.Name = "redoContextMenuItem";
            this.redoContextMenuItem.Size = new System.Drawing.Size(144, 26);
            this.redoContextMenuItem.Text = "Redo";
            this.redoContextMenuItem.Click += new System.EventHandler(this.RedoContextMenu_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(141, 6);
            // 
            // deleteContextMenuItem
            // 
            this.deleteContextMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteContextMenuItem.Image")));
            this.deleteContextMenuItem.Name = "deleteContextMenuItem";
            this.deleteContextMenuItem.Size = new System.Drawing.Size(144, 26);
            this.deleteContextMenuItem.Text = "Delete";
            this.deleteContextMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenu_Click);
            // 
            // tabOption
            // 
            this.tabOption.ContextMenuStrip = this.contextTabMenuOptions;
            this.tabOption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabOption.Location = new System.Drawing.Point(4, 4);
            this.tabOption.Margin = new System.Windows.Forms.Padding(4);
            this.tabOption.Name = "tabOption";
            this.tabOption.SelectedIndex = 0;
            this.tabOption.Size = new System.Drawing.Size(891, 338);
            this.tabOption.TabIndex = 5;
            this.tabOption.SelectedIndexChanged += new System.EventHandler(this.TabOption_SelectedIndexChanged);
            // 
            // contextTabMenuOptions
            // 
            this.contextTabMenuOptions.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextTabMenuOptions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteTabMenuItem});
            this.contextTabMenuOptions.Name = "contextTabMenuStrip";
            this.contextTabMenuOptions.Size = new System.Drawing.Size(123, 28);
            // 
            // deleteTabMenuItem
            // 
            this.deleteTabMenuItem.Name = "deleteTabMenuItem";
            this.deleteTabMenuItem.Size = new System.Drawing.Size(122, 24);
            this.deleteTabMenuItem.Text = "Delete";
            this.deleteTabMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileStatusStrip,
            this.fileNameStrip,
            this.statusLabelStrip,
            this.symbolsCountStrip,
            this.rowsInfoStrip,
            this.rowsValueStrip,
            this.nameAppLabelStrip});
            this.statusStrip.Location = new System.Drawing.Point(0, 528);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip.Size = new System.Drawing.Size(1064, 26);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // fileStatusStrip
            // 
            this.fileStatusStrip.Name = "fileStatusStrip";
            this.fileStatusStrip.Size = new System.Drawing.Size(39, 20);
            this.fileStatusStrip.Text = "File: ";
            // 
            // fileNameStrip
            // 
            this.fileNameStrip.Name = "fileNameStrip";
            this.fileNameStrip.Size = new System.Drawing.Size(173, 20);
            this.fileNameStrip.Text = "                                         ";
            // 
            // statusLabelStrip
            // 
            this.statusLabelStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.statusLabelStrip.Name = "statusLabelStrip";
            this.statusLabelStrip.Size = new System.Drawing.Size(72, 20);
            this.statusLabelStrip.Text = "Symbols: ";
            // 
            // symbolsCountStrip
            // 
            this.symbolsCountStrip.Name = "symbolsCountStrip";
            this.symbolsCountStrip.Size = new System.Drawing.Size(85, 20);
            this.symbolsCountStrip.Text = "                   ";
            // 
            // rowsInfoStrip
            // 
            this.rowsInfoStrip.Name = "rowsInfoStrip";
            this.rowsInfoStrip.Size = new System.Drawing.Size(47, 20);
            this.rowsInfoStrip.Text = "Rows:";
            // 
            // rowsValueStrip
            // 
            this.rowsValueStrip.Name = "rowsValueStrip";
            this.rowsValueStrip.Size = new System.Drawing.Size(89, 20);
            this.rowsValueStrip.Text = "                    ";
            // 
            // nameAppLabelStrip
            // 
            this.nameAppLabelStrip.Name = "nameAppLabelStrip";
            this.nameAppLabelStrip.Size = new System.Drawing.Size(106, 20);
            this.nameAppLabelStrip.Text = "NotepadSharp";
            // 
            // documentMap
            // 
            this.documentMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentMap.ForeColor = System.Drawing.Color.Maroon;
            this.documentMap.Location = new System.Drawing.Point(903, 4);
            this.documentMap.Margin = new System.Windows.Forms.Padding(4);
            this.documentMap.Name = "documentMap";
            this.documentMap.Size = new System.Drawing.Size(151, 338);
            this.documentMap.TabIndex = 2;
            this.documentMap.Target = null;
            this.documentMap.Text = "documentMap";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.txtInfo, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 58);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1064, 470);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // txtInfo
            // 
            this.txtInfo.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.txtInfo.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\n^\\s*(case|default)\\s*[^:]*(" +
    "?<range>:)\\s*(?<range>[^;]+);";
            this.txtInfo.AutoScrollMinSize = new System.Drawing.Size(29, 19);
            this.txtInfo.BackBrush = null;
            this.txtInfo.CharHeight = 19;
            this.txtInfo.CharWidth = 9;
            this.txtInfo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtInfo.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.txtInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInfo.Font = new System.Drawing.Font("Consolas", 9.75F);
            this.txtInfo.IsReplaceMode = false;
            this.txtInfo.Location = new System.Drawing.Point(4, 356);
            this.txtInfo.Margin = new System.Windows.Forms.Padding(4);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Paddings = new System.Windows.Forms.Padding(0);
            this.txtInfo.ReadOnly = true;
            this.txtInfo.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.txtInfo.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("txtInfo.ServiceColors")));
            this.txtInfo.Size = new System.Drawing.Size(1056, 110);
            this.txtInfo.TabIndex = 12;
            this.txtInfo.Zoom = 100;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 85F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 15F));
            this.tableLayoutPanel2.Controls.Add(this.documentMap, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tabOption, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1058, 346);
            this.tableLayoutPanel2.TabIndex = 11;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 554);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolButtons);
            this.Controls.Add(this.mainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.Text = "NotepadSharp";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DeveloperEditor_FormClosing);
            this.Load += new System.EventHandler(this.DeveloperEditor_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.toolButtons.ResumeLayout(false);
            this.toolButtons.PerformLayout();
            this.contextOptions.ResumeLayout(false);
            this.contextTabMenuOptions.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtInfo)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private OpenFileDialog openFile;
        private SaveFileDialog saveFileDialog;
        private MenuStrip mainMenu;
        private ToolStripMenuItem fileToolMenu;
        private ToolStripMenuItem newToolMenuItem;
        private ToolStripMenuItem openToolMenuItem;
        private ToolStripMenuItem saveAsToolMenuItem;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem exitToolMenuItem;
        private ToolStrip toolButtons;
        private ToolStripButton newButton;
        private ToolStripButton saveAs;
        private ToolStripMenuItem editToolMenu;
        private ToolStripMenuItem copyToolMenuItem;
        private ToolStripMenuItem pasteToolMenuItem;
        private ToolStripMenuItem cutToolMenuItem;
        private ToolStripMenuItem themeToolMenu;
        private ToolStripMenuItem backgroundThemeToolMenuItem;
        private ToolStripMenuItem generalThemeToolMenu;
        private ColorDialog colorOption;
        private ToolStripMenuItem selectAllToolMenuItem;
        private ContextMenuStrip contextOptions;
        private ToolStripMenuItem copyContextMenuItem;
        private ToolStripMenuItem pasteContextMenuItem;
        private ToolStripMenuItem selectAllContextMenuItem;
        private ToolStripMenuItem saveToolMenuItem;
        private ToolStripButton saveButton;
        private ToolStripButton openButton;
        private ToolStripMenuItem projectToolMenu;
        private ToolStripMenuItem compileCodeToolMenuItem;
        private ToolStripMenuItem darkThemeToolMenuItem;
        private ToolStripMenuItem hackerThemeToolMenuItem;
        private ToolStripSeparator toolStripMenuItem4;
        private ToolStripMenuItem lightThemeToolMenuItem;
        private TabControl tabOption;
        private ToolStripSeparator toolStripButton5;
        private ToolStripButton backButton;
        private ToolStripButton returnButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton cutButton;
        private ToolStripButton copyButton;
        private ToolStripButton selectAll;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton zoomIn;
        private ToolStripButton zoomOut;
        private ToolStripMenuItem Help;
        private ToolStripMenuItem aboutToolMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel statusLabelStrip;
        private ToolStripStatusLabel rowsInfoStrip;
        private ToolStripStatusLabel nameAppLabelStrip;
        private ToolStripMenuItem foreColorThemeToolMenuItem;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripStatusLabel symbolsCountStrip;
        private ToolStripStatusLabel rowsValueStrip;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem undoToolMenuItem;
        private ToolStripMenuItem redoToolMenuItem;
        private ContextMenuStrip contextTabMenuOptions;
        private ToolStripMenuItem deleteTabMenuItem;
        private ToolStripSeparator toolStripMenuItem8;
        private ToolStripMenuItem updateWindowToolMenuItem;
        private ToolStripMenuItem selectContextMenuItem;
        private ToolStripMenuItem cutContextMenuItem;
        private ToolStripMenuItem redoContextMenuItem;
        private ToolStripMenuItem undoContextMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem deleteContextMenuItem;
        private ToolStripSeparator FullWindowedToolStrip;
        private ToolStripMenuItem fullWindowedToolMenuItem;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem saveIntervalMenuItem;
        private ToolStripMenuItem firstToolMenuItem;
        private ToolStripMenuItem secondToolMenuItem;
        private ToolStripMenuItem thirdToolMenuItem;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem stopToolMenuItem;
        private Timer timerInterval;
        private ToolStripStatusLabel fileStatusStrip;
        private ToolStripStatusLabel fileNameStrip;
        private ToolStripButton closeTabPageButton;
        private ToolStripMenuItem deleteToolMenuItem;
        private ToolStripMenuItem newWindowToolMenuItem;
        private DocumentMap documentMap;
        private ToolStripMenuItem runToolMenuItem;
        private ToolStripMenuItem compileAndRunToolMenuItem;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripSeparator toolStripSeparator9;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private FastColoredTextBox txtInfo;
    }
}

