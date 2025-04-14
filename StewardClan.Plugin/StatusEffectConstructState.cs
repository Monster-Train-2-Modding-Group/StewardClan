using System;
using System.Collections;
using System.Runtime.CompilerServices;
using StewardClan.Plugin;
using TrainworksReloaded.Core;

namespace Steward_Clan.Plugin
{
    public sealed class StatusEffectConstructState : StatusEffectState
    {

        public override int GetTriggerOrder()
        {
            return 1;
        }

        public override bool TestTrigger(StatusEffectState.InputTriggerParams inputTriggerParams, StatusEffectState.OutputTriggerParams outputTriggerParams, ICoreGameManagers coreGameManagers)
        {
            CombatManager combatManager = coreGameManagers.GetCombatManager();
            return (!(combatManager != null) || combatManager.GetTurnCount() != 0) && inputTriggerParams.associatedCharacter.GetStatusEffectStacks(base.GetStatusId()) > 0;
        }

        protected override IEnumerator OnTriggered(StatusEffectState.InputTriggerParams inputTriggerParams, StatusEffectState.OutputTriggerParams outputTriggerParams, ICoreGameManagers coreGameManagers)
        {
            CombatManager combatManager = coreGameManagers.GetCombatManager();
            if (combatManager != null && combatManager.GetTurnCount() == 0)
            {
                yield break;
            }
            int statusEffectStacks = inputTriggerParams.associatedCharacter.GetStatusEffectStacks(base.GetStatusId());
            if (statusEffectStacks <= 0)
            {
                yield break;
            }
            var construct = this.GetConstructAmount(statusEffectStacks);
            inputTriggerParams.associatedCharacter.BuffDamage(construct, null, true);
            var effect = this.relicManager.GetRelicEffect<RelicEffectArmorOnCore>();
            if (effect != null)
            {
                inputTriggerParams.associatedCharacter.AddStatusEffect("armor", construct, new CharacterState.AddStatusEffectParams
                {
                    sourceRelicState = effect.SourceRelicState
                }, null, true, false);
            }
            yield return inputTriggerParams.associatedCharacter.BuffMaxHP(construct, true, null);
            yield break;
        }

        public override int GetEffectMagnitude(int stacks = 1)
        {
            return this.GetConstructAmount(stacks);
        }

        private int GetConstructAmount(int stacks)
        {
            return (base.GetParamInt() + this.relicManager.GetModifiedStatusMagnitudePerStack("construct", base.GetAssociatedCharacter().GetTeamType())) * stacks;
        }

        public const string StatusId = "construct";
    }
}