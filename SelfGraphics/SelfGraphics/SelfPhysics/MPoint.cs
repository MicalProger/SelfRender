using SelfGraphics.LowGraphics;

namespace SelfGraphics.SelfPhysics
{
    public class MPoint : PhysicalPrim
    {
        public Point2 current;
        
        public MPoint(Point2 point2, double mass, Point2 speed, bool isStatic=false)
        {
            IsStatic = isStatic;
            Mass = mass;
            Speed = speed;
            Position = point2;
        }
    }
}