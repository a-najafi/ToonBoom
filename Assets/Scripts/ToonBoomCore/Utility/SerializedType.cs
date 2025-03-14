using System;
using UnityEngine;

namespace ToonBoomCore.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;

    [Serializable]
    public class SerializedType<TBase> where TBase : class
    {
        [SerializeField] private string typeName; // Store only class name, not full assembly name

        private static Dictionary<string, Type> typeLookup;

        public Type Type
        {
            get
            {
                if (string.IsNullOrEmpty(typeName)) return null;
                EnsureTypeLookup(); // Make sure the dictionary is initialized
                return typeLookup.TryGetValue(typeName, out Type type) ? type : null;
            }
            set
            {
                typeName = value != null ? value.Name : "";
            }
        }

        private static void EnsureTypeLookup()
        {
            if (typeLookup == null)
            {
                typeLookup = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(t => typeof(TBase).IsAssignableFrom(t) && !t.IsAbstract)
                    .ToDictionary(t => t.Name, t => t);
            }
        }
    }

}