using Core.EventSystemComponents;
namespace UI.Popups.Buildings.Info.Events
{
  public class RepairBuildingEvent : BaseEvent
  {
    public string buildingId;

    public RepairBuildingEvent (string buildingId)
    {
      this.buildingId = buildingId;
    }
  }
}