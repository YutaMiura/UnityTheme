using UnityEngine;
using UnityEngine.UI;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    [AddComponentMenu("UI/UnityTheme/Observers/ColorThemeWithoutAlphaObserver")]
    [RequireComponent(typeof(Graphic))]
    public class ColorThemeWithoutAlphaObserver : ThemeObserver<Graphic>
    {
        public override void ChangeTheme(Theme theme)
        {
            var e = Entries.Instance.FindEntryByKeyAndTheme(key, theme.Id);
            if (e.Type != EntryType.Color)
            {
                Debug.LogWarning(
                    $"{nameof(ColorThemeWithoutAlphaObserver)} expects type of {EntryType.Color}, but you set {e.Type}");
                return;
            }

            var a = Target.color.a;
            
            Target.color = new Color(e.ColorEntry.Value.r, e.ColorEntry.Value.g, e.ColorEntry.Value.b, a);
        }
    }
}