using DI;
using Timers;
using UnityEngine;
namespace Installers
{
  public class TimerInstaller : BaseInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind(CreateTimer(), BindType.Singleton);
    }

    public override void RemoveBindings()
    {}

    private TickTimer CreateTimer()
    {
      GameObject go = new GameObject("TickTimer");
      DontDestroyOnLoad(go);

      return go.AddComponent<TickTimer>();
    }
  }
}