using HarmonyLib;
using System;
using System.IO;
using System.Net.Http;
using UnityEngine;
using static LasMonjas.LasMonjas;
using LasMonjas.Objects;
using System.Collections.Generic;
using System.Linq;
using Hazel;
using static LasMonjas.RoleInfo;
using Reactor;
using static LasMonjas.MapOptions;
using LasMonjas.Core;

namespace LasMonjas.Patches {
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    class HudManagerUpdatePatch
    {
        static void resetNameTagsAndColors() {
            Dictionary<byte, PlayerControl> playersById = Helpers.allPlayersById();

            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                String playerName = player.Data.PlayerName;
                if (Mimic.transformTimer > 0f && Mimic.mimic == player && Mimic.transformTarget != null) playerName = Mimic.transformTarget.Data.PlayerName;

                player.nameText.text = Helpers.hidePlayerName(PlayerControl.LocalPlayer, player) ? "" : playerName;
                if (PlayerControl.LocalPlayer.Data.Role.IsImpostor && player.Data.Role.IsImpostor) {
                    player.nameText.color = Palette.ImpostorRed;
                } else {
                    player.nameText.color = Color.white;
                }
            }
            if (MeetingHud.Instance != null) {
                foreach (PlayerVoteArea player in MeetingHud.Instance.playerStates) {
                    PlayerControl playerControl = playersById.ContainsKey((byte)player.TargetPlayerId) ? playersById[(byte)player.TargetPlayerId] : null;
                    if (playerControl != null) {
                        player.NameText.text = playerControl.Data.PlayerName;
                        if (PlayerControl.LocalPlayer.Data.Role.IsImpostor && playerControl.Data.Role.IsImpostor) {
                            player.NameText.color = Palette.ImpostorRed;
                        } else {
                            player.NameText.color = Color.white;
                        }
                    }
                }
            }
            if (PlayerControl.LocalPlayer.Data.Role.IsImpostor) {
                List<PlayerControl> impostors = PlayerControl.AllPlayerControls.ToArray().ToList();
                impostors.RemoveAll(x => !x.Data.Role.IsImpostor);
                foreach (PlayerControl player in impostors)
                    player.nameText.color = Palette.ImpostorRed;
                if (MeetingHud.Instance != null)
                    foreach (PlayerVoteArea player in MeetingHud.Instance.playerStates) {
                        PlayerControl playerControl = Helpers.playerById((byte)player.TargetPlayerId);
                        if (playerControl != null && playerControl.Data.Role.IsImpostor)
                            player.NameText.color =  Palette.ImpostorRed;
                    }
            }

        }

        static void setPlayerNameColor(PlayerControl p, Color color) {
            p.nameText.color = color;
            if (MeetingHud.Instance != null)
                foreach (PlayerVoteArea player in MeetingHud.Instance.playerStates)
                    if (player.NameText != null && p.PlayerId == player.TargetPlayerId)
                        player.NameText.color = color;
        }

        static void setNameColors() {

            if (CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode && !KingOfTheHill.kingOfTheHillMode) {
                foreach (PlayerControl redplayer in CaptureTheFlag.redteamFlag) {
                    if (redplayer != null) {
                        setPlayerNameColor(redplayer, Palette.PlayerColors[0]);
                        // This changes redteam players to have red color on everything, but causes lag so only color name changes
                        /*if (!redplayer.Data.IsDead) {
                            redplayer.setLook(redplayer.name, 0, redplayer.Data.DefaultOutfit.HatId, redplayer.Data.DefaultOutfit.VisorId, redplayer.Data.DefaultOutfit.SkinId, redplayer.Data.DefaultOutfit.PetId);
                            //Helpers.setLook(redplayer, redplayer.name, 0, redplayer.Data.DefaultOutfit.HatId, redplayer.Data.DefaultOutfit.VisorId, redplayer.Data.DefaultOutfit.SkinId, redplayer.Data.DefaultOutfit.PetId);
                        }*/
                    }
                }
                foreach (PlayerControl blueplayer in CaptureTheFlag.blueteamFlag) {
                    if (blueplayer != null) {
                        setPlayerNameColor(blueplayer, Palette.PlayerColors[1]);
                        // This changes blueteam players to have blue color on everything, but causes lag so only color name changes
                        /*if (!blueplayer.Data.IsDead) {
                        blueplayer.setLook(blueplayer.name, 1, blueplayer.Data.DefaultOutfit.HatId, blueplayer.Data.DefaultOutfit.VisorId, blueplayer.Data.DefaultOutfit.SkinId, blueplayer.Data.DefaultOutfit.PetId);
                            //Helpers.setLook(blueplayer, blueplayer.name, 1, blueplayer.Data.DefaultOutfit.HatId, blueplayer.Data.DefaultOutfit.VisorId, blueplayer.Data.DefaultOutfit.SkinId, blueplayer.Data.DefaultOutfit.PetId);
                        }*/
                    }
                }
            }
            else if (PoliceAndThief.policeAndThiefMode && !CaptureTheFlag.captureTheFlagMode && !KingOfTheHill.kingOfTheHillMode) {
                foreach (PlayerControl policeplayer in PoliceAndThief.policeTeam) {
                    if (policeplayer != null) {
                        setPlayerNameColor(policeplayer, Palette.PlayerColors[10]);
                        // This changes policeteam players to have orange color on everything and changes their skin/hats, but causes lag so only color name changes
                        /*setPlayerNameColor(policeplayer, Palette.PlayerColors[4]);
                        if (!policeplayer.Data.IsDead) {
                            policeplayer.setLook(policeplayer.name, policeplayer.Data.DefaultOutfit.ColorId, "hat_police", policeplayer.Data.DefaultOutfit.VisorId, "skin_Police", policeplayer.Data.DefaultOutfit.PetId);
                            //policeplayer.setLook(policeplayer.name, 4, "hat_police", policeplayer.Data.DefaultOutfit.VisorId, "skin_Police", policeplayer.Data.DefaultOutfit.PetId);
                            //Helpers.setLook(policeplayer, policeplayer.name, 4, "hat_police", policeplayer.Data.DefaultOutfit.VisorId, "skin_Police", policeplayer.Data.DefaultOutfit.PetId);
                        }*/
                    }
                }
                foreach (PlayerControl thiefplayer in PoliceAndThief.thiefTeam) {
                    if (thiefplayer != null) {
                        setPlayerNameColor(thiefplayer, Palette.PlayerColors[16]);                        
                        // This changes thiefteam players to have brown color on everything and changes their skin/hats, but causes lag so only color name changes
                        /*if (!thiefplayer.Data.IsDead) {
                            thiefplayer.setLook(thiefplayer.name, thiefplayer.Data.DefaultOutfit.ColorId, "hat_pk04_Vagabond", thiefplayer.Data.DefaultOutfit.VisorId, "skin_SuitB", thiefplayer.Data.DefaultOutfit.PetId);
                            //thiefplayer.setLook(thiefplayer.name, 16, "hat_pk04_Vagabond", thiefplayer.Data.DefaultOutfit.VisorId, "skin_SuitB", thiefplayer.Data.DefaultOutfit.PetId);
                            //Helpers.setLook(thiefplayer, thiefplayer.name, 16, "hat_pk04_Vagabond", thiefplayer.Data.DefaultOutfit.VisorId, "skin_SuitB", thiefplayer.Data.DefaultOutfit.PetId);
                        }
                        foreach (PlayerControl capturedthief in PoliceAndThief.thiefArrested) {
                            capturedthief.setLook(capturedthief.name, capturedthief.Data.DefaultOutfit.ColorId, "hat_pk04_Vagabond", capturedthief.Data.DefaultOutfit.VisorId, "skin_prisoner", capturedthief.Data.DefaultOutfit.PetId);
                            //capturedthief.setLook(capturedthief.name, 16, "hat_pk04_Vagabond", capturedthief.Data.DefaultOutfit.VisorId, "skin_prisoner", capturedthief.Data.DefaultOutfit.PetId);
                            //Helpers.setLook(capturedthief, capturedthief.name, 16, "hat_pk04_Vagabond", thiefplayer.Data.DefaultOutfit.VisorId, "skin_prisoner", capturedthief.Data.DefaultOutfit.PetId);
                        }*/
                    }
                }
            }
            else if (KingOfTheHill.kingOfTheHillMode && !PoliceAndThief.policeAndThiefMode && !CaptureTheFlag.captureTheFlagMode) {
                if (KingOfTheHill.usurperPlayer != null) {
                    setPlayerNameColor(KingOfTheHill.usurperPlayer, Palette.PlayerColors[15]);
                }
                
                foreach (PlayerControl greenplayer in KingOfTheHill.greenTeam) {
                    if (greenplayer != null) {
                        setPlayerNameColor(greenplayer, Palette.PlayerColors[2]);
                        /*if (!greenplayer.Data.IsDead) {
                            greenplayer.setLook(greenplayer.name, 2, greenplayer.Data.DefaultOutfit.HatId, greenplayer.Data.DefaultOutfit.VisorId, greenplayer.Data.DefaultOutfit.SkinId, greenplayer.Data.DefaultOutfit.PetId);
                            //Helpers.setLook(greenplayer, greenplayer.name, 2, greenplayer.Data.DefaultOutfit.HatId, greenplayer.Data.DefaultOutfit.VisorId, greenplayer.Data.DefaultOutfit.SkinId, greenplayer.Data.DefaultOutfit.PetId);
                        }*/
                    }
                }
                foreach (PlayerControl yellowplayer in KingOfTheHill.yellowTeam) {
                    if (yellowplayer != null) {
                        setPlayerNameColor(yellowplayer, Palette.PlayerColors[5]);
                        /*if (!yellowplayer.Data.IsDead) {
                            yellowplayer.setLook(yellowplayer.name, 5, yellowplayer.Data.DefaultOutfit.HatId, yellowplayer.Data.DefaultOutfit.VisorId, yellowplayer.Data.DefaultOutfit.SkinId, yellowplayer.Data.DefaultOutfit.PetId);
                            //Helpers.setLook(yellowplayer, yellowplayer.name, 5, yellowplayer.Data.DefaultOutfit.HatId, yellowplayer.Data.DefaultOutfit.VisorId, yellowplayer.Data.DefaultOutfit.SkinId, yellowplayer.Data.DefaultOutfit.PetId);
                        }*/
                    }
                }
            }
            else {
                // Crewmates name color
                if (Captain.captain != null && Captain.captain == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Captain.captain, Captain.color);
                else if (Mechanic.mechanic != null && Mechanic.mechanic == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Mechanic.mechanic, Mechanic.color);
                else if (Sheriff.sheriff != null && Sheriff.sheriff == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Sheriff.sheriff, Sheriff.color);
                else if (Detective.detective != null && Detective.detective == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Detective.detective, Detective.color);
                else if (Forensic.forensic != null && Forensic.forensic == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Forensic.forensic, Forensic.color);
                else if (TimeTraveler.timeTraveler != null && TimeTraveler.timeTraveler == PlayerControl.LocalPlayer)
                    setPlayerNameColor(TimeTraveler.timeTraveler, TimeTraveler.color);
                else if (Squire.squire != null && Squire.squire == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Squire.squire, Squire.color);
                else if (Cheater.cheater != null && Cheater.cheater == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Cheater.cheater, Cheater.color);
                else if (FortuneTeller.fortuneTeller != null && FortuneTeller.fortuneTeller == PlayerControl.LocalPlayer)
                    setPlayerNameColor(FortuneTeller.fortuneTeller, FortuneTeller.color);
                else if (Hacker.hacker != null && Hacker.hacker == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Hacker.hacker, Hacker.color);
                else if (Sleuth.sleuth != null && Sleuth.sleuth == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Sleuth.sleuth, Sleuth.color);
                else if (Fink.fink != null && Fink.fink == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Fink.fink, Fink.color);
                else if (Kid.kid != null && Kid.kid == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Kid.kid, Kid.color);
                else if (Welder.welder != null && Welder.welder == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Welder.welder, Welder.color);
                else if (Spiritualist.spiritualist != null && Spiritualist.spiritualist == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Spiritualist.spiritualist, Spiritualist.color);
                else if (TheChosenOne.theChosenOne != null && TheChosenOne.theChosenOne == PlayerControl.LocalPlayer)
                    setPlayerNameColor(TheChosenOne.theChosenOne, TheChosenOne.color);
                else if (Vigilant.vigilant != null && Vigilant.vigilant == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Vigilant.vigilant, Vigilant.color);
                else if (Vigilant.vigilantMira != null && Vigilant.vigilantMira == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Vigilant.vigilantMira, Vigilant.color);
                else if (Performer.performer != null && Performer.performer == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Performer.performer, Performer.color);
                else if (Hunter.hunter != null && Hunter.hunter == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Hunter.hunter, Hunter.color);
                else if (Jinx.jinx != null && Jinx.jinx == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Jinx.jinx, Jinx.color);

                // Neutrals name color
                else if (Joker.joker != null && Joker.joker == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Joker.joker, Joker.color);
                else if (RoleThief.rolethief != null && RoleThief.rolethief == PlayerControl.LocalPlayer)
                    setPlayerNameColor(RoleThief.rolethief, RoleThief.color);
                else if (Pyromaniac.pyromaniac != null && Pyromaniac.pyromaniac == PlayerControl.LocalPlayer) {
                    setPlayerNameColor(Pyromaniac.pyromaniac, Pyromaniac.color);
                }
                else if (TreasureHunter.treasureHunter != null && TreasureHunter.treasureHunter == PlayerControl.LocalPlayer) {
                    setPlayerNameColor(TreasureHunter.treasureHunter, TreasureHunter.color);
                }
                else if (Devourer.devourer != null && Devourer.devourer == PlayerControl.LocalPlayer) {
                    setPlayerNameColor(Devourer.devourer, Devourer.color);
                }

                // Rebeld name color
                else if (Renegade.renegade != null && Renegade.renegade == PlayerControl.LocalPlayer) {
                    // Renegade can see his minion
                    setPlayerNameColor(Renegade.renegade, Renegade.color);
                    if (Minion.minion != null) {
                        setPlayerNameColor(Minion.minion, Renegade.color);
                    }
                    if (Renegade.fakeMinion != null) {
                        setPlayerNameColor(Renegade.fakeMinion, Renegade.color);
                    }
                }
                else if (BountyHunter.bountyhunter != null && BountyHunter.bountyhunter == PlayerControl.LocalPlayer)
                    setPlayerNameColor(BountyHunter.bountyhunter, BountyHunter.color);
                else if (Trapper.trapper != null && Trapper.trapper == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Trapper.trapper, Trapper.color);
                else if (Yinyanger.yinyanger != null && Yinyanger.yinyanger == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Yinyanger.yinyanger, Yinyanger.color);
                else if (Challenger.challenger != null && Challenger.challenger == PlayerControl.LocalPlayer)
                    setPlayerNameColor(Challenger.challenger, Challenger.color);

                else if (Modifiers.lover1 != null && Modifiers.lover2 != null && (Modifiers.lover1 == PlayerControl.LocalPlayer || Modifiers.lover2 == PlayerControl.LocalPlayer)) {
                    setPlayerNameColor(Modifiers.lover1, Modifiers.loverscolor);
                    setPlayerNameColor(Modifiers.lover2, Modifiers.loverscolor);
                }
                // No else if here, as a Lover of team Renegade needs the colors
                if (Minion.minion != null && Minion.minion == PlayerControl.LocalPlayer) {
                    // Minion can see the renegade
                    setPlayerNameColor(Minion.minion, Minion.color);
                    if (Renegade.renegade != null) {
                        setPlayerNameColor(Renegade.renegade, Renegade.color);
                    }
                }

                // Impostor roles with no color changes: Mimic, Painter, Demon, Janitor, Ilusionist, Manipulator, Bomberman, Chameleon, Gambler and Sorcerer
            }
        }

