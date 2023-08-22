using UnityEngine;
using UnityEngine.UI;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    [AddComponentMenu("UI/UnityTheme/Observers/ColorThemeObserver")]
    [RequireComponent(typeof(Graphic))]
    public class ColorThemeObserver : ThemeObserver<Graphic>
    {

        public override void ChangeTheme(Theme theme)
        {
            var e = Entries.Instance.FindEntryByKeyAndTheme(key, theme.Id);
            if (e.Type != EntryType.Color)
            {
                Debug.LogWarning(
                    $"{nameof(ColorThemeObserver)} expects type of {EntryType.Color}, but you set {e.Type}");
                return;
            }

            Target.color = e.ColorEntry.Value;
        }
    }
}