using System;
namespace DI.Contexts
{
  public class ProjectContext : BaseContext
  {
    public static ProjectContext Instance
    {
      get;
      set;
    }

    private void Awake()
    {
      if (Instance != null && Instance != this) {
        Destroy(gameObject);

        return;
      }

      Instance = this;
      _container = new DiContainer();
      BindInstallers();
    }
  }
}