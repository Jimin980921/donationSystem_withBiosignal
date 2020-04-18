using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace coc
{
    public partial class Form6 : Form
    {
        private System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer time1 = new System.Windows.Forms.Timer();
        private int t = 0, t1 = 0;
        private int[] l = new int[4];
        private int[] l1 = new int[4];
        private int[] l2 = new int[4];
        private int[] l3 = new int[4];
        private int[] l4 = new int[4];
        public static int[] l5 = new int[16];
        private Random r = new Random(DateTime.Now.Millisecond);
		String[] ll = { "C:\\Users\\sohyeon\\Desktop\\소현\\전공\\신경공학(특별학기)\\바름조_최종\\image\\가족.png",
				"C:\\Users\\sohyeon\\Desktop\\소현\\전공\\신경공학(특별학기)\\바름조_최종\\image\\동물.png",
				"C:\\Users\\sohyeon\\Desktop\\소현\\전공\\신경공학(특별학기)\\바름조_최종\\image\\건강.png",
				"C:\\Users\\sohyeon\\Desktop\\소현\\전공\\신경공학(특별학기)\\바름조_최종\\image\\옷.png"};
		String[] lll = { "가족", "애완동물", "건강", "옷" };
        public Form6()
        {
            InitializeComponent();
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2;
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = (this.ClientSize.Height - label1.Height) / 2 - 400;
			label2.Left = (this.ClientSize.Width - label2.Width) / 2;
			label2.Top = (this.ClientSize.Height - label2.Height) / 2 + 200;
			this.Controls.Remove(label2);
			for (int i = 0; i < 4; i++)
            {
                l[i] = (int)r.Next(1, 5);
                for (int j = 0; j <= i; j++)
                    if (l[i] == l[j] && j != i)
                        i = i - 1;
                l5[i] = l[i];
            }
            time.Interval = 1000;
            time.Tick += new EventHandler(time_Tick); 
            time.Start();
			this.Controls.Add(label2);
		}
        private void time_Tick(object sender, EventArgs e)
        {
            if (t1 == 0)
            {
                if (t < 4)
                {
                    if(t==0) System.IO.File.AppendAllText(@"C:\Users\sohyeon\Desktop\불러오기.txt", "2"); //사진 측정 시작을 알림F
                    pictureBox1.ImageLocation = ll[l[t] - 1];
					label2.Text = lll[l[t] - 1];
					pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else if (t == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        l1[i] = (int)r.Next(1, 5);
                        for (int j = 0; j <= i; j++)
                            if (l1[i] == l1[j] && j != i)
                                i = i - 1;
                        l5[i+4] = l1[i];
                    }
                    t = 0;
                    t1 = 1;
                }
            }
            if (t1 == 1)
            {
                if (t < 4)
                {
                    pictureBox1.ImageLocation = ll[l1[t] - 1];
					label2.Text = lll[l1[t] - 1];
					pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else if (t == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        l2[i] = (int)r.Next(1, 5);
                        for (int j = 0; j <= i; j++)
                            if (l2[i] == l2[j] && j != i)
                                i = i - 1;
                        l5[i+8] = l2[i];
                    }
                    t = 0;
                    t1 = 2;
                }
            }
            if (t1 == 2)
            {
                if (t < 4)
                {
                    pictureBox1.ImageLocation = ll[l2[t] - 1];
					label2.Text = lll[l2[t] - 1];
					pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else if (t == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        l3[i] = (int)r.Next(1, 5);
                        for (int j = 0; j <= i; j++)
                            if (l3[i] == l3[j] && j != i)
                                i = i - 1;
                        l5[i+12] = l3[i];
                    }
                    t = 0;
                    t1 = 3;
                }
            }
            if (t1 == 3)
            {
                if (t < 4)
                {
                    pictureBox1.ImageLocation = ll[l3[t] - 1];
					label2.Text = lll[l3[t] - 1];
					pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                }
                else if (t == 4)
                {
                    System.IO.File.Delete(@"C:\Users\sohyeon\Desktop\불러오기.txt");  //파일 삭제
                    time.Stop();
                    Form7 showForm7 = new Form7();
                    showForm7.ShowDialog();
                    this.Visible = false;
                    Close();
                }
            }
            t++;
        }
    }
}
