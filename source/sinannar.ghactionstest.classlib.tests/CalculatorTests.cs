namespace sinannar.ghactionstest.classlib.tests;

public class CalculatorTests
{
    private readonly ICalculator cal = new Calculator();

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 2, 3)]
        [InlineData(-1, 2, 1)]
        [InlineData(-5, -7, -12)]
        public void Add_returns_expected(int a, int b, int expected)
        {
            var result = cal.total(a, b);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(5, 3, 2)]
        [InlineData(3, 5, -2)]
        [InlineData(-5, -7, 2)]
        [InlineData(-5, 7, -12)]
        public void Subtract_returns_expected(int a, int b, int expected)
        {
            var result = cal.subtract(a, b);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0, 5, 0)]
        [InlineData(4, 6, 24)]
        [InlineData(-3, 7, -21)]
        [InlineData(-3, -7, 21)]
        public void Multiply_returns_expected(int a, int b, int expected)
        {
            var result = cal.multiply(a, b);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(10, 2, 5)]
        [InlineData(9, 3, 3)]
        [InlineData(-12, 3, -4)]
        [InlineData(-12, -3, 4)]
        public void Divide_returns_expected(int a, int b, int expected)
        {
            var result = cal.divide(a, b);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Divide_by_zero_throws()
        {
            Assert.Throws<DivideByZeroException>(() => cal.divide(1, 0));
        }
}
