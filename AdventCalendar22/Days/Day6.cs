using AdventCalendar22.Helpers;

namespace AdventCalendar22.Days
{
    internal class Day6
    {
        internal static async Task<int> GetResult(int markerLength)
        {
            string input = await AdventHelper.GetFile(6);

            for (int i = 0; i < input.Length; i++)
            {
                List<char> charList = new List<char>();
                bool repeat = false;

                for (int j = 0; j < markerLength; j++)
                {
                    if (charList.Contains(input[j + i]))
                    {
                        repeat = true;
                        break;
                    }
                    charList.Add(input[i + j]);
                }

                if (repeat)
                    continue;
                
                return i + markerLength;
            }

            return 0;
        }
    }
}
