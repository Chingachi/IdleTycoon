using System.Collections.Generic;
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
      return new List<BuildingDto>(_buildings);
    }

    public BuildingDto GetRandomHouse()
    {
      return new BuildingDto(_buildings[Random.Range(0, _buildings.Count)]);
    }
  }
}