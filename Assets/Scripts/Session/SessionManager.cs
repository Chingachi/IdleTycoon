using System;
using Buildings.BuildingState.Income;
using EventSystemComponents;
using Popups.Buildings.Buy;
using Storages;
using Storages.Base;
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
    }

    ~SessionManager()
    {
      _eventManager.UnsubscribeEvent<IncomeEvent>(HandleIncome);
      _eventManager.UnsubscribeEvent<BuildingPurchasedEvent>(HandleBuildingPurchase);
    }


    private void HandleBuildingPurchase (BuildingPurchasedEvent eventData)
    {
      ChangeBalance(eventData.BuildingDto.Price, BalanceChangeEvent.BalanceChangeType.Outcome);
    }


    private void HandleIncome (IncomeEvent eventData)
    {
      ChangeBalance((int)eventData.Money, BalanceChangeEvent.BalanceChangeType.Income);
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
      _eventManager.Fire(new BalanceChangeEvent(_storage.Data.Money, amount, BalanceChangeEvent.BalanceChangeType.Income));
    }
  }
}