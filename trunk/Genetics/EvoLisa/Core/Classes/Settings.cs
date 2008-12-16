using System.Drawing.Imaging;
using System.Xml.Serialization;
using System;
namespace GenArt.Classes
{
    public class Settings
    {
        public Settings()
        {
            HistoryImageFormat = ImageFormat.Jpeg;

            Reset();
        }

        public bool MuteLinePolygon { get; set; }
        public bool MuteCurvePolygon { get; set; }
        public bool MuteLineFillPolygon { get; set; }
        public bool MuteCurveFillPolygon { get; set; }

        public bool MuteAddPolygonClone { get; set; }
        public bool MuteAddPolygonNew { get; set; }
        public bool MuteRemovePolygon { get; set; }
        public bool MuteMovePolygon { get; set; }

        public bool MuteAddPointNew { get; set; }
        public bool MuteAddPointClone { get; set; }
        public bool MuteRemovePoint { get; set; }
        public bool MuteMovePointMax { get; set; }
        public bool MuteMovePointMid { get; set; }
        public bool MuteMovePointMin { get; set; }

        public int Scale { get; set; }

        [XmlIgnore]
        public ImageFormat HistoryImageFormat 
        {
            get 
            {
                if (string.IsNullOrEmpty(HistoryImageFormatName))
                    return ImageFormat.Jpeg;
                switch (HistoryImageFormatName.ToLower())
                {
                    case "bmp":
                        return ImageFormat.Bmp;
                    case "gif":
                        return ImageFormat.Gif;
                    default:
                        return ImageFormat.Jpeg;
                }
            }
            set { HistoryImageFormatName = value.ToString(); } 
        }
        public string HistoryImageFormatName { get; set; }

        public int HistoryImageScale { get; set; }
        public decimal HistoryImageSteps { get; set; }

        [XmlIgnore]
        public HistorySaveTrigger HistoryImageSaveTrigger
        {
            get 
            {
                if (string.IsNullOrEmpty(HistoryImageSaveTriggerName))
                    return HistorySaveTrigger.None;
                return (HistorySaveTrigger)Enum.Parse(typeof(HistorySaveTrigger), HistoryImageSaveTriggerName); 
            }
            set { HistoryImageSaveTriggerName = value.ToString(); }
        }

        public string HistoryImageSaveTriggerName { get; set; }

        //Mutation rates
        private int addPointMutationRate = 1500;
        private int addPointCloneMutationRate = 1500;
        private int addPolygonMutationRate = 700;
        private int addPolygonCloneMutationRate = 700;
        private int alphaMutationRate = 1500;
        private int alphaRangeMax = 60;
        private int alphaRangeMin = 30;
        private int blueMutationRate = 1500;
        private int blueRangeMax = 255;
        private int blueRangeMin;
        private int greenMutationRate = 1500;
        private int greenRangeMax = 255;
        private int greenRangeMin;
        private int movePointMaxMutationRate = 1500;
        private int movePointMidMutationRate = 1500;
        private int movePointMinMutationRate = 1500;
        private int movePointRangeMid = 20;
        private int movePointRangeMin = 3;
        private int movePolygonMutationRate = 700;
        private int pointsMax = 1500;
        private int pointsMin;
        private int pointsPerPolygonMax = 10;
        private int pointsPerPolygonMin = 3;
        private int polygonsMax = 255;
        private int polygonsMin;
        private int redMutationRate = 1500;
        private int redRangeMax = 255;
        private int redRangeMin;
        private int removePointMutationRate = 1500;

        private int removePolygonMutationRate = 1500;

        public int AddPolygonMutationRate
        {
            get { return addPolygonMutationRate; }
            set { addPolygonMutationRate = value; }
        }

        public int AddPolygonCloneMutationRate
        {
            get { return addPolygonCloneMutationRate; }
            set { addPolygonCloneMutationRate = value; }
        }

        public int RemovePolygonMutationRate
        {
            get { return removePolygonMutationRate; }
            set { removePolygonMutationRate = value; }
        }

        public int MovePolygonMutationRate
        {
            get { return movePolygonMutationRate; }
            set { movePolygonMutationRate = value; }
        }

        public int AddPointMutationRate
        {
            get { return addPointMutationRate; }
            set { addPointMutationRate = value; }
        }

        public int AddPointCloneMutationRate
        {
            get { return addPointCloneMutationRate; }
            set { addPointCloneMutationRate = value; }
        }

        public int RemovePointMutationRate
        {
            get { return removePointMutationRate; }
            set { removePointMutationRate = value; }
        }

