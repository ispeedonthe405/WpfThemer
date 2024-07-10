using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfThemer;

namespace Test
{
    internal class DgridItem
    {
        public string Value0 { get; set; } = string.Empty;
        public string Value1 { get; set; } = string.Empty;
        public string Value2 { get; set; } = string.Empty;

        public static DgridItem Generate()
        {
            Random rnd = new();
            DgridItem item = new()
            {
                Value0 = rnd.NextDouble().ToString(),
                Value1 = rnd.NextDouble().ToString(),
                Value2 = rnd.NextDouble().ToString()
            };
            return item;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<DgridItem> GridItems { get; set; } = [];

        private void BuildSymbols()
        {
            SymbolManager.AddSymbol("add", new("pack://application:,,,/images/dark/add.png"), Theme.eThemeType.Light);
            SymbolManager.AddSymbol("add", new("pack://application:,,,/images/light/add.png"), Theme.eThemeType.Dark);
            SymbolManager.AddSymbol("add", new("pack://application:,,,/images/dark/add.png"), Theme.eThemeType.Undefined);

            SymbolManager.AddSymbol("cancel", new("pack://application:,,,/images/dark/cancel.png"), Theme.eThemeType.Light);
            SymbolManager.AddSymbol("cancel", new("pack://application:,,,/images/light/cancel.png"), Theme.eThemeType.Dark);
            SymbolManager.AddSymbol("cancel", new("pack://application:,,,/images/dark/cancel.png"), Theme.eThemeType.Undefined);
        }

        public MainWindow()
        {
            InitializeComponent();

            BuildSymbols();

            cb_Theme.ItemsSource = ThemeManager.Themes;
            cb_Theme.DisplayMemberPath = "DisplayName";
            cb_Theme.SelectedItem = ThemeManager.ActiveTheme;

            for(int i = 0; i < 10; i++)
            {
                GridItems.Add(DgridItem.Generate());
            }
            datagrid.ItemsSource = GridItems;
        }

        private void cb_Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb_Theme.SelectedItem is Theme theme)
            {
                ThemeManager.ActiveTheme = theme;
                tb_Description.Text = theme.Description;
            }
        }
    }
}