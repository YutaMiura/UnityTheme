using UnityEngine;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    public abstract class ThemeObserverBase : MonoBehaviour
    {
        public abstract void ChangeTheme(Theme theme);
    }
}