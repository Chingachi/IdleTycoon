using Buildings.BuildingState;
using Buildings.BuildingState.Income;
using Core.DI;
using Core.Storages;
using Core.Storages.Base;
using Timers;
namespace Installers
{
  public class GameInstaller : BaseInstaller
  {
    public override void InstallBindings()
    {
      Container.CreateAndBind<PointOneSecondTimer>(BindType.Cached);
      Container.CreateAndBindTo<Storage<BuildingsSaveData>, FileStorage<BuildingsSaveData>>(BindType.Cached);
      Container.CreateAndBind<IncomeManager>(BindType.Cached);
      Container.CreateAndBind<DecayManager>(BindType.Cached);

    }
  }
}