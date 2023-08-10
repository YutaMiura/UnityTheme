using UnityEngine;
using UnityEngine.UI;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    [RequireComponent(typeof(RawImage))]
    public class TextureThemeObserver : MonoBehaviour
    {
        [SerializeField] private string key;
        private RawImage _image;

        private void Awake()
        {
            _image = GetComponent<RawImage>();
            ThemeManager.Instance.OnChangeTheme += OnChangeTheme;

            OnChangeTheme(ThemeManager.Instance.SelectedTheme);
        }

        private void OnChangeTheme(Theme theme)
        {
            var e = Entries.Instance.FindEntryByKeyAndTheme(key, theme.Id);
            if (e.Type != EntryType.Texture)
            {
                Debug.LogWarning(
                    $"{nameof(SpriteThemeObserver)} expects type of {EntryType.Texture}, but you set {e.Type}");
                return;
            }

            _image.texture = e.TextureEntry.Value;
        }

        private void OnDestroy()
        {
            ThemeManager.Instance.OnChangeTheme -= OnChangeTheme;
        }
    }
}