using System;
using System.Collections;
using System.Runtime.CompilerServices;
using TrainworksReloaded.Core;
using TrainworksReloaded.Core.Extensions;
using TrainworksReloaded.Base;
using UnityEngine;

namespace Steward_Clan.Plugin
{
    public sealed class CardEffectCopyEquipment : CardEffectBase
    {
        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            string fieldName = CardEffectFieldNames.ParamCardFilter.GetFieldName();
            propDescriptions[fieldName] = new PropDescription("Duplication Filter", "", null, false);
            string fieldName2 = CardEffectFieldNames.ParamInt.GetFieldName();
            propDescriptions[fieldName2] = new PropDescription("Num Copies", "If -1, copy all equipment", null, false);
            return propDescriptions;
        }

        // Token: 0x060005B5 RID: 1461 RVA: 0x00018D91 File Offset: 0x00016F91
        private bool CardFilterFunc(CardState checkCard, CardUpgradeMaskData mask, RelicManager relicManager)
        {
            if (mask != null)
            {
                return !mask.FilterCard<CardState>(checkCard, relicManager) || checkCard.IsChampionCard() || !checkCard.CanBeCopied();
            }
            return checkCard.IsChampionCard() || !checkCard.CanBeCopied();
        }
        public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers)
        {
            if (cardEffectParams.targets.Count == 0)
                return false;

            CharacterState targetCharacter = cardEffectParams.targets[0];
            if (targetCharacter == null)
                return false;

            var equipmentState = targetCharacter.GetEquipment().LastOrDefault();
            if (equipmentState == null)
                return false;

            return !this.CardFilterFunc(equipmentState, cardEffectState.GetParamCardFilter(), coreGameManagers.GetRelicManager());
        }

        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
        {
            if (cardEffectParams.targets.Count == 0)
                yield break;

            CharacterState targetCharacter = cardEffectParams.targets[0];
            if (targetCharacter == null)
                yield break;

            // Get the equipment from the target
            var equipmentState = targetCharacter.GetEquipment().LastOrDefault();
            if (equipmentState == null)
                yield break;

            // Create a copy of the equipment card
			string cardDataID = equipmentState.GetCardDataID();
			var cardData = coreGameManagers.GetAllGameData().FindCardData(cardDataID);
            if (cardData == null)
                yield break;

            // Add the copy to the player's hand
			CardManager.AddCardUpgradingInfo addCardUpgradingInfo = new()
            {
                upgradeDatas = [],
				copyModifiersFromCard = equipmentState
			};
            var cardState = coreGameManagers.GetCardManager().AddCard(cardData, CardPile.DeckPile, 1, 1, false, false, addCardUpgradingInfo, true, 1f);
            if (cardState != null)
            {
                cardState.UpdateDamageText();
                cardState.UpdateCardBodyText(null);
            }
            yield break;
        }
    }
} 