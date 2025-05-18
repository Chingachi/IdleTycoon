using System;
using System.Collections.Generic;
using Buildings;
using Core.Storages.Base;
namespace Core.Storages
{
  [Serializable, SaveFilename("BuildingsInfo")]
  public class BuildingsSaveData : ISaveData
  {
    public List<BuildingData> buildingsData = new List<BuildingData>();

    public void Serialize()
    {}

    public void Deserialize()
    {}
  }
}