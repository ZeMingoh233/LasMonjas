using System.Net;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using HarmonyLib;
using Hazel;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using UnityEngine;
using LasMonjas.Objects;

namespace LasMonjas
{
    [HarmonyPatch]
    public static class LasMonjas
    {
        public static System.Random rnd = new System.Random((int)DateTime.Now.Ticks);

        public static bool removedSwipe = false;

        public static bool removedAirshipDoors = false;

        public static bool activatedSensei = false;

        public static bool updatedSenseiMinimap = false;

        public static bool updatedSenseiAdminmap = false;

        public static bool createdduelarena = false;

        public static bool createdcapturetheflag = false;

        public static bool createdpoliceandthief = false;

        public static bool createdkingofthehill = false;
        
        public static bool activatedReportButtonAfterCustomMode = false;

        public static int quackNumber = 0;

        public static int alivePlayers = 15;

        public static void clearAndReloadRoles() {
            Mimic.clearAndReload();
            Painter.clearAndReload();
            Demon.clearAndReload();
            Janitor.clearAndReload();
            Ilusionist.clearAndReload();
            Manipulator.clearAndReload();
            Bomberman.clearAndReload();
            Chameleon.clearAndReload();
            Gambler.clearAndReload();
            Sorcerer.clearAndReload();

            Renegade.clearAndReload();
            Minion.clearAndReload();
            BountyHunter.clearAndReload();
            Trapper.clearAndReload();
            Yinyanger.clearAndReload();
            Challenger.clearAndReload();

            Joker.clearAndReload();
            RoleThief.clearAndReload();
            Pyromaniac.clearAndReload();
            TreasureHunter.clearAndReload();
            Devourer.clearAndReload();

            Captain.clearAndReload();
            Mechanic.clearAndReload();
            Sheriff.clearAndReload();
            Detective.clearAndReload();
            Forensic.clearAndReload();
            TimeTraveler.clearAndReload();
            Squire.clearAndReload();
            Cheater.clearAndReload();
            FortuneTeller.clearAndReload();
            Hacker.clearAndReload();
            Sleuth.clearAndReload();
            Fink.clearAndReload();
            Kid.clearAndReload();
            Welder.clearAndReload();
            Spiritualist.clearAndReload();
            TheChosenOne.clearAndReload();
            Vigilant.clearAndReload();
            Performer.clearAndReload();
            Hunter.clearAndReload();
            Jinx.clearAndReload();

            Modifiers.clearAndReload();

            CaptureTheFlag.clearAndReload();

            PoliceAndThief.clearAndReload();

            KingOfTheHill.clearAndReload();
            
            removedSwipe = false;
            removedAirshipDoors = false;
            activatedSensei = false;
            updatedSenseiMinimap = false;
            updatedSenseiAdminmap = false;
            createdduelarena = false;
            createdcapturetheflag = false;
            createdpoliceandthief = false;
            createdkingofthehill = false;
            activatedReportButtonAfterCustomMode = false;
            quackNumber = 0;
            alivePlayers = 15;
        }

    }

    public static class Mimic
    {
        public static PlayerControl mimic;
        public static Color color = Palette.ImpostorRed;

        public static float cooldown = 30f;
        public static float duration = 10f;
        public static float backUpduration = 10f;

        public static PlayerControl currentTarget;
        public static PlayerControl pickTarget;
        public static PlayerControl transformTarget;
        public static float transformTimer = 0f;

        public static void resetMimic() {
            transformTarget = null;
            transformTimer = 0f;
            if (mimic == null) return;
            mimic.setDefaultLook();
        }

        public static void clearAndReload() {
            resetMimic();
            mimic = null;
            currentTarget = null;
            pickTarget = null;
            transformTarget = null;
            transformTimer = 0f;
            cooldown = CustomOptionHolder.mimicCooldown.getFloat();
            duration = CustomOptionHolder.mimicDuration.getFloat();
            backUpduration = duration;
        }

        private static Sprite pickTargetSprite;
        
        public static Sprite getpickTargetSprite() {
            if (pickTargetSprite) return pickTargetSprite;
            pickTargetSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.MimicPickTargetButton.png", 90f);
            return pickTargetSprite;
        }

        private static Sprite transformSprite;
        
        public static Sprite getTransformSprite() {
            if (transformSprite) return transformSprite;
            transformSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.MimicTransformButton.png", 90f);
            return transformSprite;
        }
    }

    public static class Painter
    {
        public static PlayerControl painter;
        public static Color color = Palette.ImpostorRed;

        public static float cooldown = 30f;
        public static float duration = 10f;
        public static float backUpduration = 10f;
        public static float painterTimer = 0f;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.PainterPaintButton.png", 90f);
            return buttonSprite;
        }

        public static void resetPaint() {
            painterTimer = 0f;
            foreach (PlayerControl p in PlayerControl.AllPlayerControls)
                p.setDefaultLook();
        }

