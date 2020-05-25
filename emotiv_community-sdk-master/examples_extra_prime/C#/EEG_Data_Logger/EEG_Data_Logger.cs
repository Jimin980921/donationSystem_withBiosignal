using System;
using System.Collections.Generic;
using Emotiv;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Collections;
using System.Linq;

namespace EEG_Data_Logger
{
	class EEG_Logger
	{
		EmoEngine engine;   // Access to the EDK is via the EmoEngine 
		int userID = -1;    // userID is used to uniquely identify a user's headset
							//string filename = "EEG_Data_Logger1000.csv"; // output filename 
		public double avg_reference = 0;

		public List<double> f3_Raw = new List<double>();
		public List<double> f4_Raw = new List<double>();

		public bool is_Reference = false;
		public bool is_Estimate = false;

		public double[] picture_peak_data = new double[16];     // 1초 간격으로 사진 4장 × 4세트
		public double[] video_peak_data = new double[60];      // 1초 간격으로 60초간 측정


		EEG_Logger()
		{
			// create the engine
			engine = EmoEngine.Instance;
			engine.UserAdded += new EmoEngine.UserAddedEventHandler(engine_UserAdded_Event);

			// connect to Emoengine.            
			engine.Connect();

			// create a header for our output file
			//WriteHeader();
		}

		void engine_UserAdded_Event(object sender, EmoEngineEventArgs e)
		{
			Console.WriteLine("User Added Event has occured");

			// record the user 
			userID = (int)e.userId;

			// enable data aquisition for this user.
			engine.DataAcquisitionEnable((uint)userID, true);

			// ask for up to 1 second of buffered data
			engine.DataSetBufferSizeInSec(1);

		}
		void Run()
		{
			#region Config

			// Handle any waiting events
			engine.ProcessEvents();

			// If the user has not yet connected, do not proceed
			if ((int)userID == -1)
				return;

			Dictionary<EdkDll.IEE_DataChannel_t, double[]> data = engine.GetData((uint)userID);

			if (data == null)
			{
				return;
			}
			int _bufferSize = data[EdkDll.IEE_DataChannel_t.IED_TIMESTAMP].Length;
			#endregion
			//Console.WriteLine("\nWriting " + _bufferSize.ToString() + " sample of data ");

			double avg_f3 = 0;
			double avg_f4 = 0;
			// Write the data to a file
			//TextWriter file = new StreamWriter(filename, true);

			for (int i = 0; i < _bufferSize; i++)
			{
				// now write the data
				foreach (EdkDll.IEE_DataChannel_t channel in data.Keys)
				{
					#region reference

					if (is_Reference && Convert.ToString(channel) == "IED_F3")
					{
						double val = data[channel][i];
						f3_Raw.Add(val);

						if (f3_Raw.Count >= 1280)
						{
							avg_f3 = f3_Raw.Average();
						}
					}
					else if (is_Reference && Convert.ToString(channel) == "IED_F4")
					{
						double val = data[channel][i];
						f4_Raw.Add(val);

						if (f4_Raw.Count >= 1280)
						{
							avg_f4 = f4_Raw.Average();
						}
					}
					
					if (is_Reference && f3_Raw.Count >= 1280 && f4_Raw.Count >= 1280)
					{
						is_Reference = false;
						is_Estimate = true;
						avg_reference = (avg_f3 + avg_f4) / 2;
						Console.WriteLine("f3 10초의 전체평균: " + avg_f3);
						Console.WriteLine("f4 10초의 전체평균: " + avg_f4);
						Console.WriteLine("10초동안의 f3,4의 평균 : " + avg_reference);
						//f3_Raw.Clear(); //list empty
						//f4_Raw.Clear();

					}
					#endregion

					// reference (10s) 이후,
					if (!is_Reference)
					{
						if (Convert.ToString(channel) == "IED_F3")
						{
							double val = data[channel][i]; //채널에
							f3_Raw.Add(val);

						}
						if (Convert.ToString(channel) == "IED_F4")
						{
							double val = data[channel][i];
							f4_Raw.Add(val);
						}

					}
				}
				//file.WriteLine("");
			}
			//file.Close();
		}

		public static int Max_array(double[] array)  // 배열의 요소 최대값(카테고리 설정)
		{
			double max = array[0];
			int index = 0;

			for (int i = 1; i < array.Length; i++)
			{
				if (max < array[i])
				{
					max = array[i];
					index = i;
				}
			}
			return index;
		}

