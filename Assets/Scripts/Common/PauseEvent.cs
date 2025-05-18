using Core.EventSystemComponents;
namespace Common
{
  public class PauseEvent : BaseEvent
  {
    public bool PauseState;

    public PauseEvent (bool pauseState)
    {
      PauseState = pauseState;
    }
  }
}