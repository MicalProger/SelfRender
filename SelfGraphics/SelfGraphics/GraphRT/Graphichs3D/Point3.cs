using System;
using System.Globalization;

namespace SelfGraphics.GraphRT.Graphichs3D
{
    public class Point3
    {
        public override string ToString()
        {
            return $"[{X} {Y} {Z}]";
        }

        public static Point3 Zero = new Point3(0, 0, 0);
        public Point3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Point3(string xyz)
        {
            var asixs = xyz.Split(' ');
            X = Convert.ToDouble(asixs[0], CultureInfo.InvariantCulture);
            Y = Convert.ToDouble(asixs[1], CultureInfo.InvariantCulture);
            Z = Convert.ToDouble(asixs[2], CultureInfo.InvariantCulture);

        }
        
        public Point3(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Point3(double xyz)
        {
            X = xyz;
            Y = xyz;
            Z = xyz;
        }
        
        public double X;
        
        public double Y;

        public double Z;

    }
}