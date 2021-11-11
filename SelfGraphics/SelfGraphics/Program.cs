using System;
using System.Collections.Generic;
using System.Timers;
using SFML.Graphics;
using SFML.Window;
using SelfGraphics.GraphRT.Graphics3D;
using SelfGraphics.GraphRT.Graphics3D.Visualization;
using SelfGraphics.LowGraphics;
using SelfGraphics.SelfPhysics;

namespace SelfGraphics
{
    class Program
    {
        private static RenderWindow _render;

        private static Space _w;
        static void Main(string[] args)
        {
            // Timer t = new Timer();
            // t.Elapsed += TOnElapsed;22
            // t.Interval += 250;
            // t.AutoReset = true;
            // List<Claster> cl = DotObjLoader.LoadObj("newScence.obj");
            // Scence sc = new Scence();
            // sc.objs = cl; Camera3D cam = new Camera3D(120, new Point3(7.5, -7, 5), new Direction(63.6, 0, 46.7));
            // var rnd = cam.RenderImage(sc, 300, 300);
            var Plus = new Point2(250, 250);
            MPoint sun = new MPoint(new Point2(0, 0) + Plus, 100, Point2.Zero, true);
            MPoint planet = new MPoint(new Point2(0, -50) + Plus, 30, new Point2(1.5, 0), false);
            MPoint aster = new MPoint(new Point2(-100, 60) + Plus, 100, Point2.Zero, false);
            // MPoint mid = new MPoint(new Point2(70, 30) + Plus, 7, new Point2(1, 0));
            _w = new Space(1, WorldType.ObjectsGravity);
            _w.Objects.AddRange(new []{sun, planet, aster});
            _render = new RenderWindow(new VideoMode(500, 500), "Render view");
            _render.Closed += (sender, eventArgs) => _render.Close();
            _render.SetFramerateLimit(10);
            _render.SetActive(true);
            // t.Start();
            while (_render.IsOpen)
            {
                _render.Clear(new(0, 0, 0));
                _render.DispatchEvents();
                {
                    {
                        _w.Next(1);
                        _w.Draw(_render, 1, false);
                    }
                }
                _render.Display();
            }

        }

        private static void TOnElapsed(object sender, ElapsedEventArgs e)
        {
        }
    }
}