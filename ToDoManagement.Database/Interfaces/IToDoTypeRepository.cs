using ToDoManagement.Database.Models;

namespace ToDoManagement.Database.Interfaces;

public interface IToDoTypeRepository : IEntityBaseRepository<ToDoManagementContext, ToDoType>
{
}
