using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
namespace SceneManagement
{
  [CreateAssetMenu(fileName = "ScenesList", menuName = "ScriptableObjects/ScenesList")]
  public class SceneDatabase : ScriptableObject
  {
    public List<SceneItem> _scenes = new List<SceneItem>();

    public string GetScene (SceneType sceneType)
    {
      SceneItem sceneItem = _scenes.FirstOrDefault(x => x.Type == sceneType);

      if (sceneItem == null || sceneItem.Scene == null) {
        throw new Exception($"{sceneType} hasn't been added to database!");
      }

      return sceneItem.Scene.name;
    }
  }
  [Serializable]
  public class SceneItem
  {
    public SceneType Type;
    public SceneAsset Scene;
  }

  public enum SceneType
  {
    Intro,
    Menu,
    Game
  }
}