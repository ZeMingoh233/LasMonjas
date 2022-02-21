using HarmonyLib;
using Hazel;
using static LasMonjas.LasMonjas;
using static LasMonjas.HudManagerStartPatch;
using static LasMonjas.GameHistory;
using static LasMonjas.MapOptions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Reactor;
using LasMonjas.Objects;
using LasMonjas.Patches;
using static LasMonjas.RoleInfo;
using LasMonjas.Core;

namespace LasMonjas
{
    enum RoleId
    {
        Mimic,
        Painter,
        Demon,
        Janitor,
        Ilusionist,
        Manipulator,
        Bomberman,
        Chameleon,
        Gambler,
        Sorcerer,
        Renegade,
        Minion,
        BountyHunter,
        Trapper,
        Yinyanger,
        Challenger,
        Joker,
        RoleThief,
        Pyromaniac,
        TreasureHunter,
        Devourer,
        Captain,
        Mechanic,
        Sheriff,
        Detective,
        Forensic,
        TimeTraveler,
        Squire,
        Cheater,
        FortuneTeller,
        Hacker,
        Sleuth,
        Fink,
        Kid,
        Welder,
        Spiritualist,
        TheChosenOne,
        Vigilant,
        VigilantMira,
        Performer,
        Hunter,
        Jinx,
        Lover,
        Lighter,
        Blind,
        Flash,
        BigChungus,
        Crewmate,
        Impostor,

        // Capture the flag
        RedPlayer01,
        RedPlayer02,
        RedPlayer03,
        RedPlayer04,
        RedPlayer05,
        RedPlayer06,
        RedPlayer07,
        BluePlayer01,
        BluePlayer02,
        BluePlayer03,
        BluePlayer04,
        BluePlayer05,
        BluePlayer06,
        BluePlayer07,
        BluePlayer08,

        // Police and Thief
        PolicePlayer01,
        PolicePlayer02,
        PolicePlayer03,
        PolicePlayer04,
        PolicePlayer05,
        ThiefPlayer01,
        ThiefPlayer02,
        ThiefPlayer03,
        ThiefPlayer04,
        ThiefPlayer05,
        ThiefPlayer06,
        ThiefPlayer07,
        ThiefPlayer08,
        ThiefPlayer09,
        ThiefPlayer10
    }

    enum CustomRPC
    {
        // Main Controls

        ResetVaribles = 60,
        ShareOptions,
        ForceEnd,
        SetRole,
        UseUncheckedVent,
        UncheckedMurderPlayer,
        UncheckedCmdReportDeadBody,
        UncheckedExilePlayer,

        // Role functionality

        MimicTransform = 101,
        PainterPaint,
        DemonSetBitten,
        PlaceNun,
        RemoveBody,
        DragPlaceBody,
        PlaceHat,
        LightsOut,
        ManipulatorKill,
        PlaceBomb,
        FixBomb,
        BombermanWin,
        ChameleonInvisible,
        GamblerShoot,
        SetSpelledPlayer,

        RenegadeRecruitMinion,
        BountyHunterSetKill,
        BountyHunterKill,
        PlaceMine,
        PlaceTrap,
        MineKill,
        ActivateTrap,
        YinyangerSetYinyang,
        ChallengerSetRival,
        ChallengerPerformDuel,
        ChallengerSelectAttack,
        ChallengerCantDuel,

        RoleThiefSteal,
        PyromaniacWin,
        PlaceTreasure,
        CollectedTreasure,
        DevourBody,

        MechanicFixLights,
        MechanicUsedRepair,
        SheriffKill,
        TimeTravelerShield,
        TimeTravelerRewindTime,
        TimeTravelerRevive,
        SquireSetShielded,
        ShieldedMurderAttempt,
        CheaterCheat,
        FortuneTellerReveal,
        HackerAbilityUses,
        SleuthUsedLocate,
        SealVent,
        SpiritualistRevive,
        SendSpiritualistIsReviving,
        MurderSpiritualistIfReportWhileReviving,
        ResetSpiritualistReviveValues,
        PlaceCamera,
        VigilantAbilityUses,
        PerformerIsReported,
        HunterUsedHunted,
        SetJinxed,

        ChangeMusic,

        // Capture the flag
        CapturetheFlagKills,
        CaptureTheFlagWhoTookTheFlag,
        CaptureTheFlagWhichTeamScored,

        // Police and Thief
        PoliceandThiefKills,
        PoliceandThiefJail,
        PoliceandThiefFreeThief,
        PoliceandThiefTakeJewel,
        PoliceandThiefDeliverJewel,
        PoliceandThiefRevertedJewelPosition
    }

    public static class RPCProcedure
    {

        // Main Controls

        public static void resetVariables() {
            Nun.clearNuns();
            Hats.clearHats();
            Mine.clearMines();
            Trap.clearTraps();
            clearAndReloadMapOptions();
            clearAndReloadRoles();
            clearGameHistory();
            setCustomButtonCooldowns();
        }

        public static void ShareOptions(int numberOfOptions, MessageReader reader) {
            try {
                for (int i = 0; i < numberOfOptions; i++) {
                    uint optionId = reader.ReadPackedUInt32();
                    uint selection = reader.ReadPackedUInt32();
                    CustomOption option = CustomOption.options.FirstOrDefault(option => option.id == (int)optionId);
                    option.updateSelection((int)selection);
                }
            }
            catch (Exception e) {
                LasMonjasPlugin.Logger.LogError("Error while deserializing options: " + e.Message);
            }
        }

        public static void forceEnd() {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (!player.Data.Role.IsImpostor) {
                    player.RemoveInfected();
                    player.MurderPlayer(player);
                    player.Data.IsDead = true;
                }
            }
        }

        public static void setRole(byte roleId, byte playerId, byte flag) {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                if (player.PlayerId == playerId) {
                    switch ((RoleId)roleId) {
                        case RoleId.Mimic:
                            Mimic.mimic = player;
                            break;
                        case RoleId.Painter:
                            Painter.painter = player;
                            break;
                        case RoleId.Demon:
                            Demon.demon = player;
                            break;
                        case RoleId.Janitor:
                            Janitor.janitor = player;
                            break;
                        case RoleId.Ilusionist:
                            Ilusionist.ilusionist = player;
                            break;
                        case RoleId.Manipulator:
                            Manipulator.manipulator = player;
                            break;
                        case RoleId.Bomberman:
                            Bomberman.bomberman = player;
                            break;
                        case RoleId.Chameleon:
                            Chameleon.chameleon = player;
                            break;
                        case RoleId.Gambler:
                            Gambler.gambler = player;
                            break;
                        case RoleId.Sorcerer:
                            Sorcerer.sorcerer = player;
                            break;
                        case RoleId.Renegade:
                            Renegade.renegade = player;
                            break;
                        case RoleId.Minion:
                            Minion.minion = player;
                            break;
                        case RoleId.BountyHunter:
                            BountyHunter.bountyhunter = player;
                            break;
                        case RoleId.Trapper:
                            Trapper.trapper = player;
                            break;
                        case RoleId.Yinyanger:
                            Yinyanger.yinyanger = player;
                            break;
                        case RoleId.Challenger:
                            Challenger.challenger = player;
                            break;
                        case RoleId.Joker:
                            Joker.joker = player;
                            break;
                        case RoleId.RoleThief:
                            RoleThief.rolethief = player;
                            break;
                        case RoleId.Pyromaniac:
                            Pyromaniac.pyromaniac = player;
                            break;
                        case RoleId.TreasureHunter:
                            TreasureHunter.treasureHunter = player;
                            break;
                        case RoleId.Devourer:
                            Devourer.devourer = player;
                            break;
                        case RoleId.Captain:
                            Captain.captain = player;
                            break;
                        case RoleId.Mechanic:
                            Mechanic.mechanic = player;
                            break;
                        case RoleId.Sheriff:
                            Sheriff.sheriff = player;
                            break;
                        case RoleId.Detective:
                            Detective.detective = player;
                            break;
                        case RoleId.Forensic:
                            Forensic.forensic = player;
                            break;
                        case RoleId.TimeTraveler:
                            TimeTraveler.timeTraveler = player;
                            break;
                        case RoleId.Squire:
                            Squire.squire = player;
                            break;
                        case RoleId.Cheater:
                            Cheater.cheater = player;
                            break;
                        case RoleId.FortuneTeller:
                            FortuneTeller.fortuneTeller = player;
                            break;
                        case RoleId.Hacker:
                            Hacker.hacker = player;
                            break;
                        case RoleId.Sleuth:
                            Sleuth.sleuth = player;
                            break;
                        case RoleId.Fink:
                            Fink.fink = player;
                            break;
                        case RoleId.Kid:
                            Kid.kid = player;
                            break;
                        case RoleId.Welder:
                            Welder.welder = player;
                            break;
                        case RoleId.Spiritualist:
                            Spiritualist.spiritualist = player;
                            break;
                        case RoleId.TheChosenOne:
                            TheChosenOne.theChosenOne = player;
                            break;
                        case RoleId.Vigilant:
                            Vigilant.vigilant = player;
                            break;
                        case RoleId.VigilantMira:
                            Vigilant.vigilantMira = player;
                            break;
                        case RoleId.Performer:
                            Performer.performer = player;
                            break;
                        case RoleId.Hunter:
                            Hunter.hunter = player;
                            break;
                        case RoleId.Jinx:
                            Jinx.jinx = player;
                            break;

                        // Modifiers
                        case RoleId.Lover:
                            if (flag == 0) Modifiers.lover1 = player;
                            else Modifiers.lover2 = player;
                            break;
                        case RoleId.Lighter:
                            Modifiers.lighter = player;
                            break;
                        case RoleId.Blind:
                            Modifiers.blind = player;
                            break;
                        case RoleId.Flash:
                            Modifiers.flash = player;
                            break;
                        case RoleId.BigChungus:
                            Modifiers.bigchungus = player;
                            break;

                        // Capture the Flag
                        case RoleId.RedPlayer01:
                            CaptureTheFlag.redplayer01 = player;
                            CaptureTheFlag.redteamFlag.Add(player);
                            break;
                        case RoleId.RedPlayer02:
                            CaptureTheFlag.redplayer02 = player;
                            CaptureTheFlag.redteamFlag.Add(player);
                            break;
                        case RoleId.RedPlayer03:
                            CaptureTheFlag.redplayer03 = player;
                            CaptureTheFlag.redteamFlag.Add(player);
                            break;
                        case RoleId.RedPlayer04:
                            CaptureTheFlag.redplayer04 = player;
                            CaptureTheFlag.redteamFlag.Add(player);
                            break;
                        case RoleId.RedPlayer05:
                            CaptureTheFlag.redplayer05 = player;
                            CaptureTheFlag.redteamFlag.Add(player);
                            break;
                        case RoleId.RedPlayer06:
                            CaptureTheFlag.redplayer06 = player;
                            CaptureTheFlag.redteamFlag.Add(player);
                            break;
                        case RoleId.RedPlayer07:
                            CaptureTheFlag.redplayer07 = player;
                            CaptureTheFlag.redteamFlag.Add(player);
                            break;
                        case RoleId.BluePlayer01:
                            CaptureTheFlag.blueplayer01 = player;
                            CaptureTheFlag.blueteamFlag.Add(player);
                            break;
                        case RoleId.BluePlayer02:
                            CaptureTheFlag.blueplayer02 = player;
                            CaptureTheFlag.blueteamFlag.Add(player);
                            break;
                        case RoleId.BluePlayer03:
                            CaptureTheFlag.blueplayer03 = player;
                            CaptureTheFlag.blueteamFlag.Add(player);
                            break;
                        case RoleId.BluePlayer04:
                            CaptureTheFlag.blueplayer04 = player;
                            CaptureTheFlag.blueteamFlag.Add(player);
                            break;
                        case RoleId.BluePlayer05:
                            CaptureTheFlag.blueplayer05 = player;
                            CaptureTheFlag.blueteamFlag.Add(player);
                            break;
                        case RoleId.BluePlayer06:
                            CaptureTheFlag.blueplayer06 = player;
                            CaptureTheFlag.blueteamFlag.Add(player);
                            break;
                        case RoleId.BluePlayer07:
                            CaptureTheFlag.blueplayer07 = player;
                            CaptureTheFlag.blueteamFlag.Add(player);
                            break;
                        case RoleId.BluePlayer08:
                            CaptureTheFlag.blueplayer08 = player;
                            CaptureTheFlag.blueteamFlag.Add(player);
                            break;

                        // Police and Thief
                        case RoleId.PolicePlayer01:
                            PoliceAndThief.policeplayer01 = player;
                            PoliceAndThief.policeTeam.Add(player);
                            break;
                        case RoleId.PolicePlayer02:
                            PoliceAndThief.policeplayer02 = player;
                            PoliceAndThief.policeTeam.Add(player);
                            break;
                        case RoleId.PolicePlayer03:
                            PoliceAndThief.policeplayer03 = player;
                            PoliceAndThief.policeTeam.Add(player);
                            break;
                        case RoleId.PolicePlayer04:
                            PoliceAndThief.policeplayer04 = player;
                            PoliceAndThief.policeTeam.Add(player);
                            break;
                        case RoleId.PolicePlayer05:
                            PoliceAndThief.policeplayer05 = player;
                            PoliceAndThief.policeTeam.Add(player);
                            break;
                        case RoleId.ThiefPlayer01:
                            PoliceAndThief.thiefplayer01 = player;
                            PoliceAndThief.thiefTeam.Add(player);
                            break;
                        case RoleId.ThiefPlayer02:
                            PoliceAndThief.thiefplayer02 = player;
                            PoliceAndThief.thiefTeam.Add(player);
                            break;
                        case RoleId.ThiefPlayer03:
                            PoliceAndThief.thiefplayer03 = player;
                            PoliceAndThief.thiefTeam.Add(player);
                            break;
                        case RoleId.ThiefPlayer04:
                            PoliceAndThief.thiefplayer04 = player;
                            PoliceAndThief.thiefTeam.Add(player);
                            break;
                        case RoleId.ThiefPlayer05:
                            PoliceAndThief.thiefplayer05 = player;
                            PoliceAndThief.thiefTeam.Add(player);
                            break;
                        case RoleId.ThiefPlayer06:
                            PoliceAndThief.thiefplayer06 = player;
                            PoliceAndThief.thiefTeam.Add(player);
                            break;
                        case RoleId.ThiefPlayer07:
                            PoliceAndThief.thiefplayer07 = player;
                            PoliceAndThief.thiefTeam.Add(player);
                            break;
                        case RoleId.ThiefPlayer08:
                            PoliceAndThief.thiefplayer08 = player;
                            PoliceAndThief.thiefTeam.Add(player);
                            break;
                        case RoleId.ThiefPlayer09:
                            PoliceAndThief.thiefplayer09 = player;
                            PoliceAndThief.thiefTeam.Add(player);
                            break;
                        case RoleId.ThiefPlayer10:
                            PoliceAndThief.thiefplayer10 = player;
                            PoliceAndThief.thiefTeam.Add(player);
                            break;
                    }
                }
        }

        public static void useUncheckedVent(int ventId, byte playerId, byte isEnter) {
            PlayerControl player = Helpers.playerById(playerId);
            if (player == null) return;
            // Fill dummy MessageReader and call MyPhysics.HandleRpc as the corountines cannot be accessed
            MessageReader reader = new MessageReader();
            byte[] bytes = BitConverter.GetBytes(ventId);
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            reader.Buffer = bytes;
            reader.Length = bytes.Length;

            Hats.startAnimation(ventId);
            player.MyPhysics.HandleRpc(isEnter != 0 ? (byte)19 : (byte)20, reader);
        }

        public static void uncheckedMurderPlayer(byte sourceId, byte targetId, byte showAnimation) {
            PlayerControl source = Helpers.playerById(sourceId);
            PlayerControl target = Helpers.playerById(targetId);
            if (source != null && target != null) {
                if (showAnimation == 0) KillAnimationCoPerformKillPatch.hideNextAnimation = true;
                source.MurderPlayer(target);
            }
        }

        public static void uncheckedCmdReportDeadBody(byte sourceId, byte targetId) {
            PlayerControl source = Helpers.playerById(sourceId);
            PlayerControl target = Helpers.playerById(targetId);
            if (source != null && target != null) source.ReportDeadBody(target.Data);
        }

        public static void uncheckedExilePlayer(byte targetId) {
            PlayerControl target = Helpers.playerById(targetId);
            if (target != null) target.Exiled();
        }


        // Role functionality

        public static void mimicTransform(byte playerId) {
            PlayerControl target = Helpers.playerById(playerId);
            if (Mimic.mimic == null || target == null) return;

            Mimic.transformTimer = Mimic.duration;
            Mimic.transformTarget = target;
            if (Painter.painterTimer <= 0f)
                Mimic.mimic.setLook(target.Data.PlayerName, target.Data.DefaultOutfit.ColorId, target.Data.DefaultOutfit.HatId, target.Data.DefaultOutfit.VisorId, target.Data.DefaultOutfit.SkinId, target.Data.DefaultOutfit.PetId);
        }

        public static void painterPaint(int colorId) {
            if (Painter.painter == null) return;

            Painter.painterTimer = Painter.duration;
            Detective.footprintcolor = colorId;
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                player.setLook("", colorId, "", "", "", "");
        }

        public static void demonSetBitten(byte targetId, byte performReset) {
            if (performReset != 0) {
                Demon.bitten = null;
                return;
            }

            if (Demon.demon == null) return;
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (player.PlayerId == targetId && !player.Data.IsDead) {
                    Demon.bitten = player;
                }
            }
        }

        public static void placeNun(byte[] buff) {
            Vector3 position = Vector3.zero;
            position.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1 * sizeof(float));
            new Nun(position);
        }

        public static void removeBody(byte playerId) {
            DeadBody[] array = UnityEngine.Object.FindObjectsOfType<DeadBody>();
            for (int i = 0; i < array.Length; i++) {
                if (GameData.Instance.GetPlayerById(array[i].ParentId).PlayerId == playerId) {
                    UnityEngine.Object.Destroy(array[i].gameObject);
                }
            }
            if (Performer.performer != null && playerId == Performer.performer.PlayerId)
                performerIsReported(0);
        }

        public static void dragPlaceBody(byte playerId) {
            DeadBody[] array = UnityEngine.Object.FindObjectsOfType<DeadBody>();
            for (int i = 0; i < array.Length; i++) {
                if (GameData.Instance.GetPlayerById(array[i].ParentId).PlayerId == playerId) {
                    if (!Janitor.dragginBody) {
                        Janitor.dragginBody = true;
                        Janitor.bodyId = playerId;
                    }
                    else {
                        Janitor.dragginBody = false;
                        Janitor.bodyId = 0;                    
                        var currentPosition = Janitor.janitor.GetTruePosition();
                        var velocity = Janitor.janitor.gameObject.GetComponent<Rigidbody2D>().velocity.normalized;
                        var newPos = ((Vector2)Janitor.janitor.GetTruePosition()) - (velocity / 3) + new Vector2(0.15f, 0.25f) + array[i].myCollider.offset;
                        if (!PhysicsHelpers.AnythingBetween(
                            currentPosition,
                            newPos,
                            Constants.ShipAndObjectsMask,
                            false
                        )) array[i].transform.position = newPos;
                    }
                }
            }
        }

        public static void janitorResetValues() {
            // Restore janitor values when rewind time
            if (Janitor.janitor != null && Janitor.dragginBody) {
                Janitor.dragginBody = false;
                Janitor.bodyId = 0;
            }
        }

        public static void placeHat(byte[] buff) {
            Vector3 position = Vector3.zero;
            position.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1 * sizeof(float));
            new Hats(position);
        }

        public static void lightsOut() {
            if (PlayerControl.LocalPlayer.Data.Role.IsImpostor && MapBehaviour.Instance || PlayerControl.LocalPlayer == Joker.joker && MapBehaviour.Instance) {
                MapBehaviour.Instance.Close();
            }
            Ilusionist.lightsOutTimer = Ilusionist.lightsOutDuration;
            // If the local player is impostor indicate lights out
            if (PlayerControl.LocalPlayer.Data.Role.IsImpostor && PlayerControl.LocalPlayer != Ilusionist.ilusionist) {
                new CustomMessage("The Ilusionist turned off the lights: ", Ilusionist.lightsOutDuration, -1, -1.3f, 9);
            }
        }


        public static void manipulatorKill(byte targetId) {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (player.PlayerId == targetId) {
                    Manipulator.manipulator.MurderPlayer(player);
                    return;
                }
            }
        }

        public static void placeBomb() {
            if (PlayerControl.LocalPlayer.Data.Role.IsImpostor && MapBehaviour.Instance || PlayerControl.LocalPlayer == Joker.joker && MapBehaviour.Instance) {
                MapBehaviour.Instance.Close();
            }
            Bomberman.activeBomb = true;
            Bomberman.currentBombNumber += 1;
            switch (PlayerControl.GameOptions.MapId) {
                case 0:
                    Bomberman.bombDuration = 60;
                    break;
                case 1:
                    Bomberman.bombDuration = 60;
                    break;
                case 2:
                    Bomberman.bombDuration = 90;
                    break;
                case 3:
                    Bomberman.bombDuration = 60;
                    break;
                case 4:
                    Bomberman.bombDuration = 180;
                    break;
            }
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (player.Data.IsDead && !player.Data.Role.IsImpostor) {
                    Bomberman.bombDuration += 5;
                }
            }
            Bomberman.bombTimer = Bomberman.bombDuration;
            new Bomb(Bomberman.bombDuration, Bomberman.bomberman, Bomberman.currentBombNumber);

            // Music Stop and play bomb music
            changeMusic(7);
            SoundManager.Instance.PlaySound(CustomMain.customAssets.bombermanBombMusic, true, 75f);
            SoundManager.Instance.StopSound(CustomMain.customAssets.performerMusic);
            // Indicate bomb text
            new CustomMessage("There's a Bomb on the map: ", Bomberman.bombTimer, Bomberman.currentBombNumber, -1.3f, 1);
        }

        public static void fixBomb() {
            GameObject bomb = GameObject.Find("Bomb");
            if (bomb != null) {
                bomb.name = "DefusedBomb";
                Bomberman.activeBomb = false;
                bomb.SetActive(false);
                resetBomberBombButton();

                //Music after fix bomb
                changeMusic(2);
            }
        }
        public static void bombermanWin() {
            SoundManager.Instance.PlaySound(CustomMain.customAssets.bombermanBombClip, false, 100f);
            Bomberman.triggerBombExploded = true;
        }

        public static void chameleonInvisible() {
            if (Chameleon.chameleon == null) return;

            Chameleon.chameleonTimer = Chameleon.duration;
        }

        public static void gamblerShoot(byte playerId) {
            PlayerControl target = Helpers.playerById(playerId);
            if (target == null) return;
            target.Exiled();
            PlayerControl partner = target.getPartner(); // Lover check
            byte partnerId = partner != null ? partner.PlayerId : playerId;
            Gambler.numberOfShots = Mathf.Max(0, Gambler.numberOfShots - 1);
            if (Constants.ShouldPlaySfx()) SoundManager.Instance.PlaySound(target.KillSfx, false, 0.8f);
            if (MeetingHud.Instance) {
                foreach (PlayerVoteArea pva in MeetingHud.Instance.playerStates) {
                    if (pva.TargetPlayerId == playerId || pva.TargetPlayerId == partnerId) {
                        pva.SetDead(pva.DidReport, true);
                        pva.Overlay.gameObject.SetActive(true);
                    }

                    //Give players back their vote if target is shot dead
                    if (pva.VotedFor != playerId || pva.VotedFor != partnerId) continue;
                    pva.UnsetVote();
                    var voteAreaPlayer = Helpers.playerById(pva.TargetPlayerId);
                    if (!voteAreaPlayer.AmOwner) continue;
                    MeetingHud.Instance.ClearVote();
                }
                if (AmongUsClient.Instance.AmHost)
                    MeetingHud.Instance.CheckForEndVoting();
            }
            if (HudManager.Instance != null && Gambler.gambler != null)
                if (PlayerControl.LocalPlayer == target)
                    HudManager.Instance.KillOverlay.ShowKillAnimation(Gambler.gambler.Data, target.Data);
                else if (partner != null && PlayerControl.LocalPlayer == partner)
                    HudManager.Instance.KillOverlay.ShowKillAnimation(partner.Data, partner.Data);
        }

        public static void setSpelledPlayer(byte playerId) {
            PlayerControl player = Helpers.playerById(playerId);
            if (Sorcerer.spelledPlayers == null)
                Sorcerer.spelledPlayers = new List<PlayerControl>();
            if (player != null) {
                Sorcerer.spelledPlayers.Add(player);
            }
        }

        public static void renegadeRecruitMinion(byte targetId) {
            PlayerControl player = Helpers.playerById(targetId);
            if (player == null) return;

            Renegade.usedRecruit = true;

            if (player == Chameleon.chameleon) {
                Chameleon.resetChameleon();
            }

            DestroyableSingleton<RoleManager>.Instance.SetRole(player, RoleTypes.Crewmate);
            erasePlayerRoles(player.PlayerId, true);
            Minion.minion = player;
            if (player.PlayerId == PlayerControl.LocalPlayer.PlayerId) PlayerControl.LocalPlayer.moveable = true;

            // Sound for both renegade and minion
            if (PlayerControl.LocalPlayer == Minion.minion || PlayerControl.LocalPlayer == Renegade.renegade) {
                SoundManager.Instance.PlaySound(CustomMain.customAssets.renegadeRecruitMinionClip, false, 100f);
            }

            // Green screen notification for the minion and renegade
            if (PlayerControl.LocalPlayer == Minion.minion || PlayerControl.LocalPlayer == Renegade.renegade) {
                HudManager.Instance.FullScreen.enabled = true;
                HudManager.Instance.StartCoroutine(Effects.Lerp(0.5f, new Action<float>((p) => {
                    var renderer = HudManager.Instance.FullScreen;
                    Color c = new Color(0f / 255f, 255f / 255f, 157f / 255f, 0f);
                    if (p < 0.5) {
                        if (renderer != null)
                            renderer.color = new Color(c.r, c.g, c.b, Mathf.Clamp01(p * 2 * 0.75f));
                    }
                    else {
                        if (renderer != null)
                            renderer.color = new Color(c.r, c.g, c.b, Mathf.Clamp01((1 - p) * 2 * 0.75f));
                    }
                })));
            }
            Renegade.canRecruitMinion = false;
            return;
        }

        public static void erasePlayerRoles(byte playerId, bool ignoreLovers = false) {
            PlayerControl player = Helpers.playerById(playerId);
            if (player == null) return;

            // Crewmate roles
            if (player == Captain.captain) Captain.clearAndReload();
            if (player == Mechanic.mechanic) Mechanic.clearAndReload();
            if (player == Sheriff.sheriff) Sheriff.clearAndReload();
            if (player == Detective.detective) Detective.clearAndReload();
            if (player == Forensic.forensic) Forensic.clearAndReload();
            if (player == TimeTraveler.timeTraveler) TimeTraveler.clearAndReload();
            if (player == Squire.squire) Squire.clearAndReload();
            if (player == Cheater.cheater) Cheater.clearAndReload();
            if (player == FortuneTeller.fortuneTeller) FortuneTeller.clearAndReload();
            if (player == Hacker.hacker) Hacker.clearAndReload();
            if (player == Sleuth.sleuth) Sleuth.clearAndReload();
            if (player == Fink.fink) Fink.clearAndReload();
            if (player == Kid.kid) Kid.clearAndReload();
            if (player == Welder.welder) Welder.clearAndReload();
            if (player == Spiritualist.spiritualist) Spiritualist.clearAndReload();
            if (player == TheChosenOne.theChosenOne) TheChosenOne.clearAndReload();
            if (player == Vigilant.vigilant) Vigilant.clearAndReload();
            if (player == Vigilant.vigilantMira) Vigilant.clearAndReload();
            if (player == Performer.performer) Performer.clearAndReload();
            if (player == Hunter.hunter) Hunter.clearAndReload();
            if (player == Jinx.jinx) Jinx.clearAndReload();

            // Impostor roles
            if (player == Mimic.mimic) Mimic.clearAndReload();
            if (player == Painter.painter) Painter.clearAndReload();
            if (player == Demon.demon) Demon.clearAndReload();
            if (player == Janitor.janitor) Janitor.clearAndReload();
            if (player == Ilusionist.ilusionist) Ilusionist.clearAndReload();
            if (player == Manipulator.manipulator) Manipulator.clearAndReload();
            if (player == Bomberman.bomberman) Bomberman.clearAndReload();
            if (player == Chameleon.chameleon) Chameleon.clearAndReload();
            if (player == Gambler.gambler) Gambler.clearAndReload();
            if (player == Sorcerer.sorcerer) Sorcerer.clearAndReload();

            // Neutral roles
            if (player == Joker.joker) Joker.clearAndReload();
            if (player == RoleThief.rolethief) RoleThief.clearAndReload();
            if (player == Pyromaniac.pyromaniac) Pyromaniac.clearAndReload();
            if (player == TreasureHunter.treasureHunter) TreasureHunter.clearAndReload();
            if (player == Devourer.devourer) Devourer.clearAndReload();

            if (!ignoreLovers && (player == Modifiers.lover1 || player == Modifiers.lover2)) { // The whole Lover couple is being erased
                Modifiers.ClearLovers();
            }
            if (player == Renegade.renegade) {
                Renegade.clearAndReload();
            }
            if (player == Minion.minion) Minion.clearAndReload();
        }


        public static void bountyHunterSetKill(byte playerid) {
            if (BountyHunter.bountyhunter == null) return;
            {
                foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                    if (player.PlayerId == playerid) {
                        BountyHunter.hasToKill = player;
                    }
                }

                if (BountyHunter.hasToKill.Data.IsDead) {
                    BountyHunter.bountyhunter.MurderPlayer(BountyHunter.bountyhunter);
                }

                if (BountyHunter.hasToKill == Joker.joker) {
                    BountyHunter.rolName = ": <color=#808080FF>Joker</color>";
                }
                else if (BountyHunter.hasToKill == RoleThief.rolethief) {
                    BountyHunter.rolName = ": <color=#808080FF>Role Thief</color>";
                }
                else if (BountyHunter.hasToKill == Pyromaniac.pyromaniac) {
                    BountyHunter.rolName = ": <color=#808080FF>Pyromaniac</color>";
                }
                else if (BountyHunter.hasToKill == TreasureHunter.treasureHunter) {
                    BountyHunter.rolName = ": <color=#808080FF>Treasure Hunter</color>";
                }
                else if (BountyHunter.hasToKill == Devourer.devourer) {
                    BountyHunter.rolName = ": <color=#808080FF>Devourer</color>";
                }
                else if (BountyHunter.hasToKill == Captain.captain) {
                    BountyHunter.rolName = ": <color=#5E3E7DFF>Captain</color>";
                }
                else if (BountyHunter.hasToKill == Mechanic.mechanic) {
                    BountyHunter.rolName = ": <color=#7F4C00FF>Mechanic</color>"; ;
                }
                else if (BountyHunter.hasToKill == Sheriff.sheriff) {
                    BountyHunter.rolName = ": <color=#FFFF00FF>Sheriff</color>";
                }
                else if (BountyHunter.hasToKill == Detective.detective) {
                    BountyHunter.rolName = ": <color=#A600FFFF>Detective</color>";
                }
                else if (BountyHunter.hasToKill == Forensic.forensic) {
                    BountyHunter.rolName = ": <color=#4E61FFFF>Forensic</color>";
                }
                else if (BountyHunter.hasToKill == TimeTraveler.timeTraveler) {
                    BountyHunter.rolName = ": <color=#00BDFFFF>Time Traveler</color>";
                }
                else if (BountyHunter.hasToKill == Squire.squire) {
                    BountyHunter.rolName = ": <color=#00FF00FF>Squire</color>";
                }
                else if (BountyHunter.hasToKill == Cheater.cheater) {
                    BountyHunter.rolName = ": <color=#F08048FF>Cheater</color>";
                }
                else if (BountyHunter.hasToKill == FortuneTeller.fortuneTeller) {
                    BountyHunter.rolName = ": <color=#00C642FF>Fortune Teller</color>";
                }
                else if (BountyHunter.hasToKill == Hacker.hacker) {
                    BountyHunter.rolName = ": <color=#72FFACFF>Hacker</color>";
                }
                else if (BountyHunter.hasToKill == Sleuth.sleuth) {
                    BountyHunter.rolName = ": <color=#009F57FF>Sleuth</color>";
                }
                else if (BountyHunter.hasToKill == Fink.fink) {
                    BountyHunter.rolName = ": <color=#FF73F6FF>Fink</color>";
                }
                else if (BountyHunter.hasToKill == Welder.welder) {
                    BountyHunter.rolName = ": <color=#6D5B2FFF>Welder</color>";
                }
                else if (BountyHunter.hasToKill == Spiritualist.spiritualist) {
                    BountyHunter.rolName = ": <color=#FFC5E1FF>Spiritualist</color>";
                }
                else if (BountyHunter.hasToKill == TheChosenOne.theChosenOne) {
                    BountyHunter.rolName = ": <color=#00F7E1FF>The Chosen One</color>";
                }
                else if (BountyHunter.hasToKill == Vigilant.vigilant) {
                    BountyHunter.rolName = ": <color=#E3E15AFF>Vigilant</color>";
                }
                else if (BountyHunter.hasToKill == Vigilant.vigilantMira) {
                    BountyHunter.rolName = ": <color=#E3E15AFF>Vigilant</color>";
                }
                else if (BountyHunter.hasToKill == Performer.performer) {
                    BountyHunter.rolName = ": <color=#F2BEFFFF>Performer</color>";
                }
                else if (BountyHunter.hasToKill == Hunter.hunter) {
                    BountyHunter.rolName = ": <color=#E1EB90FF>Hunter</color>";
                }
                else if (BountyHunter.hasToKill == Jinx.jinx) {
                    BountyHunter.rolName = ": <color=#928B55FF>Jinx</color>";
                }
                else if (BountyHunter.hasToKill == Mimic.mimic) {
                    BountyHunter.rolName = ": <color=#FF0000FF>Mimic</color>";
                }
                else if (BountyHunter.hasToKill == Painter.painter) {
                    BountyHunter.rolName = ": <color=#FF0000FF>Painter</color>";
                }
                else if (BountyHunter.hasToKill == Demon.demon) {
                    BountyHunter.rolName = ": <color=#FF0000FF>Demon</color>";
                }
                else if (BountyHunter.hasToKill == Janitor.janitor) {
                    BountyHunter.rolName = ": <color=#FF0000FF>Janitor</color>";
                }
                else if (BountyHunter.hasToKill == Ilusionist.ilusionist) {
                    BountyHunter.rolName = ": <color=#FF0000FF>Ilusionist</color>";
                }
                else if (BountyHunter.hasToKill == Manipulator.manipulator) {
                    BountyHunter.rolName = ": <color=#FF0000FF>Manipulator</color>";
                }
                else if (BountyHunter.hasToKill == Bomberman.bomberman) {
                    BountyHunter.rolName = ": <color=#FF0000FF>Bomberman</color>";
                }
                else if (BountyHunter.hasToKill == Chameleon.chameleon) {
                    BountyHunter.rolName = ": <color=#FF0000FF>Chameleon</color>";
                }
                else if (BountyHunter.hasToKill == Gambler.gambler) {
                    BountyHunter.rolName = ": <color=#FF0000FF>Gambler</color>";
                }
                else if (BountyHunter.hasToKill == Sorcerer.sorcerer) {
                    BountyHunter.rolName = ": <color=#FF0000FF>Sorcerer</color>";
                }
                else if (BountyHunter.hasToKill == Modifiers.lighter) {
                    BountyHunter.rolName = ": <color=#F08048FF>Lighter</color>";
                }
                else if (BountyHunter.hasToKill == Modifiers.blind) {
                    BountyHunter.rolName = ": <color=#F08048FF>Blind</color>";
                }
                else if (BountyHunter.hasToKill == Modifiers.flash) {
                    BountyHunter.rolName = ": <color=#F08048FF>Flash</color>";
                }
                else if (BountyHunter.hasToKill.Data.Role.IsImpostor) {
                    BountyHunter.rolName = ": <color=#FF0000FF>Impostor</color>";
                }
                else {
                    BountyHunter.rolName = ": <color=#8DFFFFFF>Crewmate</color>";
                }
            }

            RoleInfo.bountyHunter.shortDescription = "Hunt down your target" + BountyHunter.rolName;
            BountyHunter.usedTarget = true;
        }

        public static void bountyHunterKill(byte targetId) {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (player.PlayerId == targetId && BountyHunter.hasToKill.PlayerId == targetId) {
                    BountyHunter.triggerBountyHunterWin = true;
                    BountyHunter.bountyhunter.MurderPlayer(player);
                    return;
                }
                else if (player.PlayerId == targetId) {
                    BountyHunter.bountyhunter.MurderPlayer(player);
                    return;
                }
            }
        }

        public static void placeMine() {
            Trapper.currentMineNumber += 1;
            new Mine(Trapper.durationOfMines, Trapper.trapper);
        }
        public static void placeTrap() {
            Trapper.currentTrapNumber += 1;
            new Trap(Trapper.durationOfTraps, Trapper.trapper);
        }

        public static void mineKill(byte targetId) {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {

                HudManager.Instance.StartCoroutine(Effects.Lerp(1, new Action<float>((p) => {
                    if (p == 1f) {
                        foreach (Mine mine in Mine.mines) {
                            if (Vector2.Distance(player.transform.position, mine.mine.transform.position) < 1f && player != Trapper.trapper) {
                                mine.mine.transform.position = new Vector3(-1000, 500, 0);
                            }
                        }
                    }
                })));

                if (player.PlayerId == targetId) {
                    player.moveable = false;
                    player.NetTransform.Halt();
                    HudManager.Instance.StartCoroutine(Effects.Lerp(1, new Action<float>((p) => {
                        if (p == 1f) {
                            player.moveable = true;

                        }
                    })));
                }

                if (player.PlayerId == targetId) {
                    HudManager.Instance.StartCoroutine(Effects.Lerp(1, new Action<float>((p) => {
                        if (player == PlayerControl.LocalPlayer) {
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.trapperStepMineClip, false, 100f);
                        }
                        Trapper.mined = player;
                        if (p == 1f) {
                            MurderAttemptResult murderAttemptResult = Helpers.checkMurderAttempt(Trapper.trapper, Trapper.mined);
                            if (murderAttemptResult == MurderAttemptResult.PerformKill) {
                                uncheckedMurderPlayer(Trapper.trapper.PlayerId, Trapper.mined.PlayerId, 0);
                            }
                            player.moveable = true;
                            Trapper.mined = null;

                        }
                    })));
                    return;
                }
            }
        }

        public static void activateTrap(byte targetId) {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {

                if (player.PlayerId == targetId) {
                    foreach (Trap trap in Trap.traps) {
                        if (Vector2.Distance(player.transform.position, trap.trap.transform.position) < 1f && player != Trapper.trapper) {
                            player.transform.position = trap.trap.transform.position;
                        }
                    }
                    if (player == PlayerControl.LocalPlayer) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.trapperStepTrapClip, false, 100f);
                    }
                    player.moveable = false;
                    player.NetTransform.Halt();
                    HudManager.Instance.StartCoroutine(Effects.Lerp(5, new Action<float>((p) => {
                        if (p == 1f) {
                            player.moveable = true;
                            foreach (Trap trap in Trap.traps) {
                                if (Vector2.Distance(player.transform.position, trap.trap.transform.position) < 1f && player != Trapper.trapper) {
                                    trap.trap.transform.position = new Vector3(-1000, 500, 0);
                                }
                            }
                        }
                    })));
                }
            }
        }
        public static void yinyangerSetYinyang(byte targetId, byte yinflag) {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (player.PlayerId == targetId && !player.Data.IsDead) {
                    if (yinflag == 0) {
                        Yinyanger.yinyedplayer = player;
                        Yinyanger.usedYined = true;
                    }
                    else {
                        Yinyanger.yangyedplayer = player;
                        Yinyanger.usedYanged = true;
                    }
                }
            }
        }

        public static void challengerSetRival(byte targetId, byte resetRival) {
            if (resetRival != 0) {
                Challenger.rivalPlayer = null;
                return;
            }

            if (Challenger.challenger == null) return;
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (player.PlayerId == targetId && !player.Data.IsDead) {
                    Challenger.rivalPlayer = player;
                }
            }
        }

        public static void challengerPerformDuel() {

            // Force exit from vents to all players
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

            // Force map close to all players
            if (PlayerControl.LocalPlayer.Data.Role.IsImpostor && MapBehaviour.Instance || PlayerControl.LocalPlayer == Joker.joker && MapBehaviour.Instance) {
                MapBehaviour.Instance.Close();
            }
            // Force task close to all players
            if (Minigame.Instance)
                Minigame.Instance.ForceClose();

            new CustomMessage("Time Left: ", Challenger.duelDuration, -1, -1.3f, 2);

            // music stop and play duel music
            changeMusic(8);
            SoundManager.Instance.PlaySound(CustomMain.customAssets.challengerDuelMusic, false, 5f);

            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (player == PlayerControl.LocalPlayer) {
                    positionBeforeDuel = player.transform.position;
                }
            }

            Challenger.duelDuration = 30f;
            Challenger.isDueling = true;
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (player == Challenger.challenger) {
                    player.transform.position = new Vector3(45.26f, 0f, player.transform.position.z);
                }
                else if (player == Challenger.rivalPlayer) {
                    player.transform.position = new Vector3(48f, 0f, player.transform.position.z);
                }
                else {
                    player.transform.position = new Vector3(46.7f, 1f, player.transform.position.z);
                }
            }

            resetDuelButtons();
        }

        public static void challengerSelectAttack(byte challengerAttack) {
            switch (challengerAttack) {
                case 1:
                    Challenger.challengerRock = true;
                    break;
                case 2:
                    Challenger.challengerPaper = true;
                    break;
                case 3:
                    Challenger.challengerScissors = true;
                    break;
                case 4:
                    Challenger.rivalRock = true;
                    break;
                case 5:
                    Challenger.rivalPaper = true;
                    break;
                case 6:
                    Challenger.rivalScissors = true;
                    break;
            }
        }        

        public static void challengerCantDuel() {
            Challenger.challengerIsInMeeting = true;
        }

        public static PlayerControl oldRoleThief = null;

        public static void roleThiefSteal(byte targetId) {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (player.PlayerId == targetId && RoleThief.rolethief != null) {
                    // Suicide when impostor or rebel variants
                    if (player.Data.Role.IsImpostor || player == Renegade.renegade || player == Minion.minion || player == BountyHunter.bountyhunter || player == Trapper.trapper || player == Yinyanger.yinyanger || player == Challenger.challenger) {
                        RoleThief.rolethief.MurderPlayer(RoleThief.rolethief);
                        return;
                    }

                    oldRoleThief = RoleThief.rolethief;
                    // Switch tasks
                    var roleThiefSabotageTasks = oldRoleThief.myTasks;
                    oldRoleThief.myTasks = player.myTasks;
                    player.myTasks = roleThiefSabotageTasks;

                    // Switch shield
                    if (Squire.shielded != null && Squire.shielded == player) {
                        Squire.shielded.myRend.material.SetFloat("_Outline", 0f);
                        Squire.shielded = oldRoleThief;
                    }
                    else if (Squire.shielded != null && Squire.shielded == oldRoleThief) {
                        Squire.shielded.myRend.material.SetFloat("_Outline", 0f);
                        Squire.shielded = player;
                    }

                    // Switch role
                    if (Captain.captain != null && Captain.captain == player) {
                        Captain.captain = oldRoleThief;
                    }
                    else if (Mechanic.mechanic != null && Mechanic.mechanic == player) {
                        Mechanic.mechanic = oldRoleThief;
                    }
                    else if (Sheriff.sheriff != null && Sheriff.sheriff == player) {
                        Sheriff.sheriff = oldRoleThief;
                    }
                    else if (Detective.detective != null && Detective.detective == player) {
                        Detective.detective = oldRoleThief;
                    }
                    else if (Forensic.forensic != null && Forensic.forensic == player) {
                        Forensic.forensic = oldRoleThief;
                    }
                    else if (TimeTraveler.timeTraveler != null && TimeTraveler.timeTraveler == player) {
                        TimeTraveler.timeTraveler = oldRoleThief;
                    }
                    else if (Squire.squire != null && Squire.squire == player) {
                        Squire.squire = oldRoleThief;
                    }
                    else if (Cheater.cheater != null && Cheater.cheater == player) {
                        Cheater.cheater = oldRoleThief;
                    }
                    else if (FortuneTeller.fortuneTeller != null && FortuneTeller.fortuneTeller == player) {
                        FortuneTeller.fortuneTeller = oldRoleThief;
                    }
                    else if (Hacker.hacker != null && Hacker.hacker == player) {
                        if (Hacker.vitals != null) {
                            Hacker.vitals.ForceClose();
                        }
                        if (Hacker.hacker == PlayerControl.LocalPlayer) {
                            if (MapBehaviour.Instance && MapBehaviour.Instance.isActiveAndEnabled) MapBehaviour.Instance.Close();
                        }
                        Hacker.hacker = oldRoleThief;
                    }
                    else if (Sleuth.sleuth != null && Sleuth.sleuth == player) {
                        Sleuth.sleuth = oldRoleThief;
                    }
                    else if (Fink.fink != null && Fink.fink == player) {
                        Fink.fink = oldRoleThief;
                    }
                    else if (Kid.kid != null && Kid.kid == player) {
                        Kid.kid = oldRoleThief;
                    }
                    else if (Welder.welder != null && Welder.welder == player) {
                        Welder.welder = oldRoleThief;
                    }
                    else if (Spiritualist.spiritualist != null && Spiritualist.spiritualist == player) {
                        Spiritualist.spiritualist = oldRoleThief;
                    }
                    else if (TheChosenOne.theChosenOne != null && TheChosenOne.theChosenOne == player) {
                        TheChosenOne.theChosenOne = oldRoleThief;
                        if (TheChosenOne.theChosenOne.Data.IsDead) TheChosenOne.reported = true;
                    }
                    else if (Vigilant.vigilant != null && Vigilant.vigilant == player) {
                        if (Vigilant.minigame != null) {
                            Vigilant.minigame.ForceClose();
                        }
                        Vigilant.vigilant = oldRoleThief;
                    }
                    else if (Vigilant.vigilantMira != null && Vigilant.vigilantMira == player) {
                        // Vigilant delete doorlog item when switch rol
                        GameObject vigilantdoorlog = GameObject.Find("VigilantDoorLog");
                        if (vigilantdoorlog != null) {
                            UnityEngine.Object.Destroy(vigilantdoorlog);
                        }
                        Vigilant.vigilantMira = oldRoleThief;
                        // Recreate the doorlog access from anywhere to the new Vigilant after rol switch
                        if (PlayerControl.GameOptions.MapId == 1 && Vigilant.vigilantMira == PlayerControl.LocalPlayer) {
                            GameObject vigilantDoorLog = GameObject.Find("SurvLogConsole");
                            Vigilant.doorLog = GameObject.Instantiate(vigilantDoorLog, Vigilant.vigilantMira.transform);
                            Vigilant.doorLog.name = "VigilantDoorLog";
                            Vigilant.doorLog.layer = 8; // Player layer to ignore collisions
                            Vigilant.doorLog.GetComponent<SpriteRenderer>().enabled = false;
                            Vigilant.doorLog.transform.localPosition = new Vector2(0, -0.5f);
                        }
                    }
                    else if (Performer.performer != null && Performer.performer == player) {
                        Performer.performer = oldRoleThief;
                    }
                    else if (Hunter.hunter != null && Hunter.hunter == player) {
                        Hunter.hunter = oldRoleThief;
                    }
                    else if (Jinx.jinx != null && Jinx.jinx == player) {
                        Jinx.jinx = oldRoleThief;
                    }
                    else { // Crewmate
                    }

                    RoleThief.rolethief = player;

                    // Set cooldowns to max for both players
                    if (PlayerControl.LocalPlayer == RoleThief.rolethief || PlayerControl.LocalPlayer == oldRoleThief)
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.roleThiefStealRole, false, 100f);
                    CustomButton.ResetAllCooldowns();
                }
            }
        }

        public static void pyromaniacWin() {
            SoundManager.Instance.PlaySound(CustomMain.customAssets.pyromaniacIgniteClip, false, 100f);
            Pyromaniac.triggerPyromaniacWin = true;
            foreach (PlayerControl p in PlayerControl.AllPlayerControls) {
                if (p != Pyromaniac.pyromaniac) p.Exiled();
            }
        }

        public static void placeTreasure() {
            TreasureHunter.canPlace = false;
            new Treasure(1800, TreasureHunter.treasureHunter);
        }

        public static void collectedTreasure() {
            if (PlayerControl.LocalPlayer == TreasureHunter.treasureHunter) {
                SoundManager.Instance.PlaySound(CustomMain.customAssets.treasureHunterCollectTreasure, false, 100f);
            }
            TreasureHunter.treasureCollected += 1;
            if (TreasureHunter.treasureCollected >= TreasureHunter.neededTreasure) {
                TreasureHunter.triggertreasureHunterWin = true;
            }
        }

        public static void devourBody(byte playerId) {
            DeadBody[] array = UnityEngine.Object.FindObjectsOfType<DeadBody>();
            for (int i = 0; i < array.Length; i++) {
                if (GameData.Instance.GetPlayerById(array[i].ParentId).PlayerId == playerId) {
                    UnityEngine.Object.Destroy(array[i].gameObject);
                    if (Janitor.janitor != null && Janitor.dragginBody && Janitor.bodyId == playerId) {
                        janitorResetValues();
                    }
                }
            }
            if (Performer.performer != null && playerId == Performer.performer.PlayerId)
                performerIsReported(0);
            if (PlayerControl.LocalPlayer == Devourer.devourer) {
                SoundManager.Instance.PlaySound(CustomMain.customAssets.devourerDevourClip, false, 100f);
            }
            Devourer.devouredBodies += 1;
            if (Devourer.devouredBodies >= Devourer.neededBodies) {
                Devourer.triggerdevourerWin = true;
            }
        }

        public static void mechanicFixLights() {
            SwitchSystem switchSystem = ShipStatus.Instance.Systems[SystemTypes.Electrical].Cast<SwitchSystem>();
            switchSystem.ActualSwitches = switchSystem.ExpectedSwitches;
        }

        public static void mechanicUsedRepair() {
            if (Mechanic.timesUsedRepairs < Mechanic.numberOfRepairs) {
                Mechanic.timesUsedRepairs += 1;
                if (Mechanic.timesUsedRepairs == Mechanic.numberOfRepairs) {
                    Mechanic.usedRepair = true;
                }
            }
            Mechanic.mechanicRepairButtonText.text = $"{Mechanic.numberOfRepairs - Mechanic.timesUsedRepairs} / {Mechanic.numberOfRepairs}";
        }

        public static void sheriffKill(byte targetId) {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (player.PlayerId == targetId) {
                    Sheriff.sheriff.MurderPlayer(player);
                    return;
                }
            }
        }

        public static void timeTravelerShield() {
            TimeTraveler.shieldActive = true;
            HudManager.Instance.StartCoroutine(Effects.Lerp(TimeTraveler.shieldDuration, new Action<float>((p) => {
                if (p == 1f) TimeTraveler.shieldActive = false;
            })));
        }

        public static void timeTravelerRewindTime() {
            if (TimeTraveler.shieldActive == true) {
                TimeTraveler.usedShield = true;
            }
            TimeTraveler.shieldActive = false; // Shield is no longer active when rewinding
            if (TimeTraveler.timeTraveler != null && TimeTraveler.timeTraveler == PlayerControl.LocalPlayer) {
                resetTimeTravelerButton();
            }
            HudManager.Instance.FullScreen.color = new Color(0f, 0.5f, 0.8f, 0.3f);
            HudManager.Instance.FullScreen.enabled = true;
            HudManager.Instance.StartCoroutine(Effects.Lerp(TimeTraveler.rewindTime / 2, new Action<float>((p) => {
                if (p == 1f) HudManager.Instance.FullScreen.enabled = false;
            })));

            SoundManager.Instance.PlaySound(CustomMain.customAssets.timeTravelerTimeReverseClip, false, 100f);

            if (TimeTraveler.timeTraveler == null || PlayerControl.LocalPlayer == TimeTraveler.timeTraveler) return; // TimeTraveler himself does not rewind

            TimeTraveler.isRewinding = true;

            if (MapBehaviour.Instance)
                MapBehaviour.Instance.Close();
            if (Minigame.Instance)
                Minigame.Instance.ForceClose();
            PlayerControl.LocalPlayer.moveable = false;
        }

        public static void timeTravelerRevive(byte playerId) {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                if (player.PlayerId == playerId) {
                    player.Revive();
                    var body = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == playerId);
                    DeadPlayer deadPlayerEntry = deadPlayers.Where(x => x.player.PlayerId == playerId).FirstOrDefault();
                    if (body != null) UnityEngine.Object.Destroy(body.gameObject);
                    if (deadPlayerEntry != null) deadPlayers.Remove(deadPlayerEntry);
                    if (Performer.performer != null && player.PlayerId == Performer.performer.PlayerId) {
                        performerIsReported(1);
                    }
                    if (Vigilant.vigilantMira != null && player.PlayerId == Vigilant.vigilantMira.PlayerId) {
                        vigilantMiraRestoreDoorlogItem();
                    }
                    if (Hunter.hunter != null && player.PlayerId == Hunter.hunter.PlayerId) {
                        Hunter.resetHunted();
                    }
                    if (Janitor.janitor != null && Janitor.dragginBody) {
                        janitorResetValues();
                    }
                }
        }

        public static void squireSetShielded(byte shieldedId) {
            Squire.usedShield = true;
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                if (player.PlayerId == shieldedId)
                    Squire.shielded = player;
        }

        public static void shieldedMurderAttempt() {
            if (Squire.shielded != null && Squire.shielded == PlayerControl.LocalPlayer && Squire.showAttemptToShielded || Squire.showAttemptToShielded && (PlayerControl.LocalPlayer.Data.Role.IsImpostor || Sheriff.sheriff != null && Sheriff.sheriff == PlayerControl.LocalPlayer || Renegade.renegade != null && Renegade.renegade == PlayerControl.LocalPlayer || Minion.minion != null && Minion.minion == PlayerControl.LocalPlayer || BountyHunter.bountyhunter != null && BountyHunter.bountyhunter == PlayerControl.LocalPlayer || Trapper.trapper != null && Trapper.trapper == PlayerControl.LocalPlayer || Yinyanger.yinyanger != null && Yinyanger.yinyanger == PlayerControl.LocalPlayer || Challenger.challenger != null && Challenger.challenger == PlayerControl.LocalPlayer)) {
                SoundManager.Instance.PlaySound(CustomMain.customAssets.squireShieldClip, false, 100f);
                return;
            }
        }

        public static void cheaterCheat(byte playerId1, byte playerId2) {
            if (MeetingHud.Instance) {
                Cheater.playerId1 = playerId1;
                Cheater.playerId2 = playerId2;
                GameData.PlayerInfo playerById1 = GameData.Instance.GetPlayerById((byte)Cheater.playerId1);
                Cheater.cheatedP1 = playerById1.Object;
                GameData.PlayerInfo playerById2 = GameData.Instance.GetPlayerById((byte)Cheater.playerId2);
                Cheater.cheatedP2 = playerById2.Object;
                Cheater.usedCheat = true;
            }
        }

        public static void fortuneTellerReveal(byte targetId) {
            if (FortuneTeller.fortuneTeller == null) return;

            PlayerControl target = Helpers.playerById(targetId);

            FortuneTeller.revealedPlayers.Add(target);

            if (PlayerControl.LocalPlayer == FortuneTeller.fortuneTeller) {
                SoundManager.Instance.PlaySound(CustomMain.customAssets.fortuneTellerRevealClip, false, 100f);
            }

            if (PlayerControl.LocalPlayer == target && HudManager.Instance?.FullScreen != null) {
                RoleFortuneTellerInfo si = RoleFortuneTellerInfo.getFortuneTellerRoleInfoForPlayer(target); // Use RoleInfo of target here, because we need the isGood of the targets role
                bool showNotification = false;
                if (FortuneTeller.playersWithNotification == 0 && !si.isGood) showNotification = true;
                else if (FortuneTeller.playersWithNotification == 1 && si.isGood) showNotification = true;
                else if (FortuneTeller.playersWithNotification == 2) showNotification = true;
                else if (FortuneTeller.playersWithNotification == 3) showNotification = false;

                if (showNotification) {
                    SoundManager.Instance.PlaySound(CustomMain.customAssets.fortuneTellerRevealClip, false, 100f); 
                    HudManager.Instance.FullScreen.enabled = true;
                    HudManager.Instance.StartCoroutine(Effects.Lerp(0.5f, new Action<float>((p) => {
                        var renderer = HudManager.Instance.FullScreen;
                        Color c = new Color(42f / 255f, 187f / 255f, 245f / 255f, 0f);
                        if (p < 0.5) {
                            if (renderer != null)
                                renderer.color = new Color(c.r, c.g, c.b, Mathf.Clamp01(p * 2 * 0.75f));
                        }
                        else {
                            if (renderer != null)
                                renderer.color = new Color(c.r, c.g, c.b, Mathf.Clamp01((1 - p) * 2 * 0.75f));
                        }
                    })));
                }
            }

            if (FortuneTeller.timesUsedFortune < FortuneTeller.numberOfFortunes) {
                FortuneTeller.timesUsedFortune += 1;
                if (FortuneTeller.timesUsedFortune == FortuneTeller.numberOfFortunes) {
                    FortuneTeller.usedFortune = true;
                }
            }
            FortuneTeller.fortuneTellerRevealButtonText.text = $"{FortuneTeller.numberOfFortunes - FortuneTeller.timesUsedFortune} / {FortuneTeller.numberOfFortunes}";
        }

        public static void sleuthUsedLocate(byte targetId) {
            Sleuth.usedLocate = true;
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                if (player.PlayerId == targetId)
                    Sleuth.located = player;
        }

        public static void sealVent(int ventId) {
            Vent vent = ShipStatus.Instance.AllVents.FirstOrDefault((x) => x != null && x.Id == ventId);
            if (vent == null) return;

            Welder.remainingWelds -= 1;
            MapOptions.ventsToSeal.Add(vent);
            Welder.welderButtonText.text = $"{Welder.remainingWelds} / {Welder.totalWelds}";
        }

        public static void spiritualistRevive(byte playerId) {

            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (PlayerControl.LocalPlayer == Spiritualist.spiritualist) {
                    SoundManager.Instance.PlaySound(CustomMain.customAssets.spiritualistRevive, false, 100f);
                }

                if (player.PlayerId == playerId) {
                    Spiritualist.revivedPlayer = Helpers.playerById(playerId);
                    Spiritualist.usedRevive = true;
                    player.Revive();
                    var body = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == playerId);
                    player.transform.position = body.transform.position;
                    DeadPlayer deadPlayerEntry = deadPlayers.Where(x => x.player.PlayerId == playerId).FirstOrDefault();
                    if (body != null) UnityEngine.Object.Destroy(body.gameObject);
                    if (deadPlayerEntry != null) deadPlayers.Remove(deadPlayerEntry);
                    spiritualistPinkScreen(player.PlayerId);
                    if (Performer.performer != null && player == Performer.performer)
                        performerIsReported(1);
                    if (Vigilant.vigilantMira != null && player == Vigilant.vigilantMira)
                        vigilantMiraRestoreDoorlogItem();
                    if (Hunter.hunter != null && player == Hunter.hunter)
                        Hunter.resetHunted();
                    if (Janitor.janitor != null && Janitor.dragginBody) {
                        janitorResetValues();
                    }
                    // Check lovers, revive lover2 or lover1 too
                    if (player == Modifiers.lover1) {
                        PlayerControl lovertwo = Modifiers.lover2;
                        lovertwo.Revive();
                        var bodytwo = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == lovertwo.PlayerId);
                        lovertwo.transform.position = bodytwo.transform.position;
                        DeadPlayer deadPlayerEntrytwo = deadPlayers.Where(x => x.player.PlayerId == lovertwo.PlayerId).FirstOrDefault();
                        if (bodytwo != null) UnityEngine.Object.Destroy(bodytwo.gameObject);
                        if (deadPlayerEntrytwo != null) deadPlayers.Remove(deadPlayerEntrytwo);
                        spiritualistPinkScreen(lovertwo.PlayerId);
                        if (Performer.performer != null && player == Performer.performer)
                            performerIsReported(1);
                        if (Vigilant.vigilantMira != null && player == Vigilant.vigilantMira)
                            vigilantMiraRestoreDoorlogItem();
                        if (Hunter.hunter != null && player == Hunter.hunter)
                            Hunter.resetHunted();
                        if (Janitor.janitor != null && Janitor.dragginBody) {
                            janitorResetValues();
                        }
                    }
                    else if (player == Modifiers.lover2) {
                        PlayerControl loverone = Modifiers.lover1;
                        loverone.Revive();
                        var bodytwo = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == loverone.PlayerId);
                        loverone.transform.position = bodytwo.transform.position;
                        DeadPlayer deadPlayerEntrytwo = deadPlayers.Where(x => x.player.PlayerId == loverone.PlayerId).FirstOrDefault();
                        if (bodytwo != null) UnityEngine.Object.Destroy(bodytwo.gameObject);
                        if (deadPlayerEntrytwo != null) deadPlayers.Remove(deadPlayerEntrytwo);
                        spiritualistPinkScreen(loverone.PlayerId);
                        if (Performer.performer != null && player == Performer.performer)
                            performerIsReported(1);
                        if (Vigilant.vigilantMira != null && player == Vigilant.vigilantMira)
                            vigilantMiraRestoreDoorlogItem();
                        if (Hunter.hunter != null && player == Hunter.hunter)
                            Hunter.resetHunted();
                        if (Janitor.janitor != null && Janitor.dragginBody) {
                            janitorResetValues();
                        }
                    }

                    // Check bountyhunter kill and revive bountyhunter
                    if (player == BountyHunter.hasToKill) {
                        PlayerControl bountyhunter = BountyHunter.bountyhunter;
                        bountyhunter.Revive();
                        var bodybountyhunter = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == bountyhunter.PlayerId);
                        bountyhunter.transform.position = bodybountyhunter.transform.position;
                        DeadPlayer deadPlayerEntrytwo = deadPlayers.Where(x => x.player.PlayerId == bountyhunter.PlayerId).FirstOrDefault();
                        if (bodybountyhunter != null) UnityEngine.Object.Destroy(bodybountyhunter.gameObject);
                        if (deadPlayerEntrytwo != null) deadPlayers.Remove(deadPlayerEntrytwo);
                        spiritualistPinkScreen(bountyhunter.PlayerId);
                        if (Performer.performer != null && player == Performer.performer)
                            performerIsReported(1);
                        if (Vigilant.vigilantMira != null && player == Vigilant.vigilantMira)
                            vigilantMiraRestoreDoorlogItem();
                        if (Hunter.hunter != null && player == Hunter.hunter)
                            Hunter.resetHunted();
                        if (Janitor.janitor != null && Janitor.dragginBody) {
                            janitorResetValues();
                        }
                    }

                    // Check bountyhunter and try reviving their target
                    if (player == BountyHunter.bountyhunter) {
                        if (BountyHunter.hasToKill != null && BountyHunter.hasToKill.Data.IsDead) {
                            PlayerControl bountytarget = BountyHunter.hasToKill;
                            bountytarget.Revive();
                            var bodybountytarget = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == bountytarget.PlayerId);
                            bountytarget.transform.position = bodybountytarget.transform.position;
                            DeadPlayer deadPlayerEntrytwo = deadPlayers.Where(x => x.player.PlayerId == bountytarget.PlayerId).FirstOrDefault();
                            if (bodybountytarget != null) UnityEngine.Object.Destroy(bodybountytarget.gameObject);
                            if (deadPlayerEntrytwo != null) deadPlayers.Remove(deadPlayerEntrytwo);
                            spiritualistPinkScreen(bountytarget.PlayerId);
                        }
                    }
                }
            }

            Spiritualist.preventReport = true;
            murderSpiritualistIfReportWhileReviving();
            HudManager.Instance.StartCoroutine(Effects.Lerp(0.5f, new Action<float>((p) => { // Delayed action
                if (p == 1f) {
                    removeBody(Spiritualist.spiritualist.PlayerId);
                    Spiritualist.preventReport = false;
                }
            })));
        }

        public static void spiritualistPinkScreen(byte playerId) {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (player.PlayerId == playerId && playerId == PlayerControl.LocalPlayer.PlayerId) {
                    SoundManager.Instance.PlaySound(CustomMain.customAssets.spiritualistRevive, false, 100f);
                    HudManager.Instance.FullScreen.enabled = true;
                    HudManager.Instance.StartCoroutine(Effects.Lerp(0.5f, new Action<float>((p) => {
                        var renderer = HudManager.Instance.FullScreen;
                        Color c = new Color(255f / 255f, 197f / 255f, 255f / 255f, 0f);
                        if (p < 0.5) {
                            if (renderer != null)
                                renderer.color = new Color(c.r, c.g, c.b, Mathf.Clamp01(p * 2 * 0.75f));
                        }
                        else {
                            if (renderer != null)
                                renderer.color = new Color(c.r, c.g, c.b, Mathf.Clamp01((1 - p) * 2 * 0.75f));
                        }
                    })));
                }
            }
        }

        public static void sendSpiritualistIsReviving() {
            Spiritualist.canRevive = true;
            Spiritualist.isReviving = true;
        }

        public static void murderSpiritualistIfReportWhileReviving() {
            Spiritualist.spiritualist.MurderPlayer(Spiritualist.spiritualist);
            resetSpiritualistReviveValues();
        }

        public static void resetSpiritualistReviveValues() {
            Spiritualist.canRevive = false;
            Spiritualist.isReviving = false;
            resetSpiritualistReviveButton();
        }

        public static void placeCamera(byte[] buff) {
            var referenceCamera = UnityEngine.Object.FindObjectOfType<SurvCamera>();
            if (referenceCamera == null) return; // Mira HQ

            Vigilant.placedCameras++;

            Vector3 position = Vector3.zero;
            position.x = BitConverter.ToSingle(buff, 0 * sizeof(float));
            position.y = BitConverter.ToSingle(buff, 1 * sizeof(float));

            var camera = UnityEngine.Object.Instantiate<SurvCamera>(referenceCamera);
            camera.transform.position = new Vector3(position.x, position.y, referenceCamera.transform.position.z - 1f);
            camera.CamName = $"Security Camera {Vigilant.placedCameras}";
            camera.Offset = new Vector3(0f, 0f, camera.Offset.z);
            if (PlayerControl.GameOptions.MapId == 2 || PlayerControl.GameOptions.MapId == 4) camera.transform.localRotation = new Quaternion(0, 0, 1, 1); // Polus and Airship 

            if (PlayerControl.LocalPlayer == Vigilant.vigilant) {
                camera.gameObject.SetActive(true);
                camera.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.5f);
            }
            else {
                camera.gameObject.SetActive(false);
            }
            MapOptions.camerasToAdd.Add(camera);

            Vigilant.remainingCameras -= 1;
            Vigilant.vigilantButtonCameraText.text = $"{Vigilant.remainingCameras} / {Vigilant.totalCameras}";
        }

        public static void vigilantMiraRestoreDoorlogItem() {
            // Vigilant restore doorlog item when revive
            if (Vigilant.vigilantMira != null && PlayerControl.LocalPlayer == Vigilant.vigilantMira) {
                Vigilant.doorLog.SetActive(true);
            }
        }

        public static void performerIsReported(byte check) {
            if (check == 0) {
                Performer.reported = true;
            }
            else {
                Performer.reported = false;
            }
            SoundManager.Instance.StopSound(CustomMain.customAssets.performerMusic);
        }

        public static void hunterUsedHunted(byte targetId) {
            Hunter.usedHunted = true;
            foreach (PlayerControl player in PlayerControl.AllPlayerControls)
                if (player.PlayerId == targetId)
                    Hunter.hunted = player;
        }

        public static void setJinxed(byte playerId, byte value) {
            PlayerControl target = Helpers.playerById(playerId);
            if (target == null) return;
            Jinx.jinxedList.RemoveAll(x => x.PlayerId == playerId);
            if (value > 0) {
                Jinx.jinxedList.Add(target);
                Jinx.jinxs++;
                Jinx.jinxButtonJinxsText.text = $"{Jinx.jinxNumber - Jinx.jinxs} / {Jinx.jinxNumber}";
            }
        }

        public static void hackerAbilityUses(byte value) {
            if (value == 0) {
                Hacker.chargesAdminTable--;
                Hacker.hackerAdminTableChargesText.text = $"{Hacker.chargesAdminTable} / {Hacker.toolsNumber}";
            }
            else if (value == 1) {
                Hacker.chargesVitals--;
                Hacker.hackerVitalsChargesText.text = $"{Hacker.chargesVitals} / {Hacker.toolsNumber}";
            }
            else {
                Hacker.rechargedTasks += Hacker.rechargeTasksNumber;
                if (Hacker.toolsNumber > Hacker.chargesVitals) Hacker.chargesVitals++;
                if (Hacker.toolsNumber > Hacker.chargesAdminTable) Hacker.chargesAdminTable++;
            }
        }

        public static void vigilantAbilityUses(byte value) {
            if (value == 0) {
                Vigilant.charges--;
                Vigilant.vigilantButtonCameraUsesText.text = $"{Vigilant.charges} / {Vigilant.maxCharges}";
            }
            else {
                Vigilant.rechargedTasks += Vigilant.rechargeTasksNumber;
                if (Vigilant.maxCharges > Vigilant.charges) Vigilant.charges++;
            }
        }
        
        
        public static void changeMusic(byte whichmusic) {
            SoundManager.Instance.StopSound(CustomMain.customAssets.bombermanBombMusic);
            SoundManager.Instance.StopSound(CustomMain.customAssets.challengerDuelMusic);
            if (MapOptions.activateMusic && ((!CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode) || (CaptureTheFlag.captureTheFlagMode && PoliceAndThief.policeAndThiefMode))) {
                int alivePlayers = 0;
                int totalPlayers = 0;
                foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                    if (!player.Data.IsDead) {
                        alivePlayers += 1;
                    }
                    totalPlayers += 1;
                }

                // Stop all background music
                SoundManager.Instance.StopSound(CustomMain.customAssets.tasksCalmMusic);
                SoundManager.Instance.StopSound(CustomMain.customAssets.tasksCoreMusic);
                SoundManager.Instance.StopSound(CustomMain.customAssets.tasksFinalMusic);
                SoundManager.Instance.StopSound(CustomMain.customAssets.meetingCalmMusic);
                SoundManager.Instance.StopSound(CustomMain.customAssets.meetingCoreMusic);
                SoundManager.Instance.StopSound(CustomMain.customAssets.meetingFinalMusic);

                // Select which music moment
                switch (whichmusic) {
                    case 1:
                        // Meeting music                        
                        if (alivePlayers >= MathF.Round(totalPlayers / 1.25f)) {
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.meetingCalmMusic, true, 5f);
                        }
                        else if (alivePlayers >= MathF.Round(totalPlayers / 1.5f) && alivePlayers < MathF.Round(totalPlayers / 1.25f)) {
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.meetingCoreMusic, true, 5f);
                        }
                        else {
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.meetingFinalMusic, true, 5f);
                        }
                        break;
                    case 2:
                        // Tasks music
                        if (alivePlayers >= MathF.Round(totalPlayers / 1.25f)) {
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.tasksCalmMusic, true, 5f);
                        }
                        else if (alivePlayers >= MathF.Round(totalPlayers / 1.5f) && alivePlayers < MathF.Round(totalPlayers / 1.25f)) {
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.tasksCoreMusic, true, 5f);
                        }
                        else {
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.tasksFinalMusic, true, 5f);
                        }
                        break;
                    case 3:
                        // Neutrals win
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.winNeutralsMusic, false, 5f);
                        break;
                    case 4:
                        // Rebels win
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.winRebelsMusic, false, 5f);
                        break;
                    case 5:
                        //Crewmates win
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.winCrewmatesMusic, false, 5f);
                        break;
                    case 6:
                        //Impostor win
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.winImpostorsMusic, false, 5f);
                        break;
                    case 7:
                        //Music from outside musicbundle (bomb, duel, performer, gamemodes)

                        break;
                }

            }
        }

        public static void capturetheFlagKills(byte targetId, int whichplayer) {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (player.PlayerId == targetId) {
                    switch (whichplayer) {
                        case 1:
                            CaptureTheFlag.redplayer01.MurderPlayer(player);
                            break;
                        case 2:
                            CaptureTheFlag.redplayer02.MurderPlayer(player);
                            break;
                        case 3:
                            CaptureTheFlag.redplayer03.MurderPlayer(player);
                            break;
                        case 4:
                            CaptureTheFlag.redplayer04.MurderPlayer(player);
                            break;
                        case 5:
                            CaptureTheFlag.redplayer05.MurderPlayer(player);
                            break;
                        case 6:
                            CaptureTheFlag.redplayer06.MurderPlayer(player);
                            break;
                        case 7:
                            CaptureTheFlag.redplayer07.MurderPlayer(player);
                            break;
                        case 9:
                            CaptureTheFlag.blueplayer01.MurderPlayer(player);
                            break;
                        case 10:
                            CaptureTheFlag.blueplayer02.MurderPlayer(player);
                            break;
                        case 11:
                            CaptureTheFlag.blueplayer03.MurderPlayer(player);
                            break;
                        case 12:
                            CaptureTheFlag.blueplayer04.MurderPlayer(player);
                            break;
                        case 13:
                            CaptureTheFlag.blueplayer05.MurderPlayer(player);
                            break;
                        case 14:
                            CaptureTheFlag.blueplayer06.MurderPlayer(player);
                            break;
                        case 15:
                            CaptureTheFlag.blueplayer07.MurderPlayer(player);
                            break;
                        case 16:
                            CaptureTheFlag.blueplayer08.MurderPlayer(player);
                            break;
                    }
                    return;
                }
            }
        }

        public static void captureTheFlagWhoTookTheFlag(byte playerWhoStoleTheFlag, int redorblue) {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                // Red team steal blue flag
                if (player.PlayerId == playerWhoStoleTheFlag && redorblue == 1) {
                    CaptureTheFlag.blueflagtaken = true;
                    CaptureTheFlag.blueteamAlerted = false;
                    CaptureTheFlag.redPlayerWhoHasBlueFlag = player;
                    CaptureTheFlag.blueflag.transform.position = player.transform.position + new Vector3(0f, 0f, 0.5f);
                }

                // Alert red team players
                if (CaptureTheFlag.redflagtaken && !CaptureTheFlag.redteamAlerted) {
                    CaptureTheFlag.redteamAlerted = true;
                    foreach (PlayerControl redplayer in CaptureTheFlag.redteamFlag) {
                        if (redplayer == PlayerControl.LocalPlayer && redplayer != null) {
                            new CustomMessage("Your flag has been stolen!", 5, -1, 1f, 4);
                        }
                    }
                }

                // Blue team steal red flag
                if (player.PlayerId == playerWhoStoleTheFlag && redorblue == 2) {
                    CaptureTheFlag.redflagtaken = true;
                    CaptureTheFlag.redteamAlerted = false;
                    CaptureTheFlag.bluePlayerWhoHasRedFlag = player;
                    CaptureTheFlag.redflag.transform.position = player.transform.position + new Vector3(0f, 0f, 0.5f);
                }

                // Alert blue team players
                if (CaptureTheFlag.blueflagtaken && !CaptureTheFlag.blueteamAlerted) {
                    CaptureTheFlag.blueteamAlerted = true;
                    foreach (PlayerControl blueplayer in CaptureTheFlag.blueteamFlag) {
                        if (blueplayer == PlayerControl.LocalPlayer && blueplayer != null) {
                            new CustomMessage("Your flag has been stolen!", 5, -1, 1f, 4);
                        }
                    }
                }
            }
        }

        public static void captureTheFlagWhichTeamScored(int whichteam) {
            // Red team
            if (whichteam == 1) {
                CaptureTheFlag.blueflagtaken = false;
                CaptureTheFlag.redPlayerWhoHasBlueFlag = null;
                switch (PlayerControl.GameOptions.MapId) {
                    // Skeld
                    case 0:
                        if (activatedSensei) {
                            CaptureTheFlag.blueflag.transform.position = new Vector3(7.7f, -1.15f, 1f);
                        }
                        else {
                            CaptureTheFlag.blueflag.transform.position = new Vector3(16.5f, -4.65f, 1f);
                        }
                        break;
                    // MiraHQ
                    case 1:
                        CaptureTheFlag.blueflag.transform.position = new Vector3(23.25f, 5.05f, 1f);
                        break;
                    // Polus
                    case 2:
                        CaptureTheFlag.blueflag.transform.position = new Vector3(5.4f, -9.65f, 0.5f);
                        break;
                    // Dleks
                    case 3:
                        CaptureTheFlag.blueflag.transform.position = new Vector3(-16.5f, -4.65f, 1f);
                        break;
                    // Airship
                    case 4:
                        CaptureTheFlag.blueflag.transform.position = new Vector3(33.6f, 1.25f, 0.5f);
                        break;
                }
                CaptureTheFlag.currentRedTeamPoints += 1;
                new CustomMessage("<color=#FF0000FF>Red Team</color> scored!", 5, -1, 1.6f, 4);
                CaptureTheFlag.flagpointCounter = "Score: " + "<color=#FF0000FF>" + CaptureTheFlag.currentRedTeamPoints + "</color> - " + "<color=#0000FFFF>" + CaptureTheFlag.currentBlueTeamPoints + "</color>";
                if (CaptureTheFlag.currentRedTeamPoints >= CaptureTheFlag.requiredFlags) {
                    CaptureTheFlag.triggerRedTeamWin = true;
                    ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.RedTeamFlagWin, false);
                }
            }

            // Blue team
            if (whichteam == 2) {
                CaptureTheFlag.redflagtaken = false;
                CaptureTheFlag.bluePlayerWhoHasRedFlag = null;
                switch (PlayerControl.GameOptions.MapId) {
                    // Skeld
                    case 0:
                        if (activatedSensei) {
                            CaptureTheFlag.redflag.transform.position = new Vector3(-17.5f, -1.35f, 1f);
                        }
                        else {
                            CaptureTheFlag.redflag.transform.position = new Vector3(-20.5f, -5.35f, 1f);
                        }
                        break;
                    // MiraHQ
                    case 1:
                        CaptureTheFlag.redflag.transform.position = new Vector3(2.525f, 10.55f, 1f);
                        break;
                    // Polus
                    case 2:
                        CaptureTheFlag.redflag.transform.position = new Vector3(36.4f, -21.7f, 0.5f);
                        break;
                    // Dleks
                    case 3:
                        CaptureTheFlag.redflag.transform.position = new Vector3(20.5f, -5.35f, 1f);
                        break;
                    // Airship
                    case 4:
                        CaptureTheFlag.redflag.transform.position = new Vector3(-17.5f, -1.2f, 0.5f);
                        break;
                }
                CaptureTheFlag.currentBlueTeamPoints += 1;
                new CustomMessage("<color=#0000FFFF>Blue Team</color> scored!", 5, -1, 1.3f, 4);
                CaptureTheFlag.flagpointCounter = "Score: " + "<color=#FF0000FF>" + CaptureTheFlag.currentRedTeamPoints + "</color> - " + "<color=#0000FFFF>" + CaptureTheFlag.currentBlueTeamPoints + "</color>";
                if (CaptureTheFlag.currentBlueTeamPoints >= CaptureTheFlag.requiredFlags) {
                    CaptureTheFlag.triggerBlueTeamWin = true;
                    ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.BlueTeamFlagWin, false);
                }
            }
        }

        public static void policeandThiefKills(byte targetId, int whichplayer) {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (player.PlayerId == targetId) {
                    switch (whichplayer) {
                        case 1:
                            PoliceAndThief.policeplayer01.MurderPlayer(player);
                            break;
                        case 2:
                            PoliceAndThief.policeplayer02.MurderPlayer(player);
                            break;
                        case 3:
                            PoliceAndThief.policeplayer03.MurderPlayer(player);
                            break;
                        case 4:
                            PoliceAndThief.policeplayer04.MurderPlayer(player);
                            break;
                        case 5:
                            PoliceAndThief.policeplayer05.MurderPlayer(player);
                            break;
                        case 7:
                            PoliceAndThief.thiefplayer01.MurderPlayer(player);
                            break;
                        case 8:
                            PoliceAndThief.thiefplayer02.MurderPlayer(player);
                            break;
                        case 9:
                            PoliceAndThief.thiefplayer03.MurderPlayer(player);
                            break;
                        case 10:
                            PoliceAndThief.thiefplayer04.MurderPlayer(player);
                            break;
                        case 11:
                            PoliceAndThief.thiefplayer05.MurderPlayer(player);
                            break;
                        case 12:
                            PoliceAndThief.thiefplayer06.MurderPlayer(player);
                            break;
                        case 13:
                            PoliceAndThief.thiefplayer07.MurderPlayer(player);
                            break;
                        case 14:
                            PoliceAndThief.thiefplayer08.MurderPlayer(player);
                            break;
                        case 15:
                            PoliceAndThief.thiefplayer09.MurderPlayer(player);
                            break;
                        case 16:
                            PoliceAndThief.thiefplayer10.MurderPlayer(player);
                            break;
                    }
                    return;
                }
            }
        }

        public static void policeandThiefJail(byte thiefId) {
            foreach (PlayerControl player in PoliceAndThief.thiefTeam) {
                if (player.PlayerId == thiefId) {
                    if (player.inVent) {
                        foreach (Vent vent in ShipStatus.Instance.AllVents) {
                            bool canUse;
                            bool couldUse;
                            vent.CanUse(player.Data, out canUse, out couldUse);
                            if (canUse) {
                                player.MyPhysics.RpcExitVent(vent.Id);
                                vent.SetButtons(false);
                            }
                        }
                    }
                    PoliceAndThief.thiefArrested.Add(player);
                    if (PoliceAndThief.thiefplayer01 != null && thiefId == PoliceAndThief.thiefplayer01.PlayerId) {
                        if (PoliceAndThief.thiefplayer01IsStealing) {
                            policeandThiefRevertedJewelPosition(thiefId, PoliceAndThief.thiefplayer01JewelId);
                        }
                    }
                    else if (PoliceAndThief.thiefplayer02 != null && thiefId == PoliceAndThief.thiefplayer02.PlayerId) {
                        if (PoliceAndThief.thiefplayer02IsStealing) {
                            policeandThiefRevertedJewelPosition(thiefId, PoliceAndThief.thiefplayer02JewelId);
                        }
                    }
                    else if (PoliceAndThief.thiefplayer03 != null && thiefId == PoliceAndThief.thiefplayer03.PlayerId) {
                        if (PoliceAndThief.thiefplayer03IsStealing) {
                            policeandThiefRevertedJewelPosition(thiefId, PoliceAndThief.thiefplayer03JewelId);
                        }
                    }
                    else if (PoliceAndThief.thiefplayer04 != null && thiefId == PoliceAndThief.thiefplayer04.PlayerId) {
                        if (PoliceAndThief.thiefplayer04IsStealing) {
                            policeandThiefRevertedJewelPosition(thiefId, PoliceAndThief.thiefplayer04JewelId);
                        }
                    }
                    else if (PoliceAndThief.thiefplayer05 != null && thiefId == PoliceAndThief.thiefplayer05.PlayerId) {
                        if (PoliceAndThief.thiefplayer05IsStealing) {
                            policeandThiefRevertedJewelPosition(thiefId, PoliceAndThief.thiefplayer05JewelId);
                        }
                    }
                    else if (PoliceAndThief.thiefplayer06 != null && thiefId == PoliceAndThief.thiefplayer06.PlayerId) {
                        if (PoliceAndThief.thiefplayer06IsStealing) {
                            policeandThiefRevertedJewelPosition(thiefId, PoliceAndThief.thiefplayer06JewelId);
                        }
                    }
                    else if (PoliceAndThief.thiefplayer07 != null && thiefId == PoliceAndThief.thiefplayer07.PlayerId) {
                        if (PoliceAndThief.thiefplayer07IsStealing) {
                            policeandThiefRevertedJewelPosition(thiefId, PoliceAndThief.thiefplayer07JewelId);
                        }
                    }
                    else if (PoliceAndThief.thiefplayer08 != null && thiefId == PoliceAndThief.thiefplayer08.PlayerId) {
                        if (PoliceAndThief.thiefplayer08IsStealing) {
                            policeandThiefRevertedJewelPosition(thiefId, PoliceAndThief.thiefplayer08JewelId);
                        }
                    }
                    else if (PoliceAndThief.thiefplayer09 != null && thiefId == PoliceAndThief.thiefplayer09.PlayerId) {
                        if (PoliceAndThief.thiefplayer09IsStealing) {
                            policeandThiefRevertedJewelPosition(thiefId, PoliceAndThief.thiefplayer09JewelId);
                        }
                    }
                    else if (PoliceAndThief.thiefplayer10 != null && thiefId == PoliceAndThief.thiefplayer10.PlayerId) {
                        if (PoliceAndThief.thiefplayer10IsStealing) {
                            policeandThiefRevertedJewelPosition(thiefId, PoliceAndThief.thiefplayer10JewelId);
                        }
                    }
                    switch (PlayerControl.GameOptions.MapId) {
                        // Skeld
                        case 0:
                            if (activatedSensei) {
                                player.transform.position = new Vector3(-12f, 7.15f, player.transform.position.z);
                            }
                            else {
                                player.transform.position = new Vector3(-10.2f, 3.6f, player.transform.position.z);
                            }
                            break;
                        // MiraHQ
                        case 1:
                            player.transform.position = new Vector3(1.8f, 1.25f, player.transform.position.z);
                            break;
                        // Polus
                        case 2:
                            player.transform.position = new Vector3(8.25f, -5f, player.transform.position.z);
                            break;
                        // Dleks
                        case 3:
                            player.transform.position = new Vector3(10.2f, 3.6f, player.transform.position.z);
                            break;
                        // Airship
                        case 4:
                            player.transform.position = new Vector3(-18.5f, 3.5f, player.transform.position.z);
                            break;
                    }
                    PoliceAndThief.currentThiefsCaptured += 1;
                    new CustomMessage("A <color=#928B55FF>Thief</color> has been captured!", 5, -1, 1.3f, 7);
                    PoliceAndThief.thiefpointCounter = "Stolen Jewels: " + "<color=#00F7FFFF>" + PoliceAndThief.currentJewelsStoled + "/" + PoliceAndThief.requiredJewels + "</color> | " + "Captured Thiefs: " + "<color=#928B55FF>" + PoliceAndThief.currentThiefsCaptured + "/" + PoliceAndThief.thiefTeam.Count + "</color>";
                    if (PoliceAndThief.currentThiefsCaptured == PoliceAndThief.thiefTeam.Count) {
                        PoliceAndThief.triggerPoliceWin = true;
                        ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.ThiefModePoliceWin, false);
                    }
                }
            }

        }

        public static void policeandThiefFreeThief() {
            switch (PlayerControl.GameOptions.MapId) {
                // Skeld
                case 0:
                    if (activatedSensei) {
                        PoliceAndThief.thiefArrested[0].transform.position = new Vector3(13.75f, -0.2f, PoliceAndThief.thiefArrested[0].transform.position.z);
                    }
                    else {
                        PoliceAndThief.thiefArrested[0].transform.position = new Vector3(-1.31f, -16.25f, PoliceAndThief.thiefArrested[0].transform.position.z);
                    }
                    break;
                // MiraHQ
                case 1:
                    PoliceAndThief.thiefArrested[0].transform.position = new Vector3(17.75f, 11.5f, PoliceAndThief.thiefArrested[0].transform.position.z);
                    break;
                // Polus
                case 2:
                    PoliceAndThief.thiefArrested[0].transform.position = new Vector3(30f, -15.75f, PoliceAndThief.thiefArrested[0].transform.position.z);
                    break;
                // Dleks
                case 3:
                    PoliceAndThief.thiefArrested[0].transform.position = new Vector3(1.31f, -16.25f, PoliceAndThief.thiefArrested[0].transform.position.z);
                    break;
                // Airship
                case 4:
                    PoliceAndThief.thiefArrested[0].transform.position = new Vector3(7.15f, -14.5f, PoliceAndThief.thiefArrested[0].transform.position.z);
                    break;
            }
            PoliceAndThief.thiefArrested.RemoveAt(0);
            PoliceAndThief.currentThiefsCaptured = PoliceAndThief.currentThiefsCaptured - 1;
            new CustomMessage("A <color=#928B55FF>Thief</color> has been released!", 5, -1, 1f, 7);
            PoliceAndThief.thiefpointCounter = "Stolen Jewels: " + "<color=#00F7FFFF>" + PoliceAndThief.currentJewelsStoled + "/" + PoliceAndThief.requiredJewels + "</color> | " + "Captured Thiefs: " + "<color=#928B55FF>" + PoliceAndThief.currentThiefsCaptured + "/" + PoliceAndThief.thiefTeam.Count + "</color>";
        }

        public static void policeandThiefTakeJewel(byte thiefWhoTookATreasure, byte jewelId) {
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                // Thief player steal a jewel
                if (player.PlayerId == thiefWhoTookATreasure) {
                    if (PoliceAndThief.thiefplayer01 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer01.PlayerId) {
                        PoliceAndThief.thiefplayer01IsStealing = true;
                        PoliceAndThief.thiefplayer01JewelId = jewelId;
                    }
                    else if (PoliceAndThief.thiefplayer02 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer02.PlayerId) {
                        PoliceAndThief.thiefplayer02IsStealing = true;
                        PoliceAndThief.thiefplayer02JewelId = jewelId;
                    }
                    else if (PoliceAndThief.thiefplayer03 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer03.PlayerId) {
                        PoliceAndThief.thiefplayer03IsStealing = true;
                        PoliceAndThief.thiefplayer03JewelId = jewelId;
                    }
                    else if (PoliceAndThief.thiefplayer04 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer04.PlayerId) {
                        PoliceAndThief.thiefplayer04IsStealing = true;
                        PoliceAndThief.thiefplayer04JewelId = jewelId;
                    }
                    else if (PoliceAndThief.thiefplayer05 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer05.PlayerId) {
                        PoliceAndThief.thiefplayer05IsStealing = true;
                        PoliceAndThief.thiefplayer05JewelId = jewelId;
                    }
                    else if (PoliceAndThief.thiefplayer06 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer06.PlayerId) {
                        PoliceAndThief.thiefplayer06IsStealing = true;
                        PoliceAndThief.thiefplayer06JewelId = jewelId;
                    }
                    else if (PoliceAndThief.thiefplayer07 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer07.PlayerId) {
                        PoliceAndThief.thiefplayer07IsStealing = true;
                        PoliceAndThief.thiefplayer07JewelId = jewelId;
                    }
                    else if (PoliceAndThief.thiefplayer08 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer08.PlayerId) {
                        PoliceAndThief.thiefplayer08IsStealing = true;
                        PoliceAndThief.thiefplayer08JewelId = jewelId;
                    }
                    else if (PoliceAndThief.thiefplayer09 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer09.PlayerId) {
                        PoliceAndThief.thiefplayer09IsStealing = true;
                        PoliceAndThief.thiefplayer09JewelId = jewelId;
                    }
                    else if (PoliceAndThief.thiefplayer10 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer10.PlayerId) {
                        PoliceAndThief.thiefplayer10IsStealing = true;
                        PoliceAndThief.thiefplayer10JewelId = jewelId;
                    }
                    switch (jewelId) {
                        case 1:
                            PoliceAndThief.jewel01BeingStealed = player;
                            PoliceAndThief.jewel01.transform.position = player.transform.position + new Vector3(0, 0.5f, -0.25f);
                            PoliceAndThief.jewel01.SetActive(true);
                            break;
                        case 2:
                            PoliceAndThief.jewel02BeingStealed = player;
                            PoliceAndThief.jewel02.transform.position = player.transform.position + new Vector3(0, 0.5f, -0.25f);
                            PoliceAndThief.jewel02.SetActive(true);
                            break;
                        case 3:
                            PoliceAndThief.jewel03BeingStealed = player;
                            PoliceAndThief.jewel03.transform.position = player.transform.position + new Vector3(0, 0.5f, -0.25f);
                            PoliceAndThief.jewel03.SetActive(true);
                            break;
                        case 4:
                            PoliceAndThief.jewel04BeingStealed = player;
                            PoliceAndThief.jewel04.transform.position = player.transform.position + new Vector3(0, 0.5f, -0.25f);
                            PoliceAndThief.jewel04.SetActive(true);
                            break;
                        case 5:
                            PoliceAndThief.jewel05BeingStealed = player;
                            PoliceAndThief.jewel05.transform.position = player.transform.position + new Vector3(0, 0.5f, -0.25f);
                            PoliceAndThief.jewel05.SetActive(true);
                            break;
                        case 6:
                            PoliceAndThief.jewel06BeingStealed = player;
                            PoliceAndThief.jewel06.transform.position = player.transform.position + new Vector3(0, 0.5f, -0.25f);
                            PoliceAndThief.jewel06.SetActive(true);
                            break;
                        case 7:
                            PoliceAndThief.jewel07BeingStealed = player;
                            PoliceAndThief.jewel07.transform.position = player.transform.position + new Vector3(0, 0.5f, -0.25f);
                            PoliceAndThief.jewel07.SetActive(true);
                            break;
                        case 8:
                            PoliceAndThief.jewel08BeingStealed = player;
                            PoliceAndThief.jewel08.transform.position = player.transform.position + new Vector3(0, 0.5f, -0.25f);
                            PoliceAndThief.jewel08.SetActive(true);
                            break;
                        case 9:
                            PoliceAndThief.jewel09BeingStealed = player;
                            PoliceAndThief.jewel09.transform.position = player.transform.position + new Vector3(0, 0.5f, -0.25f);
                            PoliceAndThief.jewel09.SetActive(true);
                            break;
                        case 10:
                            PoliceAndThief.jewel10BeingStealed = player;
                            PoliceAndThief.jewel10.transform.position = player.transform.position + new Vector3(0, 0.5f, -0.25f);
                            PoliceAndThief.jewel10.SetActive(true);
                            break;
                        case 11:
                            PoliceAndThief.jewel11BeingStealed = player;
                            PoliceAndThief.jewel11.transform.position = player.transform.position + new Vector3(0, 0.5f, -0.25f);
                            PoliceAndThief.jewel11.SetActive(true);
                            break;
                        case 12:
                            PoliceAndThief.jewel12BeingStealed = player;
                            PoliceAndThief.jewel12.transform.position = player.transform.position + new Vector3(0, 0.5f, -0.25f);
                            PoliceAndThief.jewel12.SetActive(true);
                            break;
                        case 13:
                            PoliceAndThief.jewel13BeingStealed = player;
                            PoliceAndThief.jewel13.transform.position = player.transform.position + new Vector3(0, 0.5f, -0.25f);
                            PoliceAndThief.jewel13.SetActive(true);
                            break;
                        case 14:
                            PoliceAndThief.jewel14BeingStealed = player;
                            PoliceAndThief.jewel14.transform.position = player.transform.position + new Vector3(0, 0.5f, -0.25f);
                            PoliceAndThief.jewel14.SetActive(true);
                            break;
                        case 15:
                            PoliceAndThief.jewel15BeingStealed = player;
                            PoliceAndThief.jewel15.transform.position = player.transform.position + new Vector3(0, 0.5f, -0.25f);
                            PoliceAndThief.jewel15.SetActive(true);
                            break;
                    }
                }

            }
        }

        public static void policeandThiefDeliverJewel(byte thiefWhoTookATreasure, byte jewelId) {
            // Red team
            foreach (PlayerControl player in PoliceAndThief.thiefTeam) {
                // Thief player steal a jewel
                if (player.PlayerId == thiefWhoTookATreasure) {
                    if (PoliceAndThief.thiefplayer01 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer01.PlayerId) {
                        PoliceAndThief.thiefplayer01IsStealing = false;
                        PoliceAndThief.thiefplayer01JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer02 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer02.PlayerId) {
                        PoliceAndThief.thiefplayer02IsStealing = false;
                        PoliceAndThief.thiefplayer02JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer03 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer03.PlayerId) {
                        PoliceAndThief.thiefplayer03IsStealing = false;
                        PoliceAndThief.thiefplayer03JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer04 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer04.PlayerId) {
                        PoliceAndThief.thiefplayer04IsStealing = false;
                        PoliceAndThief.thiefplayer04JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer05 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer05.PlayerId) {
                        PoliceAndThief.thiefplayer05IsStealing = false;
                        PoliceAndThief.thiefplayer05JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer06 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer06.PlayerId) {
                        PoliceAndThief.thiefplayer06IsStealing = false;
                        PoliceAndThief.thiefplayer06JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer07 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer07.PlayerId) {
                        PoliceAndThief.thiefplayer07IsStealing = false;
                        PoliceAndThief.thiefplayer07JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer08 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer08.PlayerId) {
                        PoliceAndThief.thiefplayer08IsStealing = false;
                        PoliceAndThief.thiefplayer08JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer09 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer09.PlayerId) {
                        PoliceAndThief.thiefplayer09IsStealing = false;
                        PoliceAndThief.thiefplayer09JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer10 != null && thiefWhoTookATreasure == PoliceAndThief.thiefplayer10.PlayerId) {
                        PoliceAndThief.thiefplayer10IsStealing = false;
                        PoliceAndThief.thiefplayer10JewelId = 0;
                    }
                    GameObject myJewel = null;
                    bool isDiamond = true;
                    switch (jewelId) {
                        case 1:
                            myJewel = PoliceAndThief.jewel01;
                            PoliceAndThief.jewel01BeingStealed = null;
                            break;
                        case 2:
                            myJewel = PoliceAndThief.jewel02;
                            PoliceAndThief.jewel02BeingStealed = null;
                            break;
                        case 3:
                            myJewel = PoliceAndThief.jewel03;
                            PoliceAndThief.jewel03BeingStealed = null;
                            break;
                        case 4:
                            myJewel = PoliceAndThief.jewel04;
                            PoliceAndThief.jewel04BeingStealed = null;
                            break;
                        case 5:
                            myJewel = PoliceAndThief.jewel05;
                            PoliceAndThief.jewel05BeingStealed = null;
                            break;
                        case 6:
                            myJewel = PoliceAndThief.jewel06;
                            PoliceAndThief.jewel06BeingStealed = null;
                            break;
                        case 7:
                            myJewel = PoliceAndThief.jewel07;
                            PoliceAndThief.jewel07BeingStealed = null;
                            break;
                        case 8:
                            myJewel = PoliceAndThief.jewel08;
                            PoliceAndThief.jewel08BeingStealed = null;
                            break;
                        case 9:
                            myJewel = PoliceAndThief.jewel09;
                            PoliceAndThief.jewel09BeingStealed = null;
                            isDiamond = !isDiamond;
                            break;
                        case 10:
                            myJewel = PoliceAndThief.jewel10;
                            PoliceAndThief.jewel10BeingStealed = null;
                            isDiamond = !isDiamond;
                            break;
                        case 11:
                            myJewel = PoliceAndThief.jewel11;
                            PoliceAndThief.jewel11BeingStealed = null;
                            isDiamond = !isDiamond;
                            break;
                        case 12:
                            myJewel = PoliceAndThief.jewel12;
                            PoliceAndThief.jewel12BeingStealed = null;
                            isDiamond = !isDiamond;
                            break;
                        case 13:
                            myJewel = PoliceAndThief.jewel13;
                            PoliceAndThief.jewel13BeingStealed = null;
                            isDiamond = !isDiamond;
                            break;
                        case 14:
                            myJewel = PoliceAndThief.jewel14;
                            PoliceAndThief.jewel14BeingStealed = null;
                            isDiamond = !isDiamond;
                            break;
                        case 15:
                            myJewel = PoliceAndThief.jewel15;
                            PoliceAndThief.jewel15BeingStealed = null;
                            isDiamond = !isDiamond;
                            break;
                    }
                    switch (PlayerControl.GameOptions.MapId) {
                        // Skeld
                        case 0:
                            if (activatedSensei) {
                                if (isDiamond) {
                                    myJewel.transform.position = new Vector3(15.25f, -0.33f, player.transform.position.z);
                                }
                                else {
                                    myJewel.transform.position = new Vector3(15.7f, -0.33f, player.transform.position.z);
                                }
                            }
                            else {
                                if (isDiamond) {

                                    myJewel.transform.position = new Vector3(0f, -19.4f, player.transform.position.z);
                                }
                                else {
                                    myJewel.transform.position = new Vector3(0.4f, -19.4f, player.transform.position.z);
                                }
                            }
                            break;
                        // MiraHQ
                        case 1:
                            if (isDiamond) {
                                myJewel.transform.position = new Vector3(19.65f, 13.9f, player.transform.position.z);
                            }
                            else {
                                myJewel.transform.position = new Vector3(20.075f, 13.9f, player.transform.position.z);
                            }
                            break;
                        // Polus
                        case 2:
                            if (isDiamond) {
                                myJewel.transform.position = new Vector3(33.6f, 13.9f, player.transform.position.z);
                            }
                            else {
                                myJewel.transform.position = new Vector3(34.05f, -15.9f, player.transform.position.z);
                            }
                            break;
                        // Dleks
                        case 3:
                            if (isDiamond) {
                                myJewel.transform.position = new Vector3(0f, -19.4f, player.transform.position.z);
                            }
                            else {
                                myJewel.transform.position = new Vector3(-0.4f, -19.4f, player.transform.position.z);
                            }
                            break;
                        // Airship
                        case 4:
                            if (isDiamond) {
                                myJewel.transform.position = new Vector3(11.75f, -16.35f, player.transform.position.z);
                            }
                            else {
                                myJewel.transform.position = new Vector3(12.2f, -16.35f, player.transform.position.z);
                            }
                            break;
                    }
                }

            }
            PoliceAndThief.currentJewelsStoled += 1;
            new CustomMessage("A <color=#00F7FFFF>Jewel</color> has been delivered!", 5, -1, 1.6f, 7);
            PoliceAndThief.thiefpointCounter = "Stolen Jewels: " + "<color=#00F7FFFF>" + PoliceAndThief.currentJewelsStoled + "/" + PoliceAndThief.requiredJewels + "</color> | " + "Captured Thiefs: " + "<color=#928B55FF>" + PoliceAndThief.currentThiefsCaptured + "/" + PoliceAndThief.thiefTeam.Count + "</color>";
            if (PoliceAndThief.currentJewelsStoled >= PoliceAndThief.requiredJewels) {
                PoliceAndThief.triggerThiefWin = true;
                ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.ThiefModeThiefWin, false);
            }
        }

        public static void policeandThiefRevertedJewelPosition(byte thiefWhoLostJewel, byte jewelRevertedId) {
            foreach (PlayerControl player in PoliceAndThief.thiefTeam) {
                if (player.PlayerId == thiefWhoLostJewel) {
                    if (PoliceAndThief.thiefplayer01 != null && thiefWhoLostJewel == PoliceAndThief.thiefplayer01.PlayerId) {
                        PoliceAndThief.thiefplayer01IsStealing = false;
                        PoliceAndThief.thiefplayer01JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer02 != null && thiefWhoLostJewel == PoliceAndThief.thiefplayer02.PlayerId) {
                        PoliceAndThief.thiefplayer02IsStealing = false;
                        PoliceAndThief.thiefplayer02JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer03 != null && thiefWhoLostJewel == PoliceAndThief.thiefplayer03.PlayerId) {
                        PoliceAndThief.thiefplayer03IsStealing = false;
                        PoliceAndThief.thiefplayer03JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer04 != null && thiefWhoLostJewel == PoliceAndThief.thiefplayer04.PlayerId) {
                        PoliceAndThief.thiefplayer04IsStealing = false;
                        PoliceAndThief.thiefplayer04JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer05 != null && thiefWhoLostJewel == PoliceAndThief.thiefplayer05.PlayerId) {
                        PoliceAndThief.thiefplayer05IsStealing = false;
                        PoliceAndThief.thiefplayer05JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer06 != null && thiefWhoLostJewel == PoliceAndThief.thiefplayer06.PlayerId) {
                        PoliceAndThief.thiefplayer06IsStealing = false;
                        PoliceAndThief.thiefplayer06JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer07 != null && thiefWhoLostJewel == PoliceAndThief.thiefplayer07.PlayerId) {
                        PoliceAndThief.thiefplayer07IsStealing = false;
                        PoliceAndThief.thiefplayer07JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer08 != null && thiefWhoLostJewel == PoliceAndThief.thiefplayer08.PlayerId) {
                        PoliceAndThief.thiefplayer08IsStealing = false;
                        PoliceAndThief.thiefplayer08JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer09 != null && thiefWhoLostJewel == PoliceAndThief.thiefplayer09.PlayerId) {
                        PoliceAndThief.thiefplayer09IsStealing = false;
                        PoliceAndThief.thiefplayer09JewelId = 0;
                    }
                    else if (PoliceAndThief.thiefplayer10 != null && thiefWhoLostJewel == PoliceAndThief.thiefplayer10.PlayerId) {
                        PoliceAndThief.thiefplayer10IsStealing = false;
                        PoliceAndThief.thiefplayer10JewelId = 0;
                    }
                    switch (PlayerControl.GameOptions.MapId) {
                        // Skeld
                        case 0:
                            if (activatedSensei) {
                                switch (jewelRevertedId) {
                                    case 1:
                                        PoliceAndThief.jewel01.transform.position = new Vector3(6.95f, 4.95f, 1f);
                                        PoliceAndThief.jewel01BeingStealed = null;
                                        break;
                                    case 2:
                                        PoliceAndThief.jewel02.transform.position = new Vector3(-3.75f, 5.35f, 1f);
                                        PoliceAndThief.jewel02BeingStealed = null;
                                        break;
                                    case 3:
                                        PoliceAndThief.jewel03.transform.position = new Vector3(-7.7f, 11.3f, 1f);
                                        PoliceAndThief.jewel03BeingStealed = null;
                                        break;
                                    case 4:
                                        PoliceAndThief.jewel04.transform.position = new Vector3(-19.65f, 5.3f, 1f);
                                        PoliceAndThief.jewel04BeingStealed = null;
                                        break;
                                    case 5:
                                        PoliceAndThief.jewel05.transform.position = new Vector3(-19.65f, -8, 1f);
                                        PoliceAndThief.jewel05BeingStealed = null;
                                        break;
                                    case 6:
                                        PoliceAndThief.jewel06.transform.position = new Vector3(-5.45f, -13f, 1f);
                                        PoliceAndThief.jewel06BeingStealed = null;
                                        break;
                                    case 7:
                                        PoliceAndThief.jewel07.transform.position = new Vector3(-7.65f, -4.2f, 1f);
                                        PoliceAndThief.jewel07BeingStealed = null;
                                        break;
                                    case 8:
                                        PoliceAndThief.jewel08.transform.position = new Vector3(2f, -6.75f, 1f);
                                        PoliceAndThief.jewel08BeingStealed = null;
                                        break;
                                    case 9:
                                        PoliceAndThief.jewel09.transform.position = new Vector3(8.9f, 1.45f, 1f);
                                        PoliceAndThief.jewel09BeingStealed = null;
                                        break;
                                    case 10:
                                        PoliceAndThief.jewel10.transform.position = new Vector3(4.6f, -2.25f, 1f);
                                        PoliceAndThief.jewel10BeingStealed = null;
                                        break;
                                    case 11:
                                        PoliceAndThief.jewel11.transform.position = new Vector3(-5.05f, -0.88f, 1f);
                                        PoliceAndThief.jewel11BeingStealed = null;
                                        break;
                                    case 12:
                                        PoliceAndThief.jewel12.transform.position = new Vector3(-8.25f, -0.45f, 1f);
                                        PoliceAndThief.jewel12BeingStealed = null;
                                        break;
                                    case 13:
                                        PoliceAndThief.jewel13.transform.position = new Vector3(-19.75f, -1.55f, 1f);
                                        PoliceAndThief.jewel13BeingStealed = null;
                                        break;
                                    case 14:
                                        PoliceAndThief.jewel14.transform.position = new Vector3(-12.1f, -13.15f, 1f);
                                        PoliceAndThief.jewel14BeingStealed = null;
                                        break;
                                    case 15:
                                        PoliceAndThief.jewel15.transform.position = new Vector3(7.15f, -14.45f, 1f);
                                        PoliceAndThief.jewel15BeingStealed = null;
                                        break;
                                }
                            }
                            else {
                                switch (jewelRevertedId) {
                                    case 1:
                                        PoliceAndThief.jewel01.transform.position = new Vector3(-18.65f, -9.9f, 1f);
                                        PoliceAndThief.jewel01BeingStealed = null;
                                        break;
                                    case 2:
                                        PoliceAndThief.jewel02.transform.position = new Vector3(-21.5f, -2, 1f);
                                        PoliceAndThief.jewel02BeingStealed = null;
                                        break;
                                    case 3:
                                        PoliceAndThief.jewel03.transform.position = new Vector3(-5.9f, -8.25f, 1f);
                                        PoliceAndThief.jewel03BeingStealed = null;
                                        break;
                                    case 4:
                                        PoliceAndThief.jewel04.transform.position = new Vector3(4.5f, -7.5f, 1f);
                                        PoliceAndThief.jewel04BeingStealed = null;
                                        break;
                                    case 5:
                                        PoliceAndThief.jewel05.transform.position = new Vector3(7.85f, -14.45f, 1f);
                                        PoliceAndThief.jewel05BeingStealed = null;
                                        break;
                                    case 6:
                                        PoliceAndThief.jewel06.transform.position = new Vector3(6.65f, -4.8f, 1f);
                                        PoliceAndThief.jewel06BeingStealed = null;
                                        break;
                                    case 7:
                                        PoliceAndThief.jewel07.transform.position = new Vector3(10.5f, 2.15f, 1f);
                                        PoliceAndThief.jewel07BeingStealed = null;
                                        break;
                                    case 8:
                                        PoliceAndThief.jewel08.transform.position = new Vector3(-5.5f, 3.5f, 1f);
                                        PoliceAndThief.jewel08BeingStealed = null;
                                        break;
                                    case 9:
                                        PoliceAndThief.jewel09.transform.position = new Vector3(-19, -1.2f, 1f);
                                        PoliceAndThief.jewel09BeingStealed = null;
                                        break;
                                    case 10:
                                        PoliceAndThief.jewel10.transform.position = new Vector3(-21.5f, -8.35f, 1f);
                                        PoliceAndThief.jewel10BeingStealed = null;
                                        break;
                                    case 11:
                                        PoliceAndThief.jewel11.transform.position = new Vector3(-12.5f, -3.75f, 1f);
                                        PoliceAndThief.jewel11BeingStealed = null;
                                        break;
                                    case 12:
                                        PoliceAndThief.jewel12.transform.position = new Vector3(-5.9f, -5.25f, 1f);
                                        PoliceAndThief.jewel12BeingStealed = null;
                                        break;
                                    case 13:
                                        PoliceAndThief.jewel13.transform.position = new Vector3(2.65f, -16.5f, 1f);
                                        PoliceAndThief.jewel13BeingStealed = null;
                                        break;
                                    case 14:
                                        PoliceAndThief.jewel14.transform.position = new Vector3(16.75f, -4.75f, 1f);
                                        PoliceAndThief.jewel14BeingStealed = null;
                                        break;
                                    case 15:
                                        PoliceAndThief.jewel15.transform.position = new Vector3(3.8f, 3.5f, 1f);
                                        PoliceAndThief.jewel15BeingStealed = null;
                                        break;
                                }
                            }
                            break;
                        // MiraHQ
                        case 1:
                            switch (jewelRevertedId) {
                                case 1:
                                    PoliceAndThief.jewel01.transform.position = new Vector3(-4.5f, 2.5f, 1f);
                                    PoliceAndThief.jewel01BeingStealed = null;
                                    break;
                                case 2:
                                    PoliceAndThief.jewel02.transform.position = new Vector3(6.25f, 14f, 1f);
                                    PoliceAndThief.jewel02BeingStealed = null;
                                    break;
                                case 3:
                                    PoliceAndThief.jewel03.transform.position = new Vector3(9.15f, 4.75f, 1f);
                                    PoliceAndThief.jewel03BeingStealed = null;
                                    break;
                                case 4:
                                    PoliceAndThief.jewel04.transform.position = new Vector3(14.75f, 20.5f, 1f);
                                    PoliceAndThief.jewel04BeingStealed = null;
                                    break;
                                case 5:
                                    PoliceAndThief.jewel05.transform.position = new Vector3(19.5f, 17.5f, 1f);
                                    PoliceAndThief.jewel05BeingStealed = null;
                                    break;
                                case 6:
                                    PoliceAndThief.jewel06.transform.position = new Vector3(21, 24.1f, 1f);
                                    PoliceAndThief.jewel06BeingStealed = null;
                                    break;
                                case 7:
                                    PoliceAndThief.jewel07.transform.position = new Vector3(19.5f, 4.75f, 1f);
                                    PoliceAndThief.jewel07BeingStealed = null;
                                    break;
                                case 8:
                                    PoliceAndThief.jewel08.transform.position = new Vector3(28.25f, 0, 1f);
                                    PoliceAndThief.jewel08BeingStealed = null;
                                    break;
                                case 9:
                                    PoliceAndThief.jewel09.transform.position = new Vector3(2.45f, 11.25f, 1f);
                                    PoliceAndThief.jewel09BeingStealed = null;
                                    break;
                                case 10:
                                    PoliceAndThief.jewel10.transform.position = new Vector3(4.4f, 1.75f, 1f);
                                    PoliceAndThief.jewel10BeingStealed = null;
                                    break;
                                case 11:
                                    PoliceAndThief.jewel11.transform.position = new Vector3(9.25f, 13f, 1f);
                                    PoliceAndThief.jewel11BeingStealed = null;
                                    break;
                                case 12:
                                    PoliceAndThief.jewel12.transform.position = new Vector3(13.75f, 23.5f, 1f);
                                    PoliceAndThief.jewel12BeingStealed = null;
                                    break;
                                case 13:
                                    PoliceAndThief.jewel13.transform.position = new Vector3(16, 4, 1f);
                                    PoliceAndThief.jewel13BeingStealed = null;
                                    break;
                                case 14:
                                    PoliceAndThief.jewel14.transform.position = new Vector3(15.35f, -0.9f, 1f);
                                    PoliceAndThief.jewel14BeingStealed = null;
                                    break;
                                case 15:
                                    PoliceAndThief.jewel15.transform.position = new Vector3(19.5f, -1.75f, 1f);
                                    PoliceAndThief.jewel15BeingStealed = null;
                                    break;
                            }
                            break;
                        // Polus
                        case 2:
                            switch (jewelRevertedId) {
                                case 1:
                                    PoliceAndThief.jewel01.transform.position = new Vector3(16.7f, -2.65f, 0.75f);
                                    PoliceAndThief.jewel01BeingStealed = null;
                                    break;
                                case 2:
                                    PoliceAndThief.jewel02.transform.position = new Vector3(25.35f, -7.35f, 0.75f);
                                    PoliceAndThief.jewel02BeingStealed = null;
                                    break;
                                case 3:
                                    PoliceAndThief.jewel03.transform.position = new Vector3(34.9f, -9.75f, 0.75f);
                                    PoliceAndThief.jewel03BeingStealed = null;
                                    break;
                                case 4:
                                    PoliceAndThief.jewel04.transform.position = new Vector3(36.5f, -21.75f, 0.75f);
                                    PoliceAndThief.jewel04BeingStealed = null;
                                    break;
                                case 5:
                                    PoliceAndThief.jewel05.transform.position = new Vector3(17.25f, -17.5f, 0.75f);
                                    PoliceAndThief.jewel05BeingStealed = null;
                                    break;
                                case 6:
                                    PoliceAndThief.jewel06.transform.position = new Vector3(10.9f, -20.5f, -0.75f);
                                    PoliceAndThief.jewel06BeingStealed = null;
                                    break;
                                case 7:
                                    PoliceAndThief.jewel07.transform.position = new Vector3(1.5f, -20.25f, 0.75f);
                                    PoliceAndThief.jewel07BeingStealed = null;
                                    break;
                                case 08:
                                    PoliceAndThief.jewel08.transform.position = new Vector3(3f, -12f, 0.75f);
                                    PoliceAndThief.jewel08BeingStealed = null;
                                    break;
                                case 09:
                                    PoliceAndThief.jewel09.transform.position = new Vector3(30f, -7.35f, 0.75f);
                                    PoliceAndThief.jewel09BeingStealed = null;
                                    break;
                                case 10:
                                    PoliceAndThief.jewel10.transform.position = new Vector3(40.25f, -8f, 0.75f);
                                    PoliceAndThief.jewel10BeingStealed = null;
                                    break;
                                case 11:
                                    PoliceAndThief.jewel11.transform.position = new Vector3(26f, -17.15f, 0.75f);
                                    PoliceAndThief.jewel11BeingStealed = null;
                                    break;
                                case 12:
                                    PoliceAndThief.jewel12.transform.position = new Vector3(22f, -25.25f, 0.75f);
                                    PoliceAndThief.jewel12BeingStealed = null;
                                    break;
                                case 13:
                                    PoliceAndThief.jewel13.transform.position = new Vector3(20.65f, -12f, 0.75f);
                                    PoliceAndThief.jewel13BeingStealed = null;
                                    break;
                                case 14:
                                    PoliceAndThief.jewel14.transform.position = new Vector3(9.75f, -12.25f, 0.75f);
                                    PoliceAndThief.jewel14BeingStealed = null;
                                    break;
                                case 15:
                                    PoliceAndThief.jewel15.transform.position = new Vector3(2.25f, -24f, 0.75f);
                                    PoliceAndThief.jewel15BeingStealed = null;
                                    break;
                            }
                            break;
                        // Dleks
                        case 3:
                            switch (jewelRevertedId) {
                                case 1:
                                    PoliceAndThief.jewel01.transform.position = new Vector3(18.65f, -9.9f, 1f);
                                    PoliceAndThief.jewel01BeingStealed = null;
                                    break;
                                case 2:
                                    PoliceAndThief.jewel02.transform.position = new Vector3(21.5f, -2, 1f);
                                    PoliceAndThief.jewel02BeingStealed = null;
                                    break;
                                case 3:
                                    PoliceAndThief.jewel03.transform.position = new Vector3(5.9f, -8.25f, 1f);
                                    PoliceAndThief.jewel03BeingStealed = null;
                                    break;
                                case 4:
                                    PoliceAndThief.jewel04.transform.position = new Vector3(-4.5f, -7.5f, 1f);
                                    PoliceAndThief.jewel04BeingStealed = null;
                                    break;
                                case 5:
                                    PoliceAndThief.jewel05.transform.position = new Vector3(-7.85f, -14.45f, 1f);
                                    PoliceAndThief.jewel05BeingStealed = null;
                                    break;
                                case 6:
                                    PoliceAndThief.jewel06.transform.position = new Vector3(-6.65f, -4.8f, 1f);
                                    PoliceAndThief.jewel06BeingStealed = null;
                                    break;
                                case 7:
                                    PoliceAndThief.jewel07.transform.position = new Vector3(-10.5f, 2.15f, 1f);
                                    PoliceAndThief.jewel07BeingStealed = null;
                                    break;
                                case 8:
                                    PoliceAndThief.jewel08.transform.position = new Vector3(5.5f, 3.5f, 1f);
                                    PoliceAndThief.jewel08BeingStealed = null;
                                    break;
                                case 9:
                                    PoliceAndThief.jewel09.transform.position = new Vector3(19, -1.2f, 1f);
                                    PoliceAndThief.jewel09BeingStealed = null;
                                    break;
                                case 10:
                                    PoliceAndThief.jewel10.transform.position = new Vector3(21.5f, -8.35f, 1f);
                                    PoliceAndThief.jewel10BeingStealed = null;
                                    break;
                                case 11:
                                    PoliceAndThief.jewel11.transform.position = new Vector3(12.5f, -3.75f, 1f);
                                    PoliceAndThief.jewel11BeingStealed = null;
                                    break;
                                case 12:
                                    PoliceAndThief.jewel12.transform.position = new Vector3(5.9f, -5.25f, 1f);
                                    PoliceAndThief.jewel12BeingStealed = null;
                                    break;
                                case 13:
                                    PoliceAndThief.jewel13.transform.position = new Vector3(-2.65f, -16.5f, 1f);
                                    PoliceAndThief.jewel13BeingStealed = null;
                                    break;
                                case 14:
                                    PoliceAndThief.jewel14.transform.position = new Vector3(-16.75f, -4.75f, 1f);
                                    PoliceAndThief.jewel14BeingStealed = null;
                                    break;
                                case 15:
                                    PoliceAndThief.jewel15.transform.position = new Vector3(-3.8f, 3.5f, 1f);
                                    PoliceAndThief.jewel15BeingStealed = null;
                                    break;
                            }
                            break;
                        // Airship
                        case 4:
                            switch (jewelRevertedId) {
                                case 1:
                                    PoliceAndThief.jewel01.transform.position = new Vector3(-23.5f, -1.5f, 1f);
                                    PoliceAndThief.jewel01BeingStealed = null;
                                    break;
                                case 2:
                                    PoliceAndThief.jewel02.transform.position = new Vector3(-14.15f, -4.85f, 1f);
                                    PoliceAndThief.jewel02BeingStealed = null;
                                    break;
                                case 3:
                                    PoliceAndThief.jewel03.transform.position = new Vector3(-13.9f, -16.25f, 1f);
                                    PoliceAndThief.jewel03BeingStealed = null;
                                    break;
                                case 4:
                                    PoliceAndThief.jewel04.transform.position = new Vector3(-0.85f, -2.5f, 1f);
                                    PoliceAndThief.jewel04BeingStealed = null;
                                    break;
                                case 5:
                                    PoliceAndThief.jewel05.transform.position = new Vector3(-5, 8.5f, 1f);
                                    PoliceAndThief.jewel05BeingStealed = null;
                                    break;
                                case 6:
                                    PoliceAndThief.jewel06.transform.position = new Vector3(19.3f, -4.15f, 1f);
                                    PoliceAndThief.jewel06BeingStealed = null;
                                    break;
                                case 7:
                                    PoliceAndThief.jewel07.transform.position = new Vector3(19.85f, 8, 1f);
                                    PoliceAndThief.jewel07BeingStealed = null;
                                    break;
                                case 8:
                                    PoliceAndThief.jewel08.transform.position = new Vector3(28.85f, -1.75f, 1f);
                                    PoliceAndThief.jewel08BeingStealed = null;
                                    break;
                                case 9:
                                    PoliceAndThief.jewel09.transform.position = new Vector3(-14.5f, -8.5f, 1f);
                                    PoliceAndThief.jewel09BeingStealed = null;
                                    break;
                                case 10:
                                    PoliceAndThief.jewel10.transform.position = new Vector3(6.3f, -2.75f, 1f);
                                    PoliceAndThief.jewel10BeingStealed = null;
                                    break;
                                case 11:
                                    PoliceAndThief.jewel11.transform.position = new Vector3(20.75f, 2.5f, 1f);
                                    PoliceAndThief.jewel11BeingStealed = null;
                                    break;
                                case 12:
                                    PoliceAndThief.jewel12.transform.position = new Vector3(29.25f, 7, 1f);
                                    PoliceAndThief.jewel12BeingStealed = null;
                                    break;
                                case 13:
                                    PoliceAndThief.jewel13.transform.position = new Vector3(37.5f, -3.5f, 1f);
                                    PoliceAndThief.jewel13BeingStealed = null;
                                    break;
                                case 14:
                                    PoliceAndThief.jewel14.transform.position = new Vector3(25.2f, -8.75f, 1f);
                                    PoliceAndThief.jewel14BeingStealed = null;
                                    break;
                                case 15:
                                    PoliceAndThief.jewel15.transform.position = new Vector3(16.3f, -11, 1f);
                                    PoliceAndThief.jewel15BeingStealed = null;
                                    break;
                            }
                            break;
                    }

                    // if police can't see jewels, hide it after jailing a player
                    if (PlayerControl.LocalPlayer == PoliceAndThief.policeplayer01 || PlayerControl.LocalPlayer == PoliceAndThief.policeplayer02 || PlayerControl.LocalPlayer == PoliceAndThief.policeplayer03 || PlayerControl.LocalPlayer == PoliceAndThief.policeplayer04 || PlayerControl.LocalPlayer == PoliceAndThief.policeplayer05) {
                        if (!PoliceAndThief.policeCanSeeJewels) {
                            switch (jewelRevertedId) {
                                case 1:
                                    PoliceAndThief.jewel01.SetActive(false);
                                    break;
                                case 2:
                                    PoliceAndThief.jewel02.SetActive(false);
                                    break;
                                case 3:
                                    PoliceAndThief.jewel03.SetActive(false);
                                    break;
                                case 4:
                                    PoliceAndThief.jewel04.SetActive(false);
                                    break;
                                case 5:
                                    PoliceAndThief.jewel05.SetActive(false);
                                    break;
                                case 6:
                                    PoliceAndThief.jewel06.SetActive(false);
                                    break;
                                case 7:
                                    PoliceAndThief.jewel07.SetActive(false);
                                    break;
                                case 8:
                                    PoliceAndThief.jewel08.SetActive(false);
                                    break;
                                case 9:
                                    PoliceAndThief.jewel09.SetActive(false);
                                    break;
                                case 10:
                                    PoliceAndThief.jewel10.SetActive(false);
                                    break;
                                case 11:
                                    PoliceAndThief.jewel11.SetActive(false);
                                    break;
                                case 12:
                                    PoliceAndThief.jewel12.SetActive(false);
                                    break;
                                case 13:
                                    PoliceAndThief.jewel13.SetActive(false);
                                    break;
                                case 14:
                                    PoliceAndThief.jewel14.SetActive(false);
                                    break;
                                case 15:
                                    PoliceAndThief.jewel15.SetActive(false);
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
    class RPCHandlerPatch
    {
        static void Postfix([HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader) {
            byte packetId = callId;
            switch (packetId) {

                // Main Controls

                case (byte)CustomRPC.ResetVaribles:
                    RPCProcedure.resetVariables();
                    break;
                case (byte)CustomRPC.ShareOptions:
                    RPCProcedure.ShareOptions((int)reader.ReadPackedUInt32(), reader);
                    break;
                case (byte)CustomRPC.ForceEnd:
                    RPCProcedure.forceEnd();
                    break;
                case (byte)CustomRPC.SetRole:
                    byte roleId = reader.ReadByte();
                    byte playerId = reader.ReadByte();
                    byte flag = reader.ReadByte();
                    RPCProcedure.setRole(roleId, playerId, flag);
                    break;
                case (byte)CustomRPC.UseUncheckedVent:
                    int ventId = reader.ReadPackedInt32();
                    byte ventingPlayer = reader.ReadByte();
                    byte isEnter = reader.ReadByte();
                    RPCProcedure.useUncheckedVent(ventId, ventingPlayer, isEnter);
                    break;
                case (byte)CustomRPC.UncheckedMurderPlayer:
                    byte source = reader.ReadByte();
                    byte target = reader.ReadByte();
                    byte showAnimation = reader.ReadByte();
                    RPCProcedure.uncheckedMurderPlayer(source, target, showAnimation);
                    break;
                case (byte)CustomRPC.UncheckedCmdReportDeadBody:
                    byte reportSource = reader.ReadByte();
                    byte reportTarget = reader.ReadByte();
                    RPCProcedure.uncheckedCmdReportDeadBody(reportSource, reportTarget);
                    break;
                case (byte)CustomRPC.UncheckedExilePlayer:
                    byte exileTarget = reader.ReadByte();
                    RPCProcedure.uncheckedExilePlayer(exileTarget);
                    break;

                // Role impostor functionality

                case (byte)CustomRPC.MimicTransform:
                    RPCProcedure.mimicTransform(reader.ReadByte());
                    break;
                case (byte)CustomRPC.PainterPaint:
                    int colorId = reader.ReadPackedInt32();
                    RPCProcedure.painterPaint(colorId);
                    break;
                case (byte)CustomRPC.DemonSetBitten:
                    byte bittenId = reader.ReadByte();
                    byte reset = reader.ReadByte();
                    RPCProcedure.demonSetBitten(bittenId, reset);
                    break;
                case (byte)CustomRPC.PlaceNun:
                    RPCProcedure.placeNun(reader.ReadBytesAndSize());
                    break;
                case (byte)CustomRPC.RemoveBody:
                    RPCProcedure.removeBody(reader.ReadByte());
                    break;
                case (byte)CustomRPC.DragPlaceBody:
                    RPCProcedure.dragPlaceBody(reader.ReadByte());
                    break;
                case (byte)CustomRPC.PlaceHat:
                    RPCProcedure.placeHat(reader.ReadBytesAndSize());
                    break;
                case (byte)CustomRPC.LightsOut:
                    RPCProcedure.lightsOut();
                    break;
                case (byte)CustomRPC.ManipulatorKill:
                    RPCProcedure.manipulatorKill(reader.ReadByte());
                    break;
                case (byte)CustomRPC.PlaceBomb:
                    RPCProcedure.placeBomb();
                    break;
                case (byte)CustomRPC.FixBomb:
                    RPCProcedure.fixBomb();
                    break;
                case (byte)CustomRPC.BombermanWin:
                    RPCProcedure.bombermanWin();
                    break;
                case (byte)CustomRPC.ChameleonInvisible:
                    RPCProcedure.chameleonInvisible();
                    break;
                case (byte)CustomRPC.GamblerShoot:
                    RPCProcedure.gamblerShoot(reader.ReadByte());
                    break;
                case (byte)CustomRPC.SetSpelledPlayer:
                    RPCProcedure.setSpelledPlayer(reader.ReadByte());
                    break;

                // Role rebeldes functionality

                case (byte)CustomRPC.RenegadeRecruitMinion:
                    RPCProcedure.renegadeRecruitMinion(reader.ReadByte());
                    break;
                case (byte)CustomRPC.BountyHunterSetKill:
                    byte bountyId = reader.ReadByte(); 
                    RPCProcedure.bountyHunterSetKill(bountyId);
                    break;
                case (byte)CustomRPC.BountyHunterKill:
                    RPCProcedure.bountyHunterKill(reader.ReadByte());
                    break;
                case (byte)CustomRPC.PlaceMine:
                    RPCProcedure.placeMine();
                    break;
                case (byte)CustomRPC.PlaceTrap:
                    RPCProcedure.placeTrap();
                    break;
                case (byte)CustomRPC.MineKill:
                    RPCProcedure.mineKill(reader.ReadByte());
                    break;
                case (byte)CustomRPC.ActivateTrap:
                    RPCProcedure.activateTrap(reader.ReadByte());
                    break;
                case (byte)CustomRPC.YinyangerSetYinyang:
                    byte yinedId = reader.ReadByte();
                    byte yinflag = reader.ReadByte();
                    RPCProcedure.yinyangerSetYinyang(yinedId, yinflag);
                    break;
                case (byte)CustomRPC.ChallengerCantDuel:
                    RPCProcedure.challengerCantDuel();
                    break;
                case (byte)CustomRPC.ChallengerPerformDuel:
                    RPCProcedure.challengerPerformDuel();
                    break;
                case (byte)CustomRPC.ChallengerSetRival:
                    byte rivalId = reader.ReadByte();
                    byte rivalflag = reader.ReadByte(); 
                    RPCProcedure.challengerSetRival(rivalId, rivalflag);
                    break;
                case (byte)CustomRPC.ChallengerSelectAttack:
                    byte challengerAttack = reader.ReadByte();
                    RPCProcedure.challengerSelectAttack(challengerAttack);
                    break; 

                // Role neutrals functionality

                case (byte)CustomRPC.RoleThiefSteal:
                    RPCProcedure.roleThiefSteal(reader.ReadByte());
                    break;
                case (byte)CustomRPC.PyromaniacWin:
                    RPCProcedure.pyromaniacWin();
                    break;
                case (byte)CustomRPC.PlaceTreasure:
                    RPCProcedure.placeTreasure();
                    break;
                case (byte)CustomRPC.CollectedTreasure:
                    RPCProcedure.collectedTreasure();
                    break;
                case (byte)CustomRPC.DevourBody:
                    RPCProcedure.devourBody(reader.ReadByte());
                    break;

                // Role crewmates functionality

                case (byte)CustomRPC.MechanicFixLights:
                    RPCProcedure.mechanicFixLights();
                    break;
                case (byte)CustomRPC.MechanicUsedRepair:
                    RPCProcedure.mechanicUsedRepair();
                    break;
                case (byte)CustomRPC.SheriffKill:
                    RPCProcedure.sheriffKill(reader.ReadByte());
                    break;
                case (byte)CustomRPC.TimeTravelerShield:
                    RPCProcedure.timeTravelerShield();
                    break;
                case (byte)CustomRPC.TimeTravelerRewindTime:
                    RPCProcedure.timeTravelerRewindTime();
                    break;
                case (byte)CustomRPC.TimeTravelerRevive:
                    RPCProcedure.timeTravelerRevive(reader.ReadByte());
                    break;
                case (byte)CustomRPC.SquireSetShielded:
                    RPCProcedure.squireSetShielded(reader.ReadByte());
                    break;
                case (byte)CustomRPC.ShieldedMurderAttempt:
                    RPCProcedure.shieldedMurderAttempt();
                    break;
                case (byte)CustomRPC.CheaterCheat:
                    byte playerId1 = reader.ReadByte();
                    byte playerId2 = reader.ReadByte();
                    RPCProcedure.cheaterCheat(playerId1, playerId2);
                    break;
                case (byte)CustomRPC.FortuneTellerReveal:
                    byte targetId = reader.ReadByte();
                    RPCProcedure.fortuneTellerReveal(targetId);
                    break;
                case (byte)CustomRPC.HackerAbilityUses:
                    byte adminOrVitals = reader.ReadByte();
                    RPCProcedure.hackerAbilityUses(adminOrVitals);
                    break;
                case (byte)CustomRPC.SleuthUsedLocate:
                    RPCProcedure.sleuthUsedLocate(reader.ReadByte());
                    break;
                case (byte)CustomRPC.SealVent:
                    RPCProcedure.sealVent(reader.ReadPackedInt32());
                    break;
                case (byte)CustomRPC.SpiritualistRevive:
                    RPCProcedure.spiritualistRevive(reader.ReadByte());
                    break;
                case (byte)CustomRPC.SendSpiritualistIsReviving:
                    RPCProcedure.sendSpiritualistIsReviving();
                    break;
                case (byte)CustomRPC.MurderSpiritualistIfReportWhileReviving:
                    RPCProcedure.murderSpiritualistIfReportWhileReviving();
                    break;
                case (byte)CustomRPC.ResetSpiritualistReviveValues:
                    RPCProcedure.resetSpiritualistReviveValues();
                    break;
                case (byte)CustomRPC.PlaceCamera:
                    RPCProcedure.placeCamera(reader.ReadBytesAndSize());
                    break;
                case (byte)CustomRPC.VigilantAbilityUses:
                    byte vigilantCharges = reader.ReadByte();
                    RPCProcedure.vigilantAbilityUses(vigilantCharges);
                    break;
                case (byte)CustomRPC.PerformerIsReported:
                    byte check = reader.ReadByte();
                    RPCProcedure.performerIsReported(check);
                    break;
                case (byte)CustomRPC.HunterUsedHunted:
                    RPCProcedure.hunterUsedHunted(reader.ReadByte());
                    break;
                case (byte)CustomRPC.SetJinxed:
                    var pid = reader.ReadByte();
                    var jinxedValue = reader.ReadByte();
                    RPCProcedure.setJinxed(pid, jinxedValue);
                    break;

                // Other funtionality

                case (byte)CustomRPC.ChangeMusic:
                    byte whichmusic = reader.ReadByte();
                    RPCProcedure.changeMusic(whichmusic);
                    break;

                // Capture the flag funtionality

                case (byte)CustomRPC.CapturetheFlagKills:
                    byte killId = reader.ReadByte();
                    byte whichplayer = reader.ReadByte();
                    RPCProcedure.capturetheFlagKills(killId, whichplayer);
                    break;
                case (byte)CustomRPC.CaptureTheFlagWhoTookTheFlag:
                    byte bluePlayerWhoHasRedFlag = reader.ReadByte();
                    byte redorblue = reader.ReadByte();
                    RPCProcedure.captureTheFlagWhoTookTheFlag(bluePlayerWhoHasRedFlag, redorblue);
                    break;
                case (byte)CustomRPC.CaptureTheFlagWhichTeamScored:
                    byte whichteam = reader.ReadByte();
                    RPCProcedure.captureTheFlagWhichTeamScored(whichteam);
                    break;

                // Police and Thief funtionality
                case (byte)CustomRPC.PoliceandThiefKills:
                    byte policeandthiefId = reader.ReadByte();
                    byte whichpoliceorthief = reader.ReadByte();
                    RPCProcedure.policeandThiefKills(policeandthiefId, whichpoliceorthief);
                    break;
                case (byte)CustomRPC.PoliceandThiefJail:
                    byte thiefId = reader.ReadByte();
                    RPCProcedure.policeandThiefJail(thiefId);
                    break;
                case (byte)CustomRPC.PoliceandThiefFreeThief:
                    RPCProcedure.policeandThiefFreeThief();
                    break;
                case (byte)CustomRPC.PoliceandThiefTakeJewel:
                    byte thiefwhotookjewel = reader.ReadByte();
                    byte jewelTakeId = reader.ReadByte();
                    RPCProcedure.policeandThiefTakeJewel(thiefwhotookjewel, jewelTakeId);
                    break;
                case (byte)CustomRPC.PoliceandThiefDeliverJewel:
                    byte thiefwhodeliverjewel = reader.ReadByte();
                    byte jewelDeliverId = reader.ReadByte();
                    RPCProcedure.policeandThiefDeliverJewel(thiefwhodeliverjewel, jewelDeliverId);
                    break;
                case (byte)CustomRPC.PoliceandThiefRevertedJewelPosition:
                    byte thiefWhoLostJewel = reader.ReadByte();
                    byte jewelRevertedId = reader.ReadByte();
                    RPCProcedure.policeandThiefRevertedJewelPosition(thiefWhoLostJewel, jewelRevertedId);
                    break;
            }
        }
    }
}
