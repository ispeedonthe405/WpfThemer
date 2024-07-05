using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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

        public MainWindow()
        {
            InitializeComponent();

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