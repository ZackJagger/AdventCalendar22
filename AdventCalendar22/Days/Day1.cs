using AdventCalendar22.Helpers;

namespace AdventCalendar22.Days;

internal class Day1
{
    internal static async Task<int> GetResult()
    {
        _ = await AdventHelper.GetFile(1);

        List<int> tempElf = new List<int>();
        List<int> totals = new List<int>();

        foreach (string line in File.ReadLines("Day1.txt"))
        {
            if (string.IsNullOrEmpty(line))
            {
                totals.Add(tempElf.Sum());
                tempElf = new List<int>();
            }
            else
            {
                tempElf.Add(int.Parse(line));
            }
        }

        totals.Sort();
        totals.Reverse();

        return totals[0] + totals[1] + totals[2];
    }
}
