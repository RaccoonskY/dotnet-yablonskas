namespace Dotnet.Intro;

public class Calculator
{
    public float Add(float x, float y) => x + y;

    public float Sub(float x, float y) => x - y;

    public float Mul(float x, float y) => x * y;

    public float Div(float x, float y) => y == 0 ? throw new DivideByZeroException() : x / y; 
}