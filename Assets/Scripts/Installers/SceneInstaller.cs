using Core.DI;
using SceneManagement;
using UnityEngine;
namespace Installers
{
  public class SceneInstaller : BaseInstaller
  {
    [SerializeField]
    private SceneDatabase _sceneDatabase;

    public override void InstallBindings()
    {
      Container.Bind(_sceneDatabase, BindType.Singleton);
    }
  }
}