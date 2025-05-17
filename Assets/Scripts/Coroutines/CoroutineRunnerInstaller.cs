using DI;
using UnityEngine;
namespace Coroutines
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