using System;
using System.Collections.Generic;
using System.Linq;
using SelfGraphics.GraphRT.Graphics2D;
using SelfGraphics.LowGraphics;
using SFML.Graphics;

namespace SelfGraphics.GraphRT
{
    public class Camera
    {
        public Camera(double angle, Point2 position, double fow, Grid imageGrid)
        {
            Angle = angle;
            Position = position;
            FOW = fow;
            ImageGrid = imageGrid;
        }

        public double Angle;

        public Point2 Position;

        public double FOW;

        public Grid ImageGrid;

        public List<Point2> RenderGrid(int rays, bool vis, double matrixSize)
        {
            Console.WriteLine("Start");
            Grid localGrid = new Grid(1000, 1000);
            localGrid.SetBorder(Color.Green);
            List<Point2> renders = new List<Point2>();
            List<Ray> rayCasts = new List<Ray>();
            int counter = 0;
            double step;
            for (double i = -FOW / 2; i < FOW / 2; i += FOW / rays)
            {
                Ray tmpRay = new Ray(Position, Angle + i) {grid = ImageGrid};
                var tmpPoint = tmpRay.GetEndpoint();
                tmpPoint.SetLenTo(Position);
                //tmpRay.grid = localGrid;
                //tmpRay.Angle = i;
                //tmpRay.Source = new Point2(500, ( 1 / Math.Tan(Tools.ToRads(i))) * matrixSize / 2);
                //var correction = tmpRay.GetEndpoint().GetLenTo(tmpRay.Source);
                //tmpPoint.Len -= correction;
                tmpPoint.Len *= Math.Cos(Tools.ToRads(i));
                // Console.WriteLine(Math.Cos(Tools.ToRads(i)));
                renders.Add(tmpPoint);
            }
            if (vis)
            {
                foreach (var point in renders
                    )
                {
                    ImageGrid.AddPrim(new Line(Position, point, Color.Yellow), 2);
                }
            }
            Console.WriteLine("End");
            return renders;
        }
    }
}