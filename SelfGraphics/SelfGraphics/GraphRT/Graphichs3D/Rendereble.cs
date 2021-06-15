using SFML.Graphics;

namespace SelfGraphics.GraphRT.Graphichs3D
{
    public abstract class Rendereble
    {

        public string Name;
        
        public Color BaseColor;
        
        public Point3 Position;

        public abstract Point3 GetCollision(Ray3D ray);
        
        

    }
}