using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Microsoft.Extensions.Configuration;
//using Steward_Clan.Plugin.Patches;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Extensions;

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
                        "json/plugin.json",
                        "json/champions/champion_albert.json",
                        "json/relics/relic_backup_generator.json",
                        "json/relics/relic_construct_core.json",
                        "json/relics/relic_emergency_protocol.json",
                        "json/relics/relic_maintenance_manual.json",
                        "json/relics/relic_storage_locker.json",
                        "json/characters/character_bigger_steward.json",
                        "json/characters/character_dependabot.json",
                        "json/characters/character_fabricator.json",
                        "json/characters/character_imptern.json",
                        "json/characters/character_ludo_developer.json",
                        "json/characters/character_mod_forger.json",
                        "json/characters/character_overheating_computer.json",
                        "json/characters/character_rusted_steward.json",
                        "json/characters/character_unit_tester.json",
                        "json/characters/character_winged_air_conditioner.json",
                        "json/characters/character_wrench_head.json",
                        "json/cards/card_back_to_basic.json",
                        "json/cards/card_breaking_point.json",
                        "json/cards/card_bypass.json",
                        "json/cards/card_cache.json",
                        "json/cards/card_ctrl_v.json",
                        "json/cards/card_decompile.json",
                        "json/cards/card_disarm.json",
                        "json/cards/card_galvanize.json",
                        "json/cards/card_gravitas.json",
                        "json/cards/card_mechanical_outrage.json",
                        "json/cards/card_memory_leak.json",
                        "json/cards/card_overclock.json",
                        "json/cards/card_process_data.json",
                        "json/cards/card_pyre_refactor.json",
                        "json/cards/card_sanity.json",
                        "json/cards/card_scrap.json",
                        "json/cards/card_steamburst.json",
                        "json/cards/card_steward_spike.json",
                        "json/cards/card_steward_tome.json",
                        "json/cards/card_synthesize.json",
                        "json/cards/card_try_catch.json",
                        "json/cards/card_writeline.json",
                        "json/enhancers/constructstone.json"
                    );
                }
            );

            //Harmony.CreateAndPatchAll(typeof(CharacterStateApplyDamagePatch));

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        }
    }
}
