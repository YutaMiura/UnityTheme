using UnityEngine;
using UnityEngine.UI;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    [AddComponentMenu("UI/UnityTheme/Observers/ColorThemeWithoutAlphaObserver")]
    [RequireComponent(typeof(Graphic))]
    public class ColorThemeWithoutAlphaObserver : MonoBehaviour
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

            var a = _graphic.color.a;
            
            _graphic.color = new Color(e.ColorEntry.Value.r, e.ColorEntry.Value.g, e.ColorEntry.Value.b, a);
        }

        private void OnDestroy()
        {
            ThemeManager.Instance.OnChangeTheme -= OnChangeTheme;
        }
    }
}