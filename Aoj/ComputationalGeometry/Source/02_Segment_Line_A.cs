using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Aoj.CGL.Lib;

namespace Aoj.CGL.Chapter2A
{
    class Program
    {
        public static void Solve()
        {
            int n = int.Parse(Console.ReadLine());
 
            for (int i = 0; i < n; i++)
            {
                int[] input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                var seg1 = new Segment(input[0],input[1],input[2],input[3]);
                var seg2 = new Segment(input[4],input[5],input[6],input[7]);

                int answer = 0;
                if (Segment.IsOrthogonal(seg1, seg2))
                    answer = 1;
                else if (Segment.IsParallel(seg1, seg2))
                    answer = 2;

                Console.WriteLine(answer);
            }
        }

        // public static void Main()
        // {
        //     Console.SetIn(new System.IO.StreamReader("ComputationalGeometry/Input/02_Segment_Line_A_01.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();

        //     Solve();

        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}