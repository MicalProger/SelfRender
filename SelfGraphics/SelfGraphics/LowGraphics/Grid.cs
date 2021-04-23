using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfGraphics.LowGraphics
{
    class Grid
    {
        List<List<Point2>> layers = new List<List<Point2>>() { new List<Point2>(), new List<Point2>() };
        public void TransformPos(object tag, Point2 vector)
        {
            foreach (var item in data.Where(p => p.tag == tag))
            {
                item.ChangeFor(vector);
            }
        }
        Color color;
        uint w;
        uint h;
        List<Point2> data;
        public Grid(uint wight, uint height, Color standart)
        {
            color = standart;
            h = height;
            w = wight;
            data = new List<Point2>();
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
        public void ShowToScreen(RenderWindow window)
        {
            Image i = new Image(h, w, color);
            foreach (var data in layers)
            {
                foreach (var point in data)
                {
                    if (point.X > w || point.X < 0) point.X = 0;
                    if (point.Y > h || point.Y < 0) point.Y = 0;
                    i.SetPixel((uint)Math.Abs(point.X), (uint)Math.Abs(point.Y), point.Color);
                }
            }
            Texture texture = new Texture(i);
            window.Draw(new Sprite(texture));
        }
    }
}
