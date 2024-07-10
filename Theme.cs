using System.Windows;

namespace WpfThemer
{
    public class Theme
    {
        ///////////////////////////////////////////////////////////
        #region Fields
        /////////////////////////////

        public enum eThemeType
        {
            Light,
            Dark,
            Undefined
        }

        private eThemeType _ThemeType = eThemeType.Undefined;
        private string _DisplayName = string.Empty;
        private string _Description = string.Empty;
        private ResourceDictionary _Resource = new();

        /////////////////////////////
        #endregion Fields
        ///////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////
        #region Properties
        /////////////////////////////

        public eThemeType ThemeType
        {
            get => _ThemeType;
            set => _ThemeType = value;
        }

        public string DisplayName
        {
            get => _DisplayName;
            set => _DisplayName = value;
        }

        public string Description
        {
            get => _Description;
            set => _Description = value;
        }

        public ResourceDictionary Resource
        {
            get => _Resource;
            set => _Resource = value;
        }

        /////////////////////////////
        #endregion Properties
        ///////////////////////////////////////////////////////////

        public Theme()
        {

        }

        public Theme(eThemeType themeType, string displayName, string description, ResourceDictionary resource)
        {
            ThemeType = themeType;
            DisplayName = displayName;
            Description = description;
            Resource = resource;
        }
    }
}
