using System;
using UnityEngine;

namespace UnityTheme.Model
{
    [Serializable]
    public sealed class Texture2DEntry : Entry<Texture2D>, IDisposable
    {
        [SerializeField]
        private int themeId;
        [SerializeField]
        private string key;
        [SerializeField]
        private Texture2D value;
        
        public override int ThemeId => themeId;

        public override string Key => key;

        public override Texture2D Value => value;
        public override EntryType Type => EntryType.Texture2D;

        public Texture2DEntry(int themeId, string key, Texture2D tex)
        {
            this.themeId = themeId;
            this.value = tex;
            this.key = key;            
        }

        public static Texture2DEntry CreateDraft(int themeId)
        {
            return new Texture2DEntry(themeId, "", null);
        }

        public override string ToString()
        {
            return $"{nameof(Texture2DEntry)} [Theme:{ThemeId} Key:{Key}]";
        }

        public void Dispose()
        {
            value = null;
        }
    }
}