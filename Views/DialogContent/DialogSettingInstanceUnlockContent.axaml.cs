using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BedrockBoot.Models.Helper;
using Plugin.Pro.Base.Entry;

namespace Plugin.Pro.Views.DialogContent;

public partial class DialogSettingInstanceUnlockContent : UserControl
{
    public bool IsUnlock => (bool)IsUnlockSwitch.IsChecked!;
    public DialogSettingInstanceUnlockContent(string path)
    {
        InitializeComponent();
        
        var conf = InstanceConfig.GetInstanceConfig(path).Data;
        IsUnlockSwitch.IsChecked = conf.IsUnlock;
    }
}