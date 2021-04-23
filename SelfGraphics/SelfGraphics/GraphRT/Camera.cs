using SelfGraphics.GraphRT.Graphics2D;
using SelfGraphics.LowGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SelfGraphics.GraphRT
{
    class Camera
    {

        public Grid grid;

        public double FOW;

        public Point2 Position;

        double angle;
        public void SetRoration(double value) => angle = value;

        public double GetRotation() => angle;

        public void Rotate(double value) => angle += value;


        public Camera(Point2 pos, double angle, double FOW)
        {

            this.FOW = FOW;
            Position = pos;
            this.angle = angle;
        }

        public static void RenderPoint(object renderData)
        {
            var data = renderData as RenderData;
            Ray local = new Ray(RenderData.Position, data.Ang) { grid = RenderData.BaseGrid };
            var endPoint = local.GetEndpoint(RenderData.len, false);
            lock (Menenger.Buffer)
            {
                Menenger.Buffer.Add(endPoint);
            }
            Menenger.count--;
        }

        public List<Point2> GetImage2(int count, double len = double.PositiveInfinity)
        {
            RenderData.Position = Position;
            RenderData.BaseGrid = grid;
            RenderData.len = len;
            List<double> angles = new List<double>();
            var c = 0;
            for (double i = -FOW / 2; i < FOW / 2; i += FOW / count)
            {
                angles.Add(angle + i);
            }
            Menenger.todo = RenderPoint;
            foreach (var item in angles)
            {
                Menenger.AddThread(new RenderData(item));
            }
            while (Menenger.Buffer.Count != angles.Count) continue;
            var f = Menenger.Buffer.Select(b => b as Point2).ToList();
            Menenger.Buffer.Clear();
            return f;
            
        }
    }

    class RenderData
    {
        static public Point2 Position;

        public double Ang;

        public static Grid BaseGrid;

        public static double len;

        public RenderData(double aAg)
        {
            this.Ang = aAg;
        }
    }

}
