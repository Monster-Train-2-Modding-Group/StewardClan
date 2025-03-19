using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Microsoft.Extensions.Configuration;
using Steward_Clan.Plugin.Extensions;
using TrainworksReloaded.Core;

namespace Steward_Clan.Plugin
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static new ManualLogSource Logger = new(MyPluginInfo.PLUGIN_GUID);
        public void Awake()
        {
            // Plugin startup logic
            Logger = base.Logger;

            var builder = Railhead.GetBuilder();
            builder.Configure(
                MyPluginInfo.PLUGIN_GUID,
                c =>
                {
                    c.AddMergedJsonFile(
                        "plugin.json",
                        "champion_albert.json",
                        "character_bigger_steward.json",
                        "card_breaking_point.json",
                        "card_back_to_basic.json",
                        "card_galvanize.json",
                        "card_gravitas.json",
                        "card_mechanical_outrage.json",
                        "card_overclock.json",
                        "card_pyre_refactor.json",
                        "card_steamburst.json"
                        );
                }
            );

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            Logger.LogInfo($"{typeof(CustomCardEffectAttachRoomAttachment).AssemblyQualifiedName}");
        }
    }
}
