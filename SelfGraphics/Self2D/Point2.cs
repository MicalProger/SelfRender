using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Self2D;

namespace Self2D
{
    public class Point2 : Prim
    {
        public static Point2 Zero = new Point2(0, 0);
        
        public object tag;

        public Color Color = Color.White;

        public double X;

        public double Y;

        public double Len;

        public override bool Equals(object? obj)
        {
            var eqPos = obj as Point2;
            if (eqPos == null) return false;
            return eqPos.X == X && eqPos.Y == Y;
        }

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

        public static Point2 operator +(Point2 p1, Point2 p2)
        {
            return new Point2(p1.X + p2.X, p1.Y + p2.Y);
        }
        
        
        public static Point2 operator -(Point2 p1, Point2 p2)
        {
            return new Point2(p1.X - p2.X, p1.Y - p2.Y);
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

        public override Point2 GetCollision(Ray2D ray2D)
        {
            var dir = Tools.ToRads(ray2D.angle);
            SetLenTo(ray2D.Source);
            if (ray2D.Source + new Point2(Math.Sin(dir) * Len, Math.Cos(dir) * Len) == this) return this;
            return null;

        }

        public override List<Point2> GetPixels()
        {
            throw new NotImplementedException();
        }

        public override void DrawPrim(RenderWindow win)
        {
            win.Draw(new Vertex[1]{new Vertex(new((float) X, (float) Y))}, PrimitiveType.Points);
        }

        public Vector2f getVec2f()
        {
            return new Vector2f((float) X, (float) Y);
        }
    }
}
