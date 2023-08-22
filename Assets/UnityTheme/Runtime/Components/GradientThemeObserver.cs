using UnityEngine;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    [RequireComponent(typeof(GradientImage))]
    [AddComponentMenu("UI/UnityTheme/Observers/GradientThemeObserver")]
    public class GradientThemeObserver : ThemeObserver<GradientImage>
    {
        public override void ChangeTheme(Theme theme)
        {
            var e = Entries.Instance.FindEntryByKeyAndTheme(key, theme.Id);
            if (e.Type != EntryType.Gradient)
            {
                Debug.LogWarning(
                    $"{nameof(GradientThemeObserver)} expects type of {EntryType.Gradient}, but you set {e.Type}");
                return;
            }

            Target.gradient = e.GradientEntry.Value;
        }
    }
}