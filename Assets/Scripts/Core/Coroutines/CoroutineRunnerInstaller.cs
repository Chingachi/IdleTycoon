using Core.DI;
using UnityEngine;
namespace Core.Coroutines
{
  public class CoroutineRunnerInstaller : BaseInstaller
  {

    public override void InstallBindings()
    {
      GameObject go = new GameObject("CoroutineRunner");
      DontDestroyOnLoad(go);
      CoroutineRunner runner = go.AddComponent<CoroutineRunner>();
      Container.Bind(runner, BindType.Singleton);
    }
  }
}