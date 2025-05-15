using System;
using PopupSystem.Components;
namespace Popups.Buildings.Selection
{
  public class SelectBuildingPopupData : IPopupData
  {

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