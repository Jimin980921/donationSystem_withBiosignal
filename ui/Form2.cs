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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
			pictureBox1.ImageLocation = @"C:\Users\sohyeon\Desktop\소현\전공\신경공학(특별학기)\바름조_최종\image\airpods.png";
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2-150;
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = (this.ClientSize.Height - label1.Height) / 2 + 300;
            button1.Left = (this.ClientSize.Width - button1.Width) / 2;
            button1.Top = (this.ClientSize.Height - button1.Height) / 2 +400;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 showForm3 = new Form3();
            showForm3.ShowDialog();
            this.Visible = false;
            Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
