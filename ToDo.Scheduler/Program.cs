using ToDo.ContentNotifier;
using ToDo.ContentNotifier.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddCustomQuartz();

//builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
