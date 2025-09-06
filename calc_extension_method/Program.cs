using System;

delegate double delCal(double x, double y);

public static class Extensions
{
    public static double Calc(this double a, double b, string op)
    {
        return op switch
        {
            "+" => a + b,
            "-" => a - b,
            "*" => a * b,
            "/" => b != 0 ? a / b : throw new DivideByZeroException("Divide by zero not allowed"),
            _ => throw new InvalidOperationException("Unknown operation")
        };
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Choose operation (+, -, *, /): ");
        string op = Console.ReadLine();

        Console.Write("Enter the first number: ");
        double num1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Enter the second number: ");
        double num2 = Convert.ToDouble(Console.ReadLine());

        double result = num1.Calc(num2, op);

        Console.WriteLine($"Result = {result}");
    }
}
