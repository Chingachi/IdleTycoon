using System;
using Buildings.Dto;
using Common;
using UnityEngine;
namespace Buildings
{
  [Serializable]
  public class BuildingData
  {

    [NonSerialized]
    public float IncomeWaitedSeconds;
    public long LastTimeDecayChanged;

    public string Id;
    public string Name;
    public BuildingType Type;
    public int CurrentLevel;
    public float CurrentDecay;

    public int PlaceholderIndex;

    public float BaseUpgradePrice;
    public float BaseIncome;
    public float BaseIncomeTime;
    public float BaseDecayCoefficient;

    public BuildingData (
      string id, string name, BuildingType type, int currentLevel, float currentDecay, int placeholderIndex, float baseUpgradePrice, float baseIncome, float baseIncomeTime,
      float baseDecayCoefficient, long lastTimeDecayChanged)
    {
      Id = id;
      Name = name;
      Type = type;
      CurrentLevel = currentLevel;
      CurrentDecay = currentDecay;
      PlaceholderIndex = placeholderIndex;
      BaseUpgradePrice = baseUpgradePrice;
      BaseIncome = baseIncome;
      BaseIncomeTime = baseIncomeTime;
      BaseDecayCoefficient = baseDecayCoefficient;
      LastTimeDecayChanged = lastTimeDecayChanged;
    }

    public BuildingData (BuildingDto dto)
    {
      Id = Guid.NewGuid().ToString();
      Name = dto.Name;
      Type = dto.Type;
      CurrentLevel = 1;
      CurrentDecay = 1;
      BaseUpgradePrice = dto.BaseUpgradePrice;
      BaseIncome = dto.BaseIncome;
      BaseIncomeTime = dto.BaseIncomePeriod;
      BaseDecayCoefficient = dto.BaseDecayCoefficient;
      LastTimeDecayChanged = DateTime.UtcNow.Ticks;
    }

    public float GetCurrentIncome()
    {
      if (CurrentDecay < Constants.MINIMUM_INCOME_DECAY) {
        return 0;
      }

      return BaseIncome * (1 + 0.5f * Level) * CurrentDecay;
    }

    public float GetCurrentUpgradePrice()
    {
      return BaseUpgradePrice * (Level + 1) * 1.75f;
    }

    public float GetCurrentIncomeTime()
    {
      return BaseIncomeTime * Mathf.Pow(0.98f, Level);
    }

    public float GetCurrentDecayCoefficient()
    {
      int level = Level / 2;

      float result = BaseDecayCoefficient * Mathf.Pow(0.9f, level);
      result /= 100f;

      return result;
    }

    public void ApplyDecay()
    {
      CurrentDecay -= GetCurrentDecayCoefficient();
      LastTimeDecayChanged = DateTime.Now.Ticks;
    }

    private int Level
    {
      get
      {
        return CurrentLevel == 0 ? 1 : CurrentLevel;
      }
    }
  }
}