using SodaMachine.Extensions;
using SodaMachine.Models;
using System.Collections.Generic;
using System.Linq;

namespace SodaMachine.Services
{
    public class SodaMachineState
    {
        /// <summary>
        /// The amount of credit in the machine.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Credits are increased by inserting money into the machine.
        /// </para>
        /// <para>
        /// All remaining credits will be returned upon successful completion of a purchase.
        /// </para>
        /// </remarks>
        internal ulong Credit { get; private set; }
        /// <summary>
        /// The current inventory of sodas in the machine.
        /// </summary>
        private List<Soda> Inventory { get; set; }

        public SodaMachineState(List<Soda> inventory)
        {
            Inventory = inventory;
            Credit = 0;
        }

        /// <summary>
        /// Adds a given amount of money to the credit.
        /// </summary>
        internal void AddCredit(ulong amount)
        {
            Credit += amount;
        }

        /// <summary>
        /// Returns all credit to the user.
        /// </summary>
        internal void ReturnCredit()
        {
            Credit = 0;
        }

        internal string ListInventory()
        {
            var inventoryNames = Inventory.Select(c => c.Name).ToList();
            inventoryNames.Sort();
            
            return string.Join(", ", inventoryNames);
        }

        internal Soda? GetSodaType(string name)
        {
            return Inventory.SingleOrDefault(c => c.Name == name);
        }

        internal bool Purchase(Soda soda)
        {
            if (soda.Nr == 0)
                return false;

            Credit -= soda.Price;
            soda.Nr--;

            Inventory = Inventory.Replace(soda);
            return true;
        }

        internal bool SmsPurchase(Soda soda)
        {
            if (soda.Nr == 0)
                return false;

            soda.Nr--;

            Inventory = Inventory.Replace(soda);
            return true;
        }
    }
}
