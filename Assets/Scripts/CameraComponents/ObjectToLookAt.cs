using DI;
using DI.Contexts;
using UnityEngine;
namespace CameraComponents
{
  public class ObjectToLookAt : MonoBehaviour
  {
    private void Start()
    {
      ProjectContext.Container.Bind(this, BindType.Cached);
    }
  }
}