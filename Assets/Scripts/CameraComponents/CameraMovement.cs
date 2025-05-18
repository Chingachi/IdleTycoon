using UnityEngine;
namespace CameraComponents
{
  [RequireComponent(typeof(Camera))]
  public class CameraMovement : MonoBehaviour
  {
    [Header("Zoom")]
    public float zoomSpeed = 400f;
    [Header("Pan")]
    public float panSpeed = 0.2f;
    [Header("Rotate")]
    public float rotationSpeed = 200f;

    public float minY = 10f;
    public float maxY = 35f;
    public Vector2 panLimitX = new Vector2(140, 420f);
    public Vector2 panLimitZ = new Vector2(120f, 180f);

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
      Vector3 move = new Vector3(-delta.x, 0f, -delta.y) * panSpeed;
      transform.Translate(move, Space.World);

      Vector3 clamp = transform.position;
      clamp.x = Mathf.Clamp(clamp.x, panLimitX.x, panLimitX.y);
      clamp.z = Mathf.Clamp(clamp.z, panLimitZ.x, panLimitZ.y);
      transform.position = clamp;

      _lastMousePosition = Input.mousePosition;
    }

    private void HandleRotate()
    {
      if (!Input.GetMouseButton(1)) {
        return;
      }

      float horizontal = Input.GetAxis("Mouse X");
      transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime, Space.World);
    }
  }
}