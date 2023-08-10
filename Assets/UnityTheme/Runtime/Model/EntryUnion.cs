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
        private Texture2DEntry texture2DEntry;
        
        public ColorEntry ColorEntry => colorEntry;
        public StringEntry StringEntry => stringEntry;
        public SpriteEntry SpriteEntry => spriteEntry;
        public Texture2DEntry Texture2DEntry => texture2DEntry;

        public string Key
        {
            get
            {
                return Type switch
                {
                    EntryType.Color => ColorEntry.Key,
                    EntryType.Sprite => SpriteEntry.Key,
                    EntryType.String => StringEntry.Key,
                    EntryType.Texture2D => Texture2DEntry.Key,
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
                    EntryType.Texture2D => Texture2DEntry.ThemeId,
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

        public EntryUnion(Texture2DEntry texture2DEntry)
        {
            type = texture2DEntry.Type;
            this.texture2DEntry = texture2DEntry;
        }

        public void Dispose()
        {
            ColorEntry?.Dispose();
            SpriteEntry?.Dispose();
            StringEntry?.Dispose();
            Texture2DEntry?.Dispose();
        }

        public override string ToString()
        {
            if (Type == EntryType.Color) return ColorEntry?.ToString();
            if (Type == EntryType.Sprite) return SpriteEntry?.ToString();
            if (Type == EntryType.String) return StringEntry?.ToString();
            if (Type == EntryType.Texture2D) return Texture2DEntry?.ToString();
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

            if (src.Type == EntryType.Texture2D)
            {
                return new EntryUnion(new Texture2DEntry(src.ThemeId, key, src.Texture2DEntry.Value));
            }

            throw new ArgumentException($"{src.Type} is not supported.");
        }
    }
}