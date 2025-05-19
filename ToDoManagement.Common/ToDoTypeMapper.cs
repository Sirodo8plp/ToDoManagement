using ToDoManagement.Database.Models;
using ToDoManagement.ViewModels;

namespace ToDoManagement.Common;

public static class ToDoTypeMapper
{
    public static ToDoTypeDto Map(this ToDoType source)
    {
        return new ToDoTypeDto()
        {
            Name = source.Name,
            Description = source.Description,
        };
    }

    public static ToDoType Map(this ToDoTypeDto source)
    {
        return new ToDoType(source.Name, source.Description);
    }
}