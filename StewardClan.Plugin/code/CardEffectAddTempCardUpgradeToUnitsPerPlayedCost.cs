using ShinyShoe;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Steward_Clan.Plugin
{
    /// <summary>
    /// Card effect to add a Card Upgrade to units based on the played card's cost. You shouldn't need to use this, as you can just use CardTraits / TrackedValues.
    /// This is only necessary since Champions can't have modifiable Card Traits based on Champion Upgrades.
    /// </summary>
    public class CardEffectAddTempCardUpgradeToUnitsPerPlayedCost : CardEffectBase
    {
        public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers)
        {
            if (cardEffectParams.targets.Count > 0)
            {
                return FetchPlayedCostStat(GetAffectedCard(cardEffectParams), coreGameManagers.GetCardStatistics(), coreGameManagers.GetMonsterManager(), coreGameManagers.GetRelicManager()) > 0;
            }
            return false;
        }

        private static CardState? GetAffectedCard(CardEffectParams cardEffectParams)
        {
            return cardEffectParams.playedCard ?? cardEffectParams.selfTarget?.GetSpawnerCard() ?? cardEffectParams.cardTriggeredCharacter?.GetSpawnerCard();
        }

        private int FetchPlayedCostStat(CardState? playedCard, CardStatistics cardStatistics, MonsterManager monsterManager, RelicManager relicManager)
        {
            if (playedCard == null) return 0;
            return ((!playedCard.IsConsumeRemainingEnergyCostType()) ? playedCard.GetCost(cardStatistics, monsterManager, relicManager) : playedCard.GetLastPlayedCost());
        }

        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
        {
            SaveManager saveManager = coreGameManagers.GetSaveManager();
            CardManager cardManager = coreGameManagers.GetCardManager();
            int playedCost = FetchPlayedCostStat(GetAffectedCard(cardEffectParams), coreGameManagers.GetCardStatistics(), coreGameManagers.GetMonsterManager(), coreGameManagers.GetRelicManager());
            foreach (CharacterState target in cardEffectParams.targets)
            {
                var upgrade = cardEffectState.GetParamCardUpgradeData();
                if (target.GetIsClone() && upgrade != null && upgrade.GetExcludeFromClones())
                {
                    continue;
                }
                CardUpgradeState upgradeState = new CardUpgradeState();
                upgradeState.Setup(cardEffectState.GetParamCardUpgradeData(), skipPersistentData: false, cardEffectParams.isFromHiddenTrigger);
                upgradeState.SetAttackDamage(upgradeState.GetAttackDamage() + playedCost * cardEffectState.GetParamInt());
                upgradeState.SetAdditionalHP(upgradeState.GetAdditionalHP() + playedCost * cardEffectState.GetAdditionalParamInt());

                yield return target.ApplyCardUpgrade(upgradeState, fromSpawn: false, upgradeState.GetCardUpgradeDataId(), addRoomStateModifiers: true, forceCharacterTriggerTooltipsHidden: false, null, delegate (CharacterState.UpgradeApplicationResult applicationResult)
                {
                    if (!saveManager.PreviewMode)
                    {
                        CardEffectAddCardUpgradeToUnits.ShowNotificationForUpgrade(upgradeState, target, coreGameManagers, sysManagers);
                    }
                });
                CardState spawnerCard = target.GetSpawnerCard();
                if (spawnerCard != null && !saveManager.PreviewMode && (target.GetSourceCharacterData() == spawnerCard.GetSpawnCharacterData() || spawnerCard.GetSpawnCharacterData() == null))
                {
                    CardAnimator.CardUpgradeAnimationInfo type = new CardAnimator.CardUpgradeAnimationInfo(spawnerCard, upgradeState);
                    CardAnimator.DoAddRecentCardUpgrade.Dispatch(type);
                    spawnerCard.GetTemporaryCardStateModifiers().AddUpgrade(upgradeState);
                    spawnerCard.UpdateCardBodyText();
                    cardManager.RefreshCardInHand(spawnerCard);
                }
            }
        }

        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            return [];
        }
    }
}
