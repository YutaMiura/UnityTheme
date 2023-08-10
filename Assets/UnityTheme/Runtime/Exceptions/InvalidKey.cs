using System;

namespace UnityTheme.Runtime.Exceptions
{
    public class InvalidKey : Exception
    {
        public InvalidKey() : base(nameof(InvalidKey))
        {

        }

        public InvalidKey(Exception ex) : base(nameof(InvalidKey), ex)
        {

        }

        public InvalidKey(string msg, Exception ex) : base(msg, ex)
        {

        }

        public InvalidKey(string msg) : base(msg)
        {

        }
    }
}