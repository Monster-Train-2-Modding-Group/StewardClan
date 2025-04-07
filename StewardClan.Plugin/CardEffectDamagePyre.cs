using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Extensions;
using TrainworksReloaded.Base;


namespace Steward_Clan.Plugin
{
	public class CardEffectDamagePyre : CardEffectBase
	{
		public override bool CanApplyInPreviewMode
		{
			get
			{
				return this.canApplyInPreviewMode;
			}
		}

		public override PropDescriptions CreateEditorInspectorDescriptions()
		{
			PropDescriptions propDescriptions = new PropDescriptions();
			string fieldName = CardEffectFieldNames.ParamInt.GetFieldName();
			propDescriptions[fieldName] = new PropDescription("Damage Amount", "", null, false);
			string fieldName2 = CardEffectFieldNames.UseIntRange.GetFieldName();
			propDescriptions[fieldName2] = new PropDescription("Use Range For Damage Amount", "", null, false);
			string fieldName3 = CardEffectFieldNames.ParamMinInt.GetFieldName();
			propDescriptions[fieldName3] = new PropDescription("Min Damage Amount", "", null, false);
			string fieldName4 = CardEffectFieldNames.ParamMaxInt.GetFieldName();
			propDescriptions[fieldName4] = new PropDescription("Max Damage Amount", "", null, false);
			string fieldName5 = CardEffectFieldNames.ParamMultiplier.GetFieldName();
			propDescriptions[fieldName5] = new PropDescription("Range Multiplier", "", null, false);
			string fieldName6 = CardEffectFieldNames.UseStatusEffectStackMultiplier.GetFieldName();
			propDescriptions[fieldName6] = new PropDescription("Use Status Effect Stack Multiplier", "Multiply the damage amount by the number of this type of status effect the target has.", null, false);
			return propDescriptions;
		}

		public override void Setup(CardEffectState cardEffectState)
		{
			base.Setup(cardEffectState);
			this.canApplyInPreviewMode = cardEffectState.GetTargetMode() != TargetMode.Pyre;
		}

		public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers)
		{
			int intInRange = cardEffectState.GetIntInRange();
			bool flag = !cardEffectState.GetUseIntRange() || cardEffectState.GetParamMaxInt() > 0;
			bool flag2 = true;
			if (cardEffectState.GetTargetMode() == TargetMode.DropTargetCharacter)
			{
				flag2 = cardEffectParams.targets.Count > 0;
			}
			return intInRange >= 0 && flag2 && flag;
		}

		protected int GetDamageAmount(CardEffectState cardEffectState, CharacterState? selfTarget)
		{
			int num = cardEffectState.GetIntInRange();
			return num;
		}

		public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
		{
			CombatManager combatManager = coreGameManagers.GetCombatManager();
			UnityEngine.Random.State state = RandomManager.GetState(RngId.Battle);
			int damageAmount = this.GetDamageAmount(cardEffectState, cardEffectParams.selfTarget);
			int soundGateId = SoundManager.INVALID_SOUND_GATE;
			if (cardEffectState.GetTargetMode() == TargetMode.Room)
			{
				soundGateId = combatManager.IgnoreDuplicateSounds(true);
			}
			int num;
			for (int i = 0; i < cardEffectParams.targets.Count; i = num + 1)
			{
				CharacterState characterState = cardEffectParams.targets[i];
				yield return combatManager.ApplyDamageToTarget(damageAmount, characterState, new CombatManager.ApplyDamageToTargetParameters
				{
					playedCard = cardEffectParams.playedCard,
					finalEffectInSequence = cardEffectParams.finalEffectInSequence,
					relicState = cardEffectParams.sourceRelic,
					selfTarget = cardEffectParams.selfTarget,
					appliedVfx = cardEffectState.GetAppliedVFX(),
					appliedVfxId = cardEffectParams.appliedVfxId
				});
				num = i;
			}
			combatManager.ReleaseIgnoreDuplicateCuesHandle(soundGateId);
			yield break;
		}

		public override int GetEffectApplicationCount(CardEffectState cardEffectState, CardEffectParams _unused1, ICoreGameManagers coreGameManagers, ISystemManagers _unused2)
		{
			return GameEffectHelper.GetDamageEffectApplicationCount(cardEffectState, coreGameManagers);
		}

		public bool WillEffectKillTarget(CharacterState target, CardState card, CardEffectState cardEffectState, out int resultantDamage)
		{
			int damageAmount = this.GetDamageAmount(cardEffectState, null);
			int num;
			resultantDamage = target.GetDamageToTarget(damageAmount, null, card, out num, Damage.Type.Default);
			return resultantDamage >= target.GetHP();
		}

		public override string GetHintText(CardEffectState cardEffectState, CharacterState selfTarget)
		{
			int damageAmount = this.GetDamageAmount(cardEffectState, selfTarget);
			return "CardTraitScalingAddDamage_CurrentScaling_CardText".Localize(new LocalizedIntegers(new int[] { damageAmount }));
		}


		private bool canApplyInPreviewMode;
    }
}