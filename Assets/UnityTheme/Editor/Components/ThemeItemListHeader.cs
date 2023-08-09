using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityTheme.Editor
{
    public class ThemeItemListHeader : VisualElement
    {         
        private string _columns;
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
                var uss = Resources.Load<StyleSheet>("HeaderStyle");
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
                }
            }
        }
    }
}