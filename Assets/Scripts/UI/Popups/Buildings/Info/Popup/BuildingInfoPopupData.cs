using System;
using Buildings;
using PopupSystem.Components;
namespace UI.Popups.Buildings.Info.Popup
{
  public class BuildingInfoPopupData : IPopupData
  {
    public BuildingData BuildingData;

    public BuildingInfoPopupData (BuildingData buildingData)
    {
      BuildingData = buildingData;
    }

    public Type GetPopupType()
    {
      return typeof(BuildingInfoPopup);
    }

    public Action<BasePopup> Callback
    {
      get;
      set;
    }
  }
}