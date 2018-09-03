using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace Simple_Subset_Sum
{
    public static class FastFourierTransform
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="a"></param>
        /// <param name="inv">Indicating Inverse FFT or Not</param>
        /// <returns></returns>
        private static IList<Complex> RecoursiveFft(IList<Complex> a, bool inv)
        {
            var n = a.Count;
            var y = new Complex[n]; // result

            /* Base Case */
            if (n == 1)
            {
                y[0] = a[0];
                return y;
            }

            /* Calculate principal nth root of unity (i.e. exp(2*PI*i/n)) */
            var wn = inv
                ? new Complex(Math.Cos(-2.0 * Math.PI / n), Math.Sin(-2.0 * Math.PI / n))
                : new Complex(Math.Cos(2.0 * Math.PI / n), Math.Sin(2.0 * Math.PI / n));
            var w = new Complex(1.0, 0.0);

            /* Extract even and odd coefficients */
            var a0 = new Complex[n / 2];
            var a1 = new Complex[n / 2];
            for (var i = 0; i < n / 2; i++)
            {
                a0[i] = a[2 * i];
                a1[i] = a[2 * i + 1];
            }

            /* Calculate 2 FFTs of size n/2 */
            var y0 = RecoursiveFft(a0, inv);
            var y1 = RecoursiveFft(a1, inv);
            /* Combine results from half-size FFTs */
            for (var k = 0; k < n / 2; k++)
            {
                var twiddle = Complex.Multiply(w, y1[k]);
                y[k] = Complex.Add(y0[k], twiddle);
                y[k + n / 2] = Complex.Subtract(y0[k], twiddle);
                w = Complex.Multiply(w, wn);
            }
            return y;
        }

        /// <summary>
        /// Mutliplies two polynomials in nlogn time.
        /// </summary>
        /// <param name="a">polynomial a</param>
        /// <param name="b">polynomial b</param>
        /// <returns></returns>
        public static IEnumerable<Complex> MultiplyPolynomials(IList<Complex> a, IList<Complex> b)
        {
            //  w = cos(2*pi/m) + i*sin(2*pi/m)
            var m = a.Count;
            var fA = RecoursiveFft(a, false); // time O(n log n)
            var fB = RecoursiveFft(b, false); // time O(n log n)
            var fC = new Complex[m];
            for (var i = 0; i < m; i++) // time O(n)
            {
                fC[i] = Complex.Multiply(fA[i], fB[i]);
            }
            // 1 / m * FFT(fC, m, w^-1) ????
            var result = RecoursiveFft(fC, true);
            return result.Select(i => i / new Complex(m, 0)); // time O(n log n)
        }
    }
}