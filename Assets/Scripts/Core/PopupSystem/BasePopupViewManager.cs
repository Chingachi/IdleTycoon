using Core.EventSystemComponents;
using Core.PopupSystem.Components;
namespace Core.PopupSystem
{
  public abstract class BasePopupViewManager<TData>
    where TData : IPopupData
  {
    protected readonly PopupManager _popupManager;
    protected readonly EventManager _eventManager;

    protected TData _data;

    protected BasePopupViewManager (PopupManager popupManager, EventManager eventManager)
    {
      _popupManager = popupManager;
      _eventManager = eventManager;
    }

    public virtual void OpenPopup (TData data)
    {
      _data = data;
      data.Callback += HandleLoadedPopup;
      _popupManager.OpenPopup(data);
    }

    protected abstract void HandleLoadedPopup (BasePopup popup);
  }
}