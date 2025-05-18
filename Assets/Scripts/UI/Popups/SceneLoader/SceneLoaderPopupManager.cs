using Core.EventSystemComponents;
using Core.PopupSystem;
namespace UI.Popups.SceneLoader
{
  public class SceneLoaderPopupManager : BasePopupViewManager<SceneLoaderPopupData, SceneLoaderPopup>
  {

    public SceneLoaderPopupManager (PopupManager popupManager, EventManager eventManager)
      : base(popupManager, eventManager)
    {}

    public override void OpenPopup (SceneLoaderPopupData data)
    {
      _data = data;
      data.Callback += HandleLoadedPopup;
      _popupManager.OpenForcePopup(data);
    }

    protected override void HandleLoadedPopup()
    {}
  }
}