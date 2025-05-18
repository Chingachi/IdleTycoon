using System.Collections.Generic;
using Coroutines;
using EventSystemComponents;
using Storages;
using Storages.Base;
using Timers;
using UI.Popups.Buildings.Info.Events;
namespace Buildings.BuildingState.Income
{
  public class IncomeManager : TimingManager
  {
    private readonly Storage<BuildingsSaveData> _storage;

    public IncomeManager (CoroutineRunner coroutineRunner, PointOneSecondTimer tickTimer, EventManager eventManager, Storage<BuildingsSaveData> storage)
      : base(coroutineRunner, tickTimer, eventManager)
    {
      _storage = storage;
      _eventManager.SubscribeEvent<BuildingUpgradeEvent>(HandleBuildingUpgrade);
    }

    ~IncomeManager()
    {
      _eventManager.UnsubscribeEvent<BuildingUpgradeEvent>(HandleBuildingUpgrade);
    }

    protected override void HandleTenthOfSecondTick()
    {
      base.HandleTenthOfSecondTick();

      foreach (Building building in _buildings) {
        building.Data.IncomeWaitedSeconds += 0.1f;
        building.UpdateIndicators();
      }
    }

    protected override float GetTiming (Building building)
    {
      return building.Data.GetCurrentIncomeTime();
    }

    protected override void HandleChange (Building building)
    {
      _eventManager.Fire(new IncomeEvent(building.Data.GetCurrentIncome()));
      building.Data.IncomeWaitedSeconds = 0;
      building.UpdateIndicators();
      SaveData();
    }

    private void HandleBuildingUpgrade (BuildingUpgradeEvent eventData)
    {
      _buildingTimings[eventData.BuildingData.Id] = eventData.BuildingData.GetCurrentIncomeTime();
      _storage.UpdateData();
      Restart();
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