		static int SelectM(double[] d, string i) //빈도수 계산
		{
			string[] sssss= new string[4];
			int part = 0;
			if (i.Equals("4"))//1번동영상에서의 구간별 비율 + 빈도수 가장 높은 구간 출력
			{
				double a = 0;
				double b = 0;
				double c = 0;
				for (int j = 0; j < 12; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					a = number / 12;
				}
				sssss[0] = a.ToString();

				for (int j = 12; j < 33; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					b = number / 22;
				}
				sssss[1] = b.ToString();

				for (int j = 33; j < 60; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					c = number / 28;
				}
				sssss[2] = c.ToString();
				//File.AppendAllLines(@"C:\Users\sohyeon\Desktop\비율.txt", sssss);//비율 넘겨주기


				if ((a > b) && (a > c))
				{
					part = 1;
				}
				else if ((b > a) && (b > c))
				{
					part = 2;
				}
				else //if ((c > a) && (c > b))
				{
					part = 3;
				}
			}
			//0
			//밑에 7?
			else if (i.Equals("7"))
			{
				double a = 0;
				double b = 0;
				double c = 0;
				for (int j = 0; j < 25; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					a = number / 25;
				}
				sssss[0] = a.ToString();

				for (int j = 25; j < 49; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					b = number / 24;
				}
				sssss[1] = b.ToString();

				for (int j = 49; j < 60; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					c = number / 11;
				}
				sssss[2] = c.ToString();
				//File.AppendAllLines(@"C:\Users\sohyeon\Desktop\비율.txt", sssss);//비율 넘겨주기

				if ((a > b) && (a > c))
				{
					part = 1;
				}
				else if ((b > a) && (b > c))
				{
					part = 2;
				}
				else //if ((c > a) && (c > b))
				{
					part = 3;
				}
			}
			//밑에 8
			else if (i.Equals("8"))
			{
				double a = 0;
				double b = 0;
				for (int j = 0; j < 36; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					a = number / 36;
				}
				sssss[0] = a.ToString();

				for (int j = 36; j < 60; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					b = number / 24;
				}
				sssss[1] = b.ToString();
				//File.AppendAllLines(@"C:\Users\sohyeon\Desktop\비율.txt", sssss);//비율 넘겨주기

				if ((a > b))
					part = 1;
				else
					part = 2;
			}
			//밑에 5
			else if (i.Equals("5"))
			{
				double a = 0;
				double b = 0;
				for (int j = 0; j < 31; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					a = number / 31;
				}
				sssss[0] = a.ToString();

				for (int j = 31; j < 60; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					b = number / 29;
				}
				sssss[1] = b.ToString();
				//File.AppendAllLines(@"C:\Users\sohyeon\Desktop\비율.txt", sssss);//비율 넘겨주기

				if ((a > b))
					part = 1;
				else
					part = 2;
			}
			//밑에 9
			else if (i.Equals("9"))
			{
				double a = 0;
				double b = 0;
				double c = 0;
				double r = 0;
				for (int j = 0; j < 18; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					a = number / 18;
				}
				sssss[0] = a.ToString();

				for (int j = 18; j < 25; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					b = number / 7;
				}
				sssss[1] = b.ToString();

				for (int j = 25; j < 37; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					c = number / 12;
				}
				sssss[2] = c.ToString();


				for (int j = 37; j < 60; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					r = number / 23;
				}
				sssss[3] = r.ToString();
				//File.AppendAllLines(@"C:\Users\sohyeon\Desktop\비율.txt", sssss);//비율 넘겨주기

				if ((a > b) && (a > c) && (a > r))
				{
					part = 1;
				}
				else if ((b > a) && (b > c) && (b > r))
				{
					part = 2;
				}
				else if ((c > a) && (c > b) && (c > r))
				{
					part = 3;
				}
				else //if ((r > a) && (r > b) && (r > c))
				{
					part = 4;
				}
			}
			//alxdp 6
			else //if (i.Equals(9))
			{
				double a = 0;
				double b = 0;
				double c = 0;
				for (int j = 0; j < 16; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					a = number / 16;
				}
				sssss[0] = a.ToString();

				for (int j = 16; j < 51; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					b = number / 35;
				}
				sssss[1] = b.ToString();

				for (int j = 51; j < 60; j++)
				{
					double number = 0;
					if (d[j] == 1)
					{
						number++;
					}
					c = number / 9;
				}
				sssss[2] = c.ToString();
				//File.AppendAllLines(@"C:\Users\sohyeon\Desktop\비율.txt", sssss);//비율 넘겨주기

				if ((a > b) && (a > c))
				{
					part = 1;
				}
				else if ((b > a) && (b > c))
				{
					part = 2;
				}
				else // if ((c > a) && (c > b))
				{
					part = 3;
				}
			}
			File.AppendAllLines(@"C:\Users\sohyeon\Desktop\비율.txt", sssss);//비율 넘겨주기
			return part;
			
		}
		static int SelectM1(double[] d, string i) {
			string[] beeyul = { "-","-","-","-"};// 비율들어가는 것
			int part = 0;
			int count = 0;
			if (i.Equals("4"))//만약 첫번째 동영상이면
			{
				for (int j = 0; j < 12; j++)
				{
					if (d[j] == 1) count++;
					if (j == 11) { beeyul[0] = ((double)count / 12).ToString(); count = 0; }
				}
				for (int j = 12; j < 33; j++)
				{
					if (d[j] == 1) count++;
					if (j == 32) { beeyul[1] = ((double)count / 21).ToString(); count = 0; }
				}
				for (int j = 33; j < 60; j++)
				{
					if (d[j] == 1) count++;
					if (j == 59) { beeyul[2] = ((double)count / 27).ToString(); count = 0; }
				}
				if (Convert.ToDouble(beeyul[0]) >= Convert.ToDouble(beeyul[1]) && Convert.ToDouble(beeyul[0]) >= Convert.ToDouble(beeyul[2]))
					part = 1;
				else if (Convert.ToDouble(beeyul[1]) >= Convert.ToDouble(beeyul[0]) && Convert.ToDouble(beeyul[0]) >= Convert.ToDouble(beeyul[2]))
					part = 2;
				else
					part = 3;
			}
			else if (i.Equals("5"))//만약 2번째 동영상이면
			{
				for (int j = 0; j < 31; j++)
				{
					if (d[j] == 1) count++;
					if (j == 30) { beeyul[0] = ((double)count / 31).ToString(); count = 0; }
				}
				for (int j = 31; j < 60; j++)
				{
					if (d[j] == 1) count++;
					if (j == 59) { beeyul[1] = ((double)count / 29).ToString(); count = 0; }
				}
				if (Convert.ToDouble(beeyul[0]) >= Convert.ToDouble(beeyul[1]))
					part = 1;
				else 
					part = 2;
			}
			else if (i.Equals("6"))//만약 3번째 동영상이면
			{
				for (int j = 0; j < 16; j++)
				{
					if (d[j] == 1) count++;
					if (j == 15) { beeyul[0] = ((double)count / 16).ToString(); count = 0; }
				}
				for (int j = 16; j < 51; j++)
				{
					if (d[j] == 1) count++;
					if (j == 50) { beeyul[1] = ((double)count / 35).ToString(); count = 0; }
				}
				for (int j = 51; j < 60; j++)
				{
					if (d[j] == 1) count++;
					if (j == 59) { beeyul[2] = ((double)count / 9).ToString(); count = 0; }
				}
				if (Convert.ToDouble(beeyul[0]) >= Convert.ToDouble(beeyul[1]) && Convert.ToDouble(beeyul[0]) >= Convert.ToDouble(beeyul[2]))
					part = 1;
				else if (Convert.ToDouble(beeyul[1]) >= Convert.ToDouble(beeyul[0]) && Convert.ToDouble(beeyul[0]) >= Convert.ToDouble(beeyul[2]))
					part = 2;
				else
					part = 3;
			}
			else if (i.Equals("7"))//만약 4번째 동영상이면
			{
				for (int j = 0; j < 25; j++)
				{
					if (d[j] == 1) count++;
					if (j == 24) { beeyul[0] = ((double)count / 25).ToString(); count = 0; }
				}
				for (int j = 25; j < 49; j++)
				{
					if (d[j] == 1) count++;
					if (j == 48) { beeyul[1] = ((double)count / 24).ToString(); count = 0; }
				}
				for (int j = 49; j < 60; j++)
				{
					if (d[j] == 1) count++;
					if (j == 59) { beeyul[2] = ((double)count / 11).ToString(); count = 0; }
				}
				if (Convert.ToDouble(beeyul[0]) >= Convert.ToDouble(beeyul[1]) && Convert.ToDouble(beeyul[0]) >= Convert.ToDouble(beeyul[2]))
					part = 1;
				else if (Convert.ToDouble(beeyul[1]) >= Convert.ToDouble(beeyul[0]) && Convert.ToDouble(beeyul[0]) >= Convert.ToDouble(beeyul[2]))
					part = 2;
				else
					part = 3;
			}
			else if (i.Equals("8"))//만약 5번째 동영상이면
			{
				for (int j = 0; j < 36; j++)
				{
					if (d[j] == 1) count++;
					if (j == 35) { beeyul[0] = ((double)count / 36).ToString(); count = 0; }
				}

				for (int j = 36; j < 60; j++)
				{
					if (d[j] == 1) count++;
					if (j == 59) { beeyul[1] = ((double)count / 24).ToString(); count = 0; }
				}
				if (Convert.ToDouble(beeyul[0]) >= Convert.ToDouble(beeyul[1]))
					part = 1;
				else
					part = 2;
			}
			else if (i.Equals("9"))//만약 6번째 동영상이면
			{
				for (int j = 0; j < 18; j++)
				{
					if (d[j] == 1) count++;
					if (j == 17) { beeyul[0] = ((double)count / 18).ToString(); count = 0; }
				}
				for (int j = 18; j < 25; j++)
				{
					if (d[j] == 1) count++;
					if (j == 24) { beeyul[1] = ((double)count / 7).ToString(); count = 0; }
				}
				for (int j = 25; j < 37; j++)
				{
					if (d[j] == 1) count++;
					if (j == 36) { beeyul[2] = ((double)count / 12).ToString(); count = 0; }
				}
				for (int j = 37; j < 60; j++)
				{
					if (d[j] == 1) count++;
					if (j == 59) { beeyul[3] = ((double)count / 23).ToString(); count = 0; }
				}
				if (Convert.ToDouble(beeyul[0]) >= Convert.ToDouble(beeyul[1]) && Convert.ToDouble(beeyul[0]) >= Convert.ToDouble(beeyul[2]) && Convert.ToDouble(beeyul[0]) >= Convert.ToDouble(beeyul[3]))
					part = 1;
				else if (Convert.ToDouble(beeyul[1]) >= Convert.ToDouble(beeyul[0]) && Convert.ToDouble(beeyul[1]) >= Convert.ToDouble(beeyul[2])&& Convert.ToDouble(beeyul[1]) >= Convert.ToDouble(beeyul[3]))
					part = 2;
				else if (Convert.ToDouble(beeyul[2]) >= Convert.ToDouble(beeyul[0]) && Convert.ToDouble(beeyul[2]) >= Convert.ToDouble(beeyul[1])&& Convert.ToDouble(beeyul[2]) >= Convert.ToDouble(beeyul[3]))
					part = 3;
				else
					part = 4;
			}
			for (int a = 0; a < 4; a++)
			{
				Console.WriteLine("\n\n 비율은: \n\n" + beeyul[a]);
			}

			File.AppendAllLines(@"C:\Users\sohyeon\Desktop\비율.txt", beeyul);//비율 넘겨주기
			return part;
		}

