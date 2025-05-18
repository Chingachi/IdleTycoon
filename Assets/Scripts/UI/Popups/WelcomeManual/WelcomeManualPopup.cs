using System.Collections.Generic;
using Core.PopupSystem.Components;
using UnityEngine;
namespace UI.Popups.WelcomeManual
{
  public class WelcomeManualPopup : BasePopup
  {
    [SerializeField]
    private List<TabPanelPair> _tabPanels;

    public override void SetData (IPopupData data)
    {
      InitTabs();
    }

    private void InitTabs()
    {
      foreach (TabPanelPair tabPanel in _tabPanels) {
        tabPanel.OnClick += HandleTapClick;
      }

      HandleTapClick(TabType.Welcome);
    }

    private void HandleTapClick (TabType tabType)
    {
      foreach (TabPanelPair tabPanel in _tabPanels) {
        tabPanel.SetPanelActive(tabPanel.TabType == tabType);
      }
    }
  }
}