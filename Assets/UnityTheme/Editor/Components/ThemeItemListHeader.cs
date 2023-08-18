using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;
using UnityTheme.Editor.Styles;
using UnityTheme.Model;

namespace UnityTheme.Editor
{
    public class ThemeItemListHeader : VisualElement
    {
        private const string HEADER_KEY = "Key";
        private string _columns;
        public ThemeListHeaderEvents.OnClickAddTheme OnClickAddTheme;
        public ThemeListHeaderEvents.OnClickRemoveTheme OnClickRemoveTheme;
        public string columns
        {
            get => _columns;
            set
            {
                ClearAllColumns();
                _columns = value;
                var columns = value.Split(",");
                var uss = StyleLoader.LoadStyle();
                var idAndNames = columns.Select(c => c.Split(':', 2)).ToArray();
                
                var keyHeader = new TextField();
                keyHeader.value = HEADER_KEY;
                hierarchy.Add(keyHeader);
                
                foreach (var idAndName in idAndNames)
                {
                    var id = int.Parse(idAndName[0]);
                    var name = idAndName[1];
                    var tf = new TextField();
                    tf.value = name;
                    tf.Add(CreateRemoveThemeButton(uss, () => OnClickRemoveTheme?.Invoke(id)));
                    hierarchy.Add(tf);
                }
                
                hierarchy.Add(AddThemeButton(() => OnClickAddTheme?.Invoke(Themes.Instance.Count + 1)));
            }
        }
        
        void ClearAllColumns()
        {
            hierarchy.Clear();
        }
        
        public new class UxmlFactory : UxmlFactory<ThemeItemListHeader, UxmlTraits>{}

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            private UxmlStringAttributeDescription _columnsAttr = new UxmlStringAttributeDescription()
            {
                name = "columns"
            };
            
            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                var uss = StyleLoader.LoadStyle();
                if (ve is ThemeItemListHeader header)
                {
                    var value = _columnsAttr.GetValueFromBag(bag, cc);
                    header.ClearAllColumns();
                    header.AddToClassList("header");
                    header.styleSheets.Add(uss);
                    var columns = value.Split(",");
                    foreach (var column in columns)
                    {
                        var tf = new TextField();
                        tf.value = column;
                        header.hierarchy.Add(tf);
                    }
                    
                    header.hierarchy.Add(header.AddThemeButton(() => header.OnClickAddTheme?.Invoke(Themes.Instance.Count + 1)));
                }
            }
        }

        private Button AddThemeButton(Action clicked)
        {
            var uss = StyleLoader.LoadStyle();
            var bt = new Button();
            bt.name = "AddThemeButton";
            bt.text = "+";
            bt.AddToClassList("addThemeButton");
            bt.styleSheets.Add(uss);
            bt.clicked += clicked;
            return bt;
        }

        private Button CreateRemoveThemeButton(StyleSheet uss, Action clicked)
        {
            var removeThemeButton = new Button();
            removeThemeButton.styleSheets.Add(uss);
            removeThemeButton.name = "removeThemeButton";
            removeThemeButton.text = "-";
            removeThemeButton.AddToClassList("removeThemeButton");
            removeThemeButton.clicked += clicked;
            return removeThemeButton;
        }

        public class ThemeListHeaderEvents
        {
            public delegate void OnClickAddTheme(int themeId);

            public delegate void OnClickRemoveTheme(int themeId);
        }
    }
}