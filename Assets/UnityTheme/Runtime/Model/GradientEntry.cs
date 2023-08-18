using System;
using UnityEngine;

namespace UnityTheme.Model
{
    [Serializable]
    public class GradientEntry : Entry<Gradient>, IDisposable
    {
        [SerializeField]
        private int themeId;
        [SerializeField]
        private string key;
        [SerializeField]
        private Gradient value;
        
        public override int ThemeId => themeId;
        public override string Key => key;
        public override Gradient Value => value;
        public override EntryType Type => EntryType.Gradient;

        public GradientEntry(int themeId, string key, Gradient gradient)
        {
            this.themeId = themeId;
            this.value = gradient;
            this.key = key;
        }

        public static GradientEntry CreateDraft(int themeId)
        {
            return CreateDraftWithKey(themeId, "");
        }

        public static GradientEntry CreateDraftWithKey(int themeId, string key)
        {
            return new GradientEntry(themeId, key, new Gradient());
        }
        
        public override string ToString()
        {
            return $"{nameof(GradientEntry)} [Theme:{ThemeId} Key:{Key}]";
        }
        
        public void Dispose()
        {
            value = null;
        }
    }
}