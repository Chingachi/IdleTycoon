using Buildings.BuildingState.Income;
using Common;
using Core.EventSystemComponents;
using Core.PopupSystem;
namespace UI.Popups.OfflineIncome
{
  public class OfflineIncomePopupManager : BasePopupViewManager<OfflineIncomePopupData, OfflineIncomePopup>
  {

    public OfflineIncomePopupManager (PopupManager popupManager, EventManager eventManager)
      : base(popupManager, eventManager)
    {}

    protected override void HandleLoadedPopup()
    {
      _popup.OkButtonClick += HandleOkButtonClick;
      _eventManager.Fire(new PauseEvent(true));
    }

    protected override void HandleClose()
    {
      _eventManager.Fire(new PauseEvent(false));
    }

    private void HandleOkButtonClick (float totalEarned)
    {
      _eventManager.Fire(new IncomeEvent(totalEarned));
      _popup.Close();
    }
  }
}