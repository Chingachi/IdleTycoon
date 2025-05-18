using UnityEngine;
namespace CameraComponents
{
  [RequireComponent(typeof(Camera))]
  public class CameraMovement : MonoBehaviour
  {
    public float zoomSpeed = 400f;
    public float panSpeed = 0.2f;
    public float rotationSpeed = 200f;

    public float minY = 10f;
    public float maxY = 25f;
    public Vector2 panLimitX = new Vector2(140, 420f);
    public Vector2 panLimitZ = new Vector2(135f, 175f);

    private Vector3 _lastMousePosition;

    private void Update()
    {
      HandleZoom();
      HandlePan();
      HandleRotate();
    }

    private void HandleZoom()
    {
      float scroll = Input.GetAxis("Mouse ScrollWheel");

      if (!(Mathf.Abs(scroll) > Mathf.Epsilon)) {
        return;
      }

      Vector3 position = transform.position;
      position.y -= scroll * zoomSpeed * Time.deltaTime;
      position.y = Mathf.Clamp(position.y, minY, maxY);
      transform.position = position;
    }

    private void HandlePan()
    {
      if (Input.GetMouseButtonDown(2)) {
        _lastMousePosition = Input.mousePosition;
      }

      if (!Input.GetMouseButton(2)) {
        return;
      }

      Vector3 delta = Input.mousePosition - _lastMousePosition;

      Vector3 right = transform.right;
      right.y = 0;
      right.Normalize();
      Vector3 forward = transform.forward;
      forward.y = 0;
      forward.Normalize();

      Vector3 move = (-right * delta.x - forward * delta.y) * panSpeed;
      Vector3 nextPos = transform.position + move;

      nextPos.x = Mathf.Clamp(nextPos.x, panLimitX.x, panLimitX.y);
      nextPos.z = Mathf.Clamp(nextPos.z, panLimitZ.x, panLimitZ.y);

      transform.position = nextPos;
      _lastMousePosition = Input.mousePosition;
    }

    private void HandleRotate()
    {
      if (!Input.GetMouseButton(1)) {
        return;
      }

      float h = Input.GetAxis("Mouse X");
      transform.Rotate(Vector3.up, h * rotationSpeed * Time.deltaTime, Space.World);
    }
  }
}