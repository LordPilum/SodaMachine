using System;
using System.Threading.Tasks;

namespace SodaMachine.Services
{
    internal static class SodaMachineInterface
    {
        /// <summary>
        /// This is the starter method for the service.
        /// </summary>
        internal static async Task Run(SodaMachineState machine)
        {
            bool run;

            Console.Clear();
            do
            {
                var inventory = machine.ListInventory();

                Console.WriteLine("\n");
                Console.WriteLine("Available commands:");
                Console.WriteLine("insert (money) - Money put into money slot");
                Console.WriteLine("order ({0}) - Order from machines buttons", inventory);
                Console.WriteLine("sms order ({0}) - Order sent by sms", inventory);
                Console.WriteLine("recall - gives money back");
                Console.WriteLine("quit - turn off the soda machine");
                Console.WriteLine("-------");
                Console.WriteLine("Inserted money: {0}", machine.Credit);
                Console.WriteLine("-------\n\n");

                var input = Console.ReadLine();

                (run, machine) = HandleInput(machine, input);
            } while(run);
        }

        private static (bool, SodaMachineState) HandleInput(SodaMachineState machine, string? input)
        {
            if(string.IsNullOrEmpty(input))
                return (true, machine);

            if (input.StartsWith("insert"))
                return HandleInsert(machine, input);

            if (input.StartsWith("order"))
                return HandleOrder(machine, input);

            if (input.StartsWith("sms order"))
                return HandleSmsOrder(machine, input);

            if (input.Equals("recall"))
                return HandleRecall(machine, input);

            if (input.Equals("quit"))
                return Quit(machine);
            
            return (true, machine);
        }

        private static (bool, SodaMachineState) HandleInsert(SodaMachineState machine, string input)
        {
            // Add to credit.
            ulong.TryParse(input.Split(' ')[1], out var amount);

            Console.WriteLine("Adding {0} to credit.", amount);

            machine.AddCredit(amount);

            return (true, machine);
        }

        private static (bool, SodaMachineState) HandleOrder(SodaMachineState machine, string input)
        {
            // Split string on space to get soda name.
            var sodaName = input.Split(' ')[1];
            var soda = machine.GetSodaType(sodaName);

            if(soda == null)
            {
                Console.WriteLine("{0} not in stock.", sodaName);
                return (true, machine);
            }

            var status = machine.Purchase(soda);

            if(status == Models.PurchaseStatus.NotEnoughCredit)
            {
                var amount = soda.Price - machine.Credit;
                Console.WriteLine("Need {0} more credits.", amount);
                return (true, machine);
            }

            if(status == Models.PurchaseStatus.OutOfStock)
            {
                Console.WriteLine("No {0} left.", sodaName);
                return (true, machine);
            }

            var returned = machine.ReturnCredit();
            Console.WriteLine("Giving {0} out in change.", returned);

            return (true, machine);
        }

        private static (bool, SodaMachineState) HandleSmsOrder(SodaMachineState machine, string input)
        {
            var sodaName = input.Split(' ')[2];
            var soda = machine.GetSodaType(sodaName);

            if(soda == null)
            {
                Console.WriteLine("{0} not in stock.", sodaName);
                return (true, machine);
            }
            
            var status = machine.SmsPurchase(soda);

            if (status == Models.PurchaseStatus.NotEnoughCredit)
            {
                var amount = soda.Price - machine.Credit;
                Console.WriteLine("Need {0} more credits.", amount);
                return (true, machine);
            }

            return (true, machine);
        }

        private static (bool, SodaMachineState) HandleRecall(SodaMachineState machine, string input)
        {
            // Give money back.
            var amount = machine.ReturnCredit();
            Console.WriteLine("Returning {0} to customer.", amount);

            return (true, machine);
        }

        private static (bool, SodaMachineState) Quit(SodaMachineState machine)
        {
            Environment.Exit(0);
            return (false, machine);
        }
    }
}
