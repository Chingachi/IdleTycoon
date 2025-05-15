using Common;
using UnityEngine;
namespace Buildings
{
  public class Placeholder : ClickableGameObject
  {
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
      _building.transform.position = Vector3.zero;
      building.transform.localPosition = Vector3.zero;
    }
  }
}