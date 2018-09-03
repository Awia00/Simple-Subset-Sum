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
                new Complex(1.0, 0.0), // x^1
                new Complex(1.0, 0.0), // x^2
                new Complex(0.0, 0.0), // x^3
                new Complex(0.0, 0.0), // x^4
            };

            // 1x^1 + 1x^2
            var polyB = new List<Complex>
            {
                new Complex(1.0, 0.0), // x^1
                new Complex(1.0, 0.0), // x^2
                new Complex(0.0, 0.0), // x^3
                new Complex(0.0, 0.0), // x^4
            };

            // 0x^1 + 1x^2 + 2x^3 + 1x^4
            var polyC = FastFourierTransform.MultiplyPolynomials(polyA, polyB).ToList();

            Assert.Equal(0, polyC[0].Real); // coefficient of x^1 is 0
            Assert.Equal(1, polyC[1].Real); // coefficient of x^2 is 1
            Assert.Equal(2, polyC[2].Real); // coefficient of x^3 is 2
            Assert.Equal(1, polyC[3].Real); // coefficient of x^4 is 1
        }

        [Fact]
        public void TestTwoPolynomial2()
        {
            // 2x^1
            var polyA = new List<Complex> { new Complex(2.0, 0.0), new Complex(0.0, 0.0), new Complex(0.0, 0.0), new Complex(0.0, 0.0) };

            // 1x^1 + 3x^2
            var polyB = new List<Complex> { new Complex(1.0, 0.0), new Complex(3.0, 0.0), new Complex(0.0, 0.0), new Complex(0.0, 0.0) };

            // 0x^1 + 0x^2 + 2x^3 + 6x^4
            var polyC = FastFourierTransform.MultiplyPolynomials(polyA, polyB).ToList();

            Assert.Equal(0, polyC[0].Real); // coefficient of x^1 is 0
            Assert.Equal(0, polyC[1].Real); // coefficient of x^2 is 0
            Assert.Equal(2, polyC[2].Real); // coefficient of x^3 is 2
            Assert.Equal(6, polyC[3].Real); // coefficient of x^4 is 6
        }
    }
}