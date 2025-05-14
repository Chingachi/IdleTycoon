using UnityEngine;
namespace DI
{
  public abstract class BaseInstaller : MonoBehaviour
  {
    public abstract void InstallBindings();

    private DiContainer Container { get; set; }

    public void SetContainer (DiContainer container)
    {
      Container = container;
    }
  }
}