## Color-scheme module for WPF

**Features Overview:**

- Easy integration into your app
- A built-in collection of themes, exposed to make it easy to offer theme selection in your app
- Easy interface for adding more themes
- A built-in collection of common control symbol images, which exist in sets of various colors. This is used by way of WpfThemer.DynamicImage (derived from Image control). You need only specify the symbol name, and the correct image resource for that symbol is automatically selected when the theme changes.


**Integration:**

Some technical notes:

- Although WpfThemer includes a suite of control and style templates, only the brush and color fields are changed from the default system templates. This ensures consistency, in the sense that the physical composition of the standard controls remains as Microsoft defined them. In some cases you'll find that the template is expanded beyond what you will find in the Microsoft documentation. However, the modifications are limited to colors and the VisualState map. This exposes more dynamic response for states like MouseOver, Selected, and so on, without changing the geometry or composition of the control. Templates/ScrollBar.xaml is a good example of this.


