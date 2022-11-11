using SodaMachine.Models;
using SodaMachine.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SodaMachine
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            SodaMachine sodaMachine = new SodaMachine();
            sodaMachine.Start();
        }
    }
}
