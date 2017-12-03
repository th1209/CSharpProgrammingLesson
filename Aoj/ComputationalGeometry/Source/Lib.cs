using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.CGL.Lib
{
    using Line = Segment;

    using Polygon = List<Point>;

    public struct Point
    {
        private double _x;
        private double _y;

        public double X { get { return _x;} }
        public double Y { get { return _y;} }

        public Point(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point operator *(Point p, double k)
        {
            return new Point(p.X * k, p.Y * k);
        }

        public static Point operator *(double k, Point p)
        {
            return p * k;
        }

        public static explicit operator Vector(Point p)
        {
            return new Vector(p.X, p.Y);
        }

        public static bool IsOrthogonal(Point a1, Point a2, Point b1, Point b2)
        {
            return Vector.IsOrthogonal((Vector)(a1 - a2), (Vector)(b1 - b2));
        }

        public static bool IsParallel(Point a1, Point a2, Point b1, Point b2)
        {
            return Vector.IsParallel((Vector)(a1 - a2), (Vector)(b1 - b2));
        }

        public override string ToString()
        {
            return string.Format("x:{0},y:{1}", X, Y);
        }
    }

    public struct Segment
    {
        private Point _p1;
        private Point _p2;

        public Point P1 { get{ return _p1; } }
        public Point P2 { get{ return _p2; } }

        public Segment(Point p1, Point p2)
        {
            _p1 = p1;
            _p2 = p2;
        }

        public Segment(double x1, double y1, double x2, double y2)
        {
            _p1 = new Point(x1, y1);
            _p2 = new Point(x2, y2);
        }
        public static bool IsOrthogonal(Segment s1, Segment s2)
        {
            return Vector.IsOrthogonal((Vector)(s1.P1 - s1.P2), (Vector)(s2.P1 - s2.P2));
        }

        public static bool IsParallel(Segment s1, Segment s2)
        {
            return Vector.IsParallel((Vector)(s1.P1 - s1.P2), (Vector)(s2.P1 - s2.P2));
        }

        public override string ToString()
        {
            return string.Format("P1x:{0},P1y:{1}, P2x:{2},P2y:{3}", P1.X, P1.Y, P2.X, P2.Y);
        }
    }

    public struct Circle
    {
        private Point _p;
        private double _r;

        public Point P { get { return _p; } }
        public double R { get { return _r; } }

        public Circle(Point p, double r)
        {
            _p = p;
            _r = r;
        }

        public override string ToString()
        {
            return string.Format("px:{0},py:{1},r:{2}", P.X, P.Y, R);
        }
    }

    public struct Vector
    {
        private double _x;
        private double _y;

        public double X { get { return _x;} }
        public double Y { get { return _y;} }

        public Vector(double x, double y)
        {
            _x = x;
            _y = y;
        }

        public double Norm()
        {
            return X * X + Y * Y;
        }

        public double Abs()
        {
            return Math.Sqrt(Norm());
        }

        public Vector Normalize()
        {
            double mag = Abs();
            return new Vector(X * mag, Y * mag);
        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector operator *(Vector v, double k)
        {
            return new Vector(v.X * k, v.Y * k);
        }

        public static Vector operator *(double k, Vector v)
        {
            return v * k;
        }

        public static double Dot(Vector v1, Vector v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public static double Cross(Vector v1, Vector v2)
        {
            return v1.X * v2.Y - v1.Y * v2.X;
        }

        public static bool IsOrthogonal(Vector v1, Vector v2)
        {
            return Dot(v1, v2) == 0.0;
        }

        public static bool IsParallel(Vector v1, Vector v2)
        {
            return Cross(v1, v2) == 0.0;
        }

        public static Vector Project(Vector v, Vector onNormal)
        {
            // mag = |b|cosθ / |a|
            //     = |a||b|cosθ / |a|^2
            double mag = Dot(v, onNormal) / onNormal.Norm();
            return new Vector(onNormal.X * mag, onNormal.Y * mag);
        }

        public static Vector Reflect(Vector v, Vector inNormal)
        {
            // TODO Unity版だと逆向きにしたものが返る。
            Vector p = Project(v, inNormal);
            return 2.0 * p - v;
        }

        public override string ToString()
        {
            return string.Format("x:{0},y:{1}", X, Y);
        }
    }
}