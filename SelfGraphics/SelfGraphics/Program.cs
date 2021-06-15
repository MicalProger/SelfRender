using System;
using SFML.Graphics;
using SFML.Window;
using SelfGraphics.LowGraphics;
using System.Diagnostics;
using System.Threading;
using SelfGraphics.GraphRT;
using SelfGraphics.GraphRT.Graphichs3D;
using SelfGraphics.GraphRT.Graphics2D;
using SFML.System;

namespace SelfGraphics
{
    class Program
    {
        static void Main(string[] args)
        {
            Claster cl = DotObjLoader.LoadObj("TestCube.obj");
            Console.WriteLine(cl.Poligons.Count);
        }
    }
}