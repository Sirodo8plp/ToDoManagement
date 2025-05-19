using Microsoft.AspNetCore.Mvc;
using ToDoManagement.Core.Interfaces;
using ToDoManagement.ViewModels;

namespace ToDoManagement.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ToDoTypeController(
    ILogger<ToDoTypeController> logger,
    IToDoTypeService service) : ControllerBase
{
    private readonly ILogger<ToDoTypeController> _logger = logger;
    private readonly IToDoTypeService _service = service;

    [HttpPost]
    public async Task<IActionResult> Post(ToDoTypeDto toDoType, CancellationToken cancellationToken)
    {
        await _service.CreateAsync(toDoType, cancellationToken);

        return Ok();
    }
}
