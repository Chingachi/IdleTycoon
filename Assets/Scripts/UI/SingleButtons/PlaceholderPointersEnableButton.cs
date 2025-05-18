using Buildings.PlaceholderComponents;
using Core.DI.Contexts;
using Core.EventSystemComponents;
using Core.Storages;
using Session;
using UnityEngine;
using UnityEngine.UI;
namespace UI.SingleButtons
{
  public class PlaceholderPointersEnableButton : MonoBehaviour
  {
    [SerializeField]
    private Image _image;

    private void Awake()
    {
      GetComponent<Button>().onClick.AddListener(HandleBackToMenu);
    }

    private void Start()
    {
      Storage<SettingsSaveData> storage = ProjectContext.Container.Resolve<Storage<SettingsSaveData>>();
      ChangeImage(storage.Data.PointersActiveState);
      ProjectContext.Container.Resolve<EventManager>().Fire(new ShowPlaceholdersEvent(storage.Data.PointersActiveState));
    }

    private void HandleBackToMenu()
    {
      Storage<SettingsSaveData> storage = ProjectContext.Container.Resolve<Storage<SettingsSaveData>>();
      storage.Data.PointersActiveState = !storage.Data.PointersActiveState;
      storage.UpdateData();
      ChangeImage(storage.Data.PointersActiveState);
      ProjectContext.Container.Resolve<EventManager>().Fire(new ShowPlaceholdersEvent(storage.Data.PointersActiveState));
    }

    private void ChangeImage (bool active)
    {
      _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, active ? 1 : 0.5f);
    }
  }
}