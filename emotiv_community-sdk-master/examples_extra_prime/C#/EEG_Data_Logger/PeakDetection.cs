using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace EEG_Data_Logger
{
    class PeakDetection
	{
		private double lastval=0;
		public void Excute(double[] result)
        {
            var lastindex = 0;
            bool dirpos = true;
            for (int i = 0; i < result.Length; i++)
            {
                var currentval = result[i];
                if (dirpos && currentval < lastval)
                {
                    //Console.WriteLine(string.Format("i: {0} value: {1}", lastindex, result[lastindex]));
                    dirpos = false;
                }

                if (currentval >= lastval)
                    dirpos = true;

                lastval = currentval;
                lastindex = i;	
            }
        }
		public double GetValue()
		{
			return lastval;	
		}

	}
}