        static void setNameTags() {

            // Lovers add a heart to their names
            if (Modifiers.lover1 != null && Modifiers.lover2 != null && (Modifiers.lover1 == PlayerControl.LocalPlayer || Modifiers.lover2 == PlayerControl.LocalPlayer)) {
                string suffix = Helpers.cs(Modifiers.loverscolor, " ♥");
                Modifiers.lover1.nameText.text += suffix;
                Modifiers.lover2.nameText.text += suffix;

                if (MeetingHud.Instance != null)
                    foreach (PlayerVoteArea player in MeetingHud.Instance.playerStates)
                        if (Modifiers.lover1.PlayerId == player.TargetPlayerId || Modifiers.lover2.PlayerId == player.TargetPlayerId)
                            player.NameText.text += suffix;
            }

            // Forensic show color type on meeting
            if (PlayerControl.LocalPlayer == Forensic.forensic) {
                if (MeetingHud.Instance != null) {
                    foreach (PlayerVoteArea player in MeetingHud.Instance.playerStates) {
                        var target = Helpers.playerById(player.TargetPlayerId);
                        if (target != null) player.NameText.text += $" ({(Helpers.isLighterColor(target.Data.DefaultOutfit.ColorId) ? "L" : "D")})";
                    }
                }
            }
        }

        static void updateShielded() {
            if (Squire.shielded == null) return;

            if (Squire.shielded.Data.IsDead || Squire.squire == null || Squire.squire.Data.IsDead) {
                Squire.shielded = null;
            }
        }

        static void fortuneTellerUpdate() {
            if (FortuneTeller.fortuneTeller == null || FortuneTeller.fortuneTeller != PlayerControl.LocalPlayer) return;

            // Update revealed players names if not in the duel
            if (!Challenger.isDueling) {
                foreach (PlayerControl p in FortuneTeller.revealedPlayers) {
                    // Update color and name regarding settings and given info
                    string result = p.Data.PlayerName;
                    RoleFortuneTellerInfo si = RoleFortuneTellerInfo.getFortuneTellerRoleInfoForPlayer(p);
                    if (FortuneTeller.kindOfInfo == 0)
                        si.color = si.isGood ? new Color(141f / 255f, 255f / 255f, 255f / 255f, 1) : new Color(255f / 255f, 0f / 255f, 0f / 255f, 1);
                    else if (FortuneTeller.kindOfInfo == 1) {
                        result = p.Data.PlayerName + " (" + si.name + ")";
                    }

                    // Set color and name
                    p.nameText.color = si.color;
                    p.nameText.text = result;
                    if (MeetingHud.Instance != null) {
                        foreach (PlayerVoteArea player in MeetingHud.Instance.playerStates) {
                            if (p.PlayerId == player.TargetPlayerId) {
                                player.NameText.text = result;
                                player.NameText.color = si.color;
                                break;
                            }
                        }
                    }
                }
            }
        }

