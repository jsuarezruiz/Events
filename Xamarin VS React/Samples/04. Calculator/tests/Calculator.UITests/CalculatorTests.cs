using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Calculator.UITests
{
    [TestFixture(Platform.Android)]
    public class CalculatorTests
    {
        static readonly Func<AppQuery, AppQuery> TwoButton = c => c.Marked("Digit2");
        static readonly Func<AppQuery, AppQuery> PlusButton = c => c.Marked("Operator+");
        static readonly Func<AppQuery, AppQuery> EqualsButton = c => c.Marked("OperatorEquals");
        
        private Platform _platform;
        private IApp _app;

        public CalculatorTests(Platform platform)
        {
            _platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            _app = AppInitializer.StartApp(_platform);
        }

        [Test()]
        public void FirstTest()
        {
            _app.Repl();
        }

        public void SevenPlusThreeIsTenTest()
        {
            _app.Tap(b => b.Marked("Digit7"));
            _app.Tap(b => b.Marked("Operator+"));
            _app.Tap(b => b.Marked("Digit3"));
            _app.Tap(b => b.Marked("OperatorEquals"));

            AppResult[] results = _app.WaitForElement(c => c.Marked("DisplayValue").Text("10"));

            Assert.IsTrue(results.Any());
        }


        [Test]
        public void TheTwoPlusTwoIsFourTest()
        {
            _app.WaitForElement(c => c.Marked("OperatorEquals"));

            _app.Tap(TwoButton);
            _app.Tap(PlusButton);
            _app.Tap(TwoButton);
            _app.Tap(EqualsButton);
            _app.Screenshot("When I get the result value");

            AppResult[] results = _app.WaitForElement(c => c.Marked("DisplayValue").Text("4"));

            Assert.IsTrue(results.Any());
        }

        [Test]
        public void NewTest()
        {
            _app.Tap(x => x.Marked("Digit7"));
            _app.Tap(x => x.Marked("Operator+"));
            _app.Tap(x => x.Marked("Digit3"));
            _app.Tap(x => x.Marked("OperatorEquals"));
            _app.Screenshot("Tapped on view with class: Button marked: OperatorEquals");
            _app.WaitForElement(x => x.Marked("DisplayValue"));
        }
    }
}

