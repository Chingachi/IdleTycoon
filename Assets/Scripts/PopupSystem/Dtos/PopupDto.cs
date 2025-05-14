using System;
using System.Collections.Generic;
using System.Linq;
using PopupSystem.Components;
using UnityEngine;
namespace PopupSystem.Dtos
{
  [Serializable]
  public class PopupDto
  {
    [SerializeField]
    private string _groupName;
    [SerializeField]
    private List<BasePopup> _popups = new List<BasePopup>();



    public BasePopup GetPopupInstance (Type dataPopupType)
    {
      return _popups.FirstOrDefault(x => x.GetType() == dataPopupType);
    }
  }
}