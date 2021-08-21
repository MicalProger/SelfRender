using System.Runtime.Intrinsics.X86;
using SelfGraphics.LowGraphics;

namespace SelfGraphics.GraphRT.Graphics3D
{
    public class Direction
    {
        private Point3 vector;

        public Direction(double xRotation, double yRotation, double zRotation)
        {
            vector = new Point3(0, 0, 1);
            XRotation = xRotation;
            YRotation = yRotation;
            ZRotation = zRotation;
            if (xRotation != 0)
                vector.SetRotate(Axis.X, xRotation);
            if (yRotation != 0)
                vector.SetRotate(Axis.Y, yRotation);
            if (zRotation != 0)
                vector.SetRotate(Axis.Z, zRotation);
        }

        public void Add(double xRotation, double yRotation, double zRotation)
        {
            vector = new Point3(0, 0, 1);
            XRotation += xRotation;
            YRotation += yRotation;
            ZRotation += zRotation;
            if (XRotation != 0)
                vector.SetRotate(Axis.X, XRotation);
            if (YRotation != 0)
                vector.SetRotate(Axis.Y, YRotation);
            if (ZRotation != 0)
                vector.SetRotate(Axis.Z, ZRotation);
        }

        public static Direction operator +(Direction d1, Direction d2)
        {
            return new(d1.XRotation + d2.XRotation, d1.YRotation + d2.YRotation, d1.ZRotation + d2.ZRotation);
        }

        public double XRotation;

        public double YRotation;

        public double ZRotation;

        public double L => vector.X;

        public double M => vector.Y;

        public double N => vector.Z;

        public override string ToString()
        {
            var v = new Point3(L, M, N).Rounded(4);
            return $"L : {v.X} |M : {v.Y} |N : {v.Z}";
        }
    }
}