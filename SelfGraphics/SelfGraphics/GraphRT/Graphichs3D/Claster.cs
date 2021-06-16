using System.Collections.Generic;
using System.Linq;

namespace SelfGraphics.GraphRT.Graphichs3D
{
    public class Claster
    {

        public object Tag;

        public List<Polygon> Poligons;

        public Claster()
        {
            Poligons = new List<Polygon>();
        }
        
        public Point3 GetCollision(Ray3D ray)
        {
            List<Point3> polygons = Poligons.Select(p => p.GetCollision(ray)).Where(p => p != null).ToList();
            if (polygons.Count == 0) return null;
            polygons = polygons.OrderBy(p => p.GetDistanceTo(ray.Source)).ToList();
            return polygons.First();
        }
    }
}