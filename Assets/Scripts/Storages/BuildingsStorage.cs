using System;
using System.Collections.Generic;
using Buildings;
using Storages.Base;
namespace Storages
{
  [Serializable]
  public class BuildingsStorage : IData
  {
    public List<BuildingData> buildingsData = new List<BuildingData>();

    public void Serialize()
    {}

    public void Deserialize()
    {}
  }
}