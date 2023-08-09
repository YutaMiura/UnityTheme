using System;
using UnityEditor;
using UnityTheme.Model;

namespace UnityTheme.Editor
{
    public class EntriesFactory
    {
        public static Entries CreateDefault()
        {
            var utils = new AssetDataBaseUtils<Entries>();
            var path = utils.AssetPath();
            if (path != null)
            {
                throw new InvalidOperationException($"{nameof(Entries)} already exists at {path}");
            }
            
            var assetPath = EditorUtility.SaveFilePanelInProject($"Save {nameof(Entries)}",
                nameof(Entries),
                "asset", "", "Assets");
            
            if (string.IsNullOrEmpty(assetPath))
                // Return if canceled.
                return null;

            var asset =  utils.CreateAsset(assetPath, false);
            return asset;
        }
    }
}