using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMSUnitTesting
{
    public class CalculatorUnitTest
    {
        [Fact]
        public void ShouldReturnTrueSum3Nubers()
        {
            // (1)Arrange
            Calculator calculator = new Calculator();
            int a = 1;
            int b = 2;
            int c = 3;
            int expected = 6;
            // Act
            int result = calculator.Sum(a, b, c);//from obj
            // Assert
            Assert.Equal(expected, result);//Assert is to check the result from Xunit
        }
    }
}
