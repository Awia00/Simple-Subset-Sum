using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Xunit;

namespace Simple_Subset_Sum.Tests
{
    public class FastFourierTransformTests
    {
        [Fact]
        public void TestTwoPolynomial1()
        {
            // 1x^1 + 1x^2
            var polyA = new List<Complex>
            {
                new Complex(0.0, 0.0), // x^0
                new Complex(1.0, 0.0), // x^1
                new Complex(1.0, 0.0), // x^2
                new Complex(0.0, 0.0), // x^3
                new Complex(0.0, 0.0), // x^4
                new Complex(0.0, 0.0), // x^5
                new Complex(0.0, 0.0), // x^6
                new Complex(0.0, 0.0), // x^7
            };

            // 1x^1 + 1x^2
            var polyB = new List<Complex>
            {
                new Complex(0.0, 0.0), // x^0
                new Complex(1.0, 0.0), // x^1
                new Complex(1.0, 0.0), // x^2
                new Complex(0.0, 0.0), // x^3
                new Complex(0.0, 0.0), // x^4
                new Complex(0.0, 0.0), // x^5
                new Complex(0.0, 0.0), // x^6
                new Complex(0.0, 0.0), // x^7
            };

            // 0x^1 + 1x^2 + 2x^3 + 1x^4
            var polyC = FastFourierTransform.MultiplyPolynomials(polyA, polyB).ToList();

            Assert.Equal(0, Math.Round(polyC[0].Real)); // coefficient of x^0 is 0
            Assert.Equal(0, Math.Round(polyC[1].Real)); // coefficient of x^1 is 0
            Assert.Equal(1, Math.Round(polyC[2].Real)); // coefficient of x^2 is 1
            Assert.Equal(2, Math.Round(polyC[3].Real)); // coefficient of x^3 is 2
            Assert.Equal(1, Math.Round(polyC[4].Real)); // coefficient of x^4 is 1
            Assert.Equal(0, Math.Round(polyC[5].Real)); // coefficient of x^5 is 1
            Assert.Equal(0, Math.Round(polyC[6].Real)); // coefficient of x^6 is 1
            Assert.Equal(0, Math.Round(polyC[7].Real)); // coefficient of x^7 is 1
        }

        [Fact]
        public void TestTwoPolynomial2()
        {
            // 2x^2
            var polyA = new List<Complex>
            {
                new Complex(0.0, 0.0), // x^0
                new Complex(0.0, 0.0), // x^1
                new Complex(2.0, 0.0), // x^2
                new Complex(0.0, 0.0), // x^3
                new Complex(0.0, 0.0), // x^4
                new Complex(0.0, 0.0), // x^5
                new Complex(0.0, 0.0), // x^6
                new Complex(0.0, 0.0), // x^7
            };

            // 1x^1 + 3x^2
            var polyB = new List<Complex>
            {
                new Complex(0.0, 0.0), // x^0
                new Complex(1.0, 0.0), // x^1
                new Complex(3.0, 0.0), // x^2
                new Complex(0.0, 0.0), // x^3
                new Complex(0.0, 0.0), // x^4
                new Complex(0.0, 0.0), // x^5
                new Complex(0.0, 0.0), // x^6
                new Complex(0.0, 0.0), // x^7
            };

            // 0x^1 + 0x^2 + 2x^3 + 6x^4
            var polyC = FastFourierTransform.MultiplyPolynomials(polyA, polyB).ToList();

            Assert.Equal(0, Math.Round(polyC[0].Real)); // coefficient of x^0 is 0
            Assert.Equal(0, Math.Round(polyC[1].Real)); // coefficient of x^1 is 0
            Assert.Equal(0, Math.Round(polyC[2].Real)); // coefficient of x^2 is 0
            Assert.Equal(2, Math.Round(polyC[3].Real)); // coefficient of x^3 is 2
            Assert.Equal(6, Math.Round(polyC[4].Real)); // coefficient of x^4 is 6
            Assert.Equal(0, Math.Round(polyC[5].Real)); // coefficient of x^5 is 0
            Assert.Equal(0, Math.Round(polyC[6].Real)); // coefficient of x^6 is 0
            Assert.Equal(0, Math.Round(polyC[7].Real)); // coefficient of x^7 is 0
        }

        [Fact]
        public void TestTwoPolynomial3()
        {
            // x^2 +  2x + 4
            var polyA = new List<Complex>
            {
                new Complex(4.0, 0.0), // x^0
                new Complex(2.0, 0.0), // x^1
                new Complex(1.0, 0.0), // x^2
                new Complex(0.0, 0.0), // x^3
                new Complex(0.0, 0.0), // x^4
                new Complex(0.0, 0.0), // x^5
                new Complex(0.0, 0.0), // x^6
                new Complex(0.0, 0.0), // x^7
            };

            // 3x^2 +  2x + 1
            var polyB = new List<Complex>
            {
                new Complex(1.0, 0.0), // x^0
                new Complex(2.0, 0.0), // x^1
                new Complex(3.0, 0.0), // x^2
                new Complex(0.0, 0.0), // x^3
                new Complex(0.0, 0.0), // x^4
                new Complex(0.0, 0.0), // x^5
                new Complex(0.0, 0.0), // x^6
                new Complex(0.0, 0.0), // x^7
            };

            // 3x^4 + 8x^3 + 17x^2 + 10x + 4
            var polyC = FastFourierTransform.MultiplyPolynomials(polyA, polyB).ToList();

            Assert.Equal(4, Math.Round(polyC[0].Real)); // coefficient of x^0 is 4
            Assert.Equal(10, Math.Round(polyC[1].Real)); // coefficient of x^1 is 10
            Assert.Equal(17, Math.Round(polyC[2].Real)); // coefficient of x^2 is 17
            Assert.Equal(8, Math.Round(polyC[3].Real)); // coefficient of x^3 is 8
            Assert.Equal(3, Math.Round(polyC[4].Real)); // coefficient of x^4 is 3
            Assert.Equal(0, Math.Round(polyC[5].Real)); // coefficient of x^5 is 0
            Assert.Equal(0, Math.Round(polyC[6].Real)); // coefficient of x^6 is 0
            Assert.Equal(0, Math.Round(polyC[7].Real)); // coefficient of x^7 is 0
        }
    }
}