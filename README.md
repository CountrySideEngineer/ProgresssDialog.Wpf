# ProgresssDialog.Wpf


**ProgresssDialog.Wpf** is a lightweight and reusable WPF component for displaying progress during long-running asynchronous operations.  
It allows you to run any user-defined asynchronous task and provides a modal progress window with cancellation support and progress updates.

---

## Features

- Accepts any `IAsyncTask<T>`-based task with progress updates
- Executes tasks asynchronously without freezing the UI
- Supports real-time progress reporting via `IProgress<T>`
- Allows cancellation using a flag (`ShouldContinue`) in progress data
- Clean MVVM design with clear separation of concerns
- Modal progress dialog for intuitive user feedback

---

## Installation

(Coming soon as a NuGet package)

---

## Usage

### 1. Implement a task with `IAsyncTask<ProgressInfo>` interface

```csharp
public class SampleTask : IAsyncTask<ProgressInfo>
{
    public async Task RunAsync(IProgress<ProgressInfo> progress)
    {
        for (int i = 0; i <= 100; i++)
        {
            await Task.Delay(50);
            progress?.Report(new ProgressInfo { Percentage = i, ShouldContinue = true });
        }
    }
}
```
### 2. Start the progress window

```csharp
var task = new SampleTask();
var window = new AsyncProgressDialog.ProgressWindow();
window.Start(task);
```

For more detailed implementation and source code, please refer to the sample program.  
The sample program is stored [here](sample/).
