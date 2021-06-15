using System.Collections.Generic;

namespace SelfGraphics.GraphRT.Graphichs3D
{
    public class Claster : Rendereble
    {

        public List<Polygon> Poligons;

        public Claster()
        {
            Poligons = new List<Polygon>();
        }
        
        public override Point3 GetCollision(Ray3D ray)
        {
            throw new System.NotImplementedException();
        }
    }
}