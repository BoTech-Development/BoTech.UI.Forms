using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BoTech.UI.Forms.Controls.Text
{
    public interface ITextBlock : IFormElement
    {
        public FontStyle FontStyle { get; set; }
        public FontWeight FontWeight { get; set; }
        public Color Foreground { get; set; }
        public Color Background { get; set; }
        public string Text { get; set; }
    }
}
