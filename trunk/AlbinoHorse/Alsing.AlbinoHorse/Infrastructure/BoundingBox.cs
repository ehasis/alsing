using System.Drawing;

namespace AlbinoHorse.Infrastructure
{
    public abstract class BoundingItem
    {
        #region Property Target

        public object Target { get; set; }

        #endregion

        #region Property Data

        public object Data { get; set; }

        #endregion

        public abstract bool HitTest(int x, int y);
    }

    public class BoundingBox : BoundingItem
    {
        #region Property Bounds 

        public Rectangle Bounds { get; set; }

        #endregion

        public override bool HitTest(int x,int y)
        {
            return Bounds.Contains(x, y);
        }
    }

    public class BoundingLine : BoundingBox
    {
        public int X1 { get; set; }
        public int Y1 { get; set; }
        public int X2 { get; set; }
        public int Y2 { get; set; }
        public int Width { get; set; }

        public override bool HitTest(int x, int y)
        {
            throw new System.NotImplementedException();
        }
    }
}