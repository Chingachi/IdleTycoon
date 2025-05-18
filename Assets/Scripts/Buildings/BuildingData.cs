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
    public float CurrentDurability;

    public int PlaceholderIndex;

    public float BaseUpgradePrice;
    public float BaseIncome;
    public float BaseIncomeTime;
    public float BaseDecayCoefficient;

    public float BuyPrice;
    public float RepairCoefficient;


    public BuildingData (
      string id, string name, BuildingType type, int currentLevel, float currentDurability, int placeholderIndex, float baseUpgradePrice, float baseIncome, float baseIncomeTime,
      float baseDecayCoefficient, float buyPrice, float repairCoefficient, long lastTimeDecayChanged)
    {
      Id = id;
      Name = name;
      Type = type;
      CurrentLevel = currentLevel;
      CurrentDurability = currentDurability;
      PlaceholderIndex = placeholderIndex;
      BaseUpgradePrice = baseUpgradePrice;
      BaseIncome = baseIncome;
      BaseIncomeTime = baseIncomeTime;
      BaseDecayCoefficient = baseDecayCoefficient;
      LastTimeDecayChanged = lastTimeDecayChanged;

      BuyPrice = buyPrice;
      RepairCoefficient = repairCoefficient;
    }

    public BuildingData (BuildingDto dto)
    {
      Id = Guid.NewGuid().ToString();
      Name = dto.Name;
      Type = dto.Type;
      CurrentLevel = 1;
      CurrentDurability = 1;
      BaseUpgradePrice = dto.BaseUpgradePrice;
      BaseIncome = dto.BaseIncome;
      BaseIncomeTime = dto.BaseIncomePeriod;
      BaseDecayCoefficient = dto.BaseDecayCoefficient;
      LastTimeDecayChanged = DateTime.UtcNow.Ticks;

      BuyPrice = dto.Price;
      RepairCoefficient = dto.RepairCoefficient;
    }

    public float GetCurrentIncome()
    {
      if (CurrentDurability < Constants.MINIMUM_INCOME_DURABILITY) {
        return 0;
      }

      return BaseIncome * (1 + 0.3f * Level) * CurrentDurability;
    }

    public float GetCurrentUpgradePrice()
    {
      return BaseUpgradePrice * (Level * (1 + 0.5f));
    }

    public float GetCurrentIncomeTime()
    {
      return BaseIncomeTime * Mathf.Pow(0.95f, Level);
    }

    public float GetCurrentDecayCoefficient()
    {
      float result = BaseDecayCoefficient * Mathf.Pow(0.92f, Level);
      result /= 100f;

      return result;
    }

    public void ApplyDecay()
    {
      CurrentDurability = Mathf.Max(0, CurrentDurability - GetCurrentDecayCoefficient());
      LastTimeDecayChanged = DateTime.Now.Ticks;
    }

    public void ResetDurability()
    {
      CurrentDurability = 1;
      LastTimeDecayChanged = DateTime.Now.Ticks;
    }

    public float GetRepairCost()
    {
      float baseRepair = BuyPrice * (1 + RepairCoefficient * Level);

      return baseRepair * (1 - CurrentDurability);
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