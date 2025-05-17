using Storages.Base;
namespace Storages
{
  public class SessionSaveData : IData
  {
    public int Money = 1000;
    public long LastTimeUpdated;

    public void Serialize()
    {}

    public void Deserialize()
    {}
  }
}