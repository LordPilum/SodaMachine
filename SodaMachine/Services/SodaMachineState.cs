using SodaMachine.Extensions;
using SodaMachine.Models;

namespace SodaMachine.Services
{
    /// <summary>
    /// State object for the soda machine.
    /// </summary>
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

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="inventory">Inventory for the soda machine.</param>
        public SodaMachineState(List<Soda> inventory)
        {
            Inventory = inventory;
            Credit = 0;
        }

        /// <summary>
        /// Adds a given amount of money to the credit.
        /// </summary>
        /// <returns>The current credit total.</returns>
        internal ulong AddCredit(ulong amount)
        {
            Credit += amount;

            return Credit;
        }

        /// <summary>
        /// Returns all credit to the user.
        /// </summary>
        /// <returns>The current credit total.</returns>
        internal ulong ReturnCredit()
        {
            var amount = Credit;
            Credit = 0;

            return amount;
        }

        /// <summary>
        /// Lists the items in the inventory, by name.
        /// </summary>
        /// <remarks>
        /// The items are ordered alphabetically.
        /// </remarks>
        /// <returns>A list of items in the inventory.</returns>
        internal string ListInventory()
        {
            var inventoryNames = Inventory.Select(c => c.Name).ToList();
            inventoryNames.Sort();
            
            return string.Join(", ", inventoryNames);
        }

        /// <summary>
        /// Gets a soda item by its name.
        /// </summary>
        /// <param name="name">Name of the soda.</param>
        /// <returns>A soda item, if found. <see cref="Soda"/></returns>
        internal Soda? GetSodaType(string name)
        {
            return Inventory.SingleOrDefault(c => c.Name == name);
        }

        /// <summary>
        /// Purchase a soda.
        /// </summary>
        /// <param name="soda">The soda type to purchase. <see cref="Soda"/></param>
        /// <returns>Purchasing status. <see cref="PurchaseStatus"/></returns>
        internal PurchaseStatus Purchase(Soda soda)
        {
            if (soda.Nr == 0)
                return PurchaseStatus.OutOfStock;
            if (Credit < soda.Price)
                return PurchaseStatus.NotEnoughCredit;

            Credit -= soda.Price;
            soda.Nr--;

            Inventory = Inventory.Replace(soda);
            return PurchaseStatus.OK;
        }

        /// <summary>
        /// Purchase a soda via SMS.
        /// </summary>
        /// <param name="soda">The soda type to purchase. <see cref="Soda"/></param>
        /// <returns>Purchasing status. <see cref="PurchaseStatus"/></returns>
        internal PurchaseStatus SmsPurchase(Soda soda)
        {
            if (soda.Nr == 0)
                return PurchaseStatus.OutOfStock;

            soda.Nr--;

            Inventory = Inventory.Replace(soda);
            return PurchaseStatus.OK;
        }
    }
}
