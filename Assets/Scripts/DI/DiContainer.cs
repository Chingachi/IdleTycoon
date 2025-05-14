using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DI
{
  public class DiContainer
  {
    private Dictionary<Type, object> _singletons = new Dictionary<Type, object>();
    private Dictionary<Type, object> _caches = new Dictionary<Type, object>();
    private HashSet<Type> _transients = new HashSet<Type>();
    private Dictionary<Type, BindType> _binds = new Dictionary<Type, BindType>();

    private Dictionary<BindType, Action<Type, object>> _binders;
    private Dictionary<BindType, Action<Type>> _unbinders;
    private Dictionary<BindType, Func<Type, object>> _resolvers;

    public DiContainer()
    {
      _binders = new Dictionary<BindType, Action<Type, object>>()
      {
        {
          BindType.Transient, (type, instance) => _transients.Add(type)
        },
        {
          BindType.Cached, (type, instance) => AddToDictionary(type, instance, _caches)
        },
        {
          BindType.Singleton, (type, instance) => AddToDictionary(type, instance, _singletons)
        }
      };

      _unbinders = new Dictionary<BindType, Action<Type>>()
      {
        {
          BindType.Transient, type => _transients.Remove(type)
        },
        {
          BindType.Cached, type => _caches.Remove(type)
        },
        {
          BindType.Singleton, type => _singletons.Remove(type)
        }
      };

      _resolvers = new Dictionary<BindType, Func<Type, object>>()
      {

        {
          BindType.Transient, Activator.CreateInstance
        },
        {
          BindType.Cached, type => _caches.TryGetValue(type, out var result) ? result : null
        },
        {
          BindType.Singleton, type => _singletons.TryGetValue(type, out var result) ? result : null
        }
      };
    }

    public T Resolve<T>()
      where T : class
    {
      var type = typeof(T);
      var bindType = _binds[type];

      var result = (T)_resolvers[bindType](type);

      if (result == null) {
        throw new Exception($"Resolver for {type.Name} was not found");
      }

      return result;
    }

    public void Bind<T> (T instance, BindType bindType)
      where T : class
    {
      var type = typeof(T);

      AddBinding(type, bindType);

      if (_binders.TryGetValue(bindType, out var binder)) {
        binder.Invoke(type, instance);
      }
    }



    public void Unbind<T>()
      where T : class
    {
      var type = typeof(T);

      if (!_binds.ContainsKey(type)) {
        return;
      }

      var bindType = _binds[type];

      if (_unbinders.TryGetValue(bindType, out var unbinder)) {
        unbinder.Invoke(type);
      }

      _binds.Remove(type);
    }

    private void AddToDictionary<T> (Type type, T instance, Dictionary<Type, object> dictionary)
      where T : class
    {
      if (dictionary.ContainsKey(type)) {
        dictionary[type] = instance;
      } else {
        dictionary.Add(type, instance);
      }
    }


    private void AddBinding (Type type, BindType bindType)
    {
      if (_binds.ContainsKey(type)) {
        if (_binds[type] != bindType) {
          Debug.LogWarning($"Bind type for {type.Name} has been changed from {_binds[type]} to {bindType}");
          Unbind<Type>();
          _binds[type] = bindType;
        }
      } else {
        _binds.Add(type, bindType);
      }
    }
  }

  public enum BindType
  {
    Transient,
    Cached,
    Singleton
  }
}