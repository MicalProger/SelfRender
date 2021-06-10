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

        public static double ang;

        public static Grid mapGrid;
        static void Main(string[] args)
        {

            _window = new RenderWindow(new VideoMode((uint)Wight, (uint)Height), "Window", Styles.Default);
            _window.Closed += (o, args) => { _window.Close(); };
            _window.SetActive(true);
            _window.Resized += (o, args) => { _window.Size = new Vector2u((uint)Wight, (uint)Height); };
            _window.KeyPressed += KeyHandler;
            
            mapGrid = new Grid((uint)Wight, (uint)Height, Color.Black);
            mapGrid.SetBorder(Color.Red);
            
            var rg = new Rectangle(new Point2(250, 250), new(55, 55), Color.Red) { tag = "Rg" };
            var rr = new Rectangle(new Point2(250, 250), new(55, 55), Color.Green) { tag = "Rr" };
            rr.startPos.X = 700;
            mapGrid.AddPrim(rg);
            mapGrid.AddPrim(rr);
            mapGrid.AddLayer();
            Stopwatch time = new Stopwatch();

            Camera cam = new Camera(200, new Point2(50, 50), 60, mapGrid);
            
            while (_window.IsOpen)
            {
                time.Start();
                _window.Clear(Color.Black);
                _window.DispatchEvents();
                cam.Angle = ang;
                cam.RenderGrid(35, true);
                Console.WriteLine($"{cam.Angle} : {ang}");
                mapGrid.ShowToScreen(_window);
                _window.Display();
                _window.SetTitle($"FPS {1000 / (double)(time.ElapsedMilliseconds)}");
                mapGrid.ClearLayer(2);
                time.Reset();
                Thread.Sleep(50);
            }
        }


        private static void KeyHandler(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape) _window.Close();
            if (e.Code == Keyboard.Key.Right) ang -= 1;
            if (e.Code == Keyboard.Key.Left) ang += 1;
            if (e.Code == Keyboard.Key.C) mapGrid.ClearLayer(3);
               

        }
    }
}