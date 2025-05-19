using ToDoManagement.ViewModels;

namespace ToDoManagement.Core.Interfaces;

public interface IToDoTypeService
{
    public Task<bool> CreateAsync(ToDoTypeDto toDoType, CancellationToken cancellationToken);
}
