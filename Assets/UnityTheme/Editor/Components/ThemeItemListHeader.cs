using System;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityTheme.Editor.Styles;

namespace UnityTheme.Editor
{
    public class ThemeItemListHeader : VisualElement
    {         
        private string _columns;
        public ThemeListHeaderEvents.OnClickThemeAdd OnClickThemeAdd;
        public string columns
        {
            get => _columns;
            set
            {
                ClearAllColumns();
                _columns = value;
                var columns = value.Split(",");
                foreach (var column in columns)
                {
                    var tf = new TextField();
                    tf.value = column;
                    hierarchy.Add(tf);
                }
                
                hierarchy.Add(AddThemeButton(() => OnClickThemeAdd?.Invoke()));
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
                    
                    header.hierarchy.Add(header.AddThemeButton(() => header.OnClickThemeAdd?.Invoke()));
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

        public class ThemeListHeaderEvents
        {
            public delegate void OnClickThemeAdd();
        }
    }
}