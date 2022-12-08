using AdventCalendar22.Data;
using System.Text.RegularExpressions;

namespace AdventCalendar22.Days
{
    internal class Day7
    {
        private static Dictionary<string, int> directorySizes = new Dictionary<string, int>();

        internal static async Task<int> GetResult()
        {
            //_ = await AdventHelper.GetFile(7);
            var lines = File.ReadLines("Day7.txt");

            DirInfo currentDirectory = new DirInfo();
            List<DirInfo> directories = new List<DirInfo>();

            foreach (var line in lines)
            {
                if (line.StartsWith("$ cd"))
                {
                    // Get directory value
                    var cdValue = line.Substring(5);

                    if (cdValue == "..")
                    {
                        // Get parent of current directory
                        var parentDirs = directories.Where(x => x.Name == currentDirectory.Parent.Name && x.Size == currentDirectory.Parent.Size && x.Parent.Name == currentDirectory.Parent.Parent.Name);
                        var count = parentDirs.Count();
                        currentDirectory = parentDirs.First();
                    }
                    else
                    {
                        // Set parent to current dir
                        var parentDir = currentDirectory;

                        // Update to new dir
                        currentDirectory = new DirInfo()
                        {
                            Name = cdValue,
                            Parent = parentDir,
                            Size = 0
                        };

                        directories.Add(currentDirectory);

                    }
                }
                else if (Regex.IsMatch(line, @"^\d"))
                {
                    string[] values = line.Split(" ");
                    int size = int.Parse(values[0]);
                    currentDirectory.Size += size;
                }
            }

            int totalSpace = 70_000_000;
            int requiredSpace = 30_000_000;
            int totalSize = CalculateSizes(directories, directories[0]);
            int remainingSpace = totalSpace - totalSize;
            int spaceDiff = requiredSpace - remainingSpace;

            DirInfo smallest = new DirInfo() { Size = 100_000_000 };

            foreach (var dir in directories)
            {
                if (dir.Size > spaceDiff && dir.Size < smallest.Size)
                    smallest = dir;
            }

            return smallest.Size;
        }

        private static int CalculateSizes(List<DirInfo> directories, DirInfo directory)
        {
            var childrenDirs = directories.Where(x => x.Parent.Name == directory.Name && x.Parent.Size == directory.Size);
            int total = 0;

            if (childrenDirs.Count() > 0) {
                foreach (DirInfo dir in childrenDirs)
                {
                    directory.Size += CalculateSizes(directories, dir);
                }

                return directory.Size;
            }
            else
            {
                return directory.Size;
            }
        }
    }
}
