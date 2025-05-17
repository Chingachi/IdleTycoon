using System;
using EventSystemComponents;
using IncomeSystem;
using Storages;
using Storages.Base;
namespace Session
{
  public class SessionManager
  {
    private readonly EventManager _eventManager;
    private readonly Storage<SessionSaveData> _storage;


    public SessionManager (EventManager eventManager, Storage<SessionSaveData> storage)
    {
      _eventManager = eventManager;
      _storage = storage;
      _eventManager.SubscribeEvent<IncomeEvent>(HandleIncome);
    }

    ~SessionManager()
    {
      _eventManager.UnsubscribeEvent<IncomeEvent>(HandleIncome);
    }

    private void HandleIncome (IncomeEvent eventData)
    {
      _storage.Data.Money += (int)eventData.Money;
      _storage.Data.LastTimeUpdated = DateTime.UtcNow.Ticks;
      _storage.UpdateData();
      _eventManager.Fire(new BalanceChangeEvent(_storage.Data.Money, eventData.Money, BalanceChangeEvent.BalanceChangeType.Income));
    }
  }
}