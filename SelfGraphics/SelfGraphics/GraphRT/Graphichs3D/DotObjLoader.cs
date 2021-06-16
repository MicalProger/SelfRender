using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SelfGraphics.GraphRT.Graphichs3D
{
    public class DotObjLoader
    {
        public static List<Claster> LoadObj(string file)
        {
            List<Claster> full = new List<Claster>();
            Claster claster = new Claster();
            List<Point3> verts = new List<Point3>();

            foreach (var parameter in File.ReadAllLines(file))
            {
                var tag = parameter.Split(' ')[0];
                switch (tag)
                {
                    case "o":
                        full.Add(new Claster());
                        
                        full.Last().Name = parameter.Split(' ')[1];
                        break;
                    case "v":
                        verts.Add(new Point3(parameter.Substring(2)));
                        break;
                    case "f":
                        Polygon tmpPoligon = new Polygon( parameter.Split(' ').Skip(1).Take(3)
                            .Select(i => verts[Int32.Parse(i.Split(@"/")[0]) - 1]).ToList());
                        full.Last().Poligons.Add(tmpPoligon);
                        break;
                }

            }
            return full;
        }
    }
}