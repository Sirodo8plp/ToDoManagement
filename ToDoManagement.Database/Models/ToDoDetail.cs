namespace ToDoManagement.Database.Models;

public class ToDoDetail : BaseModel
{
    public string Description { get; set; } = string.Empty;
    public string Caption { get; set; } = string.Empty;
}
