using System.Collections;
using System.Collections.Generic;
using Coroutines;
using Timers;
using UnityEngine;
namespace Buildings.BuildingState
{
  public abstract class TimingManager
  {
    protected readonly CoroutineRunner _coroutineRunner;
    protected readonly PointOneSecondTimer _tickTimer;

    protected readonly List<Building> _buildings = new List<Building>();
    protected readonly Dictionary<string, float> _buildingTimings = new Dictionary<string, float>();

    protected Coroutine _coroutine;
    protected float waitedTime;

    protected TimingManager (CoroutineRunner coroutineRunner, PointOneSecondTimer tickTimer)
    {
      _coroutineRunner = coroutineRunner;
      _tickTimer = tickTimer;
      tickTimer.OnTick += HandleTenthOfSecondTick;
    }

    ~TimingManager()
    {
      _coroutineRunner.Stop(_coroutine);
      _tickTimer.OnTick -= HandleTenthOfSecondTick;
    }

    public void Start()
    {
      _coroutine = _coroutineRunner.Run(WaitAndCountIncome());
    }

    public void Stop()
    {
      _coroutineRunner.Stop(_coroutine);

      foreach (Building b in _buildings) {
        if (_buildingTimings.ContainsKey(b.Id)) {
          _buildingTimings[b.Id] -= waitedTime;
        }
      }

      waitedTime = 0;
    }

    public virtual void RegisterBuilding (Building building)
    {
      _buildings.Add(building);

      if (_coroutine == null) {
        return;
      }

      Restart();
    }

    protected virtual void Restart()
    {
      Stop();
      _coroutine = _coroutineRunner.Run(WaitAndCountIncome());
    }

    protected virtual void HandleTenthOfSecondTick()
    {
      waitedTime += 0.1f;
    }

    protected IEnumerator WaitAndCountIncome()
    {
      WaitForSecondsRealtime wait = new WaitForSecondsRealtime(1);
      float waitingTime = 0;

      while (true) {
        if (_buildings.Count == 0) {
          yield return new WaitUntil(() => _buildings.Count > 0);
        }

        foreach (Building building in _buildings) {
          if (!_buildingTimings.ContainsKey(building.Id)) {
            _buildingTimings.Add(building.Id, GetTiming(building));

            continue;
          }

          float currentTime = _buildingTimings[building.Id];
          currentTime -= waitingTime;

          if (currentTime > 0) {
            _buildingTimings[building.Id] = currentTime;
          } else {
            HandleChange(building);
            _buildingTimings[building.Id] = GetTiming(building);
          }
        }

        waitingTime = GetShortestTime();

        wait.waitTime = waitingTime;

        yield return wait;
      }

      float GetShortestTime()
      {
        float result = 0;
        bool isFirst = true;

        foreach (KeyValuePair<string, float> timing in _buildingTimings) {
          if (isFirst) {
            result = timing.Value;
            isFirst = false;

            continue;
          }

          if (result > timing.Value) {
            result = timing.Value;
          }
        }

        return result;
      }
    }

    protected abstract float GetTiming (Building building);

    protected abstract void HandleChange (Building building);
  }
}