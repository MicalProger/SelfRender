using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;

namespace SelfGraphics.GraphRT.Graphics3D
{
    public class Polygon
    {
        public double D => Normal.X * vertexs[0].X + Normal.Y * vertexs[0].Y + Normal.Y * vertexs[0].Y;
        
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
            decimal a, b, c, d, e, f, g, h, j, k, t = Decimal.Zero;
            a = (decimal) Normal.X;
            b = (decimal) Normal.Y;
            c = (decimal) Normal.Z;
            d = (decimal) ray.L;
            f = (decimal) ray.M;
            h = (decimal) ray.N;
            e = (decimal) ray.Source.X;
            g = (decimal) ray.Source.Y;
            j = (decimal) ray.Source.Z;
            k = (decimal) D;
            t = -(a * e + b * g + c * j + k) / (a * d + b * f + c * h);
            fin.X = (double) (d * t + e);
            fin.Y = (double) (f * t + g);
            fin.Z = (double) (h * t + j);
            fin.Color = this.Color;
            if(ray.GetPointByDist(fin.GetDistanceTo(ray.Source)).Rounded().Equals(fin.Rounded()))
                return fin;
            return null;
        }

        public Color Color { get; set; }
    }
}