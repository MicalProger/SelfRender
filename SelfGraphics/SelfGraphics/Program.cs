using System;
using SFML.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SFML.Window;
using SelfGraphics.LowGraphics;
using System.Timers;
using System.Diagnostics;
using System.Collections.Generic;
using SelfGraphics.GraphRT.Graphics2D;
using System.Linq;
using SelfGraphics.GraphRT;
using System.Threading;

namespace SelfGraphics
{
    class Program
    {
        static RenderWindow _window;

        const double Height = 900;

        const double Wight = 1500;

        static void Main(string[] args)
        {
            _window = new RenderWindow(new VideoMode((uint) Wight, (uint) Height), "Window", Styles.Default);
            _window.Closed += (o, args) => { _window.Close(); };
            _window.SetActive(true);
            _window.KeyPressed += KeyHandler;
            Grid grid = new Grid((uint) Wight, (uint) Height, Color.Black);
            var line = new Line(new Point2(0, 0), new Point2(20, 20));
            grid.AddPrim(line);
            Stopwatch time = new Stopwatch();
            while (_window.IsOpen)
            {
                time.Start();
                // grid.ClearLayer(1);
                _window.DispatchEvents();
                
                
                
                grid.ShowToScreen(_window);
                _window.Display();
                _window.SetTitle($"FPS {1000 / (double) (time.ElapsedMilliseconds)}");
                time.Reset();
            }
        }


        private static void KeyHandler(object sender, KeyEventArgs e)
        {
            if(e.Code == Keyboard.Key.Escape) _window.Close();
        }
    }
}