using UnityEngine;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    [RequireComponent(typeof(GradientImage))]
    public class GradientThemeObserver : MonoBehaviour
    {
        [SerializeField]
        private string key;

        private GradientImage _gradientImage;

        private void Awake()
        {
            _gradientImage = GetComponent<GradientImage>();
            ThemeManager.Instance.OnChangeTheme += OnChangeTheme;
            
            OnChangeTheme(ThemeManager.Instance.SelectedTheme);
        }

        private void OnChangeTheme(Theme theme)
        {
            var e = Entries.Instance.FindEntryByKeyAndTheme(key, theme.Id);
            if (e.Type != EntryType.Gradient)
            {
                Debug.LogWarning(
                    $"{nameof(ColorThemeObserver)} expects type of {EntryType.Gradient}, but you set {e.Type}");
                return;
            }

            _gradientImage.gradient = e.GradientEntry.Value;
        }

        private void OnDestroy()
        {
            ThemeManager.Instance.OnChangeTheme -= OnChangeTheme;
        }
    }
}