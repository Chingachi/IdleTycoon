using Core.DI;
using Core.Storages;
using Session;
namespace Installers
{
  public class MenuInstaller : BaseInstaller
  {
    public override void InstallBindings()
    {
      Container.CreateAndBindTo<Storage<SettingsSaveData>, FileStorage<SettingsSaveData>>(BindType.Transient);
    }
  }
}