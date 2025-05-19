using ToDoManagement.Cache;
using ToDoManagement.Common;
using ToDoManagement.Core;
using ToDoManagement.Database;

var builder = WebApplication.CreateBuilder(args);

var redisOptions = new RedisOptions();
builder.Configuration.GetSection(nameof(RedisOptions)).Bind(redisOptions);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddToDoManagementCache(redisOptions.ConnectionString);
builder.Services.AddToDoManagementServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
