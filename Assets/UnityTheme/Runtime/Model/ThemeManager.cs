using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTheme.Runtime.Exceptions;

namespace UnityTheme.Model
{
    public class ThemeManager : MonoBehaviour
    {
        [SerializeField]
        private int _selectedThemeIndex = 0;

        public ThemeManagerEvents.OnChangeTheme OnChangeTheme;

        
        private static ThemeManager _instance;
        
        public static ThemeManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    var go = new GameObject("ThemeManager");
                    _instance = go.AddComponent<ThemeManager>();                     
                }
                return _instance;
            }
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public Theme SelectedTheme
        {
            get
            {
                if (Themes.Instance == null)
                {
                    throw new NoThemeFound("There is no theme. Themes not initialized.");
                }
                
                if (Themes.Instance.IsEmpty)
                {
                    throw new NoThemeFound("There is no theme.");
                }

                return Themes.Instance.All.ElementAt(_selectedThemeIndex);
            }
        }

        private void OnValidate()
        {
            if (_selectedThemeIndex < 0 || AvailableThemes.Count() >= _selectedThemeIndex)
            {
                _selectedThemeIndex = 0;
                return;
            }
            
            OnChangeTheme?.Invoke(AvailableThemes[_selectedThemeIndex]);
        }

        public IReadOnlyList<Theme> AvailableThemes => Themes.Instance.All;

        public void ChangeTheme(int index)
        {
            if (!AvailableThemes.Any())
            {
                throw new NoThemeFound("Theme is empty.");
            }
            
            _selectedThemeIndex = index;
            
            if (_selectedThemeIndex < 0 || AvailableThemes.Count() <= _selectedThemeIndex)
            {
                _selectedThemeIndex = 0;
            }
            
            OnChangeTheme?.Invoke(AvailableThemes[_selectedThemeIndex]);
        }
    }

    public static class ThemeManagerEvents
    {
        public delegate void OnChangeTheme(Theme theme);
    }
}