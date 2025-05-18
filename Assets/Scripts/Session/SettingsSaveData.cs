using System;
using Core.Storages;
namespace Session
{
  [Serializable]
  public class SettingsSaveData : ISaveData
  {
    public float MusicVolume = 1;
    public bool PointersActiveState = true;

    public void Serialize()
    {}

    public void Deserialize()
    {}
  }
}