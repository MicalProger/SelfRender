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
        
        const int RAYS = 200;
        
        static double _angle;

        const double Height = 900;

        const double Wight = 1500;

        public static double CamAngel
        {
            get => _angle;
            set
            {
                _angle = value;
                cam.SetRoration(_angle);
                cam.Render(RAYS, MaxLen);
            }
        }

        public static double GetEven(double f)
        {
            f = Math.Round(f);
            if (f % 2 == 0) return f + 1;
            return f;
        }
        
        public const double MaxLen = 500;

        public static double PerspectiveHeight(double x)
        {
            if (x > MaxLen) return 0;
            return x / MaxLen;
        }

        static Camera cam;

        private static Point2 CamPosit
        {
            set
            {
                camPos = value;
                cam.Position = value;
                cam.Render(RAYS, MaxLen);
            }
            get => camPos;
        }

        static Point2 camPos = new Point2(50, 50);

        static void Main(string[] args)
        {
            _window = new RenderWindow(new VideoMode((uint) Wight, (uint) Height), "Window", Styles.Default);
            _window.Closed += (o, args) => { _window.Close(); };

            _window.SetActive(true);
            _window.KeyPressed += KeyHandler;
            Grid mapGrid = new Grid(@"testMap.png", Color.White);

            Grid grid = new Grid((uint) Wight, (uint) Height, Color.White);
            Rectangle rect = new Rectangle(new Point2(0, Height / 2), Wight, Height / 2, grid);
            rect.SetRect(Color.Green, 0);
            cam = new Camera(camPos, _angle, 50, RenderMode.SingleRender) {grid = mapGrid};
            Stopwatch time = new Stopwatch();
            cam.Render(RAYS, MaxLen);
            while (_window.IsOpen)
            {
                time.Start();
                grid.ClearLayer(1);
                _window.DispatchEvents();

                var copy = new List<Point2>();
                lock (Menenger.Buffer)
                    copy.AddRange(from local in Menenger.Buffer
                        let p = local as Point2
                        select p);
                Console.WriteLine(copy.Count);
                foreach (Point2 epoint in copy)
                {
                    var H = (Height / 2 - PerspectiveHeight(epoint.Len) * Height / 2);
                    Point2 posit = new Point2(Convert.ToInt32(epoint.tag) * Wight / RAYS, Height / 2);
                    Rectangle imagePart = new Rectangle(posit, GetEven((double) Wight / RAYS), H, grid);
                    imagePart.SetFromCenter(posit.ChangedFor(Wight * 2 / RAYS, 0).Rounded(), epoint.Color);
                }

                grid.ShowToScreen(_window);
                _window.Display();
                _window.SetTitle($"FPS {1000 / (double) (time.ElapsedMilliseconds)}");
                time.Reset();
            }
        }


        private static void KeyHandler(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.W)
            {
                CamPosit = CamPosit.ChangedFor(0, 20);
            }
            else if (e.Code == Keyboard.Key.S)
            {
                CamPosit = CamPosit.ChangedFor(0, -20);
            }

            if (e.Code == Keyboard.Key.A)
            {
                CamPosit = CamPosit.ChangedFor(-20, 0);
            }
            else if (e.Code == Keyboard.Key.D)
            {
                CamPosit = CamPosit.ChangedFor(20, 0);
            }
            else if (e.Code == Keyboard.Key.Q)
            {
                CamAngel = CamAngel + 15;
            }
            else if (e.Code == Keyboard.Key.E)
            {
                CamAngel = CamAngel - 15;
            }
        }
    }
}