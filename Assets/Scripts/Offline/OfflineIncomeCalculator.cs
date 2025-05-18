using System;
using Buildings;
using Common;
using Core.DI;
using Core.Storages;
using SavingData;
using UI.Popups.OfflineIncome;
namespace Offline
{
  public class OfflineIncomeCalculator
  {
    private readonly Storage<BuildingsSaveData> _buildingsStorage;
    private readonly Storage<ProfileSaveData> _sessionStorage;
    private readonly OfflineIncomePopupManager _offlineIncomePopupManager;

    public OfflineIncomeCalculator (
      Storage<BuildingsSaveData> buildingsStorage, Storage<ProfileSaveData> sessionStorage, DiContainer container, OfflineIncomePopupManager popupManager)
    {
      _buildingsStorage = buildingsStorage;
      _sessionStorage = sessionStorage;
      _offlineIncomePopupManager = popupManager;

      container.Unbind<OfflineIncomeCalculator>();

      CalculateIncomes();
    }

    public void CalculateIncomes()
    {
      float decayInterval = Constants.DECAY_INTERVAL_IN_SECONDS;

      DateTime now = DateTime.UtcNow;
      ProfileSaveData sessionData = _sessionStorage.Data;
      DateTime lastSessionTime = new DateTime(sessionData.LastTimeUpdated);
      float offlineSeconds = (float)(now - lastSessionTime).TotalSeconds;

      int totalEarned = 0;

      foreach (BuildingData building in _buildingsStorage.Data.buildingsData) {
        float remainingTime = offlineSeconds;
        float nextIncomeTime = building.GetCurrentIncomeTime();

        DateTime lastDecayDate = new DateTime(building.LastTimeDecayChanged);
        float timeSinceLastDecay = (float)(lastSessionTime - lastDecayDate).TotalSeconds;

        float nextDecayTime = decayInterval - timeSinceLastDecay % decayInterval;

        if (nextDecayTime <= 0) {
          nextDecayTime = decayInterval;
        }

        float decayPerTick = building.GetCurrentDecayCoefficient();

        while (remainingTime >= Math.Min(nextIncomeTime, nextDecayTime) && building.CurrentDurability > 0f) {
          if (nextIncomeTime <= nextDecayTime) {

            remainingTime -= nextIncomeTime;
            int earnedNow = (int)building.GetCurrentIncome();
            totalEarned += earnedNow;

            nextDecayTime -= nextIncomeTime;
            nextIncomeTime = building.GetCurrentIncomeTime();
          } else {
            remainingTime -= nextDecayTime;
            building.CurrentDurability = Math.Max(0f, building.CurrentDurability - decayPerTick);

            nextIncomeTime -= nextDecayTime;
            nextDecayTime = decayInterval;
          }
        }

        DateTime lastDecayEvent = now.AddSeconds(-nextDecayTime);
        building.LastTimeDecayChanged = lastDecayEvent.Ticks;
      }

      if (totalEarned > 0) {
        _offlineIncomePopupManager.OpenPopup(new OfflineIncomePopupData(totalEarned));
      }
    }
  }
}