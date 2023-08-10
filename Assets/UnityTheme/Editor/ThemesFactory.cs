using System;
using UnityEditor;
using UnityTheme.Model;

namespace UnityTheme.Editor
{
    public static class ThemesFactory
    {
        public static Themes CreateDefault()
        {
            var utils = new AssetDataBaseUtils<Themes>();
            var path = utils.AssetPath();
            if (path != null)
            {
                throw new InvalidOperationException($"{nameof(Theme)} already exists at {path}");
            }
            
            var assetPath = EditorUtility.SaveFilePanelInProject($"Save {nameof(Themes)}",
                nameof(Themes),
                "asset", "", "Assets");
            
            if (string.IsNullOrEmpty(assetPath))
                // Return if canceled.
                return null;

            var asset =  utils.CreateAsset(assetPath, false);
            asset.AddTheme("Default");
            return asset;
        }
    }
}