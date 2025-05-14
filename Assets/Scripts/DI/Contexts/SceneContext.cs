using System;
namespace DI.Contexts
{
  public class SceneContext : BaseContext
  {
    private void Awake()
    {
      _container = ProjectContext.Instance.Container;
      BindInstallers();
    }
  }
}