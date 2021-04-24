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

        Point2 source;

        public double angle;

        public Grid grid;

        public Ray(Point2 start, Point2 dirction)
        {
            source = start;
            angle = Math.Atan(dirction.Y / dirction.X) / Math.PI * 180;

        }

        public Ray(Grid grid)
        {
            this.grid = grid;
        }

        public Ray(Point2 start, double angle)
        {
            source = start;
            this.angle = angle;
        }

        public void Launch(bool visualize)
        {
            var katet = Tools.ToRads(angle);
            var currentPoint = source.Copy();
            for (double i = 0; i < double.PositiveInfinity; i += 0.5)
            {
                currentPoint = source.ChangedFor(Math.Sin(katet) * i, Math.Cos(katet) * i);
                currentPoint.Round();
                if (grid.IsPoint(currentPoint, 0))
                {
                    target = currentPoint;
                    break;
                }
                if (visualize) grid.SetPoint(currentPoint);
            }
        }

        public void launch(double distance, double step, bool visualize=true )
        {
            var katet = Tools.ToRads(angle);
            var xLen = Math.Sin(katet);
            var yLen = Math.Cos(katet);
            var currentPoint = source.Copy();
            for (double i = 0; i < distance; i += step)
            {
                currentPoint = source.ChangedFor(xLen * i, yLen* i);
                currentPoint.Round();
                if (grid.IsPoint(currentPoint, 0))
                {
                    target = grid.GetSamePoint(currentPoint);
                    break;
                }
                if (visualize) grid.SetPoint(currentPoint);
            }
        }

        public Point2 GetEndpoint(double len, bool isDraw = false)
        {
            launch(len, 1.5, isDraw);
            return this.target;
        }
    }
}
