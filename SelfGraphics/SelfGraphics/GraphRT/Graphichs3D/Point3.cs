using SelfGraphics.LowGraphics;
using System;
using System.Diagnostics;
using System.Globalization;
using SelfGraphics.GraphRT.Graphics2D;
using SFML.Graphics;

namespace SelfGraphics.GraphRT.Graphics3D
{
    public enum Axis
    {
        X = 1,
        Y = 2,
        Z = 3
    }

    public class Point3
    {
        public double Distance;

        public Color Color;

        public void SetRotate(Axis axis, double angle)
        {
            Point2 flatVector;
            double flatLen;
            switch (axis)
            {
                case Axis.X:
                    flatLen = Point2.Zero.GetLenTo(new Point2(Y, Z));
                    flatVector = new Ray2D(Point2.Zero, angle).GetPixelByLen(flatLen);
                    Y = -flatVector.X;
                    Z = -flatVector.Y;
                    break;
                case Axis.Y:
                    flatLen = Point2.Zero.GetLenTo(new Point2(X, Z));
                    flatVector = new Ray2D(Point2.Zero, angle).GetPixelByLen(flatLen);

                    X = -flatVector.X;
                    Z = -flatVector.Y;
                    break;
                case Axis.Z:
                    flatLen = Point2.Zero.GetLenTo(new Point2(X, Y));
                    flatVector = new Ray2D(Point2.Zero, angle).GetPixelByLen(flatLen);
                    X = -flatVector.X;
                    Y = -flatVector.Y;
                    break;
            }
        }

        public Point2 GetPoint2()
        {
            return new(X, Y) { Color = Color };
        }

        public void SetDistanceTo(Point3 p)
        {
            Point3 d = (this - p).Absoluted();
            var s1 = Math.Sqrt(Math.Pow(d.X, 2) + Math.Pow(d.Y, 2) + Math.Pow(d.Z, 2));
        }

        public Direction GetDirectionTo(Point3 p)
        {
            Direction dir = new Direction(0, 0, 0);
            var x = Tools.GetAngle(new(X, Y), new(p.X, p.Y));
            var y = Tools.GetAngle(new(Y, Z), new(p.Y, p.Z));
            dir.XRotation = x;
            dir.YRotation = y;
            return dir;
        }

        public double GetDistanceTo(Point3 p)
        {
            SetDistanceTo(p);
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
            X = X.Round(r);
            Y = Y.Round(r);
            Z = Z.Round(r);
        }

        public Point3 Rounded(int r = 2)
        {
            return new Point3(
                X.Round(r),
                Y.Round(r),
                Z.Round(r));
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