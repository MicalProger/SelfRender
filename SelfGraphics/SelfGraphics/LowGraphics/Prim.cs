using System.Collections.Generic;
using SelfGraphics.GraphRT.Graphics2D;
using SFML.Graphics;

namespace SelfGraphics.LowGraphics
{
    public abstract class Prim
    {
        public Point2 Center;
        
        public List<Point2> pxs;
        
        public object tag;

        public abstract Point2 GetCollision(Ray2D ray2D);
        
        public abstract List<Point2> GetPixels();

        public abstract void DrawPrim(RenderWindow win);
    }
}