using DI;
using Offline;
namespace Installers
{
  public class OfflineIncomeInstaller : BaseInstaller
  {
    public override void InstallBindings()
    {
      Container.CreateAndBind<OfflineIncomeCalculator>(BindType.Transient);
    }
  }
}