using Core.DI;
using Offline;
namespace Installers.GameScene
{
  public class OfflineIncomeInstaller : BaseInstaller
  {
    public override void InstallBindings()
    {
      Container.CreateAndBind<OfflineIncomeCalculator>(BindType.Transient);
    }
  }
}