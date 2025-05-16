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

    public TickTimer(CoroutineRunner runner)
    {
      runner.Run(TickLoop());
    }

    private IEnumerator TickLoop()
    {
      WaitForSecondsRealtime wait = new WaitForSecondsRealtime(1f);

      while (true) {
        yield return wait;
        OnTick?.Invoke();
      }
    }
  }
}