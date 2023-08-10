using System;

namespace UnityTheme.Runtime.Exceptions
{
    public class NoEntryFound : Exception
    {
        public NoEntryFound() : base(nameof(NoEntryFound))
        {

        }

        public NoEntryFound(Exception ex) : base(nameof(NoEntryFound), ex)
        {

        }

        public NoEntryFound(string msg, Exception ex) : base(msg, ex)
        {

        }

        public NoEntryFound(string msg) : base(msg)
        {

        }
    }
}