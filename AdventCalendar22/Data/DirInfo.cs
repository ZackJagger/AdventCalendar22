namespace AdventCalendar22.Data;
internal class DirInfo
{
    public int Size { get; set; }
    public string Name { get; set; } = string.Empty;
    public DirInfo Parent { get; set; }
}
