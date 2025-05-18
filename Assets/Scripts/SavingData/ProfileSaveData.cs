using Core.Storages;
namespace SavingData
{
  [SaveFilename("ProfileData")]
  public class ProfileSaveData : ISaveData
  {
    public int Money = 1000;
    public long LastTimeUpdated;
    public bool FirstLaunch = true;

    public void Serialize()
    {}

    public void Deserialize()
    {}
  }
}