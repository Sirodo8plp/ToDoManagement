namespace ToDoManagement.Database.Models;

public class ToDoItem : BaseModel
{
    public string Caption { get; set; } = string.Empty;

    public DateTime? StartsAt { get; set; }
    public DateTime? EndsAt { get; set; }

    public ICollection<ToDoDetail>? Details { get; set; }
}
