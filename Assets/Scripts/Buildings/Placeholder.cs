using Common;
using UnityEngine;
namespace Buildings
{
  public class Placeholder : ClickableGameObject
  {
    [SerializeField]
    private Vector2 _size;

    private Building _building;

    protected override void OnMouseUpAsButton()
    {
      if (_building != null) {
        return;
      }

      base.OnMouseUpAsButton();
    }

    public void AttachBuilding (Building building)
    {
      _building = building;
      _building.transform.localPosition = new Vector3(-_size.x/2f, 0, -_size.y/2f);
    }
  }
}