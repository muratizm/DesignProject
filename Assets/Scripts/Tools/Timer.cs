using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Timer 
{
    private bool _isPaused = false;
    private float _duration;

    private float _elapsedTime;
    public float ElapsedTime => _elapsedTime;


    private CancellationTokenSource _cts;
    public event Action OnTimerTick;
    public event Action OnTimerComplete;


    public void SetTimer(float duration)
    {
        this._duration = duration;
        _elapsedTime = 0f;
    }

    public async void StartTimer()
    {
        // If the timer is already running, cancel it
        _cts?.Cancel();
        _cts = new CancellationTokenSource();



        _isPaused = false;  
        await TimerAsync(_cts.Token);
    }

    public async Task TimerAsync(CancellationToken ct)
    {
        while (_elapsedTime < _duration)
        {
            if (ct.IsCancellationRequested)
            {
                return;
            }

            if (!_isPaused)
            {
                _elapsedTime += Time.deltaTime;
                OnTimerTick?.Invoke();
            }


            // Wait for a short period of time (e.g., 1 millisecond)
            try
            {
                await Task.Delay(1, ct);
            }
            catch (TaskCanceledException)
            {
                // Task was cancelled, but we're handling it so no need to throw an exception
                Debug.Log("Timer task was cancelled");
            }        
        }

        OnTimerComplete?.Invoke();
    }


    public void PauseTimer()
    {
        _isPaused = true;
    }

    public void ResumeTimer()
    {
        _isPaused = false;
    }

}