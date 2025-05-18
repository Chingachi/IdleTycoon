using Core.EventSystemComponents;
namespace Buildings.PlaceholderComponents
{
  public class ShowPlaceholdersEvent : BaseEvent
  {
    public bool IsShow;

    public ShowPlaceholdersEvent (bool isShow)
    {
      IsShow = isShow;
    }
  }
}