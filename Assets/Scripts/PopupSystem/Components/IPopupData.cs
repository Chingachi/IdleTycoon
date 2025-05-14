using System;
namespace PopupSystem.Components
{
  public interface IPopupData
  {
    Type GetPopupType();

    Action<BasePopup> Callback { get; set; }
  }
}