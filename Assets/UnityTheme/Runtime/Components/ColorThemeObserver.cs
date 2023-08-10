using UnityEngine;
using UnityEngine.UI;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    [RequireComponent(typeof(Graphic))]
    public class ColorThemeObserver : MonoBehaviour
    {
        [SerializeField]
        private string key;

        private Graphic _graphic;

        private void Awake()
        {
            _graphic = GetComponent<Graphic>();
            ThemeManager.Instance.OnChangeTheme += OnChangeTheme;

            OnChangeTheme(ThemeManager.Instance.SelectedTheme);
        }

        private void OnChangeTheme(Theme theme)
        {
            var e = Entries.Instance.FindEntryByKeyAndTheme(key, theme.Id);
            if (e.Type != EntryType.Color)
            {
                Debug.LogWarning(
                    $"{nameof(ColorThemeObserver)} expects type of {EntryType.Color}, but you set {e.Type}");
                return;
            }

            _graphic.color = e.ColorEntry.Value;
        }

        private void OnDestroy()
        {
            ThemeManager.Instance.OnChangeTheme -= OnChangeTheme;
        }
    }
}