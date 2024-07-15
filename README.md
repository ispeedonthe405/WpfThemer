Color-scheme module for WPF. Integration is lightweight. Adding application-specific themes to complement the WpfThemer color schemes is easy. Please see the Test project for details on integration.

Some technical notes:

- Although WpfThemer includes a suite of control and style templates, only the brush and color fields are changed from the default system templates. This ensures consistency, in the sense that the physical composition of the controls remains as Microsoft defined them. In some cases you'll find that the template is expanded beyond what you will find in the Microsoft documentation. However, the modifications are limited to colors and the VisualState map. This exposes more dynamic response for states like MouseOver, Selected, and so on, without changing the geometry or composition of the control. Templates/ScrollBar.xaml is a good example of this.

- When System is selected as the theme, the WpfThemer templates are dropped from the application's MergedDictionaries. Only the base colors for Window, Window Text, and Border remain, and are sampled from the system palette. The rest is handled by the OS. This occurs every time System is selected as the active theme. The result is, when the System theme is selected it's as though there's no themer package at all. Upon selection of a theme other than System, the templates are again integrated into the app's resources so that the theme colors are applied. 
