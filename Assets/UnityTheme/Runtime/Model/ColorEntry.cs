using System;
using UnityEngine;

namespace UnityTheme.Model
{
    [Serializable]
    public sealed class ColorEntry : Entry<Color>, IDisposable
    {
        [SerializeField]
        private int themeId;
        [SerializeField]
        private Color value;
        [SerializeField]
        private string key;
        
        public override int ThemeId => themeId;
        public override Color Value => value;
        public override string Key => key;
        public override EntryType Type => EntryType.Color;

        public ColorEntry(int themeId, string key, Color value)
        {
            this.themeId = themeId;
            this.key = key;
            this.value = value;
        }

        public static ColorEntry CreateDraft(int themeId)
        {
            return new ColorEntry(themeId, "", Color.black);
        }
        
        public override string ToString()
        {
            return $"{nameof(ColorEntry)} [Theme:{ThemeId} Key:{Key}]";
        }

        public void Dispose()
        {
            // do nothing.
        }
    }
}