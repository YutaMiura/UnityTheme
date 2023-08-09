using System;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityTheme.Model;

namespace UnityTheme.Editor
{
    public class ThemeItemColumnItem : VisualElement
    {
        private const int ContentWidth = 120;
        
        public EntryType type { get; set; }
        private Color? _color;
        public Color? color
        {
            get => _color;
            set
            {
                ClearAllAttribute();
                if (value != null)
                {
                    var colorField = new ColorField();
                    colorField.style.width = ContentWidth;
                    colorField.value = value.Value;
                    _color = value;
                    hierarchy.Add(colorField);
                    type = EntryType.Color;    
                }
            }
        }

        private string _text;
        public string text
        {
            get => _text;
            set
            {
                ClearAllAttribute();
                if (value != null)
                {
                    var textField = new TextField();
                    textField.style.width = ContentWidth;
                    textField.value = value;
                    _text = value;
                    hierarchy.Add(textField);
                    type = EntryType.String;    
                }
            }
        }

        private Sprite _sprite;

        public Sprite sprite
        {
            get => _sprite;
            set
            {
                ClearAllAttribute();
                var spriteField = new ObjectField();
                spriteField.style.width = ContentWidth;
                spriteField.objectType = typeof(Sprite);
                spriteField.value = value;
                _sprite = value;
                hierarchy.Add(spriteField);
                type = EntryType.Sprite;
            }
        }

        private Texture2D _texture2D;

        public Texture2D texture2D
        {
            get => _texture2D;
            set
            {
                ClearAllAttribute();
                var texture2DField = new ObjectField();
                texture2DField.style.width = ContentWidth;
                texture2DField.objectType = typeof(Texture2D);
                texture2DField.value = value;
                hierarchy.Add(texture2DField);
                type = EntryType.Texture2D;
            }
        }

        private void ClearAllAttribute()
        {
            _text = null;
            _color = null;
            _sprite = null;
            hierarchy.Clear();
        }
        
        private Texture2D Texture2D;
        
        public new class UxmlFactory : UxmlFactory<ThemeItemColumnItem, UxmlTraits>{}

        public new class UxmlTraits : VisualElement.UxmlTraits
        {

            private readonly UxmlEnumAttributeDescription<EntryType> _typeAttr = new UxmlEnumAttributeDescription<EntryType>()
            {
                name = "type"
            };
            
            private readonly UxmlColorAttributeDescription _colorAttr = new UxmlColorAttributeDescription()
            {
                name = "color"
            };

            private readonly UxmlStringAttributeDescription _strAttr = new UxmlStringAttributeDescription()
            {
                name = "text"
            };
            
            public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
            {
                get { yield break; }
            }

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                if (ve is ThemeItemColumnItem item)
                {
                    var type = _typeAttr.GetValueFromBag(bag, cc);
                    switch (type)
                    {
                        case EntryType.Color:
                            var color = _colorAttr.GetValueFromBag(bag, cc);
                            item.color = color;
                            break;
                        case EntryType.String:
                            var text = _strAttr.GetValueFromBag(bag, cc);
                            item.text = text;
                            break;
                        default:
                            throw new NotSupportedException($"{type} is not supported.");                         
                    }
                }
                else
                {
                    throw new AggregateException($"{nameof(ThemeItemColumnItem)} initialize error.");
                }
            }
        }
    }
}