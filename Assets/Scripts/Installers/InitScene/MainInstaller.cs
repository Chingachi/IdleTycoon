using Core.Coroutines;
using Core.DI;
using Core.EventSystemComponents;
using Core.PopupSystem;
using Core.Storages;
using SavingData;
using SceneManagement;
using Session;
using UI.Popups.SceneLoader;
namespace Installers.InitScene
{
  public class MainInstaller : BaseInstaller
  {
    public override void InstallBindings()
    {
      Container.CreateAndBind<Awaiter>(BindType.Singleton);
      Container.CreateAndBind<EventManager>(BindType.Singleton);
      Container.CreateAndBind<PopupManager>(BindType.Singleton);

      Container.BindTo<Storage<ProfileSaveData>, FileStorage<ProfileSaveData>>();
      Container.CreateAndBind<SessionManager>(BindType.Singleton);

      Container.Bind<SceneLoaderPopupManager>();
      Container.CreateAndBind<SceneController>(BindType.Singleton);
    }
  }
}