using System.Collections;
using System.Collections.Generic;
using Buildings;
using Coroutines;
using Storages;
using Storages.Base;
using Timers;
using UnityEngine;
namespace IncomeSystem
{
  public class IncomeManager
  {
    private List<Building> _buildings = new List<Building>();
    private readonly CoroutineRunner _coroutineRunner;
    private readonly TickTimer _tickTimer;
    private readonly Storage<BuildingsStorage> _storage;

    private Dictionary<Building, float> _buildingTimings = new Dictionary<Building, float>();

    private Coroutine _coroutine;

    public IncomeManager(CoroutineRunner coroutineRunner, TickTimer tickTimer, Storage<BuildingsStorage> storage)
    {
      _coroutineRunner = coroutineRunner;
      _tickTimer = tickTimer;
      _storage = storage;
      _tickTimer.OnTick += HandleTick;
    }

    ~IncomeManager()
    {
      _tickTimer.OnTick -= HandleTick;
      _coroutineRunner.Stop(_coroutine);
    }

    private void HandleTick()
    {
      foreach (Building building in _buildings) {
        building.Data.WaitedSeconds+=0.1f;
        building.UpdateIndicators();
      }
    }

    public void RegisterBuilding (Building building)
    {
      _buildings.Add(building);
    }

    public void StartCounting()
    {
      _coroutine = _coroutineRunner.Run(WaitAndCountIncome());
    }

    private IEnumerator WaitAndCountIncome()
    {
      var wait = new WaitForSecondsRealtime(1);
      float waitingTime = 0;

      while (true) {
        if (_buildings.Count == 0) {
          yield return new WaitUntil(() => _buildings.Count > 0);
        }

        foreach (Building building in _buildings) {
          if (!_buildingTimings.ContainsKey(building)) {
            _buildingTimings.Add(building, building.Data.GetCurrentIncomeTime());

            continue;
          }

          float currentTime = _buildingTimings[building];
          currentTime -= waitingTime;

          if (currentTime > 0) {
            _buildingTimings[building] = currentTime;
          } else {
            HandleIncome(building);
            _buildingTimings[building] = building.Data.GetCurrentIncomeTime();
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

        foreach (KeyValuePair<Building, float> timing in _buildingTimings) {
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

    private void HandleIncome (Building building)
    {
      Debug.Log($"Handle income: {building.Data.GetCurrentIncome()}");
      building.Data.WaitedSeconds = 0;
      building.UpdateIndicators();
      SaveData();
    }

    private void SaveData()
    {
      List<BuildingData> dataList = new List<BuildingData>();
      foreach (Building building in _buildings) {
        dataList.Add(building.Data);
      }

      _storage.Data.buildingsData = dataList;
      _storage.UpdateData();
    }
  }
}