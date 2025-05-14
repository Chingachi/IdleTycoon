using DI;
using EventSystemComponents;
using PopupSystem;
namespace Installers
{
  public class MainInstaller : BaseInstaller
  {
    public override void InstallBindings()
    {
      Container.CreateAndBind<EventManager>(BindType.Singleton);
      Container.CreateAndBind<PopupManager>(BindType.Singleton);
    }

    public override void RemoveBindings()
    {}
  }
}