using System;
using SFML.Graphics;
using SFML.Window;
using SelfGraphics.LowGraphics;
using System.Text.Json;
using System.Diagnostics;
using System.Threading;
using SelfGraphics.GraphRT;
using SelfGraphics.GraphRT.Graphics2D;
using SFML.System;

namespace SelfGraphics
{
    class Program
    {
        static RenderWindow _window;

        const double Height = 900;

        const double Wight = 1600;

        public static double ang = 0;

        public static Grid grid;
        static void Main(string[] args)
        {

            _window = new RenderWindow(new VideoMode((uint)Wight, (uint)Height), "Window", Styles.Default);
            _window.Closed += (o, args) => { _window.Close(); };
            _window.SetActive(true);
            _window.Resized += (o, args) => { _window.Size = new Vector2u((uint)Wight, (uint)Height); };
            _window.KeyPressed += KeyHandler;
            grid = new Grid((uint)Wight, (uint)Height, Color.Black);
            grid.SetBorder(Color.Red);
            var rg = new Rectangle(new Point2(250, 250), new(55, 55), Color.Red) { tag = "Rg" };
            var rr = new Rectangle(new Point2(250, 250), new(55, 55), Color.Green) { tag = "Rr" };
            rr.startPos.X = 700;
            grid.AddPrim(rg);
            grid.AddPrim(rr);
            Stopwatch time = new Stopwatch();
            var mPos = Mouse.GetPosition(_window);
            grid.AddLayer();
            grid.AddLayer();
            grid.AddLayer();

            Camera cam = new Camera(new Point2(50, 50), 135, 60, RenderMode.AlwaysRender) {grid = grid};
            
            while (_window.IsOpen)
            {
                time.Start();
                _window.Clear(Color.Black);
                _window.DispatchEvents();
                cam.SetRoration(ang);
                Console.WriteLine(cam.GetRotation());
                cam.Render(100);
                grid.ShowToScreen(_window);
                _window.Display();
                grid.ClearLayer(2);
                _window.SetTitle($"FPS {Math.Round(1000 / (double)(time.ElapsedMilliseconds))}");
                time.Reset();
                Thread.Sleep(50);
            }
        }

        private static void KeyHandler(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape) _window.Close();
            if (e.Code == Keyboard.Key.Right) ang -= 5;
            if (e.Code == Keyboard.Key.Left) ang += 5;
            if (e.Code == Keyboard.Key.C) grid.ClearLayer(3);
               

        }
    }
}