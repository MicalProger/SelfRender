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
        public double Len;
        public Point2 Sub(Point2 p1, Point2 p2) => new Point2(Math.Abs(p1.X - p2.X), Math.Abs(p2.Y - p1.Y), grid);

        public void SetLenTo(Point2 point) => Len = Math.Sqrt(Math.Pow(Sub(this, point).X, 2) + Math.Pow(Sub(this, point).Y, 2));
        public Color Color = Color.White;
        Grid grid;
        public double X;
        public double Y;
        public Point2(double x, double y, Grid grid)
        {
            this.grid = grid;
            X = x;
            Y = y;
        }

        public Point2(Vector2i vec)
        {
            X = (uint)vec.X;
            Y = (uint)vec.Y;
        }

        public Point2(List<Point2> points)
        {
            grid = points.First().grid;
            X = Math.Round(Tools.AVG(points.Select(p => p.X).ToList()));
            Y = Math.Round(Tools.AVG(points.Select(p => p.Y).ToList()));
        }

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

        public void Add()
        {
            grid.SetPoint(this);
        }



    }
}
