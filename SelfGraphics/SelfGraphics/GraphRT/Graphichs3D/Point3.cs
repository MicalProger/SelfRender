using SelfGraphics.LowGraphics;
using System;
using System.Globalization;
using SFML.Graphics;

namespace SelfGraphics.GraphRT.Graphics3D
{
    public class Point3
    {
        public double Distance;

        public Color Color;
        
        public void SetDistanceTo(Point3 p)
        {
            Point3 d = (this - p).Absoluted();
            var s1 = Math.Sqrt(Math.Pow(d.X, 2) + Math.Pow(d.Y, 2));
            Distance = Math.Sqrt(Math.Pow(s1, 2) + Math.Pow(d.Z, 2));
        }

        public Direction GetDirectionTo(Point3 p)
        {
            Direction dir = new Direction(0, 0,0);
            var x = Tools.GetAngle(new(X, Y), new(p.X, p.Y));
            var y = Tools.GetAngle(new(Y, Z), new(p.Y, p.Z));
            dir.XRotation = x;
            dir.YRotation = y;
            return dir;
        }

        public double GetDistanceTo(Point3 p)
        {
            if (Distance == 0)
            {
                Point3 d = (this - p).Absoluted();
                var s1 = Math.Sqrt(Math.Pow(d.X, 2) + Math.Pow(d.Y, 2));
                Distance = Math.Sqrt(Math.Pow(s1, 2) + Math.Pow(d.Z, 2));
            }
            return Distance;
        }

        public Point3 Absoluted()
        {
            return new Point3(
                Math.Abs(X),
                Math.Abs(Y),
                Math.Abs(Z));
        }

        public override string ToString()
        {
            return $"[{Math.Round(X, 3)} : {Math.Round(Y, 3)} : {Math.Round(Z, 3)}]";
        }

        public void Round(int r = 2)
        {
            X = Math.Round(X, r);
            Y = Math.Round(Y, r);
            Z = Math.Round(Z, r);
        }

        public Point3 Rounded(int r = 2)
        {
            return new Point3(
            Math.Round(X, r)
            , Math.Round(Y, r)
           , Math.Round(Z, r));
        }



        public override bool Equals(object obj)
        {
            Point3 p = obj as Point3;
            return p.X == X && p.Y == Y && p.Z == X;
        }

        public static Point3 Zero = new Point3(0, 0, 0);

        public static Point3 operator +(Point3 p1, Point3 p2)
        {
            return new Point3(p1.X + p2.X, p1.Y + p2.Y, p1.Z + p2.Z);
        }

        public static Point3 operator -(Point3 p1, Point3 p2)
        {
            return new Point3(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        }

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