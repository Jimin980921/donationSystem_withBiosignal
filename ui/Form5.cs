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
    public partial class Form5 : Form
    {
        private int t = 0;
        private System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
        public Form5() 
        {
            InitializeComponent();
            System.IO.File.AppendAllText(@"C:\Users\sohyeon\Desktop\불러오기.txt", "1"); //래퍼런스 측정 시작을 알림
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            this.Controls.Remove(label2);
			pictureBox1.ImageLocation = @"C:\Users\sohyeon\Desktop\소현\전공\신경공학(특별학기)\바름조_최종\image\그림1.png";
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2 - 150;
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = (this.ClientSize.Height - label1.Height) / 2 + 300;
            time.Interval = 1000;
            time.Tick += new EventHandler(time_Tick);
            time.Start();
        }
        private void time_Tick(object sender, EventArgs e)
        {
            t++;
            if (t == 10)
            {
                this.Controls.Remove(pictureBox1);
                this.Controls.Remove(label1);
                this.Controls.Add(label2);
                label2.Left = (this.ClientSize.Width - label2.Width) / 2;
                label2.Top = (this.ClientSize.Height - label2.Height) / 2;
            }
            if (t == 13)//13
            {
				System.IO.File.Delete(@"C:\Users\sohyeon\Desktop\불러오기.txt");  //파일 삭제
				time.Enabled = false;
                time.Stop();
                newForm shownewForm = new newForm();
                shownewForm.ShowDialog();
                this.Visible = false;
				
                Close();
            }
        }
    }
}
