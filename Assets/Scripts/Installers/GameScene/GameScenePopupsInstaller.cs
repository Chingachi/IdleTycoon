using Core.DI;
using UI.Popups.Buildings.Buy.Manager;
using UI.Popups.Buildings.Info.Manager;
using UI.Popups.Buildings.Selection.Manager;
using UI.Popups.OfflineIncome;
namespace Installers.GameScene
{
  public class GameScenePopupsInstaller : BaseInstaller
  {

    public override void InstallBindings()
    {
      Container.CreateAndBind<BuyBuildingPopupManager>(BindType.Transient);
      Container.CreateAndBind<SelectBuildingPopupManager>(BindType.Transient);
      Container.CreateAndBind<BuildingInfoPopupManager>(BindType.Transient);
      Container.CreateAndBind<OfflineIncomePopupManager>(BindType.Transient);
    }
  }
}