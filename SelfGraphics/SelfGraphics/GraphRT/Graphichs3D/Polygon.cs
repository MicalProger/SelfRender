using System.Collections.Generic;
using System.Numerics;

namespace SelfGraphics.GraphRT.Graphichs3D
{
    public class Polygon
    {
        public Point3 Normal;

        public List<Point3> vertexs;

        public Polygon(List<Point3> vertexes)
        {
            vertexs = vertexes;
        }
        
        public Polygon(){}
        
        public Point3 GetCollision(Ray3D ray)
        {
            Point3 fin = Point3.Zero;

            return fin;
        }
    }
}