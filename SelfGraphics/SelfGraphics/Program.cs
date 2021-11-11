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
            var Plus = new Point2(250, 250);
            MPoint sun = new MPoint(new Point2(0, 0) + Plus, 100, Point2.Zero, true);
            MPoint planet = new MPoint(new Point2(0, -50) + Plus, 30, new Point2(1.5, 0), false);
            MPoint aster = new MPoint(new Point2(-100, 60) + Plus, 100, Point2.Zero, false);
            _w = new Space(1, WorldType.ObjectsGravity);
            _w.Objects.AddRange(new []{sun, planet, aster});
            _render = new RenderWindow(new VideoMode(500, 500), "Render view");
            _render.Closed += (sender, eventArgs) => _render.Close();
            _render.SetFramerateLimit(10);
            _render.SetActive(true);
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