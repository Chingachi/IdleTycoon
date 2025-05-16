using System;
using UnityEngine;
namespace Common
{
  public class LookAtCamera : MonoBehaviour
  {
    private Transform _camera;

    private void Start()
    {
      _camera = Camera.main.transform;
    }

    private void Update()
    {
      if (_camera == null) {
        return;
      }
      
      transform.LookAt(_camera);
      transform.Rotate(new Vector3(0, 180, 0));
    }
  }
}