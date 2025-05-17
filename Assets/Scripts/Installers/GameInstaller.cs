using DI;
using IncomeSystem;
using Storages;
using Storages.Base;
using Timers;
namespace Installers
{
  public class GameInstaller : BaseInstaller
  {

    public override void InstallBindings()
    {
      Container.CreateAndBind<TickTimer>(BindType.Cached);
      Container.CreateAndBindTo<Storage<BuildingsSaveData>, FileStorage<BuildingsSaveData>>(BindType.Cached);
      Container.CreateAndBind<IncomeManager>(BindType.Cached);
    }

    public override void RemoveBindings()
    {}
  }
}