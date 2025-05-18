using Common;
using Core.DI.Contexts;
using Core.EventSystemComponents;
using UnityEngine;
namespace Buildings.PlaceholderComponents
{
  public class Placeholder : ClickableGameObject
  {
    [SerializeField]
    private Vector2 _size;
    [SerializeField]
    private GameObject _pointer;
    [SerializeField]
    private float _rotationSpeed = 20;

    private Building _building;

    private void OnEnable()
    {
      ProjectContext.Container.Resolve<EventManager>().SubscribeEvent<ShowPlaceholdersEvent>(HandlePointersShowStateChange);
    }

    private void Update()
    {
      RotatePointer();
    }

    private void OnDisable()
    {
      ProjectContext.Container.Resolve<EventManager>().UnsubscribeEvent<ShowPlaceholdersEvent>(HandlePointersShowStateChange);
    }

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
      _building.transform.SetParent(transform);
      _building.transform.localPosition = new Vector3(-_size.x / 2f, 0, -_size.y / 2f);
      _building.transform.localRotation = Quaternion.identity;
      _pointer.SetActive(false);
    }

    private void HandlePointersShowStateChange (ShowPlaceholdersEvent eventData)
    {
      if (_building == null) {
        _pointer.SetActive(eventData.IsShow);
      }
    }

    private void RotatePointer()
    {
      if (!_pointer.activeSelf) {
        return;
      }

      _pointer.transform.Rotate(new Vector3(0, _rotationSpeed, 0) * Time.deltaTime);
    }
  }
}