using Common;
using Coroutines;
using EventSystemComponents;
using Popups.Buildings.Info.Events;
using Storages;
using Storages.Base;
using Timers;
namespace Buildings.BuildingState
{
  public class DecayManager : TimingManager
  {
    private readonly Storage<BuildingsSaveData> _storage;
    private readonly EventManager _eventManager;

    public DecayManager (CoroutineRunner coroutineRunner, PointOneSecondTimer tickTimer, Storage<BuildingsSaveData> storage, EventManager eventManager)
      : base(coroutineRunner, tickTimer)
    {
      _storage = storage;
      _eventManager = eventManager;
      eventManager.SubscribeEvent<RepairBuildingEvent>(HandleBuildingRepair);
    }

    ~DecayManager()
    {
      _eventManager.UnsubscribeEvent<RepairBuildingEvent>(HandleBuildingRepair);
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

    private void HandleBuildingRepair (RepairBuildingEvent eventData)
    {
      _buildingTimings[eventData.buildingId] = Constants.DECAY_INTERVAL_IN_SECONDS;
      _storage.UpdateData();
      Restart();
    }
  }
}