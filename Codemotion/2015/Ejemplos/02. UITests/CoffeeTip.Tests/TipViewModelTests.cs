using System;
using NUnit.Framework;
using CoffeeTip.ViewModel;

namespace CoffeeTip.Tests
{
    [TestFixture]
    public class TipViewModelTests
    {
        TipViewModel vm;
        [SetUp]
        public void Setup()
        {
            vm = new TipViewModel();
        }

        
        [TearDown]
        public void Tear()
        {
        }

        [Test]
        public void AtStarbucks()
        {
            vm.AtStarbucks = true;
            vm.SubTotal = 5.00M;
            Assert.AreEqual(vm.Total, vm.SubTotal);
            Assert.AreEqual(vm.TipAmount, 0M);
        }

        [Test]
        public void DripCoffee()
        {
            vm.AtStarbucks = true;
            vm.SubTotal = 5.00M;
            Assert.AreEqual(vm.Total, vm.SubTotal);
            Assert.AreEqual(vm.TipAmount, 0M);
        }

        [Test]
        public void PourOver()
        {
            vm.DrinkType = 1;
            vm.SubTotal = 5.00M;
            Assert.AreEqual(vm.Total, vm.SubTotal + vm.TipAmount);
            Assert.AreEqual(vm.TipAmount, 1.0M);
        }

        [Test]
        public void Espresso()
        {
            vm.DrinkType = 2;
            vm.SubTotal = 5.00M;
            Assert.AreEqual(vm.Total, vm.SubTotal);
            Assert.AreEqual(vm.TipAmount, 0M);
        }

        [Test]
        public void EspressoTampered()
        {
            vm.DrinkType = 2;
            vm.Tampered = true;
            vm.SubTotal = 5.00M;
            Assert.AreEqual(vm.Total, vm.SubTotal + vm.TipAmount);
            Assert.AreEqual(vm.TipAmount, 0.5M);
        }

        [Test]
        public void Latte()
        {
            vm.DrinkType = 3;
            vm.SubTotal = 5.00M;
            Assert.AreEqual(vm.Total, vm.SubTotal + vm.TipAmount);
            Assert.AreEqual(vm.TipAmount, 0.5M);
        }

        [Test]
        public void LatteTampered()
        {
            vm.DrinkType = 3;
            vm.Tampered = true;
            vm.SubTotal = 5.00M;
            Assert.AreEqual(vm.Total, vm.SubTotal + vm.TipAmount);
            Assert.AreEqual(vm.TipAmount, 1.0M);
        }

    }
}

