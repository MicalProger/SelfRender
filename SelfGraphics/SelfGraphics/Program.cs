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

        static int len = 5;
        public static Ray ray;
        static RenderWindow window;

        public static double angle;

        public static void DrawRender()
        {
            RenderWindow render = new RenderWindow(new VideoMode(750, 200), "Render");
            render.Closed += (o, args) => { window.Close(); };
            render.SetActive(true);
            while (render.IsOpen)
            {
                render.DispatchEvents();

                render.Display();
            }
        }

        static void Main(string[] args)
        {   
            window = new RenderWindow(new VideoMode(332, 350), "Window");
            window.Closed += (o, args) => { window.Close(); };
            window.KeyReleased += KeyboardHandler;
            window.SetActive(true);
            Grid grid = new Grid(@"testMap.png", Color.White);
            grid.SetColor(new Color(127, 127, 127));
            Camera cam = new Camera(new Point2(165, 20), angle, 60) {grid = grid };
            Stopwatch time = new Stopwatch();
            while (window.IsOpen)
            {
                time.Start();
                (window as Window).DispatchEvents();
                grid.ClearLayer(1);
                cam.SetRoration(angle);
                var x = cam.GetImage2(15, 200);

                grid.ShowToScreen(window);
                window.Display();
                time.Stop();
                window.SetTitle($"FPS {1000 / time.ElapsedMilliseconds}");
                time.Reset();
            }
        }

        private static void KeyboardHandler(object sender, KeyEventArgs e)
        {

            if (e.Code == Keyboard.Key.L) angle += 5;
            if (e.Code == Keyboard.Key.R) angle -= 5;
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            angle += 1;
            len += 5;
        }
    }
}
