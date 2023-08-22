using TMPro;
using UnityEngine;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    [AddComponentMenu("UI/UnityTheme/Observers/TMPTextStringThemeObserver")]
    [RequireComponent(typeof(TMP_Text))]
    public class TMPTextStringThemeObserver : ThemeObserver<TMP_Text>
    {
        public override void ChangeTheme(Theme theme)
        {
            var e = Entries.Instance.FindEntryByKeyAndTheme(key, theme.Id);
            if (e.Type != EntryType.String)
            {
                Debug.LogWarning(
                    $"{nameof(TMPTextStringThemeObserver)} expects type of {EntryType.String}, but you set {e.Type}");
                return;
            }

            Target.text = e.StringEntry.Value;
        }
    }
}