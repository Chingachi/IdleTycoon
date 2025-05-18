using Buildings.Dto;
using Core.EventSystemComponents;
using Core.PopupSystem;
using Core.Storages;
using Core.Storages.Base;
using Session;
using UI.Popups.Buildings.Buy.Popup;
namespace UI.Popups.Buildings.Buy.Manager
{
  public class BuyBuildingPopupManager : BasePopupViewManager<BuyBuildingPopupData, BuyBuildingPopup>
  {
    private readonly Storage<ProfileSaveData> _storage;

    public BuyBuildingPopupManager (PopupManager popupManager, EventManager eventManager, Storage<ProfileSaveData> storage)
      : base(popupManager, eventManager)
    {
      _storage = storage;
    }

    public override void OpenPopup (BuyBuildingPopupData data)
    {
      data.Callback += HandleLoadedPopup;
      _popupManager.OpenForcePopup(data);
    }

    protected override void HandleLoadedPopup()
    {
      _popup.OnBuy += HandlePurchase;
      _popup.SetAvailableToBuy(_storage.Data.Money);
      _eventManager.SubscribeEvent<BalanceChangeEvent>(HandleBalanceChange);
    }

    protected override void HandleClose()
    {
      _eventManager.UnsubscribeEvent<BalanceChangeEvent>(HandleBalanceChange);
    }

    private void HandleBalanceChange (BalanceChangeEvent balance)
    {
      _popup.SetAvailableToBuy(balance.CurrentBalance);
    }

    private void HandlePurchase (BuildingDto dto)
    {
      _eventManager.Fire(new BuildingPurchasedEvent(dto));
      _popup.Close();
    }
  }
}