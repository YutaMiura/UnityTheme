using UnityEngine;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    [AddComponentMenu("UI/UnityTheme/Observers/GameObjectActiveThemeObserver")]
    public class GameObjectActiveThemeObserver : MonoBehaviour
    {
        [SerializeField]
        private string key;

        private void Awake()
        {
            ThemeManager.Instance.OnChangeTheme += OnChangeTheme;
            
            OnChangeTheme(ThemeManager.Instance.SelectedTheme);
        }

        private void OnChangeTheme(Theme theme)
        {
            var e = Entries.Instance.FindEntryByKeyAndTheme(key, theme.Id);
            if (e.Type != EntryType.GameObjectActive)
            {
                Debug.LogWarning(
                    $"{nameof(GameObjectActiveThemeObserver)} expects type of {EntryType.GameObjectActive}, but you set {e.Type}");
                return;
            }

            gameObject.SetActive(e.GameObjectActivateEntry.Value);
        }

        private void OnDestroy()
        {
            ThemeManager.Instance.OnChangeTheme -= OnChangeTheme;
        }
    }
}