using AdventCalendar22.Helpers;

namespace AdventCalendar22.Days
{
    internal class Day8
    {
        private static IEnumerable<string> lines;
        private static int width;
        private static int height;
        private static int[,] forest;

        internal static async Task<int> GetResult()
        {
            await GetForest();
            int visible = 0;

            for (int col = 0; col < lines.ElementAt(0).Length; col++)
            {
                for (int row = 0; row < lines.Count(); row++)
                {
                    forest[col, row] = int.Parse(lines.ElementAt(col)[row].ToString());
                }
            }

            for (int col = 0; col < lines.ElementAt(0).Length; col++)
            {
                for (int row = 0; row < lines.Count(); row++)
                {
                    if (CheckDirection(row, col, 1, 0) ||
                        CheckDirection(row, col, -1, 0) ||
                        CheckDirection(row, col, 0, 1) ||
                        CheckDirection(row, col, 0, -1))
                    {
                        if (!(col == 0 || row == 0 || col == 4 || row == 4))
                            Console.WriteLine($"Tree at row {row + 1}, col {col + 1} is visible");
                        visible++;
                    }
                }
            }

            return visible;
        }

        internal static async Task<int> GetResultPartTwo()
        {
            await GetForest();
            int mostTrees = 0;

            for (int col = 0; col < lines.ElementAt(0).Length; col++)
            {
                for (int row = 0; row < lines.Count(); row++)
                {
                    var right = GetViewDistance(row, col, 1, 0);
                    var left = GetViewDistance(row, col, -1, 0);
                    var down = GetViewDistance(row, col, 0, 1);
                    var up = GetViewDistance(row, col, 0, -1);

                    int trees = right * left * down * up;

                    if (trees > mostTrees)
                        mostTrees = trees;
                }
            }

            return mostTrees;
        }

        private async static Task GetForest()
        {
            _ = await AdventHelper.GetFile(8);
            lines = File.ReadLines("Day8.txt");

            width = lines.ElementAt(0).Length;
            height = lines.Count();

            forest = new int[width, height];

            for (int col = 0; col < lines.ElementAt(0).Length; col++)
            {
                for (int row = 0; row < lines.Count(); row++)
                {
                    forest[col, row] = int.Parse(lines.ElementAt(col)[row].ToString());
                }
            }
        }

        private static bool CheckDirection(int row, int col, int xDir, int yDir)
        {
            //Console.WriteLine($"Row: {row}, Col: {col}, xDir: {xDir}, yDir: {yDir}");

            // If moving in x direction
            if (xDir != 0)
            {
                // If out of bounds, then it is visible.
                if (col + xDir >= width ||
                    col + xDir < 0)
                {
                    return true;
                }
                // If new value is less or equal to current value
                else if (forest[col + xDir, row] < forest[col, row])
                {
                    if (xDir < 0) xDir--;
                    else xDir++;

                    return CheckDirection(row, col, xDir, yDir);
                }
                // Otherwise is not visible
                else
                {
                    return false;
                }
            }

            if (yDir != 0)
            {
                // If out of bounds, then it is visible.
                if (row + yDir >= height ||
                    row + yDir < 0)
                {
                    return true;
                }
                // If new value is less or equal to current value
                else if (forest[col, row + yDir] < forest[col, row])
                {
                    if (yDir < 0) yDir--;
                    else yDir++;

                    return CheckDirection(row, col, xDir, yDir);
                }
                else
                {
                    return false;
                }
            }

            return false;
        }

        private static int GetViewDistance(int row, int col, int xDir, int yDir)
        {
            //Console.WriteLine($"Row: {row}, Col: {col}, xDir: {xDir}, yDir: {yDir}");

            // If moving in x direction
            if (xDir != 0)
            {
                // If out of bounds, then it is visible.
                if (col + xDir >= width ||
                    col + xDir < 0)
                {
                    return Math.Abs(xDir) - 1;
                }
                // If new value is less or equal to current value
                else if (forest[col + xDir, row] < forest[col, row])
                {
                    if (xDir < 0) xDir--;
                    else xDir++;

                    return GetViewDistance(row, col, xDir, yDir);
                }
                // Otherwise is not visible
                else
                {
                    return Math.Abs(xDir);
                }
            }

            if (yDir != 0)
            {
                // If out of bounds, then it is visible.
                if (row + yDir >= height ||
                    row + yDir < 0)
                {
                    return Math.Abs(yDir) - 1;
                }
                // If new value is less or equal to current value
                else if (forest[col, row + yDir] < forest[col, row])
                {
                    if (yDir < 0) yDir--;
                    else yDir++;

                    return GetViewDistance(row, col, xDir, yDir);
                }
                else
                {
                    return Math.Abs(yDir);
                }
            }

            return 0;
        }
    }
}
