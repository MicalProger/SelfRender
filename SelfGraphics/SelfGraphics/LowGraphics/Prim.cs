using System.Collections.Generic;
using SFML.Graphics;

namespace SelfGraphics.LowGraphics
{

    enum ColideState
    {
        ObjColided = 1,
        HitboxColoided = 2,
        NonColided = 3
    }
    abstract class Prim
    {
        public Point2 Center;
        
        public List<Point2> pxs;
        
        public object tag;

        public abstract ColideState IsContain(Point2 point);
        
        public abstract List<Point2> GetPixels();

        public abstract void DrawPrim(RenderWindow win);
    }
}