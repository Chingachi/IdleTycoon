using Core.DI;
using UI.Popups.WelcomeManual;
namespace Installers.GameScene
{
  public class WelcomeInstaller : BaseInstaller
  {
    public override void InstallBindings()
    {
      Container.CreateAndBind<WelcomeManualPopupManager>(BindType.Cached);
    }
  }
}