using AdventCalendar22.Helpers;

namespace AdventCalendar22.Days
{
    internal class Day5
    {
        internal static async Task<string> GetResult()
        {
            _ = await AdventHelper.GetFile(5);

            string result = "";
            Dictionary<int, List<string>> crates = new Dictionary<int, List<string>>();
            int columnNumberLineIndex = 0;
            int columnCount = 0;

            var lines = File.ReadLines("Day5.txt").ToList();

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && line[1] == '1')
                {
                    var columns = line.Split("   ");

                    foreach(var column in columns)
                        crates.Add(Int32.Parse(column.Replace(" ", "")), new List<string>());

                    columnCount = columns.Length;
                    columnNumberLineIndex = lines.ToList().IndexOf(line);
                    break;
                }
            }

            for (int lineNum = columnNumberLineIndex - 1; lineNum >= 0; lineNum--)
            {
                for (int crateCol = 0; crateCol < columnCount; crateCol++)
                {
                    var crateVal = lines[lineNum][(crateCol * 4) + 1].ToString();
                    if (!string.IsNullOrWhiteSpace(crateVal))
                        crates[crateCol + 1].Add(crateVal);
                }
            }

            for (int lineNum = columnNumberLineIndex + 2; lineNum < lines.Count; lineNum++)
            {
                var words = lines[lineNum].Split(" ");
                int movement = int.Parse(words[1]);
                int from = int.Parse(words[3]);
                int to = int.Parse(words[5]);

                for (int popCount = 0; popCount < movement; popCount++)
                {
                    crates[to].Add(crates[from].Last());
                    crates[from].RemoveAt(crates[from].Count() - 1);
                }
            }

            foreach (var crate in crates)
            {
                result += crate.Value[crate.Value.Count - 1];
            }

            return result;
        }

        internal static async Task<string> GetResultPartTwo()
        {
            _ = await AdventHelper.GetFile(5);

            string result = "";
            Dictionary<int, List<string>> crates = new Dictionary<int, List<string>>();
            int columnNumberLineIndex = 0;
            int columnCount = 0;

            var lines = File.ReadLines("Day5.txt").ToList();

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line) && line[1] == '1')
                {
                    var columns = line.Split("   ");

                    foreach (var column in columns)
                        crates.Add(Int32.Parse(column.Replace(" ", "")), new List<string>());

                    columnCount = columns.Length;
                    columnNumberLineIndex = lines.ToList().IndexOf(line);
                    break;
                }
            }

            for (int lineNum = columnNumberLineIndex - 1; lineNum >= 0; lineNum--)
            {
                for (int crateCol = 0; crateCol < columnCount; crateCol++)
                {
                    var crateVal = lines[lineNum][(crateCol * 4) + 1].ToString();
                    if (!string.IsNullOrWhiteSpace(crateVal))
                        crates[crateCol + 1].Add(crateVal);
                }
            }

            for (int lineNum = columnNumberLineIndex + 2; lineNum < lines.Count; lineNum++)
            {
                var words = lines[lineNum].Split(" ");
                int movement = int.Parse(words[1]);
                int from = int.Parse(words[3]);
                int to = int.Parse(words[5]);

                List<string> newCrates = new List<string>();

                for (int popCount = 0; popCount < movement; popCount++)
                {
                    newCrates.Add(crates[from].Last());
                    crates[from].RemoveAt(crates[from].Count() - 1);
                }

                newCrates.Reverse();
                crates[to].AddRange(newCrates);
            }

            foreach (var crate in crates)
            {
                result += crate.Value[crate.Value.Count - 1];
            }

            return result;
        }
    }
}
