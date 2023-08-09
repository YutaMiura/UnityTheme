using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityTheme.Model;

namespace UnityTheme.Editor
{
    public class AssetDataBaseUtils<T> where T : ScriptableObject
    {
        internal string AssetPath()
        {
            var asset = LoadFromAssetDatabase();
            return asset != null ? AssetDatabase.GetAssetPath(asset) : null;
        }
        
        internal T CreateAsset(string assetPath, bool registerToPreloadedAssets)
        {
            if (string.IsNullOrEmpty(assetPath)) throw new ArgumentNullException(nameof(assetPath));

            // Create folders if needed.
            var folderPath = Path.GetDirectoryName(assetPath);
            if (!string.IsNullOrEmpty(folderPath) && !Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var instance = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(instance, assetPath);
            AssetDatabase.SaveAssets();
            
            if (registerToPreloadedAssets)
                RegisterToPreloadedAssets();

            return instance;
        }
        
        public T LoadFromAssetDatabase()
        {
            var asset = AssetDatabase.FindAssets($"t:{nameof(T)}")
                .Select(x => AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(x)))
                .FirstOrDefault();

            return asset;
        }

        private void RegisterToPreloadedAssets(bool saveAsset = true)
        {
            var asset = LoadFromAssetDatabase();

            if (asset == null)
                throw new InvalidOperationException($"{nameof(Themes)} does not exists.");

            var preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();
            if (preloadedAssets.Contains(asset))
                return;
            
            preloadedAssets.Add(asset);
            PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());
            if (saveAsset)
                AssetDatabase.SaveAssets();
        }
    }
}