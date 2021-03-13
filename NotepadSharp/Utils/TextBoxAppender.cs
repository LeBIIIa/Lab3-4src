using FastColoredTextBoxNS;

using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Repository.Hierarchy;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace NotepadSharp.Utils
{
    public class TextBoxAppender : AppenderSkeleton
    {
        private readonly IReadOnlyDictionary<string, TextStyle> styles = new Dictionary<string, TextStyle>()
        {
            { SystemCategories.GeneralUIDebug.ToString(), new TextStyle(Brushes.Black, null, FontStyle.Regular) },
            { SystemCategories.GeneralUIError.ToString(), new TextStyle(Brushes.Red, null, FontStyle.Regular) },
        };

        private FastColoredTextBox _richTextBox;
        public string FormName { get; set; }
        public string TextBoxName { get; set; }
        private readonly object _lockObj = new object();

        public TextBoxAppender(FastColoredTextBox textBox)
        {
            Form frm = textBox.FindForm();
            if (frm == null)
            {
                return;
            }

            frm.FormClosing += delegate { Close(); };

            _richTextBox = textBox;
        }

        public TextBoxAppender()
        {
        }

        public static void ConfigureTextBoxAppender(FastColoredTextBox textBox)
        {
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            TextBoxAppender appender = new TextBoxAppender(textBox);
            hierarchy.Root.AddAppender(appender);
        }
        
        protected override void OnClose()
        {
            base.OnClose();
            if (_richTextBox != null)
            {
                lock (_lockObj)
                {
                    if(_richTextBox != null)
                    {
                        _richTextBox = null;
                    }
                }

            }
        }
        
        public static Control FindControlRecursive(Control container, string name)
        {
            if ((container.Name != null) && (container.Name.Equals(name)))
            {
                return container;
            }

            foreach (Control ctrl in container.Controls)
            {
                Control foundCtrl = FindControlRecursive(ctrl, name);
                if (foundCtrl != null)
                {
                    return foundCtrl;
                }
            }
            return null;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (_richTextBox == null)
            {
                if (string.IsNullOrEmpty(FormName) ||
                    string.IsNullOrEmpty(TextBoxName))
                {
                    return;
                }

                Form form = Application.OpenForms[FormName];
                if (form == null)
                {
                    return;
                }

                _richTextBox = FindControlRecursive(form, TextBoxName) as FastColoredTextBox;
                if (_richTextBox == null)
                {
                    return;
                }

                form.FormClosing += (s, e) => _richTextBox = null;
            }

            lock (_richTextBox)
            {
                // This check is required a second time because this class 
                // is executing on multiple threads.
                if (_richTextBox == null)
                {
                    return;
                }

                // Because the logging is running on a different thread than
                // the GUI, the control's "BeginInvoke" method has to be
                // leveraged in order to append the message. Otherwise, a 
                // threading exception will be thrown. 
                Action<FastColoredTextBox, string> del = new Action<FastColoredTextBox, string>((_richTextBox, s) => 
                    {
                        //some stuffs for best performance
                        _richTextBox.BeginUpdate();
                        _richTextBox.Selection.BeginUpdate();
                        //remember user selection
                        Range userSelection = _richTextBox.Selection.Clone();
                        //add text with predefined style
                        _richTextBox.TextSource.CurrentTB = _richTextBox;
                        _richTextBox.AppendText("=======================================================\n", styles[loggingEvent.Level.Name]);
                        _richTextBox.AppendText(s, styles[loggingEvent.Level.Name]);
                        _richTextBox.AppendText("=======================================================\n", styles[loggingEvent.Level.Name]);
                        //restore user selection
                        if (!userSelection.IsEmpty || userSelection.Start.iLine < _richTextBox.LinesCount - 2)
                        {
                            _richTextBox.Selection.Start = userSelection.Start;
                            _richTextBox.Selection.End = userSelection.End;
                        }
                        else
                        {
                            _richTextBox.GoEnd();//scroll to end of the text
                        }
                        //
                        _richTextBox.Selection.EndUpdate();
                        _richTextBox.EndUpdate();
                    }
                );
                _richTextBox.BeginInvoke(del, _richTextBox, loggingEvent.RenderedMessage + Environment.NewLine);
            }
        }
    }
}
