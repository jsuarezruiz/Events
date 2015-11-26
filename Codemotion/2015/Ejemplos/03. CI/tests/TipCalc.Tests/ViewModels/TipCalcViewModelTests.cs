using NUnit.Framework;
using TipCalc.ViewModels;

namespace TipCalc.Tests.ViewModels
{
    [TestFixture]
    public class TipCalcViewModelTests
    {
        [Test]
        public void TipPercent_Updated_TipAmountAndTotalAreUpdated()
        {
            var model = new TipCalcViewModel
            {
                SubTotal = 10,
                PostTaxTotal = 12,
                TipPercent = 20
            };


            Assert.AreEqual(14, model.Total);
            Assert.AreEqual(2, model.TipAmount);
        }
    }
}