using System;
using Core.PopupSystem.Components;
namespace UI.Popups.Settings
{
  public class SettingsPopupData : IPopupData
  {
    public float Volume;

    public SettingsPopupData (float volume)
    {
      Volume = volume;
    }

    public Type GetPopupType()
    {
      return typeof(SettingsPopup);
    }

    public Action<BasePopup> Callback
    {
      get;
      set;
    }
  }
}