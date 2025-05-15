using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace Buildings.Dto
{
  [CreateAssetMenu(fileName = "Buildings", menuName = "ScriptableObjects/Buildings", order = 2)]
  public class BuildingsSO : ScriptableObject
  {
    [SerializeField]
    private List<BuildingDto> _buildings = new List<BuildingDto>();

    public List<BuildingDto> GetAllBuildings()
    {
      return _buildings;
    }

    public BuildingDto GetRandomHouse()
    {
      return _buildings[Random.Range(0, _buildings.Count)];
    }

    public BuildingDto GetBuildingByNameOrNull(string buildingName)
    {
      return _buildings.FirstOrDefault(x => x.Name == buildingName);
    }
  }
}