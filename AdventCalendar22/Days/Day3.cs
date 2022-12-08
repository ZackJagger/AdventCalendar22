using AdventCalendar22.Helpers;

namespace AdventCalendar22.Days
{
    internal class Day3
    {
        internal static async Task<int> GetResult()
        {
            _ = await AdventHelper.GetFile(3);

            int total = 0;

            foreach (string line in File.ReadLines("Day3.txt"))
            {
                var compartmentOne = line.Substring(0, line.Length / 2);
                var compartmentTwo = line.Substring(line.Length / 2);

                foreach (char c in compartmentOne)
                {
                    if (compartmentTwo.Contains(c))
                    {
                        if (c.ToString() == c.ToString().ToUpper())
                            total += c - 38;
                        else
                            total += c - 96;

                        break;
                    }
                }
            }

            return total;
        }

        internal static async Task<int> GetResultPartTwo()
        {
            _ = await AdventHelper.GetFile(3);

            var lines = File.ReadLines("Day3.txt");
            int total = 0;

            for (int i = 0; i < lines.Count(); i += 3)
            {
                foreach (char c in lines.ElementAt(i))
                {
                    if (lines.ElementAt(i + 1).Contains(c) && lines.ElementAt(i + 2).Contains(c))
                    {
                        if (c.ToString() == c.ToString().ToUpper())
                            total += c - 38;
                        else
                            total += c - 96;

                        break;
                    }
                }
            }

            return total;
        }
    }
}
