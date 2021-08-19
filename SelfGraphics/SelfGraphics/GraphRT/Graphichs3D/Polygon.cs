using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using SelfGraphics.LowGraphics;
using SFML.Graphics;

namespace SelfGraphics.GraphRT.Graphics3D
{
    public class Polygon
    {
        public double D => Normal.X * vertexs[0].X + Normal.Y * vertexs[0].Y + Normal.Z * vertexs[0].Z;

        public Point3 Normal;

        public List<Point3> vertexs;

        public Polygon(List<Point3> vertexes)
        {
            vertexs = vertexes;
        }

        public Polygon()
        {

        }

        public Point3 GetCollision(Ray3D ray)
        {
            Point3 fin = Point3.Zero;
            decimal a, b, c, d, e, f, g, h, j, k, t = Decimal.Zero;
            a = (decimal) Normal.X;
            b = (decimal) Normal.Y;
            c = (decimal) Normal.Z;
            d = (decimal) ray.Direct.L;
            f = (decimal) ray.Direct.M;
            h = (decimal) ray.Direct.N;
            e = (decimal) ray.Source.X;
            g = (decimal) ray.Source.Y;
            j = (decimal) ray.Source.Z;

            k = (decimal) D;
            try
            {
                var A = -(a * e + b * g + c * j + k);
                var B = (a * d + b * f + c * h);
                if (A != 0 && B == 0) return null;
                t = A / B;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }

            fin.X = (double) (d * t + e);
            fin.Y = (double) (f * t + g);
            fin.Z = (double) (h * t + j);
            fin.Color = this.Color;
            if (Normal.Z != 0)
            {
                if (!Tools.IsInPolygon(vertexs.Select(p => new Point2(p.X, p.Y)).ToArray(), new Point2(fin.X, fin.Y)))
                    return null;
            }
            else if (Normal.Y != 0)
            {
                if (!Tools.IsInPolygon(vertexs.Select(p => new Point2(p.X, p.Z)).ToArray(), new Point2(fin.X, fin.Z)))
                    return null;
            }
            else
            {
                if (!Tools.IsInPolygon(vertexs.Select(p => new Point2(p.Y, p.Z)).ToArray(), new Point2(fin.Y, fin.Z)))
                    return null;
            }

            return fin;
        }
        public Color Color { get; set; }
    }
}