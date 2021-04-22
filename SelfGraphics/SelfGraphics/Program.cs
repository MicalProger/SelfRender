using System;
using SFML.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SFML.Window;
using SelfGraphics.LowGraphics;

namespace SelfGraphics
{
    class Program
    {
        static RenderWindow window;
        static void Main(string[] args)
        {
            window = new RenderWindow(new VideoMode(1000, 1000), "Window");
            window.Closed += (o, args) => { window.Close(); };
            window.SetActive(true);
            Grid grid = new Grid(1000, 1000, Color.Black);
            Circle circle = new Circle(grid, new Point2(500, 500, grid), 250);
            circle.SetCircle(10, BorderType.In);
            while (window.IsOpen)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Q))
                    grid.Clear();
                (window as Window).DispatchEvents();
                
                grid.ShowToScreen(window);
                
                window.Display();
            }
        }
    }
}
