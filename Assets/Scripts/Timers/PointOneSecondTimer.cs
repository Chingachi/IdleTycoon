using Coroutines;
using EventSystemComponents;
namespace Timers
{
  public class PointOneSecondTimer : TickTimer
  {
    public PointOneSecondTimer (CoroutineRunner runner, EventManager eventManager)
      : base(runner, eventManager)
    {}

    protected override float GetWaitTimeInSeconds()
    {
      return 0.1f;
    }
  }
}