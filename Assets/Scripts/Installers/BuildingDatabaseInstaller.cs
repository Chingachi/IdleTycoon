using Buildings.Dto;
using DI;
using UnityEngine;
namespace Installers
{
  public class BuildingDatabaseInstaller : BaseInstaller
  {
    [SerializeField]
    private BuildingsSO _buildingDatabase;

    public override void InstallBindings()
    {

      Container.Bind(_buildingDatabase, BindType.Cached);
    }
  }
}