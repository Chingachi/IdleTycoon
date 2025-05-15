using DI.Contexts;
using SceneManagement;
using UnityEngine;
public class GameStarter : MonoBehaviour
{
  private void Start()
  {
    ProjectContext.Instance.Container.Resolve<SceneController>().ChangeScene("Game");
  }
}