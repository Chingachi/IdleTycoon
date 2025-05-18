using Buildings.Dto;
using EventSystemComponents;
namespace UI.Popups.Buildings.Buy
{
  public class BuildingPurchasedEvent : BaseEvent
  {
    public BuildingDto BuildingDto;

    public BuildingPurchasedEvent (BuildingDto buildingDto)
    {
      BuildingDto = buildingDto;
    }
  }
}