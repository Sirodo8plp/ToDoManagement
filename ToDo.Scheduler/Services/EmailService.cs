namespace ToDo.ContentNotifier.Services;

public class EmailService : IEmailService
{
    public async Task SendEmail(string email)
    {
        Console.WriteLine(email);
        await Task.CompletedTask;
    }
}
