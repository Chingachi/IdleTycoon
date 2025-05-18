using System;
using Buildings.Dto;
using Core.PopupSystem.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.Popups.Buildings.Buy.Popup
{
  public class BuyBuildingPopup : BasePopup
  {
    public event Action<BuildingDto> OnBuy;
    [SerializeField]
    private Button _buyButton;
    [SerializeField]
    private Image _preview;
    [SerializeField]
    private TMP_Text _nameField;
    [SerializeField]
    private TMP_Text _descriptionField;
    [SerializeField]
    private TMP_Text _priceField;
    [SerializeField]
    private TMP_Text _incomeField;
    [SerializeField]
    private TMP_Text _incomePeriodField;
    [SerializeField]
    private TMP_Text _decayFactorField;

    private BuyBuildingPopupData _data;

    protected override void Awake()
    {
      base.Awake();
      _buyButton.onClick.AddListener(HandleBuy);
    }

    public override void SetData (IPopupData data)
    {
      _data = (BuyBuildingPopupData)data;

      _preview.sprite = _data.BuildingDto.Preview;
      _nameField.text = _data.BuildingDto.Name;
      _descriptionField.text = _data.BuildingDto.Description;
      _priceField.text = $"Buy {_data.BuildingDto.Price:0.} $";
      _incomeField.text = $"Income: {_data.BuildingDto.BaseIncome:0.#}";
      _incomePeriodField.text = $"Receive income every {_data.BuildingDto.BaseIncomePeriod:0.#} seconds";
      _decayFactorField.text = $"Decay: {_data.BuildingDto.BaseDecayCoefficient:0.#} per minute";
    }

    public void SetAvailableToBuy (float currentBalance)
    {
      _buyButton.interactable = _data.BuildingDto.Price <= currentBalance;
    }

    private void HandleBuy()
    {
      OnBuy?.Invoke(_data.BuildingDto);
    }
  }
}