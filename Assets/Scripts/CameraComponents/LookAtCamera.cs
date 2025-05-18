using Core.DI.Contexts;
using UnityEngine;
namespace CameraComponents
{
  public class LookAtCamera : MonoBehaviour
  {
    private Transform _objectToLookAt;

    private void Update()
    {
      TryToGetObject();

      if (_objectToLookAt == null) {
        return;
      }

      transform.LookAt(_objectToLookAt);
      transform.Rotate(new Vector3(0, 180, 0));
    }

    private void TryToGetObject()
    {
      if (_objectToLookAt != null) {
        return;
      }


      _objectToLookAt = ProjectContext.Container.Resolve<ObjectToLookAt>().transform;
    }
  }
}