using Core.DI.Contexts;
using SceneManagement;
using UnityEngine;
using UnityEngine.UI;
namespace UI.SingleButtons
{
  [RequireComponent(typeof(Button))]
  public class BackToMenuButton : MonoBehaviour
  {
    private void Awake()
    {
      GetComponent<Button>().onClick.AddListener(HandleBackToMenu);
    }

    private void HandleBackToMenu()
    {
      ProjectContext.Container.Resolve<SceneController>().ChangeScene(SceneType.Menu);
    }
  }
}