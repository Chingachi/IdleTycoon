using Core.DI.Contexts;
using UnityEngine;
using UnityEngine.UI;
namespace UI.Popups.WelcomeManual
{
  [RequireComponent(typeof(Button))]
  public class WelcomeManualButton : MonoBehaviour
  {

    private WelcomeManualPopupManager _settingsPopupManager;

    private void Start()
    {
      _settingsPopupManager = ProjectContext.Container.Resolve<WelcomeManualPopupManager>();
      GetComponent<Button>().onClick.AddListener(HandleButtonClick);
    }

    private void HandleButtonClick()
    {
      _settingsPopupManager.OpenPopup();
    }
  }
}