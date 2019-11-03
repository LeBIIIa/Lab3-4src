using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScintillaNET.Demo.Utils
{
    public class TextBoxAppender : AppenderSkeleton
    {
        private RichTextBox _richTextBox;
        public string FormName { get; set; }
        public string TextBoxName { get; set; }
        private readonly object _lockObj = new object();

        public TextBoxAppender(RichTextBox textBox)
        {
            var frm = textBox.FindForm();
            if (frm == null)
                return;

            frm.FormClosing += delegate { Close(); };

            _richTextBox = textBox;
        }

        public TextBoxAppender()
        {
        }

        public static void ConfigureTextBoxAppender(RichTextBox textBox)
        {
            var hierarchy = (Hierarchy)LogManager.GetRepository();
            var appender = new TextBoxAppender(textBox);
            hierarchy.Root.AddAppender(appender);
        }

        public void Close()
        {
            try
            {
                // This locking is required to avoid null reference exceptions
                // in situations where DoAppend() is writing to the TextBox while
                // Close() is nulling out the TextBox.
                lock (_lockObj)
                {
                    _richTextBox = null;
                }

                var hierarchy = (Hierarchy)LogManager.GetRepository();
                hierarchy.Root.RemoveAppender(this);
            }
            catch
            {
                // There is not much that can be done here, and
                // swallowing the error is desired in my situation.
            }
        }

        public static Control FindControlRecursive(Control container, string name)
        {
            if ((container.Name != null) && (container.Name.Equals(name)))
                return container;

            foreach (Control ctrl in container.Controls)
            {
                Control foundCtrl = FindControlRecursive(ctrl, name);
                if (foundCtrl != null)
                    return foundCtrl;
            }
            return null;
        }

        protected override void Append(LoggingEvent loggingEvent)
        {
            if (_richTextBox == null)
            {
                if (string.IsNullOrEmpty(FormName) ||
                    string.IsNullOrEmpty(TextBoxName))
                    return;

                Form form = Application.OpenForms[FormName];
                if (form == null)
                    return;

                _richTextBox = FindControlRecursive(form, TextBoxName) as RichTextBox;
                if (_richTextBox == null)
                    return;

                form.FormClosing += (s, e) => _richTextBox = null;
            }

            lock (_richTextBox)
            {
                // This check is required a second time because this class 
                // is executing on multiple threads.
                if (_richTextBox == null)
                    return;

                // Because the logging is running on a different thread than
                // the GUI, the control's "BeginInvoke" method has to be
                // leveraged in order to append the message. Otherwise, a 
                // threading exception will be thrown. 
                var del = new Action<string>(s => 
                    {

                        _richTextBox.AppendText("=======================================================\n");
                        _richTextBox.AppendText(s);
                        _richTextBox.AppendText("=======================================================\n");
                    }
                );
                _richTextBox.BeginInvoke(del, loggingEvent.RenderedMessage + Environment.NewLine);
            }
        }
    }
}
