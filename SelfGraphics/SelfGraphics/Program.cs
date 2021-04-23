using System;
using SFML.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SFML.Window;
using SelfGraphics.LowGraphics;
using System.Timers;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace SelfGraphics
{

    class Program
    {
        static RenderWindow window;
        static void Main(string[] args)
        {
            Grid grid = new Grid(1000, 1000, Color.Black);
            Timer timer = new Timer(1000);
            List<Line> hourMarkers = new List<Line>();
            for (int i = 0; i < 12; i++)
            {
                hourMarkers.Add(new Line(new Point2(500, 500, grid), 275, 30 * i, grid));
                hourMarkers.Last().CutFromStart(8, Color.White, 60, 0);
            }
            window = new RenderWindow(new VideoMode(1000, 1000), "Window");
            window.Closed += (o, args) => { window.Close(); };

            window.SetActive(true);
            
            
            Circle circle = new Circle(grid, new Point2(500, 500, grid), 250);
            circle.SetCircle(4, BorderType.In, 0);
            while (window.IsOpen)
            {
                grid.ClearLayer(1);
                (window as Window).DispatchEvents();
                Line second = new Line(new Point2(500, 500, grid), 200, DateTime.Now.Second * -6 - 180, grid);
                Line minute = new Line(new Point2(500, 500, grid), 150, DateTime.Now.Minute * -3 + 90, grid);
                Line hours = new Line(new Point2(500, 500, grid), 100, ((DateTime.Now.Hour + 1) * -15) + 90, grid);
                second.SetLine(10, Color.White);
                minute.SetLine(10, Color.Cyan);
                hours.SetLine(10, Color.Red);
                grid.ShowToScreen(window);
                window.Display();
            }
        }
    }
}