        public int MovePointMaxMutationRate
        {
            get { return movePointMaxMutationRate; }
            set { movePointMaxMutationRate = value; }
        }

        public int MovePointMidMutationRate
        {
            get { return movePointMidMutationRate; }
            set { movePointMidMutationRate = value; }
        }

        public int MovePointMinMutationRate
        {
            get { return movePointMinMutationRate; }
            set { movePointMinMutationRate = value; }
        }

        public int RedMutationRate
        {
            get { return redMutationRate; }
            set { redMutationRate = value; }
        }

        public int GreenMutationRate
        {
            get { return greenMutationRate; }
            set { greenMutationRate = value; }
        }

        public int BlueMutationRate
        {
            get { return blueMutationRate; }
            set { blueMutationRate = value; }
        }

        public int AlphaMutationRate
        {
            get { return alphaMutationRate; }
            set { alphaMutationRate = value; }
        }

        //Ranges

        public int RedRangeMin
        {
            get { return redRangeMin; }
            set
            {
                if (value > redRangeMax)
                    RedRangeMax = value;

                redRangeMin = value;
            }
        }

        public int RedRangeMax
        {
            get { return redRangeMax; }
            set
            {
                if (value < redRangeMin)
                    RedRangeMin = value;

                redRangeMax = value;
            }
        }

        public int GreenRangeMin
        {
            get { return greenRangeMin; }
            set
            {
                if (value > greenRangeMax)
                    GreenRangeMax = value;

                greenRangeMin = value;
            }
        }

        public int GreenRangeMax
        {
            get { return greenRangeMax; }
            set
            {
                if (value < greenRangeMin)
                    GreenRangeMin = value;

                greenRangeMax = value;
            }
        }

        public int BlueRangeMin
        {
            get { return blueRangeMin; }
            set
            {
                if (value > blueRangeMax)
                    BlueRangeMax = value;

                blueRangeMin = value;
            }
        }

        public int BlueRangeMax
        {
            get { return blueRangeMax; }
            set
            {
                if (value < blueRangeMin)
                    BlueRangeMin = value;

                blueRangeMax = value;
            }
        }

        public int AlphaRangeMin
        {
            get { return alphaRangeMin; }
            set
            {
                if (value > alphaRangeMax)
                    AlphaRangeMax = value;

                alphaRangeMin = value;
            }
        }

        public int AlphaRangeMax
        {
            get { return alphaRangeMax; }
            set
            {
                if (value < alphaRangeMin)
                    AlphaRangeMin = value;

                alphaRangeMax = value;
            }
        }

        public int PolygonsMin
        {
            get { return polygonsMin; }
            set
            {
                if (value > polygonsMax)
                    PolygonsMax = value;

                polygonsMin = value;
            }
        }

        public int PolygonsMax
        {
            get { return polygonsMax; }
            set
            {
                if (value < polygonsMin)
                    PolygonsMin = value;

                polygonsMax = value;
            }
        }

        public int PointsPerPolygonMin
        {
            get { return pointsPerPolygonMin; }
            set
            {
                if (value > pointsPerPolygonMax)
                    PointsPerPolygonMax = value;

                if (value < 3)
                    return;

                pointsPerPolygonMin = value;
            }
        }

        public int PointsPerPolygonMax
        {
            get { return pointsPerPolygonMax; }
            set
            {
                if (value < pointsPerPolygonMin)
                    PointsPerPolygonMin = value;

                pointsPerPolygonMax = value;
            }
        }

        public int PointsMin
        {
            get { return pointsMin; }
            set
            {
                if (value > pointsMax)
                    PointsMax = value;

                pointsMin = value;
            }
        }

        public int PointsMax
        {
            get { return pointsMax; }
            set
            {
                if (value < pointsMin)
                    PointsMin = value;

                pointsMax = value;
            }
        }

        public int MovePointRangeMin
        {
            get { return movePointRangeMin; }
            set
            {
                if (value > movePointRangeMid)
                    MovePointRangeMid = value;

                movePointRangeMin = value;
            }
        }

        public int MovePointRangeMid
        {
            get { return movePointRangeMid; }
            set
            {
                if (value < movePointRangeMin)
                    MovePointRangeMin = value;

                movePointRangeMid = value;
            }
        }

