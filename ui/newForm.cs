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
	public partial class newForm : Form
	{
		private System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
		private int t = 0;
		public newForm()
		{
			InitializeComponent();
			this.Width = Screen.PrimaryScreen.Bounds.Width;
			this.Height = Screen.PrimaryScreen.Bounds.Height;
			this.Location = new Point(0, 0);
			pictureBox1.ImageLocation = @"C:\Users\sohyeon\Desktop\소현\전공\신경공학(특별학기)\바름조_최종\image\new.png";
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
			pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
			pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2;
			label1.Left = (this.ClientSize.Width - label1.Width) / 2;
			label1.Top = (this.ClientSize.Height - label1.Height) / 2 - 400;
			time.Interval = 1000;
			time.Tick += new EventHandler(time_Tick);
			time.Start();
		}
		private void time_Tick(object sender, EventArgs e) {
			if (t == 5) {
				time.Stop();
				Form6 showForm6 = new Form6();
				showForm6.ShowDialog();
				this.Visible = false;
				Close();
			}
			t++;
		}
	}
}
