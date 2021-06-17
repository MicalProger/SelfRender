using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using SFML.Graphics;

namespace SelfGraphics.GraphRT.Graphics3D
{
    public class DotObjLoader
    {
        public static List<Claster> LoadObj(string file)
        {
            List<Claster> full = new List<Claster>();
            Claster claster = new Claster();
            List<Point3> verts = new List<Point3>();
            List<Point3> normals = new List<Point3>();
            string mtlFile = string.Empty;
            Color currentColor = new Color();
            foreach (var parameter in File.ReadAllLines(file))
            {
                var tag = parameter.Split(' ')[0];
                switch (tag)
                {
                    case "mtllib":
                        mtlFile = parameter.Split(' ')[1];
                        break;
                    case "o":
                        full.Add(new Claster());
                        full.Last().Tag = parameter.Split(' ')[1];
                        break;
                    case "v":
                        verts.Add(new Point3(parameter.Substring(2)));
                        break;
                    case  "vn":
                        normals.Add(new Point3(parameter.Substring(3)));
                        break;
                    case "usemtl":
                        currentColor = MtlReader.GetColor(mtlFile, parameter.Split(' ')[1]);
                        break;
                    case "f":
                        Polygon tmpPoligon = new Polygon(parameter.Split(' ').Skip(1).Take(3)
                            .Select(i => verts[Int32.Parse(i.Split(@"/")[0]) - 1]).ToList());
                        var index = parameter.Split(' ')[1].Split(@"/");
                        tmpPoligon.Normal = normals[Int32.Parse(index[2]) - 1];
                        tmpPoligon.Color = currentColor;
                        full.Last().Poligons.Add(tmpPoligon);
                        break;
                }

            }
            return full;
        }
    }
}