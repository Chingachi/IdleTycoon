using Core.EventSystemComponents;
namespace UI.Popups.Buildings.Info.Events
{
  public class BuildingRepairEvent : BaseEvent
  {
    public string BuildingId;
    public float RepairCost;

    public BuildingRepairEvent (string buildingId, float repairCost)
    {
      BuildingId = buildingId;
      RepairCost = repairCost;
    }
  }
}