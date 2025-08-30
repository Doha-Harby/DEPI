using System;

delegate double delCal(double x, double y);

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

       
        delCal operation = op switch
        {
            "+" => Add,
            "-" => Sub,
            "*" => Mul,
            "/" => Div,
            _ => throw new InvalidOperationException("Unknown operation")
        };

        Console.WriteLine($"Result = {operation(num1, num2)}");
    }

    static double Add(double a, double b) => a + b;
    static double Sub(double a, double b) => a - b;
    static double Mul(double a, double b) => a * b;
    static double Div(double a, double b) => b != 0 ? a / b
            : throw new DivideByZeroException("Divide by zero not allowed");
}
