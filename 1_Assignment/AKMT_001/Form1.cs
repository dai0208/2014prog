using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AKMT_001
{
    public partial class Form1 : Form
    {
        private bool Flag = true;
        private int Operator;
        private double tmpValue;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnInOne_Click(object sender, EventArgs e)
        {
            if (Flag)
            {
                tbxShow.Text = "1";
                Flag = false;
                return;
            }
            tbxShow.Text += '1';
        }

        private void btnInTwo_Click(object sender, EventArgs e)
        {
            if (Flag)
            {
                tbxShow.Text = "2";
                Flag = false;
                return;
            }
            tbxShow.Text += '2';
        }

        private void btnInThree_Click(object sender, EventArgs e)
        {
            if (Flag)
            {
                tbxShow.Text = "3";
                Flag = false;
                return;
            }
            tbxShow.Text += '3';
        }

        private void btnInFour_Click(object sender, EventArgs e)
        {
            if (Flag)
            {
                tbxShow.Text = "4";
                Flag = false;
                return;
            }
            tbxShow.Text += '4';
        }

        private void btnInFive_Click(object sender, EventArgs e)
        {
            if (Flag)
            {
                tbxShow.Text = "5";
                Flag = false;
                return;
            }
            tbxShow.Text += '5';
        }

        private void btnInSix_Click(object sender, EventArgs e)
        {
            if (Flag)
            {
                tbxShow.Text = "6";
                Flag = false;
                return;
            }
            tbxShow.Text += '6';
        }

        private void btnInSeven_Click(object sender, EventArgs e)
        {
            if (Flag)
            {
                tbxShow.Text = "7";
                Flag = false;
                return;
            }
            tbxShow.Text += '7';
        }

        private void btnInEight_Click(object sender, EventArgs e)
        {
            if (Flag)
            {
                tbxShow.Text = "8";
                Flag = false;
                return;
            }
            tbxShow.Text += '8';
        }

        private void btnInNine_Click(object sender, EventArgs e)
        {
            if (Flag)
            {
                tbxShow.Text = "9";
                Flag = false;
                return;
            }
            tbxShow.Text += '9';
        }

        private void btnInZero_Click(object sender, EventArgs e)
        {
            if (Flag)
            {
                tbxShow.Text = "0";
                Flag = false;
                return;
            }

            //先頭文字でなければ末尾に0を付加
            if(tbxShow.Text!="0")
                tbxShow.Text += '0';
        }

        private void btnPeriod_Click(object sender, EventArgs e)
        {
            if (Flag)
            {
                tbxShow.Text = "0.";
                Flag = false;
                return;
            }

            //ピリオドは二回続けて打点しないように
            if(!tbxShow.Text.EndsWith("."))
                tbxShow.Text += '.';
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            tbxShow.Text = "0";
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (Operator == 1)
                tbxShow.Text = (tmpValue + double.Parse(tbxShow.Text)).ToString();
            else if (Operator == 2)
                tbxShow.Text = (tmpValue - double.Parse(tbxShow.Text)).ToString();
            else if (Operator == 3)
                tbxShow.Text = (tmpValue * double.Parse(tbxShow.Text)).ToString();
            else if (Operator == 4)
                tbxShow.Text = (tmpValue / double.Parse(tbxShow.Text)).ToString();
            else { }

            Flag = true;
            tmpValue = 0;
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            BackupToMemory(1);
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            BackupToMemory(2);
        }

        private void btnMult_Click(object sender, EventArgs e)
        {
            BackupToMemory(3);
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            BackupToMemory(4);
        }

        /// <summary>
        /// 一時変数にテキストボックスの値と演算内容を退避します
        /// </summary>
        private void BackupToMemory(int OperatorNum)
        {
            tmpValue = double.Parse(tbxShow.Text);
            Operator = OperatorNum;
            Flag = true;
        }
    }
}
