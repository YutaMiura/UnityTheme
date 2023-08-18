using System;
using UnityEngine;

namespace UnityTheme.Model
{
    [Serializable]
    public sealed class SpriteEntry : Entry<Sprite>, IDisposable
    {
        [SerializeField]
        private int themeId;
        [SerializeField]
        private string key;
        [SerializeField]
        private Sprite value;


        public override int ThemeId => themeId;

        public override string Key => key;

        public override Sprite Value => value;
        public override EntryType Type => EntryType.Sprite;

        public SpriteEntry(int themeId, string key, Sprite value)
        {
            this.themeId = themeId;
            this.key = key;
            this.value = value;
        }

        public static SpriteEntry CreateDraft(int themeId)
        {
            return CreateDraftWithKey(themeId, "");
        }

        public static SpriteEntry CreateDraftWithKey(int themeId, string key)
        {
            return new SpriteEntry(themeId, key, null);
        }
        
        public override string ToString()
        {
            return $"{nameof(SpriteEntry)} [Theme:{ThemeId} Key:{Key}]";
        }

        public void Dispose()
        {
            value = null;
        }
    }
}