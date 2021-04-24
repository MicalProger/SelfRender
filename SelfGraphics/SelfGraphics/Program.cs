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
        static RenderWindow window;

        public const int rays = 100;

        public static double angle;

        public static double CamAngel
        {
            
            get => angle;
            set
            {
              
                angle = value;
                cam.SetRoration(angle);
                cam.Render(rays, MaxLen);
            }
        }
        public static double GetEven(double f)
        {
            f = Math.Round(f);
            if (f % 2 == 0) return f + 1;
            return f;
        }
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


        public const double MaxLen = 1000;
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
                cam.Render(rays, MaxLen);
            }
            get => camPos;
        }

        static Point2 camPos = new Point2(275, 160);

        static void Main(string[] args)
        {
            window = new RenderWindow(new VideoMode(1980, 1080), "Window", Styles.Default);
            window.Closed += (o, args) => { window.Close(); };
            
            window.SetActive(true);
            window.KeyPressed += KeyHandler;
            Grid mapGrid = new Grid(@"testMap.png", Color.White);

            Grid grid = new Grid(1980, 1080, Color.White);
            Rectangle rect = new Rectangle(new Point2(0, 1080 / 2), 1980, 1080 / 2, grid);
            rect.SetRect(Color.Green, 0);
            cam = new Camera(camPos, angle, 100, RenderMode.SingleRender) { grid = mapGrid };
            Stopwatch time = new Stopwatch();
            cam.Render(rays, MaxLen);
            while (window.IsOpen)
            {
                time.Start();
                grid.ClearLayer(1);
                (window as Window).DispatchEvents();

                var copy = new List<Point2>();
                lock (Menenger.Buffer)
                    copy.AddRange(from local in Menenger.Buffer
                                  let p = local as Point2
                                  select p);
                foreach (Point2 epoint in copy)
                {
                    var H = (1080 / 2 - PerspectiveHeight(epoint.Len) * 1080 / 2);
                    Point2 posit = new Point2(Convert.ToInt32(epoint.tag) * 1980 / rays, 1080 / 2);
                    Rectangle imagePart = new Rectangle(posit, GetEven((double)1980 / rays), H, grid);
                    imagePart.SetFromCenter(posit.ChangedFor(1980 * 2 / rays , 0).Rounded(), epoint.Color);
                }
                grid.ShowToScreen(window);
                window.Display();
                window.SetTitle($"FPS {1000 / (double)(time.ElapsedMilliseconds)}");
                time.Reset();
            }
        }


        private static void KeyHandler(object sender, KeyEventArgs e)
        {
            if (e.Code == Keyboard.Key.W)
            {
                CamPosit = CamPosit.ChangedFor(0, 50);
            }
            else if (e.Code == Keyboard.Key.S)
            {
                CamPosit = CamPosit.ChangedFor(0, -50);
            }
            if (e.Code == Keyboard.Key.A)
            {
                CamPosit = CamPosit.ChangedFor(-50, 0);
            }
            else if (e.Code == Keyboard.Key.D)
            {
                CamPosit = CamPosit.ChangedFor(50, 0);
            }else if(e.Code == Keyboard.Key.Q)
            {
                CamAngel = CamAngel + 15;
            }
            else if(e.Code == Keyboard.Key.E)
            {
                CamAngel = CamAngel - 15;
            }

        }
    }

}

