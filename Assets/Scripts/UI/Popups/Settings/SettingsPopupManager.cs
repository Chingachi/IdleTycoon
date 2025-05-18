using Core.EventSystemComponents;
using Core.PopupSystem;
using Core.Storages;
using Music;
using Session;
namespace UI.Popups.Settings
{
  public class SettingsPopupManager : BasePopupViewManager<SettingsPopupData, SettingsPopup>
  {
    private readonly MusicController _musicController;
    private readonly Storage<SettingsSaveData> _storage;

    public SettingsPopupManager (PopupManager popupManager, EventManager eventManager, MusicController musicController, Storage<SettingsSaveData> storage)
      : base(popupManager, eventManager)
    {
      _musicController = musicController;
      _storage = storage;
    }

    protected override void HandleLoadedPopup()
    {
      _popup.OnVolumeChange += HandleVolumeChange;
    }

    public void OpenPopup()
    {
      _data = new SettingsPopupData(_storage.Data.MusicVolume);
      _data.Callback += HandleLoadedPopup;
      _popupManager.OpenForcePopup(_data);
    }

    private void HandleVolumeChange (float volume)
    {
      _musicController.ChangeVolume(volume);
      _storage.Data.MusicVolume = volume;
      _storage.UpdateData();
    }
  }
}