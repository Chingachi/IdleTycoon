using System;
using System.Collections;
using UnityEngine;
namespace Timers
{
  public class TickTimer : MonoBehaviour
  {
    public event Action OnTick;
    private Coroutine _coroutine;

    private void Start()
    {
      _coroutine = StartCoroutine(TickLoop());
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