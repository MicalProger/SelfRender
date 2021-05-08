using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SelfGraphics.LowGraphics
{
    class Line : Prim
    {
        void countPxs()
        {
            Center = new Point2(new List<Point2>() {point1, point2});
            var finalList = new List<Point2>();
            var k = Tools.ToRads(Ang);
            var xLen = Math.Sin(k);
            var yLen = Math.Cos(k);
            point1.SetLenTo(point2);
            for (double i = 0; i < point1.Len; i += 1.2)
            {
                var local = point1.ChangedFor(xLen * i, yLen * i);
                local.Round();
                if(finalList.Contains(local) == false) finalList.Add(local);
            }

            pxs = finalList;
        }
        public double Ang
        {
            get => Tools.ToRads(Math.Acos((point1.X - point2.X) / (point1.Y - point2.Y)));
            set => throw new NotImplementedException();
        }
        
        Point2 point1;
        private Point2 point2;

        public Line(Point2 start, double length, double direction)
        {
            Ang = direction;
            direction /= 180;
            direction *= Math.PI;
            point1 = start;
            point2 = start.ChangedFor(Math.Sin(direction) * length, Math.Cos(direction) * length);
            countPxs();
        }

        public Line(Point2 s, Point2 e)
        {
            point2 = e;
            point1 = s;
            countPxs();
        }

        public override ColideState IsContain(Point2 point)
        {
            var verts = new List<double>() {point1.Y, point2.Y};
            var hors = new List<double>() {point1.X, point2.X};
            if (!(verts.Min() <= point.X && point.X >= verts.Max())) return ColideState.NonColided;
            if (!(hors.Min() <= point.Y && point.Y >= hors.Max())) return ColideState.NonColided;
            double xPart = hors.Max() - hors.Min() / (point.X - hors.Min());
            if (Math.Abs(verts.Max() - verts.Min() / xPart - point.Y) != 0) return ColideState.HitboxColoided;
            return ColideState.ObjColided;
        }

        public override List<Point2> GetPixels()
        {
            return pxs;
        }
    }
}
