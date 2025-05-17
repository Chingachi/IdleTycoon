using Coroutines;
using Storages;
using Storages.Base;
using Timers;
namespace Buildings.BuildingState
{
  public class DecayManager : TimingManager
  {
    private const float DECAY_PERIOD_SECONDS = 60f;
    private readonly Storage<BuildingsSaveData> _storage;

    public DecayManager (CoroutineRunner coroutineRunner, PointOneSecondTimer tickTimer, Storage<BuildingsSaveData> storage)
      : base(coroutineRunner, tickTimer)
    {
      _storage = storage;
    }

    protected override float GetTiming (Building building)
    {
      return DECAY_PERIOD_SECONDS;
    }

    protected override void HandleChange (Building building)
    {
      building.Data.ApplyDecay();

      if (_storage.Data != default) {
        _storage.UpdateData();
      }

      building.UpdateIndicators();
    }
  }
}