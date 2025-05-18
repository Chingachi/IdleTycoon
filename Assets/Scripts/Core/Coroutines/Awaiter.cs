using System;
using System.Collections.Generic;
namespace Core.Coroutines
{
  public class Awaiter
  {
    private readonly Dictionary<Type, int> _waitingList = new Dictionary<Type, int>();

    public void AddToWait (Type type)
    {
      if (_waitingList.ContainsKey(type)) {
        _waitingList[type]++;
      } else {
        _waitingList.Add(type, 1);
      }
    }

    public void RemoveFromWaiting (Type type)
    {
      if (!_waitingList.ContainsKey(type)) {
        return;
      }

      if (--_waitingList[type] <= 0) {
        _waitingList.Remove(type);
      }
    }

    public bool CheckAnyWaiting()
    {
      return _waitingList.Count > 0;
    }

    public bool CheckSpecificWaiting (Type type)
    {
      return _waitingList.ContainsKey(type);
    }
  }
}