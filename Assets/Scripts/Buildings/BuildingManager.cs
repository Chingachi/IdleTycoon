using System;
using System.Collections.Generic;
using Buildings.Dto;
using DI;
using DI.Contexts;
using Popups.Buildings;
using Popups.Buildings.Selection;
using PopupSystem;
using PopupSystem.Components;
using UnityEngine;
namespace Buildings
{
  public class BuildingManager : MonoBehaviour
  {
    [SerializeField]
    private List<Placeholder> _placeholders;
    [SerializeField]
    private BuildingsSO _buildingsDatabase;

    private PopupManager _popupManager;
    private Placeholder _selectedPlaceholder;

    private void Awake()
    {
      InitBindings();
      
      foreach (Placeholder placeholder in _placeholders) {
        placeholder.OnClick += ()=>  SelectBuilding(placeholder);
      }
    }

    private void InitBindings()
    {
      var container = ProjectContext.Instance.Container;
      container.Bind(this, BindType.Cached);
      container.Bind(_buildingsDatabase, BindType.Cached);
      _popupManager = container.Resolve<PopupManager>();
    }

    private void SelectBuilding(Placeholder placeholder)
    {
      _selectedPlaceholder = placeholder;
      SelectBuildingPopupData data = new SelectBuildingPopupData();
      data.Callback += HandlePopup;
      _popupManager.OpenPopup(data);
    }

    private void HandlePopup (BasePopup resultPopup)
    {
      SelectBuildingPopup popup = (SelectBuildingPopup)resultPopup;
      popup.OnBuildingSelection += SpawnRandomBuilding;
    }

    private void SpawnRandomBuilding (string buildingName)
    {
      
      BuildingDto data = _buildingsDatabase.GetBuildingByNameOrNull(buildingName);

      if (data == null) {
        throw new Exception($"No building with name [{buildingName}] in database");
      }

      Building building = Instantiate(data.Prefab, _selectedPlaceholder.gameObject.transform);
      building.name = data.Name;
      _selectedPlaceholder.AttachBuilding(building);
      _selectedPlaceholder = null;
    }
  }
}