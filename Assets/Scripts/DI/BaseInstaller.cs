using UnityEngine;
namespace DI
{
  public abstract class BaseInstaller : MonoBehaviour
  {
    public abstract void InstallBindings();

    public void SetContainer (DiContainer container)
    {
      Container = container;
    }

    protected DiContainer Container { get; set; }
  }
}