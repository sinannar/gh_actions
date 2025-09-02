namespace sinannar.ghactionstest.classlib;

public class Calculator : ICalculator
{
    public int divide(int a, int b)
    {
        return a / b;
    }

    public int multiply(int a, int b)
    {
        return a * b;
    }

    public int subtract(int a, int b)
    {
        return a - b;
    }

    public int total(int a, int b)
    {
        return a + b;
    }
}
