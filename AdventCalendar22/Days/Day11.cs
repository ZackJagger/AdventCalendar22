using AdventCalendar22.Data;
using AdventCalendar22.Data.Enum;
using AdventCalendar22.Helpers;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;

namespace AdventCalendar22.Days
{
    internal class Day11
    {
        internal static async Task<long> GetResult()
        {
            _ = await AdventHelper.GetFile(11);
            var lines = File.ReadLines("Day11.txt");

            List<Monkey> monkeys = new List<Monkey>();

            for (int i = 0; i < lines.Count(); i += 7)
            {
                // Init Monkey
                Monkey monkey = new Monkey() { Id = int.Parse(lines.ElementAt(i).Substring(7, 1)) };

                // Get Items
                string items = lines.ElementAt(i + 1).Substring(18);
                string[] itemsParsed = items.Split(", ");

                foreach (var item in itemsParsed)
                    monkey.CurrentItems.Add(int.Parse(item));

                // Get Operation
                string operation = lines.ElementAt(i + 2);

                if (operation[23] == '*')
                    monkey.Operation = Operation.Multiply;
                else 
                    monkey.Operation = Operation.Add;

                // Get Operation Value
                var multiplier = operation.Substring(25);
                if (int.TryParse(multiplier, out int value))
                    monkey.Multiplier = value;
                else
                    monkey.Operation = Operation.Squared;

                // Conditional Monkeys
                monkey.ConditionalDivider = int.Parse(lines.ElementAt(i + 3).Substring(21));
                monkey.TrueMonkey = int.Parse(lines.ElementAt(i + 4).Substring(29));
                monkey.FalseMonkey = int.Parse(lines.ElementAt(i + 5).Substring(30));

                monkeys.Add(monkey);
            }

            for (int round = 0; round < 10000; round++)
            {
                Console.WriteLine($"Round: {round}");

                foreach (var monkey in monkeys)
                {
                    foreach (var item in monkey.CurrentItems)
                    {
                        var newWorry = CalculateResult(monkey.Operation, item, monkey.Multiplier);
                        newWorry = newWorry % 9699690;

                        if (newWorry % monkey.ConditionalDivider == 0)
                            monkeys.Where(m => m.Id == monkey.TrueMonkey).First().CurrentItems.Add(newWorry);
                        else
                            monkeys.Where(m => m.Id == monkey.FalseMonkey).First().CurrentItems.Add(newWorry);

                        monkey.ItemsInspected++;
                    }

                    monkey.CurrentItems.Clear();
                    Console.WriteLine($"Monkey {monkey.Id}: {monkey.ItemsInspected}");
                }

                Console.WriteLine("\n");
            }

            var orderedMonkeys = monkeys.OrderByDescending(m => m.ItemsInspected).ToList();
            return orderedMonkeys[0].ItemsInspected * orderedMonkeys[1].ItemsInspected;
        }

        internal static async Task<int> GetResultPartTwo()
        {
            _ = await AdventHelper.GetFile(9);
            int mostTrees = 0;

            return mostTrees;
        }

        private static long CalculateResult(Operation operation, long value, int multiplier)
        {
            switch (operation)
            {
                case Operation.Multiply:
                    return value * multiplier;
                case Operation.Add:
                    return value + multiplier;
                case Operation.Squared:
                    return value * value;
                default:
                    throw new Exception("Not working mate");
            }
        }
    }
}
