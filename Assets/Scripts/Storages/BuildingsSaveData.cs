using System;
using System.Collections.Generic;
using Buildings;
using Storages.Base;
namespace Storages
{
  [Serializable]
  public class BuildingsSaveData : IData
  {
    public List<BuildingData> buildingsData = new List<BuildingData>();

    public void Serialize()
    {}

    public void Deserialize()
    {}
  }
}