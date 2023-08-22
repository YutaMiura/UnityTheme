using UnityEngine;
using UnityTheme.Model;

namespace UnityTheme.Runtime.Components
{
    public abstract class ThemeObserver<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField]
        protected string key;
        
        private T _target;
        
        protected T Target => _target ??= GetComponent<T>();
        
        private void Awake()
        {
            _target = GetComponent<T>();
            ThemeManager.Instance.OnChangeTheme += ChangeTheme;

            ChangeTheme(ThemeManager.Instance.SelectedTheme);
        }
        
        
        public abstract void ChangeTheme(Theme theme);

        private void OnDestroy()
        {
            ThemeManager.Instance.OnChangeTheme -= ChangeTheme;
        }
    }
}