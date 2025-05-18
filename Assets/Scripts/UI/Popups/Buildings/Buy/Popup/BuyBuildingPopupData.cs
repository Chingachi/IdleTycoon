using System;
using Buildings.Dto;
using Core.PopupSystem.Components;
namespace UI.Popups.Buildings.Buy.Popup
{
  public class BuyBuildingPopupData : IPopupData
  {
    public BuildingDto BuildingDto;

    public BuyBuildingPopupData()
    {}

    public BuyBuildingPopupData (BuildingDto buildingDto)
    {
      BuildingDto = buildingDto;
    }

    public Type GetPopupType()
    {
      return typeof(BuyBuildingPopup);
    }

    public Action<BasePopup> Callback
    {
      get;
      set;
    }
  }
}