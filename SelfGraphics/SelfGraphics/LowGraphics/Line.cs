using SFML.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace SelfGraphics.LowGraphics
{
    class Line
    {
        Grid grid;
        Point2 point1;
        Point2 point2;
        public Line(Point2 s, Point2 e, Grid grid)
        {
            this.grid = grid;
            point2 = e;
            point1 = s;
        }
        public List<Point2> GetLine(int iter)
        {
            List<List<Point2>> pairs = new List<List<Point2>>() { new List<Point2>() { point1, point2 } };
            List<List<Point2>> newp;
            for (int i = 0; i < iter; i++)
            {
                newp = new List<List<Point2>>();
                foreach (var points in pairs)
                {
                    var middle = new Point2(points);
                    newp.Add(new List<Point2>{points.First(), middle });
                    newp.Add(new List<Point2> { points.Last(), middle });
                }
                pairs = newp;
            }
            var f = pairs.Select(p => p.First()).ToList();
            f.Add(pairs.Last().Last());
            return f;
        }
        public void SetLine(int iter, Color color)
        {
            foreach (var item in this.GetLine(iter))
            {
                grid.SetPoint(item);
            }
        }
    }
}
