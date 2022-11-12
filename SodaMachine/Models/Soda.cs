using System;

namespace SodaMachine.Models
{
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


        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            
            Soda soda = obj as Soda;
            
            if (soda == null) return false;
            
            return Equals(soda);
        }

        public bool Equals(Soda? obj)
        {
            if (obj == null) return false;
            
            return Name.Equals(obj.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}
