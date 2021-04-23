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
            window = new RenderWindow(new VideoMode(1000, 1000), "Window");
            window.Closed += (o, args) => { window.Close(); };
            window.SetActive(true);
            while (window.IsOpen)
            {
                grid.ClearLayer(1);
                (window as Window).DispatchEvents();






                grid.ShowToScreen(window);
                window.Display();
            }
        }
    }
}
