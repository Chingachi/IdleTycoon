using Core.EventSystemComponents;
namespace Music
{
  public class VolumeChangeEvent : BaseEvent
  {
    public int Volume;

    public VolumeChangeEvent (int volume)
    {
      Volume = volume;
    }
  }
}