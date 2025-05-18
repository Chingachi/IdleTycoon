using System;
using System.Collections.Generic;
using Buildings.BuildingState;
using Buildings.BuildingState.Income;
using Buildings.Dto;
using Core.DI;
using Core.DI.Contexts;
using Core.EventSystemComponents;
using Core.Storages;
using UI.Popups.Buildings.Buy;
using UI.Popups.Buildings.Info.Manager;
using UI.Popups.Buildings.Info.Popup;
using UI.Popups.Buildings.Selection.Manager;
using UI.Popups.Buildings.Selection.Popup;
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

    private Placeholder _selectedPlaceholder;

    private IncomeManager _incomeManager;
    private DecayManager _decayManager;
    private Storage<BuildingsSaveData> _storage;
    private EventManager _eventManager;

    private SelectBuildingPopupManager _selectBuildingPopupManager;
    private BuildingInfoPopupManager _buildingInfoPopupManager;

    private void Awake()
    {
      DiContainer container = ProjectContext.Container;
      container.Bind(this, BindType.Cached);

      foreach (Placeholder placeholder in _placeholders) {
        placeholder.OnClick += go => SelectBuilding(placeholder);
      }
    }

    private void Start()
    {
      InitBindings();
      LoadBuildings();
      _eventManager.SubscribeEvent<BuildingPurchasedEvent>(SpawnSelectedBuilding);
      _incomeManager.Start();
      _decayManager.Start();
    }

    private void OnDestroy()
    {
      _eventManager.UnsubscribeEvent<BuildingPurchasedEvent>(SpawnSelectedBuilding);
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

      _incomeManager = container.Resolve<IncomeManager>();
      _storage = container.Resolve<Storage<BuildingsSaveData>>();
      _decayManager = container.Resolve<DecayManager>();
      _eventManager = container.Resolve<EventManager>();

      _selectBuildingPopupManager = container.Resolve<SelectBuildingPopupManager>();
      _buildingInfoPopupManager = container.Resolve<BuildingInfoPopupManager>();
    }

    private void SelectBuilding (Placeholder placeholder)
    {
      if (_selectedPlaceholder != null) {
        return;
      }

      _selectedPlaceholder = placeholder;
      _selectBuildingPopupManager.OpenPopup(new SelectBuildingPopupData());
    }

    private void SpawnSelectedBuilding (BuildingPurchasedEvent eventData)
    {
      BuildingDto dto = _buildingsDatabase.GetBuildingByNameOrNull(eventData.BuildingDto.Name);

      Building building = InstantiateBuilding(dto, eventData.BuildingDto.Name);
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
      building.OnClick += HandleBuildingClick;

      StatusIndicator indicator = Instantiate(_statusIndicatorPrefab, building.transform);
      building.SetIndicator(indicator);

      return building;
    }

    private void HandleBuildingClick (GameObject go)
    {
      BuildingData data = go.GetComponent<Building>().Data;
      _buildingInfoPopupManager.OpenPopup(new BuildingInfoPopupData(data));
    }
  }
}