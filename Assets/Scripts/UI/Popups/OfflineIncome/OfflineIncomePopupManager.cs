using Buildings.BuildingState.Income;
using Common;
using EventSystemComponents;
using PopupSystem;
using PopupSystem.Components;
namespace UI.Popups.OfflineIncome
{
  public class OfflineIncomePopupManager : BasePopupViewManager<OfflineIncomePopupData>
  {
    private OfflineIncomePopup _popup;

    public OfflineIncomePopupManager (PopupManager popupManager, EventManager eventManager)
      : base(popupManager, eventManager)
    {}

    protected override void HandleLoadedPopup (BasePopup popup)
    {
      _popup = (OfflineIncomePopup)popup;
      _popup.OnClose += HandleClose;
      _popup.OkButtonClick += HandleOkButtonClick;
      _eventManager.Fire(new PauseEvent(true));
    }

    private void HandleOkButtonClick (float totalEarned)
    {
      _eventManager.Fire(new IncomeEvent(totalEarned));
      _popup.Close();
    }

    private void HandleClose()
    {
      _eventManager.Fire(new PauseEvent(false));
    }
  }
}