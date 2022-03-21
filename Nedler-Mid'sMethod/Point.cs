using System;

namespace NelderMidMethod
{
    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Value { get; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
            Value = RosenbrockFunction();
        }

        public override string ToString()
        {
            return "{" + X + "; " + Y + "} - " + Value;
        }

        private double RosenbrockFunction()
        {
            return Math.Pow(1 - X, 2) + 100 * Math.Pow(Y - X * X, 2);
        } 
    }
}
