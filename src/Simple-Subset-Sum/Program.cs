using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Simple_Subset_Sum
{
    /// <summary>
    /// Calculates all subset sums up to a certain integer.
    /// </summary>
    public class Program
    {
        // Notation from the article:

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

        /// <summary>
        /// Finds all pairwise sums of X and Y with values up to [u]
        /// Can be done with FftCompute in O(u log u) time.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <param name="u"></param>
        /// <returns></returns>
        private static IEnumerable<int> PairWiseSums(IEnumerable<int> s, IEnumerable<int> t, int u)
        {
            var x = Complex.ImaginaryOne; // ????
            var fs = s.Aggregate(Complex.Zero, (current, i) => current + Complex.Pow(x, i));
            var ft = t.Aggregate(Complex.Zero, (current, i) => current + Complex.Pow(x, i));
            var g = fs * ft;
            var fftBuffer = new[] { g };
            //FastFourierTransform.FftCompute(fftBuffer);
            return fftBuffer.Select(poly => (int)poly.Magnitude);
        }

        /// <summary>
        /// Finds the set of pairwise sums of points in X and Y where the first element of the pair sums up to u.
        /// Can be done with 2 dimensional FftCompute in (u v log(u v)) time.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="u"></param>
        /// <returns></returns>
        private static IEnumerable<(int, int)> PairWiseSums(IEnumerable<(int, int)> s, IEnumerable<(int, int)> t, int u)
        {
            var x = Complex.ImaginaryOne; // ????
            var fs = s.Sum(pair => (Complex.Pow(x, pair.Item1) + Complex.Pow(x, pair.Item2)).Real);
            var ft = t.Sum(pair => (Complex.Pow(x, pair.Item1) * Complex.Pow(x, pair.Item2)).Real);
            var g = fs * ft;
            var fftBuffer = new Complex[] { g };
            //FastFourierTransform.FftCompute(fftBuffer);
            return new HashSet<(int, int)> { ((int)fftBuffer[0].Real, 0) };
        }

        private static IEnumerable<(int, int)> AllSubsetSumsWithCardinality(ICollection<int> s, int u)
        {
            if (s.Count == 1)
            {
                return new HashSet<(int, int)> { (0, 0), (s.Single(), 1) };
            }

            var t = s.Take(s.Count / 2);
            var sMinusT = s.Skip(s.Count / 2);
            return PairWiseSums(AllSubsetSumsWithCardinality(t.ToHashSet(), u), AllSubsetSumsWithCardinality(sMinusT.ToHashSet(), u), u);
        }

        /// <summary>
        /// Calculates the set of all realizable numbers of s up to n.
        /// </summary>
        /// <param name="s">set of numbers</param>
        /// <param name="u">upper bound</param>
        public IEnumerable<int> AllSubsetSums(ICollection<int> s, int u)
        {
            var n = s.Count;

            var b = (int)Math.Sqrt(n * Math.Log(n));
            var r = new List<IEnumerable<int>>(b);
            for (var l = 0; l < b; l++)
            {
                var sL = s.Where(x => x % b == l); // removed union since we could just as well run on sL.
                var qL = sL.Select(x => (x - l) / b);
                var sUMinusBofQl = AllSubsetSumsWithCardinality(qL.ToList(), u / b);
                r[l] = sUMinusBofQl.Select((z, j) => z.Item2 + l * j).ToList();
            }

            var result = r[0];
            for (var i = 1; i < r.Count; i++)
            {
                result = PairWiseSums(result, r[i], u);
            }
            return result;
        }

        private static void Main(string[] args)
        {
            Complex[] input = { 1.0, 1.0, 1.0, 1.0, 0.0, 0.0, 0.0, 0.0 };

            //FastFourierTransform.FftCompute(input);

            Console.WriteLine("Results:");
            foreach (var c in input)
            {
                Console.WriteLine(c);
            }

            var result = PairWiseSums(new List<int> { 1, 2 }, new List<int> { 3, 4 }, 10);
        }
    }
}