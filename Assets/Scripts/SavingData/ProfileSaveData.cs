namespace Core.Storages
{
  [SaveFilename("ProfileData")]
  public class ProfileSaveData : ISaveData
  {
    public int Money = 1000;
    public long LastTimeUpdated;

    public void Serialize()
    {}

    public void Deserialize()
    {}
  }
}