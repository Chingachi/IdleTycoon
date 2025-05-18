using Core.Coroutines;
using Core.DI;
using Core.EventSystemComponents;
using Core.PopupSystem;
using Core.Storages;
using Core.Storages.Base;
using SceneManagement;
using Session;
namespace Installers
{
  public class MainInstaller : BaseInstaller
  {
    public override void InstallBindings()
    {
      Container.CreateAndBind<Awaiter>(BindType.Singleton);
      Container.CreateAndBind<EventManager>(BindType.Singleton);
      Container.CreateAndBind<SceneController>(BindType.Singleton);
      Container.CreateAndBind<PopupManager>(BindType.Singleton);
      Container.CreateAndBindTo<Storage<ProfileSaveData>, FileStorage<ProfileSaveData>>(BindType.Transient);
      Container.CreateAndBind<SessionManager>(BindType.Singleton);
    }
  }
}