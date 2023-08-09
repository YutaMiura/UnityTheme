using System;
using UnityEngine;

namespace UnityTheme.Model
{
    [Serializable]
    public sealed class StringEntry : Entry<string>, IDisposable
    {
        [SerializeField]
        private int themeId;
        [SerializeField]
        private string key;
        [SerializeField]
        private string value;

        public override int ThemeId => themeId;

        public override string Key => key;
        
        public override string Value => value;
        public override EntryType Type => EntryType.String;

        public StringEntry(int themeId, string key, string value)
        {
            this.themeId = themeId;
            this.key = key;
            this.value = value;
        }

        public static StringEntry CreateDraft(int themeId)
        {
            return new StringEntry(themeId, "", "");
        }

        public override string ToString()
        {
            return $"{nameof(StringEntry)} [Theme:{ThemeId} Key:{Key}]";
        }

        public void Dispose()
        {
            value = null;
        }
    }
}