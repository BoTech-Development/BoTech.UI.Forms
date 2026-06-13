using System.Runtime.Versioning;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Browser;
using BoTech.UI.Forms.Avalonia.Demo;
using ReactiveUI.Avalonia;


internal sealed partial class Program
{
    private static Task Main(string[] args) => BuildAvaloniaApp()
        .WithInterFont()
#if DEBUG
        .WithDeveloperTools()
#endif
        .UseReactiveUI((builder) => {})
        .StartBrowserAppAsync("out");

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>();
}