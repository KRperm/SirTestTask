using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public UnityEvent started;
    public TimerSecondPassed secondPassed;
    public UnityEvent finished;
    public int second;

    private void Start()
    {
        StartCoroutine(RunTimer());
    }

    private IEnumerator RunTimer()
    {
        started.Invoke();
        for (var i = second; i > 0; i--)
        {
            secondPassed.Invoke(i);
            yield return new WaitForSecondsRealtime(1);
        }
        finished.Invoke();
    }
}

[Serializable]
public class TimerSecondPassed : UnityEvent<int> { }