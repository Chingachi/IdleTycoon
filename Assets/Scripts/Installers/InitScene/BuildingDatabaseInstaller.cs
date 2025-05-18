using Buildings.Dto;
using Core.DI;
using UnityEngine;
namespace Installers.InitScene
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