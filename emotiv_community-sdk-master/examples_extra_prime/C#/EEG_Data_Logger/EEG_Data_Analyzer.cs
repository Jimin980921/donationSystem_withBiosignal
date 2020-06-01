using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EEG_Data_Logger
{
	class EEG_Data_Analyzer
	{
		//double sum = 0;
		//double average = 0;
		int time = 10;
		int length = 0;
		//int ref_Cnt = 0;
		public double[] series;
		public double[] p300_avg = new double[64];				// 64개 (두 채널에 대한 평균)

		//series = new double[length];
		//double[] totalData_A = new double[128]; 
		//double[] totalData_B = new double[128];
		//ArrayList dataList;

		public EEG_Data_Analyzer() //
		{
			//dataList = new ArrayList();
			//	time = _time;
			length = 128 * time;
			series = new double[length];

			double[] p300_data = new double[64]; // 64개 (0.5초 간격) - p300 원본값

			//for (int i = 0; i < 1280; i++)
			//{
			//	series[i] = 0;
			//}
		}
		
		public void AverageOfTwoArrays(double[] a, double[] b, double[] c)
		{       // 배열a = 배열 b, c의 평균(길이 같아야함) 채널배열생성
			if (a.Length == b.Length && a.Length == c.Length)
			{
				for (int i = 0; i < a.Length; i++)
				{
					a[i] = (b[i] + c[i]) / 2;
				};
			}
		}
		
		//Overloading GetP300 
		public double GetP300(double[] src)		// P300 ~ peak값
		{
			double[] dst = new double[38];
			Array.Copy(src, 38, dst, 0, 38);    // 38번(0.3초) 부터 26개(0.2초) 복사 (0.3 ~ 0.5초) - 비교할 값 보관//0.3~0.6초로 다시 바꿈

			PeakDetection peak = new PeakDetection();
			peak.Excute(dst);
			var p300 = peak.GetValue();
			return p300;
		}

		//Overloading GetP300
		public double GetP300(List<double> src_list)        // P300 ~ peak값
		{
			double[] dst = new double[38];
			var src = src_list.ToArray();
			Array.Copy(src, 38, dst, 0, 38);

			PeakDetection peak = new PeakDetection();
			peak.Excute(dst);
			var p300 = peak.GetValue();
			return p300;
		}

		void Peak_P300(double[] array)
		{       // peak 집합
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = GetP300(p300_avg);
			}
		}

		public void Format_1()
		{
			//length = 128 * time;
			//series = new double[length];

			for (int i = 0; i < 1280; i++)
			{
				this.series[i] = 0;
			}
		}

		public double[] getSeries()
		{
			return series;
		}

		public int getLength()
		{
			return series.Length;
		}

		public void AddData(double data)
		{
			for (int i = 0; i < length; i++)
			{
				series[i] = data; //data(뇌파)를 넣어서 배열생성

			}
			//Console.WriteLine("series: " + series);
		}
		public double Analysis()
		{
			/*for (int i = 0; i < 128 * 10; i++)
			{
				sum += series[i];//합해라				
				//var data = dataList[i];
				//dataList.Add(data);
			}
			average = sum / 1280;*/

			double[] Data = series; //배열을 data에 넣고
			//double[] Data = (Double[])dataList.ToArray(typeof(Double));
			double reference = CalculateReference(Data); //밑에 함수로 평균값을 계산해라

			return reference; //reference=avg
		}

		public static double CalculateReference(double[] reference) //reference에 data를 넣고 평균값을 구해라
		{
			double sum = 0;
			for (int i = 0; i < reference.Length; i++)
			{
				sum += reference[i];
			}
			double avg = sum / reference.Length;
			return avg;
		}
	}
}
