using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityTheme.Editor.Extentions;
using UnityTheme.Model;
using UnityTheme.Runtime.Exceptions;

namespace UnityTheme.Editor
{
    public sealed class ThemeEditorWindow : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset uxmlAsset;

        private const string WindowTitle = "Theme Editor";

        private ScrollView _rows;
        

        private void CreateGUI()
        {
            Debug.Log("call CreateGUI");
            var uxml = uxmlAsset.Instantiate();
            rootVisualElement.Add(uxml);
            _rows = rootVisualElement.Q<ScrollView>("rows");

            var controls = rootVisualElement.Q<VisualElement>("controls");
            var createNewThemeButton = rootVisualElement.Q<Button>("CreateTheme");
            var createNewEntriesButton = rootVisualElement.Q<Button>("CreateEntries");
            var createNewThemeButtonDescription = rootVisualElement.Q<Label>("CreateNewThemeDescription");
            var createNewEntriesButtonDescription = rootVisualElement.Q<Label>("CreateNewEntriesDescription");
            
            var header = rootVisualElement.Q<ThemeItemListHeader>("header");
            if (IsReady)
            {
                _rows.style.display = DisplayStyle.Flex;
                header.style.display = DisplayStyle.Flex;
                controls.style.display = DisplayStyle.Flex;
                createNewThemeButton.style.display = DisplayStyle.None;
                createNewThemeButtonDescription.style.display = DisplayStyle.None;
                createNewEntriesButton.style.display = DisplayStyle.None;
                createNewEntriesButtonDescription.style.display = DisplayStyle.None;
                var headerList = new List<string>()
                {
                    "Key"
                };
                headerList.AddRange(Themes.Instance.All.Select(t => t.Name));
                header.columns = string.Join(",", headerList);
                
                foreach (var key in Entries.Instance.AllKeys)
                {
                    var entries = Entries.Instance.EntriesByKey(key);
                    var row = new ThemeItemRow();
                    row.SetEntries(entries.ToArray());
                    _rows.Add(row);
                }
                
            } 
            if (!IsThemesReady)
            {
                _rows.style.display = DisplayStyle.None;
                header.style.display = DisplayStyle.None;
                controls.style.display = DisplayStyle.None;
                createNewThemeButton.style.display = DisplayStyle.Flex;
                createNewThemeButtonDescription.style.display = DisplayStyle.Flex;
            } 
            if (!IsEntriesReady)
            {
                _rows.style.display = DisplayStyle.None;
                header.style.display = DisplayStyle.None;
                controls.style.display = DisplayStyle.None;
                createNewEntriesButton.style.display = DisplayStyle.Flex;
                createNewEntriesButtonDescription.style.display = DisplayStyle.Flex;
            }
            
            createNewThemeButton.clicked += OnClickCreateNewThemeFirst;
            createNewEntriesButton.clicked += OnClickCreateEntriesFirst;

            var addButton = rootVisualElement.Q<Button>("AddButton");

            addButton.clicked += OnClickAdd;
            
            rootVisualElement.MarkDirtyRepaint();
        }

        private void OnClickCreateEntriesFirst()
        {
            EntriesFactory.CreateDefault();
        }

        private void OnClickCreateNewThemeFirst()
        {
            ThemesFactory.CreateDefault();
        }

        private void OnClickAdd()
        {
            var d = rootVisualElement.Q<EnumField>("AddCategory");
            var e = (EntryType)d.value;
            var entries = Themes.Instance.All.Select(t => {
                switch (e)
                {
                    case EntryType.Color:
                        return new EntryUnion(ColorEntry.CreateDraft(t.Id));
                    case EntryType.Sprite:
                        return new EntryUnion(SpriteEntry.CreateDraft(t.Id));
                    case EntryType.String:
                        return new EntryUnion(StringEntry.CreateDraft(t.Id));
                    case EntryType.Texture2D:
                        return new EntryUnion(Texture2DEntry.CreateDraft(t.Id));
                    default:
                        throw new NotSupportedException($"{e} is not supported EntryType.");
                }
            }).ToArray();
            var row = new ThemeItemRow();
            row.SetEntries(entries);
            
            _rows.Add(row);
            
            _rows.MarkDirtyRepaint();
        }

        private void OnGUI()
        {
            
            var e = Event.current;
            if (e == null)
            {
                return;
            }

            if (!IsReady)
            {
                return;
            }

            if (e.IsSaveClicked())
            {
                Save();
                e.Use();
            }
        }

        private bool IsThemesReady => Themes.Instance != null;
        private bool IsEntriesReady => Entries.Instance != null;
        private bool IsReady => IsThemesReady && IsEntriesReady;

        private void Save()
        {
            Debug.Log("Save");
            if (!IsEntriesReady) throw new ArgumentException($"{nameof(Entries)} is not ready.");
            if (_rows == null) throw new SystemException($"{nameof(_rows)} is not ready.");

            var rows = _rows.Children().OfType<ThemeItemRow>();
            var themeItemRows = rows as ThemeItemRow[] ?? rows.ToArray();
            if (themeItemRows.HasDuplicatedKey()) throw new DuplicateKey("Save failed. duplicated key found.");
            
            
            Entries.Instance.ClearEntry();
            foreach (var row in themeItemRows)
            {
                foreach (var e in row.Entries)
                {
                    Entries.Instance.AddEntry(e);    
                }
            }

            EntriesRepository.Save();
        }


        private void OnEnable()
        {
            Debug.Log("call OnEnable");
        }

        [MenuItem("Window/UnityTheme/Theme Editor")]
        public static void Open()
        {
            GetWindow<ThemeEditorWindow>(WindowTitle);
        }
    }
}