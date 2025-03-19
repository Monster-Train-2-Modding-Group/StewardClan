using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ShinyShoe;
using ShinyShoe.Logging;

namespace Steward_Clan.Plugin
{
    public class CustomCardEffectAttachRoomAttachment : CardEffectBase
    {
        private class ApplyEffectAction : IEnumerator<object>, IDisposable
        {
            private enum State
            {
                NotStarted,
                Waiting,
                Finished,
                Disposed,
            }

            private readonly CardEffectState cardEffectState;
            private readonly CardEffectParams cardEffectParams;
            private readonly ICoreGameManagers coreGameManagers;
            private State state;
            private object current = null!;
            private UsingGenericPoolObject<List<CardUpgradeData>>? upgradeDatasContext = null;

            public ApplyEffectAction(
                CardEffectState cardEffectState,
                CardEffectParams cardEffectParams,
                ICoreGameManagers coreGameManagers
            )
            {
                this.state = State.NotStarted;
                this.cardEffectState = cardEffectState;
                this.cardEffectParams = cardEffectParams;
                this.coreGameManagers = coreGameManagers;
            }

            public object Current => current;

            public void Dispose()
            {
                if (state == State.Finished || state == State.NotStarted)
                {
                    Cleanup();
                }
            }

            private void Cleanup()
            {
                state = State.Disposed;
                upgradeDatasContext?.Dispose();
            }

            public bool MoveNext()
            {
                if (state == State.Waiting)
                {
                    state = State.Finished;
                    Cleanup();
                    return false;
                }
                else if (state != State.NotStarted)
                {
                    return false;
                }

                RoomState room = this
                    .coreGameManagers.GetRoomManager()
                    .GetRoom(this.cardEffectParams.selectedRoom);
                if (room == null)
                {
                    state = State.Disposed;
                    return false;
                }

                Team.Type targetTeamType = this.cardEffectState.GetTargetTeamType();
                bool paramBool = this.cardEffectState.GetParamBool();
                bool additionalBool = this.cardEffectState.GetParamBool2();

                // Get a list from a generic pool
                this.upgradeDatasContext = GenericPools.GetList<CardUpgradeData>(out var list);
                if (list == null)
                {
                    state = State.Disposed;
                    return false;
                }

                CustomCardEffectAttachRoomAttachment.CollectUpgrades(
                    this.cardEffectState,
                    list
                );

                // Call AddTrainRoomAttachment and yield control
                this.current = room.AddTrainRoomAttachment(
                    Guid.NewGuid().ToString(),
                    this.cardEffectState.GetParentCardState(),
                    list,
                    null,
                    targetTeamType,
                    paramBool,
                    this.coreGameManagers,
                    additionalBool,
                    false,
                    this.cardEffectState.GetTargetIgnoreBosses()
                );

                this.state = State.Waiting;
                return true; // Yield control
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }
        }

        public override bool CanPlayInEngineRoom
        {
            get { return false; }
        }

        public override bool TestEffect(
            CardEffectState cardEffectState,
            CardEffectParams cardEffectParams,
            ICoreGameManagers coreGameManagers
        )
        {
            RoomState room = coreGameManagers
                .GetRoomManager()
                .GetRoom(cardEffectParams.selectedRoom);
            Console.WriteLine($"{room?.name ?? ""} -- {cardEffectParams.selectedRoom}");
            var return_value =
                !(room == null)
                && (
                    !room.TryFindTrainRoomAttachment(
                        out TrainRoomAttachmentState trainRoomAttachmentState,
                        false
                    ) || !trainRoomAttachmentState.IsEmbedded
                );
            Console.WriteLine($"{return_value}");
            return return_value;
        }

        public override IEnumerator ApplyEffect(
            CardEffectState cardEffectState,
            CardEffectParams cardEffectParams,
            ICoreGameManagers coreGameManagers,
            ISystemManagers sysManagers
        )
        {
            Console.WriteLine("Doing a Thing");
            return new ApplyEffectAction(cardEffectState, cardEffectParams, coreGameManagers);
        }

        // Token: 0x060004FA RID: 1274 RVA: 0x000168D0 File Offset: 0x00014AD0
        public static void CollectUpgrades(
            CardEffectState cardEffectStateSelf,
            List<CardUpgradeData> upgradeDatasCollected
        )
        {
            upgradeDatasCollected.Clear();
            upgradeDatasCollected.Add(cardEffectStateSelf.GetParamCardUpgradeData());
        }

        // Token: 0x060004FB RID: 1275 RVA: 0x0001695C File Offset: 0x00014B5C
        public override PropDescriptions CreateEditorInspectorDescriptions()
        {
            PropDescriptions propDescriptions = new PropDescriptions();
            string fieldName = CardEffectFieldNames.ParamCardUpgradeData.GetFieldName();
            propDescriptions[fieldName] = new PropDescription("Upgrade", "", null, false);
            string fieldName2 = CardEffectFieldNames.ParamBool.GetFieldName();
            propDescriptions[fieldName2] = new PropDescription("Embed", "", null, false);
            return propDescriptions;
        }
    }
}
