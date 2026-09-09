using System;
using System.Collections.Generic;
using System.Text;
using Avalonia.Controls;
using BoTech.UI.Forms.Avalonia.Rendering;
using BoTech.UI.Forms.Controls;
using BoTech.UI.Forms.Controls.Layout;
using BoTech.UI.Forms.Models;
using BoTech.UI.Forms.Rendering;
using TextBlock = BoTech.UI.Forms.Avalonia.Controls.Text.TextBlock;

namespace BoTech.UI.Forms.Avalonia.Controls.Layout
{
    public class Group : IGroup
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public bool IsVisible { get; set; }
        public bool IsEnabled { get; set; }
        public IFormElement Content { get; set; }

        public string Title
        {
            get => field;
            set
            {
                if (_titleTextBlock is not null)
                    _titleTextBlock.Text = value;
                field = value;
            }
        }
        public string SubTitle
        {
            get => field;
            set
            {
                if (_subTitleTextBlock is not null)
                    _subTitleTextBlock.Text = value;
                field = value;
            }
        }
        public Icon Icon { get; set; }

        private TextBlock? _subTitleTextBlock = null;
        private TextBlock? _titleTextBlock = null;

        public IComponentBuilderConfiguration BuildComponentBuilderConfigurationFromThis()
        {
            if (string.IsNullOrEmpty(SubTitle))
            {
                return CreateNormalHeader();
            }
            else
            {
                return CreateHeaderWithSubTitle();
            }
        }
        private ComponentBuilderConfiguration CreateHeaderWithSubTitle()
        {
            Stack textStackPanel = new Stack()
            {
                Orientation = Orientation.Vertical
            };
            _titleTextBlock = new TextBlock()
            {
                Text = Title
            };
            _subTitleTextBlock = new TextBlock()
            {
                Text = SubTitle
            };
            textStackPanel.TryToAddChild(_titleTextBlock);
            textStackPanel.TryToAddChild(_subTitleTextBlock);
            
            return new ComponentBuilderConfiguration(this)
            {
                ComponentType = typeof(GroupBox),
                ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                {
                    ComponentBuilderAttributeConfiguration.CreateConstantAttributeWithControlAsValue("Header", _titleTextBlock)
                }
            };
        }
        private ComponentBuilderConfiguration CreateNormalHeader()
        {
            Stack textStackPanel = new Stack()
            {
                Orientation = Orientation.Vertical
            };
            _titleTextBlock = new TextBlock()
            {
                Text = Title
            };
            textStackPanel.TryToAddChild(_titleTextBlock);

            return new ComponentBuilderConfiguration(this)
            {
                ComponentType = typeof(GroupBox),
                ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                {
                    ComponentBuilderAttributeConfiguration.CreateConstantAttributeWithControlAsValue("Header", textStackPanel)
                }
            };
        }
        public void TryToAddChild(IFormElement child)
        {
            Content = child;
        }
    }
}
