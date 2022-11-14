using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodaMachine.Models;
using SodaMachine.Services;

namespace SodaMachine.Tests
{
    [TestClass]
    public class InventoryTests
    {
        [TestMethod]
        public void ShouldSellExactlyThreeSodas()
        {
            var inventory = new List<Soda>
            {
                new Soda
                {
                    Name = "coke",
                    Nr = 3,
                    Price = 20
                },
                new Soda
                {
                    Name = "fanta",
                    Nr = 8,
                    Price = 17
                }
            };

            var machine = new SodaMachineState(inventory);

            var coke = machine.GetSodaType("coke");

            var total = machine.AddCredit(15);
            total = machine.AddCredit(17);

            Assert.AreEqual(total, 32u);

            var status = machine.Purchase(coke);
            Assert.AreEqual(status, PurchaseStatus.OK);

            status = machine.Purchase(coke);
            Assert.AreEqual(status, PurchaseStatus.NotEnoughCredit);

            total = machine.AddCredit(20);
            status = machine.Purchase(coke);
            Assert.AreEqual(status, PurchaseStatus.OK);

            total = machine.AddCredit(20);
            status = machine.Purchase(coke);
            Assert.AreEqual(status, PurchaseStatus.OK);

            total = machine.AddCredit(20);
            status = machine.Purchase(coke);

            Assert.AreEqual(total, 32u);
            Assert.AreEqual(status, PurchaseStatus.OutOfStock);

            var remainder = machine.ReturnCredit();

            Assert.AreEqual(remainder, 32u);

            Assert.AreEqual(machine.Credit, 0u);
        }
    }
}
