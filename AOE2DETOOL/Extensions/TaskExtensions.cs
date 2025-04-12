public static class TaskExtensions
{
    public static async void Forget(this Task task)
    {
        try
        {
            await task.ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unhandled exception in fire-and-forget task: {ex}");
        }
    }
}
