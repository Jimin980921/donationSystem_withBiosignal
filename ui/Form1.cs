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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
            this.Location = new Point(0, 0);
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = ((this.ClientSize.Height - label1.Height) / 2)-150;
            button1.Left = (this.ClientSize.Width - button1.Width) / 2;
            button1.Top = ((this.ClientSize.Height - button1.Height) / 2) +400;
        }

        private void button1_Click(object sender, EventArgs e)
        {  
            Form2 showForm2 = new Form2();
            showForm2.ShowDialog();
            this.Visible = false;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
