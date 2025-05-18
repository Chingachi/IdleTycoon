using Common;
using Core.Coroutines;
using Core.EventSystemComponents;
using Core.Storages;
using Timers;
using UI.Popups.Buildings.Info.Events;
namespace Buildings.BuildingState
{
  public class DecayManager : TimingManager
  {
    private readonly Storage<BuildingsSaveData> _storage;

    public DecayManager (CoroutineRunner coroutineRunner, PointOneSecondTimer tickTimer, EventManager eventManager, Storage<BuildingsSaveData> storage)
      : base(coroutineRunner, tickTimer, eventManager)
    {
      _storage = storage;
      _eventManager.SubscribeEvent<BuildingRepairEvent>(HandleBuildingRepair);
      _eventManager.SubscribeEvent<BuildingUpgradeEvent>(HandleBuildingUpgrade);
    }

    ~DecayManager()
    {
      _eventManager.UnsubscribeEvent<BuildingRepairEvent>(HandleBuildingRepair);
      _eventManager.UnsubscribeEvent<BuildingUpgradeEvent>(HandleBuildingUpgrade);
    }

    protected override float GetTiming (Building building)
    {
      return Constants.DECAY_INTERVAL_IN_SECONDS;
    }

    protected override void HandleChange (Building building)
    {
      building.Data.ApplyDecay();

      if (_storage.Data != default) {
        _storage.UpdateData();
      }

      building.UpdateIndicators();
    }

    private void HandleBuildingUpgrade (BuildingUpgradeEvent eventData)
    {
      _buildingTimings[eventData.BuildingData.Id] = Constants.DECAY_INTERVAL_IN_SECONDS;
      _storage.UpdateData();
      Restart();
    }

    private void HandleBuildingRepair (BuildingRepairEvent eventData)
    {
      _buildingTimings[eventData.BuildingId] = Constants.DECAY_INTERVAL_IN_SECONDS;
      _storage.UpdateData();
      Restart();
    }
  }
}