using System;
using UnityEngine;
using UnityEngine.UI;
namespace UI.Popups.WelcomeManual
{
  [RequireComponent(typeof(Button))]
  public class TabPanelPair : MonoBehaviour
  {
    public event Action<TabType> OnClick;
    [SerializeField]
    private Image _image;
    [SerializeField]
    private TabType _tabType;
    [SerializeField]
    private GameObject _panel;

    private void Awake()
    {
      GetComponent<Button>().onClick.AddListener(HandleButtonClick);
    }

    public void SetPanelActive (bool state)
    {
      _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, state ? 1 : 0.5f);
      _panel.SetActive(state);
    }

    private void HandleButtonClick()
    {
      OnClick?.Invoke(_tabType);
    }

    public TabType TabType
    {
      get
      {
        return _tabType;
      }
    }
  }

  public enum TabType
  {
    Welcome,
    GettingStarted,
    IncomeAndMaintenance,
    CameraControls,
    OfflineEarnings
  }
}