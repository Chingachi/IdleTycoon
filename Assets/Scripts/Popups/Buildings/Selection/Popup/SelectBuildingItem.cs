using System;
using Buildings.Dto;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Popups.Buildings.Selection.Popup
{
  public class SelectBuildingItem : MonoBehaviour
  {
    public event Action<BuildingDto> OnBuildingClick;

    [SerializeField]
    private Image _preview;
    [SerializeField]
    private Button _itemButton;
    [SerializeField]
    private TMP_Text _priceField;

    private BuildingDto _data;

    private void Awake()
    {
      _itemButton.onClick.AddListener(OnItemClick);
    }

    public void SetData (BuildingDto data)
    {
      _data = data;
      _preview.sprite = data.Preview;
      _priceField.text = $"${data.Price}";
    }

    private void OnItemClick()
    {
      OnBuildingClick?.Invoke(_data);
    }
  }
}