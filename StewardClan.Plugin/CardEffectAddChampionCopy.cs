using System;
using System.Collections;

namespace Steward_Clan.Plugin
{
    public class CardEffectAddChampionCopy : CardEffectBase
    {
        public override bool CanPlayAfterBossDead
        {
            get
            {
                return false;
            }
        }
        public override bool CanApplyInPreviewMode
        {
            get
            {
                return false;
            }
        }
        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = [];
            return propDescriptions;
        }

        public override bool TestEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers)
        {
            return base.TestEffect(cardEffectState, cardEffectParams, coreGameManagers);
        }
        public override IEnumerator ApplyEffect(CardEffectState cardEffectState, CardEffectParams cardEffectParams, ICoreGameManagers coreGameManagers, ISystemManagers sysManagers)
        {
            var saveManager = coreGameManagers.GetSaveManager();
            var cardManager = coreGameManagers.GetCardManager();

            var allCards = new List<CardState>();
            allCards = cardManager.GetAllCards(allCards);
            foreach (var card in allCards)
            {
                if (card.GetCardDataID() == saveManager.GetMainChampionData().championCardData.GetID())
                {
                    // Add the copy to the player's hand
                    CardManager.AddCardUpgradingInfo addCardUpgradingInfo = new()
                    {
                        upgradeDatas = [],
                        copyModifiersFromCard = card
                    };
                    var cardState = coreGameManagers.GetCardManager().AddCard(saveManager.GetMainChampionData().championCardData, CardPile.HandPile, 1, 1, false, false, addCardUpgradingInfo, true, 1f);
                    if (cardState != null)
                    {
                        cardState.UpdateDamageText();
                        cardState.UpdateCardBodyText(null);
                    }
                    yield break;
                }
            }
            yield break;
        }
    }
}