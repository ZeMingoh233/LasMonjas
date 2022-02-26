using HarmonyLib;
using Hazel;
using System;
using UnityEngine;
using static LasMonjas.LasMonjas;
using LasMonjas.Objects;
using System.Linq;
using LasMonjas.Core;

namespace LasMonjas
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Start))]
    static class HudManagerStartPatch
    {
        // Impostor buttons
        private static CustomButton mimicTransformButton;
        private static CustomButton painterPaintButton;
        private static CustomButton demonKillButton;
        private static CustomButton nunButton;
        public static CustomButton janitorCleanButton;
        public static CustomButton janitorDragBodyButton;
        private static CustomButton placeHatButton;
        private static CustomButton ilusionistLightsOutButton;
        public static CustomButton manipulatorManipulateButton;
        public static CustomButton bombermanBombButton;
        public static CustomButton chameleonInvisibleButton;
        public static CustomButton sorcererSpellButton;

        // Rebels buttons
        private static CustomButton renegadeKillButton;
        private static CustomButton renegadeMinionButton;
        private static CustomButton minionKillButton;
        private static CustomButton bountyHunterKillButton;
        private static CustomButton bountyHunterSetKillButton;
        private static CustomButton trapperMineButton;
        private static CustomButton trapperTrapButton;
        private static CustomButton trapperKillButton;
        private static CustomButton yinyangerYinButton;
        private static CustomButton yinyangerYangButton;
        private static CustomButton yinyangerKillButton;
        private static CustomButton challengerChallengeButton;
        private static CustomButton challengerKillButton;
        public static CustomButton challengerRockButton;
        public static CustomButton challengerPaperButton;
        public static CustomButton challengerScissorsButton;
        public static CustomButton rivalplayerRockButton;
        public static CustomButton rivalplayerPaperButton;
        public static CustomButton rivalplayerScissorsButton;

        // Neutral buttons
        private static CustomButton roleThiefStealButton;
        public static CustomButton pyromaniacButton;
        private static CustomButton treasureHunterButton;
        private static CustomButton devourerButton;

        // Crewmate buttons
        public static CustomButton captainCallButton;
        private static CustomButton mechanicRepairButton;
        private static CustomButton sheriffKillButton;
        private static CustomButton forensicButton;
        private static CustomButton timeTravelerShieldButton;
        private static CustomButton timeTravelerRewindTimeButton;
        private static CustomButton squireShieldButton;
        private static CustomButton fortuneTellerRevealButton;
        private static CustomButton hackerButton;
        private static CustomButton hackerVitalsButton;
        private static CustomButton hackerAdminTableButton;
        private static CustomButton sleuthLocatePlayerButton;
        private static CustomButton sleuthLocateCorpsesButton;
        public static CustomButton welderSealButton;
        public static CustomButton spiritualistReviveButton;
        public static CustomButton vigilantButton;
        public static CustomButton vigilantCamButton;
        private static CustomButton hunterButton;
        public static CustomButton jinxButton;


        // Capture the flag buttons
        private static CustomButton redplayer01KillButton;
        private static CustomButton redplayer01TakeFlagButton;
        private static CustomButton redplayer02KillButton;
        private static CustomButton redplayer02TakeFlagButton;
        private static CustomButton redplayer03KillButton;
        private static CustomButton redplayer03TakeFlagButton;
        private static CustomButton redplayer04KillButton;
        private static CustomButton redplayer04TakeFlagButton;
        private static CustomButton redplayer05KillButton;
        private static CustomButton redplayer05TakeFlagButton;
        private static CustomButton redplayer06KillButton;
        private static CustomButton redplayer06TakeFlagButton;
        private static CustomButton redplayer07KillButton;
        private static CustomButton redplayer07TakeFlagButton;
        private static CustomButton blueplayer01KillButton;
        private static CustomButton blueplayer01TakeFlagButton;
        private static CustomButton blueplayer02KillButton;
        private static CustomButton blueplayer02TakeFlagButton;
        private static CustomButton blueplayer03KillButton;
        private static CustomButton blueplayer03TakeFlagButton;
        private static CustomButton blueplayer04KillButton;
        private static CustomButton blueplayer04TakeFlagButton;
        private static CustomButton blueplayer05KillButton;
        private static CustomButton blueplayer05TakeFlagButton;
        private static CustomButton blueplayer06KillButton;
        private static CustomButton blueplayer06TakeFlagButton;
        private static CustomButton blueplayer07KillButton;
        private static CustomButton blueplayer07TakeFlagButton;
        private static CustomButton blueplayer08KillButton;
        private static CustomButton blueplayer08TakeFlagButton;

        // Police and Thief
        private static CustomButton policeplayer01JailButton;
        private static CustomButton policeplayer01KillButton;
        private static CustomButton policeplayer01LightButton;
        private static CustomButton policeplayer02JailButton;
        private static CustomButton policeplayer02KillButton;
        private static CustomButton policeplayer02LightButton;
        private static CustomButton policeplayer03JailButton;
        private static CustomButton policeplayer03KillButton;
        private static CustomButton policeplayer03LightButton;
        private static CustomButton policeplayer04JailButton;
        private static CustomButton policeplayer04KillButton;
        private static CustomButton policeplayer04LightButton;
        private static CustomButton policeplayer05JailButton;
        private static CustomButton policeplayer05KillButton;
        private static CustomButton policeplayer05LightButton;

        private static CustomButton thiefplayer01KillButton;
        private static CustomButton thiefplayer01FreeThiefButton;
        private static CustomButton thiefplayer01TakeDeliverJewelButton;
        private static CustomButton thiefplayer02KillButton;
        private static CustomButton thiefplayer02FreeThiefButton;
        private static CustomButton thiefplayer02TakeDeliverJewelButton;
        private static CustomButton thiefplayer03KillButton;
        private static CustomButton thiefplayer03FreeThiefButton;
        private static CustomButton thiefplayer03TakeDeliverJewelButton;
        private static CustomButton thiefplayer04KillButton;
        private static CustomButton thiefplayer04FreeThiefButton;
        private static CustomButton thiefplayer04TakeDeliverJewelButton;
        private static CustomButton thiefplayer05KillButton;
        private static CustomButton thiefplayer05FreeThiefButton;
        private static CustomButton thiefplayer05TakeDeliverJewelButton;
        private static CustomButton thiefplayer06KillButton;
        private static CustomButton thiefplayer06FreeThiefButton;
        private static CustomButton thiefplayer06TakeDeliverJewelButton;
        private static CustomButton thiefplayer07KillButton;
        private static CustomButton thiefplayer07FreeThiefButton;
        private static CustomButton thiefplayer07TakeDeliverJewelButton;
        private static CustomButton thiefplayer08KillButton;
        private static CustomButton thiefplayer08FreeThiefButton;
        private static CustomButton thiefplayer08TakeDeliverJewelButton;
        private static CustomButton thiefplayer09KillButton;
        private static CustomButton thiefplayer09FreeThiefButton;
        private static CustomButton thiefplayer09TakeDeliverJewelButton;
        private static CustomButton thiefplayer10KillButton;
        private static CustomButton thiefplayer10FreeThiefButton;
        private static CustomButton thiefplayer10TakeDeliverJewelButton;

        // King of the hill buttons
        private static CustomButton greenKingplayerKillButton;
        private static CustomButton greenKingplayerCaptureZoneButton;
        private static CustomButton greenplayer01KillButton;
        private static CustomButton greenplayer02KillButton;
        private static CustomButton greenplayer03KillButton;
        private static CustomButton greenplayer04KillButton;
        private static CustomButton greenplayer05KillButton;
        private static CustomButton greenplayer06KillButton;
        private static CustomButton yellowKingplayerKillButton;
        private static CustomButton yellowKingplayerCaptureZoneButton;
        private static CustomButton yellowplayer01KillButton;
        private static CustomButton yellowplayer02KillButton;
        private static CustomButton yellowplayer03KillButton;
        private static CustomButton yellowplayer04KillButton;
        private static CustomButton yellowplayer05KillButton;
        private static CustomButton yellowplayer06KillButton;
        private static CustomButton usurperPlayerKillButton;

        public static void setCustomButtonCooldowns() {
            // Impostor buttons
            mimicTransformButton.MaxTimer = Mimic.cooldown;
            mimicTransformButton.EffectDuration = Mimic.duration;
            painterPaintButton.MaxTimer = Painter.cooldown;
            painterPaintButton.EffectDuration = Painter.duration;
            demonKillButton.MaxTimer = Demon.cooldown;
            demonKillButton.EffectDuration = Demon.delay;
            nunButton.MaxTimer = 10f;
            janitorCleanButton.MaxTimer = Janitor.cooldown;
            janitorDragBodyButton.MaxTimer = 10;
            placeHatButton.MaxTimer = Ilusionist.placeHatCooldown;
            ilusionistLightsOutButton.MaxTimer = Ilusionist.lightsOutCooldown;
            ilusionistLightsOutButton.EffectDuration = Ilusionist.lightsOutDuration;
            ilusionistLightsOutButton.Timer = ilusionistLightsOutButton.MaxTimer;
            manipulatorManipulateButton.MaxTimer = Manipulator.cooldown;
            bombermanBombButton.MaxTimer = Bomberman.bombCooldown;
            bombermanBombButton.EffectDuration = Bomberman.bombDuration;
            chameleonInvisibleButton.MaxTimer = Chameleon.cooldown;
            chameleonInvisibleButton.EffectDuration = Chameleon.duration;
            sorcererSpellButton.MaxTimer = Sorcerer.cooldown;
            sorcererSpellButton.EffectDuration = Sorcerer.spellDuration;

            // Rebels buttons
            renegadeKillButton.MaxTimer = Renegade.cooldown;
            renegadeMinionButton.MaxTimer = Renegade.createMinionCooldown;
            minionKillButton.MaxTimer = Minion.cooldown;
            bountyHunterKillButton.MaxTimer = BountyHunter.cooldown;
            bountyHunterSetKillButton.MaxTimer = BountyHunter.cooldown;
            trapperMineButton.MaxTimer = Trapper.cooldown;
            trapperTrapButton.MaxTimer = Trapper.cooldown;
            trapperKillButton.MaxTimer = 30f;
            yinyangerYinButton.MaxTimer = Yinyanger.cooldown;
            yinyangerYangButton.MaxTimer = Yinyanger.cooldown;
            yinyangerKillButton.MaxTimer = 30f;
            challengerChallengeButton.MaxTimer = Challenger.cooldown;
            challengerChallengeButton.EffectDuration = Challenger.duration;
            challengerKillButton.MaxTimer = 30f;
            challengerRockButton.MaxTimer = 10f;
            challengerPaperButton.MaxTimer = 10f;
            challengerScissorsButton.MaxTimer = 10f;
            rivalplayerRockButton.MaxTimer = 10f;
            rivalplayerPaperButton.MaxTimer = 10f;
            rivalplayerScissorsButton.MaxTimer = 10f;

            // Neutral buttons
            roleThiefStealButton.MaxTimer = RoleThief.cooldown;
            pyromaniacButton.MaxTimer = Pyromaniac.cooldown;
            pyromaniacButton.EffectDuration = Pyromaniac.duration;
            treasureHunterButton.MaxTimer = TreasureHunter.cooldown;
            devourerButton.MaxTimer = Devourer.cooldown;

            // Crewmate buttons
            captainCallButton.MaxTimer = 10f;
            mechanicRepairButton.MaxTimer = 10f;
            sheriffKillButton.MaxTimer = Sheriff.cooldown;
            forensicButton.MaxTimer = Forensic.cooldown;
            forensicButton.EffectDuration = Forensic.duration;
            timeTravelerShieldButton.MaxTimer = TimeTraveler.cooldown;
            timeTravelerRewindTimeButton.MaxTimer = 30f;
            timeTravelerShieldButton.EffectDuration = TimeTraveler.shieldDuration;
            squireShieldButton.MaxTimer = 10f;
            fortuneTellerRevealButton.MaxTimer = FortuneTeller.cooldown;
            fortuneTellerRevealButton.EffectDuration = FortuneTeller.duration;
            hackerButton.MaxTimer = Hacker.cooldown;
            hackerButton.EffectDuration = Hacker.duration;
            hackerVitalsButton.MaxTimer = Hacker.cooldown;
            hackerAdminTableButton.MaxTimer = Hacker.cooldown;
            hackerVitalsButton.EffectDuration = Hacker.duration;
            hackerAdminTableButton.EffectDuration = Hacker.duration;
            sleuthLocatePlayerButton.MaxTimer = 10f;
            sleuthLocateCorpsesButton.MaxTimer = Sleuth.corpsesPathfindCooldown;
            sleuthLocateCorpsesButton.EffectDuration = Sleuth.corpsesPathfindDuration;
            welderSealButton.MaxTimer = Welder.cooldown;
            spiritualistReviveButton.MaxTimer = 30f;
            spiritualistReviveButton.EffectDuration = Spiritualist.spiritualistReviveTime;
            vigilantButton.MaxTimer = Vigilant.cooldown;
            vigilantCamButton.MaxTimer = Vigilant.cooldown;
            vigilantCamButton.EffectDuration = Vigilant.duration; 
            hunterButton.MaxTimer = 10f;
            jinxButton.MaxTimer = Jinx.cooldown;

            // Remaining uses text
            Mechanic.mechanicRepairButtonText.text = $"{Mechanic.numberOfRepairs - Mechanic.timesUsedRepairs} / {Mechanic.numberOfRepairs}";
            FortuneTeller.fortuneTellerRevealButtonText.text = $"{FortuneTeller.numberOfFortunes - FortuneTeller.timesUsedFortune} / {FortuneTeller.numberOfFortunes}";
            Welder.welderButtonText.text = $"{Welder.remainingWelds} / {Welder.totalWelds}";
            Vigilant.vigilantButtonCameraText.text = $"{Vigilant.remainingCameras} / {Vigilant.totalCameras}";
            Vigilant.vigilantButtonCameraUsesText.text = $"{Vigilant.charges} / {Vigilant.maxCharges}";
            Jinx.jinxButtonJinxsText.text = $"{Jinx.jinxNumber - Jinx.jinxs} / {Jinx.jinxNumber}";
            Hacker.hackerAdminTableChargesText.text = $"{Hacker.chargesAdminTable} / {Hacker.toolsNumber}";
            Hacker.hackerVitalsChargesText.text = $"{Hacker.chargesVitals} / {Hacker.toolsNumber}";

            // Capture the flag buttons
            redplayer01KillButton.MaxTimer = CaptureTheFlag.killCooldown;
            redplayer01TakeFlagButton.MaxTimer = 0;
            redplayer02KillButton.MaxTimer = CaptureTheFlag.killCooldown;
            redplayer02TakeFlagButton.MaxTimer = 0;
            redplayer03KillButton.MaxTimer = CaptureTheFlag.killCooldown;
            redplayer03TakeFlagButton.MaxTimer = 0;
            redplayer04KillButton.MaxTimer = CaptureTheFlag.killCooldown;
            redplayer04TakeFlagButton.MaxTimer = 0;
            redplayer05KillButton.MaxTimer = CaptureTheFlag.killCooldown;
            redplayer05TakeFlagButton.MaxTimer = 0;
            redplayer06KillButton.MaxTimer = CaptureTheFlag.killCooldown;
            redplayer06TakeFlagButton.MaxTimer = 0;
            redplayer07KillButton.MaxTimer = CaptureTheFlag.killCooldown;
            redplayer07TakeFlagButton.MaxTimer = 0;
            blueplayer01KillButton.MaxTimer = CaptureTheFlag.killCooldown;
            blueplayer01TakeFlagButton.MaxTimer = 0;
            blueplayer02KillButton.MaxTimer = CaptureTheFlag.killCooldown;
            blueplayer02TakeFlagButton.MaxTimer = 0;
            blueplayer03KillButton.MaxTimer = CaptureTheFlag.killCooldown;
            blueplayer03TakeFlagButton.MaxTimer = 0;
            blueplayer04KillButton.MaxTimer = CaptureTheFlag.killCooldown;
            blueplayer04TakeFlagButton.MaxTimer = 0;
            blueplayer05KillButton.MaxTimer = CaptureTheFlag.killCooldown;
            blueplayer05TakeFlagButton.MaxTimer = 0;
            blueplayer06KillButton.MaxTimer = CaptureTheFlag.killCooldown;
            blueplayer06TakeFlagButton.MaxTimer = 0;
            blueplayer07KillButton.MaxTimer = CaptureTheFlag.killCooldown;
            blueplayer07TakeFlagButton.MaxTimer = 0;
            blueplayer08KillButton.MaxTimer = CaptureTheFlag.killCooldown;
            blueplayer08TakeFlagButton.MaxTimer = 0;

            // Police And Thief buttons
            policeplayer01KillButton.MaxTimer = PoliceAndThief.policeKillCooldown;
            policeplayer01JailButton.MaxTimer = PoliceAndThief.policeCatchCooldown;
            policeplayer01JailButton.EffectDuration = PoliceAndThief.captureThiefTime;
            policeplayer01LightButton.MaxTimer = 20;
            policeplayer01LightButton.EffectDuration = 10;
            policeplayer02KillButton.MaxTimer = PoliceAndThief.policeKillCooldown;
            policeplayer02JailButton.MaxTimer = PoliceAndThief.policeCatchCooldown;
            policeplayer02JailButton.EffectDuration = PoliceAndThief.captureThiefTime;
            policeplayer02LightButton.MaxTimer = 20;
            policeplayer02LightButton.EffectDuration = 10;
            policeplayer03KillButton.MaxTimer = PoliceAndThief.policeKillCooldown;
            policeplayer03JailButton.MaxTimer = PoliceAndThief.policeCatchCooldown;
            policeplayer03JailButton.EffectDuration = PoliceAndThief.captureThiefTime;
            policeplayer03LightButton.MaxTimer = 20;
            policeplayer03LightButton.EffectDuration = 10;
            policeplayer04KillButton.MaxTimer = PoliceAndThief.policeKillCooldown;
            policeplayer04JailButton.MaxTimer = PoliceAndThief.policeCatchCooldown;
            policeplayer04JailButton.EffectDuration = PoliceAndThief.captureThiefTime;
            policeplayer04LightButton.MaxTimer = 20;
            policeplayer04LightButton.EffectDuration = 10;
            policeplayer05KillButton.MaxTimer = PoliceAndThief.policeKillCooldown;
            policeplayer05JailButton.MaxTimer = PoliceAndThief.policeCatchCooldown;
            policeplayer05JailButton.EffectDuration = PoliceAndThief.captureThiefTime;
            policeplayer05LightButton.MaxTimer = 20;
            policeplayer05LightButton.EffectDuration = 10;

            thiefplayer01KillButton.MaxTimer = PoliceAndThief.thiefKillCooldown;
            thiefplayer01FreeThiefButton.MaxTimer = 20f;
            thiefplayer01TakeDeliverJewelButton.MaxTimer = 5f;
            thiefplayer02KillButton.MaxTimer = PoliceAndThief.thiefKillCooldown;
            thiefplayer02FreeThiefButton.MaxTimer = 20f;
            thiefplayer02TakeDeliverJewelButton.MaxTimer = 5f;
            thiefplayer03KillButton.MaxTimer = PoliceAndThief.thiefKillCooldown;
            thiefplayer03FreeThiefButton.MaxTimer = 20f;
            thiefplayer03TakeDeliverJewelButton.MaxTimer = 5f;
            thiefplayer04KillButton.MaxTimer = PoliceAndThief.thiefKillCooldown;
            thiefplayer04FreeThiefButton.MaxTimer = 20f;
            thiefplayer04TakeDeliverJewelButton.MaxTimer = 5f;
            thiefplayer05KillButton.MaxTimer = PoliceAndThief.thiefKillCooldown;
            thiefplayer05FreeThiefButton.MaxTimer = 20f;
            thiefplayer05TakeDeliverJewelButton.MaxTimer = 5f;
            thiefplayer06KillButton.MaxTimer = PoliceAndThief.thiefKillCooldown;
            thiefplayer06FreeThiefButton.MaxTimer = 20f;
            thiefplayer06TakeDeliverJewelButton.MaxTimer = 5f;
            thiefplayer07KillButton.MaxTimer = PoliceAndThief.thiefKillCooldown;
            thiefplayer07FreeThiefButton.MaxTimer = 20f;
            thiefplayer07TakeDeliverJewelButton.MaxTimer = 5f;
            thiefplayer08KillButton.MaxTimer = PoliceAndThief.thiefKillCooldown;
            thiefplayer08FreeThiefButton.MaxTimer = 20f;
            thiefplayer08TakeDeliverJewelButton.MaxTimer = 5f;
            thiefplayer09KillButton.MaxTimer = PoliceAndThief.thiefKillCooldown;
            thiefplayer09FreeThiefButton.MaxTimer = 20f;
            thiefplayer09TakeDeliverJewelButton.MaxTimer = 5f;
            thiefplayer10KillButton.MaxTimer = PoliceAndThief.thiefKillCooldown;
            thiefplayer10FreeThiefButton.MaxTimer = 20f;
            thiefplayer10TakeDeliverJewelButton.MaxTimer = 5f;

            // King of the hill buttons
            greenKingplayerCaptureZoneButton.MaxTimer = KingOfTheHill.captureCooldown;
            greenKingplayerKillButton.MaxTimer = KingOfTheHill.killCooldown;
            greenplayer01KillButton.MaxTimer = KingOfTheHill.killCooldown;
            greenplayer02KillButton.MaxTimer = KingOfTheHill.killCooldown;
            greenplayer03KillButton.MaxTimer = KingOfTheHill.killCooldown;
            greenplayer04KillButton.MaxTimer = KingOfTheHill.killCooldown;
            greenplayer05KillButton.MaxTimer = KingOfTheHill.killCooldown;
            greenplayer06KillButton.MaxTimer = KingOfTheHill.killCooldown;
            yellowKingplayerCaptureZoneButton.MaxTimer = KingOfTheHill.captureCooldown;
            yellowKingplayerKillButton.MaxTimer = KingOfTheHill.killCooldown;
            yellowplayer01KillButton.MaxTimer = KingOfTheHill.killCooldown;
            yellowplayer02KillButton.MaxTimer = KingOfTheHill.killCooldown;
            yellowplayer03KillButton.MaxTimer = KingOfTheHill.killCooldown;
            yellowplayer04KillButton.MaxTimer = KingOfTheHill.killCooldown;
            yellowplayer05KillButton.MaxTimer = KingOfTheHill.killCooldown;
            yellowplayer06KillButton.MaxTimer = KingOfTheHill.killCooldown;
            usurperPlayerKillButton.MaxTimer = KingOfTheHill.killCooldown;
        }

        public static void resetBomberBombButton() {
            bombermanBombButton.Timer = bombermanBombButton.MaxTimer;
            bombermanBombButton.isEffectActive = false;
            bombermanBombButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
        }

        public static void resetTimeTravelerButton() {
            timeTravelerShieldButton.Timer = timeTravelerShieldButton.MaxTimer;
            timeTravelerShieldButton.isEffectActive = false;
            timeTravelerShieldButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
        }
        public static void resetSpiritualistReviveButton() {
            spiritualistReviveButton.Timer = 0f;
            spiritualistReviveButton.isEffectActive = false;
        }

        public static void resetDuelButtons() {
            challengerRockButton.Timer = 10f;
            challengerPaperButton.Timer = 10f;
            challengerScissorsButton.Timer = 10f;
            rivalplayerRockButton.Timer = 10f;
            rivalplayerPaperButton.Timer = 10f;
            rivalplayerScissorsButton.Timer = 10f;
        }

        public static void Postfix(HudManager __instance) {
            // Impostor buttons code

            // Mimic transform
            mimicTransformButton = new CustomButton(
                () => {
                    if (Mimic.pickTarget != null) {
                        if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Mimic.mimic.Data.PlayerId)) {
                            MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                            writerKiller.Write(Mimic.mimic.PlayerId);
                            writerKiller.Write((byte)0);
                            AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                            RPCProcedure.setJinxed(Mimic.mimic.PlayerId, 0);

                            SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                            Mimic.pickTarget = null;
                            Mimic.duration = quackNumber;
                            mimicTransformButton.EffectDuration = Mimic.duration;
                            return;
                        }

                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.MimicTransform, Hazel.SendOption.Reliable, -1);
                        writer.Write(Mimic.pickTarget.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.mimicTransform(Mimic.pickTarget.PlayerId);
                        Mimic.pickTarget = null;
                        Mimic.duration = Mimic.backUpduration;
                        mimicTransformButton.EffectDuration = Mimic.duration;
                    }
                    else if (Mimic.currentTarget != null) {

                        Mimic.pickTarget = Mimic.currentTarget;
                        mimicTransformButton.Sprite = Mimic.getTransformSprite();
                        mimicTransformButton.EffectDuration = 1f;
                    }
                },
                () => { return Mimic.mimic != null && Mimic.mimic == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return (Mimic.currentTarget || Mimic.pickTarget) && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => {
                    mimicTransformButton.Timer = mimicTransformButton.MaxTimer;
                    mimicTransformButton.Sprite = Mimic.getpickTargetSprite();
                    mimicTransformButton.isEffectActive = false;
                    mimicTransformButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                    Mimic.pickTarget = null;
                },
                Mimic.getpickTargetSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T,
                true,
                Mimic.duration,
                () => {
                    if (Mimic.pickTarget == null) {
                        mimicTransformButton.Timer = mimicTransformButton.MaxTimer;
                        mimicTransformButton.Sprite = Mimic.getpickTargetSprite();
                    }
                }
            );

            // Painter paint
            painterPaintButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Painter.painter.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Painter.painter.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Painter.painter.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        Painter.duration = quackNumber;
                        painterPaintButton.EffectDuration = Painter.duration;
                        return;
                    }

                    int colorNumber = rnd.Next(1, 18);

                    Painter.duration = Painter.backUpduration;
                    painterPaintButton.EffectDuration = Painter.duration;
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PainterPaint, Hazel.SendOption.Reliable, -1);
                    writer.Write(colorNumber);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.painterPaint(colorNumber);
                },
                () => { return Painter.painter != null && Painter.painter == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => {
                    painterPaintButton.Timer = painterPaintButton.MaxTimer;
                    painterPaintButton.isEffectActive = false;
                    painterPaintButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                Painter.getButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T,
                true,
                Painter.duration,
                () => { painterPaintButton.Timer = painterPaintButton.MaxTimer; }
            );

            // Demon bite
            demonKillButton = new CustomButton(
                () => {
                    MurderAttemptResult murder = Helpers.checkMurderAttempt(Demon.demon, Demon.currentTarget);
                    if (murder == MurderAttemptResult.PerformKill) {
                        if (Demon.targetNearNun) {
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UncheckedMurderPlayer, Hazel.SendOption.Reliable, -1);
                            writer.Write(Demon.demon.PlayerId);
                            writer.Write(Demon.currentTarget.PlayerId);
                            writer.Write(Byte.MaxValue);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.uncheckedMurderPlayer(Demon.demon.PlayerId, Demon.currentTarget.PlayerId, Byte.MaxValue);

                            demonKillButton.HasEffect = false;
                            demonKillButton.Timer = demonKillButton.MaxTimer + 20f;
                        }
                        else {
                            Demon.bitten = Demon.currentTarget;
                            
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.DemonSetBitten, Hazel.SendOption.Reliable, -1);
                            writer.Write(Demon.bitten.PlayerId);
                            writer.Write((byte)0);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.demonSetBitten(Demon.bitten.PlayerId, 0);

                            HudManager.Instance.StartCoroutine(Effects.Lerp(Demon.delay, new Action<float>((p) => { 
                                if (p == 1f) {
                                   
                                    MurderAttemptResult murder = Helpers.checkMurderAttemptAndKill(Demon.demon, Demon.bitten, showAnimation: false);
                                    if (murder == MurderAttemptResult.JinxKill) {
                                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                                    }
                                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.DemonSetBitten, Hazel.SendOption.Reliable, -1);
                                    writer.Write(byte.MaxValue);
                                    writer.Write(byte.MaxValue);
                                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                                    RPCProcedure.demonSetBitten(byte.MaxValue, byte.MaxValue);
                                }
                            })));

                            demonKillButton.HasEffect = true;
                        }
                    }
                    else if (murder == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        demonKillButton.Timer = demonKillButton.MaxTimer;
                        demonKillButton.HasEffect = false;
                    }
                    else {
                        demonKillButton.HasEffect = false;
                    }
                },
                () => { return Demon.demon != null && Demon.demon == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    int currentAlivePlayers = 0;
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                        if (!player.Data.IsDead) {
                            currentAlivePlayers += 1;
                        }
                    }
                    if (Demon.targetNearNun && Demon.canKillNearNun || currentAlivePlayers <= 2) {
                        demonKillButton.actionButton.graphic.sprite = __instance.KillButton.graphic.sprite;
                        demonKillButton.showButtonText = true;
                    }
                    else {
                        demonKillButton.actionButton.graphic.sprite = Demon.getButtonSprite();
                        demonKillButton.showButtonText = false;
                    }
                    return Demon.currentTarget != null && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling && ((!Demon.targetNearNun || Demon.canKillNearNun) || currentAlivePlayers <= 2);
                },
                () => {
                    demonKillButton.Timer = demonKillButton.MaxTimer;
                    demonKillButton.isEffectActive = false;
                    demonKillButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                Demon.getButtonSprite(),
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q,
                false,
                0f,
                () => {
                    demonKillButton.Timer = demonKillButton.MaxTimer;
                }
            );

            // Nun button only if there's Demon ingame
            nunButton = new CustomButton(
                () => {
                    Demon.localPlacedNun = true;
                    var pos = PlayerControl.LocalPlayer.transform.position;
                    byte[] buff = new byte[sizeof(float) * 2];
                    Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, buff, 0 * sizeof(float), sizeof(float));
                    Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, buff, 1 * sizeof(float), sizeof(float));

                    MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlaceNun, Hazel.SendOption.Reliable);
                    writer.WriteBytesAndSize(buff);
                    writer.EndMessage();
                    RPCProcedure.placeNun(buff);
                },
                () => { return !Demon.localPlacedNun && !PlayerControl.LocalPlayer.Data.IsDead && Demon.nunsActive && Demon.demon != null && !Challenger.isDueling; },
                () => { return PlayerControl.LocalPlayer.CanMove && !Demon.localPlacedNun; },
                () => { },
                Demon.getNunButtonSprite(),
                new Vector3(0, -0.06f, 0),
                __instance,
                null,
                true
            );

            // Janitor clean body
            janitorCleanButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Janitor.janitor.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Janitor.janitor.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Janitor.janitor.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        janitorCleanButton.Timer = janitorCleanButton.MaxTimer;
                        return;
                    }

                    foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(PlayerControl.LocalPlayer.GetTruePosition(), 1f, Constants.PlayersOnlyMask)) {
                        if (collider2D.tag == "DeadBody") {
                            DeadBody component = collider2D.GetComponent<DeadBody>();
                            if (component && !component.Reported) {
                                Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
                                Vector2 truePosition2 = component.TruePosition;
                                if (Vector2.Distance(truePosition2, truePosition) <= PlayerControl.LocalPlayer.MaxReportDistance && PlayerControl.LocalPlayer.CanMove && !PhysicsHelpers.AnythingBetween(truePosition, truePosition2, Constants.ShipAndObjectsMask, false)) {
                                    GameData.PlayerInfo playerInfo = GameData.Instance.GetPlayerById(component.ParentId);

                                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.RemoveBody, Hazel.SendOption.Reliable, -1);
                                    writer.Write(playerInfo.PlayerId);
                                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                                    RPCProcedure.removeBody(playerInfo.PlayerId);

                                    Janitor.janitor.killTimer = janitorCleanButton.Timer = janitorCleanButton.MaxTimer;
                                    break;
                                }
                            }
                        }
                    }
                },
                () => { return Janitor.janitor != null && Janitor.janitor == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    bool canClean = false;
                    foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(PlayerControl.LocalPlayer.GetTruePosition(), 1f, Constants.PlayersOnlyMask))
                        if (collider2D.tag == "DeadBody")
                            canClean = true;
                    return canClean && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling && !Janitor.dragginBody;
                },
                () => { janitorCleanButton.Timer = janitorCleanButton.MaxTimer; },
                Janitor.getButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                null
            );

            // Janitor dragbody button
            janitorDragBodyButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Janitor.janitor.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Janitor.janitor.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Janitor.janitor.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        janitorDragBodyButton.Timer = janitorDragBodyButton.MaxTimer;
                        return;
                    }

                    if (Janitor.dragginBody) {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.DragPlaceBody, Hazel.SendOption.Reliable, -1);
                        writer.Write(Janitor.bodyId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.dragPlaceBody(Janitor.bodyId);

                        janitorDragBodyButton.Timer = janitorDragBodyButton.MaxTimer;
                    }
                    else {
                        foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(PlayerControl.LocalPlayer.GetTruePosition(), 1f, Constants.PlayersOnlyMask)) {
                            if (collider2D.tag == "DeadBody") {
                                DeadBody component = collider2D.GetComponent<DeadBody>();
                                if (component && !component.Reported) {
                                    Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
                                    Vector2 truePosition2 = component.TruePosition;
                                    if (Vector2.Distance(truePosition2, truePosition) <= PlayerControl.LocalPlayer.MaxReportDistance && PlayerControl.LocalPlayer.CanMove && !PhysicsHelpers.AnythingBetween(truePosition, truePosition2, Constants.ShipAndObjectsMask, false)) {
                                        GameData.PlayerInfo playerInfo = GameData.Instance.GetPlayerById(component.ParentId);
                                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.DragPlaceBody, Hazel.SendOption.Reliable, -1);
                                        writer.Write(playerInfo.PlayerId);
                                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                                        RPCProcedure.dragPlaceBody(playerInfo.PlayerId);

                                        janitorDragBodyButton.Timer = 1;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                },
                () => { return Janitor.janitor != null && Janitor.janitor == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (Janitor.dragginBody)
                        janitorDragBodyButton.actionButton.graphic.sprite = Janitor.getMoveBodyButtonSprite();
                    else
                        janitorDragBodyButton.actionButton.graphic.sprite = Janitor.getDragButtonSprite();
                    bool canDrag = false;
                    foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(PlayerControl.LocalPlayer.GetTruePosition(), 1f, Constants.PlayersOnlyMask))
                        if (collider2D.tag == "DeadBody")
                            canDrag = true;
                    return canDrag && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling;
                },
                () => {
                    janitorDragBodyButton.Timer = janitorDragBodyButton.MaxTimer;
                    Janitor.dragginBody = false;
                    Janitor.bodyId = 0;
                },
                Janitor.getDragButtonSprite(),
                new Vector3(-3f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Ilusionist place hats
            placeHatButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Ilusionist.ilusionist.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Ilusionist.ilusionist.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Ilusionist.ilusionist.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        placeHatButton.Timer = placeHatButton.MaxTimer;
                        return;
                    }

                    placeHatButton.Timer = placeHatButton.MaxTimer;

                    var pos = PlayerControl.LocalPlayer.transform.position;
                    byte[] buff = new byte[sizeof(float) * 2];
                    Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, buff, 0 * sizeof(float), sizeof(float));
                    Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, buff, 1 * sizeof(float), sizeof(float));

                    MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlaceHat, Hazel.SendOption.Reliable);
                    writer.WriteBytesAndSize(buff);
                    writer.EndMessage();
                    RPCProcedure.placeHat(buff);
                },
                () => { return Ilusionist.ilusionist != null && Ilusionist.ilusionist == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && !Hats.hasHatLimitReached(); },
                () => { return PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling && !Hats.hasHatLimitReached(); },
                () => { placeHatButton.Timer = placeHatButton.MaxTimer; },
                Ilusionist.getPlaceHatButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Ilusionist light button
            ilusionistLightsOutButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Ilusionist.ilusionist.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Ilusionist.ilusionist.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Ilusionist.ilusionist.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        Ilusionist.lightsOutDuration = quackNumber;
                        ilusionistLightsOutButton.EffectDuration = Ilusionist.lightsOutDuration;
                        ilusionistLightsOutButton.Timer = ilusionistLightsOutButton.MaxTimer;
                        return;
                    }

                    Ilusionist.lightsOutDuration = Ilusionist.backUpduration;
                    ilusionistLightsOutButton.EffectDuration = Ilusionist.lightsOutDuration;
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.LightsOut, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.lightsOut();
                },
                () => { return Ilusionist.ilusionist != null && Ilusionist.ilusionist == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && Hats.hasHatLimitReached() && Hats.hatsConvertedToVents; },
                () => {
                    bool sabotageActive = false;
                    if (Bomberman.activeBomb == true || Ilusionist.lightsOutTimer > 0) {
                        sabotageActive = true;
                    }
                    else {
                        foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
                            if (task.TaskType == TaskTypes.FixLights || task.TaskType == TaskTypes.RestoreOxy || task.TaskType == TaskTypes.ResetReactor || task.TaskType == TaskTypes.ResetSeismic || task.TaskType == TaskTypes.FixComms || task.TaskType == TaskTypes.StopCharles)
                                sabotageActive = true;
                    }
                    return !sabotageActive && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling && Hats.hasHatLimitReached() && Hats.hatsConvertedToVents;
                },
                () => {
                    ilusionistLightsOutButton.Timer = ilusionistLightsOutButton.MaxTimer;
                    ilusionistLightsOutButton.isEffectActive = false;
                    ilusionistLightsOutButton.actionButton.graphic.color = Palette.EnabledColor;
                },
                Ilusionist.getLightsOutButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T,
                true,
                Ilusionist.lightsOutDuration,
                () => { ilusionistLightsOutButton.Timer = ilusionistLightsOutButton.MaxTimer; }
            );

            // Manipulator manipulate
            manipulatorManipulateButton = new CustomButton(
                () => {
                    if (Manipulator.manipulatedVictim == null) {
                        
                        Manipulator.manipulatedVictim = Manipulator.currentTarget;
                        manipulatorManipulateButton.Sprite = Manipulator.getManipulateButtonSprite();
                        manipulatorManipulateButton.Timer = 1f;
                    }
                    else if (Manipulator.manipulatedVictim != null && Manipulator.manipulatedVictimTarget != null) {
                        MurderAttemptResult murder = Helpers.checkMurderAttemptAndKill(Manipulator.manipulator, Manipulator.manipulatedVictimTarget, showAnimation: false);
                        if (murder == MurderAttemptResult.JinxKill) {
                            SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                            manipulatorManipulateButton.Timer = manipulatorManipulateButton.MaxTimer;
                            manipulatorManipulateButton.Sprite = Manipulator.getManipulateKillButtonSprite();
                            Manipulator.manipulatedVictim = null;
                            Manipulator.manipulatedVictimTarget = null;
                            return;
                        }
                        else if (murder == MurderAttemptResult.SuppressKill) {
                            return;
                        }                       

                        Manipulator.manipulatedVictim = null;
                        Manipulator.manipulatedVictimTarget = null;
                        manipulatorManipulateButton.Sprite = Manipulator.getManipulateButtonSprite();
                        Manipulator.manipulator.killTimer = manipulatorManipulateButton.Timer = manipulatorManipulateButton.MaxTimer;
                        manipulatorManipulateButton.Timer = manipulatorManipulateButton.MaxTimer + 20f;

                    }
                },
                () => { return Manipulator.manipulator != null && Manipulator.manipulator == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return ((Manipulator.manipulatedVictim == null && Manipulator.currentTarget != null) || (Manipulator.manipulatedVictim != null && Manipulator.manipulatedVictimTarget != null)) && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => {
                    manipulatorManipulateButton.Timer = manipulatorManipulateButton.MaxTimer;
                    manipulatorManipulateButton.Sprite = Manipulator.getManipulateButtonSprite();
                    Manipulator.manipulatedVictim = null;
                    Manipulator.manipulatedVictimTarget = null;
                },
                Manipulator.getManipulateButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            //Bomberman place bomb
            bombermanBombButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Bomberman.bomberman.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Bomberman.bomberman.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Bomberman.bomberman.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        Bomberman.bombDuration = 0f;
                        bombermanBombButton.EffectDuration = Bomberman.bombDuration;
                        return;
                    }

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

                    bombermanBombButton.EffectDuration = Bomberman.bombDuration;

                    SoundManager.Instance.PlaySound(CustomMain.customAssets.bombermanPlaceBombClip, false, 100f);
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlaceBomb, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.placeBomb();
                },
                () => { return Bomberman.bomberman != null && Bomberman.bomberman == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    bool closetoPlayer = false;
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                        if (Vector2.Distance(player.transform.position, Bomberman.bomberman.transform.position) < 1.5f && player != Bomberman.bomberman && !player.Data.IsDead) {
                            closetoPlayer = true;
                        }
                    }
                    bool sabotageActive = false;
                    foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
                        if (task.TaskType == TaskTypes.FixLights || task.TaskType == TaskTypes.RestoreOxy || task.TaskType == TaskTypes.ResetReactor || task.TaskType == TaskTypes.ResetSeismic || task.TaskType == TaskTypes.FixComms || task.TaskType == TaskTypes.StopCharles)
                            sabotageActive = true;
                    return !closetoPlayer && !sabotageActive && PlayerControl.LocalPlayer.CanMove && !Bomberman.activeBomb && !Challenger.isDueling && Ilusionist.lightsOutTimer <= 0;
                },
                () => {
                    bombermanBombButton.Timer = bombermanBombButton.MaxTimer;
                    bombermanBombButton.isEffectActive = false;
                    bombermanBombButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                Bomberman.getBombButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T,
                true,
                Bomberman.bombDuration,
                () => { bombermanBombButton.Timer = bombermanBombButton.MaxTimer; }
            );

            // Chameleon invisible
            chameleonInvisibleButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Chameleon.chameleon.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Chameleon.chameleon.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Chameleon.chameleon.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        Chameleon.duration = quackNumber;
                        chameleonInvisibleButton.EffectDuration = Chameleon.duration;
                        chameleonInvisibleButton.Timer = chameleonInvisibleButton.MaxTimer;
                        return;
                    }

                    Chameleon.duration = Chameleon.backUpduration;
                    chameleonInvisibleButton.EffectDuration = Chameleon.duration;
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ChameleonInvisible, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.chameleonInvisible();
                },
                () => { return Chameleon.chameleon != null && Chameleon.chameleon == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && PlayerControl.LocalPlayer.Data.Role.IsImpostor; },
                () => { return PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => {
                    chameleonInvisibleButton.Timer = chameleonInvisibleButton.MaxTimer;
                    chameleonInvisibleButton.isEffectActive = false;
                    chameleonInvisibleButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                Chameleon.getInvisibleButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T,
                true,
                0f,
                () => {
                    chameleonInvisibleButton.Timer = chameleonInvisibleButton.MaxTimer;
                }
            );

            // Sorcerer Spell button
            sorcererSpellButton = new CustomButton(
                () => {
                    if (Sorcerer.currentTarget != null) {
                        Sorcerer.spellTarget = Sorcerer.currentTarget;
                    }
                },
                () => { return Sorcerer.sorcerer != null && Sorcerer.sorcerer == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (sorcererSpellButton.isEffectActive && Sorcerer.spellTarget != Sorcerer.currentTarget) {
                        Sorcerer.spellTarget = null;
                        sorcererSpellButton.Timer = 0f;
                        sorcererSpellButton.isEffectActive = false;
                    }
                    return PlayerControl.LocalPlayer.CanMove && Sorcerer.currentTarget != null && !Challenger.isDueling;
                },
                () => {
                    sorcererSpellButton.Timer = sorcererSpellButton.MaxTimer;
                    sorcererSpellButton.isEffectActive = false;
                    Sorcerer.spellTarget = null;
                    Sorcerer.cooldownAddition = Sorcerer.cooldownAdditionInitial;
                },
                Sorcerer.getSpellButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T,
                true,
                Sorcerer.spellDuration,
                () => {
                    if (Sorcerer.spellTarget == null) return;
                    MurderAttemptResult attempt = Helpers.checkMurderAttempt(Sorcerer.sorcerer, Sorcerer.spellTarget);
                    if (attempt == MurderAttemptResult.PerformKill) {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetSpelledPlayer, Hazel.SendOption.Reliable, -1);
                        writer.Write(Sorcerer.currentTarget.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.setSpelledPlayer(Sorcerer.currentTarget.PlayerId);
                    }
                    if (attempt == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        sorcererSpellButton.Timer = sorcererSpellButton.MaxTimer;
                        Sorcerer.sorcerer.killTimer = PlayerControl.GameOptions.KillCooldown;
                    }
                    else if (attempt == MurderAttemptResult.PerformKill) {
                        sorcererSpellButton.MaxTimer += Sorcerer.cooldownAddition;
                        sorcererSpellButton.Timer = sorcererSpellButton.MaxTimer;
                        Sorcerer.sorcerer.killTimer = PlayerControl.GameOptions.KillCooldown;
                    }
                    else {
                        sorcererSpellButton.Timer = 0f;
                    }
                    Sorcerer.spellTarget = null;
                }
            );


            // Rebels buttons

            // Renegade Kill
            renegadeKillButton = new CustomButton(
                () => {
                    MurderAttemptResult murderAttemptResult = Helpers.checkMurderAttempt(Renegade.renegade, Renegade.currentTarget);
                    if (murderAttemptResult == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        renegadeKillButton.Timer = renegadeKillButton.MaxTimer;
                        Renegade.currentTarget = null;
                        return;
                    }
                    else if (murderAttemptResult == MurderAttemptResult.SuppressKill) {
                        return;
                    }

                    if (murderAttemptResult == MurderAttemptResult.PerformKill) {
                        
                        MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UncheckedMurderPlayer, Hazel.SendOption.Reliable, -1);
                        killWriter.Write(Renegade.renegade.Data.PlayerId);
                        killWriter.Write(Renegade.currentTarget.PlayerId);
                        killWriter.Write(byte.MaxValue);
                        AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                        RPCProcedure.uncheckedMurderPlayer(Renegade.renegade.Data.PlayerId, Renegade.currentTarget.PlayerId, Byte.MaxValue);
                    }

                    renegadeKillButton.Timer = renegadeKillButton.MaxTimer;
                    Renegade.currentTarget = null; 
                },
                () => { return Renegade.renegade != null && Renegade.renegade == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return Renegade.currentTarget && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => { renegadeKillButton.Timer = renegadeKillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Renegade recruit
            renegadeMinionButton = new CustomButton(
                () => {
                    if (Helpers.checkMurderAttempt(Renegade.renegade, Renegade.currentTarget) == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        renegadeMinionButton.Timer = renegadeMinionButton.MaxTimer;
                        Renegade.currentTarget = null;
                        return;
                    }

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.RenegadeRecruitMinion, Hazel.SendOption.Reliable, -1);
                    writer.Write(Renegade.currentTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.renegadeRecruitMinion(Renegade.currentTarget.PlayerId);
                },
                () => { return Renegade.canRecruitMinion && Renegade.renegade != null && Renegade.renegade == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && !Renegade.usedRecruit; },
                () => { return Renegade.canRecruitMinion && Renegade.currentTarget != null && PlayerControl.LocalPlayer.CanMove && !Renegade.usedRecruit && !Challenger.isDueling; },
                () => { renegadeMinionButton.Timer = renegadeMinionButton.MaxTimer; },
                Renegade.getMinionButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Minion Kill
            minionKillButton = new CustomButton(
                () => {
                    MurderAttemptResult murderAttemptResult = Helpers.checkMurderAttempt(Minion.minion, Minion.currentTarget);
                    if (murderAttemptResult == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        minionKillButton.Timer = minionKillButton.MaxTimer;
                        Minion.currentTarget = null;
                        return;
                    }
                    else if (murderAttemptResult == MurderAttemptResult.SuppressKill) {
                        return;
                    }

                    if (murderAttemptResult == MurderAttemptResult.PerformKill) {

                        MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UncheckedMurderPlayer, Hazel.SendOption.Reliable, -1);
                        killWriter.Write(Minion.minion.Data.PlayerId);
                        killWriter.Write(Minion.currentTarget.PlayerId);
                        killWriter.Write(byte.MaxValue);
                        AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                        RPCProcedure.uncheckedMurderPlayer(Minion.minion.Data.PlayerId, Minion.currentTarget.PlayerId, Byte.MaxValue);
                    }

                    minionKillButton.Timer = minionKillButton.MaxTimer;
                    Minion.currentTarget = null; 
                },
                () => { return Minion.minion != null && Minion.minion == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return Minion.currentTarget && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => { minionKillButton.Timer = minionKillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // BountyHunter Kill
            bountyHunterKillButton = new CustomButton(
                () => {
                    MurderAttemptResult murderAttemptResult = Helpers.checkMurderAttempt(BountyHunter.bountyhunter, BountyHunter.currentTarget);
                    if (murderAttemptResult == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        bountyHunterKillButton.Timer = bountyHunterKillButton.MaxTimer;
                        BountyHunter.currentTarget = null;
                        return;
                    }
                    else if (murderAttemptResult == MurderAttemptResult.SuppressKill) {
                        return;
                    }
                    else if (murderAttemptResult == MurderAttemptResult.PerformKill) {
                        byte targetId = 0;
                        if (BountyHunter.currentTarget == BountyHunter.hasToKill) {
                            targetId = BountyHunter.currentTarget.PlayerId;
                        }
                        else {
                            targetId = PlayerControl.LocalPlayer.PlayerId;
                        }

                        MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.BountyHunterKill, Hazel.SendOption.Reliable, -1);
                        killWriter.Write(targetId);
                        AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                        RPCProcedure.bountyHunterKill(targetId);
                    }

                    bountyHunterKillButton.Timer = bountyHunterKillButton.MaxTimer;
                    BountyHunter.currentTarget = null;
                },
                () => { return BountyHunter.bountyhunter != null && BountyHunter.bountyhunter == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return BountyHunter.usedTarget && BountyHunter.currentTarget && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => { bountyHunterKillButton.Timer = bountyHunterKillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Bounty hunter set target
            bountyHunterSetKillButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == BountyHunter.bountyhunter.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(BountyHunter.bountyhunter.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(BountyHunter.bountyhunter.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        bountyHunterSetKillButton.Timer = bountyHunterSetKillButton.MaxTimer;
                        return;
                    }

                    foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                        if (player != Kid.kid && player != Modifiers.lover1 && player != Modifiers.lover2 && player != BountyHunter.bountyhunter && player != Modifiers.bigchungus) {
                            BountyHunter.possibleTargets.Add(player);
                        }
                    }

                    int bountyTarget = rnd.Next(1, BountyHunter.possibleTargets.Count);

                    PlayerControl finaltarget = Helpers.playerById(BountyHunter.possibleTargets[bountyTarget].PlayerId);

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.BountyHunterSetKill, Hazel.SendOption.Reliable, -1);
                    writer.Write(finaltarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.bountyHunterSetKill(finaltarget.PlayerId);
                },
                () => { return !BountyHunter.usedTarget && BountyHunter.bountyhunter != null && BountyHunter.bountyhunter == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => { bountyHunterSetKillButton.Timer = bountyHunterSetKillButton.MaxTimer; },
                BountyHunter.getButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T,
                false,
                0f,
                () => {
                    bountyHunterSetKillButton.Timer = bountyHunterSetKillButton.MaxTimer;
                }
            );

            // Trapper place mine
            trapperMineButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Trapper.trapper.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Trapper.trapper.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Trapper.trapper.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        trapperMineButton.Timer = trapperMineButton.MaxTimer;
                        return;
                    }

                    trapperMineButton.Timer = trapperMineButton.MaxTimer;

                    SoundManager.Instance.PlaySound(CustomMain.customAssets.bombermanPlaceBombClip, false, 100f);
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlaceMine, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.placeMine();
                },
                () => {
                    int currentAlivePlayers = 0;
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                        if (!player.Data.IsDead) {
                            currentAlivePlayers += 1;
                        }
                    }
                    return currentAlivePlayers > 2 && Trapper.trapper != null && Trapper.trapper == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => {
                    bool closetoPlayer = false;
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                        if (Vector2.Distance(player.transform.position, Trapper.trapper.transform.position) < 3f && player != Trapper.trapper && !player.Data.IsDead) {
                            closetoPlayer = true;
                        }
                    }
                    bool closetoMine = false;
                    foreach (Mine mine in Mine.mines) {
                        if (Vector2.Distance(mine.mine.transform.position, Trapper.trapper.transform.position) < 2f) {
                            closetoMine = true;
                        }
                    }
                    return !closetoPlayer && !closetoMine && PlayerControl.LocalPlayer.CanMove && Trapper.currentMineNumber < Trapper.numberOfMines && !Challenger.isDueling;
                },
                () => {
                    trapperMineButton.Timer = trapperMineButton.MaxTimer;
                },
                Trapper.getMineButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q
            );

            // Trapper place trap
            trapperTrapButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Trapper.trapper.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Trapper.trapper.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Trapper.trapper.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        trapperTrapButton.Timer = trapperTrapButton.MaxTimer;
                        return;
                    }

                    trapperTrapButton.Timer = trapperTrapButton.MaxTimer;

                    SoundManager.Instance.PlaySound(CustomMain.customAssets.bombermanPlaceBombClip, false, 100f);
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlaceTrap, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.placeTrap();
                },
                () => {
                    int currentAlivePlayers = 0;
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                        if (!player.Data.IsDead) {
                            currentAlivePlayers += 1;
                        }
                    }
                    return currentAlivePlayers > 2 && Trapper.trapper != null && Trapper.trapper == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => {
                    bool closetoPlayer = false;
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                        if (Vector2.Distance(player.transform.position, Trapper.trapper.transform.position) < 3f && player != Trapper.trapper && !player.Data.IsDead) {
                            closetoPlayer = true;
                        }
                    }
                    bool closetoTrap = false;
                    foreach (Trap trap in Trap.traps) {
                        if (Vector2.Distance(trap.trap.transform.position, Trapper.trapper.transform.position) < 2f) {
                            closetoTrap = true;
                        }
                    }
                    return !closetoPlayer && !closetoTrap && PlayerControl.LocalPlayer.CanMove && Trapper.currentTrapNumber < Trapper.numberOfTraps && !Challenger.isDueling;
                },
                () => {
                    trapperTrapButton.Timer = trapperTrapButton.MaxTimer;
                },
                Trapper.getTrapButtonSprite(),
                new Vector3(-3f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Trapper Kill
            trapperKillButton = new CustomButton(
                () => {
                    MurderAttemptResult murderAttemptResult = Helpers.checkMurderAttempt(Trapper.trapper, Trapper.currentTarget);
                    if (murderAttemptResult == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        trapperKillButton.Timer = trapperKillButton.MaxTimer;
                        Trapper.currentTarget = null;
                        return;
                    }

                    if (Helpers.checkMurderAttemptAndKill(Trapper.trapper, Trapper.currentTarget) == MurderAttemptResult.SuppressKill) return;

                    trapperKillButton.Timer = trapperKillButton.MaxTimer;
                    Trapper.currentTarget = null;
                },
                () => {
                    int currentAlivePlayers = 0;
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                        if (!player.Data.IsDead) {
                            currentAlivePlayers += 1;
                        }
                    }
                    return currentAlivePlayers <= 2 && Trapper.trapper != null && Trapper.trapper == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { return Trapper.currentTarget && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => { trapperKillButton.Timer = trapperKillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Yinyanger set Yin
            yinyangerYinButton = new CustomButton(
                () => {
                    MurderAttemptResult murderAttemptResult = Helpers.checkMurderAttempt(Yinyanger.yinyanger, Yinyanger.currentTarget);
                    if (murderAttemptResult == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        yinyangerYinButton.Timer = yinyangerYinButton.MaxTimer;
                        return;
                    }

                    Yinyanger.yinyedplayer = Yinyanger.currentTarget;
                    SoundManager.Instance.PlaySound(CustomMain.customAssets.yinyangerYinyangClip, false, 100f);
                    // Notify players about who is the Yined
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.YinyangerSetYinyang, Hazel.SendOption.Reliable, -1);
                    writer.Write(Yinyanger.yinyedplayer.PlayerId);
                    writer.Write(0);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.yinyangerSetYinyang(Yinyanger.yinyedplayer.PlayerId, 0);

                    yinyangerYinButton.Timer = yinyangerYangButton.Timer = yinyangerYinButton.MaxTimer;

                },
                () => {
                    int currentAlivePlayers = 0;
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                        if (!player.Data.IsDead) {
                            currentAlivePlayers += 1;
                        }
                    }
                    return currentAlivePlayers > 2 && Yinyanger.yinyanger == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => {
                    bool closetoYanged = false;
                    if (Yinyanger.yangyedplayer != null) {
                        if (Vector2.Distance(Yinyanger.yinyanger.transform.position, Yinyanger.yangyedplayer.transform.position) < 4f && !Yinyanger.yinyanger.Data.IsDead) {
                            closetoYanged = true;
                        }
                    }
                    bool canYin = true;
                    if (Yinyanger.yangyedplayer != null && Yinyanger.currentTarget == Yinyanger.yangyedplayer) {
                        canYin = false;
                    }
                    return !closetoYanged && canYin && Yinyanger.currentTarget && !Yinyanger.usedYined && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling;
                },
                () => {
                    yinyangerYinButton.Timer = yinyangerYinButton.MaxTimer;
                },
                Yinyanger.getYinButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q
            );

            // Yinyanger set Yang
            yinyangerYangButton = new CustomButton(
                () => {
                    MurderAttemptResult murderAttemptResult = Helpers.checkMurderAttempt(Yinyanger.yinyanger, Yinyanger.currentTarget);
                    if (murderAttemptResult == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        yinyangerYangButton.Timer = yinyangerYangButton.MaxTimer;
                        return;
                    }

                    Yinyanger.yangyedplayer = Yinyanger.currentTarget;
                    SoundManager.Instance.PlaySound(CustomMain.customAssets.yinyangerYinyangClip, false, 100f);
                    // Notify players about who is the Yanged
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.YinyangerSetYinyang, Hazel.SendOption.Reliable, -1);
                    writer.Write(Yinyanger.yangyedplayer.PlayerId);
                    writer.Write(1); AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.yinyangerSetYinyang(Yinyanger.yangyedplayer.PlayerId, 1);

                    yinyangerYangButton.Timer = yinyangerYinButton.Timer = yinyangerYangButton.MaxTimer;
                },
                () => {
                    int currentAlivePlayers = 0;
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                        if (!player.Data.IsDead) {
                            currentAlivePlayers += 1;
                        }
                    }
                    return currentAlivePlayers > 2 && Yinyanger.yinyanger == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => {
                    bool closetoYined = false;
                    if (Yinyanger.yinyedplayer != null) {
                        if (Vector2.Distance(Yinyanger.yinyanger.transform.position, Yinyanger.yinyedplayer.transform.position) < 4f && !Yinyanger.yinyanger.Data.IsDead) {
                            closetoYined = true;
                        }
                    }
                    bool canYang = true;
                    if (Yinyanger.yinyedplayer != null && Yinyanger.currentTarget == Yinyanger.yinyedplayer) {
                        canYang = false;
                    }
                    return !closetoYined && canYang && Yinyanger.currentTarget && !Yinyanger.usedYanged && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling;
                },
                () => {
                    yinyangerYangButton.Timer = yinyangerYangButton.MaxTimer;
                },
                Yinyanger.getYangButtonSprite(),
                new Vector3(-3f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Yinyanger Kill
            yinyangerKillButton = new CustomButton(
                () => {
                    MurderAttemptResult murderAttemptResult = Helpers.checkMurderAttempt(Yinyanger.yinyanger, Yinyanger.currentTarget);
                    if (murderAttemptResult == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        yinyangerKillButton.Timer = yinyangerKillButton.MaxTimer;
                        Yinyanger.currentTarget = null;
                        return;
                    }

                    if (Helpers.checkMurderAttemptAndKill(Yinyanger.yinyanger, Yinyanger.currentTarget) == MurderAttemptResult.SuppressKill) return;

                    yinyangerKillButton.Timer = yinyangerKillButton.MaxTimer;
                    Yinyanger.currentTarget = null;
                },
                () => {
                    int currentAlivePlayers = 0;
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                        if (!player.Data.IsDead) {
                            currentAlivePlayers += 1;
                        }
                    }
                    return currentAlivePlayers <= 2 && Yinyanger.yinyanger != null && Yinyanger.yinyanger == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { return Yinyanger.currentTarget && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => { yinyangerKillButton.Timer = yinyangerKillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            //Challenger challenge button
            challengerChallengeButton = new CustomButton(
                () => {
                    MurderAttemptResult murder = Helpers.checkMurderAttempt(Challenger.challenger, Challenger.currentTarget);
                    if (murder == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);

                        Challenger.duration = quackNumber;
                        challengerChallengeButton.EffectDuration = Challenger.duration;
                        challengerChallengeButton.Timer = challengerChallengeButton.MaxTimer;
                        Challenger.currentTarget = null;
                        return;
                    }

                    if (murder == MurderAttemptResult.PerformKill) {
                        Challenger.duration = 10f;
                        challengerChallengeButton.EffectDuration = Challenger.duration;

                        Challenger.rivalPlayer = Challenger.currentTarget;
                        // Notify players about the who is the rival
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ChallengerSetRival, Hazel.SendOption.Reliable, -1);
                        writer.Write(Challenger.rivalPlayer.PlayerId);
                        writer.Write(0);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.challengerSetRival(Challenger.rivalPlayer.PlayerId, 0);

                        HudManager.Instance.StartCoroutine(Effects.Lerp(Challenger.duration, new Action<float>((p) => { // Delayed action
                            if (p == 1f) {
                                MurderAttemptResult murder = Helpers.checkMurderAttempt(Challenger.challenger, Challenger.rivalPlayer);
                                if (murder == MurderAttemptResult.JinxKill) {
                                    SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                                    challengerChallengeButton.Timer = challengerChallengeButton.MaxTimer;
                                    Challenger.currentTarget = null;
                                    return;
                                }
                                else if (!Challenger.challenger.Data.IsDead && Challenger.rivalPlayer != null && !Challenger.rivalPlayer.Data.IsDead && murder == MurderAttemptResult.PerformKill /*Helpers.handleMurderAttempt(Challenger.challenger, Challenger.rivalPlayer)*/ && !Challenger.challengerIsInMeeting && !TimeTraveler.isRewinding) {
                                    // Perform duel if no sabotage and player has no squire shield
                                    bool sabotageActive = false;
                                    if (Bomberman.activeBomb == true || Ilusionist.lightsOutTimer > 0) {
                                        sabotageActive = true;
                                    }
                                    else {
                                        foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
                                            if (task.TaskType == TaskTypes.FixLights || task.TaskType == TaskTypes.RestoreOxy || task.TaskType == TaskTypes.ResetReactor || task.TaskType == TaskTypes.ResetSeismic || task.TaskType == TaskTypes.FixComms || task.TaskType == TaskTypes.StopCharles)
                                                sabotageActive = true;
                                    }

                                    if (!sabotageActive) {
                                        // If the Demon bitten is the challenger or the rival player, murder him and cancel the duel
                                        if (Demon.demon != null && Demon.bitten != null && (Demon.bitten == Challenger.challenger || Demon.bitten == Challenger.rivalPlayer)) {
                                            Helpers.handleDemonBiteOnBodyReport();
                                        }
                                        else {
                                            // Activate the Demon bitten kill to spawn the body outside the duel arena
                                            if (Demon.demon != null && Demon.bitten != null) {
                                                Helpers.handleDemonBiteOnBodyReport();
                                            }
                                            MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ChallengerPerformDuel, Hazel.SendOption.Reliable, -1);
                                            AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                                            RPCProcedure.challengerPerformDuel();
                                        }
                                    }
                                }
                                else {
                                    // Notify players about clearing rivalplayer
                                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ChallengerSetRival, Hazel.SendOption.Reliable, -1);
                                    writer.Write(byte.MaxValue);
                                    writer.Write(byte.MaxValue);
                                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                                    RPCProcedure.challengerSetRival(byte.MaxValue, byte.MaxValue);
                                }
                            }
                        })));
                        challengerChallengeButton.HasEffect = true; // Trigger effect on this click

                    }
                    else {
                        challengerChallengeButton.HasEffect = false; // Block effect if no action was fired
                    }
                },
                () => {
                    int currentAlivePlayers = 0;
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                        if (!player.Data.IsDead) {
                            currentAlivePlayers += 1;
                        }
                    }
                    return currentAlivePlayers > 2 && !Challenger.isDueling && Challenger.challenger != null && Challenger.challenger == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => {
                    bool sabotageActive = false;
                    if (Bomberman.activeBomb == true || Ilusionist.lightsOutTimer > 0) {
                        sabotageActive = true;
                    }
                    else {
                        foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
                            if (task.TaskType == TaskTypes.FixLights || task.TaskType == TaskTypes.RestoreOxy || task.TaskType == TaskTypes.ResetReactor || task.TaskType == TaskTypes.ResetSeismic || task.TaskType == TaskTypes.FixComms || task.TaskType == TaskTypes.StopCharles)
                                sabotageActive = true;
                    }
                    return !sabotageActive && Challenger.currentTarget && PlayerControl.LocalPlayer.CanMove;
                },
                () => {
                    challengerChallengeButton.Timer = challengerChallengeButton.MaxTimer;
                    challengerChallengeButton.isEffectActive = false;
                    challengerChallengeButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                    Challenger.currentTarget = null;
                    Challenger.challengerIsInMeeting = false;
                },
                Challenger.getChallengeButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q,
                false,
                0f,
                () => {
                    challengerChallengeButton.Timer = challengerChallengeButton.MaxTimer;
                }
            );

            // Challenger Kill
            challengerKillButton = new CustomButton(
                () => {
                    MurderAttemptResult murder = Helpers.checkMurderAttempt(Challenger.challenger, Challenger.currentTarget);
                    if (murder == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        challengerKillButton.Timer = challengerKillButton.MaxTimer;
                        Challenger.currentTarget = null;
                        return;
                    }

                    if (Helpers.checkMurderAttemptAndKill(Challenger.challenger, Challenger.currentTarget) == MurderAttemptResult.SuppressKill) return;

                    challengerKillButton.Timer = challengerKillButton.MaxTimer;
                    Challenger.currentTarget = null;
                },
                () => {
                    int currentAlivePlayers = 0;
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                        if (!player.Data.IsDead) {
                            currentAlivePlayers += 1;
                        }
                    }
                    return currentAlivePlayers <= 2 && Challenger.challenger != null && Challenger.challenger == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { return Challenger.currentTarget && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => { challengerKillButton.Timer = challengerKillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Challenger rock
            challengerRockButton = new CustomButton(
                () => {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ChallengerSelectAttack, Hazel.SendOption.Reliable, -1);
                    writer.Write(1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.challengerSelectAttack(1);
                },
                () => { return Challenger.challenger == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && Challenger.isDueling; },
                () => { return !Challenger.challengerRock && !Challenger.challengerPaper && !Challenger.challengerScissors && PlayerControl.LocalPlayer.CanMove && Challenger.isDueling && !Challenger.timeOutDuel; },
                () => { },
                Challenger.getRockButtonSprite(),
                new Vector3(-7.4f, -0.06f, 0f),
                __instance,
                null,
                false
            );

            // Challenger paper
            challengerPaperButton = new CustomButton(
                () => {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ChallengerSelectAttack, Hazel.SendOption.Reliable, -1);
                    writer.Write(2);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.challengerSelectAttack(2);
                },
                () => { return Challenger.challenger == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && Challenger.isDueling; },
                () => { return !Challenger.challengerRock && !Challenger.challengerPaper && !Challenger.challengerScissors && PlayerControl.LocalPlayer.CanMove && Challenger.isDueling && !Challenger.timeOutDuel; },
                () => { },
                Challenger.getPaperButtonSprite(),
                new Vector3(-6.3f, -0.06f, 0f),
                __instance,
                null,
                false
            );

            // Challenger scissors
            challengerScissorsButton = new CustomButton(
                () => {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ChallengerSelectAttack, Hazel.SendOption.Reliable, -1);
                    writer.Write(3);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.challengerSelectAttack(3);
                },
                () => { return Challenger.challenger == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && Challenger.isDueling; },
                () => { return !Challenger.challengerRock && !Challenger.challengerPaper && !Challenger.challengerScissors && PlayerControl.LocalPlayer.CanMove && Challenger.isDueling && !Challenger.timeOutDuel; },
                () => { },
                Challenger.getScissorsButtonSprite(),
                new Vector3(-5.2f, -0.06f, 0f),
                __instance,
                null,
                false
            );

            // Rivalplayer rock
            rivalplayerRockButton = new CustomButton(
                () => {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ChallengerSelectAttack, Hazel.SendOption.Reliable, -1);
                    writer.Write(4);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.challengerSelectAttack(4);
                },
                () => { return Challenger.rivalPlayer == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && Challenger.isDueling; },
                () => { return !Challenger.rivalRock && !Challenger.rivalPaper && !Challenger.rivalScissors && PlayerControl.LocalPlayer.CanMove && Challenger.isDueling && !Challenger.timeOutDuel; },
                () => { },
                Challenger.getRockButtonSprite(),
                new Vector3(-7.4f, -0.06f, 0f),
                __instance,
                null,
                false
            );

            // Rivalplayer paper
            rivalplayerPaperButton = new CustomButton(
                () => {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ChallengerSelectAttack, Hazel.SendOption.Reliable, -1);
                    writer.Write(5);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.challengerSelectAttack(5);
                },
                () => { return Challenger.rivalPlayer == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && Challenger.isDueling; },
                () => { return !Challenger.rivalRock && !Challenger.rivalPaper && !Challenger.rivalScissors && PlayerControl.LocalPlayer.CanMove && Challenger.isDueling && !Challenger.timeOutDuel; },
                () => { },
                Challenger.getPaperButtonSprite(),
                new Vector3(-6.3f, -0.06f, 0f),
                __instance,
                null,
                false
            );

            // Rivalplayer scissors
            rivalplayerScissorsButton = new CustomButton(
                () => {
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ChallengerSelectAttack, Hazel.SendOption.Reliable, -1);
                    writer.Write(6);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.challengerSelectAttack(6);
                },
                () => { return Challenger.rivalPlayer == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && Challenger.isDueling; },
                () => { return !Challenger.rivalRock && !Challenger.rivalPaper && !Challenger.rivalScissors && PlayerControl.LocalPlayer.CanMove && Challenger.isDueling && !Challenger.timeOutDuel; },
                () => { },
                Challenger.getScissorsButtonSprite(),
                new Vector3(-5.2f, -0.06f, 0f),
                __instance,
                null,
                false
            );


            // Neutral buttons code

            // RoleThief steal
            roleThiefStealButton = new CustomButton(
                () => {
                    MurderAttemptResult murder = Helpers.checkMurderAttempt(RoleThief.rolethief, RoleThief.currentTarget);
                    if (murder == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        roleThiefStealButton.Timer = roleThiefStealButton.MaxTimer;
                        RoleThief.currentTarget = null;
                        return;
                    }

                    roleThiefStealButton.Timer = roleThiefStealButton.MaxTimer;

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.RoleThiefSteal, Hazel.SendOption.Reliable, -1);
                    writer.Write(RoleThief.currentTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);

                    RPCProcedure.roleThiefSteal(RoleThief.currentTarget.PlayerId);
                },
                () => { return RoleThief.rolethief != null && RoleThief.rolethief == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return RoleThief.currentTarget && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => { roleThiefStealButton.Timer = roleThiefStealButton.MaxTimer; },
                RoleThief.getButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q
            );

            // Pyromaniac douse ignite
            pyromaniacButton = new CustomButton(
                () => {
                    bool dousedEveryoneAlive = Pyromaniac.sprayedEveryoneAlive();
                    if (dousedEveryoneAlive) {
                        MessageWriter winWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PyromaniacWin, Hazel.SendOption.Reliable, -1);
                        AmongUsClient.Instance.FinishRpcImmediately(winWriter);
                        RPCProcedure.pyromaniacWin();
                        pyromaniacButton.HasEffect = false;
                    }
                    else if (Pyromaniac.currentTarget != null) {
                        Pyromaniac.sprayTarget = Pyromaniac.currentTarget;
                        pyromaniacButton.HasEffect = true;
                    }
                },
                () => { return Pyromaniac.pyromaniac != null && Pyromaniac.pyromaniac == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    bool dousedEveryoneAlive = Pyromaniac.sprayedEveryoneAlive();
                    if (dousedEveryoneAlive) pyromaniacButton.actionButton.graphic.sprite = Pyromaniac.getIgniteSprite();

                    if (pyromaniacButton.isEffectActive && Pyromaniac.sprayTarget != Pyromaniac.currentTarget) {
                        Pyromaniac.sprayTarget = null;
                        pyromaniacButton.Timer = 0f;
                        pyromaniacButton.isEffectActive = false;
                    }

                    return PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling && (dousedEveryoneAlive || Pyromaniac.currentTarget != null);
                },
                () => {
                    pyromaniacButton.Timer = pyromaniacButton.MaxTimer;
                    pyromaniacButton.isEffectActive = false;
                    pyromaniacButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                    Pyromaniac.sprayTarget = null;
                },
                Pyromaniac.getSpraySprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q,
                true,
                Pyromaniac.duration,
                () => {
                    MurderAttemptResult murder = Helpers.checkMurderAttempt(Pyromaniac.pyromaniac, Pyromaniac.sprayTarget);
                    if (murder == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        pyromaniacButton.Timer = pyromaniacButton.MaxTimer;
                        Pyromaniac.sprayTarget = null;
                        return;
                    }

                    if (Pyromaniac.sprayTarget != null) Pyromaniac.sprayedPlayers.Add(Pyromaniac.sprayTarget);
                    Pyromaniac.sprayTarget = null;
                    pyromaniacButton.Timer = Pyromaniac.sprayedEveryoneAlive() ? 0 : pyromaniacButton.MaxTimer;

                    foreach (PlayerControl p in Pyromaniac.sprayedPlayers) {
                        if (MapOptions.playerIcons.ContainsKey(p.PlayerId)) {
                            MapOptions.playerIcons[p.PlayerId].setSemiTransparent(false);
                        }
                    }
                }
            );

            // Treasure Hunter spawn random treasure
            treasureHunterButton = new CustomButton(
                () => {

                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == TreasureHunter.treasureHunter.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(TreasureHunter.treasureHunter.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(TreasureHunter.treasureHunter.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        treasureHunterButton.Timer = treasureHunterButton.MaxTimer;
                        return;
                    }

                    SoundManager.Instance.PlaySound(CustomMain.customAssets.treasureHunterPlaceTreasure, false, 100f);
                    switch (PlayerControl.GameOptions.MapId) {
                        case 0:
                            int skeldNumber = rnd.Next(1, 15);
                            TreasureHunter.randomSpawn = skeldNumber;
                            break;
                        case 1:
                            int miraHQNumber = rnd.Next(1, 13);
                            TreasureHunter.randomSpawn = miraHQNumber;
                            break;
                        case 2:
                            int polusNumber = rnd.Next(1, 21);
                            TreasureHunter.randomSpawn = polusNumber;
                            break;
                        case 3:
                            int dleksNumber = rnd.Next(1, 15);
                            TreasureHunter.randomSpawn = dleksNumber;
                            break;
                        case 4:
                            int airshipNumber = rnd.Next(1, 27);
                            TreasureHunter.randomSpawn = airshipNumber;
                            break;
                    }
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlaceTreasure, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.placeTreasure();
                },
                () => { return TreasureHunter.treasureHunter != null && TreasureHunter.canPlace == true && TreasureHunter.treasureHunter == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => {
                    treasureHunterButton.Timer = treasureHunterButton.MaxTimer;
                },
                TreasureHunter.getButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q,
                false,
                0f,
                () => { treasureHunterButton.Timer = treasureHunterButton.MaxTimer; }
            );

            // Devourer devour
            devourerButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Devourer.devourer.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Devourer.devourer.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Devourer.devourer.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        devourerButton.Timer = devourerButton.MaxTimer;
                        return;
                    }

                    foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(PlayerControl.LocalPlayer.GetTruePosition(), 1f, Constants.PlayersOnlyMask)) {
                        if (collider2D.tag == "DeadBody") {
                            DeadBody component = collider2D.GetComponent<DeadBody>();
                            if (component && !component.Reported) {
                                Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
                                Vector2 truePosition2 = component.TruePosition;
                                if (Vector2.Distance(truePosition2, truePosition) <= PlayerControl.LocalPlayer.MaxReportDistance && PlayerControl.LocalPlayer.CanMove && !PhysicsHelpers.AnythingBetween(truePosition, truePosition2, Constants.ShipAndObjectsMask, false)) {
                                    GameData.PlayerInfo playerInfo = GameData.Instance.GetPlayerById(component.ParentId);

                                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.DevourBody, Hazel.SendOption.Reliable, -1);
                                    writer.Write(playerInfo.PlayerId);
                                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                                    RPCProcedure.devourBody(playerInfo.PlayerId);

                                    devourerButton.Timer = devourerButton.MaxTimer;
                                    break;
                                }
                            }
                        }
                    }
                },
                () => { return Devourer.devourer != null && Devourer.devourer == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    bool canEat = false;
                    foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(PlayerControl.LocalPlayer.GetTruePosition(), 1f, Constants.PlayersOnlyMask))
                        if (collider2D.tag == "DeadBody")
                            canEat = true;
                    return canEat && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling;
                },
                () => { devourerButton.Timer = devourerButton.MaxTimer; },
                Devourer.getButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q
            );


            // Crewmates buttons

            // Captain emergency call
            captainCallButton = new CustomButton(
                () => {

                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Captain.captain.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Captain.captain.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Captain.captain.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        captainCallButton.Timer = captainCallButton.MaxTimer;
                        return;
                    }

                    foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks) {
                        if (task.TaskType == TaskTypes.FixLights) {
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.MechanicFixLights, Hazel.SendOption.Reliable, -1);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.mechanicFixLights();
                        }
                        else if (task.TaskType == TaskTypes.RestoreOxy) {
                            ShipStatus.Instance.RpcRepairSystem(SystemTypes.LifeSupp, 0 | 64);
                            ShipStatus.Instance.RpcRepairSystem(SystemTypes.LifeSupp, 1 | 64);
                        }
                        else if (task.TaskType == TaskTypes.ResetReactor) {
                            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Reactor, 16);
                        }
                        else if (task.TaskType == TaskTypes.ResetSeismic) {
                            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Laboratory, 16);
                        }
                        else if (task.TaskType == TaskTypes.FixComms) {
                            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Comms, 16 | 0);
                            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Comms, 16 | 1);
                        }
                        else if (task.TaskType == TaskTypes.StopCharles) {
                            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Reactor, 0 | 16);
                            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Reactor, 1 | 16);
                        }
                    }
                    if (Bomberman.activeBomb == true) {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.FixBomb, Hazel.SendOption.Reliable, -1);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.fixBomb();
                    }

                    HudManager.Instance.StartCoroutine(Effects.Lerp(0.1f, new Action<float>((p) => { // Delayed action
                        if (p == 1f) {
                            PlayerControl.LocalPlayer.CmdReportDeadBody(null);
                        }
                    })));
                    captainCallButton.Timer = captainCallButton.MaxTimer;
                },
                () => {
                    int currentAlivePlayers = 0;
                    foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                        if (!player.Data.IsDead) {
                            currentAlivePlayers += 1;
                        }
                    }
                    return currentAlivePlayers <= 2 && Captain.captain != null && Captain.captain == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { return PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => { captainCallButton.Timer = captainCallButton.MaxTimer; },
                Captain.getCallButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q
            );

            // Mechanic Repair
            mechanicRepairButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Mechanic.mechanic.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Mechanic.mechanic.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Mechanic.mechanic.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        mechanicRepairButton.Timer = mechanicRepairButton.MaxTimer;
                        return;
                    }

                    mechanicRepairButton.Timer = mechanicRepairButton.MaxTimer;
                    MessageWriter usedRepairWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.MechanicUsedRepair, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(usedRepairWriter);
                    RPCProcedure.mechanicUsedRepair();

                    foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks) {
                        if (task.TaskType == TaskTypes.FixLights) {
                            MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.MechanicFixLights, Hazel.SendOption.Reliable, -1);
                            AmongUsClient.Instance.FinishRpcImmediately(writer);
                            RPCProcedure.mechanicFixLights();
                        }
                        else if (task.TaskType == TaskTypes.RestoreOxy) {
                            ShipStatus.Instance.RpcRepairSystem(SystemTypes.LifeSupp, 0 | 64);
                            ShipStatus.Instance.RpcRepairSystem(SystemTypes.LifeSupp, 1 | 64);
                        }
                        else if (task.TaskType == TaskTypes.ResetReactor) {
                            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Reactor, 16);
                        }
                        else if (task.TaskType == TaskTypes.ResetSeismic) {
                            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Laboratory, 16);
                        }
                        else if (task.TaskType == TaskTypes.FixComms) {
                            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Comms, 16 | 0);
                            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Comms, 16 | 1);
                        }
                        else if (task.TaskType == TaskTypes.StopCharles) {
                            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Reactor, 0 | 16);
                            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Reactor, 1 | 16);
                        }
                    }
                    if (Bomberman.activeBomb == true) {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.FixBomb, Hazel.SendOption.Reliable, -1);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.fixBomb();
                    }
                },
                () => { return Mechanic.mechanic != null && Mechanic.mechanic == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    bool sabotageActive = false;
                    if (Bomberman.activeBomb == true) {
                        sabotageActive = true;
                    }
                    else {
                        foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
                            if (task.TaskType == TaskTypes.FixLights || task.TaskType == TaskTypes.RestoreOxy || task.TaskType == TaskTypes.ResetReactor || task.TaskType == TaskTypes.ResetSeismic || task.TaskType == TaskTypes.FixComms || task.TaskType == TaskTypes.StopCharles)
                                sabotageActive = true;
                    }
                    return sabotageActive && !Mechanic.usedRepair && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling;
                },
                () => { },
                Mechanic.getButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q
            );

            // Mechanic button spawn remaining uses text
            Mechanic.mechanicRepairButtonText = GameObject.Instantiate(mechanicRepairButton.actionButton.cooldownTimerText, mechanicRepairButton.actionButton.cooldownTimerText.transform.parent);
            Mechanic.mechanicRepairButtonText.enableWordWrapping = false;
            Mechanic.mechanicRepairButtonText.transform.localScale = Vector3.one * 0.5f;
            Mechanic.mechanicRepairButtonText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);

            // Sheriff Kill
            sheriffKillButton = new CustomButton(
                () => {
                    MurderAttemptResult murderAttemptResult = Helpers.checkMurderAttempt(Sheriff.sheriff, Sheriff.currentTarget);
                    if (murderAttemptResult == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        sheriffKillButton.Timer = sheriffKillButton.MaxTimer;
                        Sheriff.currentTarget = null;
                        return;
                    }
                    else if (murderAttemptResult == MurderAttemptResult.SuppressKill) {
                        return;
                    }

                    if (murderAttemptResult == MurderAttemptResult.PerformKill) {
                        byte targetId = 0;
                        if ((Sheriff.currentTarget.Data.Role.IsImpostor) ||
                            (Sheriff.canKillNeutrals && (Joker.joker == Sheriff.currentTarget || RoleThief.rolethief == Sheriff.currentTarget || Pyromaniac.pyromaniac == Sheriff.currentTarget || TreasureHunter.treasureHunter == Sheriff.currentTarget || Devourer.devourer == Sheriff.currentTarget)) ||
                            (Renegade.renegade == Sheriff.currentTarget || Minion.minion == Sheriff.currentTarget || BountyHunter.bountyhunter == Sheriff.currentTarget || Trapper.trapper == Sheriff.currentTarget || Yinyanger.yinyanger == Sheriff.currentTarget || Challenger.challenger == Sheriff.currentTarget)) {
                            targetId = Sheriff.currentTarget.PlayerId;
                        }
                        else {
                            targetId = PlayerControl.LocalPlayer.PlayerId;
                        }

                        MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.UncheckedMurderPlayer, Hazel.SendOption.Reliable, -1);
                        killWriter.Write(Sheriff.sheriff.Data.PlayerId);
                        killWriter.Write(targetId);
                        killWriter.Write(byte.MaxValue);
                        AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                        RPCProcedure.uncheckedMurderPlayer(Sheriff.sheriff.Data.PlayerId, targetId, Byte.MaxValue);
                    }

                    sheriffKillButton.Timer = sheriffKillButton.MaxTimer;
                    Sheriff.currentTarget = null;
                },
                () => { return Sheriff.sheriff != null && Sheriff.sheriff == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return Sheriff.currentTarget && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => { sheriffKillButton.Timer = sheriffKillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0f, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Forensic button
            forensicButton = new CustomButton(
                () => {
                    if (Forensic.target != null) {
                        Forensic.soulTarget = Forensic.target;
                        forensicButton.HasEffect = true;
                    }
                },
                () => { return Forensic.forensic != null && Forensic.forensic == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (forensicButton.isEffectActive && Forensic.target != Forensic.soulTarget) {
                        Forensic.soulTarget = null;
                        forensicButton.Timer = 0f;
                        forensicButton.isEffectActive = false;
                    }
                    return Forensic.target != null && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling;
                },
                () => {
                    forensicButton.Timer = forensicButton.MaxTimer;
                    forensicButton.isEffectActive = false;
                    Forensic.soulTarget = null;
                },
                Forensic.getQuestionSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q,
                true,
                Forensic.duration,
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Forensic.forensic.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Forensic.forensic.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Forensic.forensic.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        forensicButton.Timer = forensicButton.MaxTimer;
                        return;
                    }

                    forensicButton.Timer = forensicButton.MaxTimer;
                    if (Forensic.target == null || Forensic.target.player == null) return;
                    string msg = "";

                    int randomNumber = Forensic.target.killerIfExisting.PlayerId == Kid.kid?.PlayerId ? LasMonjas.rnd.Next(3) : LasMonjas.rnd.Next(4);
                    string typeOfColor = Helpers.isLighterColor(Forensic.target.killerIfExisting.Data.DefaultOutfit.ColorId) ? "lighter (L)" : "darker (D)";
                    float timeSinceDeath = ((float)(Forensic.meetingStartTime - Forensic.target.timeOfDeath).TotalMilliseconds);

                    string name = "(" + Forensic.target.player.Data.PlayerName + "): ";
                    if (randomNumber == 0) msg = name + "I was the " + RoleInfo.GetRolesString(Forensic.target.player, false);
                    else if (randomNumber == 1) msg = name + "My killer has a " + typeOfColor + " color";
                    else if (randomNumber == 2) msg = name + "I've been dead for " + Math.Round(timeSinceDeath / 1000) + " seconds";
                    else msg = name + "My killer was the " + RoleInfo.GetRolesString(Forensic.target.killerIfExisting, false); 

                    DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, $"{msg}");

                    // Remove ghost
                    if (Forensic.oneTimeUse) {
                        float closestDistance = float.MaxValue;
                        SpriteRenderer target = null;

                        foreach ((DeadPlayer db, Vector3 ps) in Forensic.deadBodies) {
                            if (db == Forensic.target) {
                                Tuple<DeadPlayer, Vector3> deadBody = Tuple.Create(db, ps);
                                Forensic.deadBodies.Remove(deadBody);
                                break;
                            }

                        }
                        foreach (SpriteRenderer rend in Forensic.souls) {
                            float distance = Vector2.Distance(rend.transform.position, PlayerControl.LocalPlayer.GetTruePosition());
                            if (distance < closestDistance) {
                                closestDistance = distance;
                                target = rend;
                            }
                        }

                        HudManager.Instance.StartCoroutine(Effects.Lerp(5f, new Action<float>((p) => {
                            if (target != null) {
                                var tmp = target.color;
                                tmp.a = Mathf.Clamp01(1 - p);
                                target.color = tmp;
                            }
                            if (p == 1f && target != null && target.gameObject != null) UnityEngine.Object.Destroy(target.gameObject);
                        })));

                        Forensic.souls.Remove(target);
                    }
                }
            );

            // TimeTraveler shield
            timeTravelerShieldButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == TimeTraveler.timeTraveler.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(TimeTraveler.timeTraveler.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(TimeTraveler.timeTraveler.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        TimeTraveler.shieldDuration = quackNumber;
                        timeTravelerShieldButton.EffectDuration = TimeTraveler.shieldDuration;
                        timeTravelerShieldButton.Timer = timeTravelerShieldButton.MaxTimer;
                        return;
                    }

                    TimeTraveler.shieldDuration = TimeTraveler.backUpduration;
                    timeTravelerShieldButton.EffectDuration = TimeTraveler.shieldDuration;
                    timeTravelerRewindTimeButton.Timer = timeTravelerRewindTimeButton.MaxTimer;
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.TimeTravelerShield, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.timeTravelerShield();
                },
                () => { return TimeTraveler.timeTraveler != null && TimeTraveler.timeTraveler == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    bool sabotageActive = false;
                    if (Bomberman.activeBomb == true || Ilusionist.lightsOutTimer > 0) {
                        sabotageActive = true;
                    }
                    else {
                        foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
                            if (task.TaskType == TaskTypes.FixLights || task.TaskType == TaskTypes.RestoreOxy || task.TaskType == TaskTypes.ResetReactor || task.TaskType == TaskTypes.ResetSeismic || task.TaskType == TaskTypes.FixComms || task.TaskType == TaskTypes.StopCharles)
                                sabotageActive = true;
                    }
                    return !sabotageActive && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling && !TimeTraveler.usedShield;
                },
                () => {
                    timeTravelerShieldButton.Timer = timeTravelerShieldButton.MaxTimer;
                    timeTravelerShieldButton.isEffectActive = false;
                    timeTravelerShieldButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                TimeTraveler.getShieldButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q,
                true,
                TimeTraveler.shieldDuration,
                () => { timeTravelerShieldButton.Timer = timeTravelerShieldButton.MaxTimer; }
            );

            // TimeTraveler rewind time
            timeTravelerRewindTimeButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == TimeTraveler.timeTraveler.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(TimeTraveler.timeTraveler.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(TimeTraveler.timeTraveler.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        timeTravelerRewindTimeButton.Timer = timeTravelerRewindTimeButton.MaxTimer;
                        return;
                    }

                    timeTravelerRewindTimeButton.Timer = 0f;
                    TimeTraveler.usedRewind = true;
                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.TimeTravelerRewindTime, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.timeTravelerRewindTime();
                },
                () => { return TimeTraveler.timeTraveler != null && TimeTraveler.timeTraveler == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    bool sabotageActive = false;
                    if (Bomberman.activeBomb == true || Ilusionist.lightsOutTimer > 0) {
                        sabotageActive = true;
                    }
                    else {
                        foreach (PlayerTask task in PlayerControl.LocalPlayer.myTasks)
                            if (task.TaskType == TaskTypes.FixLights || task.TaskType == TaskTypes.RestoreOxy || task.TaskType == TaskTypes.ResetReactor || task.TaskType == TaskTypes.ResetSeismic || task.TaskType == TaskTypes.FixComms || task.TaskType == TaskTypes.StopCharles)
                                sabotageActive = true;
                    }
                    return !sabotageActive && !TimeTraveler.usedRewind && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling;
                },
                () => { timeTravelerRewindTimeButton.Timer = timeTravelerRewindTimeButton.MaxTimer; },
                TimeTraveler.getRewindButtonSprite(),
                new Vector3(-3f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Squire Shield
            squireShieldButton = new CustomButton(
                () => {
                    MurderAttemptResult murder = Helpers.checkMurderAttempt(Squire.squire, Squire.currentTarget);
                    if (murder == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        squireShieldButton.Timer = squireShieldButton.MaxTimer;
                        Squire.currentTarget = null;
                        return;
                    }

                    squireShieldButton.Timer = 0f;

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SquireSetShielded, Hazel.SendOption.Reliable, -1);
                    writer.Write(Squire.currentTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.squireSetShielded(Squire.currentTarget.PlayerId);
                },
                () => { return Squire.squire != null && Squire.squire == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return !Squire.usedShield && Squire.currentTarget && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => { if (Squire.resetShieldAfterMeeting) Squire.resetShield(); },
                Squire.getButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q
            );

            // FortuneTeller reveal
            fortuneTellerRevealButton = new CustomButton(
                () => {
                    if (FortuneTeller.currentTarget != null) {
                        FortuneTeller.revealTarget = FortuneTeller.currentTarget;
                        fortuneTellerRevealButton.HasEffect = true;
                    }
                },
                () => { return FortuneTeller.fortuneTeller != null && FortuneTeller.fortuneTeller == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (fortuneTellerRevealButton.isEffectActive && FortuneTeller.revealTarget != FortuneTeller.currentTarget) {
                        FortuneTeller.revealTarget = null;
                        fortuneTellerRevealButton.Timer = 0f;
                        fortuneTellerRevealButton.isEffectActive = false;
                    }

                    return PlayerControl.LocalPlayer.CanMove && FortuneTeller.currentTarget != null && !FortuneTeller.usedFortune && !Challenger.isDueling;
                },
                () => {
                    FortuneTeller.revealTarget = null;
                    fortuneTellerRevealButton.isEffectActive = false;
                    fortuneTellerRevealButton.Timer = fortuneTellerRevealButton.MaxTimer;
                    fortuneTellerRevealButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                FortuneTeller.getButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q,
                true,
                FortuneTeller.duration,
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == FortuneTeller.fortuneTeller.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(FortuneTeller.fortuneTeller.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(FortuneTeller.fortuneTeller.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        FortuneTeller.revealTarget = null;
                        fortuneTellerRevealButton.Timer = fortuneTellerRevealButton.MaxTimer;
                        return;
                    }

                    if (FortuneTeller.revealTarget != null) {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.FortuneTellerReveal, Hazel.SendOption.Reliable, -1);
                        writer.Write(FortuneTeller.currentTarget.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.fortuneTellerReveal(FortuneTeller.currentTarget.PlayerId);
                        FortuneTeller.revealTarget = null;
                        fortuneTellerRevealButton.Timer = fortuneTellerRevealButton.MaxTimer;
                    }
                }
            );

            // FortuneTeller button remaining uses text
            FortuneTeller.fortuneTellerRevealButtonText = GameObject.Instantiate(fortuneTellerRevealButton.actionButton.cooldownTimerText, fortuneTellerRevealButton.actionButton.cooldownTimerText.transform.parent);
            FortuneTeller.fortuneTellerRevealButtonText.enableWordWrapping = false;
            FortuneTeller.fortuneTellerRevealButtonText.transform.localScale = Vector3.one * 0.5f;
            FortuneTeller.fortuneTellerRevealButtonText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);

            // Hacker button
            hackerButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Hacker.hacker.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Hacker.hacker.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Hacker.hacker.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);

                        Hacker.duration = quackNumber;
                        hackerButton.EffectDuration = Hacker.duration;
                        hackerButton.Timer = hackerButton.MaxTimer;
                        return;
                    }

                    Hacker.duration = Hacker.backUpduration;
                    hackerButton.EffectDuration = Hacker.duration;
                    hackerButton.Timer = hackerButton.MaxTimer;

                    Hacker.hackerTimer = Hacker.duration;
                },
                () => { return Hacker.hacker != null && Hacker.hacker == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => {
                    hackerButton.Timer = hackerButton.MaxTimer;
                    hackerButton.isEffectActive = false;
                    hackerButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                Hacker.getButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q,
                true,
                0f,
                () => {
                    hackerButton.Timer = hackerButton.MaxTimer;
                }
            );

            hackerAdminTableButton = new CustomButton(
               () => {
                   if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Hacker.hacker.Data.PlayerId)) {
                       MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                       writerKiller.Write(Hacker.hacker.PlayerId);
                       writerKiller.Write((byte)0);
                       AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                       RPCProcedure.setJinxed(Hacker.hacker.PlayerId, 0);

                       SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                       Hacker.duration = quackNumber;
                       hackerAdminTableButton.EffectDuration = Hacker.duration;
                       hackerAdminTableButton.Timer = hackerAdminTableButton.MaxTimer;
                       return;
                   }

                   Hacker.duration = Hacker.backUpduration;
                   hackerAdminTableButton.EffectDuration = Hacker.duration;

                   if (!MapBehaviour.Instance || !MapBehaviour.Instance.isActiveAndEnabled)
                       DestroyableSingleton<HudManager>.Instance.ShowMap((System.Action<MapBehaviour>)(m => m.ShowCountOverlay()));

                   PlayerControl.LocalPlayer.NetTransform.Halt();

                   MessageWriter usedAdminWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.HackerAbilityUses, Hazel.SendOption.Reliable, -1);
                   usedAdminWriter.Write(0);
                   AmongUsClient.Instance.FinishRpcImmediately(usedAdminWriter);
                   RPCProcedure.hackerAbilityUses(0); 
               },
               () => { return Hacker.hacker != null && Hacker.hacker == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
               () => {
                   if (Hacker.hackerAdminTableChargesText != null) Hacker.hackerAdminTableChargesText.text = $"{Hacker.chargesAdminTable} / {Hacker.toolsNumber}";
                   return PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling && Hacker.chargesAdminTable > 0;
               },
               () => {
                   hackerAdminTableButton.Timer = hackerAdminTableButton.MaxTimer;
                   hackerAdminTableButton.isEffectActive = false;
                   hackerAdminTableButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
               },
               Hacker.getAdminSprite(),
               new Vector3(-3f, -0.06f, 0),
               __instance,
               KeyCode.Q,
               true,
               0f,
               () => {
                   hackerAdminTableButton.Timer = hackerAdminTableButton.MaxTimer;
                   if (MapBehaviour.Instance && MapBehaviour.Instance.isActiveAndEnabled) MapBehaviour.Instance.Close();
               }
           );

            // Hacker Admin Table Charges
            Hacker.hackerAdminTableChargesText = GameObject.Instantiate(hackerAdminTableButton.actionButton.cooldownTimerText, hackerAdminTableButton.actionButton.cooldownTimerText.transform.parent);
            Hacker.hackerAdminTableChargesText.text = "";
            Hacker.hackerAdminTableChargesText.enableWordWrapping = false;
            Hacker.hackerAdminTableChargesText.transform.localScale = Vector3.one * 0.5f;
            Hacker.hackerAdminTableChargesText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);

            hackerVitalsButton = new CustomButton(
               () => {
                   if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Hacker.hacker.Data.PlayerId)) {
                       MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                       writerKiller.Write(Hacker.hacker.PlayerId);
                       writerKiller.Write((byte)0);
                       AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                       RPCProcedure.setJinxed(Hacker.hacker.PlayerId, 0);

                       SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                       Hacker.duration = quackNumber;
                       hackerVitalsButton.EffectDuration = Hacker.duration;
                       hackerVitalsButton.Timer = hackerVitalsButton.MaxTimer; return;
                   }

                   Hacker.duration = Hacker.backUpduration;
                   hackerVitalsButton.EffectDuration = Hacker.duration;

                   if (PlayerControl.GameOptions.MapId != 1) {
                       if (Hacker.vitals == null) {
                           var e = UnityEngine.Object.FindObjectsOfType<SystemConsole>().FirstOrDefault(x => x.gameObject.name.Contains("panel_vitals"));
                           if (e == null || Camera.main == null) return;
                           Hacker.vitals = UnityEngine.Object.Instantiate(e.MinigamePrefab, Camera.main.transform, false);
                       }
                       Hacker.vitals.transform.SetParent(Camera.main.transform, false);
                       Hacker.vitals.transform.localPosition = new Vector3(0.0f, 0.0f, -50f);
                       Hacker.vitals.Begin(null);
                   }

                   MessageWriter usedVitalsWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.HackerAbilityUses, Hazel.SendOption.Reliable, -1);
                   usedVitalsWriter.Write(1);
                   AmongUsClient.Instance.FinishRpcImmediately(usedVitalsWriter);
                   RPCProcedure.hackerAbilityUses(1); 
            },
            () => { return Hacker.hacker != null && Hacker.hacker == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && PlayerControl.GameOptions.MapId != 0 && PlayerControl.GameOptions.MapId != 1 && PlayerControl.GameOptions.MapId != 3; },
            () => {
                if (Hacker.hackerVitalsChargesText != null) Hacker.hackerVitalsChargesText.text = $"{Hacker.chargesVitals} / {Hacker.toolsNumber}";
                return PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling && Hacker.chargesVitals > 0;
            },
               () => {
                   hackerVitalsButton.Timer = hackerVitalsButton.MaxTimer;
                   hackerVitalsButton.isEffectActive = false;
                   hackerVitalsButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
               },
               Hacker.getVitalsSprite(),
               new Vector3(-4.1f, -0.06f, 0),
               __instance,
               KeyCode.Q,
               true,
               0f,
               () => {
                   hackerVitalsButton.Timer = hackerVitalsButton.MaxTimer;
                   if (Minigame.Instance) Hacker.vitals.ForceClose();
               },
               false
           );

            // Hacker Vitals Charges
            Hacker.hackerVitalsChargesText = GameObject.Instantiate(hackerVitalsButton.actionButton.cooldownTimerText, hackerVitalsButton.actionButton.cooldownTimerText.transform.parent);
            Hacker.hackerVitalsChargesText.text = "";
            Hacker.hackerVitalsChargesText.enableWordWrapping = false;
            Hacker.hackerVitalsChargesText.transform.localScale = Vector3.one * 0.5f;
            Hacker.hackerVitalsChargesText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);

            // Sleuth locate button
            sleuthLocatePlayerButton = new CustomButton(
                () => {
                    MurderAttemptResult murderAttemptResult = Helpers.checkMurderAttempt(Sleuth.sleuth, Sleuth.currentTarget);
                    if (murderAttemptResult == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        sleuthLocatePlayerButton.Timer = sleuthLocatePlayerButton.MaxTimer;
                        Sleuth.currentTarget = null;
                        return;
                    }

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SleuthUsedLocate, Hazel.SendOption.Reliable, -1);
                    writer.Write(Sleuth.currentTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.sleuthUsedLocate(Sleuth.currentTarget.PlayerId);
                },
                () => { return Sleuth.sleuth != null && Sleuth.sleuth == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && Sleuth.currentTarget != null && !Sleuth.usedLocate && !Challenger.isDueling; },
                () => { if (Sleuth.resetTargetAfterMeeting) Sleuth.resetLocated(); },
                Sleuth.getLocateButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q
            );

            // Sleuth locate corpses
            sleuthLocateCorpsesButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Sleuth.sleuth.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Sleuth.sleuth.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Sleuth.sleuth.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);

                        Sleuth.corpsesPathfindDuration = quackNumber;
                        sleuthLocateCorpsesButton.EffectDuration = Sleuth.corpsesPathfindDuration;
                        sleuthLocateCorpsesButton.Timer = sleuthLocateCorpsesButton.MaxTimer;
                        return;
                    }

                    Sleuth.corpsesPathfindDuration = Sleuth.backUpduration;
                    sleuthLocateCorpsesButton.EffectDuration = Sleuth.corpsesPathfindDuration;

                    Sleuth.corpsesPathfindTimer = Sleuth.corpsesPathfindDuration;
                },
                () => { return Sleuth.sleuth != null && Sleuth.sleuth == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling; },
                () => {
                    sleuthLocateCorpsesButton.Timer = sleuthLocateCorpsesButton.MaxTimer;
                    sleuthLocateCorpsesButton.isEffectActive = false;
                    sleuthLocateCorpsesButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                Sleuth.getCorpsePathfindButtonSprite(),
                new Vector3(-3f, -0.06f, 0),
                __instance,
                KeyCode.T,
                true,
                Sleuth.corpsesPathfindDuration,
                () => {
                    sleuthLocateCorpsesButton.Timer = sleuthLocateCorpsesButton.MaxTimer;
                }
            );

            // Welder button
            welderSealButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Welder.welder.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Welder.welder.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Welder.welder.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        welderSealButton.Timer = welderSealButton.MaxTimer;
                        return;
                    }

                    if (Welder.ventTarget != null) {
                        MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SealVent, Hazel.SendOption.Reliable);
                        writer.WritePacked(Welder.ventTarget.Id);
                        writer.EndMessage();
                        RPCProcedure.sealVent(Welder.ventTarget.Id);
                        Welder.ventsSealed.Add(Welder.ventTarget);
                        Welder.ventTarget = null;
                    }
                    welderSealButton.Timer = welderSealButton.MaxTimer;
                },
                () => { return Welder.welder != null && Welder.welder == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    bool canSeal = true;
                    if (Welder.ventTarget != null) {
                        foreach (Vent vent in Welder.ventsSealed) {
                            if (vent == Welder.ventTarget) {
                                canSeal = false;
                            }
                        }
                    }

                    return Welder.ventTarget != null && canSeal && Welder.remainingWelds > 0 && Welder.remainingWelds <= Welder.totalWelds && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling;
                },
                () => { welderSealButton.Timer = welderSealButton.MaxTimer; },
                Welder.getCloseVentButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q
            );

            // Welder button remaining uses text
            Welder.welderButtonText = GameObject.Instantiate(welderSealButton.actionButton.cooldownTimerText, welderSealButton.actionButton.cooldownTimerText.transform.parent);
            Welder.welderButtonText.enableWordWrapping = false;
            Welder.welderButtonText.transform.localScale = Vector3.one * 0.5f;
            Welder.welderButtonText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);

            //Spiritualist revive
            spiritualistReviveButton = new CustomButton(
                () => {
                    spiritualistReviveButton.HasEffect = true;

                    MessageWriter isRevivingSpiritualist = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SendSpiritualistIsReviving, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(isRevivingSpiritualist);
                    RPCProcedure.sendSpiritualistIsReviving();
                },
                () => { return Spiritualist.spiritualist != null && !Spiritualist.usedRevive && Spiritualist.spiritualist == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (spiritualistReviveButton.isEffectActive && (!Spiritualist.isReviving || !Spiritualist.canRevive)) {
                        MessageWriter resetSpiritualist = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.ResetSpiritualistReviveValues, Hazel.SendOption.Reliable, -1);
                        AmongUsClient.Instance.FinishRpcImmediately(resetSpiritualist);
                        RPCProcedure.resetSpiritualistReviveValues();
                    }
                    Spiritualist.canRevive = false;
                    foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(PlayerControl.LocalPlayer.GetTruePosition(), 1f, Constants.PlayersOnlyMask))
                        if (collider2D.tag == "DeadBody")
                            Spiritualist.canRevive = true;
                    return Spiritualist.canRevive && !Spiritualist.usedRevive && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling;
                },
                () => {
                    spiritualistReviveButton.Timer = spiritualistReviveButton.MaxTimer;
                    spiritualistReviveButton.isEffectActive = false;
                    Spiritualist.isReviving = false;
                },
                Spiritualist.getButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q,
                true,
                Spiritualist.spiritualistReviveTime,
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Spiritualist.spiritualist.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Spiritualist.spiritualist.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Spiritualist.spiritualist.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        spiritualistReviveButton.Timer = spiritualistReviveButton.MaxTimer;
                        return;
                    }

                    if (Spiritualist.isReviving & Spiritualist.canRevive) {
                        foreach (Collider2D collider2D in Physics2D.OverlapCircleAll(PlayerControl.LocalPlayer.GetTruePosition(), 1f, Constants.PlayersOnlyMask)) {
                            if (collider2D.tag == "DeadBody") {
                                DeadBody component = collider2D.GetComponent<DeadBody>();
                                if (component && !component.Reported) {
                                    Vector2 truePosition = PlayerControl.LocalPlayer.GetTruePosition();
                                    Vector2 truePosition2 = component.TruePosition;
                                    if (Vector2.Distance(truePosition2, truePosition) <= PlayerControl.LocalPlayer.MaxReportDistance && PlayerControl.LocalPlayer.CanMove && !PhysicsHelpers.AnythingBetween(truePosition, truePosition2, Constants.ShipAndObjectsMask, false)) {
                                        GameData.PlayerInfo playerInfo = GameData.Instance.GetPlayerById(component.ParentId);

                                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SpiritualistRevive, Hazel.SendOption.Reliable, -1);
                                        writer.Write(playerInfo.PlayerId);
                                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                                        RPCProcedure.spiritualistRevive(playerInfo.PlayerId);

                                        spiritualistReviveButton.Timer = spiritualistReviveButton.MaxTimer;

                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            );

            // Vigilant camera button
            vigilantButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Vigilant.vigilant.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Vigilant.vigilant.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Vigilant.vigilant.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        vigilantButton.Timer = vigilantButton.MaxTimer;
                        return;
                    }

                    if (PlayerControl.GameOptions.MapId != 1) { 
                        var pos = PlayerControl.LocalPlayer.transform.position;
                        byte[] buff = new byte[sizeof(float) * 2];
                        Buffer.BlockCopy(BitConverter.GetBytes(pos.x), 0, buff, 0 * sizeof(float), sizeof(float));
                        Buffer.BlockCopy(BitConverter.GetBytes(pos.y), 0, buff, 1 * sizeof(float), sizeof(float));

                        MessageWriter writer = AmongUsClient.Instance.StartRpc(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PlaceCamera, Hazel.SendOption.Reliable);
                        writer.WriteBytesAndSize(buff);
                        writer.EndMessage();
                        RPCProcedure.placeCamera(buff);
                    }
                    vigilantButton.Timer = vigilantButton.MaxTimer;
                },
                () => { return Vigilant.vigilant != null && Vigilant.vigilant == PlayerControl.LocalPlayer && Vigilant.placedCameras < 4 && !PlayerControl.LocalPlayer.Data.IsDead && PlayerControl.GameOptions.MapId != 1; },
                () => {
                    return Vigilant.remainingCameras > 0 && Vigilant.remainingCameras <= Vigilant.totalCameras && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling;
                },
                () => { vigilantButton.Timer = vigilantButton.MaxTimer; },
                Vigilant.getPlaceCameraButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q
            );

            // Vigilant button remaining camera text
            Vigilant.vigilantButtonCameraText = GameObject.Instantiate(vigilantButton.actionButton.cooldownTimerText, vigilantButton.actionButton.cooldownTimerText.transform.parent);
            Vigilant.vigilantButtonCameraText.enableWordWrapping = false;
            Vigilant.vigilantButtonCameraText.transform.localScale = Vector3.one * 0.5f;
            Vigilant.vigilantButtonCameraText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);

            // Vigilant view cam button
            vigilantCamButton = new CustomButton(
                () => {
                    if (Jinx.jinxedList.Any(p => p.Data.PlayerId == Vigilant.vigilant.Data.PlayerId)) {
                        MessageWriter writerKiller = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writerKiller.Write(Vigilant.vigilant.PlayerId);
                        writerKiller.Write((byte)0);
                        AmongUsClient.Instance.FinishRpcImmediately(writerKiller);
                        RPCProcedure.setJinxed(Vigilant.vigilant.PlayerId, 0);

                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);

                        Vigilant.duration = quackNumber;
                        vigilantCamButton.EffectDuration = Vigilant.duration;
                        vigilantCamButton.Timer = vigilantCamButton.MaxTimer;
                        return;
                    }

                    switch (PlayerControl.GameOptions.MapId) {
                        case 0:
                            if (Vigilant.minigame == null) {
                                var e = UnityEngine.Object.FindObjectsOfType<SystemConsole>().FirstOrDefault(x => x.gameObject.name.Contains("SurvConsole"));
                                if (e == null || Camera.main == null) return;
                                Vigilant.minigame = UnityEngine.Object.Instantiate(e.MinigamePrefab, Camera.main.transform, false);
                            }
                            break;
                        case 2:
                            if (Vigilant.minigame == null) {
                                var e = UnityEngine.Object.FindObjectsOfType<SystemConsole>().FirstOrDefault(x => x.gameObject.name.Contains("Surv_Panel"));
                                if (e == null || Camera.main == null) return;
                                Vigilant.minigame = UnityEngine.Object.Instantiate(e.MinigamePrefab, Camera.main.transform, false);
                            }
                            break;
                        case 3:
                            if (Vigilant.minigame == null) {
                                var e = UnityEngine.Object.FindObjectsOfType<SystemConsole>().FirstOrDefault(x => x.gameObject.name.Contains("SurvConsole"));
                                if (e == null || Camera.main == null) return;
                                Vigilant.minigame = UnityEngine.Object.Instantiate(e.MinigamePrefab, Camera.main.transform, false);
                            }
                            break;
                        case 4:
                            if (Vigilant.minigame == null) {
                                var e = UnityEngine.Object.FindObjectsOfType<SystemConsole>().FirstOrDefault(x => x.gameObject.name.Contains("task_cams"));
                                if (e == null || Camera.main == null) return;
                                Vigilant.minigame = UnityEngine.Object.Instantiate(e.MinigamePrefab, Camera.main.transform, false);
                            }
                            break;
                    }
                    Vigilant.minigame.transform.SetParent(Camera.main.transform, false);
                    Vigilant.minigame.transform.localPosition = new Vector3(0.0f, 0.0f, -50f);
                    Vigilant.minigame.Begin(null);

                    MessageWriter usedCamsWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.VigilantAbilityUses, Hazel.SendOption.Reliable, -1);
                    usedCamsWriter.Write(0);
                    AmongUsClient.Instance.FinishRpcImmediately(usedCamsWriter);
                    RPCProcedure.vigilantAbilityUses(0);

                    PlayerControl.LocalPlayer.NetTransform.Halt();

                    Vigilant.duration = Vigilant.backUpduration;
                    vigilantCamButton.EffectDuration = Vigilant.duration;

                },
                () => { return Vigilant.vigilant != null && Vigilant.vigilant == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && Vigilant.placedCameras >= 4 && PlayerControl.GameOptions.MapId != 1; },
                () => {
                    if (Vigilant.vigilantButtonCameraUsesText != null) Vigilant.vigilantButtonCameraUsesText.text = $"{Vigilant.charges} / {Vigilant.maxCharges}";
                    return PlayerControl.LocalPlayer.CanMove && Vigilant.charges > 0;
                },
                () => {
                    vigilantCamButton.Timer = vigilantCamButton.MaxTimer;
                    vigilantCamButton.isEffectActive = false;
                    vigilantCamButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                Vigilant.getCamSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q,
                true,
                0f,
                () => {
                    vigilantCamButton.Timer = vigilantCamButton.MaxTimer;
                    if (Minigame.Instance) {
                        Vigilant.minigame.ForceClose();
                    }
                    PlayerControl.LocalPlayer.moveable = true;
                },
                false
            );

            // Vigilant cam button charges
            Vigilant.vigilantButtonCameraUsesText = GameObject.Instantiate(vigilantCamButton.actionButton.cooldownTimerText, vigilantCamButton.actionButton.cooldownTimerText.transform.parent);
            Vigilant.vigilantButtonCameraUsesText.text = "";
            Vigilant.vigilantButtonCameraUsesText.enableWordWrapping = false;
            Vigilant.vigilantButtonCameraUsesText.transform.localScale = Vector3.one * 0.5f;
            Vigilant.vigilantButtonCameraUsesText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);
            
            // Hunter button
            hunterButton = new CustomButton(
                () => {
                    MurderAttemptResult murderAttemptResult = Helpers.checkMurderAttempt(Hunter.hunter, Hunter.currentTarget);
                    if (murderAttemptResult == MurderAttemptResult.JinxKill) {
                        SoundManager.Instance.PlaySound(CustomMain.customAssets.jinxQuack, false, 5f);
                        hunterButton.Timer = hunterButton.MaxTimer;
                        Hunter.currentTarget = null;
                        return;
                    }

                    MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.HunterUsedHunted, Hazel.SendOption.Reliable, -1);
                    writer.Write(Hunter.currentTarget.PlayerId);
                    AmongUsClient.Instance.FinishRpcImmediately(writer);
                    RPCProcedure.hunterUsedHunted(Hunter.currentTarget.PlayerId);
                },
                () => { return Hunter.hunter != null && Hunter.hunter == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && Hunter.currentTarget != null && !Hunter.usedHunted && !Challenger.isDueling; },
                () => { if (Hunter.resetTargetAfterMeeting) Hunter.resetHunted(); },
                Hunter.getButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.Q
            );

            // Jinx button
            jinxButton = new CustomButton(
                () => {
                    if (Jinx.target != null) {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.SetJinxed, Hazel.SendOption.Reliable, -1);
                        writer.Write(Jinx.target.PlayerId);
                        writer.Write(Byte.MaxValue);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.setJinxed(Jinx.target.PlayerId, Byte.MaxValue);

                        Jinx.target = null;

                        jinxButton.Timer = jinxButton.MaxTimer;
                    }

                },
                () => { return Jinx.jinx != null && Jinx.jinx == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead && Jinx.jinxs < Jinx.jinxNumber; },
                () => {
                    if (Jinx.jinxButtonJinxsText != null) Jinx.jinxButtonJinxsText.text = $"{Jinx.jinxNumber - Jinx.jinxs} / {Jinx.jinxNumber}";

                    return Jinx.jinxNumber > Jinx.jinxs && PlayerControl.LocalPlayer.CanMove && !Challenger.isDueling && Jinx.target != null;
                },
                () => { jinxButton.Timer = jinxButton.MaxTimer; },
                Jinx.getTargetSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Jinx button jinxs left
            Jinx.jinxButtonJinxsText = GameObject.Instantiate(jinxButton.actionButton.cooldownTimerText, jinxButton.actionButton.cooldownTimerText.transform.parent);
            Jinx.jinxButtonJinxsText.text = "";
            Jinx.jinxButtonJinxsText.enableWordWrapping = false;
            Jinx.jinxButtonJinxsText.transform.localScale = Vector3.one * 0.5f;
            Jinx.jinxButtonJinxsText.transform.localPosition += new Vector3(-0.05f, 0.7f, 0);


            // Capture the flag buttons
            // Redplayer01 Kill
            redplayer01KillButton = new CustomButton(
                () => {
                    byte targetId = CaptureTheFlag.redplayer01currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CapturetheFlagKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(1);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.capturetheFlagKills(targetId, 1);
                    redplayer01KillButton.Timer = redplayer01KillButton.MaxTimer;
                    CaptureTheFlag.redplayer01currentTarget = null;
                },
                () => { return CaptureTheFlag.redplayer01 != null && CaptureTheFlag.redplayer01 == PlayerControl.LocalPlayer; },
                () => { return CaptureTheFlag.redplayer01currentTarget && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead && !CaptureTheFlag.redplayer01IsReviving && PlayerControl.LocalPlayer != CaptureTheFlag.redPlayerWhoHasBlueFlag; },
                () => { redplayer01KillButton.Timer = redplayer01KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Redplayer01 TakeFlag Button
            redplayer01TakeFlagButton = new CustomButton(
                () => {
                    if (PlayerControl.LocalPlayer == CaptureTheFlag.redPlayerWhoHasBlueFlag) {
                        MessageWriter whichTeamScored = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhichTeamScored, Hazel.SendOption.Reliable, -1);
                        whichTeamScored.Write(1);
                        AmongUsClient.Instance.FinishRpcImmediately(whichTeamScored);
                        RPCProcedure.captureTheFlagWhichTeamScored(1);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        MessageWriter redPlayerStoleBlueFlag = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhoTookTheFlag, Hazel.SendOption.Reliable, -1);
                        redPlayerStoleBlueFlag.Write(targetId);
                        redPlayerStoleBlueFlag.Write(1);
                        AmongUsClient.Instance.FinishRpcImmediately(redPlayerStoleBlueFlag);
                        RPCProcedure.captureTheFlagWhoTookTheFlag(targetId, 1);
                    }
                    redplayer01TakeFlagButton.Timer = redplayer01TakeFlagButton.MaxTimer;
                },
                () => { return CaptureTheFlag.redplayer01 != null && CaptureTheFlag.redplayer01 == PlayerControl.LocalPlayer; },
                () => {
                    if (CaptureTheFlag.localRedFlagArrow.Count != 0) {
                        CaptureTheFlag.localRedFlagArrow[0].Update(CaptureTheFlag.redflag.transform.position);
                    }
                    if (CaptureTheFlag.localBlueFlagArrow.Count != 0) {
                        CaptureTheFlag.localBlueFlagArrow[0].Update(CaptureTheFlag.blueflag.transform.position);
                    }
                    if (CaptureTheFlag.redplayer01 == CaptureTheFlag.redPlayerWhoHasBlueFlag)
                        redplayer01TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getDeliverBlueFlagButtonSprite();
                    else
                        redplayer01TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getTakeBlueFlagButtonSprite();
                    bool CanUse = false;
                    if (CaptureTheFlag.blueflag != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.blueflag.transform.position) < 0.5f && !PlayerControl.LocalPlayer.Data.IsDead && CaptureTheFlag.redPlayerWhoHasBlueFlag == null) {
                        CanUse = true;
                    }
                    else if (CaptureTheFlag.redflagbase != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.redflagbase.transform.position) < 0.5f && PlayerControl.LocalPlayer == CaptureTheFlag.redPlayerWhoHasBlueFlag) {
                        CanUse = true;
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { redplayer01TakeFlagButton.Timer = redplayer01TakeFlagButton.MaxTimer; },
                CaptureTheFlag.getTakeBlueFlagButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Redplayer02 Kill
            redplayer02KillButton = new CustomButton(
                () => {
                    byte targetId = CaptureTheFlag.redplayer02currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CapturetheFlagKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(2);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.capturetheFlagKills(targetId, 2);
                    redplayer02KillButton.Timer = redplayer02KillButton.MaxTimer;
                    CaptureTheFlag.redplayer02currentTarget = null;
                },
                () => { return CaptureTheFlag.redplayer02 != null && CaptureTheFlag.redplayer02 == PlayerControl.LocalPlayer; },
                () => { return CaptureTheFlag.redplayer02currentTarget && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead && !CaptureTheFlag.redplayer02IsReviving && PlayerControl.LocalPlayer != CaptureTheFlag.redPlayerWhoHasBlueFlag; },
                () => { redplayer02KillButton.Timer = redplayer02KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Redplayer02 TakeFlag Button
            redplayer02TakeFlagButton = new CustomButton(
                () => {
                    if (PlayerControl.LocalPlayer == CaptureTheFlag.redPlayerWhoHasBlueFlag) {
                        MessageWriter whichTeamScored = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhichTeamScored, Hazel.SendOption.Reliable, -1);
                        whichTeamScored.Write(1);
                        AmongUsClient.Instance.FinishRpcImmediately(whichTeamScored);
                        RPCProcedure.captureTheFlagWhichTeamScored(1);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        MessageWriter redPlayerStoleBlueFlag = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhoTookTheFlag, Hazel.SendOption.Reliable, -1);
                        redPlayerStoleBlueFlag.Write(targetId);
                        redPlayerStoleBlueFlag.Write(1);
                        AmongUsClient.Instance.FinishRpcImmediately(redPlayerStoleBlueFlag);
                        RPCProcedure.captureTheFlagWhoTookTheFlag(targetId, 1);
                    }
                    redplayer02TakeFlagButton.Timer = redplayer02TakeFlagButton.MaxTimer;
                },
                () => { return CaptureTheFlag.redplayer02 != null && CaptureTheFlag.redplayer02 == PlayerControl.LocalPlayer; },
                () => {
                    if (CaptureTheFlag.localRedFlagArrow.Count != 0) {
                        CaptureTheFlag.localRedFlagArrow[0].Update(CaptureTheFlag.redflag.transform.position);
                    }
                    if (CaptureTheFlag.localBlueFlagArrow.Count != 0) {
                        CaptureTheFlag.localBlueFlagArrow[0].Update(CaptureTheFlag.blueflag.transform.position);
                    }
                    if (CaptureTheFlag.redplayer02 == CaptureTheFlag.redPlayerWhoHasBlueFlag)
                        redplayer02TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getDeliverBlueFlagButtonSprite();
                    else
                        redplayer02TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getTakeBlueFlagButtonSprite();
                    bool CanUse = false;
                    if (CaptureTheFlag.blueflag != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.blueflag.transform.position) < 0.5f && !PlayerControl.LocalPlayer.Data.IsDead && CaptureTheFlag.redPlayerWhoHasBlueFlag == null) {
                        CanUse = true;
                    }
                    else if (CaptureTheFlag.redflagbase != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.redflagbase.transform.position) < 0.5f && PlayerControl.LocalPlayer == CaptureTheFlag.redPlayerWhoHasBlueFlag) {
                        CanUse = true;
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { redplayer02TakeFlagButton.Timer = redplayer02TakeFlagButton.MaxTimer; },
                CaptureTheFlag.getTakeBlueFlagButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Redplayer03 Kill
            redplayer03KillButton = new CustomButton(
                () => {
                    byte targetId = CaptureTheFlag.redplayer03currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CapturetheFlagKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(3);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.capturetheFlagKills(targetId, 3);
                    redplayer03KillButton.Timer = redplayer03KillButton.MaxTimer;
                    CaptureTheFlag.redplayer03currentTarget = null;
                },
                () => { return CaptureTheFlag.redplayer03 != null && CaptureTheFlag.redplayer03 == PlayerControl.LocalPlayer; },
                () => { return CaptureTheFlag.redplayer03currentTarget && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead && !CaptureTheFlag.redplayer03IsReviving && PlayerControl.LocalPlayer != CaptureTheFlag.redPlayerWhoHasBlueFlag; },
                () => { redplayer03KillButton.Timer = redplayer03KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Redplayer03 TakeFlag Button
            redplayer03TakeFlagButton = new CustomButton(
                () => {
                    if (PlayerControl.LocalPlayer == CaptureTheFlag.redPlayerWhoHasBlueFlag) {
                        MessageWriter whichTeamScored = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhichTeamScored, Hazel.SendOption.Reliable, -1);
                        whichTeamScored.Write(1);
                        AmongUsClient.Instance.FinishRpcImmediately(whichTeamScored);
                        RPCProcedure.captureTheFlagWhichTeamScored(1);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        MessageWriter redPlayerStoleBlueFlag = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhoTookTheFlag, Hazel.SendOption.Reliable, -1);
                        redPlayerStoleBlueFlag.Write(targetId);
                        redPlayerStoleBlueFlag.Write(1);
                        AmongUsClient.Instance.FinishRpcImmediately(redPlayerStoleBlueFlag);
                        RPCProcedure.captureTheFlagWhoTookTheFlag(targetId, 1);
                    }
                    redplayer03TakeFlagButton.Timer = redplayer03TakeFlagButton.MaxTimer;
                },
                () => { return CaptureTheFlag.redplayer03 != null && CaptureTheFlag.redplayer03 == PlayerControl.LocalPlayer; },
                () => {
                    if (CaptureTheFlag.localRedFlagArrow.Count != 0) {
                        CaptureTheFlag.localRedFlagArrow[0].Update(CaptureTheFlag.redflag.transform.position);
                    }
                    if (CaptureTheFlag.localBlueFlagArrow.Count != 0) {
                        CaptureTheFlag.localBlueFlagArrow[0].Update(CaptureTheFlag.blueflag.transform.position);
                    }
                    if (CaptureTheFlag.redplayer03 == CaptureTheFlag.redPlayerWhoHasBlueFlag)
                        redplayer03TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getDeliverBlueFlagButtonSprite();
                    else
                        redplayer03TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getTakeBlueFlagButtonSprite();
                    bool CanUse = false;
                    if (CaptureTheFlag.blueflag != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.blueflag.transform.position) < 0.5f && !PlayerControl.LocalPlayer.Data.IsDead && CaptureTheFlag.redPlayerWhoHasBlueFlag == null) {
                        CanUse = true;
                    }
                    else if (CaptureTheFlag.redflagbase != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.redflagbase.transform.position) < 0.5f && PlayerControl.LocalPlayer == CaptureTheFlag.redPlayerWhoHasBlueFlag) {
                        CanUse = true;
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { redplayer03TakeFlagButton.Timer = redplayer03TakeFlagButton.MaxTimer; },
                CaptureTheFlag.getTakeBlueFlagButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Redplayer04 Kill
            redplayer04KillButton = new CustomButton(
                () => {
                    byte targetId = CaptureTheFlag.redplayer04currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CapturetheFlagKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(4);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.capturetheFlagKills(targetId, 4);
                    redplayer04KillButton.Timer = redplayer04KillButton.MaxTimer;
                    CaptureTheFlag.redplayer04currentTarget = null;
                },
                () => { return CaptureTheFlag.redplayer04 != null && CaptureTheFlag.redplayer04 == PlayerControl.LocalPlayer; },
                () => { return CaptureTheFlag.redplayer04currentTarget && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead && !CaptureTheFlag.redplayer04IsReviving && PlayerControl.LocalPlayer != CaptureTheFlag.redPlayerWhoHasBlueFlag; },
                () => { redplayer04KillButton.Timer = redplayer04KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Redplayer04 TakeFlag Button
            redplayer04TakeFlagButton = new CustomButton(
                () => {
                    if (PlayerControl.LocalPlayer == CaptureTheFlag.redPlayerWhoHasBlueFlag) {
                        MessageWriter whichTeamScored = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhichTeamScored, Hazel.SendOption.Reliable, -1);
                        whichTeamScored.Write(1);
                        AmongUsClient.Instance.FinishRpcImmediately(whichTeamScored);
                        RPCProcedure.captureTheFlagWhichTeamScored(1);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        MessageWriter redPlayerStoleBlueFlag = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhoTookTheFlag, Hazel.SendOption.Reliable, -1);
                        redPlayerStoleBlueFlag.Write(targetId);
                        redPlayerStoleBlueFlag.Write(1);
                        AmongUsClient.Instance.FinishRpcImmediately(redPlayerStoleBlueFlag);
                        RPCProcedure.captureTheFlagWhoTookTheFlag(targetId, 1);
                    }
                    redplayer04TakeFlagButton.Timer = redplayer04TakeFlagButton.MaxTimer;
                },
                () => { return CaptureTheFlag.redplayer04 != null && CaptureTheFlag.redplayer04 == PlayerControl.LocalPlayer; },
                () => {
                    if (CaptureTheFlag.localRedFlagArrow.Count != 0) {
                        CaptureTheFlag.localRedFlagArrow[0].Update(CaptureTheFlag.redflag.transform.position);
                    }
                    if (CaptureTheFlag.localBlueFlagArrow.Count != 0) {
                        CaptureTheFlag.localBlueFlagArrow[0].Update(CaptureTheFlag.blueflag.transform.position);
                    }
                    if (CaptureTheFlag.redplayer04 == CaptureTheFlag.redPlayerWhoHasBlueFlag)
                        redplayer04TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getDeliverBlueFlagButtonSprite();
                    else
                        redplayer04TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getTakeBlueFlagButtonSprite();
                    bool CanUse = false;
                    if (CaptureTheFlag.blueflag != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.blueflag.transform.position) < 0.5f && !PlayerControl.LocalPlayer.Data.IsDead && CaptureTheFlag.redPlayerWhoHasBlueFlag == null) {
                        CanUse = true;
                    }
                    else if (CaptureTheFlag.redflagbase != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.redflagbase.transform.position) < 0.5f && PlayerControl.LocalPlayer == CaptureTheFlag.redPlayerWhoHasBlueFlag) {
                        CanUse = true;
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { redplayer04TakeFlagButton.Timer = redplayer04TakeFlagButton.MaxTimer; },
                CaptureTheFlag.getTakeBlueFlagButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Redplayer05 Kill
            redplayer05KillButton = new CustomButton(
                () => {
                    byte targetId = CaptureTheFlag.redplayer05currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CapturetheFlagKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(5);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.capturetheFlagKills(targetId, 5);
                    redplayer05KillButton.Timer = redplayer05KillButton.MaxTimer;
                    CaptureTheFlag.redplayer05currentTarget = null;
                },
                () => { return CaptureTheFlag.redplayer05 != null && CaptureTheFlag.redplayer05 == PlayerControl.LocalPlayer; },
                () => { return CaptureTheFlag.redplayer05currentTarget && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead && !CaptureTheFlag.redplayer05IsReviving && PlayerControl.LocalPlayer != CaptureTheFlag.redPlayerWhoHasBlueFlag; },
                () => { redplayer05KillButton.Timer = redplayer05KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Redplayer05 TakeFlag Button
            redplayer05TakeFlagButton = new CustomButton(
                () => {
                    if (PlayerControl.LocalPlayer == CaptureTheFlag.redPlayerWhoHasBlueFlag) {
                        MessageWriter whichTeamScored = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhichTeamScored, Hazel.SendOption.Reliable, -1);
                        whichTeamScored.Write(1);
                        AmongUsClient.Instance.FinishRpcImmediately(whichTeamScored);
                        RPCProcedure.captureTheFlagWhichTeamScored(1);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        MessageWriter redPlayerStoleBlueFlag = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhoTookTheFlag, Hazel.SendOption.Reliable, -1);
                        redPlayerStoleBlueFlag.Write(targetId);
                        redPlayerStoleBlueFlag.Write(1);
                        AmongUsClient.Instance.FinishRpcImmediately(redPlayerStoleBlueFlag);
                        RPCProcedure.captureTheFlagWhoTookTheFlag(targetId, 1);
                    }
                    redplayer05TakeFlagButton.Timer = redplayer05TakeFlagButton.MaxTimer;
                },
                () => { return CaptureTheFlag.redplayer05 != null && CaptureTheFlag.redplayer05 == PlayerControl.LocalPlayer; },
                () => {
                    if (CaptureTheFlag.localRedFlagArrow.Count != 0) {
                        CaptureTheFlag.localRedFlagArrow[0].Update(CaptureTheFlag.redflag.transform.position);
                    }
                    if (CaptureTheFlag.localBlueFlagArrow.Count != 0) {
                        CaptureTheFlag.localBlueFlagArrow[0].Update(CaptureTheFlag.blueflag.transform.position);
                    }
                    if (CaptureTheFlag.redplayer05 == CaptureTheFlag.redPlayerWhoHasBlueFlag)
                        redplayer05TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getDeliverBlueFlagButtonSprite();
                    else
                        redplayer05TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getTakeBlueFlagButtonSprite();
                    bool CanUse = false;
                    if (CaptureTheFlag.blueflag != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.blueflag.transform.position) < 0.5f && !PlayerControl.LocalPlayer.Data.IsDead && CaptureTheFlag.redPlayerWhoHasBlueFlag == null) {
                        CanUse = true;
                    }
                    else if (CaptureTheFlag.redflagbase != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.redflagbase.transform.position) < 0.5f && PlayerControl.LocalPlayer == CaptureTheFlag.redPlayerWhoHasBlueFlag) {
                        CanUse = true;
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { redplayer05TakeFlagButton.Timer = redplayer05TakeFlagButton.MaxTimer; },
                CaptureTheFlag.getTakeBlueFlagButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Redplayer06 Kill
            redplayer06KillButton = new CustomButton(
                () => {
                    byte targetId = CaptureTheFlag.redplayer06currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CapturetheFlagKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(6);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.capturetheFlagKills(targetId, 6);
                    redplayer06KillButton.Timer = redplayer06KillButton.MaxTimer;
                    CaptureTheFlag.redplayer06currentTarget = null;
                },
                () => { return CaptureTheFlag.redplayer06 != null && CaptureTheFlag.redplayer06 == PlayerControl.LocalPlayer; },
                () => { return CaptureTheFlag.redplayer06currentTarget && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead && !CaptureTheFlag.redplayer06IsReviving && PlayerControl.LocalPlayer != CaptureTheFlag.redPlayerWhoHasBlueFlag; },
                () => { redplayer06KillButton.Timer = redplayer06KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Redplayer06 TakeFlag Button
            redplayer06TakeFlagButton = new CustomButton(
                () => {
                    if (PlayerControl.LocalPlayer == CaptureTheFlag.redPlayerWhoHasBlueFlag) {
                        MessageWriter whichTeamScored = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhichTeamScored, Hazel.SendOption.Reliable, -1);
                        whichTeamScored.Write(1);
                        AmongUsClient.Instance.FinishRpcImmediately(whichTeamScored);
                        RPCProcedure.captureTheFlagWhichTeamScored(1);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        MessageWriter redPlayerStoleBlueFlag = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhoTookTheFlag, Hazel.SendOption.Reliable, -1);
                        redPlayerStoleBlueFlag.Write(targetId);
                        redPlayerStoleBlueFlag.Write(1);
                        AmongUsClient.Instance.FinishRpcImmediately(redPlayerStoleBlueFlag);
                        RPCProcedure.captureTheFlagWhoTookTheFlag(targetId, 1);
                    }
                    redplayer06TakeFlagButton.Timer = redplayer06TakeFlagButton.MaxTimer;
                },
                () => { return CaptureTheFlag.redplayer06 != null && CaptureTheFlag.redplayer06 == PlayerControl.LocalPlayer; },
                () => {
                    if (CaptureTheFlag.localRedFlagArrow.Count != 0) {
                        CaptureTheFlag.localRedFlagArrow[0].Update(CaptureTheFlag.redflag.transform.position);
                    }
                    if (CaptureTheFlag.localBlueFlagArrow.Count != 0) {
                        CaptureTheFlag.localBlueFlagArrow[0].Update(CaptureTheFlag.blueflag.transform.position);
                    }
                    if (CaptureTheFlag.redplayer06 == CaptureTheFlag.redPlayerWhoHasBlueFlag)
                        redplayer06TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getDeliverBlueFlagButtonSprite();
                    else
                        redplayer06TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getTakeBlueFlagButtonSprite();
                    bool CanUse = false;
                    if (CaptureTheFlag.blueflag != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.blueflag.transform.position) < 0.5f && !PlayerControl.LocalPlayer.Data.IsDead && CaptureTheFlag.redPlayerWhoHasBlueFlag == null) {
                        CanUse = true;
                    }
                    else if (CaptureTheFlag.redflagbase != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.redflagbase.transform.position) < 0.5f && PlayerControl.LocalPlayer == CaptureTheFlag.redPlayerWhoHasBlueFlag) {
                        CanUse = true;
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { redplayer06TakeFlagButton.Timer = redplayer06TakeFlagButton.MaxTimer; },
                CaptureTheFlag.getTakeBlueFlagButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Redplayer07  Kill
            redplayer07KillButton = new CustomButton(
                () => {
                    byte targetId = CaptureTheFlag.redplayer07currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CapturetheFlagKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(7);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.capturetheFlagKills(targetId, 7);
                    redplayer07KillButton.Timer = redplayer07KillButton.MaxTimer;
                    CaptureTheFlag.redplayer07currentTarget = null;
                },
                () => { return CaptureTheFlag.redplayer07 != null && CaptureTheFlag.redplayer07 == PlayerControl.LocalPlayer; },
                () => { return CaptureTheFlag.redplayer07currentTarget && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead && !CaptureTheFlag.redplayer07IsReviving && PlayerControl.LocalPlayer != CaptureTheFlag.redPlayerWhoHasBlueFlag; },
                () => { redplayer07KillButton.Timer = redplayer07KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Redplayer07 TakeFlag Button
            redplayer07TakeFlagButton = new CustomButton(
                () => {
                    if (PlayerControl.LocalPlayer == CaptureTheFlag.redPlayerWhoHasBlueFlag) {
                        MessageWriter whichTeamScored = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhichTeamScored, Hazel.SendOption.Reliable, -1);
                        whichTeamScored.Write(1);
                        AmongUsClient.Instance.FinishRpcImmediately(whichTeamScored);
                        RPCProcedure.captureTheFlagWhichTeamScored(1);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        MessageWriter redPlayerStoleBlueFlag = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhoTookTheFlag, Hazel.SendOption.Reliable, -1);
                        redPlayerStoleBlueFlag.Write(targetId);
                        redPlayerStoleBlueFlag.Write(1);
                        AmongUsClient.Instance.FinishRpcImmediately(redPlayerStoleBlueFlag);
                        RPCProcedure.captureTheFlagWhoTookTheFlag(targetId, 1);
                    }
                    redplayer07TakeFlagButton.Timer = redplayer07TakeFlagButton.MaxTimer;
                },
                () => { return CaptureTheFlag.redplayer07 != null && CaptureTheFlag.redplayer07 == PlayerControl.LocalPlayer; },
                () => {
                    if (CaptureTheFlag.localRedFlagArrow.Count != 0) {
                        CaptureTheFlag.localRedFlagArrow[0].Update(CaptureTheFlag.redflag.transform.position);
                    }
                    if (CaptureTheFlag.localBlueFlagArrow.Count != 0) {
                        CaptureTheFlag.localBlueFlagArrow[0].Update(CaptureTheFlag.blueflag.transform.position);
                    }
                    if (CaptureTheFlag.redplayer07 == CaptureTheFlag.redPlayerWhoHasBlueFlag)
                        redplayer07TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getDeliverBlueFlagButtonSprite();
                    else
                        redplayer07TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getTakeBlueFlagButtonSprite();
                    bool CanUse = false;
                    if (CaptureTheFlag.blueflag != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.blueflag.transform.position) < 0.5f && !PlayerControl.LocalPlayer.Data.IsDead && CaptureTheFlag.redPlayerWhoHasBlueFlag == null) {
                        CanUse = true;
                    }
                    else if (CaptureTheFlag.redflagbase != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.redflagbase.transform.position) < 0.5f && PlayerControl.LocalPlayer == CaptureTheFlag.redPlayerWhoHasBlueFlag) {
                        CanUse = true;
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { redplayer07TakeFlagButton.Timer = redplayer07TakeFlagButton.MaxTimer; },
                CaptureTheFlag.getTakeBlueFlagButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Blueplayer01 Kill
            blueplayer01KillButton = new CustomButton(
                () => {
                    byte targetId = CaptureTheFlag.blueplayer01currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CapturetheFlagKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(9);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.capturetheFlagKills(targetId, 9);
                    blueplayer01KillButton.Timer = blueplayer01KillButton.MaxTimer;
                    CaptureTheFlag.blueplayer01currentTarget = null;
                },
                () => { return CaptureTheFlag.blueplayer01 != null && CaptureTheFlag.blueplayer01 == PlayerControl.LocalPlayer; },
                () => { return CaptureTheFlag.blueplayer01currentTarget && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead && !CaptureTheFlag.blueplayer01IsReviving && PlayerControl.LocalPlayer != CaptureTheFlag.bluePlayerWhoHasRedFlag; },
                () => { blueplayer01KillButton.Timer = blueplayer01KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Blueplayer01 TakeFlag Button
            blueplayer01TakeFlagButton = new CustomButton(
                () => {
                    if (PlayerControl.LocalPlayer == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                        MessageWriter whichTeamScored = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhichTeamScored, Hazel.SendOption.Reliable, -1);
                        whichTeamScored.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(whichTeamScored);
                        RPCProcedure.captureTheFlagWhichTeamScored(2);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        MessageWriter bluePlayerStoleRedFlag = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhoTookTheFlag, Hazel.SendOption.Reliable, -1);
                        bluePlayerStoleRedFlag.Write(targetId);
                        bluePlayerStoleRedFlag.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(bluePlayerStoleRedFlag);
                        RPCProcedure.captureTheFlagWhoTookTheFlag(targetId, 2);
                    }
                    blueplayer01TakeFlagButton.Timer = blueplayer01TakeFlagButton.MaxTimer;
                },
                () => { return CaptureTheFlag.blueplayer01 != null && CaptureTheFlag.blueplayer01 == PlayerControl.LocalPlayer; },
                () => {
                    if (CaptureTheFlag.localRedFlagArrow.Count != 0) {
                        CaptureTheFlag.localRedFlagArrow[0].Update(CaptureTheFlag.redflag.transform.position);
                    }
                    if (CaptureTheFlag.localBlueFlagArrow.Count != 0) {
                        CaptureTheFlag.localBlueFlagArrow[0].Update(CaptureTheFlag.blueflag.transform.position);
                    }
                    if (CaptureTheFlag.blueplayer01 == CaptureTheFlag.bluePlayerWhoHasRedFlag)
                        blueplayer01TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getDeliverRedFlagButtonSprite();
                    else
                        blueplayer01TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getTakeRedFlagButtonSprite();
                    bool CanUse = false;
                    if (CaptureTheFlag.redflag != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.redflag.transform.position) < 0.5f && !PlayerControl.LocalPlayer.Data.IsDead && CaptureTheFlag.bluePlayerWhoHasRedFlag == null) {
                        CanUse = true;
                    }
                    else if (CaptureTheFlag.blueflagbase != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.blueflagbase.transform.position) < 0.5f && PlayerControl.LocalPlayer == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                        CanUse = true;
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { blueplayer01TakeFlagButton.Timer = blueplayer01TakeFlagButton.MaxTimer; },
                CaptureTheFlag.getTakeRedFlagButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Blueplayer02 Kill
            blueplayer02KillButton = new CustomButton(
                () => {
                    byte targetId = CaptureTheFlag.blueplayer02currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CapturetheFlagKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(10);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.capturetheFlagKills(targetId, 10);
                    blueplayer02KillButton.Timer = blueplayer02KillButton.MaxTimer;
                    CaptureTheFlag.blueplayer02currentTarget = null;
                },
                () => { return CaptureTheFlag.blueplayer02 != null && CaptureTheFlag.blueplayer02 == PlayerControl.LocalPlayer; },
                () => { return CaptureTheFlag.blueplayer02currentTarget && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead && !CaptureTheFlag.blueplayer02IsReviving && PlayerControl.LocalPlayer != CaptureTheFlag.bluePlayerWhoHasRedFlag; },
                () => { blueplayer02KillButton.Timer = blueplayer02KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Blueplayer02 TakeFlag Button
            blueplayer02TakeFlagButton = new CustomButton(
                () => {
                    if (PlayerControl.LocalPlayer == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                        MessageWriter whichTeamScored = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhichTeamScored, Hazel.SendOption.Reliable, -1);
                        whichTeamScored.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(whichTeamScored);
                        RPCProcedure.captureTheFlagWhichTeamScored(2);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        MessageWriter bluePlayerStoleRedFlag = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhoTookTheFlag, Hazel.SendOption.Reliable, -1);
                        bluePlayerStoleRedFlag.Write(targetId);
                        bluePlayerStoleRedFlag.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(bluePlayerStoleRedFlag);
                        RPCProcedure.captureTheFlagWhoTookTheFlag(targetId, 2);
                    }
                    blueplayer02TakeFlagButton.Timer = blueplayer02TakeFlagButton.MaxTimer;
                },
                () => { return CaptureTheFlag.blueplayer02 != null && CaptureTheFlag.blueplayer02 == PlayerControl.LocalPlayer; },
                () => {
                    if (CaptureTheFlag.localRedFlagArrow.Count != 0) {
                        CaptureTheFlag.localRedFlagArrow[0].Update(CaptureTheFlag.redflag.transform.position);
                    }
                    if (CaptureTheFlag.localBlueFlagArrow.Count != 0) {
                        CaptureTheFlag.localBlueFlagArrow[0].Update(CaptureTheFlag.blueflag.transform.position);
                    }
                    if (CaptureTheFlag.blueplayer02 == CaptureTheFlag.bluePlayerWhoHasRedFlag)
                        blueplayer02TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getDeliverRedFlagButtonSprite();
                    else
                        blueplayer02TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getTakeRedFlagButtonSprite();
                    bool CanUse = false;
                    if (CaptureTheFlag.redflag != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.redflag.transform.position) < 0.5f && !PlayerControl.LocalPlayer.Data.IsDead && CaptureTheFlag.bluePlayerWhoHasRedFlag == null) {
                        CanUse = true;
                    }
                    else if (CaptureTheFlag.blueflagbase != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.blueflagbase.transform.position) < 0.5f && PlayerControl.LocalPlayer == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                        CanUse = true;
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { blueplayer02TakeFlagButton.Timer = blueplayer02TakeFlagButton.MaxTimer; },
                CaptureTheFlag.getTakeRedFlagButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Blueplayer03 Kill
            blueplayer03KillButton = new CustomButton(
                () => {
                    byte targetId = CaptureTheFlag.blueplayer03currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CapturetheFlagKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(11);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.capturetheFlagKills(targetId, 11);
                    blueplayer03KillButton.Timer = blueplayer03KillButton.MaxTimer;
                    CaptureTheFlag.blueplayer03currentTarget = null;
                },
                () => { return CaptureTheFlag.blueplayer03 != null && CaptureTheFlag.blueplayer03 == PlayerControl.LocalPlayer; },
                () => { return CaptureTheFlag.blueplayer03currentTarget && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead && !CaptureTheFlag.blueplayer03IsReviving && PlayerControl.LocalPlayer != CaptureTheFlag.bluePlayerWhoHasRedFlag; },
                () => { blueplayer03KillButton.Timer = blueplayer03KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Blueplayer03 TakeFlag Button
            blueplayer03TakeFlagButton = new CustomButton(
                () => {
                    if (PlayerControl.LocalPlayer == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                        MessageWriter whichTeamScored = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhichTeamScored, Hazel.SendOption.Reliable, -1);
                        whichTeamScored.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(whichTeamScored);
                        RPCProcedure.captureTheFlagWhichTeamScored(2);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        MessageWriter bluePlayerStoleRedFlag = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhoTookTheFlag, Hazel.SendOption.Reliable, -1);
                        bluePlayerStoleRedFlag.Write(targetId);
                        bluePlayerStoleRedFlag.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(bluePlayerStoleRedFlag);
                        RPCProcedure.captureTheFlagWhoTookTheFlag(targetId, 2);
                    }
                    blueplayer03TakeFlagButton.Timer = blueplayer03TakeFlagButton.MaxTimer;
                },
                () => { return CaptureTheFlag.blueplayer03 != null && CaptureTheFlag.blueplayer03 == PlayerControl.LocalPlayer; },
                () => {
                    if (CaptureTheFlag.localRedFlagArrow.Count != 0) {
                        CaptureTheFlag.localRedFlagArrow[0].Update(CaptureTheFlag.redflag.transform.position);
                    }
                    if (CaptureTheFlag.localBlueFlagArrow.Count != 0) {
                        CaptureTheFlag.localBlueFlagArrow[0].Update(CaptureTheFlag.blueflag.transform.position);
                    }
                    if (CaptureTheFlag.blueplayer03 == CaptureTheFlag.bluePlayerWhoHasRedFlag)
                        blueplayer03TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getDeliverRedFlagButtonSprite();
                    else
                        blueplayer03TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getTakeRedFlagButtonSprite();
                    bool CanUse = false;
                    if (CaptureTheFlag.redflag != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.redflag.transform.position) < 0.5f && !PlayerControl.LocalPlayer.Data.IsDead && CaptureTheFlag.bluePlayerWhoHasRedFlag == null) {
                        CanUse = true;
                    }
                    else if (CaptureTheFlag.blueflagbase != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.blueflagbase.transform.position) < 0.5f && PlayerControl.LocalPlayer == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                        CanUse = true;
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { blueplayer03TakeFlagButton.Timer = blueplayer03TakeFlagButton.MaxTimer; },
                CaptureTheFlag.getTakeRedFlagButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Blueplayer04 Kill
            blueplayer04KillButton = new CustomButton(
                () => {
                    byte targetId = CaptureTheFlag.blueplayer04currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CapturetheFlagKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(12);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.capturetheFlagKills(targetId, 12);
                    blueplayer04KillButton.Timer = blueplayer04KillButton.MaxTimer;
                    CaptureTheFlag.blueplayer04currentTarget = null;
                },
                () => { return CaptureTheFlag.blueplayer04 != null && CaptureTheFlag.blueplayer04 == PlayerControl.LocalPlayer; },
                () => { return CaptureTheFlag.blueplayer04currentTarget && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead && !CaptureTheFlag.blueplayer04IsReviving && PlayerControl.LocalPlayer != CaptureTheFlag.bluePlayerWhoHasRedFlag; },
                () => { blueplayer04KillButton.Timer = blueplayer04KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Blueplayer04 TakeFlag Button
            blueplayer04TakeFlagButton = new CustomButton(
                () => {
                    if (PlayerControl.LocalPlayer == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                        MessageWriter whichTeamScored = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhichTeamScored, Hazel.SendOption.Reliable, -1);
                        whichTeamScored.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(whichTeamScored);
                        RPCProcedure.captureTheFlagWhichTeamScored(2);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        MessageWriter bluePlayerStoleRedFlag = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhoTookTheFlag, Hazel.SendOption.Reliable, -1);
                        bluePlayerStoleRedFlag.Write(targetId);
                        bluePlayerStoleRedFlag.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(bluePlayerStoleRedFlag);
                        RPCProcedure.captureTheFlagWhoTookTheFlag(targetId, 2);
                    }
                    blueplayer04TakeFlagButton.Timer = blueplayer04TakeFlagButton.MaxTimer;
                },
                () => { return CaptureTheFlag.blueplayer04 != null && CaptureTheFlag.blueplayer04 == PlayerControl.LocalPlayer; },
                () => {
                    if (CaptureTheFlag.localRedFlagArrow.Count != 0) {
                        CaptureTheFlag.localRedFlagArrow[0].Update(CaptureTheFlag.redflag.transform.position);
                    }
                    if (CaptureTheFlag.localBlueFlagArrow.Count != 0) {
                        CaptureTheFlag.localBlueFlagArrow[0].Update(CaptureTheFlag.blueflag.transform.position);
                    }
                    if (CaptureTheFlag.blueplayer04 == CaptureTheFlag.bluePlayerWhoHasRedFlag)
                        blueplayer04TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getDeliverRedFlagButtonSprite();
                    else
                        blueplayer04TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getTakeRedFlagButtonSprite();
                    bool CanUse = false;
                    if (CaptureTheFlag.redflag != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.redflag.transform.position) < 0.5f && !PlayerControl.LocalPlayer.Data.IsDead && CaptureTheFlag.bluePlayerWhoHasRedFlag == null) {
                        CanUse = true;
                    }
                    else if (CaptureTheFlag.blueflagbase != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.blueflagbase.transform.position) < 0.5f && PlayerControl.LocalPlayer == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                        CanUse = true;
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { blueplayer04TakeFlagButton.Timer = blueplayer04TakeFlagButton.MaxTimer; },
                CaptureTheFlag.getTakeRedFlagButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Blueplayer05 Kill
            blueplayer05KillButton = new CustomButton(
                () => {
                    byte targetId = CaptureTheFlag.blueplayer05currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CapturetheFlagKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(13);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.capturetheFlagKills(targetId, 13);
                    blueplayer05KillButton.Timer = blueplayer05KillButton.MaxTimer;
                    CaptureTheFlag.blueplayer05currentTarget = null;
                },
                () => { return CaptureTheFlag.blueplayer05 != null && CaptureTheFlag.blueplayer05 == PlayerControl.LocalPlayer; },
                () => { return CaptureTheFlag.blueplayer05currentTarget && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead && !CaptureTheFlag.blueplayer05IsReviving && PlayerControl.LocalPlayer != CaptureTheFlag.bluePlayerWhoHasRedFlag; },
                () => { blueplayer05KillButton.Timer = blueplayer05KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Blueplayer05 TakeFlag Button
            blueplayer05TakeFlagButton = new CustomButton(
                () => {
                    if (PlayerControl.LocalPlayer == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                        MessageWriter whichTeamScored = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhichTeamScored, Hazel.SendOption.Reliable, -1);
                        whichTeamScored.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(whichTeamScored);
                        RPCProcedure.captureTheFlagWhichTeamScored(2);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        MessageWriter bluePlayerStoleRedFlag = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhoTookTheFlag, Hazel.SendOption.Reliable, -1);
                        bluePlayerStoleRedFlag.Write(targetId);
                        bluePlayerStoleRedFlag.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(bluePlayerStoleRedFlag);
                        RPCProcedure.captureTheFlagWhoTookTheFlag(targetId, 2);
                    }
                    blueplayer05TakeFlagButton.Timer = blueplayer05TakeFlagButton.MaxTimer;
                },
                () => { return CaptureTheFlag.blueplayer05 != null && CaptureTheFlag.blueplayer05 == PlayerControl.LocalPlayer; },
                () => {
                    if (CaptureTheFlag.localRedFlagArrow.Count != 0) {
                        CaptureTheFlag.localRedFlagArrow[0].Update(CaptureTheFlag.redflag.transform.position);
                    }
                    if (CaptureTheFlag.localBlueFlagArrow.Count != 0) {
                        CaptureTheFlag.localBlueFlagArrow[0].Update(CaptureTheFlag.blueflag.transform.position);
                    }
                    if (CaptureTheFlag.blueplayer05 == CaptureTheFlag.bluePlayerWhoHasRedFlag)
                        blueplayer05TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getDeliverRedFlagButtonSprite();
                    else
                        blueplayer05TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getTakeRedFlagButtonSprite();
                    bool CanUse = false;
                    if (CaptureTheFlag.redflag != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.redflag.transform.position) < 0.5f && !PlayerControl.LocalPlayer.Data.IsDead && CaptureTheFlag.bluePlayerWhoHasRedFlag == null) {
                        CanUse = true;
                    }
                    else if (CaptureTheFlag.blueflagbase != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.blueflagbase.transform.position) < 0.5f && PlayerControl.LocalPlayer == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                        CanUse = true;
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { blueplayer05TakeFlagButton.Timer = blueplayer05TakeFlagButton.MaxTimer; },
                CaptureTheFlag.getTakeRedFlagButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Blueplayer06 Kill
            blueplayer06KillButton = new CustomButton(
                () => {
                    byte targetId = CaptureTheFlag.blueplayer06currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CapturetheFlagKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(14);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.capturetheFlagKills(targetId, 14);
                    blueplayer06KillButton.Timer = blueplayer06KillButton.MaxTimer;
                    CaptureTheFlag.blueplayer06currentTarget = null;
                },
                () => { return CaptureTheFlag.blueplayer06 != null && CaptureTheFlag.blueplayer06 == PlayerControl.LocalPlayer; },
                () => { return CaptureTheFlag.blueplayer06currentTarget && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead && !CaptureTheFlag.blueplayer06IsReviving && PlayerControl.LocalPlayer != CaptureTheFlag.bluePlayerWhoHasRedFlag; },
                () => { blueplayer06KillButton.Timer = blueplayer06KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Blueplayer06 TakeFlag Button
            blueplayer06TakeFlagButton = new CustomButton(
                () => {
                    if (PlayerControl.LocalPlayer == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                        MessageWriter whichTeamScored = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhichTeamScored, Hazel.SendOption.Reliable, -1);
                        whichTeamScored.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(whichTeamScored);
                        RPCProcedure.captureTheFlagWhichTeamScored(2);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        MessageWriter bluePlayerStoleRedFlag = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhoTookTheFlag, Hazel.SendOption.Reliable, -1);
                        bluePlayerStoleRedFlag.Write(targetId);
                        bluePlayerStoleRedFlag.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(bluePlayerStoleRedFlag);
                        RPCProcedure.captureTheFlagWhoTookTheFlag(targetId, 2);
                    }
                    blueplayer06TakeFlagButton.Timer = blueplayer06TakeFlagButton.MaxTimer;
                },
                () => { return CaptureTheFlag.blueplayer06 != null && CaptureTheFlag.blueplayer06 == PlayerControl.LocalPlayer; },
                () => {
                    if (CaptureTheFlag.localRedFlagArrow.Count != 0) {
                        CaptureTheFlag.localRedFlagArrow[0].Update(CaptureTheFlag.redflag.transform.position);
                    }
                    if (CaptureTheFlag.localBlueFlagArrow.Count != 0) {
                        CaptureTheFlag.localBlueFlagArrow[0].Update(CaptureTheFlag.blueflag.transform.position);
                    }
                    if (CaptureTheFlag.blueplayer06 == CaptureTheFlag.bluePlayerWhoHasRedFlag)
                        blueplayer06TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getDeliverRedFlagButtonSprite();
                    else
                        blueplayer06TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getTakeRedFlagButtonSprite();
                    bool CanUse = false;
                    if (CaptureTheFlag.redflag != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.redflag.transform.position) < 0.5f && !PlayerControl.LocalPlayer.Data.IsDead && CaptureTheFlag.bluePlayerWhoHasRedFlag == null) {
                        CanUse = true;
                    }
                    else if (CaptureTheFlag.blueflagbase != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.blueflagbase.transform.position) < 0.5f && PlayerControl.LocalPlayer == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                        CanUse = true;
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { blueplayer06TakeFlagButton.Timer = blueplayer06TakeFlagButton.MaxTimer; },
                CaptureTheFlag.getTakeRedFlagButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Blueplayer07 Kill
            blueplayer07KillButton = new CustomButton(
                () => {
                    byte targetId = CaptureTheFlag.blueplayer07currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CapturetheFlagKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(15);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.capturetheFlagKills(targetId, 15);
                    blueplayer07KillButton.Timer = blueplayer07KillButton.MaxTimer;
                    CaptureTheFlag.blueplayer07currentTarget = null;
                },
                () => { return CaptureTheFlag.blueplayer07 != null && CaptureTheFlag.blueplayer07 == PlayerControl.LocalPlayer; },
                () => { return CaptureTheFlag.blueplayer07currentTarget && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead && !CaptureTheFlag.blueplayer07IsReviving && PlayerControl.LocalPlayer != CaptureTheFlag.bluePlayerWhoHasRedFlag; },
                () => { blueplayer07KillButton.Timer = blueplayer07KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Blueplayer07 TakeFlag Button
            blueplayer07TakeFlagButton = new CustomButton(
                () => {
                    if (PlayerControl.LocalPlayer == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                        MessageWriter whichTeamScored = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhichTeamScored, Hazel.SendOption.Reliable, -1);
                        whichTeamScored.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(whichTeamScored);
                        RPCProcedure.captureTheFlagWhichTeamScored(2);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        MessageWriter bluePlayerStoleRedFlag = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhoTookTheFlag, Hazel.SendOption.Reliable, -1);
                        bluePlayerStoleRedFlag.Write(targetId);
                        bluePlayerStoleRedFlag.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(bluePlayerStoleRedFlag);
                        RPCProcedure.captureTheFlagWhoTookTheFlag(targetId, 2);
                    }
                    blueplayer07TakeFlagButton.Timer = blueplayer07TakeFlagButton.MaxTimer;
                },
                () => { return CaptureTheFlag.blueplayer07 != null && CaptureTheFlag.blueplayer07 == PlayerControl.LocalPlayer; },
                () => {
                    if (CaptureTheFlag.localRedFlagArrow.Count != 0) {
                        CaptureTheFlag.localRedFlagArrow[0].Update(CaptureTheFlag.redflag.transform.position);
                    }
                    if (CaptureTheFlag.localBlueFlagArrow.Count != 0) {
                        CaptureTheFlag.localBlueFlagArrow[0].Update(CaptureTheFlag.blueflag.transform.position);
                    }
                    if (CaptureTheFlag.blueplayer07 == CaptureTheFlag.bluePlayerWhoHasRedFlag)
                        blueplayer07TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getDeliverRedFlagButtonSprite();
                    else
                        blueplayer07TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getTakeRedFlagButtonSprite();
                    bool CanUse = false;
                    if (CaptureTheFlag.redflag != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.redflag.transform.position) < 0.5f && !PlayerControl.LocalPlayer.Data.IsDead && CaptureTheFlag.bluePlayerWhoHasRedFlag == null) {
                        CanUse = true;
                    }
                    else if (CaptureTheFlag.blueflagbase != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.blueflagbase.transform.position) < 0.5f && PlayerControl.LocalPlayer == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                        CanUse = true;
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { blueplayer07TakeFlagButton.Timer = blueplayer07TakeFlagButton.MaxTimer; },
                CaptureTheFlag.getTakeRedFlagButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Blueplayer08 Kill
            blueplayer08KillButton = new CustomButton(
                () => {
                    byte targetId = CaptureTheFlag.blueplayer08currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CapturetheFlagKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(16);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.capturetheFlagKills(targetId, 16);
                    blueplayer08KillButton.Timer = blueplayer08KillButton.MaxTimer;
                    CaptureTheFlag.blueplayer08currentTarget = null;
                },
                () => { return CaptureTheFlag.blueplayer08 != null && CaptureTheFlag.blueplayer08 == PlayerControl.LocalPlayer; },
                () => { return CaptureTheFlag.blueplayer08currentTarget && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead && !CaptureTheFlag.blueplayer08IsReviving && PlayerControl.LocalPlayer != CaptureTheFlag.bluePlayerWhoHasRedFlag; },
                () => { blueplayer08KillButton.Timer = blueplayer08KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Blueplayer08 TakeFlag Button
            blueplayer08TakeFlagButton = new CustomButton(
                () => {
                    if (PlayerControl.LocalPlayer == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                        MessageWriter whichTeamScored = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhichTeamScored, Hazel.SendOption.Reliable, -1);
                        whichTeamScored.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(whichTeamScored);
                        RPCProcedure.captureTheFlagWhichTeamScored(2);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        MessageWriter bluePlayerStoleRedFlag = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.CaptureTheFlagWhoTookTheFlag, Hazel.SendOption.Reliable, -1);
                        bluePlayerStoleRedFlag.Write(targetId);
                        bluePlayerStoleRedFlag.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(bluePlayerStoleRedFlag);
                        RPCProcedure.captureTheFlagWhoTookTheFlag(targetId, 2);
                    }
                    blueplayer08TakeFlagButton.Timer = blueplayer08TakeFlagButton.MaxTimer;
                },
                () => { return CaptureTheFlag.blueplayer08 != null && CaptureTheFlag.blueplayer08 == PlayerControl.LocalPlayer; },
                () => {
                    if (CaptureTheFlag.localRedFlagArrow.Count != 0) {
                        CaptureTheFlag.localRedFlagArrow[0].Update(CaptureTheFlag.redflag.transform.position);
                    }
                    if (CaptureTheFlag.localBlueFlagArrow.Count != 0) {
                        CaptureTheFlag.localBlueFlagArrow[0].Update(CaptureTheFlag.blueflag.transform.position);
                    }
                    if (CaptureTheFlag.blueplayer08 == CaptureTheFlag.bluePlayerWhoHasRedFlag)
                        blueplayer08TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getDeliverRedFlagButtonSprite();
                    else
                        blueplayer08TakeFlagButton.actionButton.graphic.sprite = CaptureTheFlag.getTakeRedFlagButtonSprite();
                    bool CanUse = false;
                    if (CaptureTheFlag.redflag != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.redflag.transform.position) < 0.5f && !PlayerControl.LocalPlayer.Data.IsDead && CaptureTheFlag.bluePlayerWhoHasRedFlag == null) {
                        CanUse = true;
                    }
                    else if (CaptureTheFlag.blueflagbase != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, CaptureTheFlag.blueflagbase.transform.position) < 0.5f && PlayerControl.LocalPlayer == CaptureTheFlag.bluePlayerWhoHasRedFlag) {
                        CanUse = true;
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { blueplayer08TakeFlagButton.Timer = blueplayer08TakeFlagButton.MaxTimer; },
                CaptureTheFlag.getTakeRedFlagButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Police and Thief Mode
            // Policeplayer01 Kill
            policeplayer01KillButton = new CustomButton(
                () => {
                    byte targetId = PoliceAndThief.policeplayer01currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(1);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.policeandThiefKills(targetId, 1);
                    policeplayer01KillButton.Timer = policeplayer01KillButton.MaxTimer;
                    PoliceAndThief.policeplayer01currentTarget = null;
                },
                () => { return PoliceAndThief.policeplayer01 != null && PoliceAndThief.policeplayer01 == PlayerControl.LocalPlayer; },
                () => {
                    bool CanUse = true;
                    if (PoliceAndThief.cellbutton != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.cellbutton.transform.position) <= 3f && !PlayerControl.LocalPlayer.Data.IsDead && !PoliceAndThief.policeCanKillNearPrison) {
                        CanUse = false;
                    }
                    return CanUse && PoliceAndThief.policeplayer01currentTarget && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.policeplayer01IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { policeplayer01KillButton.Timer = policeplayer01KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Policeplayer01 Jail
            policeplayer01JailButton = new CustomButton(
                () => {
                    if (PoliceAndThief.policeplayer01currentTarget != null) {
                        PoliceAndThief.policeplayer01targetedPlayer = PoliceAndThief.policeplayer01currentTarget;
                        policeplayer01JailButton.HasEffect = true;
                    }
                },
                () => { return PoliceAndThief.policeplayer01 != null && PoliceAndThief.policeplayer01 == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (policeplayer01JailButton.isEffectActive && PoliceAndThief.policeplayer01targetedPlayer != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.policeplayer01targetedPlayer.transform.position) > GameOptionsData.KillDistances[Mathf.Clamp(PlayerControl.GameOptions.KillDistance, 0, 2)]) {
                        PoliceAndThief.policeplayer01targetedPlayer = null;
                        policeplayer01JailButton.Timer = 0f;
                        policeplayer01JailButton.isEffectActive = false;
                    }
                    return PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.policeplayer01IsReviving && PoliceAndThief.policeplayer01currentTarget != null;
                },
                () => {
                    PoliceAndThief.policeplayer01targetedPlayer = null;
                    policeplayer01JailButton.isEffectActive = false;
                    policeplayer01JailButton.Timer = policeplayer01JailButton.MaxTimer;
                    policeplayer01JailButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                PoliceAndThief.getCaptureThiefButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T,
                true,
                PoliceAndThief.captureThiefTime,
                () => {
                    if (PoliceAndThief.policeplayer01targetedPlayer != null) {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefJail, Hazel.SendOption.Reliable, -1);
                        writer.Write(PoliceAndThief.policeplayer01targetedPlayer.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.policeandThiefJail(PoliceAndThief.policeplayer01targetedPlayer.PlayerId);
                        PoliceAndThief.policeplayer01targetedPlayer = null;
                        policeplayer01JailButton.Timer = policeplayer01JailButton.MaxTimer;
                    }
                }
            );

            // Policeplayer01 Light
            policeplayer01LightButton = new CustomButton(
                () => {
                    PoliceAndThief.policeplayer01lightTimer = 10;
                },
                () => { return PoliceAndThief.policeplayer01 != null && PoliceAndThief.policeplayer01 == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.policeplayer01IsReviving; },
                () => {
                    policeplayer01LightButton.Timer = policeplayer01LightButton.MaxTimer;
                    policeplayer01LightButton.isEffectActive = false;
                    policeplayer01LightButton.actionButton.graphic.color = Palette.EnabledColor;
                },
                PoliceAndThief.getLightButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.F,
                true,
                10,
                () => { policeplayer01LightButton.Timer = policeplayer01LightButton.MaxTimer; }
            );

            // Policeplayer02 Kill
            policeplayer02KillButton = new CustomButton(
                () => {
                    byte targetId = PoliceAndThief.policeplayer02currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(2);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.policeandThiefKills(targetId, 2);
                    policeplayer02KillButton.Timer = policeplayer02KillButton.MaxTimer;
                    PoliceAndThief.policeplayer02currentTarget = null;
                },
                () => { return PoliceAndThief.policeplayer02 != null && PoliceAndThief.policeplayer02 == PlayerControl.LocalPlayer; },
                () => {
                    bool CanUse = true;
                    if (PoliceAndThief.cellbutton != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.cellbutton.transform.position) <= 3f && !PlayerControl.LocalPlayer.Data.IsDead && !PoliceAndThief.policeCanKillNearPrison) {
                        CanUse = false;
                    }
                    return CanUse && PoliceAndThief.policeplayer02currentTarget && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.policeplayer02IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { policeplayer02KillButton.Timer = policeplayer02KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Policeplayer02 Jail
            policeplayer02JailButton = new CustomButton(
                () => {
                    if (PoliceAndThief.policeplayer02currentTarget != null) {
                        PoliceAndThief.policeplayer02targetedPlayer = PoliceAndThief.policeplayer02currentTarget;
                        policeplayer02JailButton.HasEffect = true;
                    }
                },
                () => { return PoliceAndThief.policeplayer02 != null && PoliceAndThief.policeplayer02 == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (policeplayer02JailButton.isEffectActive && PoliceAndThief.policeplayer02targetedPlayer != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.policeplayer02targetedPlayer.transform.position) > GameOptionsData.KillDistances[Mathf.Clamp(PlayerControl.GameOptions.KillDistance, 0, 2)]) {
                        PoliceAndThief.policeplayer02targetedPlayer = null;
                        policeplayer02JailButton.Timer = 0f;
                        policeplayer02JailButton.isEffectActive = false;
                    }
                    return PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.policeplayer02IsReviving && PoliceAndThief.policeplayer02currentTarget != null;
                },
                () => {
                    PoliceAndThief.policeplayer02targetedPlayer = null;
                    policeplayer02JailButton.isEffectActive = false;
                    policeplayer02JailButton.Timer = policeplayer02JailButton.MaxTimer;
                    policeplayer02JailButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                PoliceAndThief.getCaptureThiefButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T,
                true,
                PoliceAndThief.captureThiefTime,
                () => {
                    if (PoliceAndThief.policeplayer02targetedPlayer != null) {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefJail, Hazel.SendOption.Reliable, -1);
                        writer.Write(PoliceAndThief.policeplayer02targetedPlayer.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.policeandThiefJail(PoliceAndThief.policeplayer02targetedPlayer.PlayerId);
                        PoliceAndThief.policeplayer02targetedPlayer = null;
                        policeplayer02JailButton.Timer = policeplayer02JailButton.MaxTimer;
                    }
                }
            );

            // Policeplayer02 Light
            policeplayer02LightButton = new CustomButton(
                () => {
                    PoliceAndThief.policeplayer02lightTimer = 10;
                },
                () => { return PoliceAndThief.policeplayer02 != null && PoliceAndThief.policeplayer02 == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.policeplayer02IsReviving; },
                () => {
                    policeplayer02LightButton.Timer = policeplayer02LightButton.MaxTimer;
                    policeplayer02LightButton.isEffectActive = false;
                    policeplayer02LightButton.actionButton.graphic.color = Palette.EnabledColor;
                },
                PoliceAndThief.getLightButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.F,
                true,
                10,
                () => { policeplayer02LightButton.Timer = policeplayer02LightButton.MaxTimer; }
            );

            // Policeplayer03 Kill
            policeplayer03KillButton = new CustomButton(
                () => {
                    byte targetId = PoliceAndThief.policeplayer03currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(3);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.policeandThiefKills(targetId, 3);
                    policeplayer03KillButton.Timer = policeplayer03KillButton.MaxTimer;
                    PoliceAndThief.policeplayer03currentTarget = null;
                },
                () => { return PoliceAndThief.policeplayer03 != null && PoliceAndThief.policeplayer03 == PlayerControl.LocalPlayer; },
                () => {
                    bool CanUse = true;
                    if (PoliceAndThief.cellbutton != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.cellbutton.transform.position) <= 3f && !PlayerControl.LocalPlayer.Data.IsDead && !PoliceAndThief.policeCanKillNearPrison) {
                        CanUse = false;
                    }
                    return CanUse && PoliceAndThief.policeplayer03currentTarget && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.policeplayer03IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { policeplayer03KillButton.Timer = policeplayer03KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Policeplayer03 Jail
            policeplayer03JailButton = new CustomButton(
                () => {
                    if (PoliceAndThief.policeplayer03currentTarget != null) {
                        PoliceAndThief.policeplayer03targetedPlayer = PoliceAndThief.policeplayer03currentTarget;
                        policeplayer03JailButton.HasEffect = true;
                    }
                },
                () => { return PoliceAndThief.policeplayer03 != null && PoliceAndThief.policeplayer03 == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (policeplayer03JailButton.isEffectActive && PoliceAndThief.policeplayer03targetedPlayer != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.policeplayer03targetedPlayer.transform.position) > GameOptionsData.KillDistances[Mathf.Clamp(PlayerControl.GameOptions.KillDistance, 0, 2)]) {
                        PoliceAndThief.policeplayer03targetedPlayer = null;
                        policeplayer03JailButton.Timer = 0f;
                        policeplayer03JailButton.isEffectActive = false;
                    }

                    return PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.policeplayer03IsReviving && PoliceAndThief.policeplayer03currentTarget != null;
                },
                () => {
                    PoliceAndThief.policeplayer03targetedPlayer = null;
                    policeplayer03JailButton.isEffectActive = false;
                    policeplayer03JailButton.Timer = policeplayer03JailButton.MaxTimer;
                    policeplayer03JailButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                PoliceAndThief.getCaptureThiefButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T,
                true,
                PoliceAndThief.captureThiefTime,
                () => {
                    if (PoliceAndThief.policeplayer03targetedPlayer != null) {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefJail, Hazel.SendOption.Reliable, -1);
                        writer.Write(PoliceAndThief.policeplayer03targetedPlayer.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.policeandThiefJail(PoliceAndThief.policeplayer03targetedPlayer.PlayerId);
                        PoliceAndThief.policeplayer03targetedPlayer = null;
                        policeplayer03JailButton.Timer = policeplayer03JailButton.MaxTimer;
                    }
                }
            );

            // Policeplayer03 Light
            policeplayer03LightButton = new CustomButton(
                () => {
                    PoliceAndThief.policeplayer03lightTimer = 10;
                },
                () => { return PoliceAndThief.policeplayer03 != null && PoliceAndThief.policeplayer03 == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.policeplayer03IsReviving; },
                () => {
                    policeplayer03LightButton.Timer = policeplayer03LightButton.MaxTimer;
                    policeplayer03LightButton.isEffectActive = false;
                    policeplayer03LightButton.actionButton.graphic.color = Palette.EnabledColor;
                },
                PoliceAndThief.getLightButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.F,
                true,
                10,
                () => { policeplayer03LightButton.Timer = policeplayer03LightButton.MaxTimer; }
            );

            // Policeplayer04 Kill
            policeplayer04KillButton = new CustomButton(
                () => {
                    byte targetId = PoliceAndThief.policeplayer04currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(4);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.policeandThiefKills(targetId, 4);
                    policeplayer04KillButton.Timer = policeplayer04KillButton.MaxTimer;
                    PoliceAndThief.policeplayer04currentTarget = null;
                },
                () => { return PoliceAndThief.policeplayer04 != null && PoliceAndThief.policeplayer04 == PlayerControl.LocalPlayer; },
                () => {
                    bool CanUse = true;
                    if (PoliceAndThief.cellbutton != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.cellbutton.transform.position) <= 3f && !PlayerControl.LocalPlayer.Data.IsDead && !PoliceAndThief.policeCanKillNearPrison) {
                        CanUse = false;
                    }
                    return CanUse && PoliceAndThief.policeplayer04currentTarget && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.policeplayer04IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { policeplayer04KillButton.Timer = policeplayer04KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Policeplayer04 Jail
            policeplayer04JailButton = new CustomButton(
                () => {
                    if (PoliceAndThief.policeplayer04currentTarget != null) {
                        PoliceAndThief.policeplayer04targetedPlayer = PoliceAndThief.policeplayer04currentTarget;
                        policeplayer04JailButton.HasEffect = true;
                    }
                },
                () => { return PoliceAndThief.policeplayer04 != null && PoliceAndThief.policeplayer04 == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (policeplayer04JailButton.isEffectActive && PoliceAndThief.policeplayer04targetedPlayer != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.policeplayer04targetedPlayer.transform.position) > GameOptionsData.KillDistances[Mathf.Clamp(PlayerControl.GameOptions.KillDistance, 0, 2)]) {
                        PoliceAndThief.policeplayer04targetedPlayer = null;
                        policeplayer04JailButton.Timer = 0f;
                        policeplayer04JailButton.isEffectActive = false;
                    }

                    return PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.policeplayer04IsReviving && PoliceAndThief.policeplayer04currentTarget != null;
                },
                () => {
                    PoliceAndThief.policeplayer04targetedPlayer = null;
                    policeplayer04JailButton.isEffectActive = false;
                    policeplayer04JailButton.Timer = policeplayer04JailButton.MaxTimer;
                    policeplayer04JailButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                PoliceAndThief.getCaptureThiefButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T,
                true,
                PoliceAndThief.captureThiefTime,
                () => {
                    if (PoliceAndThief.policeplayer04targetedPlayer != null) {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefJail, Hazel.SendOption.Reliable, -1);
                        writer.Write(PoliceAndThief.policeplayer04targetedPlayer.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.policeandThiefJail(PoliceAndThief.policeplayer04targetedPlayer.PlayerId);
                        PoliceAndThief.policeplayer04targetedPlayer = null;
                        policeplayer04JailButton.Timer = policeplayer04JailButton.MaxTimer;
                    }
                }
            );

            // Policeplayer04 Light
            policeplayer04LightButton = new CustomButton(
                () => {
                    PoliceAndThief.policeplayer04lightTimer = 10;
                },
                () => { return PoliceAndThief.policeplayer04 != null && PoliceAndThief.policeplayer04 == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.policeplayer04IsReviving; },
                () => {
                    policeplayer04LightButton.Timer = policeplayer04LightButton.MaxTimer;
                    policeplayer04LightButton.isEffectActive = false;
                    policeplayer04LightButton.actionButton.graphic.color = Palette.EnabledColor;
                },
                PoliceAndThief.getLightButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.F,
                true,
                10,
                () => { policeplayer04LightButton.Timer = policeplayer04LightButton.MaxTimer; }
            );

            // Policeplayer05 Kill
            policeplayer05KillButton = new CustomButton(
                () => {
                    byte targetId = PoliceAndThief.policeplayer05currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(5);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.policeandThiefKills(targetId, 5);
                    policeplayer05KillButton.Timer = policeplayer05KillButton.MaxTimer;
                    PoliceAndThief.policeplayer05currentTarget = null;
                },
                () => { return PoliceAndThief.policeplayer05 != null && PoliceAndThief.policeplayer05 == PlayerControl.LocalPlayer; },
                () => {
                    bool CanUse = true;
                    if (PoliceAndThief.cellbutton != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.cellbutton.transform.position) <= 3f && !PlayerControl.LocalPlayer.Data.IsDead && !PoliceAndThief.policeCanKillNearPrison) {
                        CanUse = false;
                    }
                    return CanUse && PoliceAndThief.policeplayer05currentTarget && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.policeplayer05IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { policeplayer05KillButton.Timer = policeplayer05KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Policeplayer05 Jail
            policeplayer05JailButton = new CustomButton(
                () => {
                    if (PoliceAndThief.policeplayer05currentTarget != null) {
                        PoliceAndThief.policeplayer05targetedPlayer = PoliceAndThief.policeplayer05currentTarget;
                        policeplayer05JailButton.HasEffect = true;
                    }
                },
                () => { return PoliceAndThief.policeplayer05 != null && PoliceAndThief.policeplayer05 == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => {
                    if (policeplayer05JailButton.isEffectActive && PoliceAndThief.policeplayer05targetedPlayer != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.policeplayer05targetedPlayer.transform.position) > GameOptionsData.KillDistances[Mathf.Clamp(PlayerControl.GameOptions.KillDistance, 0, 2)]) {
                        PoliceAndThief.policeplayer05targetedPlayer = null;
                        policeplayer05JailButton.Timer = 0f;
                        policeplayer05JailButton.isEffectActive = false;
                    }

                    return PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.policeplayer05IsReviving && PoliceAndThief.policeplayer05currentTarget != null;
                },
                () => {
                    PoliceAndThief.policeplayer05targetedPlayer = null;
                    policeplayer05JailButton.isEffectActive = false;
                    policeplayer05JailButton.Timer = policeplayer05JailButton.MaxTimer;
                    policeplayer05JailButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
                },
                PoliceAndThief.getCaptureThiefButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T,
                true,
                PoliceAndThief.captureThiefTime,
                () => {
                    if (PoliceAndThief.policeplayer05targetedPlayer != null) {
                        MessageWriter writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefJail, Hazel.SendOption.Reliable, -1);
                        writer.Write(PoliceAndThief.policeplayer05targetedPlayer.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);
                        RPCProcedure.policeandThiefJail(PoliceAndThief.policeplayer05targetedPlayer.PlayerId);
                        PoliceAndThief.policeplayer05targetedPlayer = null;
                        policeplayer05JailButton.Timer = policeplayer05JailButton.MaxTimer;
                    }
                }
            );

            // Policeplayer05 Light
            policeplayer05LightButton = new CustomButton(
                () => {
                    PoliceAndThief.policeplayer05lightTimer = 10;
                },
                () => { return PoliceAndThief.policeplayer05 != null && PoliceAndThief.policeplayer05 == PlayerControl.LocalPlayer && !PlayerControl.LocalPlayer.Data.IsDead; },
                () => { return PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.policeplayer05IsReviving; },
                () => {
                    policeplayer05LightButton.Timer = policeplayer05LightButton.MaxTimer;
                    policeplayer05LightButton.isEffectActive = false;
                    policeplayer05LightButton.actionButton.graphic.color = Palette.EnabledColor;
                },
                PoliceAndThief.getLightButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.F,
                true,
                10,
                () => { policeplayer05LightButton.Timer = policeplayer05LightButton.MaxTimer; }
            );

            // Thiefplayer01 Kill
            thiefplayer01KillButton = new CustomButton(
                () => {
                    byte targetId = PoliceAndThief.thiefplayer01currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(7);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.policeandThiefKills(targetId, 7);
                    thiefplayer01KillButton.Timer = thiefplayer01KillButton.MaxTimer;
                    PoliceAndThief.thiefplayer01currentTarget = null;
                },
                () => { return PoliceAndThief.thiefplayer01 != null && PoliceAndThief.thiefplayer01 == PlayerControl.LocalPlayer && !PoliceAndThief.thiefplayer01IsReviving && PoliceAndThief.thiefTeamCanKill; },
                () => { return PoliceAndThief.thiefplayer01currentTarget && PlayerControl.LocalPlayer.CanMove && !PlayerControl.LocalPlayer.Data.IsDead && !PoliceAndThief.thiefplayer01IsStealing; },
                () => { thiefplayer01KillButton.Timer = thiefplayer01KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Thiefplayer01 FreeThief Button
            thiefplayer01FreeThiefButton = new CustomButton(
                () => {
                    MessageWriter thiefFree = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefFreeThief, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(thiefFree);
                    RPCProcedure.policeandThiefFreeThief();
                    thiefplayer01FreeThiefButton.Timer = thiefplayer01FreeThiefButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer01 != null && PoliceAndThief.thiefplayer01 == PlayerControl.LocalPlayer; },
                () => {
                    bool CanUse = false;
                    if (PoliceAndThief.currentThiefsCaptured > 0) {
                        if (Vector2.Distance(PoliceAndThief.thiefplayer01.transform.position, PoliceAndThief.cellbutton.transform.position) < 0.4f && !PoliceAndThief.thiefplayer01.Data.IsDead) {
                            CanUse = true;
                        }
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer01IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer01FreeThiefButton.Timer = thiefplayer01FreeThiefButton.MaxTimer; },
                PoliceAndThief.getFreeThiefButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer01 Take/Deliver Jewel Button
            thiefplayer01TakeDeliverJewelButton = new CustomButton(
                () => {
                    if (PoliceAndThief.thiefplayer01IsStealing) {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer01JewelId;
                        MessageWriter thiefScore = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefDeliverJewel, Hazel.SendOption.Reliable, -1);
                        thiefScore.Write(targetId);
                        thiefScore.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefScore);
                        RPCProcedure.policeandThiefDeliverJewel(targetId, jewelId);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer01JewelId;
                        MessageWriter thiefWhoTookATreasure = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefTakeJewel, Hazel.SendOption.Reliable, -1);
                        thiefWhoTookATreasure.Write(targetId);
                        thiefWhoTookATreasure.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefWhoTookATreasure);
                        RPCProcedure.policeandThiefTakeJewel(targetId, jewelId);
                    }
                    thiefplayer01TakeDeliverJewelButton.Timer = thiefplayer01TakeDeliverJewelButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer01 != null && PoliceAndThief.thiefplayer01 == PlayerControl.LocalPlayer; },
                () => {
                    if (PoliceAndThief.thiefplayer01IsStealing)
                        thiefplayer01TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getDeliverJewelButtonSprite();
                    else
                        thiefplayer01TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getTakeJewelButtonSprite();
                    bool CanUse = false;
                    if (PoliceAndThief.thiefTreasures.Count != 0) {
                        foreach (GameObject jewel in PoliceAndThief.thiefTreasures) {
                            if (jewel != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, jewel.transform.position) < 0.5f && !PoliceAndThief.thiefplayer01IsStealing) {
                                //CanUse = true;
                                switch (jewel.name) {
                                    case "jewel01":
                                        PoliceAndThief.thiefplayer01JewelId = 1;
                                        CanUse = !PoliceAndThief.jewel01BeingStealed;
                                        break;
                                    case "jewel02":
                                        PoliceAndThief.thiefplayer01JewelId = 2;
                                        CanUse = !PoliceAndThief.jewel02BeingStealed;
                                        break;
                                    case "jewel03":
                                        PoliceAndThief.thiefplayer01JewelId = 3;
                                        CanUse = !PoliceAndThief.jewel03BeingStealed;
                                        break;
                                    case "jewel04":
                                        PoliceAndThief.thiefplayer01JewelId = 4;
                                        CanUse = !PoliceAndThief.jewel04BeingStealed;
                                        break;
                                    case "jewel05":
                                        PoliceAndThief.thiefplayer01JewelId = 5;
                                        CanUse = !PoliceAndThief.jewel05BeingStealed;
                                        break;
                                    case "jewel06":
                                        PoliceAndThief.thiefplayer01JewelId = 6;
                                        CanUse = !PoliceAndThief.jewel06BeingStealed;
                                        break;
                                    case "jewel07":
                                        PoliceAndThief.thiefplayer01JewelId = 7;
                                        CanUse = !PoliceAndThief.jewel07BeingStealed;
                                        break;
                                    case "jewel08":
                                        PoliceAndThief.thiefplayer01JewelId = 8;
                                        CanUse = !PoliceAndThief.jewel08BeingStealed;
                                        break;
                                    case "jewel09":
                                        PoliceAndThief.thiefplayer01JewelId = 9;
                                        CanUse = !PoliceAndThief.jewel09BeingStealed;
                                        break;
                                    case "jewel10":
                                        PoliceAndThief.thiefplayer01JewelId = 10;
                                        CanUse = !PoliceAndThief.jewel10BeingStealed;
                                        break;
                                    case "jewel11":
                                        PoliceAndThief.thiefplayer01JewelId = 11;
                                        CanUse = !PoliceAndThief.jewel11BeingStealed;
                                        break;
                                    case "jewel12":
                                        PoliceAndThief.thiefplayer01JewelId = 12;
                                        CanUse = !PoliceAndThief.jewel12BeingStealed;
                                        break;
                                    case "jewel13":
                                        PoliceAndThief.thiefplayer01JewelId = 13;
                                        CanUse = !PoliceAndThief.jewel13BeingStealed;
                                        break;
                                    case "jewel14":
                                        PoliceAndThief.thiefplayer01JewelId = 14;
                                        CanUse = !PoliceAndThief.jewel14BeingStealed;
                                        break;
                                    case "jewel15":
                                        PoliceAndThief.thiefplayer01JewelId = 15;
                                        CanUse = !PoliceAndThief.jewel15BeingStealed;
                                        break;
                                }
                            }
                            else if (PoliceAndThief.jewelbutton != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.jewelbutton.transform.position) < 0.5f && PoliceAndThief.thiefplayer01IsStealing) {
                                CanUse = true;
                            }
                        }

                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer01IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer01TakeDeliverJewelButton.Timer = thiefplayer01TakeDeliverJewelButton.MaxTimer; },
                PoliceAndThief.getTakeJewelButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer02 Kill
            thiefplayer02KillButton = new CustomButton(
                () => {
                    byte targetId = PoliceAndThief.thiefplayer02currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(8);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.policeandThiefKills(targetId, 8);
                    thiefplayer02KillButton.Timer = thiefplayer02KillButton.MaxTimer;
                    PoliceAndThief.thiefplayer02currentTarget = null;
                },
                () => { return PoliceAndThief.thiefplayer02 != null && PoliceAndThief.thiefplayer02 == PlayerControl.LocalPlayer && PoliceAndThief.thiefTeamCanKill; },
                () => { return PoliceAndThief.thiefplayer02currentTarget && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer02IsReviving && !PlayerControl.LocalPlayer.Data.IsDead && !PoliceAndThief.thiefplayer02IsStealing; },
                () => { thiefplayer02KillButton.Timer = thiefplayer02KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Thiefplayer02 FreeThief Button
            thiefplayer02FreeThiefButton = new CustomButton(
                () => {
                    MessageWriter thiefFree = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefFreeThief, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(thiefFree);
                    RPCProcedure.policeandThiefFreeThief();
                    thiefplayer02FreeThiefButton.Timer = thiefplayer02FreeThiefButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer02 != null && PoliceAndThief.thiefplayer02 == PlayerControl.LocalPlayer; },
                () => {
                    bool CanUse = false;
                    if (PoliceAndThief.currentThiefsCaptured > 0) {
                        if (Vector2.Distance(PoliceAndThief.thiefplayer02.transform.position, PoliceAndThief.cellbutton.transform.position) < 0.4f && !PoliceAndThief.thiefplayer02.Data.IsDead) {
                            CanUse = true;
                        }
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer02IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer02FreeThiefButton.Timer = thiefplayer02FreeThiefButton.MaxTimer; },
                PoliceAndThief.getFreeThiefButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer02 Take/Deliver Jewel Button
            thiefplayer02TakeDeliverJewelButton = new CustomButton(
                () => {
                    if (PoliceAndThief.thiefplayer02IsStealing) {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer02JewelId;
                        MessageWriter thiefScore = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefDeliverJewel, Hazel.SendOption.Reliable, -1);
                        thiefScore.Write(targetId);
                        thiefScore.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefScore);
                        RPCProcedure.policeandThiefDeliverJewel(targetId, jewelId);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer02JewelId;
                        MessageWriter thiefWhoTookATreasure = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefTakeJewel, Hazel.SendOption.Reliable, -1);
                        thiefWhoTookATreasure.Write(targetId);
                        thiefWhoTookATreasure.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefWhoTookATreasure);
                        RPCProcedure.policeandThiefTakeJewel(targetId, jewelId);
                    }
                    thiefplayer02TakeDeliverJewelButton.Timer = thiefplayer02TakeDeliverJewelButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer02 != null && PoliceAndThief.thiefplayer02 == PlayerControl.LocalPlayer; },
                () => {
                    if (PoliceAndThief.thiefplayer02IsStealing)
                        thiefplayer02TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getDeliverJewelButtonSprite();
                    else
                        thiefplayer02TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getTakeJewelButtonSprite();
                    bool CanUse = false;
                    if (PoliceAndThief.thiefTreasures.Count != 0) {
                        foreach (GameObject jewel in PoliceAndThief.thiefTreasures) {
                            if (jewel != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, jewel.transform.position) < 0.5f && !PoliceAndThief.thiefplayer02IsStealing) {
                                //CanUse = true;
                                switch (jewel.name) {
                                    case "jewel01":
                                        PoliceAndThief.thiefplayer02JewelId = 1;
                                        CanUse = !PoliceAndThief.jewel01BeingStealed;
                                        break;
                                    case "jewel02":
                                        PoliceAndThief.thiefplayer02JewelId = 2;
                                        CanUse = !PoliceAndThief.jewel02BeingStealed;
                                        break;
                                    case "jewel03":
                                        PoliceAndThief.thiefplayer02JewelId = 3;
                                        CanUse = !PoliceAndThief.jewel03BeingStealed;
                                        break;
                                    case "jewel04":
                                        PoliceAndThief.thiefplayer02JewelId = 4;
                                        CanUse = !PoliceAndThief.jewel04BeingStealed;
                                        break;
                                    case "jewel05":
                                        PoliceAndThief.thiefplayer02JewelId = 5;
                                        CanUse = !PoliceAndThief.jewel05BeingStealed;
                                        break;
                                    case "jewel06":
                                        PoliceAndThief.thiefplayer02JewelId = 6;
                                        CanUse = !PoliceAndThief.jewel06BeingStealed;
                                        break;
                                    case "jewel07":
                                        PoliceAndThief.thiefplayer02JewelId = 7;
                                        CanUse = !PoliceAndThief.jewel07BeingStealed;
                                        break;
                                    case "jewel08":
                                        PoliceAndThief.thiefplayer02JewelId = 8;
                                        CanUse = !PoliceAndThief.jewel08BeingStealed;
                                        break;
                                    case "jewel09":
                                        PoliceAndThief.thiefplayer02JewelId = 9;
                                        CanUse = !PoliceAndThief.jewel09BeingStealed;
                                        break;
                                    case "jewel10":
                                        PoliceAndThief.thiefplayer02JewelId = 10;
                                        CanUse = !PoliceAndThief.jewel10BeingStealed;
                                        break;
                                    case "jewel11":
                                        PoliceAndThief.thiefplayer02JewelId = 11;
                                        CanUse = !PoliceAndThief.jewel11BeingStealed;
                                        break;
                                    case "jewel12":
                                        PoliceAndThief.thiefplayer02JewelId = 12;
                                        CanUse = !PoliceAndThief.jewel12BeingStealed;
                                        break;
                                    case "jewel13":
                                        PoliceAndThief.thiefplayer02JewelId = 13;
                                        CanUse = !PoliceAndThief.jewel13BeingStealed;
                                        break;
                                    case "jewel14":
                                        PoliceAndThief.thiefplayer02JewelId = 14;
                                        CanUse = !PoliceAndThief.jewel14BeingStealed;
                                        break;
                                    case "jewel15":
                                        PoliceAndThief.thiefplayer02JewelId = 15;
                                        CanUse = !PoliceAndThief.jewel15BeingStealed;
                                        break;
                                }
                            }
                            else if (PoliceAndThief.jewelbutton != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.jewelbutton.transform.position) < 0.5f && PoliceAndThief.thiefplayer02IsStealing) {
                                CanUse = true;
                            }
                        }

                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer02IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer02TakeDeliverJewelButton.Timer = thiefplayer02TakeDeliverJewelButton.MaxTimer; },
                PoliceAndThief.getTakeJewelButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer03 Kill
            thiefplayer03KillButton = new CustomButton(
                () => {
                    byte targetId = PoliceAndThief.thiefplayer03currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(9);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.policeandThiefKills(targetId, 9);
                    thiefplayer03KillButton.Timer = thiefplayer03KillButton.MaxTimer;
                    PoliceAndThief.thiefplayer03currentTarget = null;
                },
                () => { return PoliceAndThief.thiefplayer03 != null && PoliceAndThief.thiefplayer03 == PlayerControl.LocalPlayer && PoliceAndThief.thiefTeamCanKill; },
                () => { return PoliceAndThief.thiefplayer03currentTarget && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer03IsReviving && !PlayerControl.LocalPlayer.Data.IsDead && !PoliceAndThief.thiefplayer03IsStealing; },
                () => { thiefplayer03KillButton.Timer = thiefplayer03KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Thiefplayer03 FreeThief Button
            thiefplayer03FreeThiefButton = new CustomButton(
                () => {
                    MessageWriter thiefFree = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefFreeThief, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(thiefFree);
                    RPCProcedure.policeandThiefFreeThief();
                    thiefplayer03FreeThiefButton.Timer = thiefplayer03FreeThiefButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer03 != null && PoliceAndThief.thiefplayer03 == PlayerControl.LocalPlayer; },
                () => {
                    bool CanUse = false;
                    if (PoliceAndThief.currentThiefsCaptured > 0) {
                        if (Vector2.Distance(PoliceAndThief.thiefplayer03.transform.position, PoliceAndThief.cellbutton.transform.position) < 0.4f && !PoliceAndThief.thiefplayer03.Data.IsDead) {
                            CanUse = true;
                        }
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer03IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer03FreeThiefButton.Timer = thiefplayer03FreeThiefButton.MaxTimer; },
                PoliceAndThief.getFreeThiefButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer03 Take/Deliver Jewel Button
            thiefplayer03TakeDeliverJewelButton = new CustomButton(
                () => {
                    if (PoliceAndThief.thiefplayer03IsStealing) {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer03JewelId;
                        MessageWriter thiefScore = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefDeliverJewel, Hazel.SendOption.Reliable, -1);
                        thiefScore.Write(targetId);
                        thiefScore.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefScore);
                        RPCProcedure.policeandThiefDeliverJewel(targetId, jewelId);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer03JewelId;
                        MessageWriter thiefWhoTookATreasure = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefTakeJewel, Hazel.SendOption.Reliable, -1);
                        thiefWhoTookATreasure.Write(targetId);
                        thiefWhoTookATreasure.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefWhoTookATreasure);
                        RPCProcedure.policeandThiefTakeJewel(targetId, jewelId);
                    }
                    thiefplayer03TakeDeliverJewelButton.Timer = thiefplayer03TakeDeliverJewelButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer03 != null && PoliceAndThief.thiefplayer03 == PlayerControl.LocalPlayer; },
                () => {
                    if (PoliceAndThief.thiefplayer03IsStealing)
                        thiefplayer03TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getDeliverJewelButtonSprite();
                    else
                        thiefplayer03TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getTakeJewelButtonSprite();
                    bool CanUse = false;
                    if (PoliceAndThief.thiefTreasures.Count != 0) {
                        foreach (GameObject jewel in PoliceAndThief.thiefTreasures) {
                            if (jewel != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, jewel.transform.position) < 0.5f && !PoliceAndThief.thiefplayer03IsStealing) {
                                //CanUse = true;
                                switch (jewel.name) {
                                    case "jewel01":
                                        PoliceAndThief.thiefplayer03JewelId = 1;
                                        CanUse = !PoliceAndThief.jewel01BeingStealed;
                                        break;
                                    case "jewel02":
                                        PoliceAndThief.thiefplayer03JewelId = 2;
                                        CanUse = !PoliceAndThief.jewel02BeingStealed;
                                        break;
                                    case "jewel03":
                                        PoliceAndThief.thiefplayer03JewelId = 3;
                                        CanUse = !PoliceAndThief.jewel03BeingStealed;
                                        break;
                                    case "jewel04":
                                        PoliceAndThief.thiefplayer03JewelId = 4;
                                        CanUse = !PoliceAndThief.jewel04BeingStealed;
                                        break;
                                    case "jewel05":
                                        PoliceAndThief.thiefplayer03JewelId = 5;
                                        CanUse = !PoliceAndThief.jewel05BeingStealed;
                                        break;
                                    case "jewel06":
                                        PoliceAndThief.thiefplayer03JewelId = 6;
                                        CanUse = !PoliceAndThief.jewel06BeingStealed;
                                        break;
                                    case "jewel07":
                                        PoliceAndThief.thiefplayer03JewelId = 7;
                                        CanUse = !PoliceAndThief.jewel07BeingStealed;
                                        break;
                                    case "jewel08":
                                        PoliceAndThief.thiefplayer03JewelId = 8;
                                        CanUse = !PoliceAndThief.jewel08BeingStealed;
                                        break;
                                    case "jewel09":
                                        PoliceAndThief.thiefplayer03JewelId = 9;
                                        CanUse = !PoliceAndThief.jewel09BeingStealed;
                                        break;
                                    case "jewel10":
                                        PoliceAndThief.thiefplayer03JewelId = 10;
                                        CanUse = !PoliceAndThief.jewel10BeingStealed;
                                        break;
                                    case "jewel11":
                                        PoliceAndThief.thiefplayer03JewelId = 11;
                                        CanUse = !PoliceAndThief.jewel11BeingStealed;
                                        break;
                                    case "jewel12":
                                        PoliceAndThief.thiefplayer03JewelId = 12;
                                        CanUse = !PoliceAndThief.jewel12BeingStealed;
                                        break;
                                    case "jewel13":
                                        PoliceAndThief.thiefplayer03JewelId = 13;
                                        CanUse = !PoliceAndThief.jewel13BeingStealed;
                                        break;
                                    case "jewel14":
                                        PoliceAndThief.thiefplayer03JewelId = 14;
                                        CanUse = !PoliceAndThief.jewel14BeingStealed;
                                        break;
                                    case "jewel15":
                                        PoliceAndThief.thiefplayer03JewelId = 15;
                                        CanUse = !PoliceAndThief.jewel15BeingStealed;
                                        break;
                                }
                            }
                            else if (PoliceAndThief.jewelbutton != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.jewelbutton.transform.position) < 0.5f && PoliceAndThief.thiefplayer03IsStealing) {
                                CanUse = true;
                            }
                        }

                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer03IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer03TakeDeliverJewelButton.Timer = thiefplayer03TakeDeliverJewelButton.MaxTimer; },
                PoliceAndThief.getTakeJewelButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer04 Kill
            thiefplayer04KillButton = new CustomButton(
                () => {
                    byte targetId = PoliceAndThief.thiefplayer04currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(10);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.policeandThiefKills(targetId, 10);
                    thiefplayer04KillButton.Timer = thiefplayer04KillButton.MaxTimer;
                    PoliceAndThief.thiefplayer04currentTarget = null;
                },
                () => { return PoliceAndThief.thiefplayer04 != null && PoliceAndThief.thiefplayer04 == PlayerControl.LocalPlayer && PoliceAndThief.thiefTeamCanKill; },
                () => { return PoliceAndThief.thiefplayer04currentTarget && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer04IsReviving && !PlayerControl.LocalPlayer.Data.IsDead && !PoliceAndThief.thiefplayer04IsStealing; },
                () => { thiefplayer04KillButton.Timer = thiefplayer04KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Thiefplayer04 FreeThief Button
            thiefplayer04FreeThiefButton = new CustomButton(
                () => {
                    MessageWriter thiefFree = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefFreeThief, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(thiefFree);
                    RPCProcedure.policeandThiefFreeThief();
                    thiefplayer04FreeThiefButton.Timer = thiefplayer04FreeThiefButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer04 != null && PoliceAndThief.thiefplayer04 == PlayerControl.LocalPlayer; },
                () => {
                    bool CanUse = false;
                    if (PoliceAndThief.currentThiefsCaptured > 0) {
                        if (Vector2.Distance(PoliceAndThief.thiefplayer04.transform.position, PoliceAndThief.cellbutton.transform.position) < 0.4f && !PoliceAndThief.thiefplayer04.Data.IsDead) {
                            CanUse = true;
                        }
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer04IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer04FreeThiefButton.Timer = thiefplayer04FreeThiefButton.MaxTimer; },
                PoliceAndThief.getFreeThiefButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer04 Take/Deliver Jewel Button
            thiefplayer04TakeDeliverJewelButton = new CustomButton(
                () => {
                    if (PoliceAndThief.thiefplayer04IsStealing) {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer04JewelId;
                        MessageWriter thiefScore = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefDeliverJewel, Hazel.SendOption.Reliable, -1);
                        thiefScore.Write(targetId);
                        thiefScore.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefScore);
                        RPCProcedure.policeandThiefDeliverJewel(targetId, jewelId);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer04JewelId;
                        MessageWriter thiefWhoTookATreasure = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefTakeJewel, Hazel.SendOption.Reliable, -1);
                        thiefWhoTookATreasure.Write(targetId);
                        thiefWhoTookATreasure.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefWhoTookATreasure);
                        RPCProcedure.policeandThiefTakeJewel(targetId, jewelId);
                    }
                    thiefplayer04TakeDeliverJewelButton.Timer = thiefplayer04TakeDeliverJewelButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer04 != null && PoliceAndThief.thiefplayer04 == PlayerControl.LocalPlayer; },
                () => {
                    if (PoliceAndThief.thiefplayer04IsStealing)
                        thiefplayer04TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getDeliverJewelButtonSprite();
                    else
                        thiefplayer04TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getTakeJewelButtonSprite();
                    bool CanUse = false;
                    if (PoliceAndThief.thiefTreasures.Count != 0) {
                        foreach (GameObject jewel in PoliceAndThief.thiefTreasures) {
                            if (jewel != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, jewel.transform.position) < 0.5f && !PoliceAndThief.thiefplayer04IsStealing) {
                                //CanUse = true;
                                switch (jewel.name) {
                                    case "jewel01":
                                        PoliceAndThief.thiefplayer04JewelId = 1;
                                        CanUse = !PoliceAndThief.jewel01BeingStealed;
                                        break;
                                    case "jewel02":
                                        PoliceAndThief.thiefplayer04JewelId = 2;
                                        CanUse = !PoliceAndThief.jewel02BeingStealed;
                                        break;
                                    case "jewel03":
                                        PoliceAndThief.thiefplayer04JewelId = 3;
                                        CanUse = !PoliceAndThief.jewel03BeingStealed;
                                        break;
                                    case "jewel04":
                                        PoliceAndThief.thiefplayer04JewelId = 4;
                                        CanUse = !PoliceAndThief.jewel04BeingStealed;
                                        break;
                                    case "jewel05":
                                        PoliceAndThief.thiefplayer04JewelId = 5;
                                        CanUse = !PoliceAndThief.jewel05BeingStealed;
                                        break;
                                    case "jewel06":
                                        PoliceAndThief.thiefplayer04JewelId = 6;
                                        CanUse = !PoliceAndThief.jewel06BeingStealed;
                                        break;
                                    case "jewel07":
                                        PoliceAndThief.thiefplayer04JewelId = 7;
                                        CanUse = !PoliceAndThief.jewel07BeingStealed;
                                        break;
                                    case "jewel08":
                                        PoliceAndThief.thiefplayer04JewelId = 8;
                                        CanUse = !PoliceAndThief.jewel08BeingStealed;
                                        break;
                                    case "jewel09":
                                        PoliceAndThief.thiefplayer04JewelId = 9;
                                        CanUse = !PoliceAndThief.jewel09BeingStealed;
                                        break;
                                    case "jewel10":
                                        PoliceAndThief.thiefplayer04JewelId = 10;
                                        CanUse = !PoliceAndThief.jewel10BeingStealed;
                                        break;
                                    case "jewel11":
                                        PoliceAndThief.thiefplayer04JewelId = 11;
                                        CanUse = !PoliceAndThief.jewel11BeingStealed;
                                        break;
                                    case "jewel12":
                                        PoliceAndThief.thiefplayer04JewelId = 12;
                                        CanUse = !PoliceAndThief.jewel12BeingStealed;
                                        break;
                                    case "jewel13":
                                        PoliceAndThief.thiefplayer04JewelId = 13;
                                        CanUse = !PoliceAndThief.jewel13BeingStealed;
                                        break;
                                    case "jewel14":
                                        PoliceAndThief.thiefplayer04JewelId = 14;
                                        CanUse = !PoliceAndThief.jewel14BeingStealed;
                                        break;
                                    case "jewel15":
                                        PoliceAndThief.thiefplayer04JewelId = 15;
                                        CanUse = !PoliceAndThief.jewel15BeingStealed;
                                        break;
                                }
                            }
                            else if (PoliceAndThief.jewelbutton != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.jewelbutton.transform.position) < 0.5f && PoliceAndThief.thiefplayer04IsStealing) {
                                CanUse = true;
                            }
                        }

                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer04IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer04TakeDeliverJewelButton.Timer = thiefplayer04TakeDeliverJewelButton.MaxTimer; },
                PoliceAndThief.getTakeJewelButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer05 Kill
            thiefplayer05KillButton = new CustomButton(
                () => {
                    byte targetId = PoliceAndThief.thiefplayer05currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(11);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.policeandThiefKills(targetId, 11);
                    thiefplayer05KillButton.Timer = thiefplayer05KillButton.MaxTimer;
                    PoliceAndThief.thiefplayer05currentTarget = null;
                },
                () => { return PoliceAndThief.thiefplayer05 != null && PoliceAndThief.thiefplayer05 == PlayerControl.LocalPlayer && PoliceAndThief.thiefTeamCanKill; },
                () => { return PoliceAndThief.thiefplayer05currentTarget && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer05IsReviving && !PlayerControl.LocalPlayer.Data.IsDead && !PoliceAndThief.thiefplayer05IsStealing; },
                () => { thiefplayer05KillButton.Timer = thiefplayer05KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Thiefplayer05 FreeThief Button
            thiefplayer05FreeThiefButton = new CustomButton(
                () => {
                    MessageWriter thiefFree = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefFreeThief, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(thiefFree);
                    RPCProcedure.policeandThiefFreeThief();
                    thiefplayer05FreeThiefButton.Timer = thiefplayer05FreeThiefButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer05 != null && PoliceAndThief.thiefplayer05 == PlayerControl.LocalPlayer; },
                () => {
                    bool CanUse = false;
                    if (PoliceAndThief.currentThiefsCaptured > 0) {
                        if (Vector2.Distance(PoliceAndThief.thiefplayer05.transform.position, PoliceAndThief.cellbutton.transform.position) < 0.4f && !PoliceAndThief.thiefplayer05.Data.IsDead) {
                            CanUse = true;
                        }
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer05IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer05FreeThiefButton.Timer = thiefplayer05FreeThiefButton.MaxTimer; },
                PoliceAndThief.getFreeThiefButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer05 Take/Deliver Jewel Button
            thiefplayer05TakeDeliverJewelButton = new CustomButton(
                () => {
                    if (PoliceAndThief.thiefplayer05IsStealing) {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer05JewelId;
                        MessageWriter thiefScore = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefDeliverJewel, Hazel.SendOption.Reliable, -1);
                        thiefScore.Write(targetId);
                        thiefScore.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefScore);
                        RPCProcedure.policeandThiefDeliverJewel(targetId, jewelId);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer05JewelId;
                        MessageWriter thiefWhoTookATreasure = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefTakeJewel, Hazel.SendOption.Reliable, -1);
                        thiefWhoTookATreasure.Write(targetId);
                        thiefWhoTookATreasure.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefWhoTookATreasure);
                        RPCProcedure.policeandThiefTakeJewel(targetId, jewelId);
                    }
                    thiefplayer05TakeDeliverJewelButton.Timer = thiefplayer05TakeDeliverJewelButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer05 != null && PoliceAndThief.thiefplayer05 == PlayerControl.LocalPlayer; },
                () => {
                    if (PoliceAndThief.thiefplayer05IsStealing)
                        thiefplayer05TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getDeliverJewelButtonSprite();
                    else
                        thiefplayer05TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getTakeJewelButtonSprite();
                    bool CanUse = false;
                    if (PoliceAndThief.thiefTreasures.Count != 0) {
                        foreach (GameObject jewel in PoliceAndThief.thiefTreasures) {
                            if (jewel != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, jewel.transform.position) < 0.5f && !PoliceAndThief.thiefplayer05IsStealing) {
                                //CanUse = true;
                                switch (jewel.name) {
                                    case "jewel01":
                                        PoliceAndThief.thiefplayer05JewelId = 1;
                                        CanUse = !PoliceAndThief.jewel01BeingStealed;
                                        break;
                                    case "jewel02":
                                        PoliceAndThief.thiefplayer05JewelId = 2;
                                        CanUse = !PoliceAndThief.jewel02BeingStealed;
                                        break;
                                    case "jewel03":
                                        PoliceAndThief.thiefplayer05JewelId = 3;
                                        CanUse = !PoliceAndThief.jewel03BeingStealed;
                                        break;
                                    case "jewel04":
                                        PoliceAndThief.thiefplayer05JewelId = 4;
                                        CanUse = !PoliceAndThief.jewel04BeingStealed;
                                        break;
                                    case "jewel05":
                                        PoliceAndThief.thiefplayer05JewelId = 5;
                                        CanUse = !PoliceAndThief.jewel05BeingStealed;
                                        break;
                                    case "jewel06":
                                        PoliceAndThief.thiefplayer05JewelId = 6;
                                        CanUse = !PoliceAndThief.jewel06BeingStealed;
                                        break;
                                    case "jewel07":
                                        PoliceAndThief.thiefplayer05JewelId = 7;
                                        CanUse = !PoliceAndThief.jewel07BeingStealed;
                                        break;
                                    case "jewel08":
                                        PoliceAndThief.thiefplayer05JewelId = 8;
                                        CanUse = !PoliceAndThief.jewel08BeingStealed;
                                        break;
                                    case "jewel09":
                                        PoliceAndThief.thiefplayer05JewelId = 9;
                                        CanUse = !PoliceAndThief.jewel09BeingStealed;
                                        break;
                                    case "jewel10":
                                        PoliceAndThief.thiefplayer05JewelId = 10;
                                        CanUse = !PoliceAndThief.jewel10BeingStealed;
                                        break;
                                    case "jewel11":
                                        PoliceAndThief.thiefplayer05JewelId = 11;
                                        CanUse = !PoliceAndThief.jewel11BeingStealed;
                                        break;
                                    case "jewel12":
                                        PoliceAndThief.thiefplayer05JewelId = 12;
                                        CanUse = !PoliceAndThief.jewel12BeingStealed;
                                        break;
                                    case "jewel13":
                                        PoliceAndThief.thiefplayer05JewelId = 13;
                                        CanUse = !PoliceAndThief.jewel13BeingStealed;
                                        break;
                                    case "jewel14":
                                        PoliceAndThief.thiefplayer05JewelId = 14;
                                        CanUse = !PoliceAndThief.jewel14BeingStealed;
                                        break;
                                    case "jewel15":
                                        PoliceAndThief.thiefplayer05JewelId = 15;
                                        CanUse = !PoliceAndThief.jewel15BeingStealed;
                                        break;
                                }
                            }
                            else if (PoliceAndThief.jewelbutton != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.jewelbutton.transform.position) < 0.5f && PoliceAndThief.thiefplayer05IsStealing) {
                                CanUse = true;
                            }
                        }

                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer05IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer05TakeDeliverJewelButton.Timer = thiefplayer05TakeDeliverJewelButton.MaxTimer; },
                PoliceAndThief.getTakeJewelButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer06 Kill
            thiefplayer06KillButton = new CustomButton(
                () => {
                    byte targetId = PoliceAndThief.thiefplayer06currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(12);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.policeandThiefKills(targetId, 12);
                    thiefplayer06KillButton.Timer = thiefplayer06KillButton.MaxTimer;
                    PoliceAndThief.thiefplayer06currentTarget = null;
                },
                () => { return PoliceAndThief.thiefplayer06 != null && PoliceAndThief.thiefplayer06 == PlayerControl.LocalPlayer && PoliceAndThief.thiefTeamCanKill; },
                () => { return PoliceAndThief.thiefplayer06currentTarget && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer06IsReviving && !PlayerControl.LocalPlayer.Data.IsDead && !PoliceAndThief.thiefplayer06IsStealing; },
                () => { thiefplayer06KillButton.Timer = thiefplayer06KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Thiefplayer06 FreeThief Button
            thiefplayer06FreeThiefButton = new CustomButton(
                () => {
                    MessageWriter thiefFree = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefFreeThief, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(thiefFree);
                    RPCProcedure.policeandThiefFreeThief();
                    thiefplayer06FreeThiefButton.Timer = thiefplayer06FreeThiefButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer06 != null && PoliceAndThief.thiefplayer06 == PlayerControl.LocalPlayer; },
                () => {
                    bool CanUse = false;
                    if (PoliceAndThief.currentThiefsCaptured > 0) {
                        if (Vector2.Distance(PoliceAndThief.thiefplayer06.transform.position, PoliceAndThief.cellbutton.transform.position) < 0.4f && !PoliceAndThief.thiefplayer06.Data.IsDead) {
                            CanUse = true;
                        }
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer06IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer06FreeThiefButton.Timer = thiefplayer06FreeThiefButton.MaxTimer; },
                PoliceAndThief.getFreeThiefButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer06 Take/Deliver Jewel Button
            thiefplayer06TakeDeliverJewelButton = new CustomButton(
                () => {
                    if (PoliceAndThief.thiefplayer06IsStealing) {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer06JewelId;
                        MessageWriter thiefScore = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefDeliverJewel, Hazel.SendOption.Reliable, -1);
                        thiefScore.Write(targetId);
                        thiefScore.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefScore);
                        RPCProcedure.policeandThiefDeliverJewel(targetId, jewelId);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer06JewelId;
                        MessageWriter thiefWhoTookATreasure = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefTakeJewel, Hazel.SendOption.Reliable, -1);
                        thiefWhoTookATreasure.Write(targetId);
                        thiefWhoTookATreasure.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefWhoTookATreasure);
                        RPCProcedure.policeandThiefTakeJewel(targetId, jewelId);
                    }
                    thiefplayer06TakeDeliverJewelButton.Timer = thiefplayer06TakeDeliverJewelButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer06 != null && PoliceAndThief.thiefplayer06 == PlayerControl.LocalPlayer; },
                () => {
                    if (PoliceAndThief.thiefplayer06IsStealing)
                        thiefplayer06TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getDeliverJewelButtonSprite();
                    else
                        thiefplayer06TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getTakeJewelButtonSprite();
                    bool CanUse = false;
                    if (PoliceAndThief.thiefTreasures.Count != 0) {
                        foreach (GameObject jewel in PoliceAndThief.thiefTreasures) {
                            if (jewel != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, jewel.transform.position) < 0.5f && !PoliceAndThief.thiefplayer06IsStealing) {
                                //CanUse = true;
                                switch (jewel.name) {
                                    case "jewel01":
                                        PoliceAndThief.thiefplayer06JewelId = 1;
                                        CanUse = !PoliceAndThief.jewel01BeingStealed;
                                        break;
                                    case "jewel02":
                                        PoliceAndThief.thiefplayer06JewelId = 2;
                                        CanUse = !PoliceAndThief.jewel02BeingStealed;
                                        break;
                                    case "jewel03":
                                        PoliceAndThief.thiefplayer06JewelId = 3;
                                        CanUse = !PoliceAndThief.jewel03BeingStealed;
                                        break;
                                    case "jewel04":
                                        PoliceAndThief.thiefplayer06JewelId = 4;
                                        CanUse = !PoliceAndThief.jewel04BeingStealed;
                                        break;
                                    case "jewel05":
                                        PoliceAndThief.thiefplayer06JewelId = 5;
                                        CanUse = !PoliceAndThief.jewel05BeingStealed;
                                        break;
                                    case "jewel06":
                                        PoliceAndThief.thiefplayer06JewelId = 6;
                                        CanUse = !PoliceAndThief.jewel06BeingStealed;
                                        break;
                                    case "jewel07":
                                        PoliceAndThief.thiefplayer06JewelId = 7;
                                        CanUse = !PoliceAndThief.jewel07BeingStealed;
                                        break;
                                    case "jewel08":
                                        PoliceAndThief.thiefplayer06JewelId = 8;
                                        CanUse = !PoliceAndThief.jewel08BeingStealed;
                                        break;
                                    case "jewel09":
                                        PoliceAndThief.thiefplayer06JewelId = 9;
                                        CanUse = !PoliceAndThief.jewel09BeingStealed;
                                        break;
                                    case "jewel10":
                                        PoliceAndThief.thiefplayer06JewelId = 10;
                                        CanUse = !PoliceAndThief.jewel10BeingStealed;
                                        break;
                                    case "jewel11":
                                        PoliceAndThief.thiefplayer06JewelId = 11;
                                        CanUse = !PoliceAndThief.jewel11BeingStealed;
                                        break;
                                    case "jewel12":
                                        PoliceAndThief.thiefplayer06JewelId = 12;
                                        CanUse = !PoliceAndThief.jewel12BeingStealed;
                                        break;
                                    case "jewel13":
                                        PoliceAndThief.thiefplayer06JewelId = 13;
                                        CanUse = !PoliceAndThief.jewel13BeingStealed;
                                        break;
                                    case "jewel14":
                                        PoliceAndThief.thiefplayer06JewelId = 14;
                                        CanUse = !PoliceAndThief.jewel14BeingStealed;
                                        break;
                                    case "jewel15":
                                        PoliceAndThief.thiefplayer06JewelId = 15;
                                        CanUse = !PoliceAndThief.jewel15BeingStealed;
                                        break;
                                }
                            }
                            else if (PoliceAndThief.jewelbutton != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.jewelbutton.transform.position) < 0.5f && PoliceAndThief.thiefplayer06IsStealing) {
                                CanUse = true;
                            }
                        }

                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer06IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer06TakeDeliverJewelButton.Timer = thiefplayer06TakeDeliverJewelButton.MaxTimer; },
                PoliceAndThief.getTakeJewelButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer07 Kill
            thiefplayer07KillButton = new CustomButton(
                () => {
                    byte targetId = PoliceAndThief.thiefplayer07currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(13);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.policeandThiefKills(targetId, 13);
                    thiefplayer07KillButton.Timer = thiefplayer07KillButton.MaxTimer;
                    PoliceAndThief.thiefplayer07currentTarget = null;
                },
                () => { return PoliceAndThief.thiefplayer07 != null && PoliceAndThief.thiefplayer07 == PlayerControl.LocalPlayer && PoliceAndThief.thiefTeamCanKill; },
                () => { return PoliceAndThief.thiefplayer07currentTarget && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer07IsReviving && !PlayerControl.LocalPlayer.Data.IsDead && !PoliceAndThief.thiefplayer07IsStealing; },
                () => { thiefplayer07KillButton.Timer = thiefplayer07KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Thiefplayer07 FreeThief Button
            thiefplayer07FreeThiefButton = new CustomButton(
                () => {
                    MessageWriter thiefFree = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefFreeThief, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(thiefFree);
                    RPCProcedure.policeandThiefFreeThief();
                    thiefplayer07FreeThiefButton.Timer = thiefplayer07FreeThiefButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer07 != null && PoliceAndThief.thiefplayer07 == PlayerControl.LocalPlayer; },
                () => {
                    bool CanUse = false;
                    if (PoliceAndThief.currentThiefsCaptured > 0) {
                        if (Vector2.Distance(PoliceAndThief.thiefplayer07.transform.position, PoliceAndThief.cellbutton.transform.position) < 0.4f && !PoliceAndThief.thiefplayer07.Data.IsDead) {
                            CanUse = true;
                        }
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer07IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer07FreeThiefButton.Timer = thiefplayer07FreeThiefButton.MaxTimer; },
                PoliceAndThief.getFreeThiefButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer07 Take/Deliver Jewel Button
            thiefplayer07TakeDeliverJewelButton = new CustomButton(
                () => {
                    if (PoliceAndThief.thiefplayer07IsStealing) {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer07JewelId;
                        MessageWriter thiefScore = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefDeliverJewel, Hazel.SendOption.Reliable, -1);
                        thiefScore.Write(targetId);
                        thiefScore.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefScore);
                        RPCProcedure.policeandThiefDeliverJewel(targetId, jewelId);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer07JewelId;
                        MessageWriter thiefWhoTookATreasure = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefTakeJewel, Hazel.SendOption.Reliable, -1);
                        thiefWhoTookATreasure.Write(targetId);
                        thiefWhoTookATreasure.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefWhoTookATreasure);
                        RPCProcedure.policeandThiefTakeJewel(targetId, jewelId);
                    }
                    thiefplayer07TakeDeliverJewelButton.Timer = thiefplayer07TakeDeliverJewelButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer07 != null && PoliceAndThief.thiefplayer07 == PlayerControl.LocalPlayer; },
                () => {
                    if (PoliceAndThief.thiefplayer07IsStealing)
                        thiefplayer07TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getDeliverJewelButtonSprite();
                    else
                        thiefplayer07TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getTakeJewelButtonSprite();
                    bool CanUse = false;
                    if (PoliceAndThief.thiefTreasures.Count != 0) {
                        foreach (GameObject jewel in PoliceAndThief.thiefTreasures) {
                            if (jewel != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, jewel.transform.position) < 0.5f && !PoliceAndThief.thiefplayer07IsStealing) {
                                //CanUse = true;
                                switch (jewel.name) {
                                    case "jewel01":
                                        PoliceAndThief.thiefplayer07JewelId = 1;
                                        CanUse = !PoliceAndThief.jewel01BeingStealed;
                                        break;
                                    case "jewel02":
                                        PoliceAndThief.thiefplayer07JewelId = 2;
                                        CanUse = !PoliceAndThief.jewel02BeingStealed;
                                        break;
                                    case "jewel03":
                                        PoliceAndThief.thiefplayer07JewelId = 3;
                                        CanUse = !PoliceAndThief.jewel03BeingStealed;
                                        break;
                                    case "jewel04":
                                        PoliceAndThief.thiefplayer07JewelId = 4;
                                        CanUse = !PoliceAndThief.jewel04BeingStealed;
                                        break;
                                    case "jewel05":
                                        PoliceAndThief.thiefplayer07JewelId = 5;
                                        CanUse = !PoliceAndThief.jewel05BeingStealed;
                                        break;
                                    case "jewel06":
                                        PoliceAndThief.thiefplayer07JewelId = 6;
                                        CanUse = !PoliceAndThief.jewel06BeingStealed;
                                        break;
                                    case "jewel07":
                                        PoliceAndThief.thiefplayer07JewelId = 7;
                                        CanUse = !PoliceAndThief.jewel07BeingStealed;
                                        break;
                                    case "jewel08":
                                        PoliceAndThief.thiefplayer07JewelId = 8;
                                        CanUse = !PoliceAndThief.jewel08BeingStealed;
                                        break;
                                    case "jewel09":
                                        PoliceAndThief.thiefplayer07JewelId = 9;
                                        CanUse = !PoliceAndThief.jewel09BeingStealed;
                                        break;
                                    case "jewel10":
                                        PoliceAndThief.thiefplayer07JewelId = 10;
                                        CanUse = !PoliceAndThief.jewel10BeingStealed;
                                        break;
                                    case "jewel11":
                                        PoliceAndThief.thiefplayer07JewelId = 11;
                                        CanUse = !PoliceAndThief.jewel11BeingStealed;
                                        break;
                                    case "jewel12":
                                        PoliceAndThief.thiefplayer07JewelId = 12;
                                        CanUse = !PoliceAndThief.jewel12BeingStealed;
                                        break;
                                    case "jewel13":
                                        PoliceAndThief.thiefplayer07JewelId = 13;
                                        CanUse = !PoliceAndThief.jewel13BeingStealed;
                                        break;
                                    case "jewel14":
                                        PoliceAndThief.thiefplayer07JewelId = 14;
                                        CanUse = !PoliceAndThief.jewel14BeingStealed;
                                        break;
                                    case "jewel15":
                                        PoliceAndThief.thiefplayer07JewelId = 15;
                                        CanUse = !PoliceAndThief.jewel15BeingStealed;
                                        break;
                                }
                            }
                            else if (PoliceAndThief.jewelbutton != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.jewelbutton.transform.position) < 0.5f && PoliceAndThief.thiefplayer07IsStealing) {
                                CanUse = true;
                            }
                        }

                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer07IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer07TakeDeliverJewelButton.Timer = thiefplayer07TakeDeliverJewelButton.MaxTimer; },
                PoliceAndThief.getTakeJewelButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer08 Kill
            thiefplayer08KillButton = new CustomButton(
                () => {
                    byte targetId = PoliceAndThief.thiefplayer08currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(14);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.policeandThiefKills(targetId, 14);
                    thiefplayer08KillButton.Timer = thiefplayer08KillButton.MaxTimer;
                    PoliceAndThief.thiefplayer08currentTarget = null;
                },
                () => { return PoliceAndThief.thiefplayer08 != null && PoliceAndThief.thiefplayer08 == PlayerControl.LocalPlayer && PoliceAndThief.thiefTeamCanKill; },
                () => { return PoliceAndThief.thiefplayer08currentTarget && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer08IsReviving && !PlayerControl.LocalPlayer.Data.IsDead && !PoliceAndThief.thiefplayer08IsStealing; },
                () => { thiefplayer08KillButton.Timer = thiefplayer08KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Thiefplayer08 FreeThief Button
            thiefplayer08FreeThiefButton = new CustomButton(
                () => {
                    MessageWriter thiefFree = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefFreeThief, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(thiefFree);
                    RPCProcedure.policeandThiefFreeThief();
                    thiefplayer08FreeThiefButton.Timer = thiefplayer08FreeThiefButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer08 != null && PoliceAndThief.thiefplayer08 == PlayerControl.LocalPlayer; },
                () => {
                    bool CanUse = false;
                    if (PoliceAndThief.currentThiefsCaptured > 0) {
                        if (Vector2.Distance(PoliceAndThief.thiefplayer08.transform.position, PoliceAndThief.cellbutton.transform.position) < 0.4f && !PoliceAndThief.thiefplayer08.Data.IsDead) {
                            CanUse = true;
                        }
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer08IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer08FreeThiefButton.Timer = thiefplayer08FreeThiefButton.MaxTimer; },
                PoliceAndThief.getFreeThiefButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer08 Take/Deliver Jewel Button
            thiefplayer08TakeDeliverJewelButton = new CustomButton(
                () => {
                    if (PoliceAndThief.thiefplayer08IsStealing) {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer08JewelId;
                        MessageWriter thiefScore = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefDeliverJewel, Hazel.SendOption.Reliable, -1);
                        thiefScore.Write(targetId);
                        thiefScore.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefScore);
                        RPCProcedure.policeandThiefDeliverJewel(targetId, jewelId);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer08JewelId;
                        MessageWriter thiefWhoTookATreasure = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefTakeJewel, Hazel.SendOption.Reliable, -1);
                        thiefWhoTookATreasure.Write(targetId);
                        thiefWhoTookATreasure.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefWhoTookATreasure);
                        RPCProcedure.policeandThiefTakeJewel(targetId, jewelId);
                    }
                    thiefplayer08TakeDeliverJewelButton.Timer = thiefplayer08TakeDeliverJewelButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer08 != null && PoliceAndThief.thiefplayer08 == PlayerControl.LocalPlayer; },
                () => {
                    if (PoliceAndThief.thiefplayer08IsStealing)
                        thiefplayer08TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getDeliverJewelButtonSprite();
                    else
                        thiefplayer08TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getTakeJewelButtonSprite();
                    bool CanUse = false;
                    if (PoliceAndThief.thiefTreasures.Count != 0) {
                        foreach (GameObject jewel in PoliceAndThief.thiefTreasures) {
                            if (jewel != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, jewel.transform.position) < 0.5f && !PoliceAndThief.thiefplayer08IsStealing) {
                                //CanUse = true;
                                switch (jewel.name) {
                                    case "jewel01":
                                        PoliceAndThief.thiefplayer08JewelId = 1;
                                        CanUse = !PoliceAndThief.jewel01BeingStealed;
                                        break;
                                    case "jewel02":
                                        PoliceAndThief.thiefplayer08JewelId = 2;
                                        CanUse = !PoliceAndThief.jewel02BeingStealed;
                                        break;
                                    case "jewel03":
                                        PoliceAndThief.thiefplayer08JewelId = 3;
                                        CanUse = !PoliceAndThief.jewel03BeingStealed;
                                        break;
                                    case "jewel04":
                                        PoliceAndThief.thiefplayer08JewelId = 4;
                                        CanUse = !PoliceAndThief.jewel04BeingStealed;
                                        break;
                                    case "jewel05":
                                        PoliceAndThief.thiefplayer08JewelId = 5;
                                        CanUse = !PoliceAndThief.jewel05BeingStealed;
                                        break;
                                    case "jewel06":
                                        PoliceAndThief.thiefplayer08JewelId = 6;
                                        CanUse = !PoliceAndThief.jewel06BeingStealed;
                                        break;
                                    case "jewel07":
                                        PoliceAndThief.thiefplayer08JewelId = 7;
                                        CanUse = !PoliceAndThief.jewel07BeingStealed;
                                        break;
                                    case "jewel08":
                                        PoliceAndThief.thiefplayer08JewelId = 8;
                                        CanUse = !PoliceAndThief.jewel08BeingStealed;
                                        break;
                                    case "jewel09":
                                        PoliceAndThief.thiefplayer08JewelId = 9;
                                        CanUse = !PoliceAndThief.jewel09BeingStealed;
                                        break;
                                    case "jewel10":
                                        PoliceAndThief.thiefplayer08JewelId = 10;
                                        CanUse = !PoliceAndThief.jewel10BeingStealed;
                                        break;
                                    case "jewel11":
                                        PoliceAndThief.thiefplayer08JewelId = 11;
                                        CanUse = !PoliceAndThief.jewel11BeingStealed;
                                        break;
                                    case "jewel12":
                                        PoliceAndThief.thiefplayer08JewelId = 12;
                                        CanUse = !PoliceAndThief.jewel12BeingStealed;
                                        break;
                                    case "jewel13":
                                        PoliceAndThief.thiefplayer08JewelId = 13;
                                        CanUse = !PoliceAndThief.jewel13BeingStealed;
                                        break;
                                    case "jewel14":
                                        PoliceAndThief.thiefplayer08JewelId = 14;
                                        CanUse = !PoliceAndThief.jewel14BeingStealed;
                                        break;
                                    case "jewel15":
                                        PoliceAndThief.thiefplayer08JewelId = 15;
                                        CanUse = !PoliceAndThief.jewel15BeingStealed;
                                        break;
                                }
                            }
                            else if (PoliceAndThief.jewelbutton != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.jewelbutton.transform.position) < 0.5f && PoliceAndThief.thiefplayer08IsStealing) {
                                CanUse = true;
                            }
                        }

                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer08IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer08TakeDeliverJewelButton.Timer = thiefplayer08TakeDeliverJewelButton.MaxTimer; },
                PoliceAndThief.getTakeJewelButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer09 Kill
            thiefplayer09KillButton = new CustomButton(
                () => {
                    byte targetId = PoliceAndThief.thiefplayer09currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(15);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.policeandThiefKills(targetId, 15);
                    thiefplayer09KillButton.Timer = thiefplayer09KillButton.MaxTimer;
                    PoliceAndThief.thiefplayer09currentTarget = null;
                },
                () => { return PoliceAndThief.thiefplayer09 != null && PoliceAndThief.thiefplayer09 == PlayerControl.LocalPlayer && PoliceAndThief.thiefTeamCanKill; },
                () => { return PoliceAndThief.thiefplayer09currentTarget && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer09IsReviving && !PlayerControl.LocalPlayer.Data.IsDead && !PoliceAndThief.thiefplayer09IsStealing; },
                () => { thiefplayer09KillButton.Timer = thiefplayer09KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Thiefplayer09 FreeThief Button
            thiefplayer09FreeThiefButton = new CustomButton(
                () => {
                    MessageWriter thiefFree = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefFreeThief, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(thiefFree);
                    RPCProcedure.policeandThiefFreeThief();
                    thiefplayer09FreeThiefButton.Timer = thiefplayer09FreeThiefButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer09 != null && PoliceAndThief.thiefplayer09 == PlayerControl.LocalPlayer; },
                () => {
                    bool CanUse = false;
                    if (PoliceAndThief.currentThiefsCaptured > 0) {
                        if (Vector2.Distance(PoliceAndThief.thiefplayer09.transform.position, PoliceAndThief.cellbutton.transform.position) < 0.4f && !PoliceAndThief.thiefplayer09.Data.IsDead) {
                            CanUse = true;
                        }
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer09IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer09FreeThiefButton.Timer = thiefplayer09FreeThiefButton.MaxTimer; },
                PoliceAndThief.getFreeThiefButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer09 Take/Deliver Jewel Button
            thiefplayer09TakeDeliverJewelButton = new CustomButton(
                () => {
                    if (PoliceAndThief.thiefplayer09IsStealing) {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer09JewelId;
                        MessageWriter thiefScore = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefDeliverJewel, Hazel.SendOption.Reliable, -1);
                        thiefScore.Write(targetId);
                        thiefScore.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefScore);
                        RPCProcedure.policeandThiefDeliverJewel(targetId, jewelId);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer09JewelId;
                        MessageWriter thiefWhoTookATreasure = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefTakeJewel, Hazel.SendOption.Reliable, -1);
                        thiefWhoTookATreasure.Write(targetId);
                        thiefWhoTookATreasure.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefWhoTookATreasure);
                        RPCProcedure.policeandThiefTakeJewel(targetId, jewelId);
                    }
                    thiefplayer09TakeDeliverJewelButton.Timer = thiefplayer09TakeDeliverJewelButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer09 != null && PoliceAndThief.thiefplayer09 == PlayerControl.LocalPlayer; },
                () => {
                    if (PoliceAndThief.thiefplayer09IsStealing)
                        thiefplayer09TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getDeliverJewelButtonSprite();
                    else
                        thiefplayer09TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getTakeJewelButtonSprite();
                    bool CanUse = false;
                    if (PoliceAndThief.thiefTreasures.Count != 0) {
                        foreach (GameObject jewel in PoliceAndThief.thiefTreasures) {
                            if (jewel != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, jewel.transform.position) < 0.5f && !PoliceAndThief.thiefplayer09IsStealing) {
                                //CanUse = true;
                                switch (jewel.name) {
                                    case "jewel01":
                                        PoliceAndThief.thiefplayer09JewelId = 1;
                                        CanUse = !PoliceAndThief.jewel01BeingStealed;
                                        break;
                                    case "jewel02":
                                        PoliceAndThief.thiefplayer09JewelId = 2;
                                        CanUse = !PoliceAndThief.jewel02BeingStealed;
                                        break;
                                    case "jewel03":
                                        PoliceAndThief.thiefplayer09JewelId = 3;
                                        CanUse = !PoliceAndThief.jewel03BeingStealed;
                                        break;
                                    case "jewel04":
                                        PoliceAndThief.thiefplayer09JewelId = 4;
                                        CanUse = !PoliceAndThief.jewel04BeingStealed;
                                        break;
                                    case "jewel05":
                                        PoliceAndThief.thiefplayer09JewelId = 5;
                                        CanUse = !PoliceAndThief.jewel05BeingStealed;
                                        break;
                                    case "jewel06":
                                        PoliceAndThief.thiefplayer09JewelId = 6;
                                        CanUse = !PoliceAndThief.jewel06BeingStealed;
                                        break;
                                    case "jewel07":
                                        PoliceAndThief.thiefplayer09JewelId = 7;
                                        CanUse = !PoliceAndThief.jewel07BeingStealed;
                                        break;
                                    case "jewel08":
                                        PoliceAndThief.thiefplayer09JewelId = 8;
                                        CanUse = !PoliceAndThief.jewel08BeingStealed;
                                        break;
                                    case "jewel09":
                                        PoliceAndThief.thiefplayer09JewelId = 9;
                                        CanUse = !PoliceAndThief.jewel09BeingStealed;
                                        break;
                                    case "jewel10":
                                        PoliceAndThief.thiefplayer09JewelId = 10;
                                        CanUse = !PoliceAndThief.jewel10BeingStealed;
                                        break;
                                    case "jewel11":
                                        PoliceAndThief.thiefplayer09JewelId = 11;
                                        CanUse = !PoliceAndThief.jewel11BeingStealed;
                                        break;
                                    case "jewel12":
                                        PoliceAndThief.thiefplayer09JewelId = 12;
                                        CanUse = !PoliceAndThief.jewel12BeingStealed;
                                        break;
                                    case "jewel13":
                                        PoliceAndThief.thiefplayer09JewelId = 13;
                                        CanUse = !PoliceAndThief.jewel13BeingStealed;
                                        break;
                                    case "jewel14":
                                        PoliceAndThief.thiefplayer09JewelId = 14;
                                        CanUse = !PoliceAndThief.jewel14BeingStealed;
                                        break;
                                    case "jewel15":
                                        PoliceAndThief.thiefplayer09JewelId = 15;
                                        CanUse = !PoliceAndThief.jewel15BeingStealed;
                                        break;
                                }
                            }
                            else if (PoliceAndThief.jewelbutton != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.jewelbutton.transform.position) < 0.5f && PoliceAndThief.thiefplayer09IsStealing) {
                                CanUse = true;
                            }
                        }

                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer09IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer09TakeDeliverJewelButton.Timer = thiefplayer09TakeDeliverJewelButton.MaxTimer; },
                PoliceAndThief.getTakeJewelButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer10 Kill
            thiefplayer10KillButton = new CustomButton(
                () => {
                    byte targetId = PoliceAndThief.thiefplayer10currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(16);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.policeandThiefKills(targetId, 16);
                    thiefplayer10KillButton.Timer = thiefplayer10KillButton.MaxTimer;
                    PoliceAndThief.thiefplayer10currentTarget = null;
                },
                () => { return PoliceAndThief.thiefplayer10 != null && PoliceAndThief.thiefplayer10 == PlayerControl.LocalPlayer && PoliceAndThief.thiefTeamCanKill; },
                () => { return PoliceAndThief.thiefplayer10currentTarget && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer10IsReviving && !PlayerControl.LocalPlayer.Data.IsDead && !PoliceAndThief.thiefplayer10IsStealing; },
                () => { thiefplayer10KillButton.Timer = thiefplayer10KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // Thiefplayer10 FreeThief Button
            thiefplayer10FreeThiefButton = new CustomButton(
                () => {
                    MessageWriter thiefFree = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefFreeThief, Hazel.SendOption.Reliable, -1);
                    AmongUsClient.Instance.FinishRpcImmediately(thiefFree);
                    RPCProcedure.policeandThiefFreeThief();
                    thiefplayer10FreeThiefButton.Timer = thiefplayer10FreeThiefButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer10 != null && PoliceAndThief.thiefplayer10 == PlayerControl.LocalPlayer; },
                () => {
                    bool CanUse = false;
                    if (PoliceAndThief.currentThiefsCaptured > 0) {
                        if (Vector2.Distance(PoliceAndThief.thiefplayer10.transform.position, PoliceAndThief.cellbutton.transform.position) < 0.4f && !PoliceAndThief.thiefplayer10.Data.IsDead) {
                            CanUse = true;
                        }
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer10IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer10FreeThiefButton.Timer = thiefplayer10FreeThiefButton.MaxTimer; },
                PoliceAndThief.getFreeThiefButtonSprite(),
                new Vector3(-1.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // Thiefplayer10 Take/Deliver Jewel Button
            thiefplayer10TakeDeliverJewelButton = new CustomButton(
                () => {
                    if (PoliceAndThief.thiefplayer10IsStealing) {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer10JewelId;
                        MessageWriter thiefScore = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefDeliverJewel, Hazel.SendOption.Reliable, -1);
                        thiefScore.Write(targetId);
                        thiefScore.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefScore);
                        RPCProcedure.policeandThiefDeliverJewel(targetId, jewelId);
                    }
                    else {
                        byte targetId = PlayerControl.LocalPlayer.PlayerId;
                        byte jewelId = PoliceAndThief.thiefplayer10JewelId;
                        MessageWriter thiefWhoTookATreasure = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.PoliceandThiefTakeJewel, Hazel.SendOption.Reliable, -1);
                        thiefWhoTookATreasure.Write(targetId);
                        thiefWhoTookATreasure.Write(jewelId);
                        AmongUsClient.Instance.FinishRpcImmediately(thiefWhoTookATreasure);
                        RPCProcedure.policeandThiefTakeJewel(targetId, jewelId);
                    }
                    thiefplayer10TakeDeliverJewelButton.Timer = thiefplayer10TakeDeliverJewelButton.MaxTimer;
                },
                () => { return PoliceAndThief.thiefplayer10 != null && PoliceAndThief.thiefplayer10 == PlayerControl.LocalPlayer; },
                () => {
                    if (PoliceAndThief.thiefplayer10IsStealing)
                        thiefplayer10TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getDeliverJewelButtonSprite();
                    else
                        thiefplayer10TakeDeliverJewelButton.actionButton.graphic.sprite = PoliceAndThief.getTakeJewelButtonSprite();
                    bool CanUse = false;
                    if (PoliceAndThief.thiefTreasures.Count != 0) {
                        foreach (GameObject jewel in PoliceAndThief.thiefTreasures) {
                            if (jewel != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, jewel.transform.position) < 0.5f && !PoliceAndThief.thiefplayer10IsStealing) {
                                //CanUse = true;
                                switch (jewel.name) {
                                    case "jewel01":
                                        PoliceAndThief.thiefplayer10JewelId = 1;
                                        CanUse = !PoliceAndThief.jewel01BeingStealed;
                                        break;
                                    case "jewel02":
                                        PoliceAndThief.thiefplayer10JewelId = 2;
                                        CanUse = !PoliceAndThief.jewel02BeingStealed;
                                        break;
                                    case "jewel03":
                                        PoliceAndThief.thiefplayer10JewelId = 3;
                                        CanUse = !PoliceAndThief.jewel03BeingStealed;
                                        break;
                                    case "jewel04":
                                        PoliceAndThief.thiefplayer10JewelId = 4;
                                        CanUse = !PoliceAndThief.jewel04BeingStealed;
                                        break;
                                    case "jewel05":
                                        PoliceAndThief.thiefplayer10JewelId = 5;
                                        CanUse = !PoliceAndThief.jewel05BeingStealed;
                                        break;
                                    case "jewel06":
                                        PoliceAndThief.thiefplayer10JewelId = 6;
                                        CanUse = !PoliceAndThief.jewel06BeingStealed;
                                        break;
                                    case "jewel07":
                                        PoliceAndThief.thiefplayer10JewelId = 7;
                                        CanUse = !PoliceAndThief.jewel07BeingStealed;
                                        break;
                                    case "jewel08":
                                        PoliceAndThief.thiefplayer10JewelId = 8;
                                        CanUse = !PoliceAndThief.jewel08BeingStealed;
                                        break;
                                    case "jewel09":
                                        PoliceAndThief.thiefplayer10JewelId = 9;
                                        CanUse = !PoliceAndThief.jewel09BeingStealed;
                                        break;
                                    case "jewel10":
                                        PoliceAndThief.thiefplayer10JewelId = 10;
                                        CanUse = !PoliceAndThief.jewel10BeingStealed;
                                        break;
                                    case "jewel11":
                                        PoliceAndThief.thiefplayer10JewelId = 11;
                                        CanUse = !PoliceAndThief.jewel11BeingStealed;
                                        break;
                                    case "jewel12":
                                        PoliceAndThief.thiefplayer10JewelId = 12;
                                        CanUse = !PoliceAndThief.jewel12BeingStealed;
                                        break;
                                    case "jewel13":
                                        PoliceAndThief.thiefplayer10JewelId = 13;
                                        CanUse = !PoliceAndThief.jewel13BeingStealed;
                                        break;
                                    case "jewel14":
                                        PoliceAndThief.thiefplayer10JewelId = 14;
                                        CanUse = !PoliceAndThief.jewel14BeingStealed;
                                        break;
                                    case "jewel15":
                                        PoliceAndThief.thiefplayer10JewelId = 15;
                                        CanUse = !PoliceAndThief.jewel15BeingStealed;
                                        break;
                                }
                            }
                            else if (PoliceAndThief.jewelbutton != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, PoliceAndThief.jewelbutton.transform.position) < 0.5f && PoliceAndThief.thiefplayer10IsStealing) {
                                CanUse = true;
                            }
                        }

                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !PoliceAndThief.thiefplayer10IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { thiefplayer10TakeDeliverJewelButton.Timer = thiefplayer10TakeDeliverJewelButton.MaxTimer; },
                PoliceAndThief.getTakeJewelButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T
            );

            // King of the hill buttons
            // greenplayer01 Kill
            greenplayer01KillButton = new CustomButton(
                () => {
                    byte targetId = KingOfTheHill.greenplayer01currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(1);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.kingOfTheHillKills(targetId, 1);
                    greenplayer01KillButton.Timer = greenplayer01KillButton.MaxTimer;
                    KingOfTheHill.greenplayer01currentTarget = null;
                },
                () => { return KingOfTheHill.greenplayer01 != null && KingOfTheHill.greenplayer01 == PlayerControl.LocalPlayer && PlayerControl.LocalPlayer != KingOfTheHill.greenKingplayer; },
                () => {
                    if (KingOfTheHill.localArrows.Count != 0) {
                        KingOfTheHill.localArrows[0].Update(KingOfTheHill.zoneone.transform.position, KingOfTheHill.zoneonecolor);
                        KingOfTheHill.localArrows[1].Update(KingOfTheHill.zonetwo.transform.position, KingOfTheHill.zonetwocolor);
                        KingOfTheHill.localArrows[2].Update(KingOfTheHill.zonethree.transform.position, KingOfTheHill.zonethreecolor);
                    }
                    return KingOfTheHill.greenplayer01currentTarget && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.greenplayer01IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { greenplayer01KillButton.Timer = greenplayer01KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // greenplayer02 Kill
            greenplayer02KillButton = new CustomButton(
                () => {
                    byte targetId = KingOfTheHill.greenplayer02currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(2);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.kingOfTheHillKills(targetId, 2);
                    greenplayer02KillButton.Timer = greenplayer02KillButton.MaxTimer;
                    KingOfTheHill.greenplayer02currentTarget = null;
                },
                () => { return KingOfTheHill.greenplayer02 != null && KingOfTheHill.greenplayer02 == PlayerControl.LocalPlayer && PlayerControl.LocalPlayer != KingOfTheHill.greenKingplayer; },
                () => {
                    if (KingOfTheHill.localArrows.Count != 0) {
                        KingOfTheHill.localArrows[0].Update(KingOfTheHill.zoneone.transform.position, KingOfTheHill.zoneonecolor);
                        KingOfTheHill.localArrows[1].Update(KingOfTheHill.zonetwo.transform.position, KingOfTheHill.zonetwocolor);
                        KingOfTheHill.localArrows[2].Update(KingOfTheHill.zonethree.transform.position, KingOfTheHill.zonethreecolor);
                    }
                    return KingOfTheHill.greenplayer02currentTarget && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.greenplayer02IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { greenplayer02KillButton.Timer = greenplayer02KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // greenplayer03 Kill
            greenplayer03KillButton = new CustomButton(
                () => {
                    byte targetId = KingOfTheHill.greenplayer03currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(3);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.kingOfTheHillKills(targetId, 3);
                    greenplayer03KillButton.Timer = greenplayer03KillButton.MaxTimer;
                    KingOfTheHill.greenplayer03currentTarget = null;
                },
                () => { return KingOfTheHill.greenplayer03 != null && KingOfTheHill.greenplayer03 == PlayerControl.LocalPlayer && PlayerControl.LocalPlayer != KingOfTheHill.greenKingplayer; },
                () => {
                    if (KingOfTheHill.localArrows.Count != 0) {
                        KingOfTheHill.localArrows[0].Update(KingOfTheHill.zoneone.transform.position, KingOfTheHill.zoneonecolor);
                        KingOfTheHill.localArrows[1].Update(KingOfTheHill.zonetwo.transform.position, KingOfTheHill.zonetwocolor);
                        KingOfTheHill.localArrows[2].Update(KingOfTheHill.zonethree.transform.position, KingOfTheHill.zonethreecolor);
                    }
                    return KingOfTheHill.greenplayer03currentTarget && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.greenplayer03IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { greenplayer03KillButton.Timer = greenplayer03KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // greenplayer04 Kill
            greenplayer04KillButton = new CustomButton(
                () => {
                    byte targetId = KingOfTheHill.greenplayer04currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(4);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.kingOfTheHillKills(targetId, 4);
                    greenplayer04KillButton.Timer = greenplayer04KillButton.MaxTimer;
                    KingOfTheHill.greenplayer04currentTarget = null;
                },
                () => { return KingOfTheHill.greenplayer04 != null && KingOfTheHill.greenplayer04 == PlayerControl.LocalPlayer && PlayerControl.LocalPlayer != KingOfTheHill.greenKingplayer; },
                () => {
                    if (KingOfTheHill.localArrows.Count != 0) {
                        KingOfTheHill.localArrows[0].Update(KingOfTheHill.zoneone.transform.position, KingOfTheHill.zoneonecolor);
                        KingOfTheHill.localArrows[1].Update(KingOfTheHill.zonetwo.transform.position, KingOfTheHill.zonetwocolor);
                        KingOfTheHill.localArrows[2].Update(KingOfTheHill.zonethree.transform.position, KingOfTheHill.zonethreecolor);
                    }
                    return KingOfTheHill.greenplayer04currentTarget && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.greenplayer04IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { greenplayer04KillButton.Timer = greenplayer04KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // greenplayer05 Kill
            greenplayer05KillButton = new CustomButton(
                () => {
                    byte targetId = KingOfTheHill.greenplayer05currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(5);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.kingOfTheHillKills(targetId, 5);
                    greenplayer05KillButton.Timer = greenplayer05KillButton.MaxTimer;
                    KingOfTheHill.greenplayer05currentTarget = null;
                },
                () => { return KingOfTheHill.greenplayer05 != null && KingOfTheHill.greenplayer05 == PlayerControl.LocalPlayer && PlayerControl.LocalPlayer != KingOfTheHill.greenKingplayer; },
                () => {
                    if (KingOfTheHill.localArrows.Count != 0) {
                        KingOfTheHill.localArrows[0].Update(KingOfTheHill.zoneone.transform.position, KingOfTheHill.zoneonecolor);
                        KingOfTheHill.localArrows[1].Update(KingOfTheHill.zonetwo.transform.position, KingOfTheHill.zonetwocolor);
                        KingOfTheHill.localArrows[2].Update(KingOfTheHill.zonethree.transform.position, KingOfTheHill.zonethreecolor);
                    }
                    return KingOfTheHill.greenplayer05currentTarget && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.greenplayer05IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { greenplayer05KillButton.Timer = greenplayer05KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // greenplayer06 Kill
            greenplayer06KillButton = new CustomButton(
                () => {
                    byte targetId = KingOfTheHill.greenplayer06currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(6);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.kingOfTheHillKills(targetId, 6);
                    greenplayer06KillButton.Timer = greenplayer06KillButton.MaxTimer;
                    KingOfTheHill.greenplayer06currentTarget = null;
                },
                () => { return KingOfTheHill.greenplayer06 != null && KingOfTheHill.greenplayer06 == PlayerControl.LocalPlayer && PlayerControl.LocalPlayer != KingOfTheHill.greenKingplayer; },
                () => {
                    if (KingOfTheHill.localArrows.Count != 0) {
                        KingOfTheHill.localArrows[0].Update(KingOfTheHill.zoneone.transform.position, KingOfTheHill.zoneonecolor);
                        KingOfTheHill.localArrows[1].Update(KingOfTheHill.zonetwo.transform.position, KingOfTheHill.zonetwocolor);
                        KingOfTheHill.localArrows[2].Update(KingOfTheHill.zonethree.transform.position, KingOfTheHill.zonethreecolor);
                    }
                    return KingOfTheHill.greenplayer06currentTarget && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.greenplayer06IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { greenplayer06KillButton.Timer = greenplayer06KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // greenKingplayer Kill
            greenKingplayerKillButton = new CustomButton(
                () => {
                    byte targetId = KingOfTheHill.greenKingplayercurrentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(7);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.kingOfTheHillKills(targetId, 7);
                    greenKingplayerKillButton.Timer = greenKingplayerKillButton.MaxTimer;
                    KingOfTheHill.greenKingplayercurrentTarget = null;
                },
                () => { return KingOfTheHill.greenKingplayer != null && KingOfTheHill.greenKingplayer == PlayerControl.LocalPlayer && KingOfTheHill.kingCanKill; },
                () => { return KingOfTheHill.greenKingplayercurrentTarget && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.greenKingIsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { greenKingplayerKillButton.Timer = greenKingplayerKillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // yellowplayer01 Kill
            yellowplayer01KillButton = new CustomButton(
                () => {
                    byte targetId = KingOfTheHill.yellowplayer01currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(9);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.kingOfTheHillKills(targetId, 9);
                    yellowplayer01KillButton.Timer = yellowplayer01KillButton.MaxTimer;
                    KingOfTheHill.yellowplayer01currentTarget = null;
                },
                () => { return KingOfTheHill.yellowplayer01 != null && KingOfTheHill.yellowplayer01 == PlayerControl.LocalPlayer && PlayerControl.LocalPlayer != KingOfTheHill.yellowKingplayer; },
                () => {
                    if (KingOfTheHill.localArrows.Count != 0) {
                        KingOfTheHill.localArrows[0].Update(KingOfTheHill.zoneone.transform.position, KingOfTheHill.zoneonecolor);
                        KingOfTheHill.localArrows[1].Update(KingOfTheHill.zonetwo.transform.position, KingOfTheHill.zonetwocolor);
                        KingOfTheHill.localArrows[2].Update(KingOfTheHill.zonethree.transform.position, KingOfTheHill.zonethreecolor);
                    }
                    return KingOfTheHill.yellowplayer01currentTarget && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.yellowplayer01IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { yellowplayer01KillButton.Timer = yellowplayer01KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );


            // yellowplayer02 Kill
            yellowplayer02KillButton = new CustomButton(
                () => {
                    byte targetId = KingOfTheHill.yellowplayer02currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(10);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.kingOfTheHillKills(targetId, 10);
                    yellowplayer02KillButton.Timer = yellowplayer02KillButton.MaxTimer;
                    KingOfTheHill.yellowplayer02currentTarget = null;
                },
                () => { return KingOfTheHill.yellowplayer02 != null && KingOfTheHill.yellowplayer02 == PlayerControl.LocalPlayer && PlayerControl.LocalPlayer != KingOfTheHill.yellowKingplayer; },
                () => {
                    if (KingOfTheHill.localArrows.Count != 0) {
                        KingOfTheHill.localArrows[0].Update(KingOfTheHill.zoneone.transform.position, KingOfTheHill.zoneonecolor);
                        KingOfTheHill.localArrows[1].Update(KingOfTheHill.zonetwo.transform.position, KingOfTheHill.zonetwocolor);
                        KingOfTheHill.localArrows[2].Update(KingOfTheHill.zonethree.transform.position, KingOfTheHill.zonethreecolor);
                    }
                    return KingOfTheHill.yellowplayer02currentTarget && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.yellowplayer02IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { yellowplayer02KillButton.Timer = yellowplayer02KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // yellowplayer03 Kill
            yellowplayer03KillButton = new CustomButton(
                () => {
                    byte targetId = KingOfTheHill.yellowplayer03currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(11);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.kingOfTheHillKills(targetId, 11);
                    yellowplayer03KillButton.Timer = yellowplayer03KillButton.MaxTimer;
                    KingOfTheHill.yellowplayer03currentTarget = null;
                },
                () => { return KingOfTheHill.yellowplayer03 != null && KingOfTheHill.yellowplayer03 == PlayerControl.LocalPlayer && PlayerControl.LocalPlayer != KingOfTheHill.yellowKingplayer; },
                () => {
                    if (KingOfTheHill.localArrows.Count != 0) {
                        KingOfTheHill.localArrows[0].Update(KingOfTheHill.zoneone.transform.position, KingOfTheHill.zoneonecolor);
                        KingOfTheHill.localArrows[1].Update(KingOfTheHill.zonetwo.transform.position, KingOfTheHill.zonetwocolor);
                        KingOfTheHill.localArrows[2].Update(KingOfTheHill.zonethree.transform.position, KingOfTheHill.zonethreecolor);
                    }
                    return KingOfTheHill.yellowplayer03currentTarget && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.yellowplayer03IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { yellowplayer03KillButton.Timer = yellowplayer03KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // yellowplayer04 Kill
            yellowplayer04KillButton = new CustomButton(
                () => {
                    byte targetId = KingOfTheHill.yellowplayer04currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(12);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.kingOfTheHillKills(targetId, 12);
                    yellowplayer04KillButton.Timer = yellowplayer04KillButton.MaxTimer;
                    KingOfTheHill.yellowplayer04currentTarget = null;
                },
                () => { return KingOfTheHill.yellowplayer04 != null && KingOfTheHill.yellowplayer04 == PlayerControl.LocalPlayer && PlayerControl.LocalPlayer != KingOfTheHill.yellowKingplayer; },
                () => {
                    if (KingOfTheHill.localArrows.Count != 0) {
                        KingOfTheHill.localArrows[0].Update(KingOfTheHill.zoneone.transform.position, KingOfTheHill.zoneonecolor);
                        KingOfTheHill.localArrows[1].Update(KingOfTheHill.zonetwo.transform.position, KingOfTheHill.zonetwocolor);
                        KingOfTheHill.localArrows[2].Update(KingOfTheHill.zonethree.transform.position, KingOfTheHill.zonethreecolor);
                    }
                    return KingOfTheHill.yellowplayer04currentTarget && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.yellowplayer04IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { yellowplayer04KillButton.Timer = yellowplayer04KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // yellowplayer05 Kill
            yellowplayer05KillButton = new CustomButton(
                () => {
                    byte targetId = KingOfTheHill.yellowplayer05currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(13);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.kingOfTheHillKills(targetId, 13);
                    yellowplayer05KillButton.Timer = yellowplayer05KillButton.MaxTimer;
                    KingOfTheHill.yellowplayer05currentTarget = null;
                },
                () => { return KingOfTheHill.yellowplayer05 != null && KingOfTheHill.yellowplayer05 == PlayerControl.LocalPlayer && PlayerControl.LocalPlayer != KingOfTheHill.yellowKingplayer; },
                () => {
                    if (KingOfTheHill.localArrows.Count != 0) {
                        KingOfTheHill.localArrows[0].Update(KingOfTheHill.zoneone.transform.position, KingOfTheHill.zoneonecolor);
                        KingOfTheHill.localArrows[1].Update(KingOfTheHill.zonetwo.transform.position, KingOfTheHill.zonetwocolor);
                        KingOfTheHill.localArrows[2].Update(KingOfTheHill.zonethree.transform.position, KingOfTheHill.zonethreecolor);
                    }
                    return KingOfTheHill.yellowplayer05currentTarget && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.yellowplayer05IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { yellowplayer05KillButton.Timer = yellowplayer05KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // yellowplayer06 Kill
            yellowplayer06KillButton = new CustomButton(
                () => {
                    byte targetId = KingOfTheHill.yellowplayer06currentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(14);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.kingOfTheHillKills(targetId, 14);
                    yellowplayer06KillButton.Timer = yellowplayer06KillButton.MaxTimer;
                    KingOfTheHill.yellowplayer06currentTarget = null;
                },
                () => { return KingOfTheHill.yellowplayer06 != null && KingOfTheHill.yellowplayer06 == PlayerControl.LocalPlayer && PlayerControl.LocalPlayer != KingOfTheHill.yellowKingplayer; },
                () => {
                    if (KingOfTheHill.localArrows.Count != 0) {
                        KingOfTheHill.localArrows[0].Update(KingOfTheHill.zoneone.transform.position, KingOfTheHill.zoneonecolor);
                        KingOfTheHill.localArrows[1].Update(KingOfTheHill.zonetwo.transform.position, KingOfTheHill.zonetwocolor);
                        KingOfTheHill.localArrows[2].Update(KingOfTheHill.zonethree.transform.position, KingOfTheHill.zonethreecolor);
                    }
                    return KingOfTheHill.yellowplayer06currentTarget && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.yellowplayer06IsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { yellowplayer06KillButton.Timer = yellowplayer06KillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // UsurperPlayer Kill
            usurperPlayerKillButton = new CustomButton(
                () => {
                    byte targetId = KingOfTheHill.usurperPlayercurrentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(15);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.kingOfTheHillKills(targetId, 15);
                    usurperPlayerKillButton.Timer = usurperPlayerKillButton.MaxTimer;
                    KingOfTheHill.usurperPlayercurrentTarget = null;
                },
                () => { return KingOfTheHill.usurperPlayer != null && KingOfTheHill.usurperPlayer == PlayerControl.LocalPlayer; },
                () => {
                    if (KingOfTheHill.localArrows.Count != 0) {
                        KingOfTheHill.localArrows[0].Update(KingOfTheHill.zoneone.transform.position, KingOfTheHill.zoneonecolor);
                        KingOfTheHill.localArrows[1].Update(KingOfTheHill.zonetwo.transform.position, KingOfTheHill.zonetwocolor);
                        KingOfTheHill.localArrows[2].Update(KingOfTheHill.zonethree.transform.position, KingOfTheHill.zonethreecolor);
                    }
                    return KingOfTheHill.usurperPlayercurrentTarget && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.usurperPlayerIsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { usurperPlayerKillButton.Timer = usurperPlayerKillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // yellowKingplayer Kill
            yellowKingplayerKillButton = new CustomButton(
                () => {
                    byte targetId = KingOfTheHill.yellowKingplayercurrentTarget.PlayerId;
                    MessageWriter killWriter = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillKills, Hazel.SendOption.Reliable, -1);
                    killWriter.Write(targetId);
                    killWriter.Write(16);
                    AmongUsClient.Instance.FinishRpcImmediately(killWriter);
                    RPCProcedure.kingOfTheHillKills(targetId, 16);
                    yellowKingplayerKillButton.Timer = yellowKingplayerKillButton.MaxTimer;
                    KingOfTheHill.yellowKingplayercurrentTarget = null;
                },
                () => { return KingOfTheHill.yellowKingplayer != null && KingOfTheHill.yellowKingplayer == PlayerControl.LocalPlayer && KingOfTheHill.kingCanKill; },
                () => { return KingOfTheHill.yellowKingplayercurrentTarget && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.yellowKingIsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { yellowKingplayerKillButton.Timer = yellowKingplayerKillButton.MaxTimer; },
                __instance.KillButton.graphic.sprite,
                new Vector3(0, 1f, 0),
                __instance,
                KeyCode.Q
            );

            // greenKingplayer Capture
            greenKingplayerCaptureZoneButton = new CustomButton(
                () => {

                    if (KingOfTheHill.whichGreenKingplayerzone != 0) {
                        greenKingplayerCaptureZoneButton.HasEffect = true;
                    }

                },
                () => { return KingOfTheHill.greenKingplayer != null && KingOfTheHill.greenKingplayer == PlayerControl.LocalPlayer; },
                () => {
                    if (KingOfTheHill.localArrows.Count != 0) {
                        KingOfTheHill.localArrows[0].Update(KingOfTheHill.zoneone.transform.position, KingOfTheHill.zoneonecolor);
                        KingOfTheHill.localArrows[1].Update(KingOfTheHill.zonetwo.transform.position, KingOfTheHill.zonetwocolor);
                        KingOfTheHill.localArrows[2].Update(KingOfTheHill.zonethree.transform.position, KingOfTheHill.zonethreecolor);
                    }
                    bool CanUse = false;
                    KingOfTheHill.whichGreenKingplayerzone = 0;
                    if (KingOfTheHill.kingZones.Count != 0) {
                        foreach (GameObject zone in KingOfTheHill.kingZones) {
                            if (zone != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, zone.transform.position) < 0.5f) {
                                switch (zone.name) {
                                    case "zoneone":
                                        KingOfTheHill.whichGreenKingplayerzone = 1;
                                        CanUse = !KingOfTheHill.greenKinghaszoneone;
                                        break;
                                    case "zonetwo":
                                        KingOfTheHill.whichGreenKingplayerzone = 2;
                                        CanUse = !KingOfTheHill.greenKinghaszonetwo;
                                        break;
                                    case "zonethree":
                                        KingOfTheHill.whichGreenKingplayerzone = 3;
                                        CanUse = !KingOfTheHill.greenKinghaszonethree;
                                        break;
                                }
                            }
                        }
                        if (greenKingplayerCaptureZoneButton.isEffectActive && (KingOfTheHill.whichGreenKingplayerzone == 0 || PlayerControl.LocalPlayer.Data.IsDead)) {
                            greenKingplayerCaptureZoneButton.Timer = 0f;
                            greenKingplayerCaptureZoneButton.isEffectActive = false;
                        }

                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.greenKingIsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { greenKingplayerCaptureZoneButton.Timer = greenKingplayerCaptureZoneButton.MaxTimer; },
                KingOfTheHill.getPlaceGreenFlagButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T,
                true,
                3,
                () => {
                    if (KingOfTheHill.whichGreenKingplayerzone != 0) {
                        byte zoneId = KingOfTheHill.whichGreenKingplayerzone;
                        MessageWriter greenKingCaptured = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillCapture, Hazel.SendOption.Reliable, -1);
                        greenKingCaptured.Write(zoneId);
                        greenKingCaptured.Write(1);
                        AmongUsClient.Instance.FinishRpcImmediately(greenKingCaptured);
                        RPCProcedure.kingoftheHillCapture(zoneId, 1);

                        greenKingplayerCaptureZoneButton.Timer = greenKingplayerCaptureZoneButton.MaxTimer;
                    }
                }
            );

            // yellowKingplayer Capture
            yellowKingplayerCaptureZoneButton = new CustomButton(
                () => {

                    if (KingOfTheHill.whichYellowKingplayerzone != 0) {
                        yellowKingplayerCaptureZoneButton.HasEffect = true;
                    }

                },
                () => { return KingOfTheHill.yellowKingplayer != null && KingOfTheHill.yellowKingplayer == PlayerControl.LocalPlayer; },
                () => {
                    if (KingOfTheHill.localArrows.Count != 0) {
                        KingOfTheHill.localArrows[0].Update(KingOfTheHill.zoneone.transform.position, KingOfTheHill.zoneonecolor);
                        KingOfTheHill.localArrows[1].Update(KingOfTheHill.zonetwo.transform.position, KingOfTheHill.zonetwocolor);
                        KingOfTheHill.localArrows[2].Update(KingOfTheHill.zonethree.transform.position, KingOfTheHill.zonethreecolor);
                    }
                    bool CanUse = false;
                    KingOfTheHill.whichYellowKingplayerzone = 0;
                    if (KingOfTheHill.kingZones.Count != 0) {
                        foreach (GameObject zone in KingOfTheHill.kingZones) {
                            if (zone != null && Vector2.Distance(PlayerControl.LocalPlayer.transform.position, zone.transform.position) < 0.5f) {
                                switch (zone.name) {
                                    case "zoneone":
                                        KingOfTheHill.whichYellowKingplayerzone = 1;
                                        CanUse = !KingOfTheHill.yellowKinghaszoneone;
                                        break;
                                    case "zonetwo":
                                        KingOfTheHill.whichYellowKingplayerzone = 2;
                                        CanUse = !KingOfTheHill.yellowKinghaszonetwo;
                                        break;
                                    case "zonethree":
                                        KingOfTheHill.whichYellowKingplayerzone = 3;
                                        CanUse = !KingOfTheHill.yellowKinghaszonethree;
                                        break;
                                }
                            }
                        }
                        if (yellowKingplayerCaptureZoneButton.isEffectActive && (KingOfTheHill.whichYellowKingplayerzone == 0 || PlayerControl.LocalPlayer.Data.IsDead)) {
                            yellowKingplayerCaptureZoneButton.Timer = 0f;
                            yellowKingplayerCaptureZoneButton.isEffectActive = false;
                        }
                    }
                    return CanUse && PlayerControl.LocalPlayer.CanMove && !KingOfTheHill.yellowKingIsReviving && !PlayerControl.LocalPlayer.Data.IsDead;
                },
                () => { yellowKingplayerCaptureZoneButton.Timer = yellowKingplayerCaptureZoneButton.MaxTimer; },
                KingOfTheHill.getPlaceYellowFlagButtonSprite(),
                new Vector3(-0.9f, -0.06f, 0),
                __instance,
                KeyCode.T,
                true,
                3,
                () => {
                    if (KingOfTheHill.whichYellowKingplayerzone != 0) {
                        byte zoneId = KingOfTheHill.whichYellowKingplayerzone;
                        MessageWriter yellowKingCaptured = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId, (byte)CustomRPC.KingoftheHillCapture, Hazel.SendOption.Reliable, -1);
                        yellowKingCaptured.Write(zoneId);
                        yellowKingCaptured.Write(2);
                        AmongUsClient.Instance.FinishRpcImmediately(yellowKingCaptured);
                        RPCProcedure.kingoftheHillCapture(zoneId, 2);

                        yellowKingplayerCaptureZoneButton.Timer = yellowKingplayerCaptureZoneButton.MaxTimer;
                    }
                }
            );

            setCustomButtonCooldowns();
        }
    }
}