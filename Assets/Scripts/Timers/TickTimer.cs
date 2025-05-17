using System;
using System.Collections;
using Coroutines;
using UnityEngine;
namespace Timers
{
  public class TickTimer
  {
    public event Action OnTick;
    private Coroutine _coroutine;

    public TickTimer (CoroutineRunner runner)
    {
      runner.Run(TickLoop());
    }

    protected virtual float GetWaitTimeInSeconds()
    {
      return 1f;
    }

    private IEnumerator TickLoop()
    {
      WaitForSecondsRealtime wait = new WaitForSecondsRealtime(GetWaitTimeInSeconds());

      while (true) {
        yield return wait;
        OnTick?.Invoke();
      }
    }
  }
}