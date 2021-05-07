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
            var k = Tools.ToRads(data.Ang);
            Ray local = new Ray(RenderData.Position, data.Ang) { grid = RenderData.BaseGrid }; //.ChangedFor(Math.Sin(k * (RenderData.MinLen - 2)), Math.Cos(k * (RenderData.MinLen - 2)))
            var endPoint = local.GetEndpoint(RenderData.len, false);
            if (endPoint != null)
            {
                endPoint.SetLenTo(RenderData.Position);
                endPoint.Len += Math.Sin(data.Ang - RenderData.AbsAngle);
                endPoint.tag = data.index - 1;
                lock (Menenger.Buffer)
                {
                    Menenger.Buffer.Add(endPoint);
                }
            }
            Menenger.count--;

        }

        public void Render(int rayCount, double renderLen)
        {
            Menenger.Buffer.Clear();
            RenderData.AbsAngle = angle;
            RenderData.Position = Position;
            RenderData.BaseGrid = grid;
            RenderData.len = renderLen;
            List<double> angles = new List<double>();

            for (double i = -FOW / 2; i < FOW / 2; i += FOW / rayCount)
            {
                angles.Add(angle + i);
            }
            Menenger.todo = RenderPoint;
            foreach (var item in angles)
            {
                Menenger.AddThread(new RenderData(item) { index = angles.IndexOf(item) });
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
            Menenger.todo = RenderPoint;
            foreach (var item in angles)
            {
                Menenger.AddThread(new RenderData(item) { index = angles.IndexOf(item) });
            }
            while (Menenger.count != 0) continue;
            var f = Menenger.Buffer.Select(b => b as Point2).ToList();
            Menenger.Buffer.Clear();
            return f;


        }
    }

    class RenderData
    {
        public static double AbsAngle;

        public int index;

        public static Point2 Position;

        public double Ang;

        public static Grid BaseGrid;

        public static double len;

        public RenderData(double aAg)
        {
            Ang = aAg;
        }
    }

}
