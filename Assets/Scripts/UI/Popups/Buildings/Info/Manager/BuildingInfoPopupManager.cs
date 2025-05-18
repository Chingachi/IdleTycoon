using Core.EventSystemComponents;
using Core.PopupSystem;
using Core.Storages;
using Session;
using UI.Popups.Buildings.Info.Events;
using UI.Popups.Buildings.Info.Popup;
namespace UI.Popups.Buildings.Info.Manager
{
  public class BuildingInfoPopupManager : BasePopupViewManager<BuildingInfoPopupData, BuildingInfoPopup>
  {

    private readonly Storage<ProfileSaveData> _storage;

    public BuildingInfoPopupManager (PopupManager popupManager, EventManager eventManager, Storage<ProfileSaveData> storage)
      : base(popupManager, eventManager)
    {
      _storage = storage;
    }

    protected override void HandleLoadedPopup()
    {
      _popup.OnRepair += HandleRepair;
      _popup.OnUpgrade += HandleUpgrade;
      _eventManager.SubscribeEvent<BalanceChangeEvent>(HandleBalanceChange);
      UpdateStatus(_storage.Data.Money);
    }

    protected override void HandleClose()
    {
      _eventManager.UnsubscribeEvent<BalanceChangeEvent>(HandleBalanceChange);
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
      float repairCost = _data.BuildingData.GetRepairCost();
      _data.BuildingData.ResetDurability();
      _eventManager.Fire(new BuildingRepairEvent(_data.BuildingData.Id, repairCost));
      _popup.UpdateFields();
    }

    private void HandleBalanceChange (BalanceChangeEvent eventData)
    {
      UpdateStatus(eventData.CurrentBalance);
    }

    private void UpdateStatus (float money)
    {

      bool upgradeStatus = money >= _data.BuildingData.GetCurrentUpgradePrice();
      bool repairStatus = money >= _data.BuildingData.GetRepairCost();

      _popup.SetUpgradeButtonStatus(upgradeStatus, repairStatus);
    }
  }
}