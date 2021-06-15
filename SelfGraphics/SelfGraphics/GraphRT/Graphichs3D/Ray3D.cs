namespace SelfGraphics.GraphRT.Graphichs3D
{
    public class Ray3D
    {
        public Point3 Target;

        public Point3 Source;
        
        public int MaxReflection;
        
        public Direction Direct;
        
        public Ray3D(Point3 source, Direction direct, int maxReflection=1)
        {
            Source = source;
            Direct = direct;
            MaxReflection = maxReflection;
        }
    }
}