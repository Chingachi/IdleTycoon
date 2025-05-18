using System.Collections;
using Core.Coroutines;
using Core.DI;
using UI.Popups.SceneLoader;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SceneManagement
{
  public class SceneController
  {
    private readonly CoroutineRunner _coroutineRunner;
    private readonly Awaiter _awaiter;
    private readonly DiContainer _container;
    private readonly SceneDatabase _database;
    private readonly SceneLoaderPopupManager _sceneLoaderPopupManager;

    private Coroutine _sceneChangingCoroutine;

    public SceneController (CoroutineRunner coroutineRunner, Awaiter awaiter, DiContainer container, SceneDatabase database, SceneLoaderPopupManager sceneLoaderPopupManager)
    {
      _coroutineRunner = coroutineRunner;
      _awaiter = awaiter;
      _container = container;
      _database = database;
      _sceneLoaderPopupManager = sceneLoaderPopupManager;
    }

    public void ChangeScene (SceneType scene)
    {
      _sceneChangingCoroutine = _coroutineRunner.Run(LoadSceneAsync(scene));
    }

    private IEnumerator LoadSceneAsync (SceneType scene)
    {
      _sceneLoaderPopupManager.OpenPopup(new SceneLoaderPopupData());
      WaitUntil waitForImportantProcessesToFinish = new WaitUntil(() => !_awaiter.CheckAnyWaiting());

      yield return waitForImportantProcessesToFinish;

      _container.ClearCache();

      AsyncOperation load = SceneManager.LoadSceneAsync(_database.GetScene(scene), LoadSceneMode.Single);

      while (!load.isDone) {
        yield return null;
      }

      _sceneLoaderPopupManager.ClosePopup();
    }
  }
}