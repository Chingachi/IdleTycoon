using System.Collections.Generic;
using Buildings.Dto;
using UnityEngine;
namespace Buildings
{
  public class BuildingManager : MonoBehaviour
  {
    [SerializeField]
    private List<Placeholder> _placeholders;
    [SerializeField]
    private BuildingsSO _buildingsDatabase;

    private void Awake()
    {
      foreach (Placeholder placeholder in _placeholders) {
        placeholder.OnClick += () => SpawnRandomBuilding(placeholder);
      }
    }

    private void SpawnRandomBuilding (Placeholder placeholder)
    {
      BuildingDto data = _buildingsDatabase.GetRandomHouse();
      Building building = Instantiate(data.Prefab, placeholder.gameObject.transform);
      building.name = data.Name;
      placeholder.AttachBuilding(building);
    }
  }
}