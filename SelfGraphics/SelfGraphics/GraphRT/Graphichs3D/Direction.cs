namespace SelfGraphics.GraphRT.Graphics3D
{
    public class Direction
    {
        public Direction(double xRotation, double yRotation,  double zRotation)
        {
            XRotation = xRotation;
            YRotation = yRotation;
            ZRotation = zRotation;
        }

        public static Direction operator +(Direction d1, Direction d2)
        {
            return new(d1.XRotation + d2.XRotation, d1.YRotation + d2.YRotation, d1.ZRotation + d2.ZRotation);
        }
        
        public double XRotation;
        
        public double YRotation;
        
        public double ZRotation;
    }
}