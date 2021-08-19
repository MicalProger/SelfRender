using SFML.Graphics;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Self2D
{
    public class Grid
    {
        List<List<Prim>> layers = new List<List<Prim>>() {new List<Prim>(), new List<Prim>()};
        public List<Prim> GetLayer(int layer) => layers[layer];

        uint w;

        uint h;

        public Grid(uint wight, uint height)
        {
            h = height;
            w = wight;
        }

        public Grid LoadGrid(string filePath)
        {
            return JsonSerializer.Deserialize<Grid>(File.ReadAllText(filePath));

        }

        public void SetBorder(Color color)
        {
            Line l1 = new Line(new(1, 1), new(1, h - 1), color);
            Line l2 = new Line(new(1, 1), new(w - 1, 1), color);
            Line l3 = new Line(new(w - 1, 1), new(w - 1, h - 1), color);
            Line l4 = new Line(new(1, h - 1 ), new(w - 1, h - 1), color);
            layers[1].AddRange(new List<Prim>(){l1, l2, l3, l4});

        }

        public Point2 GetSamePoint(Point2 pos, int lay = 0)
        {
            return layers[lay].FirstOrDefault(pr => pr.GetPixels().Any(p => p.Equals(pos)))?.GetPixels()
                .First(p => p.Equals(pos));
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

        

        public void ShowToScreen(RenderWindow window)
        {
            foreach (var prims in layers)
            {
                foreach (var data in prims)
                {
                    data.DrawPrim(window);
                }
            }
        }

        public void AddPrim(Prim obj, int lay = 1)
        {
            layers[lay].Add(obj);
        }

        public bool IsPoint(Point2 pos, int i)
        {
            return layers[i].FirstOrDefault(pr => pr.GetPixels().Any(p => p.Equals(pos))) != null;
        }
    }
}