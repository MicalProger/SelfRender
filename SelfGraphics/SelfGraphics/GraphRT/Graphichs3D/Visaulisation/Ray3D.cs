using SelfGraphics.LowGraphics;
using System.Linq;
using SelfGraphics.GraphRT.Graphics2D;

namespace SelfGraphics.GraphRT.Graphics3D
{
    public class Ray3D
    {
        public double L => GetPointByDist(10).X - Source.X;
        
        public double M => GetPointByDist(10).Y - Source.Y;

        public double N => GetPointByDist(10).Z - Source.Z;


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

        public Point3 GetPointByDist(double distance)
        {
            Point3 p = new Point3(Source.X, Source.Y, Source.Z);
            Ray2D tmpRay = new Ray2D(new Point2(Source.X, Source.Y), Direct.XRotation);
            var a = tmpRay.GetPixelByLen(distance);
            p += new Point3(a.X, a.Y);
            tmpRay = new Ray2D(new Point2(Source.Y, Source.Z), Direct.YRotation);
            a = tmpRay.GetPixelByLen(distance);
            p += new Point3(0, 0, a.Y);
            return p;
        }
        
        public Point3 GetEndpoint(Scence space)
        {
            Point3 final = Point3.Zero;
            var colls = space.objs.Select(p => p.GetCollision(this)).Where(i => i != null || ! i.Equals( Point3.Zero)).OrderBy(p => p.GetDistanceTo(Source)).ToList();
            return final;
        }
    }
}