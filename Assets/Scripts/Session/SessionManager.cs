using System;
using Buildings.BuildingState.Income;
using Core.EventSystemComponents;
using Core.Storages;
using SavingData;
using UI.Popups.Buildings.Buy;
using UI.Popups.Buildings.Info.Events;
using UnityEngine;
namespace Session
{
  public class SessionManager
  {
    private readonly EventManager _eventManager;
    private readonly Storage<ProfileSaveData> _storage;


    public SessionManager (EventManager eventManager, Storage<ProfileSaveData> storage)
    {
      _eventManager = eventManager;
      _storage = storage;
      _eventManager.SubscribeEvent<IncomeEvent>(HandleIncome);
      _eventManager.SubscribeEvent<BuildingPurchasedEvent>(HandleBuildingPurchase);
      _eventManager.SubscribeEvent<BuildingUpgradeEvent>(HandleBuildingUpgrade);
      _eventManager.SubscribeEvent<BuildingRepairEvent>(HandleBuildingRepair);
    }


    ~SessionManager()
    {
      _eventManager.UnsubscribeEvent<IncomeEvent>(HandleIncome);
      _eventManager.UnsubscribeEvent<BuildingPurchasedEvent>(HandleBuildingPurchase);
      _eventManager.UnsubscribeEvent<BuildingUpgradeEvent>(HandleBuildingUpgrade);
      _eventManager.UnsubscribeEvent<BuildingRepairEvent>(HandleBuildingRepair);
    }


    private void HandleBuildingPurchase (BuildingPurchasedEvent eventData)
    {
      ChangeBalance(eventData.BuildingDto.Price, BalanceChangeEvent.BalanceChangeType.Outcome);
    }


    private void HandleIncome (IncomeEvent eventData)
    {
      ChangeBalance((int)eventData.Money, BalanceChangeEvent.BalanceChangeType.Income);
    }

    private void HandleBuildingUpgrade (BuildingUpgradeEvent eventData)
    {
      ChangeBalance((int)eventData.Price, BalanceChangeEvent.BalanceChangeType.Outcome);
    }

    private void HandleBuildingRepair (BuildingRepairEvent eventData)
    {
      ChangeBalance((int)eventData.RepairCost, BalanceChangeEvent.BalanceChangeType.Outcome);
    }

    private void ChangeBalance (int amount, BalanceChangeEvent.BalanceChangeType type)
    {
      if (type == BalanceChangeEvent.BalanceChangeType.Income) {
        _storage.Data.Money += amount;
      } else {
        _storage.Data.Money = Mathf.Max(0, _storage.Data.Money - amount);
      }

      _storage.Data.LastTimeUpdated = DateTime.UtcNow.Ticks;
      _storage.UpdateData();
      _eventManager.Fire(new BalanceChangeEvent(_storage.Data.Money, amount, type));
    }
  }
}