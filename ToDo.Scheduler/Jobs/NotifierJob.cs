namespace ToDo.ContentNotifier.Jobs;

using Quartz;
using ToDo.ContentNotifier.Services;

public class NotifierJob(IEmailService emailService) : IJob
{
    public static readonly JobKey Key = new("NotifierJob", "ContentNotifier");
    public static string Description = "Notify subscribers about app changes and new features.";

    private readonly IEmailService _emailService = emailService;

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            await _emailService.SendEmail("test@test.com");
        }
        catch (Exception ex)
        {
            throw new JobExecutionException(ex, false);
        }
    }
}
