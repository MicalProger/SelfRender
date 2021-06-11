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
        
        const double sence = 0.25; 
        
        const double Height = 900;

        const double Wight = 1600;

        static double ang = 180;

        static Point2 viewPosition = new Point2(Wight / 2, 100);

        static Grid mapGrid;

        static Grid viewGrid;

        static int rays = 200;

        static void InitMap()
        {
            _mapWindow = new RenderWindow(new VideoMode((uint)Wight, (uint)Height), "Window", Styles.Default);
            _mapWindow.Closed += (o, args) => { _mapWindow.Close(); };
            _mapWindow.SetActive(true);
            _mapWindow.Resized += (o, args) => { _mapWindow.Size = new Vector2u((uint)Wight, (uint)Height); };
            _mapWindow.KeyPressed += KeyHandler;
            mapGrid = new Grid((uint)Wight * 2, (uint)Height * 2);
            mapGrid.SetBorder(Color.Magenta);
            mapGrid.AddLayer();
        }

        static void ViewInit()
        {
            _viewWindow = new RenderWindow(new VideoMode((uint)Wight, (uint)Height), "Window", Styles.Default);
            _viewWindow.Closed += (o, args) => { _viewWindow.Close(); };
            _viewWindow.Resized += (o, args) => { _mapWindow.Size = new Vector2u((uint)Wight, (uint)Height); };
            _viewWindow.SetActive(true);
            _viewWindow.KeyPressed += KeyHandler;
            viewGrid = new Grid((uint)Wight, (uint)Height);
            var sky = new Rectangle(Point2.Zero, new Vector2f((float)Wight, (float)Height / 2), Color.Cyan);
            viewGrid.AddPrim(sky, 0);
            var ground = new Rectangle(new Point2(0, Height / 2), new Vector2f((float)Wight, (float)Height / 2),
                Color.Black);
            viewGrid.AddPrim(ground, 0);
        }

        static double GetViewHeight(double dist, double maximal)
        {
            if (dist > maximal) return 0;
            var res = (maximal - dist) / maximal;
            return res;
        }

        static void Main(string[] args)
        {
            InitMap();
            ViewInit();
            Stopwatch time = new Stopwatch();
            Camera cam = new Camera(200, viewPosition, 100, mapGrid);
            while (_mapWindow.IsOpen && _viewWindow.IsOpen)
            {
                time.Start();
                _mapWindow.Clear(Color.Black);
                _mapWindow.DispatchEvents();
                _viewWindow.Clear(Color.Black);
                _viewWindow.SetMouseCursor(new Cursor(Cursor.CursorType.Cross));
                _viewWindow.DispatchEvents();
                {
                    cam.Position = viewPosition;
                    ang += (Mouse.GetPosition(_viewWindow).X - Wight / 2) * sence;
                    cam.Angle = ang;
                    var colls = cam.RenderGrid(rays, true, 100);
                    var w = Wight / colls.Count;
                    foreach (var point in colls)
                    {
                        double k = GetViewHeight(point.Len, 3500);
                        double h = k * Height;
                        point.Color = new Color((byte)(point.Color.R * k), (byte)(point.Color.G * k), (byte)(point.Color.B * k));
                        var tmpRect = new Rectangle(new Point2(w * colls.IndexOf(point), (Height - h) / 2),
                            new Vector2f((float)w, (float)h), point.Color);
                        viewGrid.AddPrim(tmpRect, 1);
                    }
                    Mouse.SetPosition(new Vector2i((int) (Wight / 2), (int) (Height / 2)), _viewWindow);
                }
                Console.WriteLine(viewPosition.ToString());
                Console.WriteLine(ang);
                mapGrid.ShowToScreen(_mapWindow);
                _mapWindow.Display();
                _mapWindow.SetTitle($"MAP : FPS {1000 / (double)(time.ElapsedMilliseconds)}");
                viewGrid.ShowToScreen(_viewWindow);
                _viewWindow.Display();
                _viewWindow.SetTitle($"VIEW : FPS {1000 / (double)(time.ElapsedMilliseconds)}");
                mapGrid.ClearLayer(2);
                viewGrid.ClearLayer(1);
                time.Reset();
                Thread.Sleep(50);
            }
        }


        private static void KeyHandler(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.Escape) _mapWindow.Close();
            if (e.Code == Keyboard.Key.E) ang += 2;
            if (e.Code == Keyboard.Key.Q) ang -= 2;
            if (e.Code == Keyboard.Key.C) mapGrid.ClearLayer(3);
            if (e.Code == Keyboard.Key.W)
            {
                Ray view = new Ray(viewPosition, ang);
                viewPosition = view.GetPixelByLen(50);
            }
            if (e.Code == Keyboard.Key.S)
            {
                Ray view = new Ray(viewPosition, ang);
                viewPosition = view.GetPixelByLen(-50);
            }
            if (e.Code == Keyboard.Key.A)
            {
                Ray view = new Ray(viewPosition, ang - 90);
                viewPosition = view.GetPixelByLen(50);
            }
            if (e.Code == Keyboard.Key.D)
            {
                Ray view = new Ray(viewPosition, ang - 90);
                viewPosition = view.GetPixelByLen(-50);
            }if(e.Code == Keyboard.Key.Up)
            {
                rays++;
                Console.WriteLine(rays);
            } 
            if(e.Code == Keyboard.Key.Down)
            {
                rays--;
                Console.WriteLine(rays);
            }

            if (e.Code == Keyboard.Key.Escape)
            {
                _viewWindow.Close();
            }
        }
    }
}