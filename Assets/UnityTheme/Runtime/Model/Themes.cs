using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
using UnityTheme.Runtime.Components;
#endif
using UnityEngine;
using UnityTheme.Runtime.Exceptions;

namespace UnityTheme.Model
{
    public sealed class Themes : ScriptableObject
    {
        [SerializeField]
        private List<Theme> themes;

        [NonSerialized]
        public ThemesEvent.OnAddTheme OnAddTheme;

        [NonSerialized]
        public ThemesEvent.OnRemoveTheme OnRemoveTheme;

        public IReadOnlyList<Theme> All => themes;

        public bool IsEmpty => themes?.Count == 0;

        public int Count => themes?.Count() ?? 0;

        private static Themes _instance;

        public static Themes Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_EDITOR
                    _instance = AssetDatabase.FindAssets($"t:{nameof(Themes)}")
                        .Select(x => {
                            var path = AssetDatabase.GUIDToAssetPath(x);
                            return AssetDatabase.LoadAssetAtPath<Themes>(path);
                        })
                        .FirstOrDefault();
#else
                    _instance = CreateInstance<Themes>();
#endif
                }

                return _instance;
            }
        }

        private Themes(IEnumerable<Theme> themes)
        {
            this.themes = themes.ToList();
        }

        public Theme Find(int id)
        {
            for (int i = 0; i < themes.Count; i++)
            {
                if (themes[i].Id == id)
                {
                    return themes[i];
                }
            }

            throw new NoThemeFound($"Theme[id:{id}] is not found.");
        }
        
        public Theme Find(string name)
        {
            for (int i = 0; i < themes.Count; i++)
            {
                if (themes[i].Name.Equals(name))
                {
                    return themes[i];
                }
            }

            throw new NoThemeFound($"Theme[name:{name}] is not found.");
        }

        public bool TryFind(string name, out Theme theme)
        {
            for (int i = 0; i < themes.Count; i++)
            {
                if (themes[i].Name.Equals(name))
                {
                    theme = themes[i];
                    return true;
                }
            }

            theme = default;
            return false;
        }

        public Theme AddTheme(string name)
        {
            var newThemeId = 1;
            if (themes == null)
            {
                themes = new List<Theme>();
            }
            else
            {
                newThemeId = themes.Max(t => t.Id) + 1;
            }

            var newTheme = new Theme(newThemeId, name);
            themes.Add(newTheme);
            OnAddTheme?.Invoke(newTheme);

            return newTheme;
        }

        public void RemoveTheme(int id)
        {
            for (int i = 0; i < themes.Count; i++)
            {
                if (themes[i].Id == id)
                {
                    var removeTheme = themes[i];
                    themes.RemoveAt(i);
                    OnRemoveTheme?.Invoke(removeTheme);
                    break;
                }
            }
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            Debug.Log($"{nameof(Themes)} OnValidate");
            if (themes.Count == 0) return;
            ApplyThemeInHierarchy(themes.First());
#endif
        }

        private void Awake()
        {
            Debug.Log($"{nameof(Themes)} Awake");
        }

        private void OnEnable()
        {
            Debug.Log($"{nameof(Themes)} OnEnable");
#if !UNITY_EDITOR
            _instance = this;
#endif
        }

        private void OnDestroy()
        {
            Debug.Log($"{nameof(Themes)} OnDestroy");
        }

#if UNITY_EDITOR
        public void ApplyThemeInHierarchy(Theme theme)
        {
            foreach (var o in FindObjectsOfType<ThemeObserverBase>(true))
            {
                o.ChangeTheme(theme);
            }
        }
#endif
    }

    public class ThemesEvent
    {
        public delegate void OnAddTheme(Theme theme);

        public delegate void OnRemoveTheme(Theme theme);
    }
}