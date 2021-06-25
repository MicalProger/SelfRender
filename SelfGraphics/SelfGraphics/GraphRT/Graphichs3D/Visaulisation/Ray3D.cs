using SelfGraphics.LowGraphics;
using System.Linq;
using SelfGraphics.GraphRT.Graphics2D;
using System;

namespace SelfGraphics.GraphRT.Graphics3D
{
    public class Ray3D
    {
        public double L => Math.Sqrt(Math.Pow(Math.Cos(Tools.ToRads(Direct.YRotation)), 2) + Math.Pow(Math.Cos(Tools.ToRads(Direct.ZRotation)), 2)) / Math.Sqrt(2);
        
        public double M => Math.Sqrt(Math.Pow(Math.Cos(Tools.ToRads(Direct.XRotation)), 2) + Math.Pow(Math.Cos(Tools.ToRads(Direct.ZRotation)), 2)) / Math.Sqrt(2);

        public double N => Math.Sqrt(Math.Pow(Math.Cos(Tools.ToRads(Direct.YRotation)), 2) + Math.Pow(Math.Cos(Tools.ToRads(Direct.XRotation)), 2)) / Math.Sqrt(2);


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
            p -= new Point3(a.X, a.Y);
            tmpRay = new Ray2D(new Point2(Source.Y, Source.Z), Direct.YRotation);
            a = tmpRay.GetPixelByLen(distance);
            p -= new Point3(0, 0, a.Y);
            return p;
        }
        
        public Point3 GetEndpoint(Scence space)
        {
            Point3 final = Point3.Zero;
            var colls = space.objs.Select(p => p.GetCollision(this)).ToList();
                colls = colls.Where(i => i != null || !
                i.Equals( Point3.Zero)).OrderBy(p => p.GetDistanceTo(Source)).ToList();
            return final;
        }
    }
}