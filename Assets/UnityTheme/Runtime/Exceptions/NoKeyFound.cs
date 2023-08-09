using System;

namespace UnityTheme.Runtime.Exceptions
{
    public class NoKeyFound : Exception
    {
        public NoKeyFound() : base(nameof(NoKeyFound))
        {

        }

        public NoKeyFound(Exception ex) : base(nameof(NoKeyFound), ex)
        {

        }

        public NoKeyFound(string msg, Exception ex) : base(msg, ex)
        {

        }

        public NoKeyFound(string msg) : base(msg)
        {

        }
    }
}