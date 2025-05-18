using System;
namespace Core.Storages
{
  public abstract class Storage<T>
    where T : ISaveData, new()
  {
    protected T _data;

    public virtual void UpdateData()
    {
      SaveData();
    }

    public virtual void UpdateData (T newData)
    {
      _data = newData;
      SaveData();
    }

    protected abstract void LoadData();

    protected abstract void SaveData();


    protected virtual string GetKey()
    {
      Type type = typeof(T);
      SaveFilenameAttribute attribute = Attribute.GetCustomAttribute(type, typeof(SaveFilenameAttribute)) as SaveFilenameAttribute;

      return attribute == null ? type.Name : attribute.Filename;
    }

    public T Data
    {
      get
      {
        if (_data != null) {
          return _data;
        }

        LoadData();

        if (_data == null) {
          _data = new T();
        }

        return _data;
      }
    }
  }
}