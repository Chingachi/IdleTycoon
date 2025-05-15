using System;
using UnityEngine;
namespace Buildings.Dto
{
  [Serializable]
  public class BuildingDto
  {

    public string Name;
    public BuildingType Type;
    public int Price;
    public Sprite Preview;
    public Building Prefab;

    public BuildingDto()
    {}

    public BuildingDto (string name, BuildingType type, int price, Sprite preview, Building prefab)
    {
      Name = name;
      Type = type;
      Price = price;
      Preview = preview;
      Prefab = prefab;
    }

    public BuildingDto (BuildingDto dto)
    {
      Name = dto.Name;
      Type = dto.Type;
      Price = dto.Price;
      Preview = dto.Preview;
      Prefab = dto.Prefab;
    }
  }
}