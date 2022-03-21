using System.Collections.Generic;

namespace NelderMidMethod
{
    public class PointComparer : IComparer<Point>
    {
        public int Compare(Point x, Point y)
        {
            if (x.Value > y.Value)
            {
                return 1;
            }
            else
            {
                if(x.Value < y.Value)
                {
                    return -1;
                }
            }

            return 0;
        }
    }
}
