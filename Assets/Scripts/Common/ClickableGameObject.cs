using System;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Common
{
  public class ClickableGameObject : MonoBehaviour
  {
    public event Action<GameObject> OnClick;
    private EventSystem _eventSystem;

    private void Start()
    {
      _eventSystem = EventSystem.current;
    }

    protected virtual void OnMouseUpAsButton()
    {
      if (_eventSystem.IsPointerOverGameObject()) {
        return;
      }

      OnClick?.Invoke(gameObject);
    }
  }
}