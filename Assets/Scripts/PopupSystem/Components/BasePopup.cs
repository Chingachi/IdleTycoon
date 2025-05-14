using System;
using UnityEngine;
using UnityEngine.UI;
namespace PopupSystem.Components
{
  public abstract class BasePopup : MonoBehaviour
  {
    public event Action OnShow;
    public event Action OnClose;
    [SerializeField]
    protected Button _closeButton;

    protected virtual void Awake()
    {
      _closeButton?.onClick.AddListener(Close);
    }

    public abstract void SetData (IPopupData data);

    public virtual void Show()
    {
      OnShow?.Invoke();
    }

    public virtual void Close()
    {
      OnClose?.Invoke();
    }
  }
}