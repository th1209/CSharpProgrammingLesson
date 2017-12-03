using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Aoj.CGL.Lib;

namespace Aoj.CGL.Chapter1A
{
    class Program
    {
        public static void Solve()
        {
            int[] input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            Vector v1 = new Vector(input[0], input[1]);
            Vector v2 = new Vector(input[2], input[3]);
            Vector baseV = v2 - v1;

            int q = int.Parse(Console.ReadLine());
 
            for (int i = 0; i < q; i++)
            {
                int[] x_y = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                Vector v = new Vector(x_y[0], x_y[1]) - v1;
                Vector p = Vector.Project(v, baseV);

                Vector answer = v1 + p;
                Console.WriteLine("{0:0.0000000000} {1:0.0000000000}", answer.X, answer.Y);
            }
        }

        // public static void Main()
        // {
        //     // Console.SetIn(new System.IO.StreamReader("ComputationalGeometry/Input/01_Projection_A_01.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();

        //     Solve();

        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}