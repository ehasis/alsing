namespace GenArt.Classes
{
    internal class Settings
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

        internal Settings()
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

        internal bool MuteLinePolygon { get; set; }
        internal bool MuteCurvePolygon { get; set; }
        internal bool MuteLineFillPolygon { get; set; }
        internal bool MuteCurveFillPolygon { get; set; }

        internal bool MuteAddPolygonClone { get; set; }
        internal bool MuteAddPolygonNew { get; set; }
        internal bool MuteRemovePolygon { get; set; }
        internal bool MuteMovePolygon { get; set; }

        internal bool MuteAddPointNew { get; set; }
        internal bool MuteAddPointClone { get; set; }
        internal bool MuteRemovePoint { get; set; }
        internal bool MuteMovePointMax { get; set; }
        internal bool MuteMovePointMid { get; set; }
        internal bool MuteMovePointMin { get; set; }
        internal int Scale { get; set; }

        ////Mutation rates

        internal int FlipSplinesMutationRate { get; set; }
        internal int FlipFilledMutationRate { get; set; }
        internal int AddPolygonMutationRate { get; set; }
        internal int AddPolygonCloneMutationRate { get; set; }
        internal int RemovePolygonMutationRate { get; set; }
        internal int MovePolygonMutationRate { get; set; }
        internal int AddPointMutationRate { get; set; }
        internal int AddPointCloneMutationRate { get; set; }
        internal int RemovePointMutationRate { get; set; }
        internal int MovePointMaxMutationRate { get; set; }
        internal int MovePointMidMutationRate { get; set; }
        internal int MovePointMinMutationRate { get; set; }
        internal int ColorMutationRate { get; set; }
        internal int AlphaMutationRate { get; set; }

        //Ranges

        internal int AlphaRangeMin
        {
            get { return alphaRangeMin; }
            set
            {
                if (value > alphaRangeMax)
                    AlphaRangeMax = value;

                alphaRangeMin = value;
            }
        }

        internal int AlphaRangeMax
        {
            get { return alphaRangeMax; }
            set
            {
                if (value < alphaRangeMin)
                    AlphaRangeMin = value;

                alphaRangeMax = value;
            }
        }

        internal int PolygonsMin
        {
            get { return polygonsMin; }
            set
            {
                if (value > polygonsMax)
                    PolygonsMax = value;

                polygonsMin = value;
            }
        }

        internal int PolygonsMax
        {
            get { return polygonsMax; }
            set
            {
                if (value < polygonsMin)
                    PolygonsMin = value;

                polygonsMax = value;
            }
        }

        internal int PointsPerPolygonMin
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

        internal int PointsPerPolygonMax
        {
            get { return pointsPerPolygonMax; }
            set
            {
                if (value < pointsPerPolygonMin)
                    PointsPerPolygonMin = value;

                pointsPerPolygonMax = value;
            }
        }

        internal int PointsMin
        {
            get { return pointsMin; }
            set
            {
                if (value > pointsMax)
                    PointsMax = value;

                pointsMin = value;
            }
        }

        internal int PointsMax
        {
            get { return pointsMax; }
            set
            {
                if (value < pointsMin)
                    PointsMin = value;

                pointsMax = value;
            }
        }

        internal int MovePointRangeMin
        {
            get { return movePointRangeMin; }
            set
            {
                if (value > movePointRangeMid)
                    MovePointRangeMid = value;

                movePointRangeMin = value;
            }
        }

        internal int MovePointRangeMid
        {
            get { return movePointRangeMid; }
            set
            {
                if (value < movePointRangeMin)
                    MovePointRangeMin = value;

                movePointRangeMid = value;
            }
        }

        internal void Reset()
        {
            //Mute these three to cause a cell-like growth pattern...
        //       MuteAddPolygonNew = true;
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

        internal void CopyTo(Settings settings)
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