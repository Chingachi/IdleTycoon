namespace Storages
{
  public abstract class Storage<T>
    where T : IDto, new()
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
        if (_data == null) {
          _data = new T();
        }

        return _data;
      }
    }
  }
}