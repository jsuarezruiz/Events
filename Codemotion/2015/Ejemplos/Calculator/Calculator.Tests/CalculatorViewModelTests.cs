namespace Calculator.Tests
{
    using Models;
    using NUnit.Framework;
    using ViewModels;

    [TestFixture()]
    public class CalculatorViewModelTests
    {
        [Test()]
        public void TestAddtion()
        {
            var calculation = new CalculatorViewModel
            {
                OperandOne = 2,
                OperandTwo = 2,
                Operation = Operator.Addition
            };

            calculation.Calculate();
            
            Assert.AreEqual(calculation.DisplayValue, "4");
        }

        [Test()]
        public void TestMultiplication()
        {
            var calculation = new CalculatorViewModel
            {
                OperandOne = 4,
                OperandTwo = 4,
                Operation = Operator.Multiplication
            };

            calculation.Calculate();

            Assert.AreEqual(calculation.DisplayValue, "16");
        }
    
        [Test()]
        public void TestSubtraction()
        {
            var calculation = new CalculatorViewModel
            {
                OperandOne = 40,
                OperandTwo = 10,
                Operation = Operator.Subtraction
            };

            calculation.Calculate();

            Assert.AreEqual(calculation.DisplayValue, "30");
        }

        [Test()]
        public void TestDivision()
        {
            var calculation = new CalculatorViewModel
            {
                OperandOne = 100,
                OperandTwo = 5,
                Operation = Operator.Division
            };

            calculation.Calculate();

            Assert.AreEqual(calculation.DisplayValue, "20");
        }
    }
}
