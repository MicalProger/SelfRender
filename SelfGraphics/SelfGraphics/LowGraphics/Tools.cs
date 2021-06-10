using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SelfGraphics.LowGraphics
{
    static class Tools
    {

        public static double GetAngle(Point2 p1, Point2 p2)
        {
            var xD = (p1.X - p2.X);
            var yD = (p1.Y - p2.Y);
            var k = xD / yD;
            var rAngle = Math.Atan(k);
            return FromRads(rAngle);
        }
        public static double FromRads(double radians)
        {
            var pre = radians;
            pre *= 180;
            pre /= Math.PI;
            return pre;
        }
        
        public static double ToRads(double angle)
        {
            var pre = angle;
            pre /= 180;
            pre *= Math.PI;
            return pre;
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
        public static List<double[]> DoubleRangePos(double x, double y, int step = 1)
        {
            List<double[]> f = new List<double[]>();
            for (double i = 0; i < x; i++)
            {
                for (double j = 0; j < y; j++)
                {
                    f.Add(new double[] { i, j });
                }
            }
            return f;
        }



    }
}