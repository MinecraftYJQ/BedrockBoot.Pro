using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BedrockBoot.Interface;
using BedrockBoot.Views.Pages.MainSubPage;
using BedrockBoot.Views.Pages.SettingSubPage;
using OnePointUI.Avalonia.Base.Entry;

namespace Plugin.Pro.Views.Page;

public partial class AboutPro : ISettingPage
{
    public AboutPro()
    {
        InitializeComponent();

        BreadcrumbItem = new List<BreadcrumbItemInfo>
        {
            new()
            {
                ItemName = "插件",
                ItemClickAction = info =>
                    MainSettingPage.NavigateTo(new SettingPlugin())
            },
            new()
            {
                ItemName = "关于增强版"
            }
        };
    }
}