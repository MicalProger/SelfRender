using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelfGraphics.GraphRT.Graphics2D;
using SFML.System;

namespace SelfGraphics.LowGraphics
{
    class Rectangle : Prim
    {
        Grid grid;

        public Point2 startPos;

        public double W;

        public double H;
        private Color col;

        public Rectangle(Point2 startPos, Vector2f sides, Color color)
        {
            this.col = color;
            this.startPos = startPos;
            W = startPos.ChangedFor(sides.X, sides.Y).X;
            H = startPos.ChangedFor(sides.X, sides.Y).Y;
        }


        public void SetFromCenter(Point2 center, Color color)
        {
            center.Color = color;
            foreach (var item in Tools.DoubleRange(W / 2, H / 2))
            {
                grid.SetPoint(center.ChangedFor(item[0], item[1]));
            }
        }

        public void SetRect(Color color, int l = 1)
        {
            startPos.Color = color;
            for (int i = 0; i < W; i++)
            {
                for (int j = 0; j < H; j++)
                {
                    grid.SetPoint(startPos.ChangedFor(i, j), l);
                }
            }
        }

        public override Point2 GetCollision(Ray ray)
        {
            Point2 fin = null;
            List<Line> sides = new List<Line>();
            sides.Add(new Line(startPos, startPos + new Point2(W, 0), col));
            sides.Add(new Line(startPos, startPos + new Point2(0, H), col));
            sides.Add(new Line(startPos + new Point2(W, 0), startPos + new Point2(0, H), col));
            sides.Add(new Line(startPos + new Point2(0, H), startPos + new Point2(W, 0), col));
            var tmpPixels = (from line in sides
                select line.GetCollision(ray)).Where(l => l != null).ToList();
            if (tmpPixels.Count == 0) return null;
            tmpPixels.ForEach(p => p.SetLenTo(ray.Source));
            tmpPixels = tmpPixels.OrderBy(i => i.Len).ToList();
            return tmpPixels.First();
        }
   

        public override List<Point2> GetPixels()
        {
            return new List<Point2>();
        }

        public override void DrawPrim(RenderWindow win)
        {
            VertexArray vrerts = new VertexArray(PrimitiveType.Quads);
            Vertex v = new Vertex();
            v.Color = col;
            v.Position = startPos.getVec2f();
            vrerts.Append(v);
            v.Position = startPos.ChangedFor(W, 0).getVec2f();
            vrerts.Append(v);
            v.Position = startPos.ChangedFor(W, H).getVec2f();
            vrerts.Append(v);
            v.Position = startPos.ChangedFor(0, H).getVec2f();
            vrerts.Append(v);
            win.Draw(vrerts);
        }
    }
}