        public static void clearAndReload() {
            resetPaint();
            painter = null;
            painterTimer = 0f;
            cooldown = CustomOptionHolder.painterCooldown.getFloat();
            duration = CustomOptionHolder.painterDuration.getFloat();
            backUpduration = duration;
        }
    }

    public static class Demon
    {
        public static PlayerControl demon;
        public static Color color = Palette.ImpostorRed;

        public static float delay = 10f;
        public static float cooldown = 30f;
        public static bool canKillNearNun = true;
        public static bool localPlacedNun = false;
        public static bool nunsActive = true;

        public static PlayerControl currentTarget;
        public static PlayerControl bitten;
        public static bool targetNearNun = false;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.DemonBiteButton.png", 90f);
            return buttonSprite;
        }

        private static Sprite nunButtonSprite;
        public static Sprite getNunButtonSprite() {
            if (nunButtonSprite) return nunButtonSprite;
            nunButtonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.DemonNunButton.png", 90f);
            return nunButtonSprite;
        }

        public static void clearAndReload() {
            demon = null;
            bitten = null;
            targetNearNun = false;
            localPlacedNun = false;
            currentTarget = null;
            nunsActive = CustomOptionHolder.demonSpawnRate.getSelection() > 0;
            delay = CustomOptionHolder.demonKillDelay.getFloat();
            cooldown = CustomOptionHolder.demonCooldown.getFloat();
            canKillNearNun = CustomOptionHolder.demonCanKillNearNuns.getBool();
        }
    }
    public static class Janitor
    {
        public static PlayerControl janitor;
        public static Color color = Palette.ImpostorRed;

        public static float cooldown = 30f;
        public static bool dragginBody = false;
        public static byte bodyId = 0;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.JanitorCleanButton.png", 90f);
            return buttonSprite;
        }

        private static Sprite buttonDragSprite;
        public static Sprite getDragButtonSprite() {
            if (buttonDragSprite) return buttonDragSprite;
            buttonDragSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.JanitorDragBodyButton.png", 90f);
            return buttonDragSprite;
        }

        private static Sprite buttonMoveSprite;
        public static Sprite getMoveBodyButtonSprite() {
            if (buttonMoveSprite) return buttonMoveSprite;
            buttonMoveSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.JanitorMoveBodyButton.png", 90f);
            return buttonMoveSprite;
        }

        public static void clearAndReload() {
            janitor = null;
            cooldown = CustomOptionHolder.janitorCooldown.getFloat();
            dragginBody = false;
            bodyId = 0;
        }
    }

    public static class Ilusionist
    {
        public static PlayerControl ilusionist;
        public static Color color = Palette.ImpostorRed;
        public static float placeHatCooldown = 30f;

        private static Sprite placeHatButtonSprite;
        private static Sprite ilusionistVentButtonSprite;
        private static Sprite lightOutButtonSprite;
        public static float backUpduration = 10f;

        public static float lightsOutCooldown = 30f;
        public static float lightsOutDuration = 10f;
        public static float lightsOutTimer = 0f;

        public static Sprite getPlaceHatButtonSprite() {
            if (placeHatButtonSprite) return placeHatButtonSprite;
            placeHatButtonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.IlusionistPlaceHatButton.png", 90f);
            return placeHatButtonSprite;
        }

        public static Sprite getIlusionistVentButtonSprite() {
            if (ilusionistVentButtonSprite) return ilusionistVentButtonSprite;
            ilusionistVentButtonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.IlusionistVentButton.png", 90f);
            return ilusionistVentButtonSprite;
        }

        public static Sprite getLightsOutButtonSprite() {
            if (lightOutButtonSprite) return lightOutButtonSprite;
            lightOutButtonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.IlusionistLightsOutButton.png", 90f);
            return lightOutButtonSprite;
        }

        public static void clearAndReload() {
            ilusionist = null;
            placeHatCooldown = CustomOptionHolder.ilusionistPlaceHatCooldown.getFloat();
            lightsOutTimer = 0f;
            lightsOutCooldown = CustomOptionHolder.ilusionistLightsOutCooldown.getFloat();
            lightsOutDuration = CustomOptionHolder.ilusionistLightsOutDuration.getFloat();
            Hats.UpdateStates(); 
            backUpduration = lightsOutDuration;
        }

    }

    public static class Manipulator
    {

        public static PlayerControl manipulator;
        public static Color color = Palette.ImpostorRed;

        public static PlayerControl currentTarget;
        public static PlayerControl manipulatedVictim;
        public static PlayerControl manipulatedVictimTarget;

        public static float cooldown = 30f;

        private static Sprite manipulateButtonSprite;
        private static Sprite manipulateKillButtonSprite;

        public static Sprite getManipulateButtonSprite() {
            if (manipulateButtonSprite) return manipulateButtonSprite;
            manipulateButtonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.ManipulatorManipulateButton.png", 90f);
            return manipulateButtonSprite;
        }

        public static Sprite getManipulateKillButtonSprite() {
            if (manipulateKillButtonSprite) return manipulateKillButtonSprite;
            manipulateKillButtonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.ManipulatorKillButton.png", 90f);
            return manipulateKillButtonSprite;
        }

        public static void clearAndReload() {
            manipulator = null;
            currentTarget = null;
            manipulatedVictim = null;
            manipulatedVictimTarget = null;
            cooldown = CustomOptionHolder.manipulatorCooldown.getFloat();
        }

        public static void resetManipulate() {
            HudManagerStartPatch.manipulatorManipulateButton.Timer = HudManagerStartPatch.manipulatorManipulateButton.MaxTimer;
            HudManagerStartPatch.manipulatorManipulateButton.Sprite = Manipulator.getManipulateButtonSprite();
            HudManagerStartPatch.manipulatorManipulateButton.actionButton.cooldownTimerText.color = Palette.EnabledColor;
            currentTarget = null;
            manipulatedVictim = null;
            manipulatedVictimTarget = null;
        }
    }

    public static class Bomberman
    {
        public static PlayerControl bomberman;
        public static Color color = Palette.ImpostorRed;
        public static float bombCooldown = 30f;
        public static float bombDuration = 10f;
        public static float bombTimer = 0f;
        public static bool activeBomb = false;
        public static bool triggerBombExploded = false;
        public static int currentBombNumber = 0;


        private static Sprite bombButtonSprite;
        public static Sprite getBombButtonSprite() {
            if (bombButtonSprite) return bombButtonSprite;
            bombButtonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.BombermanBombButton.png", 90f);
            return bombButtonSprite;
        }

        public static void clearAndReload() {
            bomberman = null;
            bombTimer = 0f;
            bombCooldown = CustomOptionHolder.bombermanBombCooldown.getFloat();
            bombDuration = CustomOptionHolder.bombermanBombDuration.getFloat();
            activeBomb = false;
            triggerBombExploded = false;
            currentBombNumber = 0;
        }

    }

    public static class Chameleon
    {
        public static PlayerControl chameleon;
        public static Color color = Palette.ImpostorRed;

        public static float cooldown = 30f;
        public static float duration = 10f;

        public static float chameleonTimer = 0f;
        public static float backUpduration = 10f;

        private static Sprite buttonInvisibleSprite;
        public static Sprite getInvisibleButtonSprite() {
            if (buttonInvisibleSprite) return buttonInvisibleSprite;
            buttonInvisibleSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.ChameleonInvisibleButton.png", 90f);
            return buttonInvisibleSprite;
        }

        public static void resetChameleon() {
            chameleonTimer = 0f;
            if (chameleon != null) {
                chameleon.nameText.color = new Color(chameleon.nameText.color.r, chameleon.nameText.color.g, chameleon.nameText.color.b, 1);
                if (chameleon.CurrentPet != null && chameleon.CurrentPet.rend != null && chameleon.CurrentPet.shadowRend != null) {
                    chameleon.CurrentPet.rend.color = new Color(chameleon.CurrentPet.rend.color.r, chameleon.CurrentPet.rend.color.g, chameleon.CurrentPet.rend.color.b, 1);
                    chameleon.CurrentPet.shadowRend.color = new Color(chameleon.CurrentPet.shadowRend.color.r, chameleon.CurrentPet.shadowRend.color.g, chameleon.CurrentPet.shadowRend.color.b, 1);
                }
                if (chameleon.HatRenderer != null) {
                    chameleon.HatRenderer.Parent.color = new Color(chameleon.HatRenderer.Parent.color.r, chameleon.HatRenderer.Parent.color.g, chameleon.HatRenderer.Parent.color.b, 1);
                    chameleon.HatRenderer.BackLayer.color = new Color(chameleon.HatRenderer.BackLayer.color.r, chameleon.HatRenderer.BackLayer.color.g, chameleon.HatRenderer.BackLayer.color.b, 1);
                    chameleon.HatRenderer.FrontLayer.color = new Color(chameleon.HatRenderer.FrontLayer.color.r, chameleon.HatRenderer.FrontLayer.color.g, chameleon.HatRenderer.FrontLayer.color.b, 1);
                }
                if (chameleon.VisorSlot != null) {
                    chameleon.VisorSlot.Image.color = new Color(chameleon.VisorSlot.Image.color.r, chameleon.VisorSlot.Image.color.g, chameleon.VisorSlot.Image.color.b, 1);
                }
                chameleon.MyPhysics.Skin.layer.color = new Color(chameleon.MyPhysics.Skin.layer.color.r, chameleon.MyPhysics.Skin.layer.color.g, chameleon.MyPhysics.Skin.layer.color.b, 1);
            }
        }

        public static void clearAndReload() {
            chameleon = null;
            chameleonTimer = 0f;
            cooldown = CustomOptionHolder.chameleonCooldown.getFloat();
            duration = CustomOptionHolder.chameleonDuration.getFloat();
            backUpduration = duration;
        }

    }

    public static class Gambler
    {
        public static PlayerControl gambler;
        public static Color color = Palette.ImpostorRed;
        private static Sprite targetSprite;
        public static bool canCallEmergency = false;
        public static int numberOfShots = 2;
        public static bool canShootMultipleTimes = false;
        public static bool canKillThroughShield = true;

        public static Sprite getTargetSprite() {
            if (targetSprite) return targetSprite;
            targetSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.GamblerTargetIcon.png", 150f);
            return targetSprite;
        }

        public static void clearAndReload() {
            gambler = null;
            canCallEmergency = CustomOptionHolder.gamblerCanCallEmergency.getBool();
            numberOfShots = Mathf.RoundToInt(CustomOptionHolder.gamblerNumberOfShots.getFloat());
            canShootMultipleTimes = CustomOptionHolder.gamblerCanShootMultipleTimes.getBool();
            canKillThroughShield = CustomOptionHolder.gamblerCanKillThroughShield.getBool();
        }
    }

    public static class Sorcerer
    {
        public static PlayerControl sorcerer;
        public static Color color = Palette.ImpostorRed;

        public static List<PlayerControl> spelledPlayers = new List<PlayerControl>();
        public static PlayerControl currentTarget;
        public static PlayerControl spellTarget;
        public static float cooldown = 30f;
        public static float spellDuration = 2f;
        public static float cooldownAddition = 10f;
        public static float cooldownAdditionInitial = 10f;
        public static float currentCooldownAddition = 0f;
        public static bool canCallEmergency = false;

        private static Sprite buttonSpellSprite;
        public static Sprite getSpellButtonSprite() {
            if (buttonSpellSprite) return buttonSpellSprite;
            buttonSpellSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.SorcererSpellButton.png", 90f);
            return buttonSpellSprite;
        }

        private static Sprite spelledMeetingSprite;
        public static Sprite getSpelledMeetingSprite() {
            if (spelledMeetingSprite) return spelledMeetingSprite;
            spelledMeetingSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.SorcererSpellButtonMeeting.png", 225f);
            return spelledMeetingSprite;
        }


        public static void clearAndReload() {
            sorcerer = null;
            spelledPlayers = new List<PlayerControl>();
            currentTarget = spellTarget = null;
            cooldown = CustomOptionHolder.sorcererCooldown.getFloat();
            cooldownAddition = CustomOptionHolder.sorcererAdditionalCooldown.getFloat();
            cooldownAdditionInitial = cooldownAddition;
            currentCooldownAddition = CustomOptionHolder.sorcererCooldown.getFloat();
            spellDuration = CustomOptionHolder.sorcererSpellDuration.getFloat();
            canCallEmergency = CustomOptionHolder.sorcererCanCallEmergency.getBool();
        }
    }

    public static class Renegade
    {
        public static PlayerControl renegade;
        public static Color color = new Color32(79, 125, 0, byte.MaxValue);
        public static PlayerControl fakeMinion;
        public static PlayerControl currentTarget;
        public static List<PlayerControl> formerRenegades = new List<PlayerControl>();

        public static float cooldown = 30f;
        public static float createMinionCooldown = 30f;
        public static bool canUseVents = true;
        public static bool canRecruitMinion = true;
        public static Sprite buttonSprite;
        public static bool usedRecruit = false;

        public static Sprite getMinionButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.RenegadeRecruitButton.png", 90f);
            return buttonSprite;
        }

        public static void removeCurrentRenegade() {
            if (!formerRenegades.Any(x => x.PlayerId == renegade.PlayerId)) formerRenegades.Add(renegade);
            renegade = null;
            currentTarget = null;
            fakeMinion = null;
            cooldown = CustomOptionHolder.renegadeKillCooldown.getFloat();
            createMinionCooldown = CustomOptionHolder.renegadeCreateMinionCooldown.getFloat();
        }

        public static void clearAndReload() {
            renegade = null;
            currentTarget = null;
            fakeMinion = null;
            cooldown = CustomOptionHolder.renegadeKillCooldown.getFloat();
            createMinionCooldown = CustomOptionHolder.renegadeCreateMinionCooldown.getFloat();
            canUseVents = CustomOptionHolder.renegadeCanUseVents.getBool();
            canRecruitMinion = CustomOptionHolder.renegadeCanRecruitMinion.getBool();
            usedRecruit = false;
            formerRenegades.Clear();
        }

    }

    public static class Minion
    {
        public static PlayerControl minion;
        public static Color color = new Color32(79, 125, 0, byte.MaxValue);

        public static PlayerControl currentTarget;

        public static float cooldown = 30f;

        public static void clearAndReload() {
            minion = null;
            currentTarget = null;
            cooldown = CustomOptionHolder.renegadeKillCooldown.getFloat();
        }
    }

    public static class BountyHunter
    {
        public static PlayerControl bountyhunter;
        public static Color color = new Color32(79, 125, 0, byte.MaxValue);

        public static List<PlayerControl> possibleTargets = new List<PlayerControl>();

        public static float cooldown = 30f;

        public static PlayerControl currentTarget;

        public static PlayerControl hasToKill;

        public static string rolName;

        public static bool triggerBountyHunterWin = false;

        public static bool usedTarget;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.BountyHunterSetTargetButton.png", 90f);
            return buttonSprite;
        }

        public static void clearAndReload() {
            bountyhunter = null;
            currentTarget = null;
            hasToKill = null;
            cooldown = CustomOptionHolder.bountyHunterCooldown.getFloat();
            triggerBountyHunterWin = false;
            rolName = "";
            RoleInfo.bountyHunter.shortDescription = "Hunt down your target" + rolName;
            usedTarget = false;
            possibleTargets = new List<PlayerControl>();
        }
    }

    public static class Trapper
    {
        public static PlayerControl trapper;
        public static Color color = new Color32(79, 125, 0, byte.MaxValue);

        public static float cooldown = 30f;

        public static int numberOfMines;

        public static int numberOfTraps;

        public static float durationOfMines;

        public static float durationOfTraps;

        public static int currentMineNumber = 0;

        public static int currentTrapNumber = 0;

        public static PlayerControl mined;

        public static PlayerControl currentTarget;

        private static Sprite buttonTrapSprite;
        public static Sprite getTrapButtonSprite() {
            if (buttonTrapSprite) return buttonTrapSprite;
            buttonTrapSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.TrapperTrapButton.png", 90f);
            return buttonTrapSprite;
        }
        
        private static Sprite buttonMineSprite;
        public static Sprite getMineButtonSprite() {
            if (buttonMineSprite) return buttonMineSprite;
            buttonMineSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.TrapperMineButton.png", 90f);
            return buttonMineSprite;
        }

        public static void clearAndReload() {
            trapper = null;
            cooldown = CustomOptionHolder.trapperCooldown.getFloat();
            numberOfMines = (int)CustomOptionHolder.trapperMineNumber.getFloat();
            numberOfTraps = (int)CustomOptionHolder.trapperTrapNumber.getFloat();
            durationOfMines = CustomOptionHolder.trapperMineDuration.getFloat();
            durationOfTraps = CustomOptionHolder.trapperTrapDuration.getFloat();
            currentMineNumber = 0;
            currentTrapNumber = 0;
            mined = null;
            currentTarget = null;
        }
    }

    public static class Yinyanger
    {
        public static PlayerControl yinyanger;
        public static Color color = new Color32(79, 125, 0, byte.MaxValue);

        public static float cooldown = 30f;

        public static PlayerControl currentTarget;

        public static PlayerControl yinyedplayer;

        public static PlayerControl yangyedplayer;

        public static bool usedYined = false;

        public static bool usedYanged = false;

        public static bool colision = false;

        public static PlayerControl yinedPlayer;

        public static PlayerControl yanedPlayer;

        private static Sprite buttonSpriteYang;
        public static Sprite getYangButtonSprite() {
            if (buttonSpriteYang) return buttonSpriteYang;
            buttonSpriteYang = Helpers.loadSpriteFromResources("LasMonjas.Images.YinyangerYangButton.png", 90f);
            return buttonSpriteYang;
        }

        private static Sprite buttonSpriteYing;
        public static Sprite getYinButtonSprite() {
            if (buttonSpriteYing) return buttonSpriteYing;
            buttonSpriteYing = Helpers.loadSpriteFromResources("LasMonjas.Images.YinyangerYinButton.png", 90f);
            return buttonSpriteYing;
        }

        public static void clearAndReload() {
            yinyanger = null;
            cooldown = CustomOptionHolder.yinyangerCooldown.getFloat();
            currentTarget = null;
            yinyedplayer = null;
            yangyedplayer = null;
            usedYined = false;
            usedYanged = false;
            colision = false;
            yinedPlayer = null;
            yanedPlayer = null;
        }
        public static void resetYined() {
            yinyedplayer = null;
            usedYined = false;
            yinedPlayer = null;
        }
        public static void resetYanged() {
            yangyedplayer = null;
            usedYanged = false;
            yanedPlayer = null;
        }
    }

    public static class Challenger
    {
        public static PlayerControl challenger;
        public static Color color = new Color32(79, 125, 0, byte.MaxValue);

        public static float cooldown = 30f;

        public static float duration = 10f;

        public static PlayerControl currentTarget;

        public static bool challengerRock = false;

        public static bool challengerPaper = false;

        public static bool challengerScissors = false;

        public static PlayerControl rivalPlayer;

        public static bool rivalRock = false;

        public static bool rivalPaper = false;

        public static bool rivalScissors = false;

        public static bool isDueling = false;

        public static bool onlyOneFinishDuel = true;

        public static float duelDuration = 30f;

        public static bool timeOutDuel = false;

        public static bool challengerIsInMeeting = false;

        private static Sprite buttonChallengeSprite;
        public static Sprite getChallengeButtonSprite() {
            if (buttonChallengeSprite) return buttonChallengeSprite;
            buttonChallengeSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.ChallengerChallengeButton.png", 90f);
            return buttonChallengeSprite;
        }

        private static Sprite buttonRockSprite;
        public static Sprite getRockButtonSprite() {
            if (buttonRockSprite) return buttonRockSprite;
            buttonRockSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.ChallengerRockButton.png", 90f);
            return buttonRockSprite;
        }

        private static Sprite buttonPaperSprite;
        public static Sprite getPaperButtonSprite() {
            if (buttonPaperSprite) return buttonPaperSprite;
            buttonPaperSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.ChallengerPaperButton.png", 90f);
            return buttonPaperSprite;
        }

        private static Sprite buttonScissorsSprite;
        public static Sprite getScissorsButtonSprite() {
            if (buttonScissorsSprite) return buttonScissorsSprite;
            buttonScissorsSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.ChallengerScissorsButton.png", 90f);
            return buttonScissorsSprite;
        }

        public static void clearAndReload() {
            challenger = null;
            cooldown = CustomOptionHolder.challengerCooldown.getFloat();
            duration = 10f;
            currentTarget = null;
            challengerRock = false;
            challengerPaper = false;
            challengerScissors = false;
            rivalPlayer = null;
            rivalRock = false;
            rivalPaper = false;
            rivalScissors = false;
            isDueling = false;
            onlyOneFinishDuel = true;
            duelDuration = 30f;
            timeOutDuel = false;
            challengerIsInMeeting = false;
        }

        public static void ResetValues() {
            challengerRock = false;
            challengerPaper = false;
            challengerScissors = false;
            rivalPlayer = null;
            rivalRock = false;
            rivalPaper = false;
            rivalScissors = false;
            isDueling = false;
            onlyOneFinishDuel = true;
            duelDuration = 30f;
            challengerIsInMeeting = false;
        }
    }

    public static class Joker
    {
        public static PlayerControl joker;
        public static Color color = new Color32(128, 128, 128, byte.MaxValue);

        public static bool triggerJokerWin = false;
        public static bool canSabotage = true;

        public static void clearAndReload() {
            joker = null;
            triggerJokerWin = false;
            canSabotage = CustomOptionHolder.jokerCanSabotage.getBool();
        }
    }

    public static class RoleThief
    {
        public static PlayerControl rolethief;
        public static Color color = new Color32(128, 128, 128, byte.MaxValue);

        public static float cooldown = float.MaxValue;

        public static PlayerControl currentTarget;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.RoleThiefStealButton.png", 90f);
            return buttonSprite;
        }

        public static void clearAndReload() {
            rolethief = null;
            currentTarget = null;
            cooldown = CustomOptionHolder.rolethiefCooldown.getFloat();
        }
    }

    public static class Pyromaniac
    {
        public static PlayerControl pyromaniac;
        public static Color color = new Color32(128, 128, 128, byte.MaxValue);

        public static float cooldown = 30f;
        public static float duration = 3f;
        public static bool triggerPyromaniacWin = false;

        public static PlayerControl currentTarget;
        public static PlayerControl sprayTarget;
        public static List<PlayerControl> sprayedPlayers = new List<PlayerControl>();

        private static Sprite spraySprite;
        public static Sprite getSpraySprite() {
            if (spraySprite) return spraySprite;
            spraySprite = Helpers.loadSpriteFromResources("LasMonjas.Images.PyromaniacSprayButton.png", 90f);
            return spraySprite;
        }

        private static Sprite igniteSprite;
        public static Sprite getIgniteSprite() {
            if (igniteSprite) return igniteSprite;
            igniteSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.PyromaniacIgniteButton.png", 90f);
            return igniteSprite;
        }

        public static bool sprayedEveryoneAlive() {
            return PlayerControl.AllPlayerControls.ToArray().All(x => { return x == Pyromaniac.pyromaniac || x.Data.IsDead || x.Data.Disconnected || Pyromaniac.sprayedPlayers.Any(y => y.PlayerId == x.PlayerId); });
        }

        public static void clearAndReload() {
            pyromaniac = null;
            currentTarget = null;
            sprayTarget = null;
            triggerPyromaniacWin = false;
            sprayedPlayers = new List<PlayerControl>();
            foreach (PoolablePlayer p in MapOptions.playerIcons.Values) {
                if (p != null && p.gameObject != null) p.gameObject.SetActive(false);
            }
            cooldown = CustomOptionHolder.pyromaniacCooldown.getFloat();
            duration = CustomOptionHolder.pyromaniacDuration.getFloat();
        }
    }

    public static class TreasureHunter
    {
        public static PlayerControl treasureHunter;
        public static Color color = new Color32(128, 128, 128, byte.MaxValue);

        public static bool triggertreasureHunterWin = false;
        public static bool canCallEmergency = true;
        public static float cooldown = 30f;
        public static bool canPlace = true;
        public static int treasureCollected = 0;
        public static float neededTreasure = 3;
        public static int randomSpawn = 0;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.TreasureHunterPlaceChestButton.png", 90f);
            return buttonSprite;
        }

        public static void clearAndReload() {
            treasureHunter = null;
            triggertreasureHunterWin = false;
            cooldown = CustomOptionHolder.treasureHunterCooldown.getFloat();
            canCallEmergency = CustomOptionHolder.treasureHunterCanCallEmergency.getBool();
            canPlace = true;
            treasureCollected = 0;
            neededTreasure = CustomOptionHolder.treasureHunterTreasureNumber.getFloat();
            randomSpawn = 0;
        }
    }

    public static class Devourer
    {
        public static PlayerControl devourer;
        public static Color color = new Color32(128, 128, 128, byte.MaxValue);

        public static bool triggerdevourerWin = false;
        public static float cooldown = 30f;
        public static int devouredBodies = 0;
        public static float neededBodies = 3;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.DevourerDevourButton.png", 90f);
            return buttonSprite;
        }

        public static void clearAndReload() {
            devourer = null;
            triggerdevourerWin = false;
            cooldown = CustomOptionHolder.devourerCooldown.getFloat();
            devouredBodies = 0;
            neededBodies = CustomOptionHolder.devourerBodiesNumber.getFloat();
        }
    }


    public static class Captain
    {

        public static PlayerControl captain;
        public static Color color = new Color32(94, 62, 125, byte.MaxValue);

        private static Sprite buttonSprite;

        public static Sprite getCallButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.CaptainMeetingButton.png", 90f);
            return buttonSprite;
        }
        public static void clearAndReload() {
            captain = null;
        }
    }

    public static class Mechanic
    {
        public static PlayerControl mechanic;
        public static Color color = new Color32(127, 76, 0, byte.MaxValue);
        public static bool usedRepair;
        public static int numberOfRepairs;
        public static int timesUsedRepairs;
        public static TMPro.TMP_Text mechanicRepairButtonText;
        private static Sprite buttonSprite;

        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.MechanicRepairButton.png", 90f);
            return buttonSprite;
        }

        public static void clearAndReload() {
            mechanic = null;
            usedRepair = false;
            timesUsedRepairs = 0;
            numberOfRepairs = (int)CustomOptionHolder.mechanicNumberOfRepairs.getFloat();
        }
    }

    public static class Sheriff
    {
        public static PlayerControl sheriff;
        public static Color color = new Color32(255, 255, 0, byte.MaxValue);

        public static float cooldown = 30f;
        public static bool canKillNeutrals = false;

        public static PlayerControl currentTarget;

        public static void clearAndReload() {
            sheriff = null;
            currentTarget = null;
            cooldown = CustomOptionHolder.sheriffCooldown.getFloat();
            canKillNeutrals = CustomOptionHolder.sheriffCanKillNeutrals.getBool();
        }
    }
   
    public static class Detective
    {
        public static PlayerControl detective;
        public static Color color = new Color32(160, 50, 177, byte.MaxValue);

        public static float footprintIntervall = 1f;
        public static float footprintDuration = 1f;
        public static bool anonymousFootprints = false;
        public static float timer = 1f;

        public static int footprintcolor = 6;

        public static void clearAndReload() {
            detective = null;
            anonymousFootprints = CustomOptionHolder.detectiveAnonymousFootprints.getBool();
            footprintIntervall = CustomOptionHolder.detectiveFootprintIntervall.getFloat();
            footprintDuration = CustomOptionHolder.detectiveFootprintDuration.getFloat();
            timer = footprintIntervall;
            footprintcolor = 6;
        }
    }

    public static class Forensic
    {
        public static PlayerControl forensic;
        public static Color color = new Color32(78, 97, 255, byte.MaxValue);

        public static float reportNameDuration = 0f;
        public static float reportColorDuration = 20f;
        public static float reportClueDuration = 40f;

        public static DeadPlayer target;
        public static DeadPlayer soulTarget;
        public static List<Tuple<DeadPlayer, Vector3>> deadBodies = new List<Tuple<DeadPlayer, Vector3>>();
        public static List<Tuple<DeadPlayer, Vector3>> featureDeadBodies = new List<Tuple<DeadPlayer, Vector3>>();
        public static List<SpriteRenderer> souls = new List<SpriteRenderer>();
        public static DateTime meetingStartTime = DateTime.UtcNow;
        public static float cooldown = 30f;
        public static float duration = 10f;
        public static bool oneTimeUse = false;

        private static Sprite soulSprite;
        public static Sprite getSoulSprite() {
            if (soulSprite) return soulSprite;
            soulSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.ForensicSoul.png", 500f);
            return soulSprite;
        }

        private static Sprite question;
        public static Sprite getQuestionSprite() {
            if (question) return question;
            question = Helpers.loadSpriteFromResources("LasMonjas.Images.ForensicAskButton.png", 90f);
            return question;
        }


        public static void clearAndReload() {
            forensic = null;
            reportNameDuration = CustomOptionHolder.forensicReportNameDuration.getFloat();
            reportColorDuration = CustomOptionHolder.forensicReportColorDuration.getFloat();
            reportClueDuration = CustomOptionHolder.forensicReportClueDuration.getFloat();
            target = null;
            soulTarget = null;
            deadBodies = new List<Tuple<DeadPlayer, Vector3>>();
            featureDeadBodies = new List<Tuple<DeadPlayer, Vector3>>();
            souls = new List<SpriteRenderer>();
            meetingStartTime = DateTime.UtcNow;
            cooldown = CustomOptionHolder.forensicCooldown.getFloat();
            duration = CustomOptionHolder.forensicDuration.getFloat();
            oneTimeUse = CustomOptionHolder.forensicOneTimeUse.getBool();
        }
    }

    public static class TimeTraveler
    {
        public static PlayerControl timeTraveler;
        public static Color color = new Color32(0, 189, 255, byte.MaxValue);

        public static bool reviveDuringRewind = false;
        public static float rewindTime = 3f;
        public static float shieldDuration = 3f;
        public static float cooldown = 30f;
        public static float backUpduration = 10f;

        public static bool shieldActive = false;
        public static bool isRewinding = false;

        public static bool usedRewind;
        public static bool usedShield;

        private static Sprite shieldbuttonSprite;
        private static Sprite rewindbuttonSprite;
        public static Sprite getShieldButtonSprite() {
            if (shieldbuttonSprite) return shieldbuttonSprite;
            shieldbuttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.TimeTravelerTimeShieldButton.png", 90f);
            return shieldbuttonSprite;
        }
        public static Sprite getRewindButtonSprite() {
            if (rewindbuttonSprite) return rewindbuttonSprite;
            rewindbuttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.TimeTravelerRewindButton.png", 90f);
            return rewindbuttonSprite;
        }

        public static void clearAndReload() {
            timeTraveler = null;
            isRewinding = false;
            shieldActive = false;
            rewindTime = CustomOptionHolder.timeTravelerRewindTime.getFloat();
            shieldDuration = CustomOptionHolder.timeTravelerShieldDuration.getFloat();
            cooldown = CustomOptionHolder.timeTravelerCooldown.getFloat();
            usedRewind = false;
            reviveDuringRewind = CustomOptionHolder.timeTravelerReviveDuringRewind.getBool();
            usedShield = false;
            backUpduration = shieldDuration;
        }
    }

    public static class Squire
    {
        public static PlayerControl squire;
        public static PlayerControl shielded;
        public static Color color = new Color32(0, 255, 0, byte.MaxValue);
        public static bool usedShield;

        public static int showShielded = 0;
        public static bool showAttemptToShielded = false;
        public static bool resetShieldAfterMeeting = false;

        public static Color shieldedColor = new Color32(0, 255, 255, byte.MaxValue);
        public static PlayerControl currentTarget;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.SquireShieldButton.png", 90f);
            return buttonSprite;
        }

        public static void resetShield() {
            shielded = null;
            usedShield = false;
        }

        public static void clearAndReload() {
            squire = null;
            shielded = null;
            currentTarget = null;
            usedShield = false;
            showShielded = CustomOptionHolder.squireShowShielded.getSelection();
            showAttemptToShielded = CustomOptionHolder.squireShowAttemptToShielded.getBool();
            resetShieldAfterMeeting = CustomOptionHolder.squireResetShieldAfterMeeting.getBool();
        }
    }

    public static class Cheater
    {
        public static PlayerControl cheater;
        public static Color color = new Color32(240, 128, 72, byte.MaxValue);
        private static Sprite spriteCheck;
        public static bool canCallEmergency = false;
        public static bool canOnlyCheatOthers = false;

        public static byte playerId1 = Byte.MaxValue;
        public static byte playerId2 = Byte.MaxValue;

        public static PlayerControl cheatedP1;
        public static PlayerControl cheatedP2;
        public static bool usedCheat;
        public static Sprite getCheckSprite() {
            if (spriteCheck) return spriteCheck;
            spriteCheck = Helpers.loadSpriteFromResources("LasMonjas.Images.CheaterCheck.png", 150f);
            return spriteCheck;
        }

        public static void clearAndReload() {
            cheater = null;
            playerId1 = Byte.MaxValue;
            playerId2 = Byte.MaxValue;
            cheatedP1 = null;
            cheatedP2 = null;
            usedCheat = false;
            canCallEmergency = CustomOptionHolder.cheaterCanCallEmergency.getBool();
            canOnlyCheatOthers = CustomOptionHolder.cheatercanOnlyCheatOthers.getBool();
        }
    }

    public static class FortuneTeller
    {
        public static PlayerControl fortuneTeller;
        public static Color color = new Color32(0, 198, 66, byte.MaxValue);

        public static List<PlayerControl> revealedPlayers = new List<PlayerControl>();
        public static float cooldown = float.MaxValue;
        public static int kindOfInfo = 0;
        public static int playersWithNotification = 0;

        public static PlayerControl currentTarget;
        public static PlayerControl revealTarget;
        public static float duration = 3;

        public static bool usedFortune;
        public static int numberOfFortunes;
        public static int timesUsedFortune;
        public static TMPro.TMP_Text fortuneTellerRevealButtonText;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.FortuneTellerRevealButton.png", 90f);
            return buttonSprite;
        }

        public static void clearAndReload() {
            fortuneTeller = null;
            revealedPlayers = new List<PlayerControl>();
            cooldown = CustomOptionHolder.fortuneTellerCooldown.getFloat();
            duration = CustomOptionHolder.fortuneTellerDuration.getFloat();
            kindOfInfo = CustomOptionHolder.fortuneTellerKindOfInfo.getSelection();
            playersWithNotification = CustomOptionHolder.fortuneTellerPlayersWithNotification.getSelection();
            usedFortune = false;
            timesUsedFortune = 0;
            numberOfFortunes = (int)CustomOptionHolder.fortuneTellerNumberOfSee.getFloat();
            currentTarget = null;
            revealTarget = null;
        }
    }

    public static class Hacker
    {
        public static PlayerControl hacker;
        public static Color color = new Color32(83, 131, 219, byte.MaxValue);

        public static float cooldown = 30f;
        public static float duration = 10f;
        public static float hackerTimer = 0f;
        public static float backUpduration = 10f;

        public static TMPro.TMP_Text hackerAdminTableChargesText;
        public static TMPro.TMP_Text hackerVitalsChargesText;

        public static Minigame vitals = null;
        public static float toolsNumber = 5f;
        public static int rechargeTasksNumber = 2;
        public static int rechargedTasks = 2;
        public static int chargesVitals = 1;
        public static int chargesAdminTable = 1;
        private static Sprite vitalsSprite;
        private static Sprite adminSprite;

        public static Sprite getVitalsSprite() {
            if (vitalsSprite) return vitalsSprite;
            vitalsSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.HackerVitalsButton.png", 90f);
            return vitalsSprite;
        }

        public static Sprite getAdminSprite() {
            if (adminSprite) return adminSprite;
            adminSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.HackerAdminButton.png", 90f);
            return adminSprite;
        }

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.HackerInfoButton.png", 90f);
            return buttonSprite;
        }

        public static void clearAndReload() {
            hacker = null;
            hackerTimer = 0f;
            cooldown = CustomOptionHolder.hackerCooldown.getFloat();
            duration = CustomOptionHolder.hackerHackeringDuration.getFloat();
            vitals = null;
            adminSprite = null;
            toolsNumber = CustomOptionHolder.hackerToolsNumber.getFloat();
            rechargeTasksNumber = Mathf.RoundToInt(CustomOptionHolder.hackerRechargeTasksNumber.getFloat());
            rechargedTasks = Mathf.RoundToInt(CustomOptionHolder.hackerRechargeTasksNumber.getFloat());
            chargesVitals = Mathf.RoundToInt(CustomOptionHolder.hackerToolsNumber.getFloat()) / 2;
            chargesAdminTable = Mathf.RoundToInt(CustomOptionHolder.hackerToolsNumber.getFloat()) / 2;
            backUpduration = duration;
        }
    }

    public static class Sleuth
    {
        public static PlayerControl sleuth;
        public static Color color = new Color32(0, 159, 87, byte.MaxValue);
        public static List<Arrow> localArrows = new List<Arrow>();

        public static float updateIntervall = 5f;
        public static bool resetTargetAfterMeeting = false;
        public static float corpsesPathfindCooldown = 30f;
        public static float corpsesPathfindDuration = 5f;
        public static float corpsesPathfindTimer = 0f;
        public static List<Vector3> deadBodyPositions = new List<Vector3>();

        public static float backUpduration = 10f;

        public static PlayerControl currentTarget;
        public static PlayerControl located;
        public static bool usedLocate = false;
        public static float timeUntilUpdate = 0f;
        public static Arrow arrow = new Arrow(Color.blue);

        private static Sprite corpsePathfindButtonSprite;
        public static Sprite getCorpsePathfindButtonSprite() {
            if (corpsePathfindButtonSprite) return corpsePathfindButtonSprite;
            corpsePathfindButtonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.SleuthPathfindButton.png", 90f);
            return corpsePathfindButtonSprite;
        }

        private static Sprite locateButtonSprite;
        public static Sprite getLocateButtonSprite() {
            if (locateButtonSprite) return locateButtonSprite;
            locateButtonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.SleuthLocateButton.png", 90f);
            return locateButtonSprite;
        }

        public static void resetLocated() {
            currentTarget = located = null;
            usedLocate = false;
            if (arrow?.arrow != null) UnityEngine.Object.Destroy(arrow.arrow);
            arrow = new Arrow(Color.blue);
            if (arrow.arrow != null) arrow.arrow.SetActive(false);
        }

        public static void clearAndReload() {
            sleuth = null;
            resetLocated();
            timeUntilUpdate = 0f;
            updateIntervall = CustomOptionHolder.sleuthUpdateIntervall.getFloat();
            resetTargetAfterMeeting = CustomOptionHolder.sleuthResetTargetAfterMeeting.getBool();
            if (localArrows != null) {
                foreach (Arrow arrow in localArrows)
                    if (arrow?.arrow != null)
                        UnityEngine.Object.Destroy(arrow.arrow);
            }
            deadBodyPositions = new List<Vector3>();
            corpsesPathfindTimer = 0f;
            corpsesPathfindCooldown = CustomOptionHolder.sleuthCorpsesPathfindCooldown.getFloat();
            corpsesPathfindDuration = CustomOptionHolder.sleuthCorpsesPathfindDuration.getFloat();
            backUpduration = corpsesPathfindDuration;
        }
    }  

    public static class Fink
    {
        public static PlayerControl fink;
        public static Color color = new Color32(184, 0, 50, byte.MaxValue);

        public static List<Arrow> localArrows = new List<Arrow>();
        public static int taskCountForImpostors = 1;
        public static bool includeTeamRenegade = false;

        public static void clearAndReload() {
            if (localArrows != null) {
                foreach (Arrow arrow in localArrows)
                    if (arrow?.arrow != null)
                        UnityEngine.Object.Destroy(arrow.arrow);
            }
            localArrows = new List<Arrow>();
            taskCountForImpostors = Mathf.RoundToInt(CustomOptionHolder.finkLeftTasksForImpostors.getFloat());
            includeTeamRenegade = CustomOptionHolder.finkIncludeTeamRenegade.getBool();
            fink = null;
        }
    }

    public static class Kid
    {
        public static PlayerControl kid;
        public static Color color = new Color32(141, 255, 255, byte.MaxValue);
        public static bool triggerKidLose = false;

        public static void clearAndReload() {
            kid = null;
            triggerKidLose = false;
        }
    }

    public static class Welder
    {
        public static PlayerControl welder;
        public static Color color = new Color32(109, 91, 47, byte.MaxValue);

        public static float cooldown = 30f;
        public static int remainingWelds = 5;
        public static int totalWelds = 5;
        public static TMPro.TMP_Text welderButtonText;
        public static Vent ventTarget = null;
        public static List<Vent> ventsSealed = new List<Vent>();

        private static Sprite closeVentButtonSprite;
        public static Sprite getCloseVentButtonSprite() {
            if (closeVentButtonSprite) return closeVentButtonSprite;
            closeVentButtonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.WelderCloseVentButton.png", 90f);
            return closeVentButtonSprite;
        }

        private static Sprite animatedVentSealedSprite;
        public static Sprite getAnimatedVentSealedSprite() {
            if (animatedVentSealedSprite) return animatedVentSealedSprite;
            animatedVentSealedSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.WelderAnimatedVentSealed.png", 160f); // Change sprite and pixelPerUnit
            return animatedVentSealedSprite;
        }

        private static Sprite staticVentSealedSprite;
        public static Sprite getStaticVentSealedSprite() {
            if (staticVentSealedSprite) return staticVentSealedSprite;
            staticVentSealedSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.WelderStaticVentSealed.png", 160f); // Change sprite and pixelPerUnit
            return staticVentSealedSprite;
        }

        public static void clearAndReload() {
            welder = null;
            ventTarget = null;
            cooldown = CustomOptionHolder.welderCooldown.getFloat();
            totalWelds = remainingWelds = Mathf.RoundToInt(CustomOptionHolder.welderTotalWelds.getFloat());
            ventsSealed.Clear();
            ventsSealed = new List<Vent>();
        }
    }

    public static class Spiritualist
    {
        public static PlayerControl spiritualist;
        public static Color color = new Color32(255, 197, 225, byte.MaxValue);
        public static bool usedRevive;
        public static float spiritualistReviveTime = 0f;
        private static Sprite buttonSprite;

        public static bool isReviving = false;
        public static bool canRevive = false;
        public static PlayerControl revivedPlayer = null;
        public static bool preventReport = false;

        public static List<Arrow> localSpiritArrows = new List<Arrow>();

        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.SpiritualistReviveButton.png", 90f);
            return buttonSprite;
        }

        public static void clearAndReload() {
            spiritualist = null;
            usedRevive = false;
            spiritualistReviveTime = CustomOptionHolder.spiritualistReviveTime.getFloat();
            isReviving = false;
            canRevive = false;
            localSpiritArrows = new List<Arrow>();
            revivedPlayer = null;
            preventReport = false;
        }
    }

    public static class TheChosenOne
    {
        public static PlayerControl theChosenOne;
        public static Color color = new Color32(0, 247, 255, byte.MaxValue);

        public static float reportDelay = 0f;

        public static bool reported = false;

        public static void clearAndReload() {
            theChosenOne = null;
            reported = false;
            reportDelay = CustomOptionHolder.theChosenOneReportDelay.getFloat();
        }
    }

    public static class Vigilant
    {
        public static PlayerControl vigilant;
        public static PlayerControl vigilantMira;
        public static bool doorLogActivated = true;

        public static Color color = new Color32(227, 225, 90, byte.MaxValue);

        public static float cooldown = 30f;
        public static TMPro.TMP_Text vigilantButtonCameraText;
        public static int totalCameras = 4;
        public static int remainingCameras = 0;
        public static int placedCameras = 0;

        public static bool createdDoorLog = false;
        public static GameObject doorLog = null;

        public static TMPro.TMP_Text vigilantButtonCameraUsesText;
        public static float duration = 10f;
        public static int maxCharges = 5;
        public static int rechargeTasksNumber = 3;
        public static int rechargedTasks = 3;
        public static int charges = 1;
        public static Minigame minigame = null;
        public static float backUpduration = 10f;

        private static Sprite camSprite;
        public static Sprite getCamSprite() {
            if (camSprite) return camSprite;
            camSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.VigilantViewCameraButton.png", 90f);
            return camSprite;
        }
        
        
        private static Sprite placeCameraButtonSprite;
        public static Sprite getPlaceCameraButtonSprite() {
            if (placeCameraButtonSprite) return placeCameraButtonSprite;
            placeCameraButtonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.VigilantCameraButton.png", 90f);
            return placeCameraButtonSprite;
        }

        public static void clearAndReload() {
            vigilant = null;
            vigilantMira = null;
            cooldown = CustomOptionHolder.vigilantCooldown.getFloat();
            totalCameras = 4;
            remainingCameras = totalCameras;
            placedCameras = 0;
            createdDoorLog = false;
            doorLog = null;
            doorLogActivated = true;

            minigame = null;
            duration = CustomOptionHolder.vigilantCamDuration.getFloat();
            maxCharges = Mathf.RoundToInt(CustomOptionHolder.vigilantCamMaxCharges.getFloat());
            rechargeTasksNumber = Mathf.RoundToInt(CustomOptionHolder.vigilantCamRechargeTasksNumber.getFloat());
            rechargedTasks = Mathf.RoundToInt(CustomOptionHolder.vigilantCamRechargeTasksNumber.getFloat());
            charges = Mathf.RoundToInt(CustomOptionHolder.vigilantCamMaxCharges.getFloat()) / 2;
            backUpduration = duration;
        }
    }

    public static class Performer
    {
        public static PlayerControl performer;
        public static Color color = new Color32(242, 190, 255, byte.MaxValue);

        public static float duration = 15f;

        public static List<Arrow> localPerformerArrows = new List<Arrow>();
        public static bool reported = false;
        public static bool musicStop = false;

        public static void clearAndReload() {
            performer = null;
            duration = CustomOptionHolder.performerDuration.getFloat();
            localPerformerArrows = new List<Arrow>();
            reported = false;
            musicStop = false;
        }
    }

    public static class Hunter
    {
        public static PlayerControl hunter;
        public static Color color = new Color32(225, 235, 144, byte.MaxValue);

        public static bool resetTargetAfterMeeting = false;

        public static PlayerControl currentTarget;
        public static PlayerControl hunted;
        public static bool usedHunted = false;

        private static Sprite buttonSprite;
        public static Sprite getButtonSprite() {
            if (buttonSprite) return buttonSprite;
            buttonSprite = Helpers.loadSpriteFromResources("LasMonjas.Images.HunterSetMarkButton.png", 90f);
            return buttonSprite;
        }

        public static void resetHunted() {
            currentTarget = hunted = null;
            usedHunted = false;
        }

        public static void clearAndReload() {
            hunter = null;
            resetHunted();
            resetTargetAfterMeeting = CustomOptionHolder.hunterResetTargetAfterMeeting.getBool();
        }
    }

    public static class Jinx
    {
        public static PlayerControl jinx;
        public static PlayerControl target;
        public static Color color = new Color32(146, 139, 85, byte.MaxValue);
        public static List<PlayerControl> jinxedList = new List<PlayerControl>();
        public static int jinxs = 0;
        public static Sprite jinxButton;

        public static float cooldown = 30f;
        public static int jinxNumber = 5;

        public static TMPro.TMP_Text jinxButtonJinxsText;


        public static Sprite getTargetSprite() {
            if (jinxButton) return jinxButton;
            jinxButton = Helpers.loadSpriteFromResources("LasMonjas.Images.JinxButton.png", 90f);
            return jinxButton;
        }

        public static void clearAndReload() {
            jinx = null;
            target = null;
            jinxedList = new List<PlayerControl>();
            jinxs = 0;

            cooldown = CustomOptionHolder.jinxCooldown.getFloat();
            jinxNumber = Mathf.RoundToInt(CustomOptionHolder.jinxJinxsNumber.getFloat());
        }
    }   

    public static class Modifiers
    {
        public static Color color = new Color32(240, 128, 72, byte.MaxValue);
        public static Color loverscolor = new Color32(255, 0, 209, byte.MaxValue);

        public static PlayerControl lover1;
        public static PlayerControl lover2;
        public static PlayerControl lighter;
        public static PlayerControl blind;
        public static PlayerControl flash;
        public static PlayerControl bigchungus;

        // Lovers save if next to be exiled is a lover, because RPC of ending game comes before RPC of exiled
        public static bool notAckedExiledIsLover = false;

        public static bool existing() {
            return lover1 != null && lover2 != null && !lover1.Data.Disconnected && !lover2.Data.Disconnected;
        }

        public static bool existingAndAlive() {
            return existing() && !lover1.Data.IsDead && !lover2.Data.IsDead && !notAckedExiledIsLover; // ADD NOT ACKED IS LOVER
        }

        public static bool existingWithKiller() {
            return existing() && (lover1 == Renegade.renegade || lover2 == Renegade.renegade
                               || lover1 == Minion.minion || lover2 == Minion.minion
                               || lover1.Data.Role.IsImpostor || lover2.Data.Role.IsImpostor);
        }

        public static bool hasAliveKillingLover(this PlayerControl player) {
            if (!Modifiers.existingAndAlive() || !existingWithKiller())
                return false;
            return (player != null && (player == lover1 || player == lover2));
        }

        public static void clearAndReload() {
            lover1 = null;
            lover2 = null;
            notAckedExiledIsLover = false;
            blind = null;
            flash = null;
            bigchungus = null;
        }

        public static void ClearLovers() {
            lover1 = null;
            lover2 = null;
            notAckedExiledIsLover = false;
        }

        public static PlayerControl getPartner(this PlayerControl player) {
            if (player == null)
                return null;
            if (lover1 == player)
                return lover2;
            if (lover2 == player)
                return lover1;
            return null;
        }

    }

    public static class CaptureTheFlag
    {
        public static List<PlayerControl> redteamFlag = new List<PlayerControl>();
        public static PlayerControl redplayer01 = null;
        public static bool redplayer01IsReviving = false;
        public static PlayerControl redplayer01currentTarget = null;
        public static PlayerControl redplayer02 = null;
        public static bool redplayer02IsReviving = false;
        public static PlayerControl redplayer02currentTarget = null;
        public static PlayerControl redplayer03 = null;
        public static bool redplayer03IsReviving = false;
        public static PlayerControl redplayer03currentTarget = null;
        public static PlayerControl redplayer04 = null;
        public static bool redplayer04IsReviving = false;
        public static PlayerControl redplayer04currentTarget = null;
        public static PlayerControl redplayer05 = null;
        public static bool redplayer05IsReviving = false;
        public static PlayerControl redplayer05currentTarget = null;
        public static PlayerControl redplayer06 = null;
        public static bool redplayer06IsReviving = false;
        public static PlayerControl redplayer06currentTarget = null;
        public static PlayerControl redplayer07 = null;
        public static bool redplayer07IsReviving = false;
        public static PlayerControl redplayer07currentTarget = null;

        public static List<PlayerControl> blueteamFlag = new List<PlayerControl>();
        public static PlayerControl blueplayer01 = null;
        public static bool blueplayer01IsReviving = false;
        public static PlayerControl blueplayer01currentTarget = null;
        public static PlayerControl blueplayer02 = null;
        public static bool blueplayer02IsReviving = false;
        public static PlayerControl blueplayer02currentTarget = null;
        public static PlayerControl blueplayer03 = null;
        public static bool blueplayer03IsReviving = false;
        public static PlayerControl blueplayer03currentTarget = null;
        public static PlayerControl blueplayer04 = null;
        public static bool blueplayer04IsReviving = false;
        public static PlayerControl blueplayer04currentTarget = null;
        public static PlayerControl blueplayer05 = null;
        public static bool blueplayer05IsReviving = false;
        public static PlayerControl blueplayer05currentTarget = null;
        public static PlayerControl blueplayer06 = null;
        public static bool blueplayer06IsReviving = false;
        public static PlayerControl blueplayer06currentTarget = null;
        public static PlayerControl blueplayer07 = null;
        public static bool blueplayer07IsReviving = false;
        public static PlayerControl blueplayer07currentTarget = null;
        public static PlayerControl blueplayer08 = null;
        public static bool blueplayer08IsReviving = false;
        public static PlayerControl blueplayer08currentTarget = null;

        public static bool captureTheFlagMode = false;
        public static float requiredFlags = 3;
        public static float killCooldown = 10f;
        public static float matchDuration = 300f;
        public static float reviveTime = 5f;
        public static float invincibilityTimeAfterRevive = 3f;

        public static GameObject redflag = null;
        public static GameObject redflagbase = null;
        public static bool redflagtaken = false;
        public static PlayerControl redPlayerWhoHasBlueFlag = null;
        public static float currentRedTeamPoints = 0;
        public static List<Arrow> localRedFlagArrow = new List<Arrow>();
        public static bool redteamAlerted = false;

        public static GameObject blueflag = null;
        public static GameObject blueflagbase = null;
        public static bool blueflagtaken = false;
        public static PlayerControl bluePlayerWhoHasRedFlag = null;
        public static float currentBlueTeamPoints = 0;
        public static List<Arrow> localBlueFlagArrow = new List<Arrow>();
        public static bool blueteamAlerted = false;

        public static bool triggerRedTeamWin = false;
        public static bool triggerBlueTeamWin = false;
        public static bool triggerDrawWin = false;

        public static string flagpointCounter = "Score: " + "<color=#FF0000FF>" + currentRedTeamPoints + "</color> - " + "<color=#0000FFFF>" + currentBlueTeamPoints + "</color>";

        private static Sprite buttonSpriteTakeRedFlag;

        public static Sprite getTakeRedFlagButtonSprite() {
            if (buttonSpriteTakeRedFlag) return buttonSpriteTakeRedFlag;
            buttonSpriteTakeRedFlag = Helpers.loadSpriteFromResources("LasMonjas.Images.CaptureTheFlagStealRedFlagButton.png", 90f);
            return buttonSpriteTakeRedFlag;
        }

        private static Sprite buttonSpriteTakeBlueFlag;

        public static Sprite getTakeBlueFlagButtonSprite() {
            if (buttonSpriteTakeBlueFlag) return buttonSpriteTakeBlueFlag;
            buttonSpriteTakeBlueFlag = Helpers.loadSpriteFromResources("LasMonjas.Images.CaptureTheFlagStealBlueFlagButton.png", 90f);
            return buttonSpriteTakeBlueFlag;
        }

        private static Sprite buttonSpriteDeliverRedFlag;
        public static Sprite getDeliverRedFlagButtonSprite() {
            if (buttonSpriteDeliverRedFlag) return buttonSpriteDeliverRedFlag;
            buttonSpriteDeliverRedFlag = Helpers.loadSpriteFromResources("LasMonjas.Images.CaptureTheFlagDeliverRedFlagButton.png", 90f);
            return buttonSpriteDeliverRedFlag;
        }

        private static Sprite buttonSpriteDeliverBlueFlag;
        public static Sprite getDeliverBlueFlagButtonSprite() {
            if (buttonSpriteDeliverBlueFlag) return buttonSpriteDeliverBlueFlag;
            buttonSpriteDeliverBlueFlag = Helpers.loadSpriteFromResources("LasMonjas.Images.CaptureTheFlagDeliverBlueFlagButton.png", 90f);
            return buttonSpriteDeliverBlueFlag;
        }

        public static void clearAndReload() {
            redteamFlag.Clear();
            redplayer01 = null;
            redplayer01currentTarget = null;
            redplayer01IsReviving = false;
            redplayer02 = null;
            redplayer02IsReviving = false;
            redplayer02currentTarget = null;
            redplayer03 = null;
            redplayer03IsReviving = false;
            redplayer03currentTarget = null;
            redplayer04 = null;
            redplayer04IsReviving = false;
            redplayer04currentTarget = null;
            redplayer05 = null;
            redplayer05IsReviving = false;
            redplayer05currentTarget = null;
            redplayer06 = null;
            redplayer06IsReviving = false;
            redplayer06currentTarget = null;
            redplayer07 = null;
            redplayer07IsReviving = false;
            redplayer07currentTarget = null;
            blueteamFlag.Clear();
            blueplayer01 = null;
            blueplayer01IsReviving = false;
            blueplayer01currentTarget = null;
            blueplayer02 = null;
            blueplayer02IsReviving = false;
            blueplayer02currentTarget = null;
            blueplayer03 = null;
            blueplayer03IsReviving = false;
            blueplayer03currentTarget = null;
            blueplayer04 = null;
            blueplayer04IsReviving = false;
            blueplayer04currentTarget = null;
            blueplayer05 = null;
            blueplayer05IsReviving = false;
            blueplayer05currentTarget = null;
            blueplayer06 = null;
            blueplayer06IsReviving = false;
            blueplayer06currentTarget = null;
            blueplayer07 = null;
            blueplayer07IsReviving = false;
            blueplayer07currentTarget = null;
            blueplayer08 = null;
            blueplayer08IsReviving = false;
            blueplayer08currentTarget = null;
            if (CustomOptionHolder.captureTheFlagMode.getSelection() == 1) {
                captureTheFlagMode = true;
            }
            else {
                captureTheFlagMode = false;
            }
            requiredFlags = CustomOptionHolder.requiredFlags.getFloat();
            killCooldown = CustomOptionHolder.flagKillCooldown.getFloat();
            matchDuration = CustomOptionHolder.flagMatchDuration.getFloat() + 10f;
            reviveTime = CustomOptionHolder.flagReviveTime.getFloat();
            invincibilityTimeAfterRevive = CustomOptionHolder.flagInvincibilityTimeAfterRevive.getFloat();
            redflag = null;
            redflagbase = null;
            redflagtaken = false;
            redPlayerWhoHasBlueFlag = null;
            currentRedTeamPoints = 0;
            redteamAlerted = false;
            blueflag = null;
            blueflagbase = null;
            blueflagtaken = false;
            bluePlayerWhoHasRedFlag = null;
            triggerRedTeamWin = false;
            triggerBlueTeamWin = false;
            triggerDrawWin = false;
            currentBlueTeamPoints = 0;
            blueteamAlerted = false;
            localRedFlagArrow = new List<Arrow>();
            localBlueFlagArrow = new List<Arrow>();
            flagpointCounter = "Score: " + "<color=#FF0000FF>" + currentRedTeamPoints + "</color> - " + "<color=#0000FFFF>" + currentBlueTeamPoints + "</color>";
        }
    }

    public static class PoliceAndThief
    {
        public static List<PlayerControl> thiefTeam = new List<PlayerControl>();
        public static PlayerControl thiefplayer01 = null;
        public static PlayerControl thiefplayer01currentTarget = null;
        public static bool thiefplayer01IsStealing = false;
        public static byte thiefplayer01JewelId = 0;
        public static bool thiefplayer01IsReviving = false;
        public static PlayerControl thiefplayer02 = null;
        public static PlayerControl thiefplayer02currentTarget = null;
        public static bool thiefplayer02IsStealing = false;
        public static byte thiefplayer02JewelId = 0;
        public static bool thiefplayer02IsReviving = false;
        public static PlayerControl thiefplayer03 = null;
        public static PlayerControl thiefplayer03currentTarget = null;
        public static bool thiefplayer03IsStealing = false;
        public static byte thiefplayer03JewelId = 0;
        public static bool thiefplayer03IsReviving = false;
        public static PlayerControl thiefplayer04 = null;
        public static PlayerControl thiefplayer04currentTarget = null;
        public static bool thiefplayer04IsStealing = false;
        public static byte thiefplayer04JewelId = 0;
        public static bool thiefplayer04IsReviving = false;
        public static PlayerControl thiefplayer05 = null;
        public static PlayerControl thiefplayer05currentTarget = null;
        public static bool thiefplayer05IsStealing = false;
        public static byte thiefplayer05JewelId = 0;
        public static bool thiefplayer05IsReviving = false;
        public static PlayerControl thiefplayer06 = null;
        public static PlayerControl thiefplayer06currentTarget = null;
        public static bool thiefplayer06IsStealing = false;
        public static byte thiefplayer06JewelId = 0;
        public static bool thiefplayer06IsReviving = false;
        public static PlayerControl thiefplayer07 = null;
        public static PlayerControl thiefplayer07currentTarget = null;
        public static bool thiefplayer07IsStealing = false;
        public static byte thiefplayer07JewelId = 0;
        public static bool thiefplayer07IsReviving = false;
        public static PlayerControl thiefplayer08 = null;
        public static PlayerControl thiefplayer08currentTarget = null;
        public static bool thiefplayer08IsStealing = false;
        public static byte thiefplayer08JewelId = 0;
        public static bool thiefplayer08IsReviving = false;
        public static PlayerControl thiefplayer09 = null;
        public static PlayerControl thiefplayer09currentTarget = null;
        public static bool thiefplayer09IsStealing = false;
        public static byte thiefplayer09JewelId = 0;
        public static bool thiefplayer09IsReviving = false;
        public static PlayerControl thiefplayer10 = null;
        public static PlayerControl thiefplayer10currentTarget = null;
        public static bool thiefplayer10IsStealing = false;
        public static byte thiefplayer10JewelId = 0;
        public static bool thiefplayer10IsReviving = false;

        public static List<PlayerControl> policeTeam = new List<PlayerControl>();
        public static PlayerControl policeplayer01 = null;
        public static PlayerControl policeplayer01currentTarget = null;
        public static PlayerControl policeplayer01targetedPlayer = null;
        public static float policeplayer01lightTimer = 0;
        public static bool policeplayer01IsReviving = false;
        public static PlayerControl policeplayer02 = null;
        public static PlayerControl policeplayer02currentTarget = null;
        public static PlayerControl policeplayer02targetedPlayer = null;
        public static float policeplayer02lightTimer = 0;
        public static bool policeplayer02IsReviving = false;
        public static PlayerControl policeplayer03 = null;
        public static PlayerControl policeplayer03currentTarget = null;
        public static PlayerControl policeplayer03targetedPlayer = null;
        public static float policeplayer03lightTimer = 0;
        public static bool policeplayer03IsReviving = false;
        public static PlayerControl policeplayer04 = null;
        public static PlayerControl policeplayer04currentTarget = null;
        public static PlayerControl policeplayer04targetedPlayer = null;
        public static float policeplayer04lightTimer = 0;
        public static bool policeplayer04IsReviving = false;
        public static PlayerControl policeplayer05 = null;
        public static PlayerControl policeplayer05currentTarget = null;
        public static PlayerControl policeplayer05targetedPlayer = null;
        public static float policeplayer05lightTimer = 0;
        public static bool policeplayer05IsReviving = false;

        public static List<PlayerControl> thiefArrested = new List<PlayerControl>();
        public static List<GameObject> thiefTreasures = new List<GameObject>();
        public static GameObject cell = null;
        public static GameObject cellbutton = null;
        public static GameObject jewelbutton = null;

        public static GameObject jewel01 = null;
        public static PlayerControl jewel01BeingStealed = null;
        public static GameObject jewel02 = null;
        public static PlayerControl jewel02BeingStealed = null;
        public static GameObject jewel03 = null;
        public static PlayerControl jewel03BeingStealed = null;
        public static GameObject jewel04 = null;
        public static PlayerControl jewel04BeingStealed = null;
        public static GameObject jewel05 = null;
        public static PlayerControl jewel05BeingStealed = null;
        public static GameObject jewel06 = null;
        public static PlayerControl jewel06BeingStealed = null;
        public static GameObject jewel07 = null;
        public static PlayerControl jewel07BeingStealed = null;
        public static GameObject jewel08 = null;
        public static PlayerControl jewel08BeingStealed = null;
        public static GameObject jewel09 = null;
        public static PlayerControl jewel09BeingStealed = null;
        public static GameObject jewel10 = null;
        public static PlayerControl jewel10BeingStealed = null;
        public static GameObject jewel11 = null;
        public static PlayerControl jewel11BeingStealed = null;
        public static GameObject jewel12 = null;
        public static PlayerControl jewel12BeingStealed = null;
        public static GameObject jewel13 = null;
        public static PlayerControl jewel13BeingStealed = null;
        public static GameObject jewel14 = null;
        public static PlayerControl jewel14BeingStealed = null;
        public static GameObject jewel15 = null;
        public static PlayerControl jewel15BeingStealed = null;

        public static bool policeAndThiefMode = false;
        public static float requiredJewels = 10;
        public static float policeKillCooldown = 20f;
        public static bool policeCanKillNearPrison = false;
        public static bool policeCanSeeJewels = false;
        public static float policeCatchCooldown = 10f;
        public static float captureThiefTime = 3f;
        public static float policeVision = 1f;
        public static float matchDuration = 300f;
        public static float policeReviveTime = 5f;
        public static bool thiefTeamCanKill = false;
        public static float thiefKillCooldown = 20f;
        public static float thiefReviveTime = 10f;
        public static float invincibilityTimeAfterRevive = 3f;

        public static float currentJewelsStoled = 0;
        public static float currentThiefsCaptured = 0;

        public static bool triggerThiefWin = false;
        public static bool triggerPoliceWin = false;

        public static string thiefpointCounter = "Stealed Jewels: " + "<color=#FF0000FF>" + currentJewelsStoled + "/" + requiredJewels + "</color> | " + "Captured Thiefs: " + "<color=#0000FFFF>" + currentThiefsCaptured + "/ 10</color>";


        private static Sprite buttonSpriteLight;

        public static Sprite getLightButtonSprite() {
            if (buttonSpriteLight) return buttonSpriteLight;
            buttonSpriteLight = Helpers.loadSpriteFromResources("LasMonjas.Images.PoliceAndThiefsLightButton.png", 90f);
            return buttonSpriteLight;
        }
        
        private static Sprite buttonSpriteCaptureThief;

        public static Sprite getCaptureThiefButtonSprite() {
            if (buttonSpriteCaptureThief) return buttonSpriteCaptureThief;
            buttonSpriteCaptureThief = Helpers.loadSpriteFromResources("LasMonjas.Images.PoliceAndThiefCaptureButton.png", 90f);
            return buttonSpriteCaptureThief;
        }

        private static Sprite buttonSpriteFreeThief;

        public static Sprite getFreeThiefButtonSprite() {
            if (buttonSpriteFreeThief) return buttonSpriteFreeThief;
            buttonSpriteFreeThief = Helpers.loadSpriteFromResources("LasMonjas.Images.PoliceAndThiefFreeButton.png", 90f);
            return buttonSpriteFreeThief;
        }


        private static Sprite buttonSpriteDeliverJewel;

        public static Sprite getDeliverJewelButtonSprite() {
            if (buttonSpriteDeliverJewel) return buttonSpriteDeliverJewel;
            buttonSpriteDeliverJewel = Helpers.loadSpriteFromResources("LasMonjas.Images.PoliceAndThiefDeliverJewelButton.png", 90f);
            return buttonSpriteDeliverJewel;
        }

        private static Sprite buttonSpriteTakeJewel;

        public static Sprite getTakeJewelButtonSprite() {
            if (buttonSpriteTakeJewel) return buttonSpriteTakeJewel;
            buttonSpriteTakeJewel = Helpers.loadSpriteFromResources("LasMonjas.Images.PoliceAndThiefTakeJewelButton.png", 90f);
            return buttonSpriteTakeJewel;
        }


        public static void clearAndReload() {
            cell = null;
            cellbutton = null;
            thiefTreasures.Clear();
            thiefArrested.Clear();

            thiefTeam.Clear();
            thiefplayer01 = null;
            thiefplayer01currentTarget = null;
            thiefplayer01IsStealing = false;
            thiefplayer01JewelId = 0;
            thiefplayer01IsReviving = false;
            thiefplayer02 = null;
            thiefplayer02currentTarget = null;
            thiefplayer02IsStealing = false;
            thiefplayer02JewelId = 0;
            thiefplayer02IsReviving = false;
            thiefplayer03 = null;
            thiefplayer03currentTarget = null;
            thiefplayer03IsStealing = false;
            thiefplayer03JewelId = 0;
            thiefplayer03IsReviving = false;
            thiefplayer04 = null;
            thiefplayer04currentTarget = null;
            thiefplayer04IsStealing = false;
            thiefplayer04JewelId = 0;
            thiefplayer04IsReviving = false;
            thiefplayer05 = null;
            thiefplayer05currentTarget = null;
            thiefplayer05IsStealing = false;
            thiefplayer05JewelId = 0;
            thiefplayer05IsReviving = false;
            thiefplayer06 = null;
            thiefplayer06currentTarget = null;
            thiefplayer06IsStealing = false;
            thiefplayer06JewelId = 0;
            thiefplayer06IsReviving = false;
            thiefplayer07 = null;
            thiefplayer07currentTarget = null;
            thiefplayer07IsStealing = false;
            thiefplayer07JewelId = 0;
            thiefplayer07IsReviving = false;
            thiefplayer08 = null;
            thiefplayer08currentTarget = null;
            thiefplayer08IsStealing = false;
            thiefplayer08JewelId = 0;
            thiefplayer08IsReviving = false;
            thiefplayer09 = null;
            thiefplayer09currentTarget = null;
            thiefplayer09IsStealing = false;
            thiefplayer09JewelId = 0;
            thiefplayer09IsReviving = false;
            thiefplayer10 = null;
            thiefplayer10currentTarget = null;
            thiefplayer10IsStealing = false;
            thiefplayer10JewelId = 0;
            thiefplayer10IsReviving = false;

            policeTeam.Clear();
            policeplayer01 = null;
            policeplayer01currentTarget = null;
            policeplayer01targetedPlayer = null;
            policeplayer01lightTimer = 0;
            policeplayer01IsReviving = false;
            policeplayer02 = null;
            policeplayer02currentTarget = null;
            policeplayer02targetedPlayer = null;
            policeplayer02lightTimer = 0;
            policeplayer02IsReviving = false;
            policeplayer03 = null;
            policeplayer03currentTarget = null;
            policeplayer03targetedPlayer = null;
            policeplayer03lightTimer = 0;
            policeplayer03IsReviving = false;
            policeplayer04 = null;
            policeplayer04currentTarget = null;
            policeplayer04targetedPlayer = null;
            policeplayer04lightTimer = 0;
            policeplayer04IsReviving = false;
            policeplayer05 = null;
            policeplayer05currentTarget = null;
            policeplayer05targetedPlayer = null;
            policeplayer05lightTimer = 0;
            policeplayer05IsReviving = false;

            jewel01 = null;
            jewel01BeingStealed = null;
            jewel02 = null;
            jewel02BeingStealed = null;
            jewel03 = null;
            jewel03BeingStealed = null;
            jewel04 = null;
            jewel04BeingStealed = null;
            jewel05 = null;
            jewel05BeingStealed = null;
            jewel06 = null;
            jewel06BeingStealed = null;
            jewel07 = null;
            jewel07BeingStealed = null;
            jewel08 = null;
            jewel08BeingStealed = null;
            jewel09 = null;
            jewel09BeingStealed = null;
            jewel10 = null;
            jewel10BeingStealed = null;
            jewel11 = null;
            jewel11BeingStealed = null;
            jewel12 = null;
            jewel12BeingStealed = null;
            jewel13 = null;
            jewel13BeingStealed = null;
            jewel14 = null;
            jewel14BeingStealed = null;
            jewel15 = null;
            jewel15BeingStealed = null;

            if (CustomOptionHolder.policeAndThiefMode.getSelection() == 1) {
                policeAndThiefMode = true;
            }
            else {
                policeAndThiefMode = false;
            }
            requiredJewels = CustomOptionHolder.thiefModerequiredJewels.getFloat();
            policeKillCooldown = CustomOptionHolder.thiefModePoliceKillCooldown.getFloat();
            policeCanKillNearPrison = CustomOptionHolder.thiefModePoliceCanKillNearPrison.getBool();
            policeCanSeeJewels = CustomOptionHolder.thiefModePoliceCanSeeJewels.getBool();
            policeCatchCooldown = CustomOptionHolder.thiefModePoliceCatchCooldown.getFloat();
            captureThiefTime = CustomOptionHolder.thiefModecaptureThiefTime.getFloat();
            policeVision = CustomOptionHolder.thiefModepolicevision.getFloat();
            matchDuration = CustomOptionHolder.thiefModeMatchDuration.getFloat() + 10f;
            policeReviveTime = CustomOptionHolder.thiefModePoliceReviveTime.getFloat();
            thiefTeamCanKill = CustomOptionHolder.thiefModeCanKill.getBool();
            thiefKillCooldown = CustomOptionHolder.thiefModeKillCooldown.getFloat();
            thiefReviveTime = CustomOptionHolder.thiefModeThiefReviveTime.getFloat();
            invincibilityTimeAfterRevive = CustomOptionHolder.thiefModeInvincibilityTimeAfterRevive.getFloat();
            currentJewelsStoled = 0;
            triggerThiefWin = false;
            triggerPoliceWin = false;
            currentThiefsCaptured = 0;
            thiefpointCounter = "Stealed Jewels: " + "<color=#00F7FFFF>" + currentJewelsStoled + "/" + requiredJewels + "</color> | " + "Captured Thiefs: " + "<color=#928B55FF>" + currentThiefsCaptured + "/" + thiefTeam.Count + "</color>";
        }
    }

    public static class KingOfTheHill
    {
        public static List<PlayerControl> greenTeam = new List<PlayerControl>();
        public static byte whichGreenKingplayerzone = 0;
        public static int totalGreenKingzonescaptured = 0;
        public static bool greenKinghaszoneone = false;
        public static bool greenKinghaszonetwo = false;
        public static bool greenKinghaszonethree = false;
        public static PlayerControl greenKingplayer = null;
        public static PlayerControl greenKingplayercurrentTarget = null;
        public static bool greenKingIsReviving = false;
        public static PlayerControl greenplayer01 = null;
        public static PlayerControl greenplayer01currentTarget = null;
        public static bool greenplayer01IsReviving = false;
        public static PlayerControl greenplayer02 = null;
        public static PlayerControl greenplayer02currentTarget = null;
        public static bool greenplayer02IsReviving = false;
        public static PlayerControl greenplayer03 = null;
        public static PlayerControl greenplayer03currentTarget = null;
        public static bool greenplayer03IsReviving = false;
        public static PlayerControl greenplayer04 = null;
        public static PlayerControl greenplayer04currentTarget = null;
        public static bool greenplayer04IsReviving = false;
        public static PlayerControl greenplayer05 = null;
        public static PlayerControl greenplayer05currentTarget = null;
        public static bool greenplayer05IsReviving = false;
        public static PlayerControl greenplayer06 = null;
        public static PlayerControl greenplayer06currentTarget = null;
        public static bool greenplayer06IsReviving = false;

        public static List<PlayerControl> yellowTeam = new List<PlayerControl>();
        public static byte whichYellowKingplayerzone = 0;
        public static int totalYellowKingzonescaptured = 0;
        public static bool yellowKinghaszoneone = false;
        public static bool yellowKinghaszonetwo = false;
        public static bool yellowKinghaszonethree = false;
        public static PlayerControl yellowKingplayer = null;
        public static PlayerControl yellowKingplayercurrentTarget = null;
        public static bool yellowKingIsReviving = false;
        public static PlayerControl yellowplayer01 = null;
        public static PlayerControl yellowplayer01currentTarget = null;
        public static bool yellowplayer01IsReviving = false;
        public static PlayerControl yellowplayer02 = null;
        public static PlayerControl yellowplayer02currentTarget = null;
        public static bool yellowplayer02IsReviving = false;
        public static PlayerControl yellowplayer03 = null;
        public static PlayerControl yellowplayer03currentTarget = null;
        public static bool yellowplayer03IsReviving = false;
        public static PlayerControl yellowplayer04 = null;
        public static PlayerControl yellowplayer04currentTarget = null;
        public static bool yellowplayer04IsReviving = false;
        public static PlayerControl yellowplayer05 = null;
        public static PlayerControl yellowplayer05currentTarget = null;
        public static bool yellowplayer05IsReviving = false;
        public static PlayerControl yellowplayer06 = null;
        public static PlayerControl yellowplayer06currentTarget = null;
        public static bool yellowplayer06IsReviving = false;
        public static PlayerControl usurperPlayer = null;
        public static PlayerControl usurperPlayercurrentTarget = null;
        public static bool usurperPlayerIsReviving = false;

        public static bool kingOfTheHillMode = false;
        public static float requiredPoints = 150;
        public static float captureCooldown = 10f;
        public static float killCooldown = 10f;
        public static float matchDuration = 300f;
        public static bool kingCanKill = false;
        public static float reviveTime = 5f;
        public static float kingInvincibilityTimeAfterRevive = 3f;

        public static GameObject greenflag = null;
        public static float currentGreenTeamPoints = 0;
        public static bool greenteamAlerted = false;

        public static GameObject yellowflag = null;
        public static float currentYellowTeamPoints = 0;
        public static bool yellowteamAlerted = false;

        public static List<GameObject> kingZones = new List<GameObject>();
        public static GameObject flagzoneone = null;
        public static GameObject flagzonetwo = null;
        public static GameObject flagzonethree = null;
        public static GameObject zoneone = null;
        public static GameObject zonetwo = null;
        public static GameObject zonethree = null;
        public static Color zoneonecolor = Color.white;
        public static Color zonetwocolor = Color.white;
        public static Color zonethreecolor = Color.white;
        public static List<Arrow> localArrows = new List<Arrow>();
        public static GameObject greenkingaura = null;
        public static GameObject yellowkingaura = null;

        public static bool triggerGreenTeamWin = false;
        public static bool triggerYellowTeamWin = false;
        public static bool triggerDrawWin = false;

        public static string kingpointCounter = "Puntuacion: " + "<color=#00FF00FF>" + currentGreenTeamPoints.ToString("F0") + "</color> - " + "<color=#FFFF00FF>" + currentYellowTeamPoints.ToString("F0") + "</color>";

        private static Sprite buttonSpritePlaceGreenFlag;

        public static Sprite getPlaceGreenFlagButtonSprite() {
            if (buttonSpritePlaceGreenFlag) return buttonSpritePlaceGreenFlag;
            buttonSpritePlaceGreenFlag = Helpers.loadSpriteFromResources("LasMonjas.Images.KingOfTheHillGreenCapture.png", 90f);
            return buttonSpritePlaceGreenFlag;
        }

        private static Sprite buttonSpritePlaceYellowFlag;

        public static Sprite getPlaceYellowFlagButtonSprite() {
            if (buttonSpritePlaceYellowFlag) return buttonSpritePlaceYellowFlag;
            buttonSpritePlaceYellowFlag = Helpers.loadSpriteFromResources("LasMonjas.Images.KingOfTheHillYellowCapture.png", 90f);
            return buttonSpritePlaceYellowFlag;
        }

        public static void clearAndReload() {
            greenTeam.Clear();
            whichGreenKingplayerzone = 0;
            totalGreenKingzonescaptured = 0;
            greenKinghaszoneone = false;
            greenKinghaszonetwo = false;
            greenKinghaszonethree = false;
            greenKingplayer = null;
            greenKingplayercurrentTarget = null;
            greenKingIsReviving = false;
            greenplayer01 = null;
            greenplayer01currentTarget = null;
            greenplayer01IsReviving = false;
            greenplayer02 = null;
            greenplayer02currentTarget = null;
            greenplayer02IsReviving = false;
            greenplayer03 = null;
            greenplayer03currentTarget = null;
            greenplayer03IsReviving = false;
            greenplayer04 = null;
            greenplayer04currentTarget = null;
            greenplayer04IsReviving = false;
            greenplayer05 = null;
            greenplayer05currentTarget = null;
            greenplayer05IsReviving = false;
            greenplayer06 = null;
            greenplayer06currentTarget = null;
            greenplayer06IsReviving = false;

            yellowTeam.Clear();
            whichYellowKingplayerzone = 0;
            totalYellowKingzonescaptured = 0;
            yellowKinghaszoneone = false;
            yellowKinghaszonetwo = false;
            yellowKinghaszonethree = false;
            yellowKingplayer = null;
            yellowKingplayercurrentTarget = null;
            yellowKingIsReviving = false;
            yellowplayer01 = null;
            yellowplayer01currentTarget = null;
            yellowplayer01IsReviving = false;
            yellowplayer02 = null;
            yellowplayer02currentTarget = null;
            yellowplayer02IsReviving = false;
            yellowplayer03 = null;
            yellowplayer03currentTarget = null;
            yellowplayer03IsReviving = false;
            yellowplayer04 = null;
            yellowplayer04currentTarget = null;
            yellowplayer04IsReviving = false;
            yellowplayer05 = null;
            yellowplayer05currentTarget = null;
            yellowplayer05IsReviving = false;
            yellowplayer06 = null;
            yellowplayer06currentTarget = null;
            yellowplayer06IsReviving = false;
            usurperPlayer = null;
            usurperPlayercurrentTarget = null;
            usurperPlayerIsReviving = false;

            if (CustomOptionHolder.kingOfTheHillMode.getSelection() == 1) {
                kingOfTheHillMode = true;
            }
            else {
                kingOfTheHillMode = false;
            }

            requiredPoints = CustomOptionHolder.kingRequiredPoints.getFloat();
            captureCooldown = CustomOptionHolder.kingCaptureCooldown.getFloat();
            killCooldown = CustomOptionHolder.kingKillCooldown.getFloat();
            matchDuration = CustomOptionHolder.kingMatchDuration.getFloat() + 10f;
            kingCanKill = CustomOptionHolder.kingCanKill.getBool();
            reviveTime = CustomOptionHolder.kingReviveTime.getFloat();
            kingInvincibilityTimeAfterRevive = CustomOptionHolder.kingInvincibilityTimeAfterRevive.getFloat();

            greenflag = null;
            currentGreenTeamPoints = 0;
            greenteamAlerted = false;

            yellowflag = null;
            currentYellowTeamPoints = 0;
            yellowteamAlerted = false;

            kingZones.Clear();
            flagzoneone = null;
            flagzonetwo = null;
            flagzonethree = null;
            zoneone = null;
            zonetwo = null;
            zonethree = null;
            zoneonecolor = Color.white;
            zonetwocolor = Color.white;
            zonethreecolor = Color.white;
            greenkingaura = null;
            yellowkingaura = null;
            triggerGreenTeamWin = false;
            triggerYellowTeamWin = false;
            triggerDrawWin = false;

            localArrows = new List<Arrow>();
            kingpointCounter = "Puntuacion: " + "<color=#00FF00FF>" + currentGreenTeamPoints.ToString("F0") + "</color> - " + "<color=#FFFF00FF>" + currentYellowTeamPoints.ToString("F0") + "</color>";
        }
    }

    public static class CustomMain
    {
        public static CustomAssets customAssets = new CustomAssets();
    }

    public class CustomAssets
    {
        // Custom Bundle Role Assets
        public GameObject bombermanBomb;
        public AudioClip bombermanPlaceBombClip;
        public AudioClip bombermanBombMusic;
        public AudioClip bombermanBombClip;
        public AudioClip renegadeRecruitMinionClip;
        public GameObject trapperMine;
        public AudioClip trapperStepMineClip;
        public GameObject trapperTrap;
        public AudioClip trapperStepTrapClip;
        public GameObject yinyangerYinyang;
        public AudioClip yinyangerYinyangClip;
        public AudioClip yinyangerYinyangColisionClip;
        public GameObject challengerDuelArena;
        public AudioClip challengerDuelMusic;
        public GameObject challengerRock;
        public GameObject challengerPaper;
        public GameObject challengerScissors;
        public AudioClip challengerDuelKillClip;
        public AudioClip roleThiefStealRole;
        public AudioClip pyromaniacIgniteClip;
        public GameObject treasureHunterTreasure;
        public AudioClip treasureHunterPlaceTreasure;
        public AudioClip treasureHunterCollectTreasure; 
        public AudioClip devourerDingClip;
        public AudioClip devourerDevourClip;
        public AudioClip timeTravelerTimeReverseClip;
        public AudioClip squireShieldClip;
        public AudioClip fortuneTellerRevealClip;
        public AudioClip spiritualistRevive;
        public GameObject performerDio;
        public AudioClip performerMusic;
        public AudioClip jinxQuack;

        // Custom Bundle Capture the flag Assets
        public AudioClip captureTheFlagMusic;
        public GameObject redflag;
        public GameObject redflagbase;
        public GameObject blueflag;
        public GameObject blueflagbase;

        // Custom Bundle Police and thief Assets
        public AudioClip policeAndThiefMusic;
        public GameObject cell;
        public GameObject jewelbutton;
        public GameObject freethiefbutton;
        public GameObject jeweldiamond;
        public GameObject jewelruby;
        public GameObject thiefspaceship;
        public GameObject thiefspaceshiphatch;

        // Custom Bundle King Of The Hill Assets
        public AudioClip kingOfTheHillMusic;
        public GameObject whiteflag;
        public GameObject greenflag;
        public GameObject yellowflag;
        public GameObject whitebase;
        public GameObject greenbase;
        public GameObject yellowbase;
        public GameObject greenaura;
        public GameObject yellowaura;
        public GameObject greenfloor;
        public GameObject yellowfloor;

        // Custom Map
        public GameObject customMap;
        public GameObject customMinimap;
        public GameObject customComms;

        // Custom Lobby
        public GameObject customLobby;

        // Custom Music
        public AudioClip lobbyMusic;
        public AudioClip tasksCalmMusic;
        public AudioClip tasksCoreMusic;
        public AudioClip tasksFinalMusic; 
        public AudioClip meetingCalmMusic;
        public AudioClip meetingCoreMusic;
        public AudioClip meetingFinalMusic;       
        public AudioClip winCrewmatesMusic;
        public AudioClip winImpostorsMusic;
        public AudioClip winNeutralsMusic;
        public AudioClip winRebelsMusic;
    }
}