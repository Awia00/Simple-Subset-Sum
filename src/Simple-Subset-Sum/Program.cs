using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Simple_Subset_Sum
{
    public class Program
    {
        // notation from the article: 

        // [u]      = { 0, 1, ..., ceil(u) }
        // that is the set of all natural numbers up u

        // S_u(X)   = { SUM Y | Y subset of X } union [u]
        // that is all subset sums of X suming to all natural numbers up to u.

        // S^#_u(X) = { (SUM Y, |Y|) | Y subset of X } union ([u], N)        
        // That is all subset sums of X summing up to u, and for each of those their cardinality information.

        // X (X)_u Y = { x + y | x in X, y in Y } union [u]
        // That is all pairwise sums of X and Y with values up to [u]

        // X (X)_u Y = { (x1 + y1, x2 + y2) | (x1, x2) in X, (y1, y2) in Y } union ([u], N)
        // That is the set of pairwise sums of points in X and Y where the first element of the pair sums up to u.

        // Fs = fs(x) = SUM x^i for i in S
        // That is the characteristic polynomial of S

        private static IEnumerable<int> PairWiseSums(IEnumerable<int> x, IEnumerable<int> y, int u)
        {
            var fs = x.Sum(i => Math.Pow(i,i)); // what is x?
            var ft = y.Sum(i => Math.Pow(i,i)); // what is x?
            var g = fs * ft;
            FastFourierTransform.FFT(new Complex[]{g});
            return new List<int>{ (int)g };
        }

        private static IEnumerable<(int, int)> PairWiseSums(IEnumerable<(int, int)> x, IEnumerable<(int, int)> y, int u)
        {
            var fs = x.Sum(pair => Math.Pow(pair.Item1, pair.Item1) * Math.Pow(pair.Item2, pair.Item2)); // what is x?
            var ft = y.Sum(pair => Math.Pow(pair.Item1, pair.Item1) * Math.Pow(pair.Item2, pair.Item2)); // what is x?
            var g = fs * ft;
            FastFourierTransform.FFT(new Complex[] { g });
            return new List<(int,int)> { ((int)g,0) };
        }

        private static IEnumerable<(int, int)> AllSubsetSumsWithCardinality(ICollection<int> s, int u)
        {
            if(s.Count == 1)
                return new List<(int, int)>{ (0,0), (s.Single(),1)};
            var t = s.Take(s.Count / 2);
            var sMinusT = s.Skip(s.Count / 2);
            return PairWiseSums(AllSubsetSumsWithCardinality(t.ToList(), u), AllSubsetSumsWithCardinality(sMinusT.ToList(), u), u);
        }

        /// <summary>
        /// Calculates the set of all realizable numbers of s up to n.
        /// </summary>
        /// <param name="s">set of numbers</param>
        /// <param name="u">upper bound</param>
        public void AllSubsetSums(ICollection<int> s, int u)
        {
            var n = s.Count;
            var sMax = s.Max(); 

            var r = new List<List<int>>();
            var b = (int) Math.Sqrt(n * Math.Log(n));
            for (var l = 0; l < b; l++)
            {
                var sL = s.Union(Enumerable.Range(0, sMax).Where(x => x % b == l)); // sMax is used instead of all natural numbers.
                var qL = s.Select(x => (x-l)/b);
                var sUMinusBofQl = AllSubsetSumsWithCardinality(qL.ToList(), u / b);
                r.Add(sUMinusBofQl.Select((z, j) => z.Item2 + l * j).ToList());
            }

            var result = r[0];
            for (var i = 1; i < r.Count; i++)
            {
                result = PairWiseSums(result, r[i], u).ToList();
            }
        }

        private static void Main(string[] args)
        {
            Complex[] input = { 1.0, 1.0, 1.0, 1.0, 0.0, 0.0, 0.0, 0.0 };

            FastFourierTransform.FFT(input);

            Console.WriteLine("Results:");
            foreach (var c in input)
            {
                Console.WriteLine(c);
            }
        }
    }
}
