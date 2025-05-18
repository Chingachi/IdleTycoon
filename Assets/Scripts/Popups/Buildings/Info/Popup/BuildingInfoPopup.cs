using System;
using Buildings;
using PopupSystem.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace Popups.Buildings.Info.Popup
{
  public class BuildingInfoPopup : BasePopup
  {
    public event Action OnRepair;
    public event Action OnUpgrade;

    [SerializeField]
    private Button _repairButton;
    [SerializeField]
    private Button _upgradeButton;
    [SerializeField]
    private Slider _durabilitySlider;
    [SerializeField, Header("Text fields")]
    private TMP_Text _houseNameField;
    [SerializeField]
    private TMP_Text _levelField;
    [SerializeField]
    private TMP_Text _incomeField;
    [SerializeField]
    private TMP_Text _incomePeriodField;
    [SerializeField]
    private TMP_Text _durabilityField;
    [SerializeField]
    private TMP_Text _repairPriceField;
    [SerializeField]
    private TMP_Text _upgradePriceField;

    private BuildingInfoPopupData _data;

    protected override void Awake()
    {
      base.Awake();
      _repairButton.onClick.AddListener(HandleRepairButtonClick);
      _upgradeButton.onClick.AddListener(HandleUpgradeButtonClick);
    }

    public override void SetData (IPopupData data)
    {
      _data = (BuildingInfoPopupData)data;
      UpdateFields();
    }

    public void UpdateFields()
    {
      BuildingData data = _data.BuildingData;
      _houseNameField.text = data.Name;
      _levelField.text = $"Lvl. {data.CurrentLevel}";
      _incomeField.text = $"Current income is {data.GetCurrentIncome():0.##}$ -> {data.GetCurrentIncome(true):0.##}$";
      _incomePeriodField.text = $"Income period is every {data.GetCurrentIncomeTime():0.#} -> {data.GetCurrentIncomeTime(true):0.#} seconds";
      _durabilityField.text = $"{data.CurrentDurability * 100:0.}/100";
      _repairPriceField.text = $"{data.GetRepairCost():0.}$";
      _upgradePriceField.text = $"{data.GetCurrentUpgradePrice()}$";

      _repairButton.gameObject.SetActive(data.CurrentDurability < 1);
      _durabilitySlider.value = data.CurrentDurability;
    }

    public void SetUpgradeButtonStatus (bool status)
    {
      _upgradeButton.interactable = status;
    }

    private void HandleUpgradeButtonClick()
    {
      OnUpgrade?.Invoke();
    }

    private void HandleRepairButtonClick()
    {
      OnRepair?.Invoke();
    }
  }
}