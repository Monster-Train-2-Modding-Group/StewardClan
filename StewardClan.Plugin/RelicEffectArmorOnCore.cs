using System;
using System.Collections.Generic;
using TrainworksReloaded.Core;

namespace StewardClan.Plugin
{
    public sealed class RelicEffectArmorOnCore : RelicEffectBase, IStatusEffectRelicEffect
    {
        public override bool CanApplyInPreviewMode => true;

        private StatusEffectStackData[] statusEffects = Array.Empty<StatusEffectStackData>();
        private CharacterState.AddStatusEffectParams addStatusEffectParams = new CharacterState.AddStatusEffectParams();

        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            return propDescriptions;
        }

        public override void Initialize(RelicState relicState, RelicData srcRelicData, RelicEffectData relicEffectData)
        {
            base.Initialize(relicState, srcRelicData, relicEffectData);
            this.addStatusEffectParams.sourceRelicState = base.SourceRelicState;
            this.statusEffects = new StatusEffectStackData[]
            {
                new StatusEffectStackData
                {
                    statusId = "armor",
                    count = 1
                }
            };
        }

        public StatusEffectStackData[]? GetStatusEffects()
        {
            return this.statusEffects;
        }

    }
}