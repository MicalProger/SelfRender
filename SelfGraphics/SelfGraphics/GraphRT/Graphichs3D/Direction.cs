namespace SelfGraphics.GraphRT.Graphics3D
{
    public class Direction
    {
        public Direction(double xRotation, double yRotation)
        {
            XRotation = xRotation;
            YRotation = yRotation;
        }

        public static Direction operator +(Direction d1, Direction d2)
        {
            return new(d1.XRotation + d2.XRotation, d1.YRotation + d2.YRotation);
        }
        
        public double XRotation;
        
        public double YRotation;
        
        
    }
}