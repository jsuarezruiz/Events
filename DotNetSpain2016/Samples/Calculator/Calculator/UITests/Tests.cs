using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace Calculator.UITests
{
	[TestFixture (Platform.Android)]
	[TestFixture (Platform.iOS)]
	public class Tests
	{
		static readonly Func<AppQuery, AppQuery> NineButton = c => c.Marked("Digit9");
		static readonly Func<AppQuery, AppQuery> TwoButton = c => c.Marked("Digit2");
		static readonly Func<AppQuery, AppQuery> ZeroButton = c => c.Marked("Digit0");
		static readonly Func<AppQuery, AppQuery> PlusButton = c => c.Marked("Operator+");
		static readonly Func<AppQuery, AppQuery> SubstractButton = c => c.Marked("Operator-");
		static readonly Func<AppQuery, AppQuery> DivideButton = c => c.Marked("OperatorDivision");
		static readonly Func<AppQuery, AppQuery> EqualsButton = c => c.Marked("OperatorEquals");

		IApp app;
		Platform platform;

		public Tests (Platform platform)
		{
			this.platform = platform;
		}

		[SetUp]
		public void BeforeEachTest ()
		{
			app = AppInitializer.StartApp (platform);
		}

		[Test]
		public void TheHelloUITest()
		{
			app.Repl ();
		}
			
		[Test]
		public void TheTwoPlusTwoIsFourTest()
		{
			app.Tap(TwoButton);
			app.Tap(PlusButton);
			app.Tap(TwoButton);
			app.Tap(EqualsButton);
			app.Screenshot("When I get the result value");

			AppResult[] results = app.WaitForElement(c => c.Marked("DisplayValue").Text("4"));

			// Assert
			Assert.IsTrue(results.Any());
		}

		[Test]
		public void TheNineLessTwoIsSevenTest()
		{
			app.Tap(NineButton);
			app.Tap(SubstractButton);
			app.Tap(TwoButton);
			app.Tap(EqualsButton);
			app.Screenshot("When I get the result value");

			AppResult[] results = app.WaitForElement(c => c.Marked("DisplayValue").Text("7"));

			// Assert
			Assert.IsTrue(results.Any());
		}

		[Test]
		public void TheDivideByZeroTest()
		{
			app.Tap(TwoButton);
			app.Tap(DivideButton);
			app.Tap(ZeroButton);
			app.Screenshot("Divide by zero!");
			app.Tap(EqualsButton);
			app.Screenshot("When I get the result value");

			AppResult[] results = app.WaitForElement(c => c.Marked("DisplayValue").Text("0"));

			// Assert
			Assert.IsTrue(results.Any());
		}
	}
}