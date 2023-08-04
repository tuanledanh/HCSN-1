using MISA.WebFresher052023.API.Caclulator;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebFresher052023Tests
{
    [TestFixture]

    public class CalculatorTests
    {
        public Calculator _calculator;
        [SetUp]
        public void Setup()
        {
            _calculator = new Calculator();
        }

        [TestCase(1, 2, 3)]
        [TestCase(1, 4, 5)]
        [TestCase(int.MaxValue, 1, int.MaxValue + (long)1)]

        public void Add_ValidInput_ValidResult(int x, int y, long expectedResult)
        {
            //// Arrange
            //var x = 1;
            //var y = 2;

            //var expectedResult = 3;
            // Act
            var actualResult = _calculator.Add(x, y);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase(1, 2, -1)]
        [TestCase(1, 4, -3)]
        [TestCase(int.MaxValue, int.MinValue, int.MaxValue * (long)2 + 1)]
        public void Sub_ValidInput_ValidResult(int x, int y, long expectedResult)
        {

            var actualResult = _calculator.Sub(x, y);



            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase(1,2,2)]
        [TestCase(-2, 2, -4)]
        [TestCase(int.MaxValue, int.MinValue , int.MaxValue * (double)int.MinValue)]
        public void Mul_ValidInput_ValidResult(int x, int y, double expectedResult)
        {
            var actualResult = _calculator.Mul(x, y);
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [TestCase(4, 2, 2)]
        [TestCase(-2, 2, -1)]
        [TestCase(2, 3, 0.666666)]
        [TestCase(int.MaxValue, int.MinValue, int.MaxValue / (double)int.MinValue)]
        public void Div_ValidInput_ValidResult(int x, int y, double expectedResult)
        {
            var actualResult = _calculator.Div(x, y);
            Assert.That(Math.Abs (actualResult - expectedResult), Is.LessThan(1e-4));
        }

        [Test]
        public void Div_DivideByZero_ThrowException()
        {
           // Arrange
            var x = 1;
            var y = 0;
            var expectedExceptionMessage = "Không được chia cho 0";

            // Act
            var execption =  Assert.Throws<Exception>(() => _calculator.Div(x, y));

            // Assert
            Assert.That(execption.Message, Is.EqualTo(expectedExceptionMessage));
        }
    }
}
