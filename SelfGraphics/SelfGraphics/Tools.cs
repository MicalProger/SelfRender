using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SelfGraphics.LowGraphics;

namespace SelfGraphics
{
    static class Tools
    {
        public static bool IsInPolygon(Point2[] poly, Point2 point)
        {
            var coef = poly.Skip(1).Select((p, i) =>
                    (point.Y - poly[i].Y) * (p.X - poly[i].X)
                    - (point.X - poly[i].X) * (p.Y - poly[i].Y))
                .ToList();

            if (coef.Any(p => p == 0))
                return true;

            for (int i = 1; i < coef.Count(); i++)
            {
                if (coef[i] * coef[i - 1] < 0)
                    return false;
            }

            return true;
        }

        public static double Pow(this double a, int r=2) => Math.Pow(a, r);
        
        public static double Round(this double a, int r=2) => Math.Round(a, r);

        public static double Sin(this double a) => Math.Sin(a.ToRadians());

        public static double Cos(this double a) => Math.Cos(a.ToRadians());

        public static double Tan(this double a) => Math.Tan(a.ToRadians());

        public static double Sqrt(this double a) => Math.Sqrt(a);
        
        public static double Abs(this double a) => Math.Abs(a);

        public static double ToRadians(this double a)
        {
            return ToRads(a);
        }

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
                    f.Add(new double[] {i, j});
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
                    f.Add(new double[] {i, j});
                }
            }

            return f;
        }
    }
}