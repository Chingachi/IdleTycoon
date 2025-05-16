using System;
using System.Collections.Generic;
using Buildings.Dto;
using DI;
using DI.Contexts;
using IncomeSystem;
using Popups.Buildings;
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
    private Storage<BuildingsStorage> _storage;

    private void Awake()
    {
      
      foreach (Placeholder placeholder in _placeholders) {
        placeholder.OnClick += ()=>  SelectBuilding(placeholder);
      }
    }

    private void Start()
    {
      InitBindings();
      LoadBuildings();
      _incomeManager.StartCounting();
    }

    private void LoadBuildings()
    {
      foreach (BuildingData data in _storage.Data.buildingsData) {
        SpawnLoadedBuilding(data);
      }
    }

    private void InitBindings()
    {
      var container = ProjectContext.Instance.Container;
      container.Bind(this, BindType.Cached);
      container.Bind(_buildingsDatabase, BindType.Cached);
      _popupManager = container.Resolve<PopupManager>();
      _incomeManager = container.Resolve<IncomeManager>();
      _storage = container.Resolve<Storage<BuildingsStorage>>();
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
      popup.OnBuildingSelection += SpawnSelectedBuilding;
    }

    private void SpawnSelectedBuilding (string buildingName)
    {
      BuildingDto dto = _buildingsDatabase.GetBuildingByNameOrNull(buildingName);
      
      var building = InstantiateBuilding(dto, buildingName); 
      _selectedPlaceholder.AttachBuilding(building);

      var buildingData = new BuildingData(dto)
      {
        PlaceholderIndex = _selectedPlaceholder.transform.GetSiblingIndex()
      };

      building.SetData(buildingData);
      _incomeManager.RegisterBuilding(building);
      
      _selectedPlaceholder = null;
    }

    private void SpawnLoadedBuilding (BuildingData data)
    {
      BuildingDto dto = _buildingsDatabase.GetBuildingByNameOrNull(data.Name);
      
      var building = InstantiateBuilding(dto, data.Name); 
      
      if (_placeholders.Count == 0) {
        throw new Exception("No placeholders found");
      }
      
      var placeholdersParent = _placeholders[0].transform.parent;

      var placeholder = placeholdersParent.GetChild(data.PlaceholderIndex).GetComponent<Placeholder>();
      placeholder.AttachBuilding(building);
      building.SetData(data);
      _incomeManager.RegisterBuilding(building);
    }

    private Building InstantiateBuilding (BuildingDto data, string buildingName)
    {
      if (data == null) {
        throw new Exception($"No building with name [{buildingName}] in database");
      }

      Building building = Instantiate(data.Prefab);
      building.name = data.Name;
      
      var indicator = Instantiate(_statusIndicatorPrefab, building.transform);
      building.SetIndicator(indicator);

      return building;
    }
  }
}