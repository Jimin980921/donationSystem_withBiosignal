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
    public partial class Form8 : Form
    {
        private System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer time1 = new System.Windows.Forms.Timer();
        private Random r = new Random(DateTime.Now.Millisecond);
		private string[] ss;
		public static int g = 0, a = 0; //몇번 사진인지,a는 몇번 동영상인지
        String[] ll = { "C:\\Users\\sohyeon\\Desktop\\소현\\전공\\신경공학(특별학기)\\바름조_최종\\video\\가족1.mp4",
					"C:\\Users\\sohyeon\\Desktop\\소현\\전공\\신경공학(특별학기)\\바름조_최종\\video\\가족2.mp4",
					"C:\\Users\\sohyeon\\Desktop\\소현\\전공\\신경공학(특별학기)\\바름조_최종\\video\\동물.mp4",
					"C:\\Users\\sohyeon\\Desktop\\소현\\전공\\신경공학(특별학기)\\바름조_최종\\video\\건강1.mp4",
					"C:\\Users\\sohyeon\\Desktop\\소현\\전공\\신경공학(특별학기)\\바름조_최종\\video\\건강2.mp4",
					"C:\\Users\\sohyeon\\Desktop\\소현\\전공\\신경공학(특별학기)\\바름조_최종\\video\\옷.mp4"};
        public Form8()
        {
            InitializeComponent();
			string[] q = { "3" };
			System.IO.File.AppendAllLines(@"C:\Users\sohyeon\Desktop\불러오기.txt", q);  //동영상이 시작됨을 알림
			g = Form7.be;  //몇번 사진인지 폼7에서 받아옴
			this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = (this.ClientSize.Height - label1.Height) / 2 + 400;
            axWindowsMediaPlayer1.Left = (this.ClientSize.Width - axWindowsMediaPlayer1.Width) / 2;
            axWindowsMediaPlayer1.Top = (this.ClientSize.Height - axWindowsMediaPlayer1.Height) / 2 ;
            RanPlay(g);
			time.Interval = 1000 * 62;
            time.Tick += new EventHandler(time_Tick);
            time.Start();
        }
        private void RanPlay(int g)
        {
            if (g == 0 )
            {
                a= (int)r.Next(0, 2);
                axWindowsMediaPlayer1.URL = ll[a];
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            else if (g == 1 )
            {
                a = 2;
                axWindowsMediaPlayer1.URL = ll[a];
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            else if (g == 2)
            {
                a = (int)r.Next(3, 5);
                axWindowsMediaPlayer1.URL = ll[a];
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
            else if (g == 3)
            {
                a = 5;
                axWindowsMediaPlayer1.URL = ll[a];
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
			int ppp = a + 4;
            string[] q= { ppp.ToString() };
            System.IO.File.AppendAllLines(@"C:\Users\sohyeon\Desktop\불러오기.txt", q);  //몇번 동영상인지 보냄
        }
        private void time_Tick(object sender, EventArgs e)
        {
            System.IO.File.Delete(@"C:\Users\sohyeon\Desktop\불러오기.txt");  //파일 삭제
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            this.Controls.Remove(axWindowsMediaPlayer1);
            time.Stop();
            Form9 showForm9 = new Form9();
            showForm9.ShowDialog();
            this.Visible = false;
            Close();
        }
    }
}
