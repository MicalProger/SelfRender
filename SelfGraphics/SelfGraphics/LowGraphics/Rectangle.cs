using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfGraphics.LowGraphics
{
    class Rectangle : Prim
    {
        Grid grid;

        Point2 startPos;

        double W;

        double H;

        public Rectangle(Point2 pos, double wigth, double heigth, Grid grid)
        {
            this.grid = grid;
            startPos = pos;
            W = wigth;
            H = heigth;
        }


        public void SetFromCenter(Point2 center, Color color)
        {
            center.Color = color;
            foreach (var item in Tools.DoubleRange(W / 2, H / 2))
            {
                grid.SetPoint(center.ChangedFor(item[0], item[1]));
            }
        }
        public void SetRect(Color color, int l=1)
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
            throw new NotImplementedException();
        }

        public override List<Point2> GetPixels()
        {
            throw new NotImplementedException();
        }
    }
}
