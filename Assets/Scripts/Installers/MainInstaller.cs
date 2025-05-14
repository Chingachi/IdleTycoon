using DI;
using EventSystemComponents;
namespace Installers
{
  public class MainInstaller : BaseInstaller
  {
    public override void InstallBindings()
    {
      Container.Bind<EventManager>(BindType.Singleton);
    }
  }
}