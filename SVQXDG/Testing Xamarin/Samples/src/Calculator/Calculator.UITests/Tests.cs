using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Calculator.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        static readonly Func<AppQuery, AppQuery> TwoButton = c => c.Marked("Digit2");
        static readonly Func<AppQuery, AppQuery> PlusButton = c => c.Marked("OperatorPlus+");
        static readonly Func<AppQuery, AppQuery> EqualsButton = c => c.Marked("OperatorEquals");

        private IApp _app;
        private Platform _platform;

        public Tests(Platform platform)
        {
            _platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            _app = AppInitializer.StartApp(_platform);
        }

        [Test]
        public void AppLaunches()
        {
            _app.Screenshot("First screen.");
        }

        [Test]
        public void Repl()
        {
            _app.Repl();
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
            _app.Tap(x => x.Marked("OperatorX"));
            _app.Tap(x => x.Marked("Digit9"));
            _app.Tap(x => x.Marked("OperatorEquals"));
            _app.Tap(x => x.Marked("DisplayValue"));
            _app.Screenshot("Tapped on view with class: FormsTextView marked: DisplayValue");
            _app.WaitForElement(x => x.Marked("DisplayValue"));
        }
    }
}

