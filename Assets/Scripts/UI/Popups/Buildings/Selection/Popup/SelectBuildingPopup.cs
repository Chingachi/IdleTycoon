using System;
using System.Collections.Generic;
using Buildings.Dto;
using Core.PopupSystem.Components;
using UnityEngine;
namespace UI.Popups.Buildings.Selection.Popup
{
  public class SelectBuildingPopup : BasePopup
  {

    private readonly List<SelectBuildingItem> _items = new List<SelectBuildingItem>();
    public event Action<BuildingDto> OnBuildingClick;

    [SerializeField]
    private Transform _itemsContainer;
    [SerializeField]
    private SelectBuildingItem _selectionItemPrefab;

    private SelectBuildingPopupData _data;

    public override void SetData (IPopupData data)
    {
      _data = (SelectBuildingPopupData)data;

      foreach (BuildingDto building in _data.Buildings) {
        SelectBuildingItem item = Instantiate(_selectionItemPrefab, _itemsContainer);
        item.SetData(building, _data.CurrentBalance);
        item.OnBuildingClick += HandleBuildingClick;
        _items.Add(item);
      }
    }

    public void UpdateAvailableToBuy (int currentBalance)
    {
      foreach (SelectBuildingItem item in _items) {
        item.SetAvailable(currentBalance);
      }
    }

    private void HandleBuildingClick (BuildingDto dto)
    {
      OnBuildingClick?.Invoke(dto);
    }
  }
}