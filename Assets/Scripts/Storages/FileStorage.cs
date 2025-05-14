using System.IO;
using UnityEngine;
namespace Storages
{
  public class FileStorage<T> : Storage<T>
    where T : new()
  {
    protected override void LoadData()
    {
      if (!File.Exists(GetFileName())) {
        _data = new T();

        return;
      }

      StreamReader sr = new StreamReader(GetFileName());

      using (sr) {
        string readData = sr.ReadToEnd();

        _data = JsonUtility.FromJson<T>(readData);
      }
    }

    protected override void SaveData()
    {
      StreamWriter sw = new StreamWriter(GetFileName());

      using (sw) {
        string dataToSave = JsonUtility.ToJson(_data);
        sw.Write(dataToSave);
      }
    }

    private string GetFileName()
    {
      return $"{Application.persistentDataPath}/{GetKey()}.dt";
    }
  }
}