using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfGraphics.LowGraphics
{
    class Point2
    {
        public object tag;

        public Color Color = Color.White;

        public double X;

        public double Y;

        public double Len;

        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        public Point2(Vector2i vec)
        {
            X = (uint)vec.X;
            Y = (uint)vec.Y;
        }

        public Point2(List<Point2> points)
        {
            X = Math.Round(Tools.AVG(points.Select(p => p.X).ToList()));
            Y = Math.Round(Tools.AVG(points.Select(p => p.Y).ToList()));
        }

        public Point2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Point2 Copy()
        {
            return new Point2(X, Y) {Len = this.Len };
        }

        public void ChangeFor(Point2 point)
        {
            X += point.X;
            Y += point.Y;
        }

        public Point2 ChangedFor(double xVal, double yVal)
        {
            return  new Point2(X + xVal, Y + yVal) { Len = this.Len, Color = this.Color };
        }


        public Point2 Rounded()
        {
            this.Round();
            return this;
        }
        public void Round()
        {
            X = Math.Round(X);
            Y = Math.Round(Y);
        }

        public Point2 Sub(Point2 p1, Point2 p2) => new Point2(Math.Abs(p1.X - p2.X), Math.Abs(p2.Y - p1.Y));

        public void SetLenTo(Point2 point) => Len = Math.Sqrt(Math.Pow(Sub(this, point).X, 2) + Math.Pow(Sub(this, point).Y, 2));

        public double GetLenTo(Point2 point)
        {
            if (Len == 0)
            {
                SetLenTo(point);
                return Len;
            }
            else
                return Len;

        }
    }
}
