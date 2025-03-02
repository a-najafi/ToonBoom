using System;
using UnityEngine;

namespace ToonBoomCore.Utility
{
  
    [Serializable]
    public class SerializableAssetPath
    {
        [SerializeField] private string assetPath;

        public string AssetPath => assetPath; // Read-only outside the class

        public void SetPath(string path)
        {
            assetPath = path;
        }
    }
}