using Buildings.Dto;
using EventSystemComponents;
using Popups.Buildings.Buy;
using Popups.Buildings.Buy.Manager;
using Popups.Buildings.Buy.Popup;
using Popups.Buildings.Selection.Popup;
using PopupSystem;
using PopupSystem.Components;
namespace Popups.Buildings.Selection.Manager
{
  public class SelectBuildingPopupManager : BasePopupViewManager<SelectBuildingPopupData>
  {
    private readonly BuyBuildingPopupManager _buyBuildingPopupManager;

    private SelectBuildingPopup _popup;

    public SelectBuildingPopupManager (PopupManager popupManager, EventManager eventManager, BuyBuildingPopupManager buyBuildingPopupManager)
      : base(popupManager, eventManager)
    {
      _buyBuildingPopupManager = buyBuildingPopupManager;
    }

    protected override void HandleLoadedPopup (BasePopup popup)
    {
      _popup = (SelectBuildingPopup)popup;
      _popup.OnBuildingClick += HandleBuildingClick;
      _popup.OnClose += HandlePopupClose;
      _eventManager.SubscribeEvent<BuildingPurchasedEvent>(HandlePurchaseSelectedBuilding);
    }

    private void HandlePurchaseSelectedBuilding (BuildingPurchasedEvent obj)
    {
      _popup.Close();
    }

    private void HandlePopupClose()
    {
      _eventManager.UnsubscribeEvent<BuildingPurchasedEvent>(HandlePurchaseSelectedBuilding);
    }

    private void HandleBuildingClick (BuildingDto dto)
    {
      _buyBuildingPopupManager.OpenPopup(new BuyBuildingPopupData(dto));
    }
  }
}