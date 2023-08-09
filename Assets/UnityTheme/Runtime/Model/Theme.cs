using System;

namespace UnityTheme.Model
{
    [Serializable]
    public struct Theme
    {
        public readonly int Id;
        private string _name;
        [NonSerialized]
        public ThemeEvents.OnChangeName OnChangeName;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnChangeName?.Invoke(_name);
            }
        }

        public Theme(int id, string name)
        {
            Id = id;
            _name = name;
            OnChangeName = null;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class ThemeEvents
    {
        public delegate void OnChangeName(string name);
    }
}