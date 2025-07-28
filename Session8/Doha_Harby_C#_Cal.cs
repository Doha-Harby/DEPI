using System;
using static System.Console;

namespace SimpleCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Hello, and welcome to my simple calculator. \n =============================================");
            char ex = ' ';
            while (ex != 'E')
            {
                WriteLine("Please enter the first number:");
                double firstNumber = Convert.ToDouble(ReadLine());
                WriteLine("Please enter the second number:");
                double secondNumber = Convert.ToDouble(ReadLine());
                WriteLine("What do you want to do with those numbers? \n [A]dd \n [S]ubtract \n [M]ultiply ");
                string operation = (ReadLine());
                
                switch (operation.ToUpper())
                {
                    case "A":
                       WriteLine(firstNumber + secondNumber); 
                        break;
                    case "S" :
                        WriteLine(firstNumber - secondNumber);
                        break;
                    case "M" :
                        WriteLine(firstNumber * secondNumber);
                        break;
                }
                WriteLine(" press E to exit if you want to exit any other key to continue");
                ex = Convert.ToChar(ReadLine().ToUpper()) ;
            }
            
        }
    }
}
