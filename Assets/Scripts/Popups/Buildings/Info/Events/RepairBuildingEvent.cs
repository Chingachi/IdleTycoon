using EventSystemComponents;
namespace Popups.Buildings.Info.Events
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