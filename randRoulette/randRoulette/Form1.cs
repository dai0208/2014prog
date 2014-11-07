using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace randRoulette
{
    public partial class Form1 : Form
    {
        int count = 0;
        bool busy = false;
        List<string> AKMTMember = new List<string>
            {
                "大坂 美保子",
                "山口 春菜",
                "山田 涼子",
                "山路 裕斗",
                "川上 恵央",
                "志宮 晃樹",
                "片山 愛久美",
                "羽田 桃子",
                "玉井 冴奈",
                "大場 悠介",
                "高木 省吾",
                "飯田 峻広"
            };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (busy)
            {
                myTimer.Stop();
                busy = false;
            }
            else
            {
                Random rand = new Random();
                myTimer.Start();
                busy = true;
            }
        }

        private void myTimer_Tick(object sender, EventArgs e)
        {
            ++count;
            count = count % AKMTMember.Count;
            lblName.Text = AKMTMember[count];
        }
    }
}
