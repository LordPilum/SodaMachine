using System;
using System.Collections.Generic;
using System.Linq;

namespace SodaMachine.Extensions
{
    public static partial class Extensions
    {
        /// <summary>
        /// Replaces an existing item in the list with a new object.
        /// </summary>
        /// <remarks>
        /// The list is returned unmodified if the item does not exist in the list.
        /// <remarks>
        /// <param name="item" cref="T">The replacement item.</param>
        /// <exception cref="Exception">Failure to remove an existing item causes an exception to be thrown.</exception>
        /// <returns>An updated list.</returns>
        public static List<T> Replace<T>(this List<T> list, T item)
        {
            if(!list.Contains(item))
                return list;

            bool success = list.Remove(item);

            if(!success)
                throw new Exception("Failed to remove existing item");

            list.Add(item);

            return list;
        }
    }
}