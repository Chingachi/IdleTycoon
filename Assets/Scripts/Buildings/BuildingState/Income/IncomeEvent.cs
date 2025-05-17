using EventSystemComponents;
namespace Buildings.BuildingState.Income
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