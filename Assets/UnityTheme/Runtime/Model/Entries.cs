using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityTheme.Runtime.Exceptions;

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

#if UNITY_EDITOR
        private void OnEnable()
        {
            Debug.Log($"OnEnable {nameof(Entries)}");
        }
#endif

        private void OnDestroy()
        {
            Debug.Log($"{nameof(Themes)} OnDestroy");
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

        public void AddEntry(Texture2DEntry entry)
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