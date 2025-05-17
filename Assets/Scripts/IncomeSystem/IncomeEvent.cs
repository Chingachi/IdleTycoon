using EventSystemComponents;
namespace IncomeSystem
{
  public class IncomeEvent : BaseEvent
  {
    public float Money;

    public IncomeEvent (float money)
    {
      Money = money;
    }
  }
}