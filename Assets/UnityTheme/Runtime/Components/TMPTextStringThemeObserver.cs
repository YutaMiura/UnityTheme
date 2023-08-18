using TMPro;
using UnityEngine;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    [AddComponentMenu("UI/UnityTheme/Observers/TMPTextStringThemeObserver")]
    [RequireComponent(typeof(TMP_Text))]
    public class TMPTextStringThemeObserver : MonoBehaviour
    {
        [SerializeField]
        private string key;

        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            ThemeManager.Instance.OnChangeTheme += OnChangeTheme;

            OnChangeTheme(ThemeManager.Instance.SelectedTheme);
        }

        private void OnChangeTheme(Theme theme)
        {
            var e = Entries.Instance.FindEntryByKeyAndTheme(key, theme.Id);
            if (e.Type != EntryType.String)
            {
                Debug.LogWarning(
                    $"{nameof(ColorThemeObserver)} expects type of {EntryType.String}, but you set {e.Type}");
                return;
            }

            _text.text = e.StringEntry.Value;
        }

        private void OnDestroy()
        {
            ThemeManager.Instance.OnChangeTheme -= OnChangeTheme;
        }
    }
}