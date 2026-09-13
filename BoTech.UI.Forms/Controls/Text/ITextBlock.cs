using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BoTech.UI.Forms.Controls.Text
{
    public interface ITextBlock : IFormElement
    {
        /// <summary>
        /// The size of the text.
        /// </summary>
        public double FontSize { get; set; }
        /// <summary>
        /// The style of the text (e.g. italic)
        /// </summary>
        public FontStyle FontStyle { get; set; }
        /// <summary>
        /// Should the text be bold, semi bold, etc. than us this property.
        /// </summary>
        public FontWeight FontWeight { get; set; }
        /// <summary>
        /// The color of the Text.
        /// </summary>
        public Color Foreground { get; set; }
        /// <summary>
        /// The color of the background.
        /// </summary>
        public Color Background { get; set; }
        public string Text { get; set; }
    }
}
