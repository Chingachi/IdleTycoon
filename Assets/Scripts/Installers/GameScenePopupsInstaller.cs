using DI;
using Popups.Buildings.Buy.Manager;
using Popups.Buildings.Selection.Manager;
namespace Installers
{
  public class GameScenePopupsInstaller : BaseInstaller
  {

    public override void InstallBindings()
    {
      Container.CreateAndBind<BuyBuildingPopupManager>(BindType.Transient);
      Container.CreateAndBind<SelectBuildingPopupManager>(BindType.Transient);
    }
  }
}