using System;
namespace Buildings.Dto
{
  [Serializable]
  public class BuildingDto
  {

    public string Name;
    public BuildingType Type;
    public int Price;
    public Building Prefab;

    public BuildingDto()
    {}

    public BuildingDto (string name, BuildingType type, int price, Building prefab)
    {
      Name = name;
      Type = type;
      Price = price;
      Prefab = prefab;
    }

    public BuildingDto (BuildingDto dto)
    {
      Name = dto.Name;
      Type = dto.Type;
      Price = dto.Price;
      Prefab = dto.Prefab;
    }
  }
}