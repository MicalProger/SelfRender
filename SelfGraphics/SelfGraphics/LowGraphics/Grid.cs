using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfGraphics.LowGraphics
{
    class Grid
    {
        List<List<Prim>> layers = new List<List<Prim>>() {new List<Prim>(), new List<Prim>()};
        public List<Prim> GetLayer(int layer) => layers[layer];

        Color color;

        uint w;

        uint h;

        public Grid(uint wight, uint height, Color standart)
        {
            color = standart;
            h = height;
            w = wight;
        }

        public Grid LoadGrid(string filePath)
        {
            StreamReader sr = new StreamReader(filePath);
            List<string> props = sr.ReadLine()?.Split(':').ToList();
            Grid local = new Grid(Convert.ToUInt32(props[0]), Convert.ToUInt32(props[1]),
                new Color(Convert.ToByte(props[2]),
                    Convert.ToByte(props[3]), Convert.ToByte(props[4])));
            foreach (var o in File.ReadAllLines(filePath))
            {
                var prms = o.Split('-');
                switch (prms[0])
                {
                    case "Line":
                        var linePr = prms[1].Split(':').ToList();

                        break;
                    case "Rect":
                        break;
                    case "Circle":
                        break;
                    case "Point":
                        break;
                    default:
                        break;
                }
            }

            return local;
        }

        public Point2 GetSamePoint(Point2 pos, int lay = 0)
        {
            return layers[lay].FirstOrDefault(pr => pr.GetPixels().Any(p => p.Equals(pos)))?.GetPixels().First(p => p.Equals(pos));
        }

        public Grid(string path, Color transparent)
        {
            Image cur = new Image(File.ReadAllBytes(path));
            h = cur.Size.Y;
            w = cur.Size.X;
            foreach (var item in Tools.DoubleRangePos(h, h))
            {
                if (cur.GetPixel((uint) item[0], (uint) item[1]) != transparent)
                    layers[0].Add(new Point2(item[0], item[1]) {Color = cur.GetPixel((uint) item[0], (uint) item[1])});
            }
        }
        
        public void Clear()
        {
            layers = new List<List<Prim>>() {new List<Prim>(), new List<Prim>()};
        }

        public void ClearLayer(int layer)
        {
            layers[layer] = new List<Prim>();
        }

        public void SetPoint(Point2 p, int layer = 1)
        {
            layers[layer].Add(p);
        }

        public void AddLayer()
        {
            layers.Add(new List<Prim>());
        }

        public void SetColor(Color color) => this.color = color;

        public void ShowToScreen(RenderWindow window)
        {
            Image i = new Image(w, h, color);
            foreach (var prims in layers)
            {
                foreach (var data in prims)
                {
                    foreach (var point in data.GetPixels())
                    {
                        try
                        {
                            if (point.X > w || point.X < 0) continue;
                            if (point.Y > h || point.Y < 0) continue;

                            i.SetPixel((uint) point.X, (uint) point.Y, point.Color);
                        }
                        catch
                        {
                            Console.WriteLine(point.ToString());
                        }
                    }
                }
            }
            Texture texture = new Texture(i);
            window.Draw(new Sprite(texture));
        }

        public void AddPrim(Prim obj, int lay=1)
        {
            layers[lay].Add(obj);
        }

        public bool IsPoint(Point2 pos, int i)
        {
            return layers[i].FirstOrDefault(pr => pr.GetPixels().Any(p => p.Equals(pos))) != null;
        }
    }
}