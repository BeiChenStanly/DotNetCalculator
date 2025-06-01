using Xunit;

namespace Calculator.Tests
{
    public class CalculatorComplexTests
    {
        [Theory]
        [InlineData("1+2*3-4/2", 5)]                  // 1 + (2*3) - (4/2) = 1+6-2=5
        [InlineData("2^3^2", 512)]                    // 2^(3^2) = 2^9 = 512
        [InlineData("2^(3^2)", 512)]                  // 2^(3^2) = 512
        [InlineData("(2^3)^2", 64)]                   // (2^3)^2 = 8^2 = 64
        [InlineData("sqrt(16)+sqrt(9)", 7)]
        [InlineData("max(1,2,3,4,5,6,7,8,9,10)", 10)]
        [InlineData("min(10,9,8,7,6,5,4,3,2,1)", 1)]
        [InlineData("max(1+2, 3*4, 5^2)", 25)]        // max(3,12,25)
        [InlineData("min(sin(0), cos(0), 3)", 0)]     // min(0,1,3)
        [InlineData("sin(0)+cos(0)", 1)]
        [InlineData("ln(exp(0))", 0)]
        [InlineData("exp(ln(5))", 5)]
        [InlineData("log(8,2)", 3)]                   // log_2(8) = 3
        [InlineData("log(100,10)", 2)]                // log_10(100) = 2
        [InlineData("max(2, min(3,4), 1)", 3)]
        [InlineData("max(1+2, 3+4, 5+6)", 11)]
        [InlineData("sqrt(max(16,25,9))", 5)]
        [InlineData("sqrt(min(16,25,9))", 3)]
        [InlineData("sin(3.14159265358979323846/2)", 1)] // sin(pi/2)
        [InlineData("cos(0)", 1)]
        [InlineData("tan(0)", 0)]
        [InlineData("2+3*4-5/2+1", 12.5)]             // 2+12-2.5+1=12.5
        [InlineData("2+3*(4-1)", 11)]                 // 2+3*3=11
        [InlineData("((2+3)*4)-5", 15)]
        [InlineData("max(1, min(2,3), 0)", 2)]
        [InlineData("min(max(2,3), 4)", 3)]
        [InlineData("1+2+3+4+5+6+7+8+9+10", 55)]
        [InlineData("10-9-8-7-6-5-4-3-2-1", -35)]
        [InlineData("100/10/2", 5)]                   // (100/10)/2=10/2=5
        [InlineData("2^3*4", 32)]                     // (2^3)*4=8*4=32
        [InlineData("2^(3*4)", 4096)]                 // 2^12
        [InlineData("2^3+4", 12)]                     // (2^3)+4=8+4=12
        [InlineData("max(1,2,3,4,5,6,7,8,9,10,100)", 100)]
        [InlineData("min(-1,-2,-3,0,1,2)", -3)]
        [InlineData("sqrt(25*4)", 10)]
        [InlineData("exp(1)", 2.7182818285)]          // e^1
        [InlineData("ln(exp(5))", 5)]
        [InlineData("max(1,2,3,4,5,min(2,3,1))", 5)]
        [InlineData("max(1,2,3,4,5,max(2,3,10))", 10)]
        [InlineData("min(5,max(2,3,10),7)", 5)]
        [InlineData("sqrt(16)+max(1,2,3)", 7)]
        [InlineData("sqrt(16)*max(1,2,3)", 12)]
        [InlineData("sqrt(16)/max(1,2,4)", 1)]
        [InlineData("2^max(2,3,4)", 16)]
        [InlineData("max(1,2,3,min(5,6,7))", 5)]
        [InlineData("max(1,2,3,min(5,6,0))", 3)]
        [InlineData("max(sin(0), cos(0), tan(0))", 1)]
        [InlineData("max(1,-1,0)", 1)]
        [InlineData("min(-1,0,1)", -1)]
        public void Calculate_ComplexExpressions_ReturnsExpected(string expr, decimal expected)
        {
            var result = Calculator.Calculate(expr);
            Assert.Equal(expected, result, 5); // 5位小数精度
        }

        [Theory]
        [InlineData("sqrt(-4)")]
        [InlineData("log(-1)")]
        [InlineData("ln(-10)")]
        [InlineData("1/0")]
        [InlineData("max()")]
        [InlineData("min()")]
        [InlineData("sqrt()")]
        [InlineData("sin()")]
        [InlineData("log()")]
        [InlineData("2++2")]
        [InlineData(")1+2(")]
        [InlineData("max(1,2,3,")]
        [InlineData("sin(,)")]
        [InlineData("unknownFunc(1,2)")]
        public void Calculate_ComplexExpressions_ThrowsException(string expr)
        {
            Assert.ThrowsAny<Exception>(() => Calculator.Calculate(expr));
        }
    }
}