        public void Reset()
        {
            //ActiveAddPolygonMutationRate = 700;
            //ActiveRemovePolygonMutationRate = 1500;
            //ActiveMovePolygonMutationRate = 700;

            //ActiveAddPointMutationRate = 1500;
            //ActiveRemovePointMutationRate = 1500;
            //ActiveMovePointMaxMutationRate = 1500;
            //ActiveMovePointMidMutationRate = 1500;
            //ActiveMovePointMinMutationRate = 1500;

            //ActiveRedMutationRate = 1500;
            //ActiveGreenMutationRate = 1500;
            //ActiveBlueMutationRate = 1500;
            //ActiveAlphaMutationRate = 1500;

            ////Limits / Constraints
            //ActiveRedRangeMin = 0;
            //ActiveRedRangeMax = 255;
            //ActiveGreenRangeMin = 0;
            //ActiveGreenRangeMax = 255;
            //ActiveBlueRangeMin = 0;
            //ActiveBlueRangeMax = 255;
            //ActiveAlphaRangeMin = 30;
            //ActiveAlphaRangeMax = 60;

            //ActivePolygonsMax = 255;
            //ActivePolygonsMin = 0;

            //ActivePointsPerPolygonMax = 10;
            //ActivePointsPerPolygonMin = 3;

            //ActivePointsMax = 1500;
            //ActivePointsMin = 0;

            //ActiveMovePointRangeMid = 20;
            //ActiveMovePointRangeMin = 3;

        }

        public void CopyTo(Settings settings)
        {

            settings.MuteAddPointClone = MuteAddPointClone;
            settings.MuteAddPointNew = MuteAddPointNew;
            settings.MuteAddPolygonClone = MuteAddPolygonClone;
            settings.MuteAddPolygonNew = MuteAddPolygonNew;
            settings.MuteCurveFillPolygon = MuteCurveFillPolygon;
            settings.MuteCurvePolygon = MuteCurvePolygon;
            settings.MuteLineFillPolygon = MuteLineFillPolygon;
            settings.MuteLinePolygon = MuteLinePolygon;
            settings.MuteMovePointMax = MuteMovePointMax;
            settings.MuteMovePointMid = MuteMovePointMid;
            settings.MuteMovePointMin = MuteMovePointMin;
            settings.MuteMovePolygon = MuteMovePolygon;
            settings.MuteRemovePoint = MuteRemovePoint;
            settings.MuteRemovePolygon = MuteRemovePolygon;

            settings.Scale = Scale;
            settings.HistoryImageFormat = HistoryImageFormat;
            settings.HistoryImageFormatName = HistoryImageFormatName;
            settings.HistoryImageSaveTrigger = HistoryImageSaveTrigger;
            settings.HistoryImageSaveTriggerName = HistoryImageSaveTriggerName;
            settings.HistoryImageScale = HistoryImageScale;
            settings.HistoryImageSteps = HistoryImageSteps;

            settings.AddPolygonMutationRate = AddPolygonMutationRate;
            settings.AddPolygonCloneMutationRate = AddPolygonCloneMutationRate;
            settings.RemovePolygonMutationRate = RemovePolygonMutationRate;
            settings.MovePolygonMutationRate = MovePolygonMutationRate;

            settings.AddPointMutationRate = AddPointMutationRate;
            settings.AddPointCloneMutationRate = AddPointCloneMutationRate;
            settings.RemovePointMutationRate = RemovePointMutationRate;
            settings.MovePointMaxMutationRate = MovePointMaxMutationRate;
            settings.MovePointMidMutationRate = MovePointMidMutationRate;
            settings.MovePointMinMutationRate = MovePointMinMutationRate;

            settings.RedMutationRate = RedMutationRate;
            settings.GreenMutationRate = GreenMutationRate;
            settings.BlueMutationRate = BlueMutationRate;
            settings.AlphaMutationRate = AlphaMutationRate;

            //Limits / Constraints
            settings.RedRangeMin = RedRangeMin;
            settings.RedRangeMax = RedRangeMax;
            settings.GreenRangeMin = GreenRangeMin;
            settings.GreenRangeMax = GreenRangeMax;
            settings.BlueRangeMin = BlueRangeMin;
            settings.BlueRangeMax = BlueRangeMax;
            settings.AlphaRangeMin = AlphaRangeMin;
            settings.AlphaRangeMax = AlphaRangeMax;

            settings.PolygonsMax = PolygonsMax;
            settings.PolygonsMin = PolygonsMin;

            settings.PointsPerPolygonMax = PointsPerPolygonMax;
            settings.PointsPerPolygonMin = PointsPerPolygonMin;

            settings.PointsMax = PointsMax;
            settings.PointsMin = PointsMin;

            settings.MovePointRangeMid = MovePointRangeMid;
            settings.MovePointRangeMin = MovePointRangeMin;
        }
    }
}