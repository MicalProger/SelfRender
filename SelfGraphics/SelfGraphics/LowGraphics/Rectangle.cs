using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfGraphics.LowGraphics
{
    class Rectangle
    {
        Grid grid;

        Point2 startPos;

        double W;

        double H;

        public Rectangle(Point2 pos, double wigth, double heigth)
        {
            startPos = pos;
            W = wigth;
            H = heigth;
        }

        public void SetRect(Color color)
        {
            for (int i = 0; i < W; i++)
            {
                for (int j = 0; j < H; j++)
                {
                    grid.SetPoint(startPos.ChangedFor(i, j));
                }
            }
        }
    }
}
