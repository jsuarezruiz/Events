using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace TipCalc.UITests
{
	[TestFixture (Platform.Android)]
	[TestFixture (Platform.iOS)]
	public class Tests
	{
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
		public void Basic()
		{
			app.Repl();
		}

		[Test]
		public void CalculateTip()
		{
			var subTotal = 10M;
			var postTaxTotal = 12M;

			app.EnterText(e => e.Marked("SubTotal"), subTotal.ToString());
			app.Screenshot("When I enter a subtotal");

			app.EnterText(e => e.Marked("PostTaxTotal"), postTaxTotal.ToString());
			app.Screenshot("And I enter the post-tax total");

			var tipPercent = decimal.Parse(app.Query(e => e.Marked("TipPercent")).Single().Text) / 100;
			var tipAmount = decimal.Parse(app.Query(e => e.Marked("TipAmount")).Single().Text.Substring(1).Replace(".", ","));
			var total = decimal.Parse(app.Query(e => e.Marked("Total")).Single().Text.Substring(1).Replace(".", ","));

			var expectedTipAmount = subTotal * tipPercent;
			Assert.AreEqual(expectedTipAmount, tipAmount);

			var expectedTotal = postTaxTotal + expectedTipAmount;
			Assert.AreEqual(expectedTotal, total);

			app.Screenshot("Then the tip and total are calculated correctly");
		}
	}
}

