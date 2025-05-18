using System;
using PopupSystem.Components;
namespace UI.Popups.OfflineIncome
{
  public class OfflineIncomePopupData : IPopupData
  {
    public float TotalEarned;

    public OfflineIncomePopupData (float totalEarned)
    {
      TotalEarned = totalEarned;
    }

    public Type GetPopupType()
    {
      return typeof(OfflineIncomePopup);
    }

    public Action<BasePopup> Callback
    {
      get;
      set;
    }
  }
}