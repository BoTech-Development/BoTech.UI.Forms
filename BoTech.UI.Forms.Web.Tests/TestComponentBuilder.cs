using BoTech.UI.Forms.Rendering;
using BoTech.UI.Forms.Web.Rendering;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace BoTech.UI.Forms.Web.Tests;

public class TestComponentBuilder
{
    [SetUp]
    public void SetUp()
    {
        
    }
    [Test]
    public void TestTextBoxBuilding()
    {
        RenderFragment fragment = new ComponentBuilder().BuildComponentFromConfig(new ComponentBuilderConfiguration()
        {
            ComponentType = typeof(MudStack),
            ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
            {
                new ComponentBuilderAttributeConfiguration()
                {
                    AttributeName = "Row",
                    AttributeValue = true
                }
            },
            Children = new List<ComponentBuilderConfiguration>()
            {
                new ComponentBuilderConfiguration()
                {
                    ComponentType = typeof(MudTextField<string>),
                    ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                    {
                        new ComponentBuilderAttributeConfiguration()
                        {
                            AttributeName = "HelperText",
                            AttributeValue = "Some Name"
                        }
                    }
                },
                new ComponentBuilderConfiguration()
                {
                    ComponentType = typeof(MudToggleIconButton),
                    ComponentAttributes = new List<ComponentBuilderAttributeConfiguration>()
                    {
                        new ComponentBuilderAttributeConfiguration()
                        {
                            AttributeName = "Icon",
                            AttributeValue = Icons.Material.Filled.Info
                        },
                        new ComponentBuilderAttributeConfiguration()
                        {
                            AttributeName = "ToggledIcon",
                            AttributeValue = Icons.Material.Filled.Close
                        }
                    }
                }
            }
        });
    }
    [TearDown]
    public void TearDown(){}
}