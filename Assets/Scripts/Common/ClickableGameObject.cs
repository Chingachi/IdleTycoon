using System;
using DI.Contexts;
using PopupSystem;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Common
{
  public class ClickableGameObject : MonoBehaviour
  {
    public event Action<GameObject> OnClick;
    private EventSystem _eventSystem;
    private PopupManager _popupManager;

    private void Start()
    {
      _eventSystem = EventSystem.current;
      _popupManager = ProjectContext.Container.Resolve<PopupManager>();
    }

    protected virtual void OnMouseUpAsButton()
    {
      if (_popupManager.AnyPopupOpened || _eventSystem.IsPointerOverGameObject()) {
        return;
      }

      OnClick?.Invoke(gameObject);
    }
  }
}