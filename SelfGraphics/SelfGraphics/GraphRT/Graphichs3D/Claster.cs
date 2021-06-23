using System.Collections.Generic;
using System.Linq;
using SFML.Graphics;

namespace SelfGraphics.GraphRT.Graphics3D
{
    public class Claster
    {
        public Color Color;
        
        public object Tag;

        public List<Polygon> Poligons;

        public Claster()
        {
            Poligons = new List<Polygon>();
        }

        public override string ToString()
        {
            return $"Name : {Tag} | Total poligions : {Poligons.Count}";
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