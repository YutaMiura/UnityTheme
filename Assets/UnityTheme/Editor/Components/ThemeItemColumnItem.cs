using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using UnityTheme.Model;

namespace UnityTheme.Editor
{
    public class ThemeItemColumnItem : VisualElement
    {
        private const int ContentWidth = 120;

        public ThemeItemColumnItemEvents.OnChangeText OnChangeText;
        public ThemeItemColumnItemEvents.OnChangeColor OnChangeColor;
        public ThemeItemColumnItemEvents.OnChangeSprite OnChangeSprite;
        public ThemeItemColumnItemEvents.OnChangeTexture OnChangeTexture;
        public ThemeItemColumnItemEvents.OnChangeGradient OnChangeGradient;
        public ThemeItemColumnItemEvents.OnChangeActivate OnChangeActivate;
        
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
                    colorField.RegisterValueChangedCallback(ev => {
                        OnChangeColor?.Invoke(ev.newValue);
                    });
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
                    textField.RegisterValueChangedCallback(ev => {
                        OnChangeText?.Invoke(ev.newValue);
                    });
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
                spriteField.RegisterValueChangedCallback(ev => {
                    if (ev.newValue == null)
                    {
                        OnChangeSprite?.Invoke(null);
                    }
                    else
                    {
                        OnChangeSprite?.Invoke(ev.newValue as Sprite);    
                    }
                });
                _sprite = value;
                hierarchy.Add(spriteField);
                type = EntryType.Sprite;
            }
        }

        private Texture _texture;

        public Texture texture
        {
            get => _texture;
            set
            {
                ClearAllAttribute();
                var textureField = new ObjectField();
                textureField.style.width = ContentWidth;
                textureField.objectType = typeof(Texture);
                textureField.value = value;
                textureField.RegisterValueChangedCallback(ev => {
                    if (ev.newValue == null)
                    {
                        OnChangeTexture?.Invoke(null);
                    }
                    else
                    {
                        OnChangeTexture?.Invoke(ev.newValue as Texture);
                    }
                });
                hierarchy.Add(textureField);
                type = EntryType.Texture;
            }
        }

        private Gradient _gradient;

        public Gradient gradient
        {
            get => _gradient;
            set
            {
                ClearAllAttribute();
                var gradientField =  new GradientField();                 
                gradientField.style.width = ContentWidth;
                gradientField.value = value;
                gradientField.RegisterValueChangedCallback(ev => {
                    if (ev.newValue == null)
                    {
                        OnChangeGradient?.Invoke(null);
                    }
                    else
                    {
                        OnChangeGradient?.Invoke(ev.newValue);
                    }
                });
                hierarchy.Add(gradientField);
                type = EntryType.Gradient;
            }
        }

        private bool _gameObjectActivate;

        public bool GameObjectActivate
        {
            get => _gameObjectActivate;
            set
            {
                ClearAllAttribute();
                var toggle = new Toggle();
                toggle.style.width = ContentWidth;
                toggle.value = value;
                toggle.RegisterValueChangedCallback(ev => {
                    OnChangeActivate?.Invoke(ev.newValue);
                });
                hierarchy.Add(toggle);
                type = EntryType.GameObjectActive;
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

        public class ThemeItemColumnItemEvents
        {
            public delegate void OnChangeText(string text);
            public delegate void OnChangeColor(Color color);
            public delegate void OnChangeSprite(Sprite sprite);
            public delegate void OnChangeTexture(Texture texture2D);
            public delegate void OnChangeGradient(Gradient gradient);
            public delegate void OnChangeActivate(bool value);
        }
    }
}