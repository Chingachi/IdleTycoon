using Common;
using UnityEngine;
namespace Buildings
{
  public class Building : ClickableGameObject
  {
    private StatusIndicator _statusIndicator;
    private BuildingData _data;

    public void SetData(BuildingData data)
    {
      _data = data;
      _statusIndicator.UpdateStatus(0, data.CurrentDecay);
    }

    public void UpdateIndicators()
    {
      //TODO: change decay slider value
      _statusIndicator.UpdateStatus(_data.WaitedSeconds/_data.GetCurrentIncomeTime(), _data.CurrentDecay);
    }
    
    public void SetIndicator (StatusIndicator indicator)
    {
      _statusIndicator = indicator;
    }

    public BuildingData Data
    {
      get
      {
        return _data;
      }
    }
  }
}