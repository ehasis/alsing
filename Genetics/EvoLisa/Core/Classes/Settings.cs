namespace GenArt.Classes
{
    public class Settings
    {
        private int alphaRangeMax = 60;
        private int alphaRangeMin = 30;
        private int movePointRangeMid = 20;
        private int movePointRangeMin = 4;
        private int pointsMax = 1500;
        private int pointsMin;
        private int pointsPerPolygonMax = 10;
        private int pointsPerPolygonMin = 3;
        private int polygonsMax = 255;
        private int polygonsMin;

        public Settings()
        {
            FlipSplinesMutationRate = 1500;
            FlipFilledMutationRate = 1500;
            AddPolygonMutationRate = 700;
            AddPolygonCloneMutationRate = 700;
            RemovePolygonMutationRate = 1500;
            MovePolygonMutationRate = 700;
            AddPointMutationRate = 1500;
            AddPointCloneMutationRate = 1500;
            RemovePointMutationRate = 1500;
            MovePointMaxMutationRate = 1500;
            MovePointMidMutationRate = 1500;
            MovePointMinMutationRate = 1500;
            ColorMutationRate = 300;
            AlphaMutationRate = 1500;

            Reset();
        }

        //Features

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

        ////Mutation rates

        public int FlipSplinesMutationRate { get; set; }
        public int FlipFilledMutationRate { get; set; }
        public int AddPolygonMutationRate { get; set; }
        public int AddPolygonCloneMutationRate { get; set; }
        public int RemovePolygonMutationRate { get; set; }
        public int MovePolygonMutationRate { get; set; }
        public int AddPointMutationRate { get; set; }
        public int AddPointCloneMutationRate { get; set; }
        public int RemovePointMutationRate { get; set; }
        public int MovePointMaxMutationRate { get; set; }
        public int MovePointMidMutationRate { get; set; }
        public int MovePointMinMutationRate { get; set; }
        public int ColorMutationRate { get; set; }
        public int AlphaMutationRate { get; set; }

        //Ranges

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
            //Mute these three to cause a cell-like growth pattern...
            //   MuteAddPolygonNew = true;
            MuteAddPolygonClone = true;
            MuteMovePointMax = true;
            //      MuteMovePointMid = true;


            MuteAddPointClone = true;

            MuteCurveFillPolygon = true;
            //MuteLineFillPolygon = true;
            MuteLinePolygon = true;
            MuteCurvePolygon = true;

            ColorMutationRate = 600;

            AddPolygonMutationRate = 200;
            AddPolygonCloneMutationRate = 200;
            RemovePolygonMutationRate = 600;
            MovePolygonMutationRate = 200;

            AddPointMutationRate = 600;
            AddPointCloneMutationRate = 600;
            RemovePointMutationRate = 600;
            MovePointMaxMutationRate = 600;
            MovePointMidMutationRate = 600;
            MovePointMinMutationRate = 600;

            AlphaMutationRate = 600;

            ////Limits / Constraints
            AlphaRangeMin = 30;
            AlphaRangeMax = 60;

            PolygonsMax = 50;
            PolygonsMin = 0;

            PointsPerPolygonMax = 50;
            PointsPerPolygonMin = 3;

            PointsMax = 1500;
            PointsMin = 0;

            MovePointRangeMid = 20;
            MovePointRangeMin = 3;
        }

        public void CopyTo(Settings settings)
        {
            settings.FlipFilledMutationRate = FlipFilledMutationRate;
            settings.FlipSplinesMutationRate = FlipSplinesMutationRate;

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

            settings.ColorMutationRate = ColorMutationRate;
            settings.AlphaMutationRate = AlphaMutationRate;

            //Limits / Constraints
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