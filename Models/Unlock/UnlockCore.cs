using BedrockBoot.Models.Global;
using BedrockBoot.Models.Helper;
using BedrockLauncher.Core;
using Plugin.Pro.Base.Entry;

namespace Plugin.Pro.Models.Unlock;

public class UnlockCore
{
    public UnlockCore(string path)
    {
        var config = InstanceConfig.GetInstanceConfig(path).Data;
        Console.WriteLine($@"启用破解：{config.IsUnlock}");
        
        var conf = GameInfoHelper.GetVersionConfig(path);
        if (conf.Info.BuildType == MinecraftBuildTypeVersion.GDK)
        {
            if(!config.IsUnlock)
                return;
            
            var preloaderFile = Path.Combine(path, "preload", "gdk_patch.dll");
            File.Copy(
                Path.Combine(PathsList.RootConfigPath, "BedrockBoot.Pro", "GamePatch", "gdk", "mcpatcher_core.dll"),
                preloaderFile, true);
        }
        else if (conf.Info.BuildType == MinecraftBuildTypeVersion.UWP)
        {
            if (File.Exists(Path.Combine(path, "IPHLPAPI.dll")))
                File.Delete(Path.Combine(path, "IPHLPAPI.dll"));
            if (File.Exists(Path.Combine(path, "Windows.ApplicationModel.Store.dll")))
                File.Delete(Path.Combine(path, "Windows.ApplicationModel.Store.dll"));
            
            if(!config.IsUnlock)
                return;
            
            File.Copy(
                Path.Combine(PathsList.RootConfigPath, "BedrockBoot.Pro", "GamePatch", "uwp", "IPHLPAPI.dll"),
                Path.Combine(path, "IPHLPAPI.dll"), true);
            File.Copy(
                Path.Combine(PathsList.RootConfigPath, "BedrockBoot.Pro", "GamePatch", "uwp", "Windows.ApplicationModel.Store.dll"),
                Path.Combine(path, "Windows.ApplicationModel.Store.dll"), true);
        }
    }
}