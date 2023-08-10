using System;
using UnityEngine;

namespace UnityTheme.Model
{
    [Serializable]
    public sealed class TextureEntry : Entry<Texture>, IDisposable
    {
        [SerializeField]
        private int themeId;
        [SerializeField]
        private string key;
        [SerializeField]
        private Texture value;
        
        public override int ThemeId => themeId;

        public override string Key => key;

        public override Texture Value => value;
        public override EntryType Type => EntryType.Texture;

        public TextureEntry(int themeId, string key, Texture tex)
        {
            this.themeId = themeId;
            this.value = tex;
            this.key = key;            
        }

        public static TextureEntry CreateDraft(int themeId)
        {
            return new TextureEntry(themeId, "", null);
        }

        public override string ToString()
        {
            return $"{nameof(TextureEntry)} [Theme:{ThemeId} Key:{Key}]";
        }

        public void Dispose()
        {
            value = null;
        }
    }
}