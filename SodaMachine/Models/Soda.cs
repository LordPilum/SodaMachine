using System;

namespace SodaMachine.Models
{
    /// <summary>
    /// This object holds information of a given type of soda,
    /// including its price and how many are in stock.
    /// </summary>
    public class Soda : IEquatable<Soda>
    {
        /// <summary>
        /// Name of the soda.
        /// </summary>
        public string Name { get; init; }
        /// <summary>
        /// Number of sodas in stock of this type.
        /// </summary>
        public uint Nr { get; set; }
        /// <summary>
        /// Price of this soda.
        /// </summary>
        public ulong Price { get; init; }


        /// <summary>
        /// Implements equality operators so the object
        /// can be compared to an existing one.
        /// </summary>
        /// <param name="obj">Object for comparison.</param>
        /// <returns>
        /// Whether the objects are equal, i.e. they have the same name.
        /// </returns>
        public bool Equals(Soda? obj)
        {
            if (obj == null) return false;
            
            return Name.Equals(obj.Name);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            Soda soda = obj as Soda;

            if (soda == null) return false;

            return Equals(soda);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
