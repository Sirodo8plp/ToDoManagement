namespace ToDo.ContentNotifier.Services;

public interface IEmailService
{
    public Task SendEmail(string email);
}