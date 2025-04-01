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
                        "champions/champion_albert.json",
                        "relics/relic_rage_treads.json",
                        "characters/character_bigger_steward.json",
                        "characters/character_wrench_head.json",
                        "cards/card_back_to_basic.json",
                        "cards/card_breaking_point.json",
                        "cards/card_bypass.json",
                        "cards/card_cache.json",
                        "cards/card_decompile.json",
                        "cards/card_disarm.json",
                        "cards/card_galvanize.json",
                        "cards/card_gravitas.json",
                        "cards/card_mechanical_outrage.json",
                        "cards/card_overclock.json",
                        "cards/card_process_data.json",
                        "cards/card_pyre_refactor.json",
                        "cards/card_scrap.json",
                        "cards/card_steamburst.json",
                        "cards/card_steward_spike.json",
                        "cards/card_synthesize.json",
                        "cards/card_try_catch.json",
                        "cards/card_writeline.json"
                    );
                }
            );

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            Logger.LogInfo($"{typeof(CustomCardEffectAttachRoomAttachment).AssemblyQualifiedName}");
        }
    }
}
