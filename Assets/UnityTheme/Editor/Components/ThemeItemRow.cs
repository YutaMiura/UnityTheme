using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityTheme.Editor.Styles;
using UnityTheme.Model;

namespace UnityTheme.Editor
{
    public class ThemeItemRow : VisualElement
    {
        public EntryUnion[] Entries { get; private set; }
        public ThemeItemRowEvents.OnDeleteEntry OnDeleteEntry;

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

            var minusButton = new Button();
            minusButton.name = "minusEntryButton";
            minusButton.text = "-";
            minusButton.AddToClassList("minusEntryButton");
            var uss = StyleLoader.LoadStyle();
            minusButton.styleSheets.Add(uss);
            minusButton.clicked += () => {
                var r = EditorUtility.DisplayDialog("Confirm delete entry", "Are you sure delete item?", "OK", "Cancel");
                if (r)
                {
                    OnDeleteEntry?.Invoke(Key);    
                }
            };
            hierarchy.Add(minusButton);
            
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
                    case EntryType.Texture:
                        item.texture = e.TextureEntry.Value;
                        item.OnChangeTexture = t => OnChangeTexture(e.ThemeId, t);
                        break;
                    case EntryType.Gradient:
                        item.gradient = e.GradientEntry.Value;
                        item.OnChangeGradient = g => OnChangeGradient(e.ThemeId, g);
                        break;
                    case EntryType.GameObjectActive:
                        item.GameObjectActivate = e.GameObjectActivateEntry.Value;
                        item.OnChangeActivate = v => OnChangeActivate(e.ThemeId, v);
                        break;
                }
                
                hierarchy.Add(item);
            }
        }

        private void OnChangeTexture(int themeId, Texture texture)
        {
            for (var i = 0; i < Entries.Length; i++)
            {
                if (Entries[i].ThemeId == themeId)
                {
                    if (Entries[i].Type != EntryType.Texture) throw new SystemException($"Entry must be same type. expected {EntryType.Texture}, but {Entries[i].Type}");
                    var newEntry = new TextureEntry(Entries[i].ThemeId, Entries[i].Key, texture);
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

        private void OnChangeGradient(int themeId, Gradient gradient)
        {
            for (var i = 0; i < Entries.Length; i++)
            {
                if (Entries[i].ThemeId == themeId)
                {
                    if (Entries[i].Type != EntryType.Gradient) throw new SystemException($"Entry must be same type. expected {EntryType.Gradient}, but {Entries[i].Type}");
                    var newEntry = new GradientEntry(Entries[i].ThemeId, Entries[i].Key, gradient);
                    Entries[i].Dispose();
                    Entries[i] = new EntryUnion(newEntry);
                    return;
                }
            }
            
            throw new SystemException($"Entry has not theme:{themeId}");
        }

        private void OnChangeActivate(int themeId, bool value)
        {
            for (var i = 0; i < Entries.Length; i++)
            {
                if (Entries[i].ThemeId == themeId)
                {
                    if (Entries[i].Type != EntryType.GameObjectActive) throw new SystemException($"Entry must be same type. expected {EntryType.GameObjectActive}, but {Entries[i].Type}");
                    var newEntry = new GameObjectActivateEntry(Entries[i].ThemeId, Entries[i].Key, value);
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

    public class ThemeItemRowEvents
    {
        public delegate void OnDeleteEntry(string key);
    }
}