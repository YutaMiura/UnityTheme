using UnityEngine;

namespace UnityTheme.Editor.Extentions
{
    public static class EventExtensions
    {
        public static bool IsSaveClicked(this Event e)
        {
            return e.command && e.type == EventType.KeyDown && e.keyCode == KeyCode.S;
        }
    }
}