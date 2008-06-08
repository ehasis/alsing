using System;
using System.Collections.Generic;
using System.Drawing;

namespace AlbinoHorse.Layout
{
    public class Node
    {
        public RectangleF Bounds;

        #region Property Connections

        private List<Node> connections;

        public List<Node> Connections
        {
            get { return connections; }
            set { connections = value; }
        }

        #endregion

        public Node()
        {
            Connections = new List<Node>();
        }

        public PointF GetRepellingForce(PointF delta, float mass)
        {
            var force = new PointF();
            double dx = delta.X;
            double dy = delta.Y;

            double dist = GetDistance(dx, dy);
            double angle = Math.Atan2(dy, dx);

            angle += Math.PI;


            dist++;
            double power = 0;
            power = (1/dist)*10*mass;


            if (power < 0.05)
                power = 0;

            if (power > 10)
                power = 10;


            force.X = (float) (Math.Cos(angle)*power);
            force.Y = (float) (Math.Sin(angle)*power);

            return force;
        }

        private static double GetDistance(double dx, double dy)
        {
            double dist = Math.Sqrt(dx*dx + dy*dy);
            return dist;
        }

        public PointF GetPullingForce(PointF delta)
        {
            var force = new PointF();
            double dx = delta.X;
            double dy = delta.Y;

            double dist = GetDistance(dx, dy);
            double angle = Math.Atan2(dy, dx);

            double power = (dist*dist)/1000;
            if (power > 60)
                power = 60;


            force.X = (float) (Math.Cos(angle)*power);
            force.Y = (float) (Math.Sin(angle)*power);

            return force;
        }

        //public PointF GetLinearPullingForce(PointF target)
        //{
        //    PointF force = new PointF();
        //    double dx = target.X - Bounds.X;
        //    double dy = target.Y - Bounds.Y;

        //    double dist = GetDistance(dx, dy);
        //    double angle = Math.Atan2(dy, dx);

        //    double power = 1;

        //    force.X = (float)(Math.Cos(angle) * power);
        //    force.Y = (float)(Math.Sin(angle) * power);

        //    return force;
        //}

        public PointF GetForceDirection(List<Node> allNodes, float massFactor)
        {
            var force = new PointF();
            foreach (Node node in allNodes)
            {
                if (node == this)
                    continue;

                PointF delta = GetDelta(Bounds, node.Bounds);
                double dist = GetDistance(delta.X, delta.Y);

                if (dist > 200)
                    continue;

                PointF nodeForce = GetRepellingForce(delta,
                                                     1 +
                                                     ((1 + (node.Bounds.Width*node.Bounds.Height)/1000)*
                                                      node.connections.Count*massFactor));

                force.X += nodeForce.X;
                force.Y += nodeForce.Y;
            }

            foreach (Node node in Connections)
            {
                if (node == this)
                    continue;

                PointF delta = GetDelta(Bounds, node.Bounds);
                PointF nodeForce = GetPullingForce(delta);

                force.X += nodeForce.X;
                force.Y += nodeForce.Y;
            }

            //PointF delta2 = GetDelta(Bounds, new RectangleF (200,200,1,1));
            //PointF center = GetPullingForce(delta2);
            //force.X += center.X;
            //force.Y += center.Y;

            //PointF tl = GetRepellingForce(new PointF(0, 0));

            //force.X += tl.X;
            //force.Y += tl.Y;

            //PointF tr = GetRepellingForce(new PointF(400, 0));

            //force.X += tr.X;
            //force.Y += tr.Y;

            //PointF bl = GetRepellingForce(new PointF(0, 400));

            //force.X += bl.X;
            //force.Y += bl.Y;

            //PointF br = GetRepellingForce(new PointF(400, 400));

            //force.X += br.X;
            //force.Y += br.Y;

            return force;
        }

        private PointF GetDelta(RectangleF bounds1, RectangleF bounds2)
        {
            float dx = 0;
            float dy = 0;

            //  return new PointF(bounds2.X - bounds1.X, bounds2.Y - bounds1.Y);

            if (bounds2.IntersectsWith(bounds1))
                return new PointF(4, 1);

            if (bounds2.Right < bounds1.Left)
                dx = bounds2.Right - bounds1.Left;

            if (bounds2.Left > bounds1.Right)
                dx = bounds2.Left - bounds1.Right;

            if (bounds2.Bottom < bounds1.Top)
                dy = bounds2.Bottom - bounds1.Top;

            if (bounds2.Top > bounds1.Bottom)
                dy = bounds2.Top - bounds1.Bottom;


            return new PointF(dx, dy);
        }
    }
}