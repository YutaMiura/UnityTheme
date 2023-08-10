using System;
using UnityEngine;

namespace UnityTheme.Model
{
    [Serializable]
    public struct Theme
    {
        [SerializeField]
        private int id;
        [SerializeField]
        private string name;
        [NonSerialized]
        public ThemeEvents.OnChangeName OnChangeName;

        public int Id => id;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnChangeName?.Invoke(name);
            }
        }

        public Theme(int id, string name)
        {
            this.id = id;
            this.name = name;
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