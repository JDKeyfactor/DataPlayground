using System;
using System.Collections.Generic;
using System.Linq;

namespace DataPlayground
{
    internal class Program
    {
        static List<Func<int[], bool>> patterns = new List<Func<int[], bool>>()
        {
            
            // Three or more of a kind
            x => x.GroupBy(i => i).Any(i => i.Count() >= 3),
            // xaxbxc or axbxcx (three of a kind evenly spaced)
            //x => (x[0] == x[2] && x[2] == x[4]) || (x[1] == x[3] && x[3] == x[5]),
            // xxx abc or abc xxx (grouped three of a kind)
            //x => (x[0] == x[1] && x[1] == x[2]) || (x[3] == x[4] && x[4] == x[5]),
            // xxa yyb or xax yby or axx byy
            //x => (x[0] == x[1] && x[3] == x[4]) || (x[0] == x[2] && x[3] == x[5]) || (x[1] == x[2] && x[4] == x[5]),
            // xya xyb or xay xby or axy bxy
            //x => (x[0] == x[3] && x[1] == x[4]) || (x[0] == x[3] && x[2] == x[5]) || (x[1] == x[4] && x[2] == x[5]),
            // Two pair
            x => x.GroupBy(i => i).Select(g => g.Count()).Count(i => i>=2) == 2,
            // No more than three distinct digits
            x => x.GroupBy(i => i).Count() <= 3,
            // All even
            x => x.All(i => i % 2 == 0),
            // All odd
            x => x.All(i => i % 2 == 1),
            // Four straight, consecutive or not
            //x => Enumerable.Range(1,x.Length-1).Select(i => x[i]-x[i-1]).GroupBy(i => i).Any(g => (g.Key == 1 || g.Key == -1) && g.Count() >= 3),
            // Non-decreasing
            x => Enumerable.Range(1,x.Length-1).All(i => x[i] >= x[i-1]),
            // Non-increasing
            x => Enumerable.Range(1,x.Length-1).All(i => x[i] <= x[i-1]),
            // All distinct
            x => x.GroupBy(i => i).Count() == 6,
            // Two leading zeroes
            //x => x[0] == 0 && x[1] == 0
        };

        // Find all six-digit pin numbers that follow a "special" pattern.
        static void Main(string[] args)
        {
            //IEnumerable<String[]> test = Enumerable.Range(0, 10).Select(x => x.ToString("D6"));
            IEnumerable<int[]> range = Enumerable.Range(0, 1000000).Select(x => x.ToString("D6").ToCharArray().Select(c => int.Parse($"{c}")).ToArray());
            IEnumerable<int[]> special = range.Where(x => patterns.Any(f => f(x)));
            //IEnumerable<int[]> basic = range.Where(x => patterns.All(f => !f(x))).ToArray();
            //var r = special.ElementAt(136);
            //var y = range.ElementAt(136).GroupBy(i => i).Count();
            Console.Write(special.Count());
            
            //Console.ReadLine();
        }
    }
}
