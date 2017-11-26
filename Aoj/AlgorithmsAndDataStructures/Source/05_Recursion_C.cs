using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter5C
{
    class Point
    {
        public double X {get; set;}
        public double Y {get; set;}

        public Point()
        {
            X = 0.0;
            Y = 0.0;
        }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
    }

    class Program
    {
        public static void Koch(int depth, Point a, Point b, Action<Point> printFunc)
        {
            if (depth <= 0)
                return;

            double rad = Math.PI * 60.0 / 180.0;

            var s = new Point(
                (2.0 * a.X + 1.0 * b.X) / 3.0,
                (2.0 * a.Y + 1.0 * b.Y) / 3.0
            );
            var t = new Point(
                (1.0 * a.X + 2.0 * b.X) / 3.0,
                (1.0 * a.Y + 2.0 * b.Y) / 3.0
            );
            var u = new Point(
                (t.X - s.X) * Math.Cos(rad) - (t.Y - s.Y) * Math.Sin(rad) + s.X,
                (t.X - s.X) * Math.Sin(rad) + (t.Y - s.Y) * Math.Cos(rad) + s.Y
            );

            Koch(depth - 1, a, s, printFunc);
            printFunc(s);
            Koch(depth - 1, s, u, printFunc);
            printFunc(u);
            Koch(depth - 1, u, t, printFunc);
            printFunc(t);
            Koch(depth - 1, t, b, printFunc);
        }

        public static void Solve(int n)
        {
            var a = new Point(0.0, 0.0);
            var b = new Point(100.0, 0.0);

            Action<Point> printFunc = (Point p) => {
                Console.WriteLine("{0:0.00000000} {1:0.00000000}", p.X, p.Y);
            };

            printFunc(a);
            Koch(n, a, b, printFunc);
            printFunc(b);
        }

        // public static void Main()
        // {
        //     int n = int.Parse(Console.ReadLine());
        //     Solve(n);
        // }
    }
}