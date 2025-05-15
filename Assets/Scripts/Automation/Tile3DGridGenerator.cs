using UnityEditor;
using UnityEngine;
namespace Automation
{
  [ExecuteInEditMode]
  public class Tile3DGridGenerator : MonoBehaviour
  {
    [SerializeField]
    private GameObject _grassPrefab;
    [SerializeField]
    private GameObject _roadPrefab;
    [SerializeField]
    private GameObject _stonePrefab;
    [SerializeField]
    private GameObject _placementPrefab;
    [SerializeField]
    private Vector2 _tileSize = new Vector2(2.986295f, 2.986295f);
    [SerializeField]
    private int _width = 100;
    [SerializeField]
    private int _height = 100;

    private GameObject _grassParent;
    private GameObject _roadParent;
    private GameObject _stoneParent;
    private GameObject _placementsParent;


    [ContextMenu("Generate Grid")]
    public void GenerateGrid()
    {
      CleanGrid();
      AddParents();

      int half = _height / 2;

      for (int x = 0; x < _width; x++) {
        for (int z = 0; z < _height; z++) {
          Vector3 position = new Vector3(x * _tileSize.x, 0f, z * _tileSize.y);
          Object prefab = GetPrefab(x, z, half);

          GameObject tile = (GameObject)PrefabUtility.InstantiatePrefab(prefab, transform);
          tile.transform.localPosition = position;
          SetTileName(tile, x, z, half);

          if (z == half) {
            tile.transform.Rotate(new Vector3(0, 90, 0));
          }

          if (z == half + 1) {
            tile.transform.Rotate(new Vector3(0, -90, 0));
          }
        }
      }

      AddPlacements();
      AddGroundCollider();
    }

    private void AddPlacements()
    {
      bool setParentPosition = false;

      for (int z = _height / 2 - 2; z < _height / 2 + 4; z++) {
        int step = 0;

        for (int x = _width / 2 - 40; x < _width / 2 + 40; x++) {
          if (step > 0 && step < 4) {
            step++;

            continue;
          }

          step = 0;

          if (z == _height / 2 - 2 || z == _height / 2 + 3) {
            Vector3 position = new Vector3(x * _tileSize.x + _tileSize.x / 2f, 0f, z * _tileSize.y + _tileSize.y / 2f);

            if (!setParentPosition) {
              _placementsParent.transform.localPosition = position;
              setParentPosition = true;
            }


            GameObject tile = (GameObject)PrefabUtility.InstantiatePrefab(_placementPrefab, transform);

            if (z == _height / 2 + 3) {
              tile.transform.Rotate(new Vector3(0, 180, 0));
              position -= new Vector3(_tileSize.x * 3, 0, _tileSize.y);
            }

            int offset = Random.Range(0, 100);

            if (offset % 3 == 1) {
              position += new Vector3(_tileSize.x / Random.Range(1, 4), 0, 0);
            } else if (offset % 3 == 2) {
              position -= new Vector3(_tileSize.x / Random.Range(1, 4), 0, 0);
            }

            tile.transform.localPosition = position;
            tile.transform.SetParent(_placementsParent.transform);
          }


          step++;
        }
      }
    }

    private void AddParents()
    {
      AddParent(ref _grassParent, "Grass");
      AddParent(ref _roadParent, "Road");
      AddParent(ref _stoneParent, "Stone");
      AddParent(ref _placementsParent, "Placements");
    }

    private void AddParent (ref GameObject go, string parentName)
    {
      go = new GameObject(parentName);
      go.transform.SetParent(transform);
    }

    private void SetTileName (GameObject tile, int x, int z, int half)
    {
      if (z == half || z == half + 1) {
        tile.name = $"Road_{x}_{z}";
        tile.transform.SetParent(_roadParent.transform);
      } else {
        tile.name = $"Grass_{x}_{z}";
        tile.transform.SetParent(_grassParent.transform);
      }

      if (z == half - 1 || z == half + 2) {
        tile.name = $"Stone_{x}_{z}";
        tile.transform.SetParent(_stoneParent.transform);
      }
    }

    private Object GetPrefab (int i, int j, int half)
    {
      if (j == half || j == half + 1) {
        return _roadPrefab;
      }

      if (j == half - 1 || j == half + 2) {
        return _stonePrefab;
      }

      return _grassPrefab;
    }

    private void CleanGrid()
    {
      for (int i = transform.childCount - 1; i >= 0; i--) {
        if (Application.isEditor && !Application.isPlaying) {
          DestroyImmediate(transform.GetChild(i).gameObject);
        } else {
          Destroy(transform.GetChild(i).gameObject);
        }
      }
    }

    private void AddGroundCollider()
    {
      float totalWidth = _width * _tileSize.x;
      float totalDepth = _height * _tileSize.y;

      Vector3 center = new Vector3(totalWidth / 2f - _tileSize.x / 2f, -0.5f, totalDepth / 2f - _tileSize.y / 2f);

      BoxCollider collider = gameObject.GetComponent<BoxCollider>();

      if (collider == null) {
        collider = gameObject.AddComponent<BoxCollider>();
      }

      collider.center = center;
      collider.size = new Vector3(totalWidth, 1, totalDepth);
    }
  }
}