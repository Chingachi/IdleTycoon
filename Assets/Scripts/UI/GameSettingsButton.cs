using Core.DI.Contexts;
using UI.Popups.Settings;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
  [RequireComponent(typeof(Button))]
  public class GameSettingsButton : MonoBehaviour
  {
    private SettingsPopupManager _settingsPopupManager;

    private void Start()
    {
      _settingsPopupManager = ProjectContext.Container.Resolve<SettingsPopupManager>();
      GetComponent<Button>().onClick.AddListener(HandleButtonClick);
    }

    private void HandleButtonClick()
    {
      _settingsPopupManager.OpenPopup();
    }
  }
}