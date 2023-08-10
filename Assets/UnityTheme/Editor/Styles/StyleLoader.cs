using UnityEngine;
using UnityEngine.UIElements;

namespace UnityTheme.Editor.Styles
{
    public static class StyleLoader
    {
        public static StyleSheet LoadStyle()
        {
            return Resources.Load<StyleSheet>("UnityThemeStyles");            
        }
    }
}