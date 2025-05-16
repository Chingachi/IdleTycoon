namespace Storages.Base
{
  public abstract class Storage<T>
    where T : IData, new()
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
      return typeof(T).Name;
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