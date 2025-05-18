using System;
using PopupSystem.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace UI.Popups.OfflineIncome
{
  public class OfflineIncomePopup : BasePopup
  {
    public event Action<float> OkButtonClick;
    [SerializeField]
    private TMP_Text _textField;
    [SerializeField]
    private Button _okButton;

    private OfflineIncomePopupData _data;

    protected override void Awake()
    {
      base.Awake();
      _okButton.onClick.AddListener(HandleOkClick);
    }

    public override void SetData (IPopupData data)
    {
      _data = (OfflineIncomePopupData)data;

      _textField.text = $"Welcome back! You earned {_data.TotalEarned:0.#}$ while you were away!";
    }

    private void HandleOkClick()
    {
      OkButtonClick?.Invoke(_data.TotalEarned);
    }
  }
}