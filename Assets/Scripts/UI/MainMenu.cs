using Core.DI.Contexts;
using SceneManagement;
using UI.Popups.Settings;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
  public class MainMenu : MonoBehaviour
  {
    [SerializeField]
    private Button _playButton;
    [SerializeField]
    private Button _settingsButton;
    [SerializeField]
    private Button _exitButton;

    private void Awake()
    {
      _playButton.onClick.AddListener(HandlePlayClick);
      _settingsButton.onClick.AddListener(HandleSettingsClick);
      _exitButton.onClick.AddListener(HandleExitClick);
    }

    private void HandlePlayClick()
    {
      ProjectContext.Container.Resolve<SceneController>().ChangeScene(SceneType.Game);
    }

    private void HandleSettingsClick()
    {
      ProjectContext.Container.Resolve<SettingsPopupManager>().OpenPopup();
    }

    private void HandleExitClick()
    {
      Application.Quit();
    }
  }
}