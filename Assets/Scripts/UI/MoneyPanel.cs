using System.Collections;
using System.Globalization;
using System.Text;
using Core.DI.Contexts;
using Core.EventSystemComponents;
using Core.Pool;
using Core.Storages;
using SavingData;
using Session;
using TMPro;
using UnityEngine;
namespace UI
{
  public class MoneyPanel : MonoBehaviour
  {
    [SerializeField]
    private TMP_Text _moneyField;
    [SerializeField]
    private Color _incomeColor;
    [SerializeField]
    private Color _outcomeColor;
    [SerializeField]
    private float _animationDuration;

    private EventManager _eventManager;
    private SimpleMonoObjectPool<TMP_Text> _pool;

    private void Awake()
    {
      _eventManager = ProjectContext.Container.Resolve<EventManager>();
      _eventManager.SubscribeEvent<BalanceChangeEvent>(HandleBalanceChange);
      _moneyField.text = $"${ProjectContext.Container.Resolve<Storage<ProfileSaveData>>().Data.Money}";
      _pool = new SimpleMonoObjectPool<TMP_Text>(_moneyField, transform);
    }

    private void OnDestroy()
    {
      _eventManager.UnsubscribeEvent<BalanceChangeEvent>(HandleBalanceChange);
    }

    private void HandleBalanceChange (BalanceChangeEvent eventData)
    {
      _moneyField.text = eventData.CurrentBalance.ToString(CultureInfo.InvariantCulture);

      TMP_Text animatedField = _pool.Get();

      animatedField.transform.localPosition = _moneyField.transform.localPosition;
      animatedField.color = eventData.Type == BalanceChangeEvent.BalanceChangeType.Income ? _incomeColor : _outcomeColor;

      StringBuilder sb = new StringBuilder();
      sb.Append(eventData.Type == BalanceChangeEvent.BalanceChangeType.Income ? "+" : "-");
      sb.Append(eventData.ChangedAmount);
      animatedField.text = sb.ToString();

      animatedField.gameObject.SetActive(true);

      StartCoroutine(MoveAndFade(animatedField, eventData.Type == BalanceChangeEvent.BalanceChangeType.Income));
    }

    private IEnumerator MoveAndFade (TMP_Text animatedField, bool up)
    {
      Vector3 startPosition = animatedField.transform.localPosition;
      Vector3 endPosition = animatedField.transform.localPosition += new Vector3(0, ((RectTransform)animatedField.transform).sizeDelta.y * (up ? 1 : -1), 0);

      float animationTimeElapsed = 0f;

      Color startColor = animatedField.color;
      Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

      while (animationTimeElapsed < _animationDuration) {
        if (animationTimeElapsed < _animationDuration) {
          animationTimeElapsed += Time.deltaTime;
          animatedField.transform.localPosition = Vector3.Lerp(startPosition, endPosition, animationTimeElapsed / _animationDuration);
          animatedField.color = Color.Lerp(startColor, endColor, animationTimeElapsed / _animationDuration);
        }

        yield return null;
      }

      animatedField.gameObject.SetActive(false);
      animatedField.color = startColor;
      _pool.Return(animatedField);
    }
  }
}