using System.Drawing;

namespace AlbinoHorse.Infrastructure
{
    public class BoundingBox
    {
        #region Property Bounds 

        public Rectangle Bounds { get; set; }

        #endregion

        #region Property Target

        public object Target { get; set; }

        #endregion

        #region Property Data

        public object Data { get; set; }

        #endregion

        public virtual bool HitTest(int x,int y)
        {
            return Bounds.Contains(x, y);
        }
    }
}