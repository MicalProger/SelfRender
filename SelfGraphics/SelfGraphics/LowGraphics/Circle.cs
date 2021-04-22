using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfGraphics.LowGraphics
{
    class Circle
    {
        double diameter;
        Point2 center;
        Grid grid;
        double rad;
        public Circle(Grid grid, Point2 center, double radius)
        {
            rad = radius;
            diameter = rad * 2;
            this.center = center;
            this.grid = grid;
        }
        delegate bool IsCircle(double len);
        public void SetCircle(double border, BorderType type)
        {
            
            IsCircle rule;
            List<Point2> points;
            switch (type)
            {
                case BorderType.Center:
                    rule = (len) =>
                    {
                        if (rad - border / 2 < len && rad + border / 2 < len) return true;
                        return false;
                    };
                    break;
                case BorderType.In:
                    rule = (len) =>
                    {
                        if (rad >= len && rad - border <= len) return true;
                        return false;
                    };
                    break;
                case BorderType.Out:
                    rule = (len) =>
                    {
                        if (rad <= len && rad + border >= len) return true;
                        return false;
                    };
                    break;
                default:
                    rule = (len) => true;
                    break;
            }
            var pre = Tools.DoubleRange(rad + border, rad + border);
            points = (from p in pre
                     let f = center.ChangedFor(p[0], p[1])
                     where rule(f.GetLenTo(center))
                     select f).ToList();
            foreach (var part in points)
            {
                grid.SetPoint(part);
            }
                     

        }
    }
    enum BorderType
    {
        Center = 1,
        In = 2,
        Out = 3
    }
}
