using System;
using UnityEngine;
namespace Buildings.Dto
{
  [Serializable, CreateAssetMenu(fileName = "Building", menuName = "ScriptableObjects/Building", order = 3)]
  public class BuildingDto : ScriptableObject
  {

    public string Name;
    public BuildingType Type;
    public string Description;
    public int Price;
    public int BaseUpgradePrice;
    public int BaseIncome;
    public float BaseIncomePeriod;
    public float BaseDecayCoefficient;
    public Sprite Preview;
    public Building Prefab;

    public BuildingDto()
    {}

    public BuildingDto (
      string name, BuildingType type, string description, int price, int baseUpgradePrice, int baseIncome, float baseIncomePeriod, float baseDecayCoefficient, Sprite preview, Building prefab)
    {
      Name = name;
      Type = type;
      Description = description;
      Price = price;
      BaseUpgradePrice = baseUpgradePrice;
      BaseIncome = baseIncome;
      BaseIncomePeriod = baseIncomePeriod;
      BaseDecayCoefficient = baseDecayCoefficient;
      Preview = preview;
      Prefab = prefab;
    }

    public BuildingDto (BuildingDto source)
    {
      Name = source.Name;
      Type = source.Type;
      Description = source.Description;
      Price = source.Price;
      BaseUpgradePrice = source.BaseUpgradePrice;
      BaseIncome = source.BaseIncome;
      BaseIncomePeriod = source.BaseIncomePeriod;
      BaseDecayCoefficient = source.BaseDecayCoefficient;
      Preview = source.Preview;
      Prefab = source.Prefab;
    }
  }
}