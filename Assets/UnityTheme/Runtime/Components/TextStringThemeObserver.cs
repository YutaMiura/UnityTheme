using UnityEngine;
using UnityEngine.UI;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    [AddComponentMenu("UI/UnityTheme/Observers/TextThemeObserver")]
    [RequireComponent(typeof(Text))]
    public class TextStringThemeObserver : ThemeObserver<Text>
    {
        public override void ChangeTheme(Theme theme)
        {
            var e = Entries.Instance.FindEntryByKeyAndTheme(key, theme.Id);
            if (e.Type != EntryType.String)
            {
                Debug.LogWarning(
                    $"{nameof(TextStringThemeObserver)} expects type of {EntryType.String}, but you set {e.Type}");
                return;
            }

            Target.text = e.StringEntry.Value;
        }
    }
}