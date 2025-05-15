using System.Collections;
using Coroutines;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SceneManagement
{
  public class SceneController
  {
    private readonly CoroutineRunner _coroutineRunner;
    private readonly Awaiter _awaiter;

    private Coroutine _sceneChangingCoroutine;

    public SceneController (CoroutineRunner coroutineRunner, Awaiter awaiter)
    {
      _coroutineRunner = coroutineRunner;
      _awaiter = awaiter;
    }

    public void ChangeScene (string sceneName)
    {
      _sceneChangingCoroutine = _coroutineRunner.Run(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync (string sceneName)
    {
      WaitUntil waitForImportantProcessesToFinish = new WaitUntil(() => !_awaiter.CheckAnyWaiting());

      yield return waitForImportantProcessesToFinish;

      AsyncOperation load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

      while (!load.isDone) {
        yield return null;
      }
    }
  }
}