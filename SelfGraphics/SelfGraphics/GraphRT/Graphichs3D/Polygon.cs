using System.Collections.Generic;

namespace SelfGraphics.GraphRT.Graphichs3D
{
    public class Polygon : Rendereble
    {
        public List<Point3> vertexs;

        public Polygon(List<Point3> vertexes)
        {
            vertexs = vertexes;
        }
        
        public Polygon(){}
        
        public override Point3 GetCollision(Ray3D ray)
        {
            Point3 fin = Point3.Zero;
            return fin;
        }
    }
}