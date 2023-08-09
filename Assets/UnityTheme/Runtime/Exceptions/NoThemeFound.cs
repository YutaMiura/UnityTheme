using System;

namespace UnityTheme.Runtime.Exceptions
{
    public class NoThemeFound : Exception
    {
        public NoThemeFound() : base(nameof(NoThemeFound))
        {

        }

        public NoThemeFound(Exception ex) : base(nameof(NoThemeFound), ex)
        {

        }

        public NoThemeFound(string msg, Exception ex) : base(msg, ex)
        {

        }

        public NoThemeFound(string msg) : base(msg)
        {

        }
    }
}