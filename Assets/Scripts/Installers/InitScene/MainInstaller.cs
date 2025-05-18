using Core.Coroutines;
using Core.DI;
using Core.EventSystemComponents;
using Core.PopupSystem;
using Core.Storages;
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

      Container.CreateAndBindTo<Storage<ProfileSaveData>, FileStorage<ProfileSaveData>>(BindType.Transient);
      Container.CreateAndBind<SessionManager>(BindType.Singleton);

      Container.CreateAndBind<SceneLoaderPopupManager>(BindType.Transient);
      Container.CreateAndBind<SceneController>(BindType.Singleton);
    }
  }
}