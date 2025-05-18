using Buildings;
using EventSystemComponents;
namespace Popups.Buildings.Info.Events
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