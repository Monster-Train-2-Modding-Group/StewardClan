using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Microsoft.Extensions.Configuration;
using Steward_Clan.Plugin.Patches;
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
                        "plugin.json",
                        "status.json",
                        "champions/champion_albert.json",
                        "relics/relic_backup_generator.json",
                        "relics/relic_construct_core.json",
                        "relics/relic_emergency_protocol.json",
                        "relics/relic_maintenance_manual.json",
                        "relics/relic_storage_locker.json",
                        "characters/character_bigger_steward.json",
                        "characters/character_dependabot.json",
                        "characters/character_fabricator.json",
                        "characters/character_imptern.json",
                        "characters/character_ludo_developer.json",
                        "characters/character_mod_forger.json",
                        "characters/character_overheating_computer.json",
                        "characters/character_rusted_steward.json",
                        "characters/character_unit_tester.json",
                        "characters/character_winged_air_conditioner.json",
                        "characters/character_wrench_head.json",
                        "cards/card_back_to_basic.json",
                        "cards/card_breaking_point.json",
                        "cards/card_bypass.json",
                        "cards/card_cache.json",
                        "cards/card_ctrl_v.json",
                        "cards/card_decompile.json",
                        "cards/card_disarm.json",
                        "cards/card_galvanize.json",
                        "cards/card_gravitas.json",
                        "cards/card_mechanical_outrage.json",
                        "cards/card_memory_leak.json",
                        "cards/card_overclock.json",
                        "cards/card_process_data.json",
                        "cards/card_pyre_refactor.json",
                        "cards/card_sanity.json",
                        "cards/card_scrap.json",
                        "cards/card_steamburst.json",
                        "cards/card_steward_spike.json",
                        "cards/card_steward_tome.json",
                        "cards/card_synthesize.json",
                        "cards/card_try_catch.json",
                        "cards/card_writeline.json"
                    );
                }
            );

            Harmony.CreateAndPatchAll(typeof(CharacterStateApplyDamagePatch));

            Logger.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
            Logger.LogInfo($"{typeof(CustomCardEffectAttachRoomAttachment).AssemblyQualifiedName}");
        }
    }
}
