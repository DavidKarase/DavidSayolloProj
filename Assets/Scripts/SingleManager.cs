using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sayollo.Services
{
    public static class SingleManager
    {
        private const string ERROR_FORMAT = "Sayollo.SingleManager.{0}: {1}";
        private static readonly Dictionary<Type, object> singletons = new Dictionary<Type, object>();

        public static void Register<T>(T instance)
        {
            Register(instance, typeof(T));
        }

        public static void Register(object instance, Type t)
        {
            if (instance == null)
            {
                Debug.LogErrorFormat(ERROR_FORMAT, "Register", "instance can't be null");
                return;
            }
            if (singletons.ContainsKey(t))
            {
                Debug.LogErrorFormat(ERROR_FORMAT, "Register", "already exists instance with the same type.  type: " + t);
                return;
            }

            singletons.Add(t, instance);
        }

        public static void OverwriteRegister<T>(T instance)
        {
            OverwriteRegister(instance, typeof(T));
        }

        public static void OverwriteRegister(object instance, Type t)
        {
            if (instance == null)
            {
                Debug.LogErrorFormat(ERROR_FORMAT, "OverwriteRegister", "instance can't be null");
                return;
            }

            singletons[t] = instance;
        }

        public static T Get<T>() where T : class
        {
            if (!singletons.ContainsKey(typeof(T)))
            {
                Debug.LogErrorFormat(ERROR_FORMAT, "Get", "can't find instance with type of: " + typeof(T));
                return null;
            }
            else
                return (T)singletons[typeof(T)];
        }

        public static bool IsExist<T>() where T : class
        {
            return singletons.ContainsKey(typeof(T));
        }

        public static void Clear()
        {
            singletons.Clear();
        }
    }
}
