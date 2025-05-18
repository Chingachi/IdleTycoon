using System;
using Core.PopupSystem.Components;
namespace UI.Popups.SceneLoader
{
  public class SceneLoaderPopupData : IPopupData
  {
    public Type GetPopupType()
    {
      return typeof(SceneLoaderPopup);
    }

    public Action<BasePopup> Callback
    {
      get;
      set;
    }
  }
}