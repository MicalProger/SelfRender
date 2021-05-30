using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using SFML.System;

namespace SelfGraphics.LowGraphics
{
    class Line : Prim
    {
        public double Ang
        {
            get => Tools.GetAngle(point1, point2);
            set => throw new NotImplementedException();
        }
        
        Point2 point1;
        private Point2 point2;
        private Color col;

        public Line(Point2 start, double length, double direction)
        {
            Ang = direction;
            direction /= 180;
            direction *= Math.PI;
            point1 = start;
            point2 = start.ChangedFor(Math.Sin(direction) * length, Math.Cos(direction) * length);
        }

        public Line(Point2 s, Point2 e, Color color)
        {
            point2 = e;
            point1 = s;
            this.col = color;
        }

        public void ResetPoint(Point2 p, int pointN)
        {
            switch (pointN)
            {
                case 1:
                    point1 = p;
                    break;
                default:
                    point2 = p;
                    break;
            }
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

        public override void DrawPrim(RenderWindow win)
        {
            win.Draw(new Vertex[2]{new Vertex(point1.getVec2f(), col), new Vertex(point2.getVec2f(), col) }, PrimitiveType.Lines);
        }
    }
}
