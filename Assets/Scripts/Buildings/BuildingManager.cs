using System;
using System.Collections.Generic;
using Buildings.BuildingState;
using Buildings.BuildingState.Income;
using Buildings.Dto;
using DI;
using DI.Contexts;
using Popups.Buildings.Selection;
using PopupSystem;
using PopupSystem.Components;
using Storages;
using Storages.Base;
using UnityEngine;
namespace Buildings
{
  public class BuildingManager : MonoBehaviour
  {
    [SerializeField]
    private List<Placeholder> _placeholders;
    [SerializeField]
    private BuildingsSO _buildingsDatabase;
    [SerializeField]
    private StatusIndicator _statusIndicatorPrefab;

    private PopupManager _popupManager;
    private Placeholder _selectedPlaceholder;
    private IncomeManager _incomeManager;
    private DecayManager _decayManager;
    private Storage<BuildingsSaveData> _storage;

    private void Awake()
    {

      foreach (Placeholder placeholder in _placeholders) {
        placeholder.OnClick += () => SelectBuilding(placeholder);
      }
    }

    private void Start()
    {
      InitBindings();
      LoadBuildings();
      _incomeManager.Start();
      _decayManager.Start();
    }

    private void LoadBuildings()
    {
      foreach (BuildingData data in _storage.Data.buildingsData) {
        SpawnLoadedBuilding(data);
      }
    }

    private void InitBindings()
    {
      DiContainer container = ProjectContext.Container;
      container.Bind(this, BindType.Cached);
      container.Bind(_buildingsDatabase, BindType.Cached);

      _popupManager = container.Resolve<PopupManager>();
      _incomeManager = container.Resolve<IncomeManager>();
      _storage = container.Resolve<Storage<BuildingsSaveData>>();
      _decayManager = container.Resolve<DecayManager>();
    }

    private void SelectBuilding (Placeholder placeholder)
    {
      _selectedPlaceholder = placeholder;
      SelectBuildingPopupData data = new SelectBuildingPopupData();
      data.Callback += HandlePopup;
      _popupManager.OpenPopup(data);
    }

    private void HandlePopup (BasePopup resultPopup)
    {
      SelectBuildingPopup popup = (SelectBuildingPopup)resultPopup;
      popup.OnBuildingSelection += SpawnSelectedBuilding;
    }

    private void SpawnSelectedBuilding (string buildingName)
    {
      BuildingDto dto = _buildingsDatabase.GetBuildingByNameOrNull(buildingName);

      Building building = InstantiateBuilding(dto, buildingName);
      _selectedPlaceholder.AttachBuilding(building);

      BuildingData buildingData = new BuildingData(dto)
      {
        PlaceholderIndex = _selectedPlaceholder.transform.GetSiblingIndex()
      };

      building.SetData(buildingData);

      _incomeManager.RegisterBuilding(building);
      _decayManager.RegisterBuilding(building);

      _selectedPlaceholder = null;
    }

    private void SpawnLoadedBuilding (BuildingData data)
    {
      BuildingDto dto = _buildingsDatabase.GetBuildingByNameOrNull(data.Name);

      Building building = InstantiateBuilding(dto, data.Name);

      if (_placeholders.Count == 0) {
        throw new Exception("No placeholders found");
      }

      Transform placeholdersParent = _placeholders[0].transform.parent;

      Placeholder placeholder = placeholdersParent.GetChild(data.PlaceholderIndex).GetComponent<Placeholder>();
      placeholder.AttachBuilding(building);

      building.SetData(data);

      _incomeManager.RegisterBuilding(building);
      _decayManager.RegisterBuilding(building);
    }

    private Building InstantiateBuilding (BuildingDto data, string buildingName)
    {
      if (data == null) {
        throw new Exception($"No building with name [{buildingName}] in database");
      }

      Building building = Instantiate(data.Prefab);
      building.name = data.Name;

      StatusIndicator indicator = Instantiate(_statusIndicatorPrefab, building.transform);
      building.SetIndicator(indicator);

      return building;
    }
  }
}