using System.Collections.Generic;
using UnityEngine;
namespace DI.Contexts
{
  public class BaseContext : MonoBehaviour
  {
    [SerializeField]
    protected List<BaseInstaller> _installers = new List<BaseInstaller>();


    protected DiContainer _container;

    protected void BindInstallers()
    {
      foreach (BaseInstaller installer in _installers) {
        installer.SetContainer(_container);
        installer.InstallBindings();
      }
    }
  }
}