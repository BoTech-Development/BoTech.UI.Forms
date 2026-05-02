namespace BoTech.UI.Forms.Rendering;
/// <summary>
/// This class builds the actual control for the specific UI-Framework.
/// </summary>
/// <typeparam name="TControlTypeBase">This generic Type declares the base control Type. For instance in blazor ControlType will be RenderFragment.</typeparam>
public interface IComponentBuilder<TControlTypeBase> where TControlTypeBase : class
{
    public TControlTypeBase BuildComponentFromConfig(ComponentBuilderConfiguration config);
}