using System;
using System.Collections;
using Common;
using Core.Coroutines;
using Core.EventSystemComponents;
using UnityEngine;
namespace Timers
{
  public class TickTimer
  {
    private readonly CoroutineRunner _runner;
    private readonly EventManager _eventManager;
    public event Action OnTick;
    private Coroutine _coroutine;

    public TickTimer (CoroutineRunner runner, EventManager eventManager)
    {
      _eventManager = eventManager;
      _runner = runner;
      _coroutine = runner.Run(TickLoop());
      _eventManager.SubscribeEvent<PauseEvent>(HandlePause);
    }

    ~TickTimer()
    {
      _eventManager.UnsubscribeEvent<PauseEvent>(HandlePause);
    }

    protected virtual float GetWaitTimeInSeconds()
    {
      return 1f;
    }

    private void HandlePause (PauseEvent eventData)
    {
      if (eventData.PauseState) {
        _runner.Stop(_coroutine);
      } else {
        _coroutine = _runner.Run(TickLoop());
      }
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