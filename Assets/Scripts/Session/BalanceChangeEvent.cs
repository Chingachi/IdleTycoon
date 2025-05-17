using EventSystemComponents;
namespace Session
{
  public class BalanceChangeEvent : BaseEvent
  {

    public enum BalanceChangeType
    {
      Income,
      Outcome
    }
    public float CurrentBalance;
    public float ChangedAmount;
    public BalanceChangeType Type;

    public BalanceChangeEvent (float currentBalance, float changedAmount, BalanceChangeType type)
    {
      CurrentBalance = currentBalance;
      ChangedAmount = changedAmount;
      Type = type;
    }
  }
}