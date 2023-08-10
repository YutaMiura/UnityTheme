using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using UnityTheme.Editor.Styles;
using UnityTheme.Model;

namespace UnityTheme.Editor
{
    public class ThemeItemRow : VisualElement
    {
        public EntryUnion[] Entries { get; private set; }

        public string Key
        {
            get
            {
                if (Entries.Length == 0) return "";
                return Entries[0].Key;       
            }
        }
        
        public void SetEntries(EntryUnion[] entryUnions)
        {
            if (entryUnions.Length == 0)
            {
                return;
            }

            var key = entryUnions[0].Key;
            var type = entryUnions[0].Type;

            if (!entryUnions.All(e => e.Key.Equals(key)))
            {
                throw new ArgumentException("All entry must has same key.");
            }

            if (entryUnions.Any(e => e.Type != type))
            {
                throw new ArgumentException("All entry must has same EntryType.");
            }

            Entries = entryUnions;
            
            var tf = new TextField();
            tf.value = key;
            tf.RegisterValueChangedCallback(OnChangeKey);
            hierarchy.Add(tf);
            foreach (var e in entryUnions)
            {
                var item = new ThemeItemColumnItem();
                switch (type)
                {
                    case EntryType.Color:
                        item.color = e.ColorEntry.Value;
                        item.OnChangeColor = c => OnChangeColor(e.ThemeId, c);
                        break;
                    case EntryType.Sprite:
                        item.sprite = e.SpriteEntry.Value;
                        item.OnChangeSprite = s => OnChangeSprite(e.ThemeId, s);
                        break;
                    case EntryType.String:
                        item.text = e.StringEntry.Value;
                        item.OnChangeText = t => OnChangeText(e.ThemeId, t);
                        break;
                    case EntryType.Texture2D:
                        item.texture2D = e.Texture2DEntry.Value;
                        item.OnChangeTexture2D = t => OnChangeTexture2D(e.ThemeId, t);
                        break;
                }
                
                hierarchy.Add(item);
            }
        }

        private void OnChangeTexture2D(int themeId, Texture2D texture2d)
        {
            for (var i = 0; i < Entries.Length; i++)
            {
                if (Entries[i].ThemeId == themeId)
                {
                    if (Entries[i].Type != EntryType.Texture2D) throw new SystemException($"Entry must be same type. expected {EntryType.Texture2D}, but {Entries[i].Type}");
                    var newEntry = new Texture2DEntry(Entries[i].ThemeId, Entries[i].Key, texture2d);
                    Entries[i].Dispose();
                    Entries[i] = new EntryUnion(newEntry);
                    return;
                }
            }
            
            throw new SystemException($"Entry has not theme:{themeId}");
        }

        private void OnChangeSprite(int themeId, Sprite sprite)
        {
            for (var i = 0; i < Entries.Length; i++)
            {
                if (Entries[i].ThemeId == themeId)
                {
                    if (Entries[i].Type != EntryType.Sprite) throw new SystemException($"Entry must be same type. expected {EntryType.Sprite}, but {Entries[i].Type}");
                    var newEntry = new SpriteEntry(Entries[i].ThemeId, Entries[i].Key, sprite);
                    Entries[i].Dispose();
                    Entries[i] = new EntryUnion(newEntry);
                    return;
                }
            }
            
            throw new SystemException($"Entry has not theme:{themeId}");
        }

        private void OnChangeText(int themeId, string text)
        {
            for (var i = 0; i < Entries.Length; i++)
            {
                if (Entries[i].ThemeId == themeId)
                {
                    if (Entries[i].Type != EntryType.String) throw new SystemException($"Entry must be same type. expected {EntryType.String}, but {Entries[i].Type}");
                    var newEntry = new StringEntry(Entries[i].ThemeId, Entries[i].Key, text);
                    Entries[i].Dispose();
                    Entries[i] = new EntryUnion(newEntry);
                    return;
                }
            }
            
            throw new SystemException($"Entry has not theme:{themeId}");
        }

        private void OnChangeColor(int themeId, Color color)
        {
            for (var i = 0; i < Entries.Length; i++)
            {
                if (Entries[i].ThemeId == themeId)
                {
                    if (Entries[i].Type != EntryType.Color) throw new SystemException($"Entry must be same type. expected {EntryType.Color}, but {Entries[i].Type}");
                    var newEntry = new ColorEntry(Entries[i].ThemeId, Entries[i].Key, color);
                    Entries[i].Dispose();
                    Entries[i] = new EntryUnion(newEntry);
                    return;
                }
            }
            
            throw new SystemException($"Entry has not theme:{themeId}");
        }

        void OnChangeKey(ChangeEvent<string> ev)
        {
            Debug.Log($"{ev.newValue}");
            var newEntries = Entries.Select(e => e.CopyWithKey(ev.newValue));
            foreach (var e in Entries)
            {
                e.Dispose();
            }
            Entries = newEntries.ToArray();
        }
        
        void ClearAllColumns()
        {
            hierarchy.Clear();
        }
        
        public new class UxmlFactory : UxmlFactory<ThemeItemRow, UxmlTraits>{}

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }
            
            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var uss = StyleLoader.LoadStyle();
                if (ve is ThemeItemRow row)
                {
                    row.name = "row";
                    row.ClearAllColumns();
                    row.ClearClassList();
                    row.AddToClassList("row");
                    row.styleSheets.Clear();
                    row.styleSheets.Add(uss);
                }
            }
        }
    }
}