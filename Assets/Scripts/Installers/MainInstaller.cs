using Coroutines;
using DI;
using EventSystemComponents;
using PopupSystem;
using SceneManagement;
namespace Installers
{
  public class MainInstaller : BaseInstaller
  {
    public override void InstallBindings()
    {
      Container.CreateAndBind<Awaiter>(BindType.Singleton);
      Container.CreateAndBind<EventManager>(BindType.Singleton);
      Container.CreateAndBind<SceneController>(BindType.Singleton);
      Container.CreateAndBind<PopupManager>(BindType.Singleton);
    }

    public override void RemoveBindings()
    {}
  }
}