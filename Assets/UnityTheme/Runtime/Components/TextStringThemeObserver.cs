using UnityEngine;
using UnityEngine.UI;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    [RequireComponent(typeof(Text))]
    public class TextStringThemeObserver : MonoBehaviour
    {
        [SerializeField]
        private string key;

        private Text _text;

        private void Awake()
        {
            _text = GetComponent<Text>();
            ThemeManager.Instance.OnChangeTheme += OnChangeTheme;
        }

        private void OnChangeTheme(Theme theme)
        {
            var e = Entries.Instance.FindEntryByKeyAndTheme(key, theme.Id);
            if (e.Type != EntryType.String)
            {
                Debug.LogWarning($"{nameof(ColorThemeObserver)} expects type of {EntryType.String}, but you set {e.Type}");
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