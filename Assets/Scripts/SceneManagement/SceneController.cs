using System.Collections;
using Coroutines;
using DI;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SceneManagement
{
  public class SceneController
  {
    private readonly CoroutineRunner _coroutineRunner;
    private readonly Awaiter _awaiter;
    private readonly DiContainer _container;

    private Coroutine _sceneChangingCoroutine;

    public SceneController (CoroutineRunner coroutineRunner, Awaiter awaiter, DiContainer container)
    {
      _coroutineRunner = coroutineRunner;
      _awaiter = awaiter;
      _container = container;
    }

    public void ChangeScene (string sceneName)
    {
      _sceneChangingCoroutine = _coroutineRunner.Run(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync (string sceneName)
    {
      WaitUntil waitForImportantProcessesToFinish = new WaitUntil(() => !_awaiter.CheckAnyWaiting());

      yield return waitForImportantProcessesToFinish;

      _container.ClearCache();
      
      AsyncOperation load = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

      while (!load.isDone) {
        yield return null;
      }
    }
  }
}