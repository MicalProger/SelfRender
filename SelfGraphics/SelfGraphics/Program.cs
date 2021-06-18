using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.Window;
using SelfGraphics.LowGraphics;
using System.Diagnostics;
using System.Threading;
using SelfGraphics.GraphRT;
using SelfGraphics.GraphRT.Graphics3D;
using SelfGraphics.GraphRT.Graphics2D;
using SelfGraphics.GraphRT.Graphics3D.Visualization;
using SFML.System;

namespace SelfGraphics
{
    class Program
    {
        private static RenderWindow _render;

        static void Main(string[] args)
        {
            _render = new RenderWindow(new VideoMode(500, 500), "Render view");
            _render.Closed += (sender, eventArgs) => _render.Close(); 
            List<Claster> cl = DotObjLoader.LoadObj("TestScence.obj");
            Scence sc = new Scence();
            sc.objs = cl;
            Camera3D cam = new Camera3D(75, new Point3(7.5, -7, 5), new Direction(63.6, 46.7));
            var rnd = cam.RenderImage(sc, 500, 500);
            while (_render.IsOpen)
            {
                _render.Clear(new(0,0,0));
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