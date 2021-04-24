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

    enum RenderMode
    {
        SingleRender = 1,
        AlwaysRender = 2
    }
    class Camera
    {
        RenderMode options;

        public Grid grid;

        public double FOW;

        public Point2 Position;

        double angle;
        public void SetRoration(double value) => angle = value;

        public double GetRotation() => angle;

        public void Rotate(double value) => angle += value;


        public Camera(Point2 pos, double angle, double FOW, RenderMode mode)
        {
            options = mode;
            this.FOW = FOW;
            Position = pos;
            this.angle = angle;
        }

        public static void RenderPoint(object renderData)
        {
            var data = renderData as RenderData;
            Ray local = new Ray(RenderData.Position, data.Ang) { grid = RenderData.BaseGrid };
            var endPoint = local.GetEndpoint(RenderData.len, true);
            if (endPoint != null)
            {
                endPoint.SetLenTo(RenderData.Position);
                endPoint.tag = data.index;
                lock (Manager.Buffer)
                {
                    Manager.Buffer.Add(endPoint);
                }
            }
            Manager.count--;
            
        }

        public void Render(int rayCount, double renderLen)
        {
            Manager.Buffer.Clear();
            RenderData.Position = Position;
            RenderData.BaseGrid = grid;
            RenderData.len = renderLen;
            List<double> angles = new List<double>();
            var c = 0;
            for (double i = -FOW / 2; i < FOW / 2; i += FOW / rayCount)
            {
                angles.Add(angle + i);
            }
            Manager.todo = RenderPoint;
            foreach (var item in angles)
            {
                Manager.AddThread(new RenderData(item) { index = angles.IndexOf(item) });
            }
        }

        public List<Point2> GetImage2(int count, double len = double.PositiveInfinity, bool stop = false)
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
            Manager.todo = RenderPoint;
            foreach (var item in angles)
            {
                Manager.AddThread(new RenderData(item) { index = angles.IndexOf(item) });
            }
            while (Manager.count != 0) continue;
            var f = Manager.Buffer.Select(b => b as Point2).ToList();
            Manager.Buffer.Clear();
            return f;
            

        }
    }

    class RenderData
    {
        public int index;

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
