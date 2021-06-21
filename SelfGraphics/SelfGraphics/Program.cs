using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SelfGraphics.GraphRT.Graphics3D;
using SelfGraphics.GraphRT.Graphics3D.Visualization;

namespace SelfGraphics
{
    class Program
    {
        private static RenderWindow _render;

        static void Main(string[] args)
        {
            _render = new RenderWindow(new VideoMode(500, 500), "Render view");
            _render.Closed += (sender, eventArgs) => _render.Close();
            _render.SetActive(true);
            List<Claster> cl = DotObjLoader.LoadObj("TestScence.obj");
            Scence sc = new Scence();
            sc.objs = cl;
            Camera3D cam = new Camera3D(120, new Point3(7.5, -7, 5), new Direction(63.6, 46.7));
            var rnd = cam.RenderImage(sc, 500, 500);

            while (_render.IsOpen)
            {
                _render.Clear(new(0, 0, 0));
                _render.DispatchEvents();
                {
                    {
                        _render.Draw(new Sprite(rnd));
                    }
                }
                _render.Display();
            }
        }
    }
}