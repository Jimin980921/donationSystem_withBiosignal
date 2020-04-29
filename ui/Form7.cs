using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace coc
{
    public partial class Form7 : Form
    {
        private int t = 0;
        private string[] ss;
        private System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
		String[] ll = { "C:\\Users\\sohyeon\\Desktop\\소현\\전공\\신경공학(특별학기)\\바름조_최종\\image\\가족.png",
				"C:\\Users\\sohyeon\\Desktop\\소현\\전공\\신경공학(특별학기)\\바름조_최종\\image\\동물.png",
				"C:\\Users\\sohyeon\\Desktop\\소현\\전공\\신경공학(특별학기)\\바름조_최종\\image\\건강.png",
				"C:\\Users\\sohyeon\\Desktop\\소현\\전공\\신경공학(특별학기)\\바름조_최종\\image\\옷.png"};
		private int[] l = new int[16];
        public static int be = 0; //몇번 사진인지
        public Form7()
        {
            InitializeComponent();
			//Form6 f6 = new Form6();
			//int[] f = f6.l5;
			int[] f = Form6.l5;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Controls.Remove(label1);//10초후
            this.Controls.Remove(label2);//뇌가 선택한
            this.Controls.Remove(pictureBox1);//선택사진
            while (true)
            {
                if (System.IO.File.Exists(@"C:\Users\sohyeon\Desktop\내보내기.txt") == true)
                {
                    ss = System.IO.File.ReadAllLines(@"C:\Users\sohyeon\Desktop\내보내기.txt");  //몇번 사진이 채택되었는지 받아옴
                    be=f[Convert.ToInt32(ss[0])]-1;
                    pictureBox1.ImageLocation = ll[be];
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    break;
                }
            }
			label3.Left = (this.ClientSize.Width - label3.Width) / 2;
            label3.Top = (this.ClientSize.Height - label3.Height) / 2;
            time.Interval = 1000;
            time.Tick += new EventHandler(time_Tick);
            time.Start();


        }
        private void time_Tick(object sender, EventArgs e)
        {
            t++;
            if (t == 3)
            {
                this.Controls.Remove(label3);
                this.Controls.Add(label2);
                this.Controls.Add(pictureBox1);
                pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
                pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2;
                label2.Left = (this.ClientSize.Width - label2.Width) / 2;
                label2.Top = (this.ClientSize.Height - label2.Height) / 2 - 400;
            }
            if (t == 10)
            {
                this.Controls.Remove(pictureBox1);
                this.Controls.Remove(label2);
                this.Controls.Add(label1);
                label1.Left = (this.ClientSize.Width - label1.Width) / 2;
                label1.Top = (this.ClientSize.Height - label1.Height) / 2;
            }
            if (t == 20)
            {
                time.Enabled = false;
                time.Stop();
                Form8 showForm8 = new Form8();
                showForm8.ShowDialog();
                this.Visible = false;
                Close();
            }
        }
    }
}
