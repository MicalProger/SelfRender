using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelfGraphics.LowGraphics;
using SFML.Graphics;

namespace SelfGraphics.GraphRT.Graphics3D.Visualization
{
    class Camera3D
    {
        public Camera3D(double fow, Point3 position, Direction direct)
        {
            FOW = fow;
            Position = position;
            Direct = direct;
        }

        public double FOW;
        
        public Point3 Position;

        public Direction Direct;

        public Texture RenderImage(Scence scence, int x, int y)
        {
            Image img = new Image((uint) x, (uint) y, Color.Black);
            var FOWV = FOW / x / y;
            uint xPos = 0;
            uint yPos = 0;
            for (double i = -FOW / 2; i < FOW / 2; i += FOW / x)
            {
                for (double j = -FOWV / 2; j < FOWV / 2; j += FOWV / x)
                {
                    Ray3D tmpRay = new Ray3D(Position, Direct + new Direction(i, j), 1); 
                    var p = tmpRay.GetEndpoint(scence);
                    img.SetPixel(xPos, yPos, p.Color);
                    yPos++;
                    var cPos = Console.GetCursorPosition();
                    Console.SetCursorPosition(0, cPos.Top);
                    Console.Write($"Line {xPos} of {x} is ready (y is {yPos})");
                }
                xPos++;
                yPos = 0;
                Console.WriteLine();
            }
            return new Texture(img);
        }
    }
}
