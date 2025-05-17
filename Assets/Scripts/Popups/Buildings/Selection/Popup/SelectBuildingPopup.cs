using System;
using System.Collections.Generic;
using Buildings.Dto;
using PopupSystem.Components;
using UnityEngine;
namespace Popups.Buildings.Selection.Popup
{
  public class SelectBuildingPopup : BasePopup
  {
    public event Action<BuildingDto> OnBuildingClick;

    [SerializeField]
    private Transform _itemsContainer;
    [SerializeField]
    private SelectBuildingItem _selectionItemPrefab;

    public override void SetData (IPopupData data)
    {
      List<BuildingDto> buildings = _container.Resolve<BuildingsSO>().GetAllBuildings();

      foreach (BuildingDto building in buildings) {
        SelectBuildingItem item = Instantiate(_selectionItemPrefab, _itemsContainer);
        item.SetData(building);
        item.OnBuildingClick += HandleBuildingClick;
      }
    }

    private void HandleBuildingClick (BuildingDto dto)
    {
      OnBuildingClick?.Invoke(dto);
    }
  }
}