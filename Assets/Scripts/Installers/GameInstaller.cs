using DI;
using Timers;
namespace Installers
{
  public class GameInstaller : BaseInstaller
  {

    public override void InstallBindings()
    {
      Container.CreateAndBind<TickTimer>(BindType.Cached);
    }

    public override void RemoveBindings()
    {
    }
  }
}