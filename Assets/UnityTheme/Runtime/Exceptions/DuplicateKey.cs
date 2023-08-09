using System;

namespace UnityTheme.Runtime.Exceptions
{
    public class DuplicateKey : Exception
    {
        public DuplicateKey() : base(nameof(DuplicateKey))
        {

        }

        public DuplicateKey(Exception ex) : base(nameof(DuplicateKey), ex)
        {

        }

        public DuplicateKey(string msg, Exception ex) : base(msg, ex)
        {

        }

        public DuplicateKey(string msg) : base(msg)
        {

        }
    }
}