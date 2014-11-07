using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NullObject
{
    class NullProgressBar : System.Windows.Forms.ProgressBar
    {
        public new int Maximum { get { return 0; } set { } }
        public new int Minimum { get { return 0; } set { } }
        public new int Value { get { return 0; } set { } }
        public new void PerformStep() { }
    }
    public class ProgressBar
    {
        System.Windows.Forms.ProgressBar _ProgressBar = new NullProgressBar();
        static readonly ProgressBar _ProgressBarInstance = new ProgressBar();
        private ProgressBar() { }
        public static ProgressBar GetInstance() { return _ProgressBarInstance; }

        public virtual void PerformStep()
        {
            _ProgressBar.PerformStep();
            System.Windows.Forms.Application.DoEvents();
        }

        public virtual int Maximum
        {
            get { return _ProgressBar.Maximum; }
            set { _ProgressBar.Maximum = value; }
        }

        public virtual int Minimum
        {
            get { return _ProgressBar.Minimum; }
            set { _ProgressBar.Minimum = value; }
        }

        public virtual int Value
        {
            get { return _ProgressBar.Value; }
            set
            {
                if (value <= _ProgressBar.Maximum)
                    _ProgressBar.Value = value;
                else
                    _ProgressBar.Value = _ProgressBar.Maximum;
                System.Windows.Forms.Application.DoEvents();
            }
        }

        public System.Windows.Forms.ProgressBar ProgressBarObject
        {
            get { return _ProgressBar; }
            set { _ProgressBar = value; }
        }

        static public implicit operator ProgressBar(System.Windows.Forms.ProgressBar ProgressBar)
        {
            ProgressBar NullProgressBar = GetInstance();
            NullProgressBar.ProgressBarObject = ProgressBar;
            return NullProgressBar;
        }

        public static implicit operator System.Windows.Forms.ProgressBar(ProgressBar ProgressBar)
        {
            return ProgressBar._ProgressBar;
        }
    }

}
