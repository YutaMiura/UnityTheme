using System;

namespace UnityTheme.Model
{     
    [Serializable]
    public abstract class Entry<T>
    {
        public abstract int ThemeId { get; }
        public abstract string Key { get; }
        public abstract T Value { get; }
        public abstract EntryType Type { get; }
    }
}