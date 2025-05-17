using Coroutines;
namespace Timers
{
  public class PointOneSecondTimer : TickTimer
  {

    public PointOneSecondTimer (CoroutineRunner runner)
      : base(runner)
    {}

    protected override float GetWaitTimeInSeconds()
    {
      return 0.1f;
    }
  }
}