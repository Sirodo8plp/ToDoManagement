using ToDoManagement.Cache;
using ToDoManagement.Common;
using ToDoManagement.Core.Interfaces;
using ToDoManagement.Database.Interfaces;
using ToDoManagement.ViewModels;

namespace ToDoManagement.Core.Services;

public class ToDoTypeService(
    IToDoTypeRepository repository,
    IToDoCacheService cacheService) : IToDoTypeService
{
    private readonly IToDoTypeRepository _repository = repository;
    private readonly IToDoCacheService _cacheService = cacheService;

    public async Task<bool> CreateAsync(ToDoTypeDto toDoType, CancellationToken cancellationToken)
    {
        var toDoTypeToInsert = toDoType.Map();

        await _repository.AddAsync(toDoTypeToInsert, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        await _cacheService.SetKey(toDoType.Name, toDoType);

        return true;
    }
}
