using System;
using Buildings.Dto;
using UnityEngine;
namespace Buildings
{
  [Serializable]
  public class BuildingData
  {
    public string Name;
    public BuildingType Type;
    public int CurrentLevel;
    public float CurrentDecay;

    public int PlaceholderIndex;

    [NonSerialized]
    public float WaitedSeconds;

    public float BaseUpgradePrice;
    public float BaseIncome;
    public float BaseIncomeTime;
    public float BaseDecayTime;

    public BuildingData (string name, BuildingType type, int currentLevel, float currentDecay, int placeholderIndex, float baseUpgradePrice, float baseIncome, float baseIncomeTime, float baseDecayTime)
    {
      Name = name;
      Type = type;
      CurrentLevel = currentLevel;
      CurrentDecay = currentDecay;
      PlaceholderIndex = placeholderIndex;
      BaseUpgradePrice = baseUpgradePrice;
      BaseIncome = baseIncome;
      BaseIncomeTime = baseIncomeTime;
      BaseDecayTime = baseDecayTime;
    }

    public BuildingData (BuildingDto dto)
    {
      
      Name = dto.Name;
      Type = dto.Type;
      CurrentLevel = 1;
      CurrentDecay = 1;
      BaseUpgradePrice = dto.BaseUpgradePrice;
      BaseIncome = dto.BaseIncome;
      BaseIncomeTime = dto.BaseIncomePeriod;
      BaseDecayTime = dto.BaseDecayCoefficient;
    }

    public float GetCurrentIncome()
    {
      return BaseIncome * (1 + 0.5f * Level);
    }

    public float GetCurrentUpgradePrice()
    {
      return BaseUpgradePrice * (Level + 1) * 1.75f;
    }

    public float GetCurrentIncomeTime()
    {
      return BaseIncomeTime * (Mathf.Pow(0.98f, Level));
    }

    public float GetCurrentDecayTime()
    {
      int level = Level / 2;

      return BaseDecayTime * (Mathf.Pow(0.98f, level));
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