namespace ToDoManagement.Database.Models;

public class ToDoType(string name, string description) : BaseModel
{
    public string Name { get; set; } = name;
    public string Description { get; set; } = description;
}
