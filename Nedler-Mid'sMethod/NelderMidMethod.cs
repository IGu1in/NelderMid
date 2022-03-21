using System;
using System.Collections.Generic;

namespace NelderMidMethod
{
    public class NelderMidMethod
    {
        private List<Point> _points;
        private Point _bestPoint;
        private Point _goodPoint;
        private Point _worstPoint;
        private Point _pointReflection;
        private Point _pointStretching;
        private Point _pointCompression;
        private readonly double _coefReflection = 1;
        private readonly double _coefCompression = 0.5;
        private readonly double _coefStretch = 2;
        private readonly double _epsilon = 0.001;
        private Point _center;
        private bool _isEnding = false;
        private readonly PointComparer _pointComparer = new PointComparer();

        public void Calculate()
        {
            _points = new List<Point> { new Point(0.5, 0), new Point(0, 0.5), new Point(1, 0.5) };

            while (!_isEnding)
            {
                _points.Sort(_pointComparer);
                _bestPoint = _points[0];
                _goodPoint = _points[1];
                _worstPoint = _points[2];
                _center = CenterOfGravity(_bestPoint, _goodPoint);
                _isEnding = EndCondition(_points, _center);

                if (!_isEnding)
                {
                    _pointReflection = Reflection(_worstPoint, _center);

                    if (_pointReflection.Value <= _bestPoint.Value)
                    {
                        _pointStretching = Stretching(_pointReflection, _center);

                        if(_pointStretching.Value < _bestPoint.Value)
                        {
                            _points[2] = _pointStretching;
                        }
                        else
                        {
                            _points[2] = _pointReflection;
                        }
                    }
                    else
                    {
                        if(_goodPoint.Value<_pointReflection.Value && _pointReflection.Value <= _worstPoint.Value)
                        {
                            _pointCompression = Compression(_worstPoint, _center);
                            _points[2] = _pointCompression;
                        }
                        else
                        {
                            if(_bestPoint.Value< _pointReflection.Value && _pointReflection.Value <= _goodPoint.Value)
                            {
                                _points[2] = _pointReflection;
                            }
                            else
                            {
                                if (_pointReflection.Value > _worstPoint.Value)
                                {
                                    _points = Reduction(_points);
                                }
                            }
                        }
                    }
                }
            }

            Console.WriteLine(_points[0].ToString());
        }

        private List<Point> Reduction(List<Point> points)
        {
            for(int i = 1; i<points.Count; i++)
            {
                points[i].X = points[0].X + 0.5 * (points[i].X - points[0].X);
                points[i].Y = points[0].Y + 0.5 * (points[i].Y - points[0].Y);
            }

            return points;
        }

        private Point Compression(Point worstPoint, Point center)
        {
            return new Point(center.X + _coefCompression * (worstPoint.X - center.X), center.Y + _coefCompression * (worstPoint.Y - center.Y));
        }

        private Point Stretching(Point reflectionPoint, Point center)
        {
            return new Point(center.X + _coefStretch * (reflectionPoint.X - center.X), 
                center.Y + _coefStretch * (reflectionPoint.Y - center.Y));
        }

        private Point Reflection(Point worstPoint, Point center)
        {
            return new Point(center.X + _coefReflection * (center.X - worstPoint.X), center.Y + _coefReflection * (center.Y - worstPoint.Y));
        }

        private Point CenterOfGravity(Point p1, Point p2)
        {
            return new Point((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2);
        }

        private bool EndCondition(List<Point> points, Point center)
        {
            double sum = 0;

            foreach (var point in points)
            {
                sum += Math.Pow(point.Value - center.Value, 2);
            }

            sum = Math.Sqrt(sum/3);

            if (sum <= _epsilon)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
