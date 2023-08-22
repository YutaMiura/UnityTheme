using UnityEngine;
using UnityEngine.UI;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    [AddComponentMenu("UI/UnityTheme/Observers/TextureThemeObserver")]
    [RequireComponent(typeof(RawImage))]
    public class TextureThemeObserver : ThemeObserver<RawImage>
    {
        public override void ChangeTheme(Theme theme)
        {
            var e = Entries.Instance.FindEntryByKeyAndTheme(key, theme.Id);
            if (e.Type != EntryType.Texture)
            {
                Debug.LogWarning(
                    $"{nameof(TextureThemeObserver)} expects type of {EntryType.Texture}, but you set {e.Type}");
                return;
            }

            Target.texture = e.TextureEntry.Value;
        }
    }
}