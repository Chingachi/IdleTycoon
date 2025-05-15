using System;
using Buildings.Dto;
using UnityEngine;
using UnityEngine.UI;
namespace Popups.Buildings.Selection
{
  public class SelectBuildingItem : MonoBehaviour
  {
    public event Action<BuildingDto> OnBuildingClick;  
    public event Action<BuildingDto> OnInfoClick;  
    
    [SerializeField]
    private Image _preview;
    [SerializeField]
    private Button _infoButton;
    [SerializeField]
    private Button _itemButton;

    private BuildingDto _data;
    private void Awake()
    {
      _infoButton.onClick.AddListener(ShowInfo);
      _itemButton.onClick.AddListener(OnItemClick);
    }

    public void SetData (BuildingDto data)
    {
      _data = data;
      _preview.sprite = data.Preview;
    }
    
    private void OnItemClick()
    {
      OnBuildingClick?.Invoke(_data);
    }

    private void ShowInfo()
    {
      OnInfoClick?.Invoke(_data);
    }
  }
}