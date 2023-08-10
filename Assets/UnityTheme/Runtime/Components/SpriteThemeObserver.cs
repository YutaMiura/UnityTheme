using UnityEngine;
using UnityEngine.UI;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    [RequireComponent(typeof(Image))]
    public class SpriteThemeObserver : MonoBehaviour
    {
        [SerializeField]
        private string key;
        private Image _image;
        private void Awake()
        {
            _image = GetComponent<Image>();
            
            ThemeManager.Instance.OnChangeTheme += OnChangeTheme;
        }

        private void OnChangeTheme(Theme theme)
        {
            var e = Entries.Instance.FindEntryByKeyAndTheme(key, theme.Id);
            if (e.Type != EntryType.Sprite)
            {
                Debug.LogWarning($"{nameof(SpriteThemeObserver)} expects type of {EntryType.Sprite}, but you set {e.Type}");
                return;
            }

            _image.sprite = e.SpriteEntry.Value;
        }
    }
}