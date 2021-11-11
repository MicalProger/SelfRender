using System;
using System.Collections.Generic;
using System.Linq;
using SelfGraphics.LowGraphics;
using SFML.Graphics;

namespace SelfGraphics.SelfPhysics
{
    public enum WorldType
    {
        GroundGravity = 1,
        ObjectsGravity = 2
    }

    public class Space
    {
        private void CalculateBoosts()
        {
            Objects.ForEach(i => i.Speed += i.LocalBoost);
            Objects.Where(i => !i.IsStatic).ToList().ForEach(i => i.Position += i.Speed);
        }

        public void Next(double k)
        {
            foreach (var obj in Objects)
            {
                obj.LocalBoost = Point2.Zero;
                Point2 totalVector = Point2.Zero;
                foreach (var local in Objects.Where(i => i != obj))
                {
                    obj.Position.SetLenTo(local.Position);
                    var force = k * Gravity * (obj.Mass * local.Mass) / Math.Pow(obj.Position.Len, 2);
                    var localVec = (local.Position - obj.Position);
                    localVec.ChangeToLen(force);
                    totalVector = totalVector + localVec;
                }
                obj.LocalBoost = totalVector * (1 / obj.Mass);
            }
            CalculateBoosts();
        }

        public double Gravity;

        public readonly WorldType Type;

        public List<PhysicalPrim> Objects;

        public bool Collisions;

        public void AddObject(PhysicalPrim obj)
        {
            Objects.Add(obj);
        }

        public Space(double G, WorldType type)
        {
            Gravity = G;
            Type = type;
            Objects = new List<PhysicalPrim>();
        }

        public void Draw(RenderWindow window, double scale, bool centrate=true)
        {
            double hs, ws = 0;
            ws = Objects.Max(i => i.Position.X) - Objects.Min(i => i.Position.X);
            hs = Objects.Max(i => i.Position.Y) - Objects.Min(i => i.Position.Y);
            Grid grid = new Grid((uint)(ws * scale + 30), (uint)(hs * scale + 30));
            ws = Objects.Min(i => i.Position.X);
            hs = Objects.Min(i => i.Position.Y);
            foreach (var prim in Objects)
            {
                grid.AddPrim((prim.Position - (centrate ? new Point2(ws - 15, hs - 15) : Point2.Zero)) * scale);

            }
            grid.ShowToScreen(window);
        }
    }
}