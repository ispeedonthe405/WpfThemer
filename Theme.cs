using System.Windows;

namespace WpfThemer
{
    public class Theme
    {
        ///////////////////////////////////////////////////////////
        #region Fields
        /////////////////////////////

        public enum eSymbolColor
        {
            c111111,
            c242424,
            c434343,
            cC0C0C0,
            cEFEFEF
        }

        private eSymbolColor _SymbolColor = eSymbolColor.c434343;
        private string _DisplayName = string.Empty;
        private string _Description = string.Empty;
        private ResourceDictionary _Resource = new();

        /////////////////////////////
        #endregion Fields
        ///////////////////////////////////////////////////////////


        ///////////////////////////////////////////////////////////
        #region Properties
        /////////////////////////////

        public eSymbolColor SymbolColor
        {
            get => _SymbolColor;
            set => _SymbolColor = value;
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

        public Theme(string displayName, string description, eSymbolColor symbolColor, ResourceDictionary resource)
        {
            DisplayName = displayName;
            Description = description;
            SymbolColor = symbolColor;
            Resource = resource;
        }
    }
}
