using AdventCalendar22.Data.Enum;

namespace AdventCalendar22.Data;
public class Monkey
{
    public int Id { get; set; }
    public long ItemsInspected { get; set; }
    public List<long> CurrentItems { get; set; } = new List<long>();
    public Operation Operation { get; set; }
    public int Multiplier { get; set; }
    public int ConditionalDivider { get; set; }
    public int TrueMonkey { get; set; }
    public int FalseMonkey { get; set; }
}
