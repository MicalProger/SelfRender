using System;
using System.Collections.Generic;
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
            List<Claster> cl = DotObjLoader.LoadObj("TestScence.obj");
            Console.WriteLine(cl[1].Poligons.Count);
        }
    }
}