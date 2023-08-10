using UnityEditor;
using UnityTheme.Model;

namespace UnityTheme.Editor
{
    public static class EntriesRepository
    {
        public static void Save()
        {
            var utils = new AssetDataBaseUtils<Entries>();
            var asset = utils.LoadFromAssetDatabase();
            if (asset == null) return;
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssets();
        }
    }
}