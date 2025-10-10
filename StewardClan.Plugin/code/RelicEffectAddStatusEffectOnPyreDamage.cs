using System;
using System.Runtime.CompilerServices;

namespace StewardClan.Plugin
{
    public sealed class RelicEffectAddStatusEffectOnPyreDamage : RelicEffectBase, IStatusEffectRelicEffect, ITowerDamageTakenModifiedRelicEffect, IRelicEffect
    {
        public override bool CanApplyInPreviewMode
        {
            get
            {
                return true;
            }
        }

        public MerchantData.Currency Currency { get; private set; }

        private StatusEffectStackData[] statusEffects = Array.Empty<StatusEffectStackData>();
        private List<CharacterState> damageTargets = new List<CharacterState>();
        private TargetHelper.CollectTargetsData targetsData;
        private Team.Type sourceTeam;
        private CharacterState.AddStatusEffectParams addStatusEffectParams = new CharacterState.AddStatusEffectParams();

        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            string fieldName = RelicEffectFieldNames.ParamTargetMode.GetFieldName();
            propDescriptions[fieldName] = new PropDescription("Target Mode", "", null, false);
            string fieldName2 = RelicEffectFieldNames.ParamSourceTeam.GetFieldName();
            propDescriptions[fieldName2] = new PropDescription("Target Team", "", null, false);
            string fieldName3 = RelicEffectFieldNames.ParamStatusEffects.GetFieldName();
            propDescriptions[fieldName3] = new PropDescription("Status Effects To Apply", "", null, false);
            return propDescriptions;
        }

        public override void Initialize(RelicState relicState, RelicData srcRelicData, RelicEffectData relicEffectData)
        {
            base.Initialize(relicState, srcRelicData, relicEffectData);
            this.sourceTeam = relicEffectData.GetParamSourceTeam();
            this.Currency = MerchantData.Currency.Gold;
            this.addStatusEffectParams.sourceRelicState = base.SourceRelicState;
            this.statusEffects = relicEffectData.GetParamStatusEffects();
            this.targetsData.targetTeamType = this.sourceTeam;
            this.targetsData.ignorePyre = true;
            this.targetsData.inCombat = false;
            this.targetsData.Reset(relicEffectData.GetTargetMode());
        }

        public int ApplyModifiedDamage(int previousDamage, RelicEffectParams relicEffectParams, ICoreGameManagers coreGameManagers)
        {
            int num = previousDamage;
            if (num <= 0)
            {
                return num;
            }
            TargetHelper.CollectTargets(this.targetsData, coreGameManagers, ref this.damageTargets);
            using (List<CharacterState>.Enumerator enumerator = this.damageTargets.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    CharacterState characterState = enumerator.Current;
                    foreach (StatusEffectStackData statusEffectStackData in this.statusEffects)
                    {
                        int num2 = statusEffectStackData.count;
                        characterState.AddStatusEffect(statusEffectStackData.statusId, num2, addStatusEffectParams, null, true, false);
                    }
                }
            }
            return num;
        }

        public StatusEffectStackData[]? GetStatusEffects()
        {
            return this.statusEffects;
        }
    }
}
