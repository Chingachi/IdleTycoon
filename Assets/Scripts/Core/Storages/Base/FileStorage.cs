using System.IO;
using UnityEngine;
namespace Core.Storages.Base
{
  public class FileStorage<T> : Storage<T>
    where T : ISaveData, new()
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
        _data.Deserialize();
      }
    }

    protected override void SaveData()
    {
      StreamWriter sw = new StreamWriter(GetFileName());

      using (sw) {
        _data.Serialize();
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