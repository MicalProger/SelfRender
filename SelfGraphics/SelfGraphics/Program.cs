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
        static RenderWindow _mapWindow;

        static RenderWindow _viewWindow;

        const double Height = 900;

        const double Wight = 1600;

        static double ang = 200;

        static Grid mapGrid;

        static Grid viewGrid;

        static void InitMap()
        {
            _mapWindow = new RenderWindow(new VideoMode((uint) Wight, (uint) Height), "Window", Styles.Default);
            _mapWindow.Closed += (o, args) => { _mapWindow.Close(); };
            _mapWindow.SetActive(true);
            _mapWindow.Resized += (o, args) => { _mapWindow.Size = new Vector2u((uint) Wight, (uint) Height); };
            _mapWindow.KeyPressed += KeyHandler;
            mapGrid = new Grid((uint) Wight, (uint) Height, Color.Black);
            mapGrid.SetBorder(Color.Magenta);
            var rg = new Rectangle(new Point2(250, 250), new(200, 200), Color.Red) {tag = "Rg"};
            var rr = new Rectangle(new Point2(250, 250), new(200, 200), Color.Green) {tag = "Rr"};
            rr.startPos.X = 700;
            mapGrid.AddPrim(rg);
            mapGrid.AddPrim(rr);
            mapGrid.AddLayer();
        }

        static void ViewInit()
        {
            _viewWindow = new RenderWindow(new VideoMode((uint) Wight, (uint) Height), "Window", Styles.Default);
            _viewWindow.Closed += (o, args) => { _viewWindow.Close(); };
            _viewWindow.Resized += (o, args) => { _mapWindow.Size = new Vector2u((uint) Wight, (uint) Height); };
            _viewWindow.SetActive(true);
            _viewWindow.KeyPressed += KeyHandler;
            viewGrid = new Grid((uint) Wight, (uint) Height, Color.Black);
            var sky = new Rectangle(Point2.Zero, new Vector2f((float) Wight, (float) Height / 2), Color.Cyan);
            viewGrid.AddPrim(sky, 0);
            var ground = new Rectangle(new Point2(0, Height / 2), new Vector2f((float) Wight, (float) Height / 2),
                Color.Black);
            viewGrid.AddPrim(ground, 0);
        }

        static double GetViewHeight(double dist, double maximal)
        {
            if (dist > maximal) return 0;
            var res =  (maximal - dist) / maximal;
            return res;
        }

        static void Main(string[] args)
        {
            InitMap();
            ViewInit();
            Stopwatch time = new Stopwatch();
            Camera cam = new Camera(200, new Point2(50, 50), 60, mapGrid);
            while (_mapWindow.IsOpen && _viewWindow.IsOpen)
            {
                time.Start();
                _mapWindow.Clear(Color.Black);
                _mapWindow.DispatchEvents();
                _viewWindow.Clear(Color.Black);
                _viewWindow.DispatchEvents();
                {
                    cam.Angle = ang;
                    var colls = cam.RenderGrid(100, true);
                    var w = Wight / colls.Count;
                    foreach (var point in colls)
                    {
                        point.SetLenTo(cam.Position);
                        double h = GetViewHeight(point.Len, 2000) * Height;
                        var tmpRect = new Rectangle(new Point2(w * colls.IndexOf(point), (Height - h) / 2),
                            new Vector2f((float) w, (float) h), point.Color);
                        viewGrid.AddPrim(tmpRect, 1);
                    }
                }

                mapGrid.ShowToScreen(_mapWindow);
                _mapWindow.Display();
                _mapWindow.SetTitle($"MAP : FPS {1000 / (double) (time.ElapsedMilliseconds)}");
                viewGrid.ShowToScreen(_viewWindow);
                _viewWindow.Display();
                _viewWindow.SetTitle($"VIEW : FPS {1000 / (double) (time.ElapsedMilliseconds)}");
                mapGrid.ClearLayer(2);
                viewGrid.ClearLayer(1);
                time.Reset();
                Thread.Sleep(50);
            }
        }


        private static void KeyHandler(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape) _mapWindow.Close();
            if (e.Code == Keyboard.Key.Right) ang -= 1;
            if (e.Code == Keyboard.Key.Left) ang += 1;
            if (e.Code == Keyboard.Key.C) mapGrid.ClearLayer(3);
        }
    }
}