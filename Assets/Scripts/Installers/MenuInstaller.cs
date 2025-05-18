using Core.DI;
using Core.Storages;
using Session;
using UI.Popups.Settings;
namespace Installers
{
  public class MenuInstaller : BaseInstaller
  {
    public override void InstallBindings()
    {
      Container.BindTo<Storage<SettingsSaveData>, FileStorage<SettingsSaveData>>();
      Container.Bind<SettingsPopupManager>();
    }
  }
}