using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NormalizeManager;

namespace Normalizer
{
    public partial class Form1 : Form
    {
        Normalize Norm;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalcDivMean_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pbxBefore.Image = new Bitmap(ofd.FileName);
            }

            Norm = new Normalize(new Bitmap(pbxBefore.Image));

            tbxBfMean.Text = Normalize.calcMean(new Bitmap(pbxBefore.Image)).ToString("0.00");
            tbxBfDiv.Text = Normalize.calcDiv(new Bitmap(pbxBefore.Image)).ToString("0.00");

            nmrMean.Value = (decimal)Normalize.calcMean(new Bitmap(pbxBefore.Image));
            nmrDiv.Value = (decimal)Normalize.calcDiv(new Bitmap(pbxBefore.Image));
        }

        private void btnNormalize_Click(object sender, EventArgs e)
        {
            Norm.normalizing((double)nmrMean.Value, (double)nmrDiv.Value);
            pbxAfter.Image = Norm.getDstImage;

            tbxMean.Text = Normalize.calcMean(new Bitmap(pbxAfter.Image)).ToString("0.00");
            tbxDiv.Text = Normalize.calcDiv(new Bitmap(pbxAfter.Image)).ToString("0.00");
        }
    }
}
