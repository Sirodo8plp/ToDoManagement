using ToDoManagement.Database.Interfaces;
using ToDoManagement.Database.Models;

namespace ToDoManagement.Database.Repositories;

public class ToDoTypeRepository(ToDoManagementContext context)
    : EntityBaseRepository<ToDoManagementContext, ToDoType>(context), IToDoTypeRepository
{
}
