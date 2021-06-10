using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using SelfGraphics.GraphRT.Graphics2D;
using SFML.System;

namespace SelfGraphics.LowGraphics
{
    class Line : Prim
    {
        public double Ang
        {
            get => Tools.GetAngle(point1, point2);
            
        }

        Point2 point1;
        private Point2 point2;
        private Color col;

        public Line(Point2 start, double length, double direction)
        {
            direction /= 180;
            direction *= Math.PI;
            point1 = start;
            point2 = start.ChangedFor(Math.Sin(direction) * length, Math.Cos(direction) * length);
        }

        public Line(Point2 s, Point2 e, Color color)
        {
            point2 = e;
            point1 = s;
            this.col = color;
        }

        public void ResetPoint(Point2 p, int pointN)
        {
            switch (pointN)
            {
                case 1:
                    point1 = p;
                    break;
                default:
                    point2 = p;
                    break;
            }
        }

        public override Point2 GetCollision(Ray ray)
        {
            try
            {
                decimal x1, x2, x3, x4, y1, y2, y3, y4 = Decimal.Zero;
                x1 = (decimal) point1.X;
                x2 = (decimal) point2.X;
                x3 = (decimal) ray.Source.X;
                x4 = (decimal) (ray.Source.X + Math.Sin(Tools.ToRads(ray.angle)) * 3);
                y1 = (decimal) point1.Y;
                y2 = (decimal) point2.Y;
                y3 = (decimal) ray.Source.Y;
                y4 = (decimal) (ray.Source.Y + Math.Cos(Tools.ToRads(ray.angle)) * 3);
                decimal upper = Decimal.Zero;
                upper = (x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4);
                decimal lower = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
                if (lower == 0) return null;
                upper = upper / lower;
                Point2 final = new Point2((double) upper, 0);
                upper = (x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4);
                final.Y = (double) (upper / lower);
                final.Color = Color.White;
                final.tag = "Pnt pr:{" + tag + "}";
                List<double> xs = new List<double>() {point1.X, point2.X};
                List<double> ys = new List<double>() {point1.Y, point2.Y};
                if (final.X >= xs.Min() && final.X <= xs.Max() && final.Y >= ys.Min() && final.Y <= ys.Max()) return final;
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public override List<Point2> GetPixels()
        {
            return pxs;
        }

        public override void DrawPrim(RenderWindow win)
        {
            win.Draw(new Vertex[2] {new Vertex(point1.getVec2f(), col), new Vertex(point2.getVec2f(), col)},
                PrimitiveType.Lines);
        }
    }
}