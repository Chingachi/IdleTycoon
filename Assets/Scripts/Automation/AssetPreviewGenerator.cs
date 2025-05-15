using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
namespace Automation
{
  [ExecuteInEditMode]
  public class AssetPreviewGenerator : MonoBehaviour
  {
    [SerializeField]
    private List<Object> assets = new List<Object>();

    [ContextMenu("Generate previews")]
    public async void GenerateAssetPreviews()
    {
      foreach (Object asset in assets) {

        while (AssetPreview.GetAssetPreview(asset)==null) {
          await Task.Yield();
        }
        Texture2D source = AssetPreview.GetAssetPreview(asset);

        Texture2D copy = new Texture2D(source.width, source.height, TextureFormat.RGBA32, false);
        copy.SetPixels(source.GetPixels());
        copy.Apply();

        byte [] bytes = copy.EncodeToPNG();
        string savePath = Path.Combine("Assets/Art/GeneratedPreviews", asset.name + ".png");
        File.WriteAllBytes(savePath, bytes);
      }
    }
  }
}