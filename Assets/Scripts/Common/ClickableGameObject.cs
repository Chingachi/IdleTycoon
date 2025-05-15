using System;
using UnityEngine;
namespace Common
{
  public class ClickableGameObject : MonoBehaviour
  {
    public event Action OnClick;

    private void OnMouseDown()
    {
      Debug.Log($"Click: {gameObject.name}");
      OnClick?.Invoke();
    }
  }
}