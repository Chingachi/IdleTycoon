using Core.EventSystemComponents;
using Core.PopupSystem;
using Core.Storages;
using SavingData;
namespace UI.Popups.WelcomeManual
{
  public class WelcomeManualPopupManager : BasePopupViewManager<WelcomeManualPopupData, WelcomeManualPopup>
  {
    public WelcomeManualPopupManager (PopupManager popupManager, EventManager eventManager, Storage<ProfileSaveData> storage)
      : base(popupManager, eventManager)
    {
      CheckFirstLaunch(storage);
    }

    protected override void HandleLoadedPopup()
    {}

    public void OpenPopup()
    {
      OpenPopup(new WelcomeManualPopupData());
    }

    private void CheckFirstLaunch (Storage<ProfileSaveData> storage)
    {
      if (!storage.Data.FirstLaunch) {
        return;
      }

      OpenPopup();
      storage.Data.FirstLaunch = false;
      storage.UpdateData();
    }
  }
}