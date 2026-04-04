using System.Reflection;
using Avalonia.Threading;
using BedrockBoot.Dependence;
using BedrockBoot.Interface;
using BedrockBoot.Models.Global;
using BedrockBoot.Models.Helper;
using OnePointUI.Avalonia.Base.Entry;
using OnePointUI.Avalonia.Base.Enum;
using OnePointUI.Avalonia.Styling.Controls.OnePointControls.Dialog;
using Plugin.Pro.Base.Entry;
using Plugin.Pro.Models.Unlock;
using Plugin.Pro.Views.DialogContent;
using Plugin.Pro.Views;
using Plugin.Pro.Views.Page;
using Round.SDK.Entry.BedrockBoot;
using Round.SDK.Helper;
using Round.SDK.Plugin.BedrockBoot;
using Round.SDK.Plugin.BedrockBoot.Register;

namespace Plugin.Pro;

public class Load : IPluginBedrockBoot
{
    public void Initialize()
    {
        UnzipDependentFiles();
        
        RegisterService.RegisterInstanceControlItem(new InstanceControlItemInfo()
        {
            Header = "解锁此实例",
            Description = "解锁该实例，使其无需正版账户登录",
            ItemGlyph = "\uE785",
            Callback = (s =>
            {
                var dialog = new DialogSettingInstanceUnlockContent(s);
                DialogHost.Show(new DialogInfo()
                {
                    Content = dialog,
                    Title = "解锁游戏",
                    CloseButtonText = "确定",
                    PrimaryButtonText = "取消",
                    AccountButton = DialogButtons.CloseButton,
                    CloseAction = () =>
                    {
                        var conf = InstanceConfig.GetInstanceConfig(s);
                        conf.Data.IsUnlock = dialog.IsUnlock;
                        conf.Save();
                    }
                });
            })
        });

        Dispatcher.UIThread.Invoke(() =>
        {
            RegisterService.RegisterSettingPage(new SettingPageInfo()
            {
                Header = "关于增强版",
                Description = "关于插件 [BedrockBoot 增强版]",
                Page = new AboutPro(),
                IconSource = "\uE946"
            });
        });
        
        RegisterService.RegisterLaunchingEvent(path =>
        {
            var unlockCore = new UnlockCore(path);
        });
    }

    private void UnzipDependentFiles()
    {
        Directory.CreateDirectory(Path.Combine(PathsList.RootConfigPath, "BedrockBoot.Pro"));
        Directory.CreateDirectory(Path.Combine(PathsList.RootConfigPath, "BedrockBoot.Pro", "GamePatch"));
        
        Console.WriteLine(@"释放插件依赖文件...");
        var loadedAssembly = Assembly.Load("Plugin.Pro");
        var fileData = Dependence.GetResource(loadedAssembly, "Plugin.Pro.Assets.Dependence.GamePatch.zip");
        var fileName = Path.Combine(PathsList.RootConfigPath, "BedrockBoot.Pro", "GamePatch", "GamePatch.zip");
        File.WriteAllBytes(fileName,
            fileData);

        ZipHelper.ExtractZipFile(fileName, Path.Combine(PathsList.RootConfigPath, "BedrockBoot.Pro", "GamePatch"),
            true);
    }
}