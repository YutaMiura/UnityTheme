using System;
using UnityEngine;

namespace UnityTheme.Model
{
    [Serializable]
    public class GameObjectActivateEntry: Entry<bool>, IDisposable
    {
        [SerializeField]
        private int themeId;
        [SerializeField]
        private string key;
        [SerializeField]
        private bool value;

        public GameObjectActivateEntry(int themeId, string key, bool value)
        {
            this.themeId = themeId;
            this.value = value;
            this.key = key;
        }

        public static GameObjectActivateEntry CreateDraft(int themeId)
        {
            return CreateDraftWithKey(themeId, "");
        }

        public static GameObjectActivateEntry CreateDraftWithKey(int themeId, string key)
        {
            return new GameObjectActivateEntry(themeId, key, false);
        }

        public override string ToString()
        {
            return $"{nameof(GradientEntry)} [Theme:{ThemeId} Key:{Key}]";
        }

        public void Dispose()
        {
            ;
        }

        public override int ThemeId => themeId;
        public override string Key => key;
        public override bool Value => value;
        public override EntryType Type => EntryType.GameObjectActive;
    }
}