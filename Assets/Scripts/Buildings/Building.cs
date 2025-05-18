using Common;
namespace Buildings
{
  public class Building : ClickableGameObject
  {
    private StatusIndicator _statusIndicator;

    public void SetData (BuildingData data)
    {
      Data = data;
      _statusIndicator.UpdateStatus(0, data.CurrentDurability);
    }

    public void UpdateIndicators()
    {
      _statusIndicator.UpdateStatus(Data.IncomeWaitedSeconds / Data.GetCurrentIncomeTime(), 1 - Data.CurrentDurability);
    }

    public void SetIndicator (StatusIndicator indicator)
    {
      _statusIndicator = indicator;
    }

    public BuildingData Data
    {
      get;
      private set;
    }
    public string Id
    {
      get
      {
        return Data.Id;
      }
    }
  }
}