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
        List<List<Point2>> layers = new List<List<Point2>>() { new List<Point2>(), new List<Point2>() };

        Color color;

        uint w;

        uint h;

        public Grid(uint wight, uint height, Color standart)
        {
            color = standart;
            h = height;
            w = wight;
        }

        public Grid(string path, Color transparent)
        {

            Image cur = new Image(File.ReadAllBytes(path));
            h = cur.Size.Y;
            w = cur.Size.X;
            foreach (var item in Tools.DoubleRangePos(h, h))
            {
                if (cur.GetPixel((uint)item[0], (uint)item[1]) != transparent)
                    layers[0].Add(new Point2(item[0], item[1]) { Color = cur.GetPixel((uint)item[0], (uint)item[1]) });
            }
        }

        public bool IsPoint(Point2 target, int layer = 1)
        {
            if (layers[layer].Any(p => p.X == target.X && p.Y == target.Y && p.Color != target.Color))
                return true;
            return false;

        }

        public void TransformPos(object tag, Point2 vector, int layer)
        {
            foreach (var item in layers[layer].Where(p => p.tag == tag))
            {
                item.ChangeFor(vector);
            }
        }

        public void Clear()
        {
            layers = new List<List<Point2>>() { new List<Point2>(), new List<Point2>() };
        }

        public void ClearLayer(int layer)
        {
            layers[layer] = new List<Point2>();
        }

        public void SetPoint(Point2 p, int layer = 1)
        {
            layers[layer].Add(p);
        }

        public void AddLayer()
        {
            layers.Add(new List<Point2>());
        }

        public void SetColor(Color color) => this.color = color;

        public void ShowToScreen(RenderWindow window)
        {
            Image i = new Image(w, h, color);
            foreach (var data in layers)
            {
                foreach (var point in data)
                {
                    if (point.X > w || point.X < 0) point.X = 0;
                    if (point.Y > h || point.Y < 0) point.Y = 0;

                    i.SetPixel((uint)point.X, (uint)point.Y, point.Color);

                }
            }
            Texture texture = new Texture(i);
            window.Draw(new Sprite(texture));
        }
    }
}