        static void timerUpdate() {
            if (Hacker.hacker != null) {
                Hacker.hackerTimer -= Time.deltaTime;
            }
            if (Bomberman.bomberman != null) {
                Bomberman.bombTimer -= Time.deltaTime;
            }
            if (Performer.performer != null) {
                Performer.duration -= Time.deltaTime;
            }
            if (Ilusionist.ilusionist != null) {
                Ilusionist.lightsOutTimer -= Time.deltaTime;
            }
            if (Sleuth.sleuth != null) {
                Sleuth.corpsesPathfindTimer -= Time.deltaTime;
            }

            // Capture the flag timer
            if (CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode && !KingOfTheHill.kingOfTheHillMode) {
                CaptureTheFlag.matchDuration -= Time.deltaTime;
                if (CaptureTheFlag.matchDuration < 0) {
                    // Draw + red team have less players than blue team = red team win
                    if (CaptureTheFlag.currentRedTeamPoints == CaptureTheFlag.currentBlueTeamPoints && CaptureTheFlag.redteamFlag.Count < CaptureTheFlag.blueteamFlag.Count) {
                        CaptureTheFlag.triggerRedTeamWin = true;
                        ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.RedTeamFlagWin, false);
                    }
                    // Draw + same team number = draw
                    else if (CaptureTheFlag.currentRedTeamPoints == CaptureTheFlag.currentBlueTeamPoints && CaptureTheFlag.redteamFlag.Count == CaptureTheFlag.blueteamFlag.Count) {
                        CaptureTheFlag.triggerDrawWin = true;
                        ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.DrawTeamWin, false);
                    }
                    // Red team more points than blue team = red team win
                    else if (CaptureTheFlag.currentRedTeamPoints > CaptureTheFlag.currentBlueTeamPoints) {
                        CaptureTheFlag.triggerRedTeamWin = true;
                        ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.RedTeamFlagWin, false);
                    }
                    // otherwise blue team win
                    else {
                        CaptureTheFlag.triggerBlueTeamWin = true;
                        ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.BlueTeamFlagWin, false);
                    }
                }
            }

            // Police and Thief timer, always police team wins if thiefs ran out of time
            if (PoliceAndThief.policeAndThiefMode && !CaptureTheFlag.captureTheFlagMode && !KingOfTheHill.kingOfTheHillMode) {
                PoliceAndThief.policeplayer01lightTimer -= Time.deltaTime;
                PoliceAndThief.policeplayer02lightTimer -= Time.deltaTime;
                PoliceAndThief.policeplayer03lightTimer -= Time.deltaTime;
                PoliceAndThief.policeplayer04lightTimer -= Time.deltaTime;
                PoliceAndThief.policeplayer05lightTimer -= Time.deltaTime; 
                
                PoliceAndThief.matchDuration -= Time.deltaTime;
                if (PoliceAndThief.matchDuration < 0) {
                    PoliceAndThief.triggerPoliceWin = true;
                    ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.ThiefModePoliceWin, false);
                }
            }

            // King of the hill timer
            if (KingOfTheHill.kingOfTheHillMode && !PoliceAndThief.policeAndThiefMode && !CaptureTheFlag.captureTheFlagMode) {
                KingOfTheHill.matchDuration -= Time.deltaTime;
                if (KingOfTheHill.matchDuration < 0) {
                    // both teams with same points = draw
                    if (KingOfTheHill.currentGreenTeamPoints == KingOfTheHill.currentYellowTeamPoints) {
                        KingOfTheHill.triggerDrawWin = true;
                        ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.TeamHillDraw, false);
                    }
                    // green team more points than yellow team = green team win
                    else if (KingOfTheHill.currentGreenTeamPoints > KingOfTheHill.currentYellowTeamPoints) {
                        KingOfTheHill.triggerGreenTeamWin = true;
                        ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.GreenTeamHillWin, false);
                    }
                    // otherwise yellow team win
                    else {
                        KingOfTheHill.triggerYellowTeamWin = true;
                        ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.YellowTeamHillWin, false);
                    }
                }

                if (KingOfTheHill.totalGreenKingzonescaptured != 0) {
                    KingOfTheHill.currentGreenTeamPoints += KingOfTheHill.totalGreenKingzonescaptured * Time.deltaTime;
                    if (KingOfTheHill.currentGreenTeamPoints >= KingOfTheHill.requiredPoints) {
                        KingOfTheHill.triggerGreenTeamWin = true;
                        ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.GreenTeamHillWin, false);
                    }
                }
                if (KingOfTheHill.totalYellowKingzonescaptured != 0) {
                    KingOfTheHill.currentYellowTeamPoints += KingOfTheHill.totalYellowKingzonescaptured * Time.deltaTime;
                    if (KingOfTheHill.currentYellowTeamPoints >= KingOfTheHill.requiredPoints) {
                        KingOfTheHill.triggerYellowTeamWin = true;
                        ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.YellowTeamHillWin, false);
                    }
                }

                KingOfTheHill.kingpointCounter = "Score: " + "<color=#00FF00FF>" + KingOfTheHill.currentGreenTeamPoints.ToString("F0") + "</color> - " + "<color=#FFFF00FF>" + KingOfTheHill.currentYellowTeamPoints.ToString("F0") + "</color>";

            }
        }

        public static void kidUpdate() {
            foreach (PlayerControl p in PlayerControl.AllPlayerControls) {
                if (p == null) continue;

                if (Kid.kid != null && Kid.kid == p)
                    p.transform.localScale = new Vector3(0.45f, 0.45f, 1f);
                else if (Mimic.mimic != null && Mimic.mimic == p && Mimic.transformTarget != null && Mimic.transformTarget == Kid.kid && Mimic.transformTimer > 0f)
                    p.transform.localScale = new Vector3(0.45f, 0.45f, 1f);
                // big chungus update, restore original scale on duel and painting to be more fair
                else if (Modifiers.bigchungus != null && Modifiers.bigchungus == p && !Challenger.isDueling && Painter.painterTimer <= 0) {
                    p.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                // Mimic big chungus update
                else if (Mimic.mimic != null && Mimic.mimic == p && Mimic.transformTarget != null && Mimic.transformTarget == Modifiers.bigchungus && Mimic.transformTimer > 0f)
                    p.transform.localScale = new Vector3(1f, 1f, 1f);
                else
                    p.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            }
        }

        static void updateImpostorKillButton(HudManager __instance) {
            if (!PlayerControl.LocalPlayer.Data.Role.IsImpostor || MeetingHud.Instance || MapBehaviour.Instance != null && MapBehaviour.Instance.IsOpen) return;
            bool enabled = true;
            if (Demon.demon != null && Demon.demon == PlayerControl.LocalPlayer && !Challenger.isDueling)
                enabled = false;
            else if (Janitor.janitor != null && Janitor.dragginBody && PlayerControl.LocalPlayer == Janitor.janitor)
                enabled = false;
            else if (Challenger.isDueling)
                enabled = false;
            else if ((CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode && !KingOfTheHill.kingOfTheHillMode) || (!CaptureTheFlag.captureTheFlagMode && PoliceAndThief.policeAndThiefMode && !KingOfTheHill.kingOfTheHillMode) || (!CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode && KingOfTheHill.kingOfTheHillMode))
                enabled = false;
            if (enabled) __instance.KillButton.Show();
            else __instance.KillButton.Hide();
        }

        static void spiritualistUpdate() {

            if (PlayerControl.LocalPlayer == Spiritualist.spiritualist && Spiritualist.spiritualist != null) {
                foreach (var player in PlayerControl.AllPlayerControls) {
                    if (player.Data.IsDead) {
                        player.myRend.gameObject.SetActive(true);
                        player.myRend.enabled = true;
                        player.nameText.enabled = true;
                        player.nameText.gameObject.SetActive(true);
                    }
                }
            }

            // Identify Spiritualist by name color if you're dead
            foreach (PlayerControl p in PlayerControl.AllPlayerControls) {
                if (Spiritualist.spiritualist != null && !Spiritualist.spiritualist.Data.IsDead && p == PlayerControl.LocalPlayer && p.Data.IsDead) {
                    Spiritualist.spiritualist.nameText.color = Spiritualist.color;
                }
            }
        }

        static void chameleonUpdate() {

            if (Chameleon.chameleon == null) return;

            Chameleon.chameleonTimer -= Time.deltaTime;

            if (Chameleon.chameleonTimer > 0f) {
                if (Chameleon.chameleon == PlayerControl.LocalPlayer) {
                    Chameleon.chameleon.nameText.color = new Color(Chameleon.chameleon.nameText.color.r, Chameleon.chameleon.nameText.color.g, Chameleon.chameleon.nameText.color.b, 0.5f);
                    if (Chameleon.chameleon.CurrentPet != null && Chameleon.chameleon.CurrentPet.rend != null && Chameleon.chameleon.CurrentPet.shadowRend != null) {
                        Chameleon.chameleon.CurrentPet.rend.color = new Color(Chameleon.chameleon.CurrentPet.rend.color.r, Chameleon.chameleon.CurrentPet.rend.color.g, Chameleon.chameleon.CurrentPet.rend.color.b, 0.5f);
                        Chameleon.chameleon.CurrentPet.shadowRend.color = new Color(Chameleon.chameleon.CurrentPet.shadowRend.color.r, Chameleon.chameleon.CurrentPet.shadowRend.color.g, Chameleon.chameleon.CurrentPet.shadowRend.color.b, 0.5f);
                    }
                    if (Chameleon.chameleon.HatRenderer != null) {
                        Chameleon.chameleon.HatRenderer.Parent.color = new Color(Chameleon.chameleon.HatRenderer.Parent.color.r, Chameleon.chameleon.HatRenderer.Parent.color.g, Chameleon.chameleon.HatRenderer.Parent.color.b, 0.5f);
                        Chameleon.chameleon.HatRenderer.BackLayer.color = new Color(Chameleon.chameleon.HatRenderer.BackLayer.color.r, Chameleon.chameleon.HatRenderer.BackLayer.color.g, Chameleon.chameleon.HatRenderer.BackLayer.color.b, 0.5f);
                        Chameleon.chameleon.HatRenderer.FrontLayer.color = new Color(Chameleon.chameleon.HatRenderer.FrontLayer.color.r, Chameleon.chameleon.HatRenderer.FrontLayer.color.g, Chameleon.chameleon.HatRenderer.FrontLayer.color.b, 0.5f);
                    }
                    if (Chameleon.chameleon.VisorSlot != null) {
                        Chameleon.chameleon.VisorSlot.Image.color = new Color(Chameleon.chameleon.VisorSlot.Image.color.r, Chameleon.chameleon.VisorSlot.Image.color.g, Chameleon.chameleon.VisorSlot.Image.color.b, 0.5f);
                    }
                    Chameleon.chameleon.MyPhysics.Skin.layer.color = new Color(Chameleon.chameleon.MyPhysics.Skin.layer.color.r, Chameleon.chameleon.MyPhysics.Skin.layer.color.g, Chameleon.chameleon.MyPhysics.Skin.layer.color.b, 0.5f);
                }
                else {
                    Chameleon.chameleon.nameText.color = new Color(Chameleon.chameleon.nameText.color.r, Chameleon.chameleon.nameText.color.g, Chameleon.chameleon.nameText.color.b, 0f);
                    if (Chameleon.chameleon.CurrentPet != null && Chameleon.chameleon.CurrentPet.rend != null && Chameleon.chameleon.CurrentPet.shadowRend != null) {
                        Chameleon.chameleon.CurrentPet.rend.color = new Color(Chameleon.chameleon.CurrentPet.rend.color.r, Chameleon.chameleon.CurrentPet.rend.color.g, Chameleon.chameleon.CurrentPet.rend.color.b, 0f);
                        Chameleon.chameleon.CurrentPet.shadowRend.color = new Color(Chameleon.chameleon.CurrentPet.shadowRend.color.r, Chameleon.chameleon.CurrentPet.shadowRend.color.g, Chameleon.chameleon.CurrentPet.shadowRend.color.b, 0f);
                    }
                    if (Chameleon.chameleon.HatRenderer != null) {
                        Chameleon.chameleon.HatRenderer.Parent.color = new Color(Chameleon.chameleon.HatRenderer.Parent.color.r, Chameleon.chameleon.HatRenderer.Parent.color.g, Chameleon.chameleon.HatRenderer.Parent.color.b, 0f);
                        Chameleon.chameleon.HatRenderer.BackLayer.color = new Color(Chameleon.chameleon.HatRenderer.BackLayer.color.r, Chameleon.chameleon.HatRenderer.BackLayer.color.g, Chameleon.chameleon.HatRenderer.BackLayer.color.b, 0f);
                        Chameleon.chameleon.HatRenderer.FrontLayer.color = new Color(Chameleon.chameleon.HatRenderer.FrontLayer.color.r, Chameleon.chameleon.HatRenderer.FrontLayer.color.g, Chameleon.chameleon.HatRenderer.FrontLayer.color.b, 0f);
                    }
                    if (Chameleon.chameleon.VisorSlot != null) {
                        Chameleon.chameleon.VisorSlot.Image.color = new Color(Chameleon.chameleon.VisorSlot.Image.color.r, Chameleon.chameleon.VisorSlot.Image.color.g, Chameleon.chameleon.VisorSlot.Image.color.b, 0f);
                    }
                    Chameleon.chameleon.MyPhysics.Skin.layer.color = new Color(Chameleon.chameleon.MyPhysics.Skin.layer.color.r, Chameleon.chameleon.MyPhysics.Skin.layer.color.g, Chameleon.chameleon.MyPhysics.Skin.layer.color.b, 0f);
                }
            }

            // Chameleon reset
            if (Chameleon.chameleonTimer <= 0f) {
                Chameleon.resetChameleon();
            }
        }
        static void yinyangerUpdate() {

            if (Yinyanger.yinyanger == null || Yinyanger.yinyanger.Data.IsDead) {
                return;
            }

            if (Yinyanger.yinyedplayer != null && (Yinyanger.yinyedplayer.Data.Disconnected || Yinyanger.yinyedplayer.Data.IsDead)) {
                // If the yined victim is disconnected or dead reset the yined use so a new target can be selected
                Yinyanger.resetYined();
            }
            if (Yinyanger.yangyedplayer != null && (Yinyanger.yangyedplayer.Data.Disconnected || Yinyanger.yangyedplayer.Data.IsDead)) {
                // If the yanged victim is disconnected or dead reset the yanged use so a new target can be selectet
                Yinyanger.resetYanged();
            }

            if (Yinyanger.yinyedplayer != null && Yinyanger.yangyedplayer != null && !Yinyanger.colision) {
                if (Vector2.Distance(Yinyanger.yinyedplayer.transform.position, Yinyanger.yangyedplayer.transform.position) < 0.5f) {
                    yinYang();
                }
            }
        }

        public static void yinYang() {
            new YinYang(1, Yinyanger.yinyedplayer);
            new YinYang(1, Yinyanger.yangyedplayer);
            Yinyanger.colision = true;
            HudManager.Instance.StartCoroutine(Effects.Lerp(1, new Action<float>((p) => {
                if (Yinyanger.yinyedplayer == PlayerControl.LocalPlayer || Yinyanger.yangyedplayer == PlayerControl.LocalPlayer) {
                    SoundManager.Instance.PlaySound(CustomMain.customAssets.yinyangerYinyangColisionClip, false, 100f);
                }
                Yinyanger.yinyedplayer.moveable = false;
                Yinyanger.yinyedplayer.NetTransform.Halt();
                Yinyanger.yangyedplayer.moveable = false;
                Yinyanger.yangyedplayer.NetTransform.Halt();

                Yinyanger.yinedPlayer = Yinyanger.yinyedplayer;
                Yinyanger.yanedPlayer = Yinyanger.yangyedplayer;
                if (p == 1f) {

                    //MurderAttemptResult murderAttemptResultforYined = Helpers.checkMurderAttempt(Yinyanger.yinyanger, Yinyanger.yinedPlayer);
                    //if (murderAttemptResultforYined == MurderAttemptResult.PerformKill) {
                        RPCProcedure.uncheckedMurderPlayer(Yinyanger.yinyanger.PlayerId, Yinyanger.yinedPlayer.PlayerId, 0);
                    //}
                    //MurderAttemptResult murderAttemptResultforYanged = Helpers.checkMurderAttempt(Yinyanger.yinyanger, Yinyanger.yanedPlayer);
                    //if (murderAttemptResultforYanged == MurderAttemptResult.PerformKill) {
                        RPCProcedure.uncheckedMurderPlayer(Yinyanger.yinyanger.PlayerId, Yinyanger.yanedPlayer.PlayerId, 0);
                    //}

                    Yinyanger.yinyedplayer.moveable = true;
                    Yinyanger.yangyedplayer.moveable = true;

                    Yinyanger.yinedPlayer = null;
                    Yinyanger.yanedPlayer = null;
                }
            })));
            return;
        }
        
        static void challengerUpdate() {

            if (Challenger.challenger == null || !Challenger.isDueling) {
                return;
            }

            // Set grey painting while dueling
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                player.setLook("", 6, "", "", "", "");
            }

            // 30 sec duel duration
            Challenger.duelDuration -= Time.deltaTime;
            if (Challenger.duelDuration < 0 && Challenger.onlyOneFinishDuel && !Challenger.timeOutDuel) {
                Challenger.onlyOneFinishDuel = false;
                Challenger.timeOutDuel = true;
                challengerFinishDuel(1);
            }

            while ((!Challenger.challengerRock && !Challenger.challengerPaper && !Challenger.challengerScissors) || (!Challenger.rivalRock && !Challenger.rivalPaper && !Challenger.rivalScissors))
                return;

            if (Challenger.onlyOneFinishDuel && !Challenger.timeOutDuel) {
                Challenger.onlyOneFinishDuel = false;
                challengerFinishDuel(0);
            }

        }

        public static void challengerFinishDuel(byte duelflag) {

            if (Challenger.challengerRock) {
                new RockPaperScissors(3, Challenger.challenger, 1);
            }
            else if (Challenger.challengerPaper) {
                new RockPaperScissors(3, Challenger.challenger, 2);
            }
            else if (Challenger.challengerScissors) {
                new RockPaperScissors(3, Challenger.challenger, 3);
            }

            if (Challenger.rivalRock) {
                new RockPaperScissors(3, Challenger.rivalPlayer, 1);
            }
            else if (Challenger.rivalPaper) {
                new RockPaperScissors(3, Challenger.rivalPlayer, 2);
            }
            else if (Challenger.rivalScissors) {
                new RockPaperScissors(3, Challenger.rivalPlayer, 3);
            }

            if (duelflag == 0) {
                HudManager.Instance.StartCoroutine(Effects.Lerp(3, new Action<float>((p) => {

                    if (p == 1f) {

                        if (Challenger.challengerRock && Challenger.rivalPaper) {
                            Challenger.rivalPlayer.MurderPlayer(Challenger.challenger);
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.challengerDuelKillClip, false, 5f);
                        }
                        else
                        if (Challenger.challengerRock && Challenger.rivalScissors) {
                            Challenger.challenger.MurderPlayer(Challenger.rivalPlayer);
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.challengerDuelKillClip, false, 5f);
                        }
                        else

                        if (Challenger.challengerPaper && Challenger.rivalRock) {
                            Challenger.challenger.MurderPlayer(Challenger.rivalPlayer);
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.challengerDuelKillClip, false, 5f);
                        }
                        else
                        if (Challenger.challengerPaper && Challenger.rivalScissors) {
                            Challenger.rivalPlayer.MurderPlayer(Challenger.challenger);
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.challengerDuelKillClip, false, 5f);
                        }
                        else

                        if (Challenger.challengerScissors && Challenger.rivalPaper) {
                            Challenger.challenger.MurderPlayer(Challenger.rivalPlayer);
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.challengerDuelKillClip, false, 5f);
                        }
                        else
                        if (Challenger.challengerScissors && Challenger.rivalRock) {
                            Challenger.rivalPlayer.MurderPlayer(Challenger.challenger);
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.challengerDuelKillClip, false, 5f);
                        }
                    }
                })));
            }
            else {
                HudManager.Instance.StartCoroutine(Effects.Lerp(3, new Action<float>((p) => {

                    if (p == 1f) {

                        if ((Challenger.challengerRock || Challenger.challengerPaper || Challenger.challengerScissors) && (!Challenger.rivalRock && !Challenger.rivalPaper && !Challenger.rivalScissors)) {
                            Challenger.challenger.MurderPlayer(Challenger.rivalPlayer);
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.challengerDuelKillClip, false, 5f);
                        }
                        else if ((!Challenger.challengerRock && !Challenger.challengerPaper && !Challenger.challengerScissors) && (Challenger.rivalRock || Challenger.rivalPaper || Challenger.rivalScissors)) {
                            Challenger.rivalPlayer.MurderPlayer(Challenger.challenger);
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.challengerDuelKillClip, false, 5f);
                        }
                        else if ((!Challenger.challengerRock && !Challenger.challengerPaper && !Challenger.challengerScissors) && (!Challenger.rivalRock || !Challenger.rivalPaper || !Challenger.rivalScissors)) {
                            Challenger.challenger.MurderPlayer(Challenger.rivalPlayer);
                            Challenger.rivalPlayer.MurderPlayer(Challenger.challenger);
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.challengerDuelKillClip, false, 5f);
                        }

                    }
                })));
            }

            HudManager.Instance.StartCoroutine(Effects.Lerp(6, new Action<float>((p) => {

                if (p == 1f) {
                    // Undo the character transform
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                        if (player == PlayerControl.LocalPlayer) {
                            player.transform.position = positionBeforeDuel;
                        }
                    }
                    RPCProcedure.changeMusic(2);
                    Challenger.timeOutDuel = false;
                }
            })));

            HudManager.Instance.StartCoroutine(Effects.Lerp(7, new Action<float>((p) => {

                if (p == 1f) {

                    // If after the duel both are dead, teleport their body to the player location
                    if (Challenger.challenger.Data.IsDead && Challenger.rivalPlayer.Data.IsDead) {
                        var bodyChallenger = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == Challenger.challenger.PlayerId);
                        bodyChallenger.transform.position = Challenger.challenger.transform.position;
                        var bodyRival = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == Challenger.rivalPlayer.PlayerId);
                        bodyRival.transform.position = Challenger.rivalPlayer.transform.position;
                        // If after the duel one of them was a lover, teleport the other lover body too
                        if (Modifiers.lover1 != null && (Challenger.rivalPlayer.PlayerId == Modifiers.lover1.PlayerId || Challenger.challenger.PlayerId == Modifiers.lover1.PlayerId)) {
                            var bodyLover2 = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == Modifiers.lover2.PlayerId);
                            bodyLover2.transform.position = Modifiers.lover2.transform.position;
                        }
                        else if (Modifiers.lover2 != null && (Challenger.rivalPlayer.PlayerId == Modifiers.lover2.PlayerId || Challenger.challenger.PlayerId == Modifiers.lover2.PlayerId)) {
                            var bodyLover1 = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == Modifiers.lover1.PlayerId);
                            bodyLover1.transform.position = Modifiers.lover1.transform.position;
                        }
                    }
                    // If after the duel the challenger is dead, teleport his body to the player location
                    else if (Challenger.challenger.Data.IsDead) {
                        var bodyC = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == Challenger.challenger.PlayerId);
                        bodyC.transform.position = Challenger.challenger.transform.position;
                        // If after the duel one of them was a lover, teleport the other lover body too
                        if (Modifiers.lover1 != null && Challenger.challenger.PlayerId == Modifiers.lover1.PlayerId) {
                            var bodyLover2 = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == Modifiers.lover2.PlayerId);
                            bodyLover2.transform.position = Modifiers.lover2.transform.position;
                        }
                        else if (Modifiers.lover2 != null && Challenger.challenger.PlayerId == Modifiers.lover2.PlayerId) {
                            var bodyLover1 = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == Modifiers.lover1.PlayerId);
                            bodyLover1.transform.position = Modifiers.lover1.transform.position;
                        }
                    }
                    // If after the duel the rival is dead, teleport his body to the player location
                    else if (Challenger.rivalPlayer.Data.IsDead) {
                        var bodyR = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == Challenger.rivalPlayer.PlayerId);
                        bodyR.transform.position = Challenger.rivalPlayer.transform.position;
                        // If after the duel one of them was a lover, teleport the other lover body too
                        if (Modifiers.lover1 != null && Challenger.rivalPlayer.PlayerId == Modifiers.lover1.PlayerId) {
                            var bodyLover2 = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == Modifiers.lover2.PlayerId);
                            bodyLover2.transform.position = Modifiers.lover2.transform.position;
                        }
                        else if (Modifiers.lover2 != null && Challenger.rivalPlayer.PlayerId == Modifiers.lover2.PlayerId) {
                            var bodyLover1 = UnityEngine.Object.FindObjectsOfType<DeadBody>().FirstOrDefault(b => b.ParentId == Modifiers.lover1.PlayerId);
                            bodyLover1.transform.position = Modifiers.lover1.transform.position;
                        }
                    }
                }
            })));

            HudManager.Instance.StartCoroutine(Effects.Lerp(8, new Action<float>((p) => {
                if (p == 1f) {
                    // Reset painting after dueling
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                        //player.moveable = true;
                        if (player == null) continue;
                        player.setDefaultLook();
                    }

                    // Reset challenger values after dueling
                    Challenger.ResetValues();
                }
            })));
        }

        static void vigilantMiraUpdate() {

            if (Vigilant.vigilantMira == null || Vigilant.vigilantMira.Data.IsDead || Vigilant.vigilantMira != PlayerControl.LocalPlayer || PlayerControl.GameOptions.MapId != 1) {
                return;
            }

            // Vigilant activate/deactivate doorlog item with Q
            if (Input.GetKeyDown(KeyCode.Q)) {
                Vigilant.doorLogActivated = !Vigilant.doorLogActivated;
                Vigilant.doorLog.SetActive(Vigilant.doorLogActivated);
            }
        }

        static void janitorUpdate() {

            if (Janitor.janitor == null)
                return;

            if (Janitor.dragginBody) {
                DeadBody[] array = UnityEngine.Object.FindObjectsOfType<DeadBody>();
                for (int i = 0; i < array.Length; i++) {
                    if (GameData.Instance.GetPlayerById(array[i].ParentId).PlayerId == Janitor.bodyId) {                      
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

        static void captureTheFlagUpdate() {

            if (!CaptureTheFlag.captureTheFlagMode || (CaptureTheFlag.captureTheFlagMode && PoliceAndThief.policeAndThiefMode && KingOfTheHill.kingOfTheHillMode))
                return;

            if (CaptureTheFlag.redPlayerWhoHasBlueFlag != null) {
                CaptureTheFlag.blueflag.transform.position = CaptureTheFlag.redPlayerWhoHasBlueFlag.transform.position + new Vector3(0f, 0f, -0.1f);
            }

            if (CaptureTheFlag.bluePlayerWhoHasRedFlag != null) {
                CaptureTheFlag.redflag.transform.position = CaptureTheFlag.bluePlayerWhoHasRedFlag.transform.position + new Vector3(0f, 0f, -0.1f);
            }
        }

        static void policeandthiefUpdate() {

            if (!PoliceAndThief.policeAndThiefMode || (PoliceAndThief.policeAndThiefMode && CaptureTheFlag.captureTheFlagMode && KingOfTheHill.kingOfTheHillMode))
                return;

            // Check number of thiefs if a thief disconnects
            foreach (PlayerControl thief in PoliceAndThief.thiefTeam) {
                if (thief.Data.Disconnected) {
                    PoliceAndThief.thiefTeam.Remove(thief);
                    PoliceAndThief.thiefpointCounter = "Stealed Jewels: " + "<color=#00F7FFFF>" + PoliceAndThief.currentJewelsStoled + "/" + PoliceAndThief.requiredJewels + "</color> | " + "Thiefs Captured: " + "<color=#928B55FF>" + PoliceAndThief.currentThiefsCaptured + "/" + PoliceAndThief.thiefTeam.Count + "</color>";
                    if (PoliceAndThief.currentThiefsCaptured == PoliceAndThief.thiefTeam.Count) {
                        PoliceAndThief.triggerPoliceWin = true;
                        ShipStatus.RpcEndGame((GameOverReason)CustomGameOverReason.ThiefModePoliceWin, false);
                    }
                    break;
                }
            }

            // Thief player steal a jewel movement
            if (PoliceAndThief.thiefplayer01 != null && PoliceAndThief.thiefplayer01IsStealing) {
                policeandthiefCheckJewel(PoliceAndThief.thiefplayer01, PoliceAndThief.thiefplayer01JewelId);
            }
            if (PoliceAndThief.thiefplayer02 != null && PoliceAndThief.thiefplayer02IsStealing) {
                policeandthiefCheckJewel(PoliceAndThief.thiefplayer02, PoliceAndThief.thiefplayer02JewelId);
            }
            if (PoliceAndThief.thiefplayer03 != null && PoliceAndThief.thiefplayer03IsStealing) {
                policeandthiefCheckJewel(PoliceAndThief.thiefplayer03, PoliceAndThief.thiefplayer03JewelId);
            }
            if (PoliceAndThief.thiefplayer04 != null && PoliceAndThief.thiefplayer04IsStealing) {
                policeandthiefCheckJewel(PoliceAndThief.thiefplayer04, PoliceAndThief.thiefplayer04JewelId);
            }
            if (PoliceAndThief.thiefplayer05 != null && PoliceAndThief.thiefplayer05IsStealing) {
                policeandthiefCheckJewel(PoliceAndThief.thiefplayer05, PoliceAndThief.thiefplayer05JewelId);
            }
            if (PoliceAndThief.thiefplayer06 != null && PoliceAndThief.thiefplayer06IsStealing) {
                policeandthiefCheckJewel(PoliceAndThief.thiefplayer06, PoliceAndThief.thiefplayer06JewelId);
            }
            if (PoliceAndThief.thiefplayer07 != null && PoliceAndThief.thiefplayer07IsStealing) {
                policeandthiefCheckJewel(PoliceAndThief.thiefplayer07, PoliceAndThief.thiefplayer07JewelId);
            }
            if (PoliceAndThief.thiefplayer08 != null && PoliceAndThief.thiefplayer08IsStealing) {
                policeandthiefCheckJewel(PoliceAndThief.thiefplayer08, PoliceAndThief.thiefplayer08JewelId);
            }
            if (PoliceAndThief.thiefplayer09 != null && PoliceAndThief.thiefplayer09IsStealing) {
                policeandthiefCheckJewel(PoliceAndThief.thiefplayer09, PoliceAndThief.thiefplayer09JewelId);
            }
            if (PoliceAndThief.thiefplayer10 != null && PoliceAndThief.thiefplayer10IsStealing) {
                policeandthiefCheckJewel(PoliceAndThief.thiefplayer10, PoliceAndThief.thiefplayer10JewelId);
            }

        }

        static void policeandthiefCheckJewel(PlayerControl thief, byte jewelId) {
            switch (jewelId) {
                case 1:
                    PoliceAndThief.jewel01.transform.position = thief.transform.position + new Vector3(0, 0.5f, -0.25f);
                    break;
                case 2:
                    PoliceAndThief.jewel02.transform.position = thief.transform.position + new Vector3(0, 0.5f, -0.25f);
                    break;
                case 3:
                    PoliceAndThief.jewel03.transform.position = thief.transform.position + new Vector3(0, 0.5f, -0.25f);
                    break;
                case 4:
                    PoliceAndThief.jewel04.transform.position = thief.transform.position + new Vector3(0, 0.5f, -0.25f);
                    break;
                case 5:
                    PoliceAndThief.jewel05.transform.position = thief.transform.position + new Vector3(0, 0.5f, -0.25f);
                    break;
                case 6:
                    PoliceAndThief.jewel06.transform.position = thief.transform.position + new Vector3(0, 0.5f, -0.25f);
                    break;
                case 7:
                    PoliceAndThief.jewel07.transform.position = thief.transform.position + new Vector3(0, 0.5f, -0.25f);
                    break;
                case 8:
                    PoliceAndThief.jewel08.transform.position = thief.transform.position + new Vector3(0, 0.5f, -0.25f);
                    break;
                case 9:
                    PoliceAndThief.jewel09.transform.position = thief.transform.position + new Vector3(0, 0.5f, -0.25f);
                    break;
                case 10:
                    PoliceAndThief.jewel10.transform.position = thief.transform.position + new Vector3(0, 0.5f, -0.25f);
                    break;
                case 11:
                    PoliceAndThief.jewel11.transform.position = thief.transform.position + new Vector3(0, 0.5f, -0.25f);
                    break;
                case 12:
                    PoliceAndThief.jewel12.transform.position = thief.transform.position + new Vector3(0, 0.5f, -0.25f);
                    break;
                case 13:
                    PoliceAndThief.jewel13.transform.position = thief.transform.position + new Vector3(0, 0.5f, -0.25f);
                    break;
                case 14:
                    PoliceAndThief.jewel14.transform.position = thief.transform.position + new Vector3(0, 0.5f, -0.25f);
                    break;
                case 15:
                    PoliceAndThief.jewel15.transform.position = thief.transform.position + new Vector3(0, 0.5f, -0.25f);
                    break;
            }
        }

        static void kingOfTheHillUpdate() {

            if (!KingOfTheHill.kingOfTheHillMode || (PoliceAndThief.policeAndThiefMode && CaptureTheFlag.captureTheFlagMode && KingOfTheHill.kingOfTheHillMode))
                return;

            // If king disconnects, assing new king
            foreach (PlayerControl greenplayer in KingOfTheHill.greenTeam) {
                if (greenplayer == KingOfTheHill.greenKingplayer && KingOfTheHill.greenKingplayer.Data.Disconnected) {
                    KingOfTheHill.greenTeam.Remove(KingOfTheHill.greenKingplayer);
                    KingOfTheHill.greenKingplayer = null;
                    KingOfTheHill.greenKingplayer = KingOfTheHill.greenTeam[0];
                    KingOfTheHill.greenkingaura.transform.position = new Vector3(KingOfTheHill.greenKingplayer.transform.position.x, KingOfTheHill.greenKingplayer.transform.position.y, 0.4f);
                    KingOfTheHill.greenkingaura.transform.parent = KingOfTheHill.greenKingplayer.transform;
                    if (PlayerControl.LocalPlayer == KingOfTheHill.greenKingplayer) {
                        new CustomMessage("You're the new <color=#00FF00FF>Green King</color>!", 5, -1, 1.6f, 11);
                    }
                    break;
                }
            }
            foreach (PlayerControl yellowplayer in KingOfTheHill.yellowTeam) {
                if (yellowplayer == KingOfTheHill.yellowKingplayer && KingOfTheHill.yellowKingplayer.Data.Disconnected) {
                    KingOfTheHill.yellowTeam.Remove(KingOfTheHill.yellowKingplayer);
                    KingOfTheHill.yellowKingplayer = null;
                    KingOfTheHill.yellowKingplayer = KingOfTheHill.yellowTeam[0];
                    KingOfTheHill.yellowkingaura.transform.position = new Vector3(KingOfTheHill.yellowKingplayer.transform.position.x, KingOfTheHill.yellowKingplayer.transform.position.y, 0.4f);
                    KingOfTheHill.yellowkingaura.transform.parent = KingOfTheHill.yellowKingplayer.transform;
                    if (PlayerControl.LocalPlayer == KingOfTheHill.yellowKingplayer) {
                        new CustomMessage("You're the new <color=#FFFF00FF>Yellow King</color>!", 5, -1, 1.6f, 11);
                    }
                    break;
                }
            }
        }

        static void UpdateMiniMap() {

            if (MapBehaviour.Instance != null && MapBehaviour.Instance.IsOpen && CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode && !KingOfTheHill.kingOfTheHillMode) {
                switch (PlayerControl.GameOptions.MapId) {
                    case 0:
                            GameObject minimapSabotageSkeld = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                            minimapSabotageSkeld.SetActive(false);
                        if (activatedSensei && !updatedSenseiMinimap) {
                            GameObject mymap = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/Background");
                            mymap.GetComponent<SpriteRenderer>().sprite = CustomMain.customAssets.customMinimap.GetComponent<SpriteRenderer>().sprite;
                            GameObject hereindicator = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/HereIndicatorParent");
                            hereindicator.transform.position = hereindicator.transform.position + new Vector3(0.23f, -0.8f, 0);

                            // Map room names
                            GameObject minimapNames = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/RoomNames (1)");
                            minimapNames.transform.GetChild(0).transform.position = minimapNames.transform.GetChild(0).transform.position + new Vector3(0f, -0.5f, 0); // Upper engine
                            minimapNames.transform.GetChild(2).transform.position = minimapNames.transform.GetChild(2).transform.position + new Vector3(0.7f, -0.55f, 0); // Reactor
                            minimapNames.transform.GetChild(3).transform.position = minimapNames.transform.GetChild(3).transform.position + new Vector3(1.75f, 2.37f, 0); // security
                            minimapNames.transform.GetChild(4).transform.position = minimapNames.transform.GetChild(4).transform.position + new Vector3(0.89f, -1.18f, 0); // medbey
                            minimapNames.transform.GetChild(5).transform.position = minimapNames.transform.GetChild(5).transform.position + new Vector3(0.52f, -1.32f, 0); // Cafetería
                            minimapNames.transform.GetChild(6).transform.position = minimapNames.transform.GetChild(6).transform.position + new Vector3(1f, -1.59f, 0); // weapons
                            minimapNames.transform.GetChild(7).transform.position = minimapNames.transform.GetChild(7).transform.position + new Vector3(-1.72f, -3.03f, 0); // nav
                            minimapNames.transform.GetChild(8).transform.position = minimapNames.transform.GetChild(8).transform.position + new Vector3(-0.08f, 1.45f, 0); // shields
                            minimapNames.transform.GetChild(9).transform.position = minimapNames.transform.GetChild(9).transform.position + new Vector3(1.1f, 2.88f, 0); // cooms
                            minimapNames.transform.GetChild(10).transform.position = minimapNames.transform.GetChild(10).transform.position + new Vector3(-2.2f, -0.82f, 0); // storage
                            minimapNames.transform.GetChild(11).transform.position = minimapNames.transform.GetChild(11).transform.position + new Vector3(0.32f, -1.02f, 0); // Admin
                            minimapNames.transform.GetChild(12).transform.position = minimapNames.transform.GetChild(12).transform.position + new Vector3(0.53f, -2.1f, 0); // electrical
                            minimapNames.transform.GetChild(13).transform.position = minimapNames.transform.GetChild(13).transform.position + new Vector3(-3.5f, -0.5f, 0); // o2

                            // Map sabotage
                            GameObject minimapSabotage = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                            minimapSabotage.transform.GetChild(0).gameObject.SetActive(false); // cafeteria doors
                            minimapSabotage.transform.GetChild(2).gameObject.SetActive(false); // medbey doors
                            minimapSabotage.transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false); // electrical doors
                            minimapSabotage.transform.GetChild(5).gameObject.SetActive(false); // upper engine doors
                            minimapSabotage.transform.GetChild(6).gameObject.SetActive(false); // lower engine doors
                            minimapSabotage.transform.GetChild(7).gameObject.SetActive(false); // storage doors
                            minimapSabotage.transform.GetChild(9).gameObject.SetActive(false); // security doors

                            minimapSabotage.transform.GetChild(1).transform.position = minimapSabotage.transform.GetChild(1).transform.position + new Vector3(0.95f, 3.3f, 0); // Sabotage cooms
                            minimapSabotage.transform.GetChild(3).transform.GetChild(1).transform.position = minimapSabotage.transform.GetChild(3).transform.GetChild(1).transform.position + new Vector3(0.165f, -1.2f, 0); // Sabotage electrical
                            minimapSabotage.transform.GetChild(4).transform.position = minimapSabotage.transform.GetChild(4).transform.position + new Vector3(-3f, 0.05f, 0); // Sabotage o2
                            minimapSabotage.transform.GetChild(8).transform.position = minimapSabotage.transform.GetChild(8).transform.position + new Vector3(0.6f, 0.1f, 0); // Sabotage reactor


                            updatedSenseiMinimap = true;
                        }
                        break;
                    case 1:
                        GameObject minimapSabotageMira = GameObject.Find("Main Camera/Hud/HqMap(Clone)/InfectedOverlay");
                        minimapSabotageMira.SetActive(false);
                        break;
                    case 2:
                        GameObject minimapSabotagePolus = GameObject.Find("Main Camera/Hud/PbMap(Clone)/InfectedOverlay");
                        minimapSabotagePolus.SetActive(false);
                        break;
                    case 3:
                        GameObject minimapSabotageDleks = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                        minimapSabotageDleks.SetActive(false);
                        break;
                    case 4:
                        GameObject minimapSabotageAirship = GameObject.Find("Main Camera/Hud/AirshipMap(Clone)/InfectedOverlay");
                        minimapSabotageAirship.SetActive(false);
                        break;
                }
            }
            else if (MapBehaviour.Instance != null && MapBehaviour.Instance.IsOpen && !CaptureTheFlag.captureTheFlagMode && PoliceAndThief.policeAndThiefMode && !KingOfTheHill.kingOfTheHillMode) {
                switch (PlayerControl.GameOptions.MapId) {
                    case 0:
                        GameObject minimapSabotageSkeld = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                        minimapSabotageSkeld.SetActive(false);
                        if (activatedSensei && !updatedSenseiMinimap) {
                            GameObject mymap = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/Background");
                            mymap.GetComponent<SpriteRenderer>().sprite = CustomMain.customAssets.customMinimap.GetComponent<SpriteRenderer>().sprite;
                            GameObject hereindicator = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/HereIndicatorParent");
                            hereindicator.transform.position = hereindicator.transform.position + new Vector3(0.23f, -0.8f, 0);

                            // Map room names
                            GameObject minimapNames = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/RoomNames (1)");
                            minimapNames.transform.GetChild(0).transform.position = minimapNames.transform.GetChild(0).transform.position + new Vector3(0f, -0.5f, 0); // upper engine
                            minimapNames.transform.GetChild(2).transform.position = minimapNames.transform.GetChild(2).transform.position + new Vector3(0.7f, -0.55f, 0); // Reactor
                            minimapNames.transform.GetChild(3).transform.position = minimapNames.transform.GetChild(3).transform.position + new Vector3(1.75f, 2.37f, 0); // secutiry
                            minimapNames.transform.GetChild(4).transform.position = minimapNames.transform.GetChild(4).transform.position + new Vector3(0.89f, -1.18f, 0); // medbey
                            minimapNames.transform.GetChild(5).transform.position = minimapNames.transform.GetChild(5).transform.position + new Vector3(0.52f, -1.32f, 0); // Cafetería
                            minimapNames.transform.GetChild(6).transform.position = minimapNames.transform.GetChild(6).transform.position + new Vector3(1f, -1.59f, 0); // weapons
                            minimapNames.transform.GetChild(7).transform.position = minimapNames.transform.GetChild(7).transform.position + new Vector3(-1.72f, -3.03f, 0); // nav
                            minimapNames.transform.GetChild(8).transform.position = minimapNames.transform.GetChild(8).transform.position + new Vector3(-0.08f, 1.45f, 0); // shields
                            minimapNames.transform.GetChild(9).transform.position = minimapNames.transform.GetChild(9).transform.position + new Vector3(1.1f, 2.88f, 0); // cooms
                            minimapNames.transform.GetChild(10).transform.position = minimapNames.transform.GetChild(10).transform.position + new Vector3(-2.2f, -0.82f, 0); // storage
                            minimapNames.transform.GetChild(11).transform.position = minimapNames.transform.GetChild(11).transform.position + new Vector3(0.32f, -1.02f, 0); // Admin
                            minimapNames.transform.GetChild(12).transform.position = minimapNames.transform.GetChild(12).transform.position + new Vector3(0.53f, -2.1f, 0); // elec
                            minimapNames.transform.GetChild(13).transform.position = minimapNames.transform.GetChild(13).transform.position + new Vector3(-3.5f, -0.5f, 0); // o2

                            // Map sabotage
                            GameObject minimapSabotage = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                            minimapSabotage.transform.GetChild(0).gameObject.SetActive(false); // cafeteria doors
                            minimapSabotage.transform.GetChild(2).gameObject.SetActive(false); // medbey
                            minimapSabotage.transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false); // elec doors
                            minimapSabotage.transform.GetChild(5).gameObject.SetActive(false); // upper engine doors
                            minimapSabotage.transform.GetChild(6).gameObject.SetActive(false); // lower engine doors
                            minimapSabotage.transform.GetChild(7).gameObject.SetActive(false); // storage doors
                            minimapSabotage.transform.GetChild(9).gameObject.SetActive(false); // security doors

                            minimapSabotage.transform.GetChild(1).transform.position = minimapSabotage.transform.GetChild(1).transform.position + new Vector3(0.95f, 3.3f, 0); // Sabotage cooms
                            minimapSabotage.transform.GetChild(3).transform.GetChild(1).transform.position = minimapSabotage.transform.GetChild(3).transform.GetChild(1).transform.position + new Vector3(0.165f, -1.2f, 0); // Sabotage elec
                            minimapSabotage.transform.GetChild(4).transform.position = minimapSabotage.transform.GetChild(4).transform.position + new Vector3(-3f, 0.05f, 0); // Sabotage o2
                            minimapSabotage.transform.GetChild(8).transform.position = minimapSabotage.transform.GetChild(8).transform.position + new Vector3(0.6f, 0.1f, 0); // Sabotage reactor


                            updatedSenseiMinimap = true;
                        }
                        break;
                    case 1:
                        GameObject minimapSabotageMira = GameObject.Find("Main Camera/Hud/HqMap(Clone)/InfectedOverlay");
                        minimapSabotageMira.SetActive(false);
                        break;
                    case 2:
                        GameObject minimapSabotagePolus = GameObject.Find("Main Camera/Hud/PbMap(Clone)/InfectedOverlay");
                        minimapSabotagePolus.SetActive(false);
                        break;
                    case 3:
                        GameObject minimapSabotageDleks = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                        minimapSabotageDleks.SetActive(false);
                        break;
                    case 4:
                        GameObject minimapSabotageAirship = GameObject.Find("Main Camera/Hud/AirshipMap(Clone)/InfectedOverlay");
                        minimapSabotageAirship.SetActive(false);
                        break;
                }
            }
            else if (MapBehaviour.Instance != null && MapBehaviour.Instance.IsOpen && !CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode && KingOfTheHill.kingOfTheHillMode) {
                switch (PlayerControl.GameOptions.MapId) {
                    case 0:
                        GameObject minimapSabotageSkeld = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                        minimapSabotageSkeld.SetActive(false);
                        if (activatedSensei && !updatedSenseiMinimap) {
                            GameObject mymap = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/Background");
                            mymap.GetComponent<SpriteRenderer>().sprite = CustomMain.customAssets.customMinimap.GetComponent<SpriteRenderer>().sprite;
                            GameObject hereindicator = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/HereIndicatorParent");
                            hereindicator.transform.position = hereindicator.transform.position + new Vector3(0.23f, -0.8f, 0);

                            // Map room names
                            GameObject minimapNames = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/RoomNames (1)");
                            minimapNames.transform.GetChild(0).transform.position = minimapNames.transform.GetChild(0).transform.position + new Vector3(0f, -0.5f, 0); // upper engine
                            minimapNames.transform.GetChild(2).transform.position = minimapNames.transform.GetChild(2).transform.position + new Vector3(0.7f, -0.55f, 0); // Reactor
                            minimapNames.transform.GetChild(3).transform.position = minimapNames.transform.GetChild(3).transform.position + new Vector3(1.75f, 2.37f, 0); // security
                            minimapNames.transform.GetChild(4).transform.position = minimapNames.transform.GetChild(4).transform.position + new Vector3(0.89f, -1.18f, 0); // medbey
                            minimapNames.transform.GetChild(5).transform.position = minimapNames.transform.GetChild(5).transform.position + new Vector3(0.52f, -1.32f, 0); // Cafetería
                            minimapNames.transform.GetChild(6).transform.position = minimapNames.transform.GetChild(6).transform.position + new Vector3(1f, -1.59f, 0); // weapons
                            minimapNames.transform.GetChild(7).transform.position = minimapNames.transform.GetChild(7).transform.position + new Vector3(-1.72f, -3.03f, 0); // nav
                            minimapNames.transform.GetChild(8).transform.position = minimapNames.transform.GetChild(8).transform.position + new Vector3(-0.08f, 1.45f, 0); // shields
                            minimapNames.transform.GetChild(9).transform.position = minimapNames.transform.GetChild(9).transform.position + new Vector3(1.1f, 2.88f, 0); // comms
                            minimapNames.transform.GetChild(10).transform.position = minimapNames.transform.GetChild(10).transform.position + new Vector3(-2.2f, -0.82f, 0); // storage
                            minimapNames.transform.GetChild(11).transform.position = minimapNames.transform.GetChild(11).transform.position + new Vector3(0.32f, -1.02f, 0); // Admin
                            minimapNames.transform.GetChild(12).transform.position = minimapNames.transform.GetChild(12).transform.position + new Vector3(0.53f, -2.1f, 0); // elec
                            minimapNames.transform.GetChild(13).transform.position = minimapNames.transform.GetChild(13).transform.position + new Vector3(-3.5f, -0.5f, 0); // o2

                            // Map sabotage
                            GameObject minimapSabotage = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                            minimapSabotage.transform.GetChild(0).gameObject.SetActive(false); // cafeteria doors
                            minimapSabotage.transform.GetChild(2).gameObject.SetActive(false); // medbey doors
                            minimapSabotage.transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false); // elec doors
                            minimapSabotage.transform.GetChild(5).gameObject.SetActive(false); // upper engine doors
                            minimapSabotage.transform.GetChild(6).gameObject.SetActive(false); // lower engine doors
                            minimapSabotage.transform.GetChild(7).gameObject.SetActive(false); // storage doors
                            minimapSabotage.transform.GetChild(9).gameObject.SetActive(false); // security doors

                            minimapSabotage.transform.GetChild(1).transform.position = minimapSabotage.transform.GetChild(1).transform.position + new Vector3(0.95f, 3.3f, 0); // Sabotage cooms
                            minimapSabotage.transform.GetChild(3).transform.GetChild(1).transform.position = minimapSabotage.transform.GetChild(3).transform.GetChild(1).transform.position + new Vector3(0.165f, -1.2f, 0); // Sabotage elec
                            minimapSabotage.transform.GetChild(4).transform.position = minimapSabotage.transform.GetChild(4).transform.position + new Vector3(-3f, 0.05f, 0); // Sabotage o2
                            minimapSabotage.transform.GetChild(8).transform.position = minimapSabotage.transform.GetChild(8).transform.position + new Vector3(0.6f, 0.1f, 0); // Sabotage reactor


                            updatedSenseiMinimap = true;
                        }
                        break;
                    case 1:
                        GameObject minimapSabotageMira = GameObject.Find("Main Camera/Hud/HqMap(Clone)/InfectedOverlay");
                        minimapSabotageMira.SetActive(false);
                        break;
                    case 2:
                        GameObject minimapSabotagePolus = GameObject.Find("Main Camera/Hud/PbMap(Clone)/InfectedOverlay");
                        minimapSabotagePolus.SetActive(false);
                        break;
                    case 3:
                        GameObject minimapSabotageDleks = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                        minimapSabotageDleks.SetActive(false);
                        break;
                    case 4:
                        GameObject minimapSabotageAirship = GameObject.Find("Main Camera/Hud/AirshipMap(Clone)/InfectedOverlay");
                        minimapSabotageAirship.SetActive(false);
                        break;
                }
            }
            else if (MapBehaviour.Instance != null && MapBehaviour.Instance.IsOpen && PlayerControl.GameOptions.MapId == 0 && activatedSensei && !updatedSenseiMinimap && ((!CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode && !KingOfTheHill.kingOfTheHillMode) || (CaptureTheFlag.captureTheFlagMode && PoliceAndThief.policeAndThiefMode && KingOfTheHill.kingOfTheHillMode) || (!CaptureTheFlag.captureTheFlagMode && PoliceAndThief.policeAndThiefMode && KingOfTheHill.kingOfTheHillMode) || (CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode && KingOfTheHill.kingOfTheHillMode) || (CaptureTheFlag.captureTheFlagMode && PoliceAndThief.policeAndThiefMode && !KingOfTheHill.kingOfTheHillMode))) {
                GameObject mymap = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/Background");
                mymap.GetComponent<SpriteRenderer>().sprite = CustomMain.customAssets.customMinimap.GetComponent<SpriteRenderer>().sprite;
                GameObject hereindicator = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/HereIndicatorParent");
                hereindicator.transform.position = hereindicator.transform.position + new Vector3(0.23f, -0.8f, 0);

                // Map room names
                GameObject minimapNames = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/RoomNames (1)");
                minimapNames.transform.GetChild(0).transform.position = minimapNames.transform.GetChild(0).transform.position + new Vector3(0f, -0.5f, 0); // upper engine
                minimapNames.transform.GetChild(2).transform.position = minimapNames.transform.GetChild(2).transform.position + new Vector3(0.7f, -0.55f, 0); // Reactor
                minimapNames.transform.GetChild(3).transform.position = minimapNames.transform.GetChild(3).transform.position + new Vector3(1.75f, 2.37f, 0); // security
                minimapNames.transform.GetChild(4).transform.position = minimapNames.transform.GetChild(4).transform.position + new Vector3(0.89f, -1.18f, 0); // medbey
                minimapNames.transform.GetChild(5).transform.position = minimapNames.transform.GetChild(5).transform.position + new Vector3(0.52f, -1.32f, 0); // Cafetería
                minimapNames.transform.GetChild(6).transform.position = minimapNames.transform.GetChild(6).transform.position + new Vector3(1f, -1.59f, 0); // weapons
                minimapNames.transform.GetChild(7).transform.position = minimapNames.transform.GetChild(7).transform.position + new Vector3(-1.72f, -3.03f, 0); // nav
                minimapNames.transform.GetChild(8).transform.position = minimapNames.transform.GetChild(8).transform.position + new Vector3(-0.08f, 1.45f, 0); // shields
                minimapNames.transform.GetChild(9).transform.position = minimapNames.transform.GetChild(9).transform.position + new Vector3(1.1f, 2.88f, 0); // cooms
                minimapNames.transform.GetChild(10).transform.position = minimapNames.transform.GetChild(10).transform.position + new Vector3(-2.2f, -0.82f, 0); // storage
                minimapNames.transform.GetChild(11).transform.position = minimapNames.transform.GetChild(11).transform.position + new Vector3(0.32f, -1.02f, 0); // Admin
                minimapNames.transform.GetChild(12).transform.position = minimapNames.transform.GetChild(12).transform.position + new Vector3(0.53f, -2.1f, 0); // elec
                minimapNames.transform.GetChild(13).transform.position = minimapNames.transform.GetChild(13).transform.position + new Vector3(-3.5f, -0.5f, 0); // o2

                // Map sabotage
                GameObject minimapSabotage = GameObject.Find("Main Camera/Hud/ShipMap(Clone)/InfectedOverlay");
                minimapSabotage.transform.GetChild(0).gameObject.SetActive(false); // cafeteria doors
                minimapSabotage.transform.GetChild(2).gameObject.SetActive(false); // medbey doors
                minimapSabotage.transform.GetChild(3).transform.GetChild(0).gameObject.SetActive(false); // Puertas electricidad
                minimapSabotage.transform.GetChild(5).gameObject.SetActive(false); // upper engine doors
                minimapSabotage.transform.GetChild(6).gameObject.SetActive(false); // lower engine doors
                minimapSabotage.transform.GetChild(7).gameObject.SetActive(false); // storage doors
                minimapSabotage.transform.GetChild(9).gameObject.SetActive(false); // security doors

                minimapSabotage.transform.GetChild(1).transform.position = minimapSabotage.transform.GetChild(1).transform.position + new Vector3(0.95f, 3.3f, 0); // Sabotage cooms
                minimapSabotage.transform.GetChild(3).transform.GetChild(1).transform.position = minimapSabotage.transform.GetChild(3).transform.GetChild(1).transform.position + new Vector3(0.165f, -1.2f, 0); // Sabotage elec
                minimapSabotage.transform.GetChild(4).transform.position = minimapSabotage.transform.GetChild(4).transform.position + new Vector3(-3f, 0.05f, 0); // Sabotage o2
                minimapSabotage.transform.GetChild(8).transform.position = minimapSabotage.transform.GetChild(8).transform.position + new Vector3(0.6f, 0.1f, 0); // Sabotage reactor


                updatedSenseiMinimap = true;
            }

            // If bomb, lights actives or special 1vs1 condition, prevent sabotage open map
            if (((CaptureTheFlag.captureTheFlagMode && PoliceAndThief.policeAndThiefMode && KingOfTheHill.kingOfTheHillMode) || (!CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode && !KingOfTheHill.kingOfTheHillMode)) && PlayerControl.LocalPlayer.Data.Role.IsImpostor && MapBehaviour.Instance != null && MapBehaviour.Instance.IsOpen && (alivePlayers <= 2 || Bomberman.activeBomb || Challenger.isDueling || Ilusionist.lightsOutTimer > 0)) {
                MapBehaviour.Instance.Close();
            }
        }

        static void updateReportButton(HudManager __instance) {
            if ((!CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode && !KingOfTheHill.kingOfTheHillMode) || (CaptureTheFlag.captureTheFlagMode && PoliceAndThief.policeAndThiefMode && KingOfTheHill.kingOfTheHillMode) || (!CaptureTheFlag.captureTheFlagMode && PoliceAndThief.policeAndThiefMode && KingOfTheHill.kingOfTheHillMode) || (CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode && KingOfTheHill.kingOfTheHillMode) || (CaptureTheFlag.captureTheFlagMode && PoliceAndThief.policeAndThiefMode && !KingOfTheHill.kingOfTheHillMode)) {
                if (!activatedReportButtonAfterCustomMode) {
                    __instance.ReportButton.gameObject.SetActive(true);
                    __instance.ReportButton.graphic.enabled = true;
                    __instance.ReportButton.enabled = true;
                    activatedReportButtonAfterCustomMode = true;
                }
                return;
            }

            bool enabled = true;
            if (CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode && !KingOfTheHill.kingOfTheHillMode || !CaptureTheFlag.captureTheFlagMode && PoliceAndThief.policeAndThiefMode && !KingOfTheHill.kingOfTheHillMode || !CaptureTheFlag.captureTheFlagMode && !PoliceAndThief.policeAndThiefMode && KingOfTheHill.kingOfTheHillMode)
                enabled = false;
            enabled &= __instance.ReportButton.isActiveAndEnabled;

            __instance.ReportButton.gameObject.SetActive(enabled);
            __instance.ReportButton.graphic.enabled = enabled;
            __instance.ReportButton.enabled = enabled;
        }

        static void Postfix(HudManager __instance)
        {
            if (AmongUsClient.Instance.GameState != InnerNet.InnerNetClient.GameStates.Started) return;

            CustomButton.HudUpdate();
            resetNameTagsAndColors();
            setNameColors();
            updateShielded();
            setNameTags();
            UpdateMiniMap();

            // Impostors
            updateImpostorKillButton(__instance);

            // Timer updates
            timerUpdate();

            // Kid
            kidUpdate();

            // FortuneTeller update
            fortuneTellerUpdate();

            // Spiritualist update
            spiritualistUpdate();

            // Chameleon update
            chameleonUpdate();

            // Yinyanger update
            yinyangerUpdate();

            // Challenger update
            challengerUpdate();

            // VigilantMira update
            vigilantMiraUpdate();

            // Custom gamemode report button update
            updateReportButton(__instance);

            // Janitor corpse moving
            janitorUpdate();

            // Capture the flag flags movement + fix if someone disconnnects
            captureTheFlagUpdate();

            // Police and thief jewel restore values if someone disconnnects
            policeandthiefUpdate();

            // King of the hill point time count
            kingOfTheHillUpdate();
        }
    }
}
