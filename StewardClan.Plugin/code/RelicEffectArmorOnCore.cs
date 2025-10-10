using System;
using System.Collections.Generic;
using TrainworksReloaded.Core;
using Conductor.StatusEffects;

namespace StewardClan.Plugin
{
    public sealed class RelicEffectArmorOnCore : RelicEffectBase, IStatusEffectRelicEffect, IConstructStatusArmorModifier
    {
        public override bool CanApplyInPreviewMode => true;

        RelicState IConstructStatusArmorModifier.SourceRelicState { get => SourceRelicState; set => throw new NotImplementedException(); }

        private StatusEffectStackData[] statusEffects = Array.Empty<StatusEffectStackData>();

        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            return [];
        }

        public override void Initialize(RelicState relicState, RelicData srcRelicData, RelicEffectData relicEffectData)
        {
            base.Initialize(relicState, srcRelicData, relicEffectData);
            this.statusEffects = relicEffectData.GetParamStatusEffects();
        }

        public StatusEffectStackData[]? GetStatusEffects()
        {
            return this.statusEffects;
        }

    }
}