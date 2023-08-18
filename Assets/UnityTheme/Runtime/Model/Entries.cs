using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityTheme.Runtime.Exceptions;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityTheme.Model
{
    public class Entries : ScriptableObject
    {
        [SerializeField]
        private List<EntryUnion> entries = new List<EntryUnion>();

        public IReadOnlyCollection<string> AllKeys => entries.Select(e => e.Key).Distinct().ToArray();

        public IReadOnlyList<EntryUnion> EntriesByKey(string key)
        {
            return entries.FindAll(e => e.Key.Equals(key));
        }

        public EntryUnion FindEntryByKeyAndTheme(string key, int themeId)
        {
            var e =entries.FirstOrDefault(e => e.Key.Equals(key) && e.ThemeId == themeId);
            if (e == null) throw new NoEntryFound();
            return e;
        }

        public EntryType EntryTypeByKey(string key)
        {
            var e = entries.FirstOrDefault(e => e.Key.Equals(key));
            if (e == null) throw new NoEntryFound();
            return e.Type;
        }

        public bool TryGetEntryTypeByKey(string key, out EntryType? type)
        {
            var e = entries.FirstOrDefault(e => e.Key.Equals(key));
            if (e == null)
            {
                type = null;
                return false;
            }

            type = e.Type;
            return true;
        }

        private static Entries _instance;

        public static Entries Instance
        {
            get
            {
                if (_instance == null)
                {
#if UNITY_EDITOR
                    _instance = AssetDatabase.FindAssets($"t:{nameof(Entries)}")
                        .Select(x => {
                            var path = AssetDatabase.GUIDToAssetPath(x);
                            return AssetDatabase.LoadAssetAtPath<Entries>(path);
                        })
                        .FirstOrDefault();
#else
                    _instance = CreateInstance<Entries>();
#endif
                }

                return _instance;
            }
        }

        private void OnValidate()
        {
            Debug.Log($"{nameof(Entries)} OnValidate");
        }

        private void Awake()
        {
            Debug.Log($"{nameof(Entries)} Awake");
        }
        
        private void OnEnable()
        {
            Debug.Log($"OnEnable {nameof(Entries)}");
#if !UNITY_EDITOR
            _instance = this;
#endif
        }

        private void OnDestroy()
        {
            Debug.Log($"{nameof(Themes)} OnDestroy");
        }

        public void AddEntriesByNewTheme(Theme theme)
        {
            if (entries.Count == 0)
            {
                return;
            }
            
            foreach (var key in AllKeys)
            {
                if (TryGetEntryTypeByKey(key, out var type))
                {
                    switch (type!)
                    {
                        case EntryType.Color:
                            AddEntry(ColorEntry.CreateDraftWithKey(theme.Id, key));
                            break;
                        case EntryType.Sprite:
                            AddEntry(SpriteEntry.CreateDraftWithKey(theme.Id, key));
                            break;
                        case EntryType.String:
                            AddEntry(StringEntry.CreateDraftWithKey(theme.Id, key));
                            break;
                        case EntryType.Texture:
                            AddEntry(TextureEntry.CreateDraftWithKey(theme.Id, key));
                            break;
                            
                    }
                }
            }
        }

        public void RemoveAllRelatedByRemovedTheme(int themeId)
        {
            entries.RemoveAll(e => e.ThemeId == themeId);
        }

        public void AddEntry(EntryUnion entry)
        {
            ThrowIfDuplicateKey(entry);
            entries.Add(entry);
        }

        public void AddEntry(ColorEntry entry)
        {
            ThrowIfDuplicateKey(entry);
            var e = new EntryUnion(entry);
            entries.Add(e);
        }

        public void AddEntry(StringEntry entry)
        {
            ThrowIfDuplicateKey(entry);
            var e = new EntryUnion(entry);
            entries.Add(e);
        }

        public void AddEntry(SpriteEntry entry)
        {
            ThrowIfDuplicateKey(entry);
            var e = new EntryUnion(entry);
            entries.Add(e);
        }

        public void AddEntry(TextureEntry entry)
        {
            ThrowIfDuplicateKey(entry);
            var e = new EntryUnion(entry);
            entries.Add(e);
        }

        public void RemoveEntry(string key)
        {
            entries.RemoveAll(e => e.Key.Equals(key));
        }

        public void ClearEntry()
        {
            entries.Clear();
        }

        private void ThrowIfDuplicateKey<T>(Entry<T> entry)
        {
            if (IsDuplicateKey(entry))
            {
                throw new DuplicateKey($"{entry.Key} is duplicated.");
            }
        }

        private void ThrowIfDuplicateKey(EntryUnion entry)
        {
            if (IsDuplicateKey(entry))
            {
                throw new DuplicateKey($"{entry.Key} is duplicated.");
            }
        }

        private bool IsDuplicateKey<T>(Entry<T> entry)
        {
            foreach (var e in entries)
            {
                if (e.ThemeId == entry.ThemeId && e.Key.Equals(entry.Key))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsDuplicateKey(EntryUnion entry)
        {
            foreach (var e in entries)
            {
                if (e.ThemeId == entry.ThemeId && e.Key.Equals(entry.Key))
                {
                    return true;
                }
            }

            return false;
        }
    }
}