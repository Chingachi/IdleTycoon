using System;
using System.Collections.Generic;
using Buildings.Dto;
using PopupSystem.Components;
namespace Popups.Buildings.Selection.Popup
{
  public class SelectBuildingPopupData : IPopupData
  {
    public List<BuildingDto> Buildings;
    public int CurrentBalance;

    public Type GetPopupType()
    {
      return typeof(SelectBuildingPopup);
    }

    public Action<BasePopup> Callback
    {
      get;
      set;
    }
  }
}