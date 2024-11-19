using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Threading.Tasks;
using scoreboard2.RemoteControl.Attributes;
#pragma warning disable CS0657 // Not a valid attribute location for this declaration

namespace scoreboard2.Models;

public partial class Clock : ObservableObject
{
    [ObservableProperty]
    private bool _running;
    [ObservableProperty]
    [property: ReplicatorIgnore]
    private string _timeLeftOutput = string.Empty;
    
    [ReplicatorSyncIgnore] public TimeSpan Time { get; }
    [ReplicatorSyncIgnore] public DateTime TimeStarted { get; private set; }
    [ReplicatorSyncIgnore] public DateTime PausedAt { get; private set; }
    public bool IsShotClock { get; set; }
    public int ShotClockThreshold { get; set; }
    
    [ObservableProperty] 
    [property: ReplicatorIgnore]
    private TimeSpan _timeLeft;

    private static DateTime CurrentTime => DateTime.Now;

    public Clock(TimeSpan time)
    {
      Time = time;
      TimeStarted = CurrentTime;
      PausedAt = CurrentTime;
      TimeLeft = Time;
      // TimeLeftOutput = Time.Seconds;
      _ = TimeThread();
    }

    private async Task TimeThread()
    {
        while (true)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(50));
            if (!Running) continue;
            
            TimeLeft = Time - (CurrentTime - TimeStarted);
            SetTimeLeftOutput();
        }
        // ReSharper disable once FunctionNeverReturns
    }

    partial void OnTimeLeftOutputChanged(string value)
    {
        Console.WriteLine(value);
    }

    private void SetTimeLeftOutput()
    {
        if (!IsShotClock)
        {
            TimeLeftOutput = $"{TimeLeft.Minutes}:{TimeLeft.Seconds}";
            return;
        }

        string output;
        if (TimeLeft.TotalSeconds < ShotClockThreshold)
        {
            output = $":{TimeLeft.TotalSeconds:F1}";
        }
        else if (TimeLeft.Minutes == 0)
        {
            output = $":{TimeLeft.Seconds}";
        }
        else
        {
            output = $"{TimeLeft.Minutes}:{TimeLeft.Seconds}";
        }

        TimeLeftOutput = output;
    }
}

public partial class Clock
{
    public void Start() => SetRunning(true);

    public void Pause() => SetRunning(false);

    public void Reset()
    {
        TimeStarted = CurrentTime;
        TimeLeft = Time;
        SetRunning(false);
    }
    
    private void SetRunning(bool value)
    {
        Running = value;
        if (value)
            TimeStarted = CurrentTime - (PausedAt - TimeStarted);
        else
            PausedAt = CurrentTime;
    }
}
