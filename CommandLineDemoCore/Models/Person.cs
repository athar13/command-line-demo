namespace CommandLineDemoCore.Models;

public class Person
{
    public string Id { get; set; }
    public Name Name { get; set; }
    public Gender Gender { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public int Age => DateTime.Now.Year - DateOfBirth.Year;
}
