using UnityEngine;
using UnityEngine.UI;
namespace Buildings
{
  public class StatusIndicator : MonoBehaviour
  {
    [SerializeField]
    private Slider _revenueSlider;
    [SerializeField]
    private Slider _decaySlider;

    public void UpdateStatus (float revenue, float decay)
    {
      _revenueSlider.value = revenue;
      _decaySlider.value = decay;
    }
  }
}