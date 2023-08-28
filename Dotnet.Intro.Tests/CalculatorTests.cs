namespace Dotnet.Intro.Tests
{
    public class CalculatorTests
    { 

        [Fact]
        public void AddMethod_Returns_Summary(){
            var calculator = new Calculator();
            var res = calculator.Add(2f, 2f);
            Assert.Equal(4, res);
        }

        [Fact]
        public void SubMethod_Returns_Subtraction() {  
            var calculator = new Calculator();
            var res = calculator.Sub(2f, 4f);
            Assert.Equal(-2, res);
        }

        [Fact]
        public void MulMethod_Returns_Multiplication()
        {
            var calculator = new Calculator();
            var res = calculator.Mul(2f, 2f);
            Assert.Equal(4, res);
        }

        [Fact]
        public void DivMethod_Returns_Division()
        {
            var calculator = new Calculator();
            var res = calculator.Div(2f, 2f);
            Assert.Equal(1, res);
        }

        [Fact]
        public void DivMethod_Throws_DivideByZeroExc()
        {
            var calculator = new Calculator();

            Assert.Throws<DivideByZeroException>(() => calculator.Div(2f, 0));
        }
    }
}