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
    public partial class Form9 : Form
    {
        private int t = 0, mov = 0, w = 0;
        private int[] sel = new int[6];
        private System.Windows.Forms.Timer time = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer time1 = new System.Windows.Forms.Timer();
        public static string[] ss;
		public static string[] sss;
		public Form9()
        {
            InitializeComponent();
			this.Width = Screen.PrimaryScreen.Bounds.Width;
			this.Height = Screen.PrimaryScreen.Bounds.Height;
			this.Controls.Remove(chart1);
			this.Controls.Remove(chart2);
			while (true)
			{
				if (System.IO.File.Exists(@"C:\Users\sohyeon\Desktop\그래프.txt") == true)
				{
					try { ss = System.IO.File.ReadAllLines(@"C:\Users\sohyeon\Desktop\그래프.txt"); }
					catch (Exception e)
					{
					}
					break;
				}
			}
			while (true)
			{
				if (System.IO.File.Exists(@"C:\Users\sohyeon\Desktop\비율.txt") == true)
				{
					try {
						sss = System.IO.File.ReadAllLines(@"C:\Users\sohyeon\Desktop\비율.txt");  }
					catch (Exception e)
					{
					}
					break;
				}
			}


			label1.Left = (this.ClientSize.Width - label1.Width) / 2;
			label1.Top = (this.ClientSize.Height - label1.Height) / 2;
			chart2.Left = (this.ClientSize.Width - chart2.Width) / 2 +300;
			chart2.Top = (this.ClientSize.Height - chart2.Height) / 2 - 500;

			

			//chart1.Left = (this.ClientSize.Width - chart1.Width) / 2;
			chart1.Left = (this.ClientSize.Width - chart1.Width) / 2 - 200;
			chart1.Top = (this.ClientSize.Height - chart1.Height) / 2 -500;
			//chart1.Top = (this.ClientSize.Height - chart1.Height) / 2 ;
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
			chart1.ChartAreas[0].AxisY.Minimum = 3500.0;    //y는 3800부터
			chart1.ChartAreas[0].AxisY.Maximum = 5000.0;    //y는 3800부터
			chart1.ChartAreas[0].AxisX.TitleForeColor = Color.White;
			chart1.ChartAreas[0].AxisY.TitleForeColor = Color.White;

			Series gra = chart1.Series.Add("집중도");
			Series re = chart1.Series.Add("평균");
			Series chu = chart1.Series.Add("텍스트 간격");

			Series se= chart1.Series.Add(" ");
			Series se1 = chart1.Series.Add("  ");
			Series se2 = chart1.Series.Add("   ");
			gra.ChartType = SeriesChartType.Line;
			re.ChartType = SeriesChartType.Line;
			se.ChartType = SeriesChartType.Line;
			se1.ChartType = SeriesChartType.Line;
			se2.ChartType = SeriesChartType.Line;
			se.Color = Color.Salmon;
			for (int i = 1; i <= 60; i++)
			{
				gra.Points.AddXY(i, ss[i]);
				re.Points.AddXY(i, ss[0]);
			}

			chart2.Series.Clear();
			Series g = chart2.Series.Add(" ");
			g.ChartType = SeriesChartType.Pie;
			g.IsValueShownAsLabel = true;
			for (int i = 0; i < sss.Length; i++)
			{
				if (!sss[i].Equals("-"))
				{ g.Points.AddY(sss[i]); }
			}
			g.Label = "#PERCENT{P1}";


			mov = Form8.a;
			SelTime(g,se,se1,se2);

            this.Controls.Remove(label2);
            this.Controls.Remove(label3);
            this.Location = new Point(0, 0);
            this.Controls.Remove(button1);
            this.Controls.Remove(button2);
			while (true)
            {
                if (System.IO.File.Exists(@"C:\Users\sohyeon\Desktop\내보내기.txt") == true)
                {
					try { ss = System.IO.File.ReadAllLines(@"C:\Users\sohyeon\Desktop\내보내기.txt"); }
					catch (Exception e)
					{
						//s = File.ReadAllLines(@"C:\Users\sohyeon\Desktop\불러오기.txt");
					}
					w = Convert.ToInt32(ss[0])-1;
                    break;
                }
            }
			time.Interval = 1000;
			time.Tick += new EventHandler(time_Tick);
			time.Start();
		}
		private void SelTime(Series g,Series se,Series se1,Series se2)
		{
			if (mov == 0)
			{
				se.Points.AddXY(11, 3500);
				se.Points.AddXY(11, 5000);
				se1.Points.AddXY(32, 3500);
				se1.Points.AddXY(32, 5000);
				g.Points[0].LegendText = "0~11초";
				g.Points[1].LegendText = "11~32초";
				g.Points[2].LegendText = "32~60초";
			}
			else if (mov == 1)
			{
				se.Points.AddXY(30, 3500);
				se.Points.AddXY(30, 5000);
				g.Points[0].LegendText = "0~30초";
				g.Points[1].LegendText = "30~60초";
			}
			else if (mov == 2)
			{
				se.Points.AddXY(15, 3500);
				se.Points.AddXY(15, 5000);
				se1.Points.AddXY(50, 3500);
				se1.Points.AddXY(50, 5000);
				g.Points[0].LegendText = "0~115초";
				g.Points[1].LegendText = "15~50초";
				g.Points[2].LegendText = "50~60초";
			}
			else if (mov == 3)
			{
				se.Points.AddXY(24, 3500);
				se.Points.AddXY(24, 5000);
				se1.Points.AddXY(48, 3500);
				se1.Points.AddXY(48, 5000);
				g.Points[0].LegendText = "0~24초";
				g.Points[1].LegendText = "24~48초";
				g.Points[2].LegendText = "48~60초";
			}
			else if (mov == 4)
			{
				se.Points.AddXY(35, 3500);
				se.Points.AddXY(35, 5000);
				g.Points[0].LegendText = "0~35초";
				g.Points[1].LegendText = "35~60초";
			}
			else if (mov == 5)
			{
				se.Points.AddXY(17, 3500);
				se.Points.AddXY(17, 5000);
				se1.Points.AddXY(24, 3500);
				se1.Points.AddXY(24, 5000);
				se2.Points.AddXY(36, 3500);
				se2.Points.AddXY(36, 5000);
				g.Points[0].LegendText = "0~17초";
				g.Points[1].LegendText = "17~24초";
				g.Points[2].LegendText = "24~36초";
				g.Points[3].LegendText = "36~60초";
			}
		}
		private void SelText()
        {
			if (mov == 0)
			{
				string[] s ={"\'좋았어요..집이 아니어서....\'\r\n기댈 곳 없는 유빈이에게 따뜻한 마음을 전달해주세요",
						  "아빠에게 장난감처럼 여겨져 학대당한\r\n유빈이에게 따뜻한 마음을 전달해주세요",
						  "기댈 곳 없는 어린 유빈이에게는\r\n따뜻한 관심이 필요합니다" };
				label2.Text = s[w];
			}
			if (mov == 1)
			{
				string[] s ={"아이들 걱정뿐인 엄마는 할 수 있는 것이 없어\r\n눈물만 흘립니다. 엄마의 마음의 짐을 덜어주세요",
						  "혼자서 엄마의 역할을 하고있는\r\n한별이의 짐을 덜어주세요" };
				label2.Text = s[w];
			}
			if (mov == 2)
			{
				string[] s ={ "소모품처럼 버려져가는 강아지에게\r\n더 나은 삶을 선물해주세요",
						  "가족에게 버림받은 \'할배\'에게\r\n더 나은 삶을 선물해주세요",
						  "입양을 기다리고 있는 수많은 유기견들에게\r\n작은 보탬이 되어주세요"};
				label2.Text = s[w];
			}
			if (mov == 3)
			{
				string[] s ={ "선천적인 병으로 걷지 못하는\r\n나영이에게 힘이 되어주세요",
						  "나쁜 상황이 겹쳐와도 희망의 끈을 놓지않고\r\n열심히 살아가는 나영이의 힘이 되어주세요",
						  "평범한 행복을 바라는\r\n나영의 꿈을 지켜주세요" };
				label2.Text = s[w];
			}
			if (mov == 4)
			{
				string[] s ={ "현우가 필요한 치료를 안정적으로\r\n받을 수 있도록 함께해주세요",
						  "\'가슴으로 받아주던 선생님에게 엄마라 부르는 현우\'\r\n현우에게 따뜻한 마음을 전달해주세요" };
				label2.Text = s[w];
			}
			if (mov == 5)
			{
				string[] s ={"연락이 끊긴 부모대신 아픈 할머니와 살고있는\r\n효정이에게 따뜻한 마음을 전해주세요",
						  "헤어진 옷 소매를 테이프로 고쳐입는\r\n효정이에게 작은 보탬이 되어주세요",
						  "매서운 바람을 테이프로 막아 추위를 견디는\r\n효정이에게 작은 보탬이 되어주세요",
						  "작아진 점퍼 하나와 테이프로 긴 겨울을\r\n보내는 효정이에게 작은 보탬이 되어주세요"};
                
                label2.Text = s[w];
			}
		}
        private void time_Tick(object sender, EventArgs e)
        {
            t++;
            if (t == 5)
            {
                SelText();
                this.Controls.Remove(label1);
                this.Controls.Add(label2);
                this.Controls.Add(label3);
				this.Controls.Add(chart1);
				this.Controls.Add(chart2);
				label2.Left = (this.ClientSize.Width - label2.Width) / 2 ;
				label2.Top = (this.ClientSize.Height - label2.Height) / 2 - 50;
				label3.Left = (this.ClientSize.Width - label3.Width) / 2;
				label3.Top = (this.ClientSize.Height - label3.Height) / 2 + 400;

			}
            if (t == 7)
            {
                button1.Left = (this.ClientSize.Width - button1.Width) / 2;
                button1.Top = (this.ClientSize.Height - button1.Height) / 2 + 200;
                button2.Left = (this.ClientSize.Width - button2.Width) / 2;
                button2.Top = (this.ClientSize.Height - button2.Height) / 2 + 280;
                this.Controls.Add(button1);
                this.Controls.Add(button2);
                time.Stop();
            }
        }

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
        {
            System.IO.File.Delete(@"C:\Users\sohyeon\Desktop\내보내기.txt");  //파일 삭제
			System.IO.File.Delete(@"C:\Users\sohyeon\Desktop\비율.txt");  //파일 삭제
			Form10 showForm10 = new Form10();
            showForm10.ShowDialog();
            this.Visible = false;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.File.Delete(@"C:\Users\sohyeon\Desktop\내보내기.txt");  //파일 삭제
			System.IO.File.Delete(@"C:\Users\sohyeon\Desktop\비율.txt");  //파일 삭제
			Form12 showForm12 = new Form12();
            showForm12.ShowDialog();
            this.Visible = false;
            Close();
        }
    }
}
