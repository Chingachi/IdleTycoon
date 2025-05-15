using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
namespace Buildings
{
  [CreateAssetMenu(fileName = "Buildings", menuName = "ScriptableObjects/Buildings", order = 2)]
  public class BuildingsSO : ScriptableObject
  {
    [SerializeField]
    private List<BuildingDto> _buildings = new List<BuildingDto>();

    public List<BuildingDto> GetAllBuildings()
    {
      return new List<BuildingDto>(_buildings);
    }

    public BuildingDto GetRandomHouse()
    {
      return new BuildingDto(_buildings[Random.Range(0, _buildings.Count)]);
    }
  }
  [Serializable]
  public class BuildingDto
  {

    public string Name;
    public BuildingType Type;
    public int Price;
    public GameObject Prefab;

    public BuildingDto()
    {}

    public BuildingDto (string name, BuildingType type, int price, GameObject prefab)
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
  public enum BuildingType
  {
    Hotel = 1,
    PizzaPlace = 2,
    GasStation = 3,
    GasStationShop = 33,
    CoffeeHouse = 4,
    Market = 5,
    Bakery = 6,
    Cinema = 7,

    LivingHouse1 = 100,
    LivingHouse2 = 101,
    LivingHouse3 = 102
  }
}