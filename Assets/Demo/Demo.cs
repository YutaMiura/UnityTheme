using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityTheme.Model;

namespace UnityTheme.Demo
{
    public class Demo : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _themeDropDown;
        [SerializeField] private Button _button;
        [SerializeField] private Image _bgImage;

        private void Awake()
        {
            var themes = ThemeManager.Instance.AvailableThemes;
            _themeDropDown.options = themes.Select(t => new TMP_Dropdown.OptionData(t.Name)).ToList();
            _themeDropDown.onValueChanged.AddListener(index => {
                ThemeManager.Instance.ChangeTheme(index);
            });
        }
    }
}