using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
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
                        break;
                    case EntryType.Sprite:
                        item.sprite = e.SpriteEntry.Value;
                        break;
                    case EntryType.String:
                        item.text = e.StringEntry.Value;
                        break;
                    case EntryType.Texture2D:
                        item.texture2D = e.Texture2DEntry.Value;
                        break;
                } 
                hierarchy.Add(item);
            }
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
                var uss = Resources.Load<StyleSheet>("HeaderStyle");
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