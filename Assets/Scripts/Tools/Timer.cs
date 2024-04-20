using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer 
{
    private float duration;
    private float elapsedTime;
    public float ElapsedTime => elapsedTime;


    public event Action OnTimerTick;
    public event Action OnTimerComplete;


    public void SetTimer(float duration)
    {
        this.duration = duration;
        elapsedTime = 0f;
    }


    public IEnumerator TimerCoroutine()
    {
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            OnTimerTick?.Invoke();
            yield return null;
        }

        OnTimerComplete?.Invoke();
    }

}