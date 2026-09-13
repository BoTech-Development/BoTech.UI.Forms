using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using BoTech.UI.Forms.Avalonia.Rendering;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Text;
using BoTech.UI.Forms.Converter;
using BoTech.UI.Forms.Rendering;
using AvaloniaTextBlock =  Avalonia.Controls.TextBlock;

namespace BoTech.UI.Forms.Avalonia.Controls.Text
{
    public class TextBlock : ITextBlock
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public bool IsVisible { get; set; }
        public bool IsEnabled { get; set; }
        public double FontSize { get; set; }
        public FontStyle FontStyle { get; set; }
        public FontWeight FontWeight { get; set; }
        public Color Foreground { get; set; }
        public Color Background { get; set; }
        public string Text { get; set; }

        public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
        {
            return new ComponentBuilderConfiguration(this)
            {
                ComponentType = typeof(AvaloniaTextBlock),
                ComponentAttributes = new List<IComponentBuilderAttributeConfiguration>()
                {
                    ComponentAttributeFactory.CreateBindingAttribute("TextProperty", Text, nameof(Text), typeof(TextBlock)),
                    //ComponentAttributeFactory.CreateBindingAttribute("FontSizeProperty", 14, nameof(FontSize), typeof(TextBlock)),
                    ComponentAttributeFactory.CreateBindingAttribute("FontStyleProperty", FontStyle, nameof(FontStyle), typeof(TextBlock), typeof(EnumConverter<FontStyle, global::Avalonia.Media.FontStyle>)),
                    ComponentAttributeFactory.CreateBindingAttribute("FontWeightProperty", FontWeight, nameof(FontWeight), typeof(TextBlock), typeof(EnumConverter<FontWeight, global::Avalonia.Media.FontWeight>))
                }
            };
        }

        public void TryToAddChild(IFormElement child)
        {
            throw new NotSupportedException();
        }
    }
}
