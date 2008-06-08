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

        //#region Property Section
        //private string section;
        //public string Section
        //{
        //    get
        //    {
        //        return this.section;
        //    }
        //    set
        //    {
        //        this.section = value;
        //    }
        //}
        //#endregion
    }
}