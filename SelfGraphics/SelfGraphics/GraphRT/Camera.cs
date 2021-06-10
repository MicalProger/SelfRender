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

        public List<Point2> RenderGrid(int rays, bool vis)
        {
            List<Point2> renders = new List<Point2>();
            for (double i = -FOW / 2; i < FOW / 2; i += FOW / rays)
            {
                Ray tmpRay = new Ray(Position, Angle + i) {grid = ImageGrid};
                renders.Add(tmpRay.GetEndpoint());
                renders.Last().SetLenTo(Position);
            }
            if (vis)
            {
                foreach (var point in renders)
                {
                    ImageGrid.AddPrim(new Line(Position, point, Color.Yellow), 2);
                }
            }
            return renders;
        }
    }
}