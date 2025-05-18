namespace Core.Storages
{
  public interface ISaveData
  {
    public void Serialize();

    public void Deserialize();
  }
}