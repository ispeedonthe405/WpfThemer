using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace WpfThemer.Shaders
{
    public class ColorInverterShader : ShaderEffect
    {
        private static readonly PixelShader _pixelShader = new PixelShader
        {
            UriSource = new Uri("pack://application:,,,/WpfThemer;component/Shaders/ColorInverter.ps")
        };

        public ColorInverterShader()
        {
            PixelShader = _pixelShader;
            UpdateShaderValue(InputProperty);
        }

        public Brush Input
        {
            get => (Brush)GetValue(InputProperty);
            set => SetValue(InputProperty, value);
        }

        public static readonly DependencyProperty InputProperty = 
            ShaderEffect.RegisterPixelShaderSamplerProperty("Input", typeof(ColorInverterShader), 0);
    }
}
