namespace SMS.Services;

public class SaveActionService
{
    public event Func<Task>? OnSaveRequested;
    public event Action? OnVisibilityChanged;

    public bool IsSaveVisible { get; private set; }

    public void SetSaveVisible(bool visible, Func<Task>? saveCallback = null)
    {
        IsSaveVisible = visible;
        OnSaveRequested = saveCallback;
        OnVisibilityChanged?.Invoke();
    }

    public async Task TriggerSaveAsync()
    {
        if (OnSaveRequested != null)
        {
            await OnSaveRequested.Invoke();
        }
    }
}