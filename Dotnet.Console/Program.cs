using Dotnet.Intro;

Console.WriteLine("Enter the expression <operand> <operator> <operand> (possible operators: +, /, -, *):");
string expresion = Console.ReadLine();
string[] expElements = expresion.Split(' ');

float x = float.Parse(expElements[0]);
float y = float.Parse(expElements[2]);

Calculator calculator = new Calculator();
switch (expElements[1])
{

    case "+":
        Console.WriteLine(calculator.Add(x,y));
        break;
    case "-":
        Console.WriteLine(calculator.Sub(x, y));
        break;
    case "*":
        Console.WriteLine(calculator.Mul(x, y));
        break; 
    case "/":
        Console.WriteLine(calculator.Div(x, y));
        break;
    default:
        throw new ArgumentException("Chosen operation is not supported!");
        break;
    }