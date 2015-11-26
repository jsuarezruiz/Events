using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace CoffeeTip.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void Test()
        {
            app.Tap("DrinkType");
            app.Tap("Espresso");
            app.DismissKeyboard();


           

            app.Tap("Tamered");
            app.ClearText("SubTotal");
            app.EnterText("SubTotal", "3.50");
            app.DismissKeyboard();

            var total = app.Query("Total").First();
            var tamered = app.Query("Tamered").First();

            Assert.IsTrue(tamered.Enabled);
            Assert.AreEqual("Total: $4.00", total.Text);
        }

        [Test]
        public void AtStarbucks()
        {
            var screen = new TipScreen(app);
            app.Screenshot("When I run the app");

            screen.EnterSubTotal(5.00M);
            screen.ToggleStarbucks();

            Assert.AreEqual("Total: $5.00", screen.TotalText);
            Assert.AreEqual("Tip: $0.00", screen.TipText);
        }
    }

    public enum CoffeeDrink
    {
        Drip,
        PourOver,
        Espresso,
        Latte
    }

    class TipScreen
    {
        readonly IApp app;

        public TipScreen(IApp app)
        {
            this.app = app;
        }

        public void ToggleStarbucks()
        {
            app.Tap("Starbucks");
        }

        public void ToggleTampered()
        {
            app.Tap("Tamered");
        }

        public void Reset()
        {
            app.Tap("Reset");
        }

        public void SelectDrink(int drink)
        {
            app.Tap("DrinkType");
            switch (drink)
            {
                case 0:
                    app.Tap("Drip Coffee");
                    break;
                case 1:
                    app.Tap("Pour Over Coffee");
                    break;
                case 2:
                    app.Tap("Espresso");
                    break;
                case 3:
                    app.Tap("Latte");
                    break;
            }
            app.DismissKeyboard();
        }

        public void EnterSubTotal(decimal subTotal)
        {
            app.ClearText("SubTotal");
            app.EnterText("SubTotal", subTotal.ToString());
            app.DismissKeyboard();
        }

        public string TipText
        {
            get { return app.Query("TipAmount").Single().Text; }
        }
        public string TotalText
        {
            get { return app.Query("Total").Single().Text; }
        }
    }
}

