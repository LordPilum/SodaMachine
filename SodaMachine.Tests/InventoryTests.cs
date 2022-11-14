using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodaMachine.Models;
using SodaMachine.Services;
using System.Collections.Generic;

namespace SodaMachine.Tests
{
    [TestClass]
    public class InventoryTests
    {
        [TestMethod]
        public void ShouldSellThreeSodas()
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

            machine.AddCredit(20);
            machine.Purchase(coke);

            machine.AddCredit(20);
            machine.Purchase(coke);

            machine.AddCredit(20);
            machine.Purchase(coke);

            machine.AddCredit(20);
            machine.Purchase(coke);

            Assert.AreEqual(machine.Credit, 0);
            Assert.AreEqual(machine.Credit, 0);
        }
    }
}
