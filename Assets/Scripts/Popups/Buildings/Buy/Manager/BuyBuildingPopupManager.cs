using Buildings.Dto;
using EventSystemComponents;
using Popups.Buildings.Buy.Popup;
using PopupSystem;
using PopupSystem.Components;
using Session;
using Storages;
using Storages.Base;
namespace Popups.Buildings.Buy.Manager
{
  public class BuyBuildingPopupManager : BasePopupViewManager<BuyBuildingPopupData>
  {
    private readonly Storage<ProfileSaveData> _storage;
    private BuyBuildingPopup _popup;

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

    protected override void HandleLoadedPopup (BasePopup popup)
    {
      _popup = (BuyBuildingPopup)popup;
      _popup.OnBuy += HandlePurchase;
      _popup.OnClose += HandlePopupClose;
      _popup.SetAvailableToBuy(_storage.Data.Money);
      _eventManager.SubscribeEvent<BalanceChangeEvent>(HandleBalanceChange);
    }

    private void HandleBalanceChange (BalanceChangeEvent balance)
    {
      _popup.SetAvailableToBuy(balance.CurrentBalance);
    }

    private void HandlePopupClose()
    {
      _eventManager.UnsubscribeEvent<BalanceChangeEvent>(HandleBalanceChange);
    }

    private void HandlePurchase (BuildingDto dto)
    {
      _eventManager.Fire(new BuildingPurchasedEvent(dto));
      _popup.Close();
    }
  }
}