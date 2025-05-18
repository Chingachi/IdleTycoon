using System;
using Core.PopupSystem.Components;
using UnityEngine;
using UnityEngine.UI;
namespace UI.Popups.Settings
{
  public class SettingsPopup : BasePopup
  {
    public event Action<float> OnVolumeChange;
    [SerializeField]
    private Slider _musicVolumeSlider;

    private SettingsPopupData _data;

    protected override void Awake()
    {
      base.Awake();
      _musicVolumeSlider.onValueChanged.AddListener(HandleVolumeChange);
    }

    public override void SetData (IPopupData data)
    {
      _data = (SettingsPopupData)data;
      _musicVolumeSlider.SetValueWithoutNotify(_data.Volume);
    }

    private void HandleVolumeChange (float volume)
    {
      OnVolumeChange?.Invoke(volume);
    }
  }
}