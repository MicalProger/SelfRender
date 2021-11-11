using SelfGraphics.LowGraphics;

namespace SelfGraphics.SelfPhysics
{
    public abstract class PhysicalPrim
    {
        public Point2 LocalBoost;
        
        public bool IsStatic;

        public double Mass;
        
        public Point2 Position;

        public Point2 Speed;

        public bool IsCollisible;
    }
}