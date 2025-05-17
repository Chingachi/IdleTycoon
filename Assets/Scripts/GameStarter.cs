using DI.Contexts;
using SceneManagement;
using UnityEngine;
public class GameStarter : MonoBehaviour
{
  private void Start()
  {
    ProjectContext.Container.Resolve<SceneController>().ChangeScene("Game");
  }
}