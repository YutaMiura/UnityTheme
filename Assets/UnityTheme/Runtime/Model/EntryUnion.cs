using System;
using UnityEngine;

namespace UnityTheme.Model
{
    [Serializable]
    public class EntryUnion : IDisposable
    {
        [SerializeField]
        private EntryType type;
        public EntryType Type => type;

        [SerializeField]
        private ColorEntry colorEntry;
        [SerializeField]
        private StringEntry stringEntry;
        [SerializeField]
        private SpriteEntry spriteEntry;
        [SerializeField]
        private TextureEntry textureEntry;
        [SerializeField]
        private GradientEntry gradientEntry;
        
        public ColorEntry ColorEntry => colorEntry;
        public StringEntry StringEntry => stringEntry;
        public SpriteEntry SpriteEntry => spriteEntry;
        public TextureEntry TextureEntry => textureEntry;
        public GradientEntry GradientEntry => gradientEntry;

        public string Key
        {
            get
            {
                return Type switch
                {
                    EntryType.Color => ColorEntry.Key,
                    EntryType.Sprite => SpriteEntry.Key,
                    EntryType.String => StringEntry.Key,
                    EntryType.Texture => TextureEntry.Key,
                    EntryType.Gradient => GradientEntry.Key,
                    _ => throw new ArgumentException($"{Type} is not supported.")
                };
            }
        }

        public int ThemeId
        {
            get
            {
                return Type switch
                {
                    EntryType.Color => ColorEntry.ThemeId,
                    EntryType.Sprite => SpriteEntry.ThemeId,
                    EntryType.String => StringEntry.ThemeId,
                    EntryType.Texture => TextureEntry.ThemeId,
                    EntryType.Gradient => GradientEntry.ThemeId,
                    _ => throw new ArgumentException($"{Type} is not supported.")
                };
            }
        }
        
        private EntryUnion(){}

        public EntryUnion(ColorEntry colorEntry)
        {
            type = colorEntry.Type;
            this.colorEntry = colorEntry;
        }

        public EntryUnion(StringEntry stringEntry)
        {
            type = stringEntry.Type;
            this.stringEntry = stringEntry;
        }

        public EntryUnion(SpriteEntry spriteEntry)
        {
            type = spriteEntry.Type;
            this.spriteEntry = spriteEntry;
        }

        public EntryUnion(TextureEntry textureEntry)
        {
            type = textureEntry.Type;
            this.textureEntry = textureEntry;
        }

        public EntryUnion(GradientEntry gradientEntry)
        {
            type = EntryType.Gradient;
            this.gradientEntry = gradientEntry;
        }

        public void Dispose()
        {
            ColorEntry?.Dispose();
            SpriteEntry?.Dispose();
            StringEntry?.Dispose();
            TextureEntry?.Dispose();
            GradientEntry?.Dispose();
        }

        public override string ToString()
        {
            if (Type == EntryType.Color) return ColorEntry?.ToString();
            if (Type == EntryType.Sprite) return SpriteEntry?.ToString();
            if (Type == EntryType.String) return StringEntry?.ToString();
            if (Type == EntryType.Texture) return TextureEntry?.ToString();
            if (Type == EntryType.Gradient) return GradientEntry?.ToString();
            return "EntryUnion has no Type.";
        }
    }

    public static class EntryUnionExtensions
    {
        public static EntryUnion CopyWithKey(this EntryUnion src, string key)
        {
            if (src.Type == EntryType.Color)
            {
                return new EntryUnion(new ColorEntry(src.ThemeId, key, src.ColorEntry.Value));
            }

            if (src.Type == EntryType.Sprite)
            {
                return new EntryUnion(new SpriteEntry(src.ThemeId, key, src.SpriteEntry.Value));
            }

            if (src.Type == EntryType.String)
            {
                return new EntryUnion(new StringEntry(src.ThemeId, key, src.StringEntry.Value));
            }

            if (src.Type == EntryType.Texture)
            {
                return new EntryUnion(new TextureEntry(src.ThemeId, key, src.TextureEntry.Value));
            }

            if (src.Type == EntryType.Gradient)
            {
                return new EntryUnion(new GradientEntry(src.ThemeId, key, src.GradientEntry.Value));
            }

            throw new ArgumentException($"{src.Type} is not supported.");
        }
    }
}