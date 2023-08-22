using UnityEngine;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    [AddComponentMenu("UI/UnityTheme/Observers/GameObjectActiveThemeObserver")]
    public class GameObjectActiveThemeObserver : ThemeObserver<MonoBehaviour>
    {
        public override void ChangeTheme(Theme theme)
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
    }
}