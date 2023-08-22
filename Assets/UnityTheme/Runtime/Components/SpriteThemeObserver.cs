using UnityEngine;
using UnityEngine.UI;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    [AddComponentMenu("UI/UnityTheme/Observers/SpriteThemeObserver")]
    [RequireComponent(typeof(Image))]
    public class SpriteThemeObserver : ThemeObserver<Image>
    {
        public override void ChangeTheme(Theme theme)
        {
            var e = Entries.Instance.FindEntryByKeyAndTheme(key, theme.Id);
            if (e.Type != EntryType.Sprite)
            {
                Debug.LogWarning(
                    $"{nameof(SpriteThemeObserver)} expects type of {EntryType.Sprite}, but you set {e.Type}");
                return;
            }

            Target.sprite = e.SpriteEntry.Value;
        }
    }
}