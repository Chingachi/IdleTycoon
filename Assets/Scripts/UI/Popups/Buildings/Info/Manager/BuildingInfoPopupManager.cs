using EventSystemComponents;
using PopupSystem;
using PopupSystem.Components;
using Session;
using Storages;
using Storages.Base;
using UI.Popups.Buildings.Info.Events;
using UI.Popups.Buildings.Info.Popup;
namespace UI.Popups.Buildings.Info.Manager
{
  public class BuildingInfoPopupManager : BasePopupViewManager<BuildingInfoPopupData>
  {

    private readonly Storage<ProfileSaveData> _storage;
    private BuildingInfoPopup _popup;

    public BuildingInfoPopupManager (PopupManager popupManager, EventManager eventManager, Storage<ProfileSaveData> storage)
      : base(popupManager, eventManager)
    {
      _storage = storage;
    }

    protected override void HandleLoadedPopup (BasePopup popup)
    {
      _popup = (BuildingInfoPopup)popup;
      _popup.OnClose += HandlePopupClose;
      _popup.OnRepair += HandleRepair;
      _popup.OnUpgrade += HandleUpgrade;
      _eventManager.SubscribeEvent<BalanceChangeEvent>(HandleBalanceChange);
      _popup.SetUpgradeButtonStatus(_storage.Data.Money >= _data.BuildingData.GetCurrentUpgradePrice());
    }

    private void HandleUpgrade()
    {
      float price = _data.BuildingData.GetCurrentUpgradePrice();
      _data.BuildingData.CurrentLevel++;
      _data.BuildingData.ResetDurability();
      _eventManager.Fire(new BuildingUpgradeEvent(_data.BuildingData, price));
      _popup.UpdateFields();
    }

    private void HandleRepair()
    {
      _data.BuildingData.ResetDurability();
      _eventManager.Fire(new RepairBuildingEvent(_data.BuildingData.Id));
      _popup.UpdateFields();
    }

    private void HandleBalanceChange (BalanceChangeEvent eventData)
    {
      _popup.SetUpgradeButtonStatus(eventData.CurrentBalance >= _data.BuildingData.GetCurrentUpgradePrice());
    }

    private void HandlePopupClose()
    {
      _eventManager.UnsubscribeEvent<BalanceChangeEvent>(HandleBalanceChange);
    }
  }
}