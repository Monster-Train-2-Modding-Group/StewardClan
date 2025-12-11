using Conductor.TargetModes;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TrainworksReloaded.Core;
using static CombatManager;

namespace Steward_Clan.Plugin
{
    public sealed class CardEffectReplayLastTrigger : CardEffectBase
    {
        // Token: 0x060006A3 RID: 1699 RVA: 0x0001B7D0 File Offset: 0x000199D0
        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            string fieldName = CardEffectFieldNames.ParamTrigger.GetFieldName();
            propDescriptions[fieldName] = new PropDescription("Trigger", "", null, false);
            return propDescriptions;
        }

        // Token: 0x060006A4 RID: 1700 RVA: 0x0001B802 File Offset: 0x00019A02
        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
        {
            if (cardEffectState != null && cardEffectParams != null && cardEffectParams.selfTarget != null)
            {
                Team.Type type = Team.Type.Monsters;
                CharacterTriggerData.Trigger paramTrigger = cardEffectState.GetParamTrigger();
                var combatManager = coreGameManagers.GetCombatManager();
                var method = typeof(CombatManager).GetMethod("GetBattleTriggerEffects", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var result = (BattleTriggeredEvents.BattleTriggers)method.Invoke(combatManager, [type, paramTrigger]);
                if (result == null)
                {
                    yield break;
                }

                var lastTrigger = result.battleTriggeredCardEffects.LastOrDefault();
                if (lastTrigger == null)
                {
                    yield break;
                }
                yield return combatManager.ApplyEffects(
                    lastTrigger.cardEffects,
                    cardEffectParams.selectedRoom,
                    playedCard: cardEffectParams.playedCard,
                    selfTarget: cardEffectParams.selfTarget,
                    dropLocation: cardEffectParams.dropLocation,
                    characterThatActivatedAbility: cardEffectParams.selfTarget);
                }
            yield break;
        }

        // Token: 0x060006A5 RID: 1701 RVA: 0x0001B820 File Offset: 0x00019A20
        public override bool HasRandomTarget(CardEffectState cardEffectState, CombatManager combatManager)
        {
            Team.Type type = Team.Type.Monsters;
            CharacterTriggerData.Trigger paramTrigger = cardEffectState.GetParamTrigger();
            return combatManager.GetAnyBattleTriggerEffectsRandom(type, paramTrigger);
        }
    }
}