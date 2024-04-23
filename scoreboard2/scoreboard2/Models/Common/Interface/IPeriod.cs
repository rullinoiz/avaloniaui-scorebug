namespace scoreboard2.Models.Common.Interface;

public interface IPeriod : ICommitable
{
    public string Name { get; }
    public int Value { get; set; }
}