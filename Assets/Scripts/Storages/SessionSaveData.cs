using Storages.Base;
namespace Storages
{
  public class SessionSaveData : IData
  {
    public int Money;
    public long LastTimeUpdated;

    public void Serialize()
    {}

    public void Deserialize()
    {}
  }
}