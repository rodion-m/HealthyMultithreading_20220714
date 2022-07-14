namespace NewThreadSyncContext;

public class NewThreadSynchronizationContext : SynchronizationContext
{
    public override void Post(SendOrPostCallback d, object? state)
    {
        Task.Factory.StartNew(() => d(state), TaskCreationOptions.LongRunning);
    }
    
    public override void Send(SendOrPostCallback d, object? state)
    {
        Task.Factory.StartNew(() => d(state), TaskCreationOptions.LongRunning).Wait();
    }
}