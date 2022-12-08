using AdventCalendar22.Data;
using Newtonsoft.Json;

namespace AdventCalendar22.Helpers;

internal static class AdventHelper
{
    internal static string session = File.ReadAllText("session.txt");

    internal static async Task<string> GetInput(int day)
    {
        using HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Cookie", $"session={session}");
        var result = await httpClient.GetStringAsync($"https://adventofcode.com/2022/day/{day}/input");
        return result;
    }

    internal static async Task<string> GetFile(int day)
    {
        var input = await GetInput(day);
        await File.WriteAllTextAsync($"Day{day}.txt", input);
        return input;
    }

    internal static async Task SubmitResult(int day, int result)
    {
        using HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Cookie", $"session={session}");

        var stringContent = new StringContent(JsonConvert.SerializeObject(new Answer(day.ToString(), result.ToString())));
        var response = await httpClient.PostAsync($"https://adventofcode.com/2022/day/{day}/answer", stringContent);
        var responseContent = await response.Content.ReadAsStringAsync();

        if (responseContent.Contains("That's not the right answer"))
        {
            Console.WriteLine("Incorrect");
        }
        else
        {
            Console.WriteLine("Correct!");
        }
    }
}
