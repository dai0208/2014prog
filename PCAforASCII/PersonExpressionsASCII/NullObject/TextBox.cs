using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullObject
{
    class NullTextBox : System.Windows.Forms.TextBox
    {
        public new string Text { get { return null; } set { } }
    }

    public class TextBox
    {
        static readonly TextBox _TextBoxInstance = new TextBox();
        System.Windows.Forms.TextBox _TextBox = new NullTextBox();
        private TextBox() { }
        public static TextBox GetInstance() { return _TextBoxInstance; }

        public void WriteLine(string String)
        {
            this.Write(String + System.Environment.NewLine);
        }

        public void Write(string String)
        {
            _TextBox.Text += String;
            _TextBox.Select(_TextBox.Text.Length, 0);
            _TextBox.ScrollToCaret();
            System.Windows.Forms.Application.DoEvents();
        }

        public void Clear()
        {
            _TextBox.Clear();
            System.Windows.Forms.Application.DoEvents();
        }

        public string Text
        {
            get { return _TextBox.Text; }
            set { _TextBox.Text = value; System.Windows.Forms.Application.DoEvents(); }
        }
        static public implicit operator TextBox(System.Windows.Forms.TextBox TextBox)
        {
            TextBox ReturnTextBox = GetInstance();
            ReturnTextBox._TextBox = TextBox;
            return ReturnTextBox;
        }

        public static implicit operator System.Windows.Forms.TextBox(TextBox TextBox)
        {
            return TextBox._TextBox;
        }
        public System.Windows.Forms.TextBox Textbox { get { return _TextBox; } set { _TextBox = value; } }
    }
}