		static void Main(string[] args)
		{
			Console.WriteLine("EEG Data Reader Example");
			EEG_Logger p = new EEG_Logger();
			EEG_Data_Analyzer f3_analyzer = new EEG_Data_Analyzer();
			EEG_Data_Analyzer f4_analyzer = new EEG_Data_Analyzer();
			List<double> p300_List = new List<double>();
			
			int money = 0;
			int aaaa = 0;
			string[] whatPartS = { null };
			string[] whatImgS = { null };
			string[] gra = new String[61];
			string[] s = new string[] { "0", "0" };
			//FileStream fs = new FileStream(@"C:\Users\sohyeon\Desktop\불러오기.txt",fi)
			int stimuli_img_count = 0;
			int stimuli_video_count = 0;
			int i = 1;
			while (true)
			{
				p.Run();
				if (File.Exists(@"C:\Users\sohyeon\Desktop\불러오기.txt")) //텍스트값받아오기(읽어오는것)
				{
					try { s = File.ReadAllLines(@"C:\Users\sohyeon\Desktop\불러오기.txt"); }
					catch (Exception e) {
						//s = File.ReadAllLines(@"C:\Users\sohyeon\Desktop\불러오기.txt");
					}
				}
				if (s[0].Equals("1") && aaaa == 0)
				{
					p.is_Reference = true;
					p.f3_Raw.Clear(); //list empty
					p.f4_Raw.Clear();
					aaaa++;
				}

				// 1초경과, 데이터 128, 분석, 데이터 모으기 시작

				// 이미지 프로세스
				if (s[0].Equals("2") && !p.is_Reference&& stimuli_img_count < 16) //p300 측정+이미지
				{
					
					if (p.f3_Raw.Count >= 128 && p.f4_Raw.Count >= 128)
					{
						//count++;
						var f3_p300 = f3_analyzer.GetP300(p.f3_Raw);
						var f4_p300 = f4_analyzer.GetP300(p.f4_Raw);
						var avg_stimuli = (f3_p300 + f4_p300) / 2;
						p.picture_peak_data[stimuli_img_count] = avg_stimuli;
						p.f3_Raw.Clear();
						p.f4_Raw.Clear();
						// 결과배열 => p300 저장
						Console.WriteLine($"ref_avg: {p.avg_reference}");
						Console.WriteLine($"stimuli_avg: {avg_stimuli}");
						stimuli_img_count++;

						if (stimuli_img_count == 16)
						{
							double max_val = p.picture_peak_data[0];
							var max_idx = 0;

							for (int a = 1; a < 16; a++)
							{
								if (p.picture_peak_data[a] > max_val)
								{
									max_val = p.picture_peak_data[a];
									max_idx = a;
								}
							}
							Console.WriteLine($"Catagory: {max_idx}");
							whatImgS[0] = max_idx.ToString();
			
							File.AppendAllLines(@"C:\Users\sohyeon\Desktop\내보내기.txt", whatImgS);
						}
					}

				}

				else if (s[0].Equals("3") && !p.is_Reference) //p300 측정+동영상재생
				{
					if (p.f3_Raw.Count >= 128 && p.f4_Raw.Count >= 128&& stimuli_video_count<60)
					{
						if (stimuli_video_count == 0)
							if (File.Exists(@"C:\Users\sohyeon\Desktop\내보내기.txt"))
								File.Delete(@"C:\Users\sohyeon\Desktop\내보내기.txt");
						
						var f3_p300 = f3_analyzer.GetP300(p.f3_Raw);
						var f4_p300 = f4_analyzer.GetP300(p.f4_Raw);
						var avg_stimuli = (f3_p300 + f4_p300) / 2;
						gra[0] = p.avg_reference.ToString();
						if (p.avg_reference < avg_stimuli)
						{
							money++;
							p.video_peak_data[stimuli_video_count] = 1;
						}
						gra[i]= avg_stimuli.ToString();
						Console.WriteLine("\n레퍼런스는" + gra[0]);
						Console.WriteLine("자극" + gra[i]);
						i++;
						p.f3_Raw.Clear();
						p.f4_Raw.Clear();
						// 결과배열 => p300 저장
						Console.WriteLine($"ref_avg: {p.avg_reference}");
						Console.WriteLine($"stimuli_avg: {avg_stimuli}");
						Console.WriteLine($"Money: {money}");
						Console.WriteLine($"video_count: {stimuli_video_count}");
						stimuli_video_count++;
						if (stimuli_video_count == 60)
						{
							int whatPart = 0;
							if (s[1].Equals("4") || s[1].Equals("5") || s[1].Equals("6") || s[1].Equals("7") || s[1].Equals("8") || s[1].Equals("9"))
							{
								//whatPart = SelectM(p.video_peak_data, s[1]);
								whatPart = SelectM1(p.video_peak_data, s[1]);
							}
							whatPartS[0] = whatPart.ToString();
							File.AppendAllLines(@"C:\Users\sohyeon\Desktop\내보내기.txt", whatPartS);
							whatImgS[0] = money.ToString();
							File.AppendAllLines(@"C:\Users\sohyeon\Desktop\내보내기.txt", whatImgS);
							File.AppendAllLines(@"C:\Users\sohyeon\Desktop\그래프.txt", gra);//위치바꾸고 sss에 일단 레퍼런스넣었고 이제 p300 60개 넣어야해
						}
					}
				}
				Thread.Sleep(10);
			}
			
		}
	}
}
