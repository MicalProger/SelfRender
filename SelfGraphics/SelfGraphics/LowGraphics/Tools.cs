using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfGraphics.LowGraphics
{
    static class Tools
    {

        public static double ToRads(double angle)
        {
            return  (angle * 180) / Math.PI;  
        }
        public static double AVG(List<double> nums)
        {
            return nums.Select(n => Convert.ToDouble(n)).Sum() / nums.Count;
        }
        public static List<double[]> DoubleRange(double x, double y, int step = 1)
        {
            List<double[]> f = new List<double[]>();
            for (double i = -x; i < x; i++)
            {
                for (double j = -y; j < y; j++)
                {
                    f.Add(new double[] { i, j });
                }
            }
            return f;
        }
    }
}