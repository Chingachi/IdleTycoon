using System;
using Buildings.Dto;
using PopupSystem.Components;
using UnityEngine;
namespace Popups.Buildings.Selection
{
  public class SelectBuildingPopup : BasePopup
  {
    public event Action<string> OnBuildingSelection; 
    
    [SerializeField]
    private Transform _itemsContainer;
    [SerializeField]
    private SelectBuildingItem _selectionItemPrefab;
    
    public override void SetData (IPopupData data)
    {
      var buildings = _container.Resolve<BuildingsSO>().GetAllBuildings();

      foreach (BuildingDto building in buildings) {
        var item = Instantiate(_selectionItemPrefab, _itemsContainer);
        item.SetData(building);
        item.OnInfoClick += HandleInfoButtonClick;
        item.OnBuildingClick += HandleBuildingClick;
      }
    }

    private void HandleBuildingClick (BuildingDto data)
    {
      OnBuildingSelection?.Invoke(data.Name);
      Close();
    }

    private void HandleInfoButtonClick (BuildingDto data)
    {
      
    }
  }
}