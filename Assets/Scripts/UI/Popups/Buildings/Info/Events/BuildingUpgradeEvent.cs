using Buildings;
using EventSystemComponents;
namespace UI.Popups.Buildings.Info.Events
{
  public class BuildingUpgradeEvent : BaseEvent
  {
    public BuildingData BuildingData;
    public float Price;

    public BuildingUpgradeEvent (BuildingData buildingData, float price)
    {
      BuildingData = buildingData;
      Price = price;
    }
  }
}