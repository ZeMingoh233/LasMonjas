using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LasMonjas.LasMonjas;
using static LasMonjas.GameHistory;
using LasMonjas.Objects;
using UnityEngine;

namespace LasMonjas.Patches {
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FixedUpdate))]
    public static class PlayerControlFixedUpdatePatch {

        static PlayerControl setTarget(bool onlyCrewmates = false, bool targetPlayersInVents = false, List<PlayerControl> untargetablePlayers = null, PlayerControl targetingPlayer = null) {
            PlayerControl result = null;
            float num = GameOptionsData.KillDistances[Mathf.Clamp(PlayerControl.GameOptions.KillDistance, 0, 2)];
            if (!ShipStatus.Instance) return result;
            if (targetingPlayer == null) targetingPlayer = PlayerControl.LocalPlayer;
            if (targetingPlayer.Data.IsDead) return result;

            Vector2 truePosition = targetingPlayer.GetTruePosition();
            Il2CppSystem.Collections.Generic.List<GameData.PlayerInfo> allPlayers = GameData.Instance.AllPlayers;
            for (int i = 0; i < allPlayers.Count; i++) {
                GameData.PlayerInfo playerInfo = allPlayers[i];
                if (!playerInfo.Disconnected && playerInfo.PlayerId != targetingPlayer.PlayerId && !playerInfo.IsDead && (!onlyCrewmates || !playerInfo.Role.IsImpostor)) {
                    PlayerControl @object = playerInfo.Object;
                    if (untargetablePlayers != null && untargetablePlayers.Any(x => x == @object)) {
                        // if that player is not targetable: skip check
                        continue;
                    }

                    if (@object && (!@object.inVent || targetPlayersInVents)) {
                        Vector2 vector = @object.GetTruePosition() - truePosition;
                        float magnitude = vector.magnitude;
                        if (magnitude <= num && !PhysicsHelpers.AnyNonTriggersBetween(truePosition, vector.normalized, magnitude, Constants.ShipAndObjectsMask)) {
                            result = @object;
                            num = magnitude;
                        }
                    }
                }
            }
            return result;
        }

        static void setPlayerOutline(PlayerControl target, Color color) {
            if (target == null || target.myRend == null) return;

            target.myRend.material.SetFloat("_Outline", 1f);
            target.myRend.material.SetColor("_OutlineColor", color);
        }

        static void setBasePlayerOutlines() {
            foreach (PlayerControl target in PlayerControl.AllPlayerControls) {
                if (target == null || target.myRend == null) continue;

                bool isTransformedMimic = target == Mimic.mimic && Mimic.transformTarget != null && Mimic.transformTimer > 0f;
                bool hasVisibleShield = false;
                if (Painter.painterTimer <= 0f && Squire.shielded != null && ((target == Squire.shielded && !isTransformedMimic) || (isTransformedMimic && Mimic.transformTarget == Squire.shielded))) {
                    hasVisibleShield = Squire.showShielded == 0 && PlayerControl.LocalPlayer == Squire.squire // Squire only
                        || (Squire.showShielded == 1 && (PlayerControl.LocalPlayer == Squire.shielded || PlayerControl.LocalPlayer == Squire.squire)) // Shielded + Squire
                        || (Squire.showShielded == 2); // Everyone
                }

                if (hasVisibleShield) {
                    target.myRend.material.SetFloat("_Outline", 1f);
                    target.myRend.material.SetColor("_OutlineColor", Squire.shieldedColor);
                }
                else {
                    target.myRend.material.SetFloat("_Outline", 0f);
                }
            }
        }

        public static void bendTimeUpdate() {
            if (TimeTraveler.isRewinding) {
                if (localPlayerPositions.Count > 0) {
                    // Set position
                    var next = localPlayerPositions[0];
                    // Exit current vent if necessary
                    if (PlayerControl.LocalPlayer.inVent) {
                        foreach (Vent vent in ShipStatus.Instance.AllVents) {
                            bool canUse;
                            bool couldUse;
                            vent.CanUse(PlayerControl.LocalPlayer.Data, out canUse, out couldUse);
                            if (canUse) {
                                PlayerControl.LocalPlayer.MyPhysics.RpcExitVent(vent.Id);
                                vent.SetButtons(false);
                            }
                        }
                    }

                    // Set position
                    PlayerControl.LocalPlayer.transform.position = next.Item1;

                    localPlayerPositions.RemoveAt(0);

                    if (localPlayerPositions.Count > 1) localPlayerPositions.RemoveAt(0); // Skip every second position to rewind twice as fast, but never skip the last position

                    // Try reviving LOCAL player 
                    if (TimeTraveler.reviveDuringRewind && PlayerControl.LocalPlayer.Data.IsDead) {
                        DeadPlayer deadPlayer = deadPlayers.Where(x => x.player == PlayerControl.LocalPlayer).FirstOrDefault();
                        if (deadPlayer != null && next.Item2 < deadPlayer.timeOfDeath) {
                            MessageWriter write = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.TimeTravelerRevive, Hazel.SendOption.Reliable, -1);
                            write.Write(PlayerControl.LocalPlayer.PlayerId);
                            AmongUsClient.Instance.FinishRpcImmediately(write);
                            RPCProcedure.timeTravelerRevive(PlayerControl.LocalPlayer.PlayerId);
                        }
                    }
                }
                else {
                    TimeTraveler.isRewinding = false;
                    PlayerControl.LocalPlayer.moveable = true;
                }
            }
            else {
                while (localPlayerPositions.Count >= Mathf.Round(TimeTraveler.rewindTime / Time.fixedDeltaTime)) localPlayerPositions.RemoveAt(localPlayerPositions.Count - 1);
                localPlayerPositions.Insert(0, new Tuple<Vector3, DateTime>(PlayerControl.LocalPlayer.transform.position, DateTime.UtcNow)); // CanMove = CanMove
            }
        }

        static void squireSetTarget() {
            if (Squire.squire == null || Squire.squire != PlayerControl.LocalPlayer) return;
            Squire.currentTarget = setTarget();
            if (!Squire.usedShield) setPlayerOutline(Squire.currentTarget, Squire.shieldedColor);
        }

        static void roleThiefSetTarget() {
            if (RoleThief.rolethief == null || RoleThief.rolethief != PlayerControl.LocalPlayer) return;
            RoleThief.currentTarget = setTarget();
            setPlayerOutline(RoleThief.currentTarget, RoleThief.color);
        }
        static void fortuneTellerSetTarget() {
            if (FortuneTeller.fortuneTeller == null || FortuneTeller.fortuneTeller != PlayerControl.LocalPlayer) return;
            FortuneTeller.currentTarget = setTarget();
            setPlayerOutline(FortuneTeller.currentTarget, FortuneTeller.color);
            if (FortuneTeller.currentTarget != null && FortuneTeller.revealedPlayers.Any(p => p.Data.PlayerId == FortuneTeller.currentTarget.Data.PlayerId)) FortuneTeller.currentTarget = null; // Remove target if already revealed
        }

        static void mimicSetTarget() {
            if (Mimic.mimic == null || Mimic.mimic != PlayerControl.LocalPlayer) return;
            Mimic.currentTarget = setTarget();
            setPlayerOutline(Mimic.currentTarget, Mimic.color);
        }

        static void sheriffSetTarget() {
            if (Sheriff.sheriff == null || Sheriff.sheriff != PlayerControl.LocalPlayer) return;
            Sheriff.currentTarget = setTarget();
            setPlayerOutline(Sheriff.currentTarget, Sheriff.color);
        }

        static void sleuthSetTarget() {
            if (Sleuth.sleuth == null || Sleuth.sleuth != PlayerControl.LocalPlayer) return;
            Sleuth.currentTarget = setTarget();
            if (!Sleuth.usedLocate) setPlayerOutline(Sleuth.currentTarget, Sleuth.color);
        }

        static void detectiveUpdateFootPrints() {
            if (Detective.detective == null || Detective.detective != PlayerControl.LocalPlayer) return;

            Detective.timer -= Time.fixedDeltaTime;
            if (Detective.timer <= 0f) {
                Detective.timer = Detective.footprintIntervall;
                foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                    if (player != null && player != PlayerControl.LocalPlayer && !player.Data.IsDead && !player.inVent && !PlayerControl.LocalPlayer.Data.IsDead) {
                        new Footprint(Detective.footprintDuration, Detective.anonymousFootprints, player);
                    }
                }
            }
        }

        static void demonSetTarget() {
            if (Demon.demon == null || Demon.demon != PlayerControl.LocalPlayer) return;

            PlayerControl target = null;
            target = setTarget(true, true);

            bool targetNearNun = false;
            if (target != null) {
                foreach (Nun nun in Nun.nuns) {
                    if (Vector2.Distance(nun.nun.transform.position, target.transform.position) <= 1.91f) {
                        targetNearNun = true;
                    }
                }
            }
            Demon.targetNearNun = targetNearNun;
            Demon.currentTarget = target;
            setPlayerOutline(Demon.currentTarget, Demon.color);
        }

        static void renegadeSetTarget() {
            if (Renegade.renegade == null || Renegade.renegade != PlayerControl.LocalPlayer) return;
            var untargetablePlayers = new List<PlayerControl>();
            if (Minion.minion != null) untargetablePlayers.Add(Minion.minion);
            Renegade.currentTarget = setTarget(untargetablePlayers: untargetablePlayers);
            setPlayerOutline(Renegade.currentTarget, Palette.ImpostorRed);
        }

        static void minionSetTarget() {
            if (Minion.minion == null || Minion.minion != PlayerControl.LocalPlayer) return;
            var untargetablePlayers = new List<PlayerControl>();
            if (Renegade.renegade != null) untargetablePlayers.Add(Renegade.renegade);
            Minion.currentTarget = setTarget(untargetablePlayers: untargetablePlayers);
            setPlayerOutline(Minion.currentTarget, Palette.ImpostorRed);
        }

        static void trapperSetTarget() {
            if (Trapper.trapper == null || Trapper.trapper != PlayerControl.LocalPlayer) return;
            Trapper.currentTarget = setTarget();
            setPlayerOutline(Trapper.currentTarget, Trapper.color);
        }
        static void ventColorUpdate() {
            if (PlayerControl.LocalPlayer.Data.Role.IsImpostor && ShipStatus.Instance?.AllVents != null) {
                foreach (Vent vent in ShipStatus.Instance.AllVents) {
                    try {
                        if (vent?.myRend?.material != null) {
                            if (Renegade.renegade != null && Renegade.renegade.inVent || Minion.minion != null && Minion.minion.inVent) {
                                vent.myRend.material.SetFloat("_Outline", 1f);
                                vent.myRend.material.SetColor("_OutlineColor", Renegade.color);
                            }
                            else if (vent.myRend.material.GetColor("_AddColor") != Color.red) {
                                vent.myRend.material.SetFloat("_Outline", 0);
                            }
                        }
                    }
                    catch { }
                }
            }
        }

        static void impostorSetTarget() {
            if (!PlayerControl.LocalPlayer.Data.Role.IsImpostor || !PlayerControl.LocalPlayer.CanMove || PlayerControl.LocalPlayer.Data.IsDead) {
                HudManager.Instance.KillButton.SetTarget(null);
                return;
            }

            PlayerControl target = null;
            target = setTarget(true, true);

            HudManager.Instance.KillButton.SetTarget(target); 
        }

        static void manipulatorSetTarget() {
            if (Manipulator.manipulator == null || Manipulator.manipulator != PlayerControl.LocalPlayer) return;
            if (Manipulator.manipulatedVictim != null && (Manipulator.manipulatedVictim.Data.Disconnected || Manipulator.manipulatedVictim.Data.IsDead)) {
                // If the manipulated victim is disconnected or dead reset the manipulate so a new manipulate can be applied
                Manipulator.resetManipulate();
            }
            if (Manipulator.manipulatedVictim == null) {
                Manipulator.currentTarget = setTarget();
                setPlayerOutline(Manipulator.currentTarget, Manipulator.color);
            }
            else {
                Manipulator.manipulatedVictimTarget = setTarget(targetingPlayer: Manipulator.manipulatedVictim);
                setPlayerOutline(Manipulator.manipulatedVictimTarget, Manipulator.color);
            }
        }

        static void sleuthUpdate() {
            // Handle player locate
            if (Sleuth.arrow?.arrow != null) {
                if (Sleuth.sleuth == null || PlayerControl.LocalPlayer != Sleuth.sleuth) {
                    Sleuth.arrow.arrow.SetActive(false);
                    return;
                }

                if (Sleuth.sleuth != null && Sleuth.located != null && PlayerControl.LocalPlayer == Sleuth.sleuth && !Sleuth.sleuth.Data.IsDead) {
                    Sleuth.timeUntilUpdate -= Time.fixedDeltaTime;

                    if (Sleuth.timeUntilUpdate <= 0f) {
                        bool locatedOnMap = !Sleuth.sleuth.Data.IsDead;
                        Vector3 position = Sleuth.located.transform.position;
                        if (!locatedOnMap) { // Check for dead body
                            DeadBody body = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == Sleuth.located.PlayerId);
                            if (body != null) {
                                locatedOnMap = true;
                                position = body.transform.position;
                            }
                        }

                        Sleuth.arrow.Update(position);
                        Sleuth.arrow.arrow.SetActive(locatedOnMap);
                        Sleuth.timeUntilUpdate = Sleuth.updateIntervall;
                    } else {
                        Sleuth.arrow.Update();
                    }
                }
            }

            // Handle corpses locate
            if (Sleuth.sleuth != null && Sleuth.sleuth == PlayerControl.LocalPlayer && Sleuth.corpsesPathfindTimer >= 0f && !Sleuth.sleuth.Data.IsDead) {
                bool arrowsCountChanged = Sleuth.localArrows.Count != Sleuth.deadBodyPositions.Count();
                int index = 0;

                if (arrowsCountChanged) {
                    foreach (Arrow arrow in Sleuth.localArrows) UnityEngine.Object.Destroy(arrow.arrow);
                    Sleuth.localArrows = new List<Arrow>();
                }
                foreach (Vector3 position in Sleuth.deadBodyPositions) {
                    if (arrowsCountChanged) {
                        Sleuth.localArrows.Add(new Arrow(Sleuth.color));
                        Sleuth.localArrows[index].arrow.SetActive(true);
                    }
                    if (Sleuth.localArrows[index] != null) Sleuth.localArrows[index].Update(position);
                    index++;
                }
            } else if (Sleuth.localArrows.Count > 0) { 
                foreach (Arrow arrow in Sleuth.localArrows) UnityEngine.Object.Destroy(arrow.arrow);
                Sleuth.localArrows = new List<Arrow>();
            }
        }

        public static void welderSetTarget() {
            if (Welder.welder == null || Welder.welder != PlayerControl.LocalPlayer || ShipStatus.Instance == null || ShipStatus.Instance.AllVents == null) return;

            Vent target = null;
            Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
            float closestDistance = float.MaxValue;
            for (int i = 0; i < ShipStatus.Instance.AllVents.Length; i++) {
                Vent vent = ShipStatus.Instance.AllVents[i];
                if (vent.gameObject.name.StartsWith("Hat_") || vent.gameObject.name.StartsWith("SealedVent_") || vent.gameObject.name.StartsWith("FutureSealedVent_")) continue;
                float distance = Vector2.Distance(vent.transform.position, truePosition);
                if (distance <= vent.UsableDistance && distance < closestDistance) {
                    closestDistance = distance;
                    target = vent;
                }
            }
            Welder.ventTarget = target;
        }      

        public static void pyromaniacSetTarget() {
            if (Pyromaniac.pyromaniac == null || Pyromaniac.pyromaniac != PlayerControl.LocalPlayer) return;
            List<PlayerControl> untargetables;
            if (Pyromaniac.sprayTarget != null)
                untargetables = PlayerControl.AllPlayerControls.ToArray().Where(x => x.PlayerId != Pyromaniac.sprayTarget.PlayerId).ToList();
            else
                untargetables = Pyromaniac.sprayedPlayers;
            Pyromaniac.currentTarget = setTarget(untargetablePlayers: untargetables);
            if (Pyromaniac.currentTarget != null) setPlayerOutline(Pyromaniac.currentTarget, Pyromaniac.color);
        }

        static void bountyHunterSetTarget() {
            if (BountyHunter.bountyhunter == null || BountyHunter.bountyhunter != PlayerControl.LocalPlayer) return;
            BountyHunter.currentTarget = setTarget();
            setPlayerOutline(BountyHunter.currentTarget, BountyHunter.color);
        }

        static void yinyangerSetTarget() {
            if (Yinyanger.yinyanger == null || Yinyanger.yinyanger != PlayerControl.LocalPlayer) return;
            Yinyanger.currentTarget = setTarget();
            setPlayerOutline(Yinyanger.currentTarget, Yinyanger.color);
        }

        static void challengerSetTarget() {
            if (Challenger.challenger == null || Challenger.challenger != PlayerControl.LocalPlayer) return;
            Challenger.currentTarget = setTarget();
            setPlayerOutline(Challenger.currentTarget, Challenger.color);
        }

        static void finkUpdate() {
            if (Fink.localArrows == null) return;

            foreach (Arrow arrow in Fink.localArrows) arrow.arrow.SetActive(false);

            if (Fink.fink == null || Fink.fink.Data.IsDead) return;

            var (playerCompleted, playerTotal) = TasksHandler.taskInfo(Fink.fink.Data);
            int numberOfTasks = playerTotal - playerCompleted;

            if (numberOfTasks <= Fink.taskCountForImpostors && (PlayerControl.LocalPlayer.Data.Role.IsImpostor || (Fink.includeTeamRenegade && (PlayerControl.LocalPlayer == Renegade.renegade || PlayerControl.LocalPlayer == Minion.minion)))) {
                if (Fink.localArrows.Count == 0) Fink.localArrows.Add(new Arrow(Fink.color));
                if (Fink.localArrows.Count != 0 && Fink.localArrows[0] != null) {
                    Fink.localArrows[0].arrow.SetActive(true);
                    Fink.localArrows[0].Update(Fink.fink.transform.position);
                }
            }
            else if (PlayerControl.LocalPlayer == Fink.fink && numberOfTasks == 0) {
                int arrowIndex = 0;
                foreach (PlayerControl p in PlayerControl.AllPlayerControls) {
                    bool arrowForImp = p.Data.Role.IsImpostor;
                    bool arrowForTeamRenegade = Fink.includeTeamRenegade && (p == Renegade.renegade || p == Minion.minion);

                    if (!p.Data.IsDead && (arrowForImp || arrowForTeamRenegade)) {
                        if (arrowIndex >= Fink.localArrows.Count) {
                            Fink.localArrows.Add(new Arrow(Color.red));
                            if (p == Renegade.renegade || p == Minion.minion) Fink.localArrows.Add(new Arrow(Renegade.color));
                        }
                        if (arrowIndex < Fink.localArrows.Count && Fink.localArrows[arrowIndex] != null) {
                            Fink.localArrows[arrowIndex].arrow.SetActive(true);
                            Fink.localArrows[arrowIndex].Update(p.transform.position);
                        }
                        arrowIndex++;
                    }
                }
            }
        }

        static void spiritualistUpdate() {
            if (Spiritualist.revivedPlayer != null) {
                if (!Spiritualist.revivedPlayer.Data.IsDead && (PlayerControl.LocalPlayer.Data.Role.IsImpostor || PlayerControl.LocalPlayer == Renegade.renegade || PlayerControl.LocalPlayer == Minion.minion || PlayerControl.LocalPlayer == BountyHunter.bountyhunter || PlayerControl.LocalPlayer == Trapper.trapper || PlayerControl.LocalPlayer == Yinyanger.yinyanger || PlayerControl.LocalPlayer == Challenger.challenger)) {
                    if (Spiritualist.localSpiritArrows.Count == 0) Spiritualist.localSpiritArrows.Add(new Arrow(Spiritualist.color));
                    if (Spiritualist.localSpiritArrows.Count != 0 && Spiritualist.localSpiritArrows[0] != null) {
                        Spiritualist.localSpiritArrows[0].arrow.SetActive(true);
                        Spiritualist.localSpiritArrows[0].Update(Spiritualist.revivedPlayer.transform.position);
                    }
                }
                else {
                    if (Spiritualist.localSpiritArrows.Count != 0) {
                        Spiritualist.localSpiritArrows[0].arrow.SetActive(false);
                    }
                }
            }
        }

        static void theChosenOneUpdate() {
            if (TheChosenOne.theChosenOne == null || TheChosenOne.theChosenOne != PlayerControl.LocalPlayer) return;

            // TheChosenOne report
            if (TheChosenOne.theChosenOne.Data.IsDead && !TheChosenOne.reported) {
                TheChosenOne.reportDelay -= Time.fixedDeltaTime;
                DeadPlayer deadPlayer = deadPlayers?.Where(x => x.player?.PlayerId == TheChosenOne.theChosenOne.PlayerId)?.FirstOrDefault();
                if (deadPlayer.killerIfExisting != null && TheChosenOne.reportDelay <= 0f) {
                    Helpers.handleDemonBiteOnBodyReport(); // Manually call Demon handling, since the CmdReportDeadBody Prefix won't be called
                    RPCProcedure.uncheckedCmdReportDeadBody(deadPlayer.killerIfExisting.PlayerId, TheChosenOne.theChosenOne.PlayerId);

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UncheckedCmdReportDeadBody, Hazel.SendOption.Reliable, -1);
                    writer.Write(deadPlayer.killerIfExisting.PlayerId);
                    writer.Write(TheChosenOne.theChosenOne.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    TheChosenOne.reported = true;

                    MessageWriter writermusic = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ChangeMusic, Hazel.SendOption.Reliable, -1);
                    writermusic.Write(1);
                    AmongUsClient.Instance.FinishRpcImmediately(writermusic);
                    RPCProcedure.changeMusic(1);
                }
            }
        }

        static void performerUpdate() {
            if (Performer.performer != null) {
                if (Performer.duration > 0 && Performer.performer.Data.IsDead && !Performer.reported && (PlayerControl.LocalPlayer != Performer.performer && PlayerControl.LocalPlayer != Spiritualist.spiritualist && PlayerControl.LocalPlayer != TimeTraveler.timeTraveler)) {
                    if (Performer.localPerformerArrows.Count == 0) Performer.localPerformerArrows.Add(new Arrow(Performer.color));
                    if (Performer.localPerformerArrows.Count != 0 && Performer.localPerformerArrows[0] != null) {
                        Performer.localPerformerArrows[0].arrow.SetActive(true);
                        var bodyPerformer = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == Performer.performer.PlayerId);
                        Performer.localPerformerArrows[0].Update(bodyPerformer.transform.position);
                    }
                }
                else {
                    if (Performer.localPerformerArrows.Count != 0) {
                        Performer.localPerformerArrows[0].arrow.SetActive(false);

                    }
                }

                // Upon performer duration, stop the music and play bomb music if there's a bomb or normal task music
                if (Performer.performer.Data.IsDead && Performer.duration <= 0 && !Performer.musicStop && !Performer.reported && (PlayerControl.LocalPlayer != Spiritualist.spiritualist && PlayerControl.LocalPlayer != TimeTraveler.timeTraveler)) {
                    Performer.musicStop = true;
                    SoundManager.Instance.StopSound(CustomMain.customAssets.performerMusic);
                    if (Bomberman.activeBomb) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.bombermanBombMusic, true, 75f);
                    }
                    else {
                        RPCProcedure.changeMusic(2);
                    }
                }
            }
        }

        public static void forensicSetTarget() {
            if (Forensic.forensic == null || Forensic.forensic != PlayerControl.LocalPlayer || Forensic.forensic.Data.IsDead || Forensic.deadBodies == null || ShipStatus.Instance?.AllVents == null) return;

            DeadPlayer target = null;
            Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
            float closestDistance = float.MaxValue;
            float usableDistance = ShipStatus.Instance.AllVents.FirstOrDefault().UsableDistance;
            foreach ((DeadPlayer dp, Vector3 ps) in Forensic.deadBodies) {
                float distance = Vector2.Distance(ps, truePosition);
                if (distance <= usableDistance && distance < closestDistance) {
                    closestDistance = distance;
                    target = dp;
                }
            }
            Forensic.target = target;
        }

        static void hunterSetTarget() {
            if (Hunter.hunter == null || Hunter.hunter != PlayerControl.LocalPlayer) return;
            Hunter.currentTarget = setTarget();
            if (!Hunter.usedHunted) setPlayerOutline(Hunter.currentTarget, Hunter.color);
        }
        
        static void mimicAndPainterUpdate() {
            float oldPaintTimer = Painter.painterTimer;
            float oldMimicTimer = Mimic.transformTimer;
            Painter.painterTimer = Mathf.Max(0f, Painter.painterTimer - Time.fixedDeltaTime);
            Mimic.transformTimer = Mathf.Max(0f, Mimic.transformTimer - Time.fixedDeltaTime);


            // Paint reset and set Mimic look if necessary
            if (oldPaintTimer > 0f && Painter.painterTimer <= 0f) {
                Painter.resetPaint();
                if (Mimic.transformTimer > 0f && Mimic.mimic != null && Mimic.transformTarget != null) {
                    PlayerControl target = Mimic.transformTarget;
                    Mimic.mimic.setLook(target.Data.PlayerName, target.Data.DefaultOutfit.ColorId, target.Data.DefaultOutfit.HatId, target.Data.DefaultOutfit.VisorId, target.Data.DefaultOutfit.SkinId, target.Data.DefaultOutfit.PetId);
                }
            }

            // Mimic reset (only if paint is inactive)
            if (Painter.painterTimer <= 0f && oldMimicTimer > 0f && Mimic.transformTimer <= 0f && Mimic.mimic != null)
                Mimic.resetMimic();
        }

        public static void hackerUpdate() {
            if (Hacker.hacker == null || PlayerControl.LocalPlayer != Hacker.hacker || Hacker.hacker.Data.IsDead) return;
            var (playerCompleted, _) = TasksHandler.taskInfo(Hacker.hacker.Data);
            if (playerCompleted == Hacker.rechargedTasks) {
                MessageWriter usedRechargeWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.HackerAbilityUses, Hazel.SendOption.Reliable, -1);
                usedRechargeWriter.Write(2);
                AmongUsClient.Instance.FinishRpcImmediately(usedRechargeWriter);
                RPCProcedure.hackerAbilityUses(2); 
            }
        }

        static void jinxSetTarget() {
            if (Jinx.jinx == null || Jinx.jinx != PlayerControl.LocalPlayer) return;
            Jinx.target = setTarget();
            setPlayerOutline(Jinx.target, Jinx.color);
            if (Jinx.target != null && Jinx.jinxedList.Any(p => p.Data.PlayerId == Jinx.target.Data.PlayerId)) Jinx.target = null; // Remove target if already Jinxed and didn't trigger the jinx
        }

        static void sorcererSetTarget() {
            if (Sorcerer.sorcerer == null || Sorcerer.sorcerer != PlayerControl.LocalPlayer) return;
            List<PlayerControl> untargetables;
            if (Sorcerer.spellTarget != null)
                untargetables = PlayerControl.AllPlayerControls.ToArray().Where(x => x.PlayerId != Sorcerer.spellTarget.PlayerId).ToList(); // Don't switch the target from the the one you're currently casting a spell on
            else {
                untargetables = new List<PlayerControl>(); // Also target players that have already been spelled, to hide spells that were jinxs/blocked by shields
            }
            Sorcerer.currentTarget = setTarget(onlyCrewmates: false, untargetablePlayers: untargetables);
            setPlayerOutline(Sorcerer.currentTarget, Sorcerer.color);
        }

        public static void vigilantUpdate() {
            if (Vigilant.vigilant == null || PlayerControl.LocalPlayer != Vigilant.vigilant || Vigilant.vigilant.Data.IsDead) return;
            var (playerCompleted, _) = TasksHandler.taskInfo(Vigilant.vigilant.Data);
            if (playerCompleted == Vigilant.rechargedTasks) {
                MessageWriter usedRechargeWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.VigilantAbilityUses, Hazel.SendOption.Reliable, -1);
                usedRechargeWriter.Write(2);
                AmongUsClient.Instance.FinishRpcImmediately(usedRechargeWriter);
                RPCProcedure.vigilantAbilityUses(2); 
            }
        }
        
        
        static void captureTheFlagSetTarget() {

            if ((!CaptureTheFlag.captureTheFlagMode && PoliceAndThief.policeAndThiefMode) || (CaptureTheFlag.captureTheFlagMode && PoliceAndThief.policeAndThiefMode))
                return;

            var untargetableRedPlayers = new List<PlayerControl>();
            foreach (PlayerControl player in CaptureTheFlag.redteamFlag) {
                untargetableRedPlayers.Add(player);
            }

            if (CaptureTheFlag.redplayer01 != null && CaptureTheFlag.redplayer01 == PlayerControl.LocalPlayer) {
                CaptureTheFlag.redplayer01currentTarget = setTarget(untargetablePlayers: untargetableRedPlayers);
                setPlayerOutline(CaptureTheFlag.redplayer01currentTarget, Palette.ImpostorRed);
            }
            if (CaptureTheFlag.redplayer02 != null && CaptureTheFlag.redplayer02 == PlayerControl.LocalPlayer) {
                CaptureTheFlag.redplayer02currentTarget = setTarget(untargetablePlayers: untargetableRedPlayers);
                setPlayerOutline(CaptureTheFlag.redplayer02currentTarget, Palette.ImpostorRed);
            }
            if (CaptureTheFlag.redplayer03 != null && CaptureTheFlag.redplayer03 == PlayerControl.LocalPlayer) {
                CaptureTheFlag.redplayer03currentTarget = setTarget(untargetablePlayers: untargetableRedPlayers);
                setPlayerOutline(CaptureTheFlag.redplayer03currentTarget, Palette.ImpostorRed);
            }
            if (CaptureTheFlag.redplayer04 != null && CaptureTheFlag.redplayer04 == PlayerControl.LocalPlayer) {
                CaptureTheFlag.redplayer04currentTarget = setTarget(untargetablePlayers: untargetableRedPlayers);
                setPlayerOutline(CaptureTheFlag.redplayer04currentTarget, Palette.ImpostorRed);
            }
            if (CaptureTheFlag.redplayer05 != null && CaptureTheFlag.redplayer05 == PlayerControl.LocalPlayer) {
                CaptureTheFlag.redplayer05currentTarget = setTarget(untargetablePlayers: untargetableRedPlayers);
                setPlayerOutline(CaptureTheFlag.redplayer05currentTarget, Palette.ImpostorRed);
            }
            if (CaptureTheFlag.redplayer06 != null && CaptureTheFlag.redplayer06 == PlayerControl.LocalPlayer) {
                CaptureTheFlag.redplayer06currentTarget = setTarget(untargetablePlayers: untargetableRedPlayers);
                setPlayerOutline(CaptureTheFlag.redplayer06currentTarget, Palette.ImpostorRed);
            }
            if (CaptureTheFlag.redplayer07 != null && CaptureTheFlag.redplayer07 == PlayerControl.LocalPlayer) {
                CaptureTheFlag.redplayer07currentTarget = setTarget(untargetablePlayers: untargetableRedPlayers);
                setPlayerOutline(CaptureTheFlag.redplayer07currentTarget, Palette.ImpostorRed);
            }

            var untargetableBluePlayers = new List<PlayerControl>();
            foreach (PlayerControl player in CaptureTheFlag.blueteamFlag) {
                untargetableBluePlayers.Add(player);
            }

            if (CaptureTheFlag.blueplayer01 != null && CaptureTheFlag.blueplayer01 == PlayerControl.LocalPlayer) {
                CaptureTheFlag.blueplayer01currentTarget = setTarget(untargetablePlayers: untargetableBluePlayers);
                setPlayerOutline(CaptureTheFlag.blueplayer01currentTarget, Color.blue);
            }
            if (CaptureTheFlag.blueplayer02 != null && CaptureTheFlag.blueplayer02 == PlayerControl.LocalPlayer) {
                CaptureTheFlag.blueplayer02currentTarget = setTarget(untargetablePlayers: untargetableBluePlayers);
                setPlayerOutline(CaptureTheFlag.blueplayer02currentTarget, Color.blue);
            }
            if (CaptureTheFlag.blueplayer03 != null && CaptureTheFlag.blueplayer03 == PlayerControl.LocalPlayer) {
                CaptureTheFlag.blueplayer03currentTarget = setTarget(untargetablePlayers: untargetableBluePlayers);
                setPlayerOutline(CaptureTheFlag.blueplayer03currentTarget, Color.blue);
            }
            if (CaptureTheFlag.blueplayer04 != null && CaptureTheFlag.blueplayer04 == PlayerControl.LocalPlayer) {
                CaptureTheFlag.blueplayer04currentTarget = setTarget(untargetablePlayers: untargetableBluePlayers);
                setPlayerOutline(CaptureTheFlag.blueplayer04currentTarget, Color.blue);
            }
            if (CaptureTheFlag.blueplayer05 != null && CaptureTheFlag.blueplayer05 == PlayerControl.LocalPlayer) {
                CaptureTheFlag.blueplayer05currentTarget = setTarget(untargetablePlayers: untargetableBluePlayers);
                setPlayerOutline(CaptureTheFlag.blueplayer05currentTarget, Color.blue);
            }
            if (CaptureTheFlag.blueplayer06 != null && CaptureTheFlag.blueplayer06 == PlayerControl.LocalPlayer) {
                CaptureTheFlag.blueplayer06currentTarget = setTarget(untargetablePlayers: untargetableBluePlayers);
                setPlayerOutline(CaptureTheFlag.blueplayer06currentTarget, Color.blue);
            }
            if (CaptureTheFlag.blueplayer07 != null && CaptureTheFlag.blueplayer07 == PlayerControl.LocalPlayer) {
                CaptureTheFlag.blueplayer07currentTarget = setTarget(untargetablePlayers: untargetableBluePlayers);
                setPlayerOutline(CaptureTheFlag.blueplayer07currentTarget, Color.blue);
            }
            if (CaptureTheFlag.blueplayer08 != null && CaptureTheFlag.blueplayer08 == PlayerControl.LocalPlayer) {
                CaptureTheFlag.blueplayer08currentTarget = setTarget(untargetablePlayers: untargetableBluePlayers);
                setPlayerOutline(CaptureTheFlag.blueplayer08currentTarget, Color.blue);
            }
        }

        static void policeandThiefSetTarget() {

            if ((!PoliceAndThief.policeAndThiefMode && CaptureTheFlag.captureTheFlagMode) || (CaptureTheFlag.captureTheFlagMode && PoliceAndThief.policeAndThiefMode))
                return;

            var untargetablePolice = new List<PlayerControl>();
            foreach (PlayerControl player in PoliceAndThief.policeTeam) {
                untargetablePolice.Add(player);
            }

            if (PoliceAndThief.policeplayer01 != null && PoliceAndThief.policeplayer01 == PlayerControl.LocalPlayer) {
                PoliceAndThief.policeplayer01currentTarget = setTarget(untargetablePlayers: untargetablePolice);
                setPlayerOutline(PoliceAndThief.policeplayer01currentTarget, Cheater.color);
            }
            if (PoliceAndThief.policeplayer02 != null && PoliceAndThief.policeplayer02 == PlayerControl.LocalPlayer) {
                PoliceAndThief.policeplayer02currentTarget = setTarget(untargetablePlayers: untargetablePolice);
                setPlayerOutline(PoliceAndThief.policeplayer02currentTarget, Cheater.color);
            }
            if (PoliceAndThief.policeplayer03 != null && PoliceAndThief.policeplayer03 == PlayerControl.LocalPlayer) {
                PoliceAndThief.policeplayer03currentTarget = setTarget(untargetablePlayers: untargetablePolice);
                setPlayerOutline(PoliceAndThief.policeplayer03currentTarget, Cheater.color);
            }
            if (PoliceAndThief.policeplayer04 != null && PoliceAndThief.policeplayer04 == PlayerControl.LocalPlayer) {
                PoliceAndThief.policeplayer04currentTarget = setTarget(untargetablePlayers: untargetablePolice);
                setPlayerOutline(PoliceAndThief.policeplayer04currentTarget, Cheater.color);
            }
            if (PoliceAndThief.policeplayer05 != null && PoliceAndThief.policeplayer05 == PlayerControl.LocalPlayer) {
                PoliceAndThief.policeplayer05currentTarget = setTarget(untargetablePlayers: untargetablePolice);
                setPlayerOutline(PoliceAndThief.policeplayer05currentTarget, Cheater.color);
            }

            var untargetableThiefs = new List<PlayerControl>();
            foreach (PlayerControl player in PoliceAndThief.thiefTeam) {
                untargetableThiefs.Add(player);
            }

            if (PoliceAndThief.thiefplayer01 != null && PoliceAndThief.thiefplayer01 == PlayerControl.LocalPlayer) {
                PoliceAndThief.thiefplayer01currentTarget = setTarget(untargetablePlayers: untargetableThiefs);
                setPlayerOutline(PoliceAndThief.thiefplayer01currentTarget, Mechanic.color);
            }
            if (PoliceAndThief.thiefplayer02 != null && PoliceAndThief.thiefplayer02 == PlayerControl.LocalPlayer) {
                PoliceAndThief.thiefplayer02currentTarget = setTarget(untargetablePlayers: untargetableThiefs);
                setPlayerOutline(PoliceAndThief.thiefplayer02currentTarget, Mechanic.color);
            }
            if (PoliceAndThief.thiefplayer03 != null && PoliceAndThief.thiefplayer03 == PlayerControl.LocalPlayer) {
                PoliceAndThief.thiefplayer03currentTarget = setTarget(untargetablePlayers: untargetableThiefs);
                setPlayerOutline(PoliceAndThief.thiefplayer03currentTarget, Mechanic.color);
            }
            if (PoliceAndThief.thiefplayer04 != null && PoliceAndThief.thiefplayer04 == PlayerControl.LocalPlayer) {
                PoliceAndThief.thiefplayer04currentTarget = setTarget(untargetablePlayers: untargetableThiefs);
                setPlayerOutline(PoliceAndThief.thiefplayer04currentTarget, Mechanic.color);
            }
            if (PoliceAndThief.thiefplayer05 != null && PoliceAndThief.thiefplayer05 == PlayerControl.LocalPlayer) {
                PoliceAndThief.thiefplayer05currentTarget = setTarget(untargetablePlayers: untargetableThiefs);
                setPlayerOutline(PoliceAndThief.thiefplayer05currentTarget, Mechanic.color);
            }
            if (PoliceAndThief.thiefplayer06 != null && PoliceAndThief.thiefplayer06 == PlayerControl.LocalPlayer) {
                PoliceAndThief.thiefplayer06currentTarget = setTarget(untargetablePlayers: untargetableThiefs);
                setPlayerOutline(PoliceAndThief.thiefplayer06currentTarget, Mechanic.color);
            }
            if (PoliceAndThief.thiefplayer07 != null && PoliceAndThief.thiefplayer07 == PlayerControl.LocalPlayer) {
                PoliceAndThief.thiefplayer07currentTarget = setTarget(untargetablePlayers: untargetableThiefs);
                setPlayerOutline(PoliceAndThief.thiefplayer07currentTarget, Mechanic.color);
            }
            if (PoliceAndThief.thiefplayer08 != null && PoliceAndThief.thiefplayer08 == PlayerControl.LocalPlayer) {
                PoliceAndThief.thiefplayer08currentTarget = setTarget(untargetablePlayers: untargetableThiefs);
                setPlayerOutline(PoliceAndThief.thiefplayer08currentTarget, Mechanic.color);
            }
            if (PoliceAndThief.thiefplayer09 != null && PoliceAndThief.thiefplayer09 == PlayerControl.LocalPlayer) {
                PoliceAndThief.thiefplayer09currentTarget = setTarget(untargetablePlayers: untargetableThiefs);
                setPlayerOutline(PoliceAndThief.thiefplayer09currentTarget, Mechanic.color);
            }
            if (PoliceAndThief.thiefplayer10 != null && PoliceAndThief.thiefplayer10 == PlayerControl.LocalPlayer) {
                PoliceAndThief.thiefplayer10currentTarget = setTarget(untargetablePlayers: untargetableThiefs);
                setPlayerOutline(PoliceAndThief.thiefplayer10currentTarget, Mechanic.color);
            }
        }

        public static void Postfix(PlayerControl __instance) {
            if (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started) return;

            if (PlayerControl.LocalPlayer == __instance) {
                // Update player outlines
                setBasePlayerOutlines();

                // Update Role Description
                Helpers.refreshRoleDescription(__instance);

                // TimeTraveler
                bendTimeUpdate();

                // Mimic
                mimicSetTarget();

                // Squire
                squireSetTarget();

                // RoleThief
                roleThiefSetTarget();

                // FortuneTeller
                fortuneTellerSetTarget();

                // Sheriff
                sheriffSetTarget();

                // Detective
                detectiveUpdateFootPrints();

                // Sleuth
                sleuthSetTarget();
                sleuthUpdate();

                // Demon
                demonSetTarget();
                Nun.UpdateAll();

                // Ventcolor
                ventColorUpdate();

                // Renegade
                renegadeSetTarget();

                // Minion
                minionSetTarget();

                // Impostor
                impostorSetTarget();

                // Manipulator
                manipulatorSetTarget();

                // Sorcerer
                sorcererSetTarget();

                // Welder
                welderSetTarget();

                // Vigilant
                vigilantUpdate();

                // Pyromaniac
                pyromaniacSetTarget();

                // Fink
                finkUpdate();

                // BountyHunter
                bountyHunterSetTarget();

                // Yinyanger
                yinyangerSetTarget();

                // TheChosenOne
                theChosenOneUpdate();

                // Spiritualist Update
                spiritualistUpdate();

                // Trapper
                trapperSetTarget();

                // Challenger
                challengerSetTarget();

                // Performer Update
                performerUpdate();

                // Hunter
                hunterSetTarget();

                // Mimic and Painter
                mimicAndPainterUpdate();

                // Forensic
                forensicSetTarget();

                // Jinx
                jinxSetTarget();

                // Hacker
                hackerUpdate();

                // Capture the flag update
                captureTheFlagSetTarget();

                // Police and Thief update
                policeandThiefSetTarget();
            }
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CmdReportDeadBody))]
    class PlayerControlCmdReportDeadBodyPatch {
        public static void Prefix(PlayerControl __instance) {
            // Bomberman bomb reset when report body
            if (Bomberman.bomberman != null && Bomberman.activeBomb == true) {
                MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.FixBomb, Hazel.SendOption.Reliable, -1);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
                RPCProcedure.fixBomb();
            }

            //If music option is enabled and the player who used emergency button was not a bitten player
            if (PlayerControl.LocalPlayer != Demon.bitten || Demon.bitten == null) {
                MessageWriter writermusic = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ChangeMusic, Hazel.SendOption.Reliable, -1);
                writermusic.Write(1);
                AmongUsClient.Instance.FinishRpcImmediately(writermusic);
                RPCProcedure.changeMusic(1);
            }

            // Murder the bitten player before the meeting starts or reset the bitten player
            Helpers.handleDemonBiteOnBodyReport();

            // Murder the Spiritualist if someone reports a body or call emergency while trying to revive another player
            if (Spiritualist.spiritualist != null && Spiritualist.isReviving && Spiritualist.canRevive) {
                MessageWriter murderSpiritualist = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.MurderSpiritualistIfReportWhileReviving, Hazel.SendOption.Reliable, -1);
                AmongUsClient.Instance.FinishRpcImmediately(murderSpiritualist);
                RPCProcedure.murderSpiritualistIfReportWhileReviving();
            }

            // Chameleon reset when emergency call or report body
            if (Chameleon.chameleon != null) {
                Chameleon.resetChameleon();
            }

            // Challenger prevent duel if there's a meeting
            if (Challenger.challenger != null) {
                MessageWriter notifyCantDuel = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ChallengerCantDuel, Hazel.SendOption.Reliable, -1);
                AmongUsClient.Instance.FinishRpcImmediately(notifyCantDuel);
                RPCProcedure.challengerCantDuel();
            }

            // Performer isreported
            if (Performer.performer != null && Performer.performer.Data.IsDead && !Performer.reported) {
                MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PerformerIsReported, Hazel.SendOption.Reliable, -1);
                writer.Write(0);
                AmongUsClient.Instance.FinishRpcImmediately(writer);
                RPCProcedure.performerIsReported(0);
            }
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.LocalPlayer.CmdReportDeadBody))]
    class BodyReportPatch
    {
        static void Postfix(PlayerControl __instance, [HarmonyArgument(0)] GameData.PlayerInfo target) {
            // Forensic report
            bool isForensicReport = Forensic.forensic != null && Forensic.forensic == PlayerControl.LocalPlayer && __instance.PlayerId == Forensic.forensic.PlayerId;
            if (isForensicReport) {
                DeadPlayer deadPlayer = deadPlayers?.Where(x => x.player?.PlayerId == target?.PlayerId)?.FirstOrDefault();

                if (deadPlayer != null && deadPlayer.killerIfExisting != null) {
                    float timeSinceDeath = ((float)(DateTime.UtcNow - deadPlayer.timeOfDeath).TotalMilliseconds);
                    string msg = "";

                    if (isForensicReport) {
                        if (deadPlayer.player == RoleThief.rolethief && deadPlayer.killerIfExisting.Data.PlayerName == RoleThief.rolethief.Data.PlayerName) {
                            msg = $"Body Report (Role Thief): It appears to be a suicide! ({Math.Round(timeSinceDeath / 1000)} seconds ago)";
                        }
                        else if (deadPlayer.player == BountyHunter.bountyhunter && deadPlayer.killerIfExisting.Data.PlayerName == BountyHunter.bountyhunter.Data.PlayerName) {
                            msg = $"Body Report (Bounty Hunter): It appears to be a suicide! ({Math.Round(timeSinceDeath / 1000)} seconds ago)";
                        }
                        else if (deadPlayer.player == Modifiers.lover1 && deadPlayer.killerIfExisting.Data.PlayerName == Modifiers.lover1.Data.PlayerName || deadPlayer.player == Modifiers.lover2 && deadPlayer.killerIfExisting.Data.PlayerName == Modifiers.lover2.Data.PlayerName) {
                            msg = $"Body Report (Lover): It appears to be a suicide! ({Math.Round(timeSinceDeath / 1000)} seconds ago)";
                        }
                        else if (deadPlayer.player == Sheriff.sheriff && deadPlayer.killerIfExisting.Data.PlayerName == Sheriff.sheriff.Data.PlayerName) {
                            msg = $"Body Report (Sheriff): It appears to be a suicide! ({Math.Round(timeSinceDeath / 1000)} seconds ago)";
                        }
                        else if (timeSinceDeath < Forensic.reportNameDuration * 1000) {
                            msg = $"Body Report: The killer appears to be {deadPlayer.killerIfExisting.Data.PlayerName}! ({Math.Round(timeSinceDeath / 1000)} seconds ago)";
                        }
                        else if (timeSinceDeath < Forensic.reportColorDuration * 1000) {
                            var typeOfColor = Helpers.isLighterColor(deadPlayer.killerIfExisting.Data.DefaultOutfit.ColorId) ? "lighter (L)" : "darker (D)";
                            msg = $"Body Report: The killer appears to have a {typeOfColor} color! ({Math.Round(timeSinceDeath / 1000)} seconds ago)";
                        }
                        else if (timeSinceDeath < Forensic.reportClueDuration * 1000) {
                            int randomClue = rnd.Next(1, 5);
                            switch (randomClue) {
                                case 1:
                                    if (deadPlayer.killerIfExisting.Data.DefaultOutfit.HatId != null) {
                                        msg = $"Body Report: The killer appears to wear a hat! ({Math.Round(timeSinceDeath / 1000)} seconds ago)";
                                    }
                                    else {
                                        msg = $"Body Report: The killer doesn't wear a hat! ({Math.Round(timeSinceDeath / 1000)} seconds ago)";
                                    }
                                    break;
                                case 2:
                                    if (deadPlayer.killerIfExisting.Data.DefaultOutfit.SkinId != null) {
                                        msg = $"Body Report: The killer appears to wear an outfit! ({Math.Round(timeSinceDeath / 1000)} seconds ago)";
                                    }
                                    else {
                                        msg = $"Body Report: The killer doesn't wear an outfit! ({Math.Round(timeSinceDeath / 1000)} seconds ago)";
                                    }
                                    break;
                                case 3:
                                    if (deadPlayer.killerIfExisting.Data.DefaultOutfit.PetId != null) {
                                        msg = $"Body Report: The killer appears to have a pet! ({Math.Round(timeSinceDeath / 1000)} seconds ago)";
                                    }
                                    else {
                                        msg = $"Body Report: The killer hasn't got a pet! ({Math.Round(timeSinceDeath / 1000)} seconds ago)";
                                    }
                                    break;
                                case 4:
                                    if (deadPlayer.killerIfExisting.Data.DefaultOutfit.VisorId != null) {
                                        msg = $"Body Report: The killer appears to wear a visor! ({Math.Round(timeSinceDeath / 1000)} seconds ago)";
                                    }
                                    else {
                                        msg = $"Body Report: The killer doesn't wear a visor! ({Math.Round(timeSinceDeath / 1000)} seconds ago)";
                                    }
                                    break;
                            }
                        }
                        else {
                            msg = $"Body Report: The body is too old to gain information from! ({Math.Round(timeSinceDeath / 1000)} seconds ago)";
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(msg)) {
                        if (AmongUsClient.Instance.AmClient && DestroyableSingleton<HudManager>.Instance) {
                            DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, msg);
                        }
                        if (msg.IndexOf("who", StringComparison.OrdinalIgnoreCase) >= 0) {
                            DestroyableSingleton<Assets.CoreScripts.Telemetry>.Instance.SendWho();
                        }
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
    public static class MurderPlayerPatch
    {
        public static bool resetToCrewmate = false;
        public static bool resetToDead = false;

        public static void Prefix(PlayerControl __instance, [HarmonyArgument(0)] PlayerControl target) {
            // Allow everyone to murder players
            resetToCrewmate = !__instance.Data.Role.IsImpostor;
            resetToDead = __instance.Data.IsDead;
            __instance.Data.Role.TeamType = RoleTeamTypes.Impostor;
            __instance.Data.IsDead = false;
        }

        public static void Postfix(PlayerControl __instance, [HarmonyArgument(0)] PlayerControl target) {
            // Collect dead player info
            DeadPlayer deadPlayer = new DeadPlayer(target, DateTime.UtcNow, DeathReason.Kill, __instance);
            GameHistory.deadPlayers.Add(deadPlayer);

            // Reset killer to crewmate if resetToCrewmate
            if (resetToCrewmate) __instance.Data.Role.TeamType = RoleTeamTypes.Crewmate;
            if (resetToDead) __instance.Data.IsDead = true;

            // Remove fake tasks when player dies
            if (target.hasFakeTasks())
                target.clearAllTasks();

            // Lover suicide trigger on murder
            if ((Modifiers.lover1 != null && target == Modifiers.lover1) || (Modifiers.lover2 != null && target == Modifiers.lover2)) {
                PlayerControl otherLover = target == Modifiers.lover1 ? Modifiers.lover2 : Modifiers.lover1;
                if (otherLover != null && !otherLover.Data.IsDead) {
                    otherLover.MurderPlayer(otherLover);
                }
            }

            // Janitor Button Sync
            if (Janitor.janitor != null && PlayerControl.LocalPlayer == Janitor.janitor && __instance == Janitor.janitor && HudManagerStartPatch.janitorCleanButton != null)
                HudManagerStartPatch.janitorCleanButton.Timer = Janitor.janitor.killTimer;

            // Manipulator Button Sync
            if (Manipulator.manipulator != null && PlayerControl.LocalPlayer == Manipulator.manipulator && __instance == Manipulator.manipulator && HudManagerStartPatch.manipulatorManipulateButton != null) {
                if (Manipulator.manipulator.killTimer > HudManagerStartPatch.manipulatorManipulateButton.Timer) {
                    HudManagerStartPatch.manipulatorManipulateButton.Timer = Manipulator.manipulator.killTimer;
                }
            }

            // Sorcerer Button Sync
            if (Sorcerer.sorcerer != null && PlayerControl.LocalPlayer == Sorcerer.sorcerer && __instance == Sorcerer.sorcerer && HudManagerStartPatch.sorcererSpellButton != null)
                HudManagerStartPatch.sorcererSpellButton.Timer = HudManagerStartPatch.sorcererSpellButton.MaxTimer;

            // Kid trigger win on murder
            if (Kid.kid != null && target == Kid.kid) {
                Kid.triggerKidLose = true;
                ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.KidLose, false);
            }

            //Chameleon reset invisibility
            if (Chameleon.chameleon != null && target == Chameleon.chameleon) {
                Chameleon.resetChameleon();
            }

            // BountyHunter suicide trigger if his target is murdered
            if (BountyHunter.bountyhunter != null && target == BountyHunter.hasToKill && BountyHunter.bountyhunter != __instance) {
                if (!BountyHunter.bountyhunter.Data.IsDead) {
                    BountyHunter.bountyhunter.MurderPlayer(BountyHunter.bountyhunter);
                }
            }

            // Yinyanger, reset both targets if he gets killed
            if (Yinyanger.yinyanger != null && target == Yinyanger.yinyanger) {
                Yinyanger.yinyedplayer = null;
                Yinyanger.yangyedplayer = null;
            }

            // Yinyanger reset the selected target if one of them gets killed
            if (Yinyanger.yinyanger != null && Yinyanger.yinyanger != __instance && (target == Yinyanger.yinyedplayer || target == Yinyanger.yangyedplayer)) {
                Yinyanger.yinyedplayer = null;
                Yinyanger.yangyedplayer = null;
            }

            // Devourer play sound when someone dies
            if (Devourer.devourer != null && Devourer.devourer == PlayerControl.LocalPlayer && !Devourer.devourer.Data.IsDead) {
                SoundManager.Instance.PlaySound(CustomMain.customAssets.devourerDingClip, false, 100f);
            }

            // Vigilant delete doorlog item when killed
            if (Vigilant.vigilantMira != null && target == Vigilant.vigilantMira) {
                GameObject vigilantdoorlog = GameObject.Find("VigilantDoorLog");
                if (vigilantdoorlog != null) {
                    vigilantdoorlog.SetActive(false);
                }
            }

            // Performer timer upon death
            if (Performer.performer != null && target == Performer.performer) {
                Performer.duration = CustomOptionHolder.performerDuration.getFloat();
                // stop current music and play performer music
                if (PlayerControl.LocalPlayer != Spiritualist.spiritualist && PlayerControl.LocalPlayer != TimeTraveler.timeTraveler) {
                    RPCProcedure.changeMusic(7);
                    SoundManager.Instance.PlaySound(CustomMain.customAssets.performerMusic, true, 5f);
                    new DIO(Performer.duration, Performer.performer);
                }
                Performer.musicStop = false;
            }

            // Hunter target suicide trigger on Hunter murder
            if (Hunter.hunter != null && target == Hunter.hunter) {
                if (Hunter.hunted != null && !Hunter.hunted.Data.IsDead) {
                    Hunter.hunted.MurderPlayer(Hunter.hunted);
                }
            }

            // Sleuth store body positions
            if (Sleuth.deadBodyPositions != null) Sleuth.deadBodyPositions.Add(target.transform.position);

            // Forensic add body
            if (Forensic.deadBodies != null) {
                Forensic.featureDeadBodies.Add(new Tuple<DeadPlayer, Vector3>(deadPlayer, target.transform.position));
            }

            // Capture the flag reset flag position if killed while having it
            if (CaptureTheFlag.redPlayerWhoHasBlueFlag != null && target == CaptureTheFlag.redPlayerWhoHasBlueFlag) {
                CaptureTheFlag.blueflagtaken = false;
                CaptureTheFlag.blueteamAlerted = false;
                CaptureTheFlag.redPlayerWhoHasBlueFlag = null;
                CaptureTheFlag.blueflag.transform.parent = CaptureTheFlag.blueflagbase.transform.parent;
                switch (PlayerControl.GameOptions.MapId) {
                    // Skeld
                    case 0:
                        if (activatedSensei) {
                            CaptureTheFlag.blueflag.transform.position = new Vector3(7.7f, -1.15f, 0.5f);
                        }
                        else {
                            CaptureTheFlag.blueflag.transform.position = new Vector3(16.5f, -4.65f, 0.5f);
                        }
                        break;
                    // MiraHQ
                    case 1:
                        CaptureTheFlag.blueflag.transform.position = new Vector3(23.25f, 5.05f, 0.5f);
                        break;
                    // Polus
                    case 2:
                        CaptureTheFlag.blueflag.transform.position = new Vector3(5.4f, -9.65f, 0.5f);
                        break;
                    // Dleks
                    case 3:
                        CaptureTheFlag.blueflag.transform.position = new Vector3(-16.5f, -4.65f, 0.5f);
                        break;
                    // Airship
                    case 4:
                        CaptureTheFlag.blueflag.transform.position = new Vector3(33.6f, 1.25f, 0.5f);
                        break;
                }
            }

            if (CaptureTheFlag.bluePlayerWhoHasRedFlag != null && target == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                CaptureTheFlag.redflagtaken = false;
                CaptureTheFlag.redteamAlerted = false;
                CaptureTheFlag.bluePlayerWhoHasRedFlag = null;
                CaptureTheFlag.redflag.transform.parent = CaptureTheFlag.redflagbase.transform.parent;
                switch (PlayerControl.GameOptions.MapId) {
                    // Skeld
                    case 0:
                        if (activatedSensei) {
                            CaptureTheFlag.redflag.transform.position = new Vector3(-17.5f, -1.35f, 0.5f);
                        }
                        else {
                            CaptureTheFlag.redflag.transform.position = new Vector3(-20.5f, -5.35f, 0.5f);
                        }
                        break;
                    // MiraHQ
                    case 1:
                        CaptureTheFlag.redflag.transform.position = new Vector3(2.525f, 10.55f, 0.5f);
                        break;
                    // Polus
                    case 2:
                        CaptureTheFlag.redflag.transform.position = new Vector3(36.4f, -21.7f, 0.5f);
                        break;
                    // Dlesk
                    case 3:
                        CaptureTheFlag.redflag.transform.position = new Vector3(20.5f, -5.35f, 0.5f);
                        break;
                    // Airship
                    case 4:
                        CaptureTheFlag.redflag.transform.position = new Vector3(-17.5f, -1.2f, 0.5f);
                        break;
                }
            }

            // Capture the flag revive player
            if (CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode) {
                foreach (PlayerControl player in CaptureTheFlag.redteamFlag) {
                    if (player.PlayerId == target.PlayerId) {
                        var body = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == target.PlayerId);
                        body.bodyRenderer.material.SetColor("_BodyColor", Palette.PlayerColors[0]);
                        body.bodyRenderer.material.SetColor("_BackColor", Palette.PlayerColors[0]);
                        HudManager.Instance.StartCoroutine(Effects.Lerp(CaptureTheFlag.reviveTime, new Action<float>((p) => {
                            if (p == 1f && player != null) {
                                player.Revive();
                                switch (PlayerControl.GameOptions.MapId) {
                                    // Skeld
                                    case 0:
                                        if (activatedSensei) {
                                            player.transform.position = new Vector3(-17.5f, -1.15f, player.transform.position.z);
                                        }
                                        else {
                                            player.transform.position = new Vector3(-20.5f, -5.15f, player.transform.position.z);
                                        }
                                        break;
                                    // MiraHQ
                                    case 1:
                                        player.transform.position = new Vector3(2.53f, 10.75f, player.transform.position.z);
                                        break;
                                    // Polus
                                    case 2:
                                        player.transform.position = new Vector3(36.4f, -21.5f, player.transform.position.z);
                                        break;
                                    // Dleks
                                    case 3:
                                        player.transform.position = new Vector3(20.5f, -5.15f, player.transform.position.z);
                                        break;
                                    // Airship
                                    case 4:
                                        player.transform.position = new Vector3(-17.5f, -1.1f, player.transform.position.z);
                                        break;
                                }
                                DeadPlayer deadPlayerEntry = deadPlayers.Where(x => x.player.PlayerId == target.PlayerId).FirstOrDefault();
                                if (body != null) UnityEngine.Object.Destroy(body.gameObject);
                                if (deadPlayerEntry != null) deadPlayers.Remove(deadPlayerEntry);
                            }

                        })));

                    }
                }
                foreach (PlayerControl player in CaptureTheFlag.blueteamFlag) {
                    if (player.PlayerId == target.PlayerId) {
                        var body = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == target.PlayerId);
                        body.bodyRenderer.material.SetColor("_BodyColor", Palette.PlayerColors[1]);
                        body.bodyRenderer.material.SetColor("_BackColor", Palette.PlayerColors[1]);
                        HudManager.Instance.StartCoroutine(Effects.Lerp(CaptureTheFlag.reviveTime, new Action<float>((p) => {
                            if (p == 1f && player != null) {
                                player.Revive();
                                switch (PlayerControl.GameOptions.MapId) {
                                    // Skeld
                                    case 0:
                                        if (activatedSensei) {
                                            player.transform.position = new Vector3(7.7f, -0.95f, player.transform.position.z);
                                        }
                                        else {
                                            player.transform.position = new Vector3(16.5f, -4.45f, player.transform.position.z);
                                        }
                                        break;
                                    // MiraHQ
                                    case 1:
                                        player.transform.position = new Vector3(23.25f, 5.25f, player.transform.position.z);
                                        break;
                                    // Polus
                                    case 2:
                                        player.transform.position = new Vector3(5.4f, -9.45f, player.transform.position.z);
                                        break;
                                    // Dleks
                                    case 3:
                                        player.transform.position = new Vector3(-16.5f, -4.45f, player.transform.position.z);
                                        break;
                                    // Airship
                                    case 4:
                                        player.transform.position = new Vector3(33.6f, 1.45f, player.transform.position.z);
                                        break;
                                }
                                DeadPlayer deadPlayerEntry = deadPlayers.Where(x => x.player.PlayerId == target.PlayerId).FirstOrDefault();
                                if (body != null) UnityEngine.Object.Destroy(body.gameObject);
                                if (deadPlayerEntry != null) deadPlayers.Remove(deadPlayerEntry);
                            }

                        })));

                    }
                }
            }


            // Police and Thief revive player
            if (PoliceAndThief.policeAndThiefMode && !CaptureTheFlag.captureTheFlagMode) {
                foreach (PlayerControl player in PoliceAndThief.policeTeam) {
                    if (player.PlayerId == target.PlayerId) {
                        var body = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == target.PlayerId);
                        body.bodyRenderer.material.SetColor("_BodyColor", Palette.PlayerColors[4]);
                        body.bodyRenderer.material.SetColor("_BackColor", Palette.PlayerColors[4]);
                        HudManager.Instance.StartCoroutine(Effects.Lerp(PoliceAndThief.reviveTime, new Action<float>((p) => {
                            if (p == 1f && player != null) {
                                player.Revive();
                                switch (PlayerControl.GameOptions.MapId) {
                                    // Skeld
                                    case 0:
                                        if (activatedSensei) {
                                            player.transform.position = new Vector3(-12f, 5f, player.transform.position.z);
                                        }
                                        else {
                                            player.transform.position = new Vector3(-10.2f, 1.18f, player.transform.position.z);
                                        }
                                        break;
                                    // MiraHQ
                                    case 1:
                                        player.transform.position = new Vector3(1.8f, -1f, player.transform.position.z);
                                        break;
                                    // Polus
                                    case 2:
                                        player.transform.position = new Vector3(8.18f, -7.4f, player.transform.position.z);
                                        break;
                                    // Dleks
                                    case 3:
                                        player.transform.position = new Vector3(10.2f, 1.18f, player.transform.position.z);
                                        break;
                                    // Airship
                                    case 4:
                                        player.transform.position = new Vector3(-18.5f, 0.75f, player.transform.position.z);
                                        break;
                                }
                                DeadPlayer deadPlayerEntry = deadPlayers.Where(x => x.player.PlayerId == target.PlayerId).FirstOrDefault();
                                if (body != null) UnityEngine.Object.Destroy(body.gameObject);
                                if (deadPlayerEntry != null) deadPlayers.Remove(deadPlayerEntry);
                            }

                        })));

                    }
                }
                foreach (PlayerControl player in PoliceAndThief.thiefTeam) {
                    if (player.PlayerId == target.PlayerId) {
                        var body = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == target.PlayerId);
                        body.bodyRenderer.material.SetColor("_BodyColor", Palette.PlayerColors[16]);
                        body.bodyRenderer.material.SetColor("_BackColor", Palette.PlayerColors[16]);
                        if (PoliceAndThief.thiefplayer01 != null && target.PlayerId == PoliceAndThief.thiefplayer01.PlayerId) {
                            if (PoliceAndThief.thiefplayer01IsStealing) {
                                RPCProcedure.policeandThiefRevertedJewelPosition(target.PlayerId, PoliceAndThief.thiefplayer01JewelId);
                            }
                        }
                        else if (PoliceAndThief.thiefplayer02 != null && target.PlayerId == PoliceAndThief.thiefplayer02.PlayerId) {
                            if (PoliceAndThief.thiefplayer02IsStealing) {
                                RPCProcedure.policeandThiefRevertedJewelPosition(target.PlayerId, PoliceAndThief.thiefplayer02JewelId);
                            }
                        }
                        else if (PoliceAndThief.thiefplayer03 != null && target.PlayerId == PoliceAndThief.thiefplayer03.PlayerId) {
                            if (PoliceAndThief.thiefplayer03IsStealing) {
                                RPCProcedure.policeandThiefRevertedJewelPosition(target.PlayerId, PoliceAndThief.thiefplayer03JewelId);
                            }
                        }
                        else if (PoliceAndThief.thiefplayer04 != null && target.PlayerId == PoliceAndThief.thiefplayer04.PlayerId) {
                            if (PoliceAndThief.thiefplayer04IsStealing) {
                                RPCProcedure.policeandThiefRevertedJewelPosition(target.PlayerId, PoliceAndThief.thiefplayer04JewelId);
                            }
                        }
                        else if (PoliceAndThief.thiefplayer05 != null && target.PlayerId == PoliceAndThief.thiefplayer05.PlayerId) {
                            if (PoliceAndThief.thiefplayer05IsStealing) {
                                RPCProcedure.policeandThiefRevertedJewelPosition(target.PlayerId, PoliceAndThief.thiefplayer05JewelId);
                            }
                        }
                        else if (PoliceAndThief.thiefplayer06 != null && target.PlayerId == PoliceAndThief.thiefplayer06.PlayerId) {
                            if (PoliceAndThief.thiefplayer06IsStealing) {
                                RPCProcedure.policeandThiefRevertedJewelPosition(target.PlayerId, PoliceAndThief.thiefplayer06JewelId);
                            }
                        }
                        else if (PoliceAndThief.thiefplayer07 != null && target.PlayerId == PoliceAndThief.thiefplayer07.PlayerId) {
                            if (PoliceAndThief.thiefplayer07IsStealing) {
                                RPCProcedure.policeandThiefRevertedJewelPosition(target.PlayerId, PoliceAndThief.thiefplayer07JewelId);
                            }
                        }
                        else if (PoliceAndThief.thiefplayer08 != null && target.PlayerId == PoliceAndThief.thiefplayer08.PlayerId) {
                            if (PoliceAndThief.thiefplayer08IsStealing) {
                                RPCProcedure.policeandThiefRevertedJewelPosition(target.PlayerId, PoliceAndThief.thiefplayer08JewelId);
                            }
                        }
                        else if (PoliceAndThief.thiefplayer09 != null && target.PlayerId == PoliceAndThief.thiefplayer09.PlayerId) {
                            if (PoliceAndThief.thiefplayer09IsStealing) {
                                RPCProcedure.policeandThiefRevertedJewelPosition(target.PlayerId, PoliceAndThief.thiefplayer09JewelId);
                            }
                        }
                        else if (PoliceAndThief.thiefplayer10 != null && target.PlayerId == PoliceAndThief.thiefplayer10.PlayerId) {
                            if (PoliceAndThief.thiefplayer10IsStealing) {
                                RPCProcedure.policeandThiefRevertedJewelPosition(target.PlayerId, PoliceAndThief.thiefplayer10JewelId);
                            }
                        }
                        HudManager.Instance.StartCoroutine(Effects.Lerp(PoliceAndThief.reviveTime * 1.5f, new Action<float>((p) => {
                            if (p == 1f && player != null) {
                                player.Revive();
                                switch (PlayerControl.GameOptions.MapId) {
                                    // Skeld
                                    case 0:
                                        if (activatedSensei) {
                                            player.transform.position = new Vector3(13.75f, -0.2f, player.transform.position.z);
                                        }
                                        else {
                                            player.transform.position = new Vector3(-1.31f, -16.25f, player.transform.position.z);
                                        }
                                        break;
                                    // MiraHQ
                                    case 1:
                                        player.transform.position = new Vector3(17.75f, 11.5f, player.transform.position.z);
                                        break;
                                    // Polus
                                    case 2:
                                        player.transform.position = new Vector3(30f, -15.75f, player.transform.position.z);
                                        break;
                                    // Dleks
                                    case 3:
                                        player.transform.position = new Vector3(1.31f, -16.25f, player.transform.position.z);
                                        break;
                                    // Airship
                                    case 4:
                                        player.transform.position = new Vector3(7.15f, -14.5f, player.transform.position.z);
                                        break;
                                }
                                DeadPlayer deadPlayerEntry = deadPlayers.Where(x => x.player.PlayerId == target.PlayerId).FirstOrDefault();
                                if (body != null) UnityEngine.Object.Destroy(body.gameObject);
                                if (deadPlayerEntry != null) deadPlayers.Remove(deadPlayerEntry);
                            }

                        })));

                    }
                }
            }

            // Check alive players for disable sabotage button if game result in 1vs1 special condition (impostor + rebel / impostor + captain / rebel + captain)
            alivePlayers = 0;
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (!player.Data.IsDead) {
                    alivePlayers += 1;
                }
            }

        }
    }

    [HarmonyPatch(typeof(KillAnimation), nameof(KillAnimation.CoPerformKill))]
    class KillAnimationCoPerformKillPatch {
        public static bool hideNextAnimation = true;
        public static void Prefix(KillAnimation __instance, [HarmonyArgument(0)]ref PlayerControl source, [HarmonyArgument(1)]ref PlayerControl target) {
            if (hideNextAnimation)
                source = target;
            hideNextAnimation = false;
        }
    }

    [HarmonyPatch(typeof(KillAnimation), nameof(KillAnimation.SetMovement))]
    class KillAnimationSetMovementPatch {
        private static int? colorId = null;
        public static void Prefix(PlayerControl source, bool canMove) {
            Color color = source.myRend.material.GetColor("_BodyColor");
            if (color != null && Mimic.mimic != null && source.Data.PlayerId == Mimic.mimic.PlayerId) {
                var index = Palette.PlayerColors.IndexOf(color);
                if (index != -1) colorId = index;
            }
        }

        public static void Postfix(PlayerControl source, bool canMove) {
            if (colorId.HasValue) source.RawSetColor(colorId.Value);
            colorId = null;
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.Exiled))]
    public static class ExilePlayerPatch
    {
        public static void Prefix(PlayerControl __instance) {
            // Kid exile lose condition
            if (Kid.kid != null && Kid.kid == __instance) {
                Kid.triggerKidLose = true;
                ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.KidLose, false);
            }
            // Joker win condition
            else if (Joker.joker != null && Joker.joker == __instance) {
                Joker.triggerJokerWin = true;
            }
        }

        public static void Postfix(PlayerControl __instance) {
            // Collect dead player info
            DeadPlayer deadPlayer = new DeadPlayer(__instance, DateTime.UtcNow, DeathReason.Exile, null);
            GameHistory.deadPlayers.Add(deadPlayer);

            // Remove fake tasks when player dies
            if (__instance.hasFakeTasks())
                __instance.clearAllTasks();

            // Lover suicide trigger on exile
            if ((Modifiers.lover1 != null && __instance == Modifiers.lover1) || (Modifiers.lover2 != null && __instance == Modifiers.lover2)) {
                PlayerControl otherLover = __instance == Modifiers.lover1 ? Modifiers.lover2 : Modifiers.lover1;
                if (otherLover != null && !otherLover.Data.IsDead)
                    otherLover.Exiled();
            }

            // Check alive players for disable sabotage button if game result in 1vs1 special condition (impostor + rebel / impostor + captain / rebel + captain)
            alivePlayers = 0;
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (!player.Data.IsDead) {
                    alivePlayers += 1;
                }
            }
        }
    }
}
