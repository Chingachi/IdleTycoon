using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
namespace DI
{
  public class DiContainer
  {
    private readonly Dictionary<Type, object> _singletons = new Dictionary<Type, object>();
    private readonly Dictionary<Type, object> _caches = new Dictionary<Type, object>();
    private readonly Dictionary<Type, Type> _transients = new Dictionary<Type, Type>();

    private readonly Dictionary<Type, BindType> _binds = new Dictionary<Type, BindType>();

    private readonly Dictionary<BindType, Action<Type, object>> _binders;
    private readonly Dictionary<BindType, Action<Type>> _unbinders;
    private readonly Dictionary<BindType, Func<Type, object>> _resolvers;

    public DiContainer()
    {
      _binders = new Dictionary<BindType, Action<Type, object>>
      {
        {
          BindType.Transient, (type, instance) => _transients.Add(type, instance.GetType())
        },
        {
          BindType.Cached, (type, instance) => AddToDictionary(type, instance, _caches)
        },
        {
          BindType.Singleton, (type, instance) =>
          {
            AddToDictionary(type, instance, _singletons);
          }
        }
      };

      _unbinders = new Dictionary<BindType, Action<Type>>
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

      _resolvers = new Dictionary<BindType, Func<Type, object>>
      {

        {
          BindType.Transient, type => _transients.TryGetValue(type, out Type implType) ? CreateInstance(implType) : null
        },
        {
          BindType.Cached, type => _caches.TryGetValue(type, out object result) ? result : null
        },
        {
          BindType.Singleton, type => _singletons.TryGetValue(type, out object result) ? result : null
        }
      };

      Bind(this, BindType.Singleton);
    }

    public T Resolve<T>()
      where T : class
    {
      Type type = typeof(T);
      BindType bindType = _binds[type];

      T result = (T)_resolvers[bindType](type);

      if (result == null) {
        Debug.LogError($"Resolver for {type.Name} was not found");
      }

      return result;
    }

    public object Resolve (Type type)
    {
      BindType bindType = _binds[type];

      object result = _resolvers[bindType](type);

      if (result == null) {
        Debug.LogError($"Resolver for {type.Name} was not found");
      }

      return result;
    }

    public void BindTo<T> (T instance, Type type, BindType bindType)
    {
      AddBinding(type, bindType);

      if (_binders.TryGetValue(bindType, out Action<Type, object> binder)) {
        binder.Invoke(type, instance);
      }
    }

    public void Bind<T> (T instance, BindType bindType)
      where T : class
    {
      Type type = instance.GetType();
      BindTo(instance, type, bindType);
    }

    public void CreateAndBind<T> (BindType bindType)
      where T : class
    {
      object instance = CreateInstance(typeof(T));
      Bind(instance, bindType);
    }

    public void CreateAndBindTo<TReference, TInstance> (BindType bindType)
      where TInstance : class, TReference where TReference : class
    {
      object instance = CreateInstance(typeof(TInstance));
      BindTo(instance, typeof(TReference), bindType);
    }

    public void ClearCache()
    {
      foreach (KeyValuePair<Type, object> cache in _caches) {
        if (_binds.ContainsKey(cache.Key) && _binds[cache.Key] == BindType.Cached) {
          _binds.Remove(cache.Key);
        }
      }

      _caches.Clear();
    }

    public void Unbind<T>()
      where T : class
    {
      Type type = typeof(T);

      if (!_binds.ContainsKey(type)) {
        return;
      }

      BindType bindType = _binds[type];

      if (_unbinders.TryGetValue(bindType, out Action<Type> unbinder)) {
        unbinder.Invoke(type);
      }

      _binds.Remove(type);
    }

    private object CreateInstance (Type type)
    {
      ConstructorInfo [] constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

      if (constructors.Length == 0) {
        return Activator.CreateInstance(type);
      }


      ConstructorInfo constructor = constructors.OrderByDescending(c => c.GetParameters().Length).First();
      ParameterInfo [] parameters = constructor.GetParameters();
      object [] resolvedParams = new object[parameters.Length];

      for (int i = 0; i < parameters.Length; i++) {
        Type parameterType = parameters[i].ParameterType;
        resolvedParams[i] = Resolve(parameterType);
      }

      return Activator.CreateInstance(type, resolvedParams);
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
        Debug.LogWarning($"Beware, you're rebinding {type.Name}");

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