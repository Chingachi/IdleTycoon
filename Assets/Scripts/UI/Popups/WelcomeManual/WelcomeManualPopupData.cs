using System;
using Core.PopupSystem.Components;
namespace UI.Popups.WelcomeManual
{
  public class WelcomeManualPopupData : IPopupData
  {

    public Type GetPopupType()
    {
      return typeof(WelcomeManualPopup);
    }

    public Action<BasePopup> Callback
    {
      get;
      set;
    }
  }
}