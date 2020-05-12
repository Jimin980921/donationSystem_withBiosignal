using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace coc
{
    public partial class Form10 : Form
    {
        private System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
        private string[] s;
		private int mov = 0;
		public Form10()
        {
            InitializeComponent();
			mov = Form8.a;
			this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;
			string[] ss;
			while (true)
			{
				if (System.IO.File.Exists(@"C:\Users\sohyeon\Desktop\그래프.txt") == true)
				{
					ss = System.IO.File.ReadAllLines(@"C:\Users\sohyeon\Desktop\그래프.txt");  //몇번 사진이 채택되었는지 받아옴
					break;
				}
			}
			chart1.Left = (this.ClientSize.Width - chart1.Width) / 2;
			chart1.Top = (this.ClientSize.Height - chart1.Height) / 2 - 500;
			chart1.Series.Clear();
			//chart1.BackColor = Color.Transparent;  //배경색 없애기
			chart1.ChartAreas[0].BorderColor = Color.White;    //내부 테두리선
			chart1.ForeColor = Color.Black;
			chart1.ChartAreas[0].BorderWidth = 2;
			chart1.ChartAreas[0].BorderDashStyle = ChartDashStyle.Solid;
			chart1.ChartAreas[0].BackColor = Color.Transparent;  //안 배경색 없애기
			//chart1.ChartAreas[0].AxisX.LineColor = Color.White;   //x축 색
			//chart1.ChartAreas[0].AxisY.LineColor = Color.White;   // y축 색
			chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.White; //y축 격자색
			chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;   //x축격자 삭제
			chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;   //x축격자 삭제
			chart1.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dot; //y축 격자를 점선으로
			chart1.ChartAreas[0].AxisX.Maximum = 60.0;   //x는 최대 60까지
			chart1.ChartAreas[0].AxisX.Minimum = 0.0;
			chart1.ChartAreas[0].AxisY.Minimum = 3800.0;    //y는 3800부터
			chart1.ChartAreas[0].AxisY.Maximum = 5000.0;    //y는 3800부터
			chart1.ChartAreas[0].AxisX.TitleForeColor = Color.White;
			chart1.ChartAreas[0].AxisY.TitleForeColor = Color.White;

			Series gra = chart1.Series.Add("집중도");
			Series re = chart1.Series.Add("평균");
		//	Series chu = chart1.Series.Add("텍스트 간격");
			
			gra.ChartType = SeriesChartType.Line;
			re.ChartType = SeriesChartType.Line;
			//chu.Color = Color.Salmon;
			for (int i = 0; i < 60; i++)
            {
                gra.Points.AddXY(i, ss[i+1]);
                re.Points.AddXY(i, ss[0]);
            }
            s = Form9.ss;//얼마인지 받아옴
            label2.Text = (50 * ((Convert.ToInt32(s[1])))).ToString() + "원 입니다";
            pictureBox1.ImageLocation = @"C:\Users\sohyeon\Desktop\소현\전공\신경공학(특별학기)\바름조_최종\image\piggy-bank.png";
			pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Left = (this.ClientSize.Width - pictureBox1.Width) / 2;
            pictureBox1.Top = (this.ClientSize.Height - pictureBox1.Height) / 2 - 150;
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = (this.ClientSize.Height - label1.Height) / 2 + 280;
            label2.Left = (this.ClientSize.Width - label2.Width) / 2;
            label2.Top = (this.ClientSize.Height - label2.Height) / 2 + 320;
            button1.Left = (this.ClientSize.Width - button1.Width) / 2;
			label3.Left = (this.ClientSize.Width - label3.Width) / 2;
			label3.Top = (this.ClientSize.Height - label3.Height) / 2 + 450;
			button1.Top = (this.ClientSize.Height - button1.Height) / 2 + 400;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            System.IO.File.Delete(@"C:\Users\sohyeon\Desktop\그래프.txt");  //파일 삭제
            Form11 showForm11 = new Form11();
            showForm11.ShowDialog();
            this.Visible = false;
            Close();
        }

		private void chart1_Click(object sender, EventArgs e)
		{

		}

		private void Form10_Load(object sender, EventArgs e)
		{

		}
	}
}
