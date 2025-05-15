using System;
using UnityEngine;
namespace Common
{
  public class ClickableGameObject : MonoBehaviour
  {
    public event Action OnClick;

    protected virtual void OnMouseUpAsButton()
    {
      OnClick?.Invoke();
    }
  }
}