using SelfGraphics.LowGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfGraphics.GraphRT.Graphics2D
{
    class Ray
    {
        public Point2 target;

        public Point2 Source;

        public double angle;

        public Grid grid;

        public Ray(Point2 start, Point2 dirction)
        {
            Source = start;
            angle = Math.Atan(dirction.Y / dirction.X) / Math.PI * 180;
        }

        public Ray(Grid grid)
        {
            this.grid = grid;
        }

        public Ray(Point2 start, double angle)
        {
            Source = start;
            this.angle = angle;
        }

        public void Launch(bool visualize)
        {
            // var katet = Tools.ToRads(angle);
            // var currentPoint = Source.Copy();
            // for (double i = 0; i < double.PositiveInfinity; i += 0.5)
            // {
            //     currentPoint = Source.ChangedFor(Math.Sin(katet) * i, Math.Cos(katet) * i);
            //     currentPoint.Round();
            //     if (grid.IsPoint(currentPoint, 0))
            //     {
            //         target = currentPoint;
            //         break;
            //     }
            //     if (visualize) grid.SetPoint(currentPoint);
        }

        public Point2 GetEndpoint()
        {
            Launch();
            return target;
        }

        public Point2 GetPixelByLen(double len)
        {
            double s, c = 0;
            s = Math.Sin(Tools.ToRads(angle));
            c = Math.Cos(Tools.ToRads(angle));
            return Source + new Point2(s * len, c * len);
        }

        public void Launch()
        {
            List<Point2> cols = grid.GetLayer(1).Select(p => p.GetCollision(this)).Where(p => p != null).ToList();
            Console.WriteLine(cols.Count);
            //cols.ForEach(p => Console.WriteLine(Tools.GetAngle(p, Source)));
            target = cols.Count == 0 ? Point2.Zero : cols.OrderBy(p => p.GetLenTo(Source)).First();
            Console.WriteLine(target.tag);
        }
    }
}