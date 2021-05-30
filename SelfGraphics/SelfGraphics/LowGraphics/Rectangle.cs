using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public override ColideState IsContain(Point2 point)
        {
            if (startPos.X <= point.X && startPos.ChangedFor(W, H).X >= point.X)
            {
                if (startPos.Y <= point.Y && startPos.ChangedFor(W, H).Y >= point.Y)
                    return ColideState.ObjColided;
            }

            return ColideState.NonColided;
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
