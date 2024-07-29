## Color-scheme module for WPF

**Features Overview:**

- Easy integration into your app
- Static interface requires no instantiation of theme lib types
- A built-in collection of themes, exposed in an ObservableCollection so it's easy to offer theme selection in your app
- Easy interface for adding more themes
- A built-in collection of common control symbol images, which exist in sets of various colors. This is used by way of WpfThemer.DynamicImage (derived from stock Image control). You need only specify the symbol name, and the correct image file resource for that symbol is automatically selected when the theme changes.


**Integration:**

Step 1: Associate the ThemeManager with your application:
``` C#
using System.Windows;
using WpfThemer;

namespace Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ThemeManager.SetApplication(this);
        }
    }
}
```


Step 2: Populate a control with the list of themes:
``` C#

public MainWindow()
{
    InitializeComponent();

    combobox_Theme.ItemsSource = ThemeManager.Themes;
    combobox_Theme.DisplayMemberPath = "DisplayName";
}

private void combobox_Theme_SelectionChanged(object sender, SelectionChangedEventArgs e)
{
    if (cb_Theme.SelectedItem is Theme theme)
    {
        ThemeManager.ActiveTheme = theme;
    }
}
```

You can also set a theme by way of its display name. This might be useful if you don't want to offer theme selection and instead just want to use a particular theme; perhaps one that you added:
``` C#
ThemeManager.SetTheme("green"); // case-insensitive
```

Adding a theme you've defined is easy:
``` C#
string uri = "/Eudora.Net;component/GUI/theme/ThemeEudora.xaml";
var theme = new WpfThemer.Theme("Eudora Classic", "Eudora Classic", Theme.eSymbolColor.c111111, new ResourceDictionary()
{
    Source = new Uri(uri, UriKind.RelativeOrAbsolute)
});
ThemeManager.AddExternalTheme(theme);
```

That's all there is to it. Please see the Test project for a working example.

Some technical notes:

- Although WpfThemer includes a suite of control and style templates, only the brush and color fields are changed from the default system templates. This ensures consistency, in the sense that the physical composition of the standard controls remains as Microsoft defined them. In some cases you'll find that the template is expanded beyond what you'll find in the Microsoft documentation. However, the modifications are limited to colors, brushes, and the VisualState map. This exposes more dynamic response for states like MouseOver, Selected, and so on, without changing the geometry or composition of the control. Templates/ScrollBar.xaml is a good example of this.


