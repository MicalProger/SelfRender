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

        static Point2 camPos = new Point2(275, 160);
        private const int RAYS = 100;
        private const double MaxLen = 1000;
        private const uint windowWidth = 750;
        private const uint windowHeight = 500;
        private static double WindowCenterHeight => windowHeight / 2;
        private const double fov = 120;
        private static double angle;
        private static Grid grid;


        public static double CamAngel
        {
            get => angle;
            set
            {
                angle = value;
                cam.SetRoration(angle);
                cam.Render(RAYS, MaxLen);
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

        static void Init()
        {
            window = new RenderWindow(new VideoMode(windowWidth, windowHeight), "Window");
            window.Closed += (o, args) => { window.Close(); };
            window.SetActive(true);
            window.KeyPressed += KeyHandler;

            var mapGrid = new Grid(@"testMap.png", Color.White);
            grid = new Grid(windowWidth, windowHeight, Color.White);

            var vertCenterPoint = new Point2(0, WindowCenterHeight);
            var rect = new Rectangle(vertCenterPoint, windowWidth, WindowCenterHeight, grid);
            rect.SetRect(Color.Green, 0);
            cam = new Camera(camPos, angle, fov, RenderMode.SingleRender) { grid = mapGrid };
            cam.Render(RAYS, MaxLen);
        }

        static void Main(string[] args)
        {
            Init();

            Stopwatch time = new Stopwatch();
            while (window.IsOpen)
            {
                time.Start();

                grid.ClearLayer(1);
                window.DispatchEvents();

                for (int i = 0; i < Manager.Buffer.Count; i++)
                {
                    Point2 epoint = Manager.Buffer[i] as Point2;
                    Point2 posit = new Point2(Convert.ToInt32(epoint.tag) * windowWidth / RAYS, WindowCenterHeight);
                    Rectangle imagePart = new Rectangle(posit, GetEven((double)windowWidth / RAYS), (WindowCenterHeight - PerspectiveHeight(epoint.Len) * WindowCenterHeight), grid);
                    imagePart.SetFromCenter(posit.ChangedFor(RAYS / 2, 0).Rounded(), epoint.Color);
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