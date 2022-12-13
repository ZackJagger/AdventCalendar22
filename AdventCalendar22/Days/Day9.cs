using AdventCalendar22.Helpers;
using System.Collections.Specialized;

namespace AdventCalendar22.Days
{
    internal class Day9
    {
        private static (int x, int y) prevPosition = (0, 0);
        private static (int x, int y) headPosition = (0, 0);
        private static (int x, int y) tailPosition = (0, 0);
        private static List<(int x, int y)> tailPositionsVisited = new List<(int x, int y)>() { (0, 0)  };

        internal static async Task<int> GetResult()
        {
            _ = await AdventHelper.GetFile(9);
            var lines = File.ReadLines("Day9.txt");

            foreach (var line in lines)
            {
                var parsedLine = line.Split(" ");
                int distance = int.Parse(parsedLine[1]);

                switch (parsedLine[0])
                {
                    case "R":
                        for (int i = 0; i < distance; i++)
                            MoveHeadTail(1, 0);
                        break;
                    case "L":
                        for (int i = 0; i < distance; i++)
                            MoveHeadTail(-1, 0);
                        break;
                    case "U":
                        for (int i = 0; i < distance; i++)
                            MoveHeadTail(0, 1);
                        break;
                    case "D":
                        for (int i = 0; i < distance; i++)
                            MoveHeadTail(0, -1);
                        break;
                }
            }

            return tailPositionsVisited.Count();
        }

        internal static async Task<int> GetResultPartTwo()
        {
            _ = await AdventHelper.GetFile(9);
            int mostTrees = 0;

            return mostTrees;
        }

        private static void MoveHeadTail(int xDir, int yDir)
        {
            headPosition.x += xDir;
            headPosition.y += yDir;

            if (headPosition != tailPosition)
            {
                if (Math.Abs(headPosition.x - tailPosition.x) > 1 || Math.Abs(headPosition.y - tailPosition.y) > 1)
                {
                    tailPosition = prevPosition;

                    if (!tailPositionsVisited.Contains(tailPosition))
                        tailPositionsVisited.Add(tailPosition);

                }
            }

            prevPosition = headPosition;
            Console.WriteLine($"Head Position: {headPosition} - Tail Position: {tailPosition}");
        }
    }
}
