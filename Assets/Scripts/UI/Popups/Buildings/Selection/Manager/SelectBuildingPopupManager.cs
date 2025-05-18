using System.Linq;
using Buildings.Dto;
using Core.EventSystemComponents;
using Core.PopupSystem;
using Core.PopupSystem.Components;
using Core.Storages;
using Core.Storages.Base;
using Session;
using UI.Popups.Buildings.Buy;
using UI.Popups.Buildings.Buy.Manager;
using UI.Popups.Buildings.Buy.Popup;
using UI.Popups.Buildings.Selection.Popup;
namespace UI.Popups.Buildings.Selection.Manager
{
  public class SelectBuildingPopupManager : BasePopupViewManager<SelectBuildingPopupData>
  {
    private readonly BuyBuildingPopupManager _buyBuildingPopupManager;
    private readonly BuildingsSO _buildingsSo;
    private readonly Storage<ProfileSaveData> _storage;

    private SelectBuildingPopup _popup;

    public SelectBuildingPopupManager (
      PopupManager popupManager, EventManager eventManager, BuyBuildingPopupManager buyBuildingPopupManager, BuildingsSO buildingsSo, Storage<ProfileSaveData> storage)
      : base(popupManager, eventManager)
    {
      _buyBuildingPopupManager = buyBuildingPopupManager;
      _buildingsSo = buildingsSo;
      _storage = storage;
    }

    public override void OpenPopup (SelectBuildingPopupData data)
    {
      SelectBuildingPopupData newData = new SelectBuildingPopupData
      {
        Buildings = _buildingsSo.GetAllBuildings().OrderBy(x => x.Price).ToList(),
        CurrentBalance = _storage.Data.Money
      };

      base.OpenPopup(newData);
    }

    protected override void HandleLoadedPopup (BasePopup popup)
    {
      _popup = (SelectBuildingPopup)popup;
      _popup.OnBuildingClick += HandleBuildingClick;
      _popup.OnClose += HandlePopupClose;
      _eventManager.SubscribeEvent<BuildingPurchasedEvent>(HandlePurchaseSelectedBuilding);
      _eventManager.SubscribeEvent<BalanceChangeEvent>(HandleBalanceChange);
    }

    private void HandleBalanceChange (BalanceChangeEvent eventData)
    {
      _popup.UpdateAvailableToBuy((int)eventData.CurrentBalance);
    }

    private void HandlePurchaseSelectedBuilding (BuildingPurchasedEvent obj)
    {
      _popup.Close();
    }

    private void HandlePopupClose()
    {
      _eventManager.UnsubscribeEvent<BuildingPurchasedEvent>(HandlePurchaseSelectedBuilding);
      _eventManager.UnsubscribeEvent<BalanceChangeEvent>(HandleBalanceChange);
    }

    private void HandleBuildingClick (BuildingDto dto)
    {
      _buyBuildingPopupManager.OpenPopup(new BuyBuildingPopupData(dto));
    }
  }
}