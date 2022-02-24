using HarmonyLib;
using System.Linq;
using System;
using System.Collections.Generic;
using static LasMonjas.LasMonjas;
using UnityEngine;

namespace LasMonjas
{
    class RoleInfo
    {
        public Color color;
        public string name;
        public string introDescription;
        public string shortDescription;
        public RoleId roleId;
        public bool isNeutral;
        public bool isRebel;

        RoleInfo(string name, Color color, string introDescription, string shortDescription, RoleId roleId, bool isNeutral = false, bool isRebel = false) {
            this.color = color;
            this.name = name;
            this.introDescription = introDescription;
            this.shortDescription = shortDescription;
            this.roleId = roleId;
            this.isNeutral = isNeutral;
            this.isRebel = isRebel;
        }

        // Capture the Flag Teams
        public static RoleInfo redplayer01 = new RoleInfo("Red Team", Color.red, "Steal <color=#0000FFFF>Blue Team</color> flag", "Steal <color=#0000FFFF>Blue Team</color> flag", RoleId.RedPlayer01);
        public static RoleInfo redplayer02 = new RoleInfo("Red Team", Color.red, "Steal <color=#0000FFFF>Blue Team</color> flag", "Steal <color=#0000FFFF>Blue Team</color> flag", RoleId.RedPlayer02);
        public static RoleInfo redplayer03 = new RoleInfo("Red Team", Color.red, "Steal <color=#0000FFFF>Blue Team</color> flag", "Steal <color=#0000FFFF>Blue Team</color> flag", RoleId.RedPlayer03);
        public static RoleInfo redplayer04 = new RoleInfo("Red Team", Color.red, "Steal <color=#0000FFFF>Blue Team</color> flag", "Steal <color=#0000FFFF>Blue Team</color> flag", RoleId.RedPlayer04);
        public static RoleInfo redplayer05 = new RoleInfo("Red Team", Color.red, "Steal <color=#0000FFFF>Blue Team</color> flag", "Steal <color=#0000FFFF>Blue Team</color> flag", RoleId.RedPlayer05);
        public static RoleInfo redplayer06 = new RoleInfo("Red Team", Color.red, "Steal <color=#0000FFFF>Blue Team</color> flag", "Steal <color=#0000FFFF>Blue Team</color> flag", RoleId.RedPlayer06);
        public static RoleInfo redplayer07 = new RoleInfo("Red Team", Color.red, "Steal <color=#0000FFFF>Blue Team</color> flag", "Steal <color=#0000FFFF>Blue Team</color> flag", RoleId.RedPlayer07);
        public static RoleInfo blueplayer01 = new RoleInfo("Blue Team", Color.blue, "Steal <color=#FF0000FF>Red Team</color> flag", "Steal <color=#FF0000FF>Red Team</color> flag", RoleId.BluePlayer01);
        public static RoleInfo blueplayer02 = new RoleInfo("Blue Team", Color.blue, "Steal <color=#FF0000FF>Red Team</color> flag", "Steal <color=#FF0000FF>Red Team</color> flag", RoleId.BluePlayer02);
        public static RoleInfo blueplayer03 = new RoleInfo("Blue Team", Color.blue, "Steal <color=#FF0000FF>Red Team</color> flag", "Steal <color=#FF0000FF>Red Team</color> flag", RoleId.BluePlayer03);
        public static RoleInfo blueplayer04 = new RoleInfo("Blue Team", Color.blue, "Steal <color=#FF0000FF>Red Team</color> flag", "Steal <color=#FF0000FF>Red Team</color> flag", RoleId.BluePlayer04);
        public static RoleInfo blueplayer05 = new RoleInfo("Blue Team", Color.blue, "Steal <color=#FF0000FF>Red Team</color> flag", "Steal <color=#FF0000FF>Red Team</color> flag", RoleId.BluePlayer05);
        public static RoleInfo blueplayer06 = new RoleInfo("Blue Team", Color.blue, "Steal <color=#FF0000FF>Red Team</color> flag", "Steal <color=#FF0000FF>Red Team</color> flag", RoleId.BluePlayer06);
        public static RoleInfo blueplayer07 = new RoleInfo("Blue Team", Color.blue, "Steal <color=#FF0000FF>Red Team</color> flag", "Steal <color=#FF0000FF>Red Team</color> flag", RoleId.BluePlayer07);
        public static RoleInfo blueplayer08 = new RoleInfo("Blue Team", Color.blue, "Steal <color=#FF0000FF>Red Team</color> flag", "Steal <color=#FF0000FF>Red Team</color> flag", RoleId.BluePlayer08);

        // Police and Thief Teams
        public static RoleInfo policeplayer01 = new RoleInfo("Police Officer", Color.cyan, "Capture all the <color=#D2B48CFF>Thiefs</color>", "Capture all the <color=#D2B48CFF>Thiefs</color>", RoleId.PolicePlayer01);
        public static RoleInfo policeplayer02 = new RoleInfo("Police Officer", Color.cyan, "Capture all the <color=#D2B48CFF>Thiefs</color>", "Capture all the <color=#D2B48CFF>Thiefs</color>", RoleId.PolicePlayer02);
        public static RoleInfo policeplayer03 = new RoleInfo("Police Officer", Color.cyan, "Capture all the <color=#D2B48CFF>Thiefs</color>", "Capture all the <color=#D2B48CFF>Thiefs</color>", RoleId.PolicePlayer03);
        public static RoleInfo policeplayer04 = new RoleInfo("Police Officer", Color.cyan, "Capture all the <color=#D2B48CFF>Thiefs</color>", "Capture all the <color=#D2B48CFF>Thiefs</color>", RoleId.PolicePlayer04);
        public static RoleInfo policeplayer05 = new RoleInfo("Police Officer", Color.cyan, "Capture all the <color=#D2B48CFF>Thiefs</color>", "Capture all the <color=#D2B48CFF>Thiefs</color>", RoleId.PolicePlayer05);
        public static RoleInfo thiefplayer01 = new RoleInfo("Thief", Mechanic.color, "Steal all the jewels without getting captured", "Steal all the jewels without getting captured", RoleId.ThiefPlayer01);
        public static RoleInfo thiefplayer02 = new RoleInfo("Thief", Mechanic.color, "Steal all the jewels without getting captured", "Steal all the jewels without getting captured", RoleId.ThiefPlayer02);
        public static RoleInfo thiefplayer03 = new RoleInfo("Thief", Mechanic.color, "Steal all the jewels without getting captured", "Steal all the jewels without getting captured", RoleId.ThiefPlayer03);
        public static RoleInfo thiefplayer04 = new RoleInfo("Thief", Mechanic.color, "Steal all the jewels without getting captured", "Steal all the jewels without getting captured", RoleId.ThiefPlayer04);
        public static RoleInfo thiefplayer05 = new RoleInfo("Thief", Mechanic.color, "Steal all the jewels without getting captured", "Steal all the jewels without getting captured", RoleId.ThiefPlayer05);
        public static RoleInfo thiefplayer06 = new RoleInfo("Thief", Mechanic.color, "Steal all the jewels without getting captured", "Steal all the jewels without getting captured", RoleId.ThiefPlayer06);
        public static RoleInfo thiefplayer07 = new RoleInfo("Thief", Mechanic.color, "Steal all the jewels without getting captured", "Steal all the jewels without getting captured", RoleId.ThiefPlayer07);
        public static RoleInfo thiefplayer08 = new RoleInfo("Thief", Mechanic.color, "Steal all the jewels without getting captured", "Steal all the jewels without getting captured", RoleId.ThiefPlayer08);
        public static RoleInfo thiefplayer09 = new RoleInfo("Thief", Mechanic.color, "Steal all the jewels without getting captured", "Steal all the jewels without getting captured", RoleId.ThiefPlayer09);
        public static RoleInfo thiefplayer10 = new RoleInfo("Thief", Mechanic.color, "Steal all the jewels without getting captured", "Steal all the jewels without getting captured", RoleId.ThiefPlayer10);

        // King of the hill Teams
        public static RoleInfo greenKing = new RoleInfo("Green King", Color.green, "Capture the zones", "Capture the zones", RoleId.GreenKing);
        public static RoleInfo greenplayer01 = new RoleInfo("Green Team", Color.green, "Protect your King and green zones", "Protect your King and green zones", RoleId.GreenPlayer01);
        public static RoleInfo greenplayer02 = new RoleInfo("Green Team", Color.green, "Protect your King and green zones", "Protect your King and green zones", RoleId.GreenPlayer02);
        public static RoleInfo greenplayer03 = new RoleInfo("Green Team", Color.green, "Protect your King and green zones", "Protect your King and green zones", RoleId.GreenPlayer03);
        public static RoleInfo greenplayer04 = new RoleInfo("Green Team", Color.green, "Protect your King and green zones", "Protect your King and green zones", RoleId.GreenPlayer04);
        public static RoleInfo greenplayer05 = new RoleInfo("Green Team", Color.green, "Protect your King and green zones", "Protect your King and green zones", RoleId.GreenPlayer05);
        public static RoleInfo greenplayer06 = new RoleInfo("Green Team", Color.green, "Protect your King and green zones", "Protect your King and green zones", RoleId.GreenPlayer06);
        public static RoleInfo yellowKing = new RoleInfo("Yellow King", Color.yellow, "Capture the zones", "Capture the zones", RoleId.YellowKing);
        public static RoleInfo yellowplayer01 = new RoleInfo("Yellow Team", Color.yellow, "Protect your King and yellow zones", "Protect your King and yellow zones", RoleId.YellowPlayer01);
        public static RoleInfo yellowplayer02 = new RoleInfo("Yellow Team", Color.yellow, "Protect your King and yellow zones", "Protect your King and yellow zones", RoleId.YellowPlayer02);
        public static RoleInfo yellowplayer03 = new RoleInfo("Yellow Team", Color.yellow, "Protect your King and yellow zones", "Protect your King and yellow zones", RoleId.YellowPlayer03);
        public static RoleInfo yellowplayer04 = new RoleInfo("Yellow Team", Color.yellow, "Protect your King and yellow zones", "Protect your King and yellow zones", RoleId.YellowPlayer04);
        public static RoleInfo yellowplayer05 = new RoleInfo("Yellow Team", Color.yellow, "Protect your King and yellow zones", "Protect your King and yellow zones", RoleId.YellowPlayer05);
        public static RoleInfo yellowplayer06 = new RoleInfo("Yellow Team", Color.yellow, "Protect your King and yellow zones", "Protect your King and yellow zones", RoleId.YellowPlayer06);
        public static RoleInfo yellowplayer07 = new RoleInfo("Yellow Team", Color.yellow, "Protect your King and yellow zones", "Protect your King and yellow zones", RoleId.YellowPlayer07);

        // Impostor roles
        public static RoleInfo mimic = new RoleInfo("Mimic", Mimic.color, "Mimic the look of other player", "Mimic the look of other player", RoleId.Mimic);
        public static RoleInfo painter = new RoleInfo("Painter", Painter.color, "Paint players with the same color", "Paint players with the same color", RoleId.Painter);
        public static RoleInfo demon = new RoleInfo("Demon", Demon.color, "Bite players to delay their death", "Bite players to delay their death", RoleId.Demon);
        public static RoleInfo janitor = new RoleInfo("Janitor", Janitor.color, "Remove and move bodies from the crime scene", "Remove and move bodies from the crime scene", RoleId.Janitor);
        public static RoleInfo ilusionist = new RoleInfo("Ilusionist", Ilusionist.color, "Create your own vent network and turn off the lights", "Create your own vent network and turn off the lights", RoleId.Ilusionist);
        public static RoleInfo manipulator = new RoleInfo("Manipulator", Manipulator.color, "Manipulate a player to kill his adjacent", "Manipulate a player to kill his adjacent", RoleId.Manipulator);
        public static RoleInfo bomberman = new RoleInfo("Bomberman", Bomberman.color, "Sabotage by putting bombs", "Sabotage by putting bombs", RoleId.Bomberman);
        public static RoleInfo chameleon = new RoleInfo("Chameleon", Chameleon.color, "Make yourself invisible", "Make yourself invisible", RoleId.Chameleon);
        public static RoleInfo gambler = new RoleInfo("Gambler", Gambler.color, "Shoot a player choosing their role during the meeting", "Shoot a player choosing their role during the meeting", RoleId.Gambler);
        public static RoleInfo sorcerer = new RoleInfo("Sorcerer", Sorcerer.color, "Casts spells on players", "Casts spells on players", RoleId.Sorcerer);

        // Rebelde roles
        public static RoleInfo renegade = new RoleInfo("Renegade", Renegade.color, "Recruit a Minion and kill everyone", "Recruit a Minion and kill everyone", RoleId.Renegade, false, true);
        public static RoleInfo minion = new RoleInfo("Minion", Minion.color, "Help the Renegade killing everyone", "Help the Renegade killing everyone", RoleId.Minion, false, true);
        public static RoleInfo bountyHunter = new RoleInfo("Bounty Hunter", BountyHunter.color, "Hunt down your target" + BountyHunter.rolName, "Hunt down your target" + BountyHunter.rolName, RoleId.BountyHunter, false, true);
        public static RoleInfo trapper = new RoleInfo("Trapper", Trapper.color, "Place landmines and traps on the map", "Place landmines and traps on the map", RoleId.Trapper, false, true);
        public static RoleInfo yinyanger = new RoleInfo("Yinyanger", Yinyanger.color, "Mark two players to die if they collide", "Mark two players to die if they collide", RoleId.Yinyanger, false, true);
        public static RoleInfo challenger = new RoleInfo("Challenger", Challenger.color, "Challenge a player to a rock-paper-scissors duel", "Challenge a player to a rock-paper-scissors duel", RoleId.Challenger, false, true);

        // Neutral roles
        public static RoleInfo joker = new RoleInfo("Joker", Joker.color, "Get voted out to win", "Get voted out to win \nOpen the map to activate the sabotage button", RoleId.Joker, true, false);
        public static RoleInfo rolethief = new RoleInfo("Role Thief", RoleThief.color, "Steal other player role", "Steal other player role", RoleId.RoleThief, true, false);
        public static RoleInfo pyromaniac = new RoleInfo("Pyromaniac", Pyromaniac.color, "Ignite all survivors to win", "Ignite all survivors to win", RoleId.Pyromaniac, true, false);
        public static RoleInfo treasureHunter = new RoleInfo("Treasure Hunter", TreasureHunter.color, "Find treasures to win", "Find treasures to win", RoleId.TreasureHunter, true, false);
        public static RoleInfo devourer = new RoleInfo("Devourer", Devourer.color, "Devour bodies to win", "Devour bodies to win", RoleId.Devourer, true, false);

        // Crewmate roles
        public static RoleInfo captain = new RoleInfo("Captain", Captain.color, "Your vote counts twice", "Your vote counts twice", RoleId.Captain);
        public static RoleInfo mechanic = new RoleInfo("Mechanic", Mechanic.color, "Repair sabotages on the ship", "Repair sabotages on the ship", RoleId.Mechanic);
        public static RoleInfo sheriff = new RoleInfo("Sheriff", Sheriff.color, "Kill the <color=#FF0000FF>Impostors</color>", "Kill the <color=#FF0000FF>Impostors</color>", RoleId.Sheriff);
        public static RoleInfo detective = new RoleInfo("Detective", Detective.color, "Examine footprints to find the <color=#FF0000FF>Impostors</color>", "Examine footprints to find the <color=#FF0000FF>Impostors</color>", RoleId.Detective);
        public static RoleInfo forensic = new RoleInfo("Forensic", Forensic.color, "Find clues reporting bodies and asking their ghosts", "Find clues reporting bodies and asking their ghosts", RoleId.Forensic);
        public static RoleInfo timeTraveler = new RoleInfo("Time Traveler", TimeTraveler.color, "Rewind the time", "Rewind the time", RoleId.TimeTraveler);
        public static RoleInfo squire = new RoleInfo("Squire", Squire.color, "Protect a player with your shield", "Protect a player with your shield", RoleId.Squire);
        public static RoleInfo cheater = new RoleInfo("Cheater", Cheater.color, "Swap the votes of two players", "Swap the votes of two players", RoleId.Cheater);
        public static RoleInfo fortuneTeller = new RoleInfo("Fortune Teller", FortuneTeller.color, "Reveal who are the <color=#FF0000FF>Impostors</color>", "Reveal who are the <color=#FF0000FF>Impostors</color>", RoleId.FortuneTeller);
        public static RoleInfo hacker = new RoleInfo("Hacker", Hacker.color, "Use Admin and Vitals from anywhere", "Use Admin and Vitals from anywhere", RoleId.Hacker);
        public static RoleInfo sleuth = new RoleInfo("Sleuth", Sleuth.color, "Track down a player and corpses", "Track down a player and corpses", RoleId.Sleuth);
        public static RoleInfo fink = new RoleInfo("Fink", Fink.color, "Finish your tasks to reveal the <color=#FF0000FF>Impostors</color>", "Finish your tasks to reveal the <color=#FF0000FF>Impostors</color>", RoleId.Fink);
        public static RoleInfo kid = new RoleInfo("Kid", Kid.color, "Everyone lose if you die or get voted out", "Everyone lose if you die or get voted out", RoleId.Kid);
        public static RoleInfo welder = new RoleInfo("Welder", Welder.color, "Seal vents", "Seal vents", RoleId.Welder);
        public static RoleInfo spiritualist = new RoleInfo("Spiritualist", Spiritualist.color, "Sacrifice yourself to revive a player", "Sacrifice yourself to revive a player", RoleId.Spiritualist);
        public static RoleInfo theChosenOne = new RoleInfo("The Chosen One", TheChosenOne.color, "Force your killer to report your body", "Force your killer to report your body", RoleId.TheChosenOne);
        public static RoleInfo vigilant = new RoleInfo("Vigilant", Vigilant.color, "Put additional cameras on the map", "Put additional cameras on the map", RoleId.Vigilant);
        public static RoleInfo vigilantMira = new RoleInfo("Vigilant", Vigilant.color, "Activate remote Doorlog with Q key", "Activate remote Doorlog with Q key", RoleId.VigilantMira);
        public static RoleInfo performer = new RoleInfo("Performer", Performer.color, "Your dead will trigger an alarm and reveal where your body is", "Your dead will trigger an alarm and reveal where your body is", RoleId.Performer);
        public static RoleInfo hunter = new RoleInfo("Hunter", Hunter.color, "Mark a player to die if you get killed", "Mark a player to die if you get killed", RoleId.Hunter);
        public static RoleInfo jinx = new RoleInfo("Jinx", Jinx.color, "Jinx players abilities", "Jinx players abilities", RoleId.Jinx);
        public static RoleInfo impostor = new RoleInfo("Impostor", Palette.ImpostorRed, Helpers.cs(Palette.ImpostorRed, "Sabotage and kill everyone"), "Sabotage and kill everyone", RoleId.Impostor);
        public static RoleInfo crewmate = new RoleInfo("Crewmate", Kid.color, "Find and exile the <color=#FF0000FF>Impostors</color>", "Find and exile the <color=#FF0000FF>Impostors</color>", RoleId.Crewmate);
        public static RoleInfo lighter = new RoleInfo("Lighter", Modifiers.color, "You have more vision", "You have more vision", RoleId.Lighter);
        public static RoleInfo blind = new RoleInfo("Blind", Modifiers.color, "You have less vision", "You have less vision", RoleId.Blind);
        public static RoleInfo flash = new RoleInfo("Flash", Modifiers.color, "You're faster", "You're faster", RoleId.Flash);
        public static RoleInfo bigchungus = new RoleInfo("Big Chungus", Modifiers.color, "You're bigger and slower", "You're bigger and slower", RoleId.BigChungus);
        public static RoleInfo lover = new RoleInfo("Lover", Modifiers.loverscolor, $"♥Survive as a couple with your partner♥", $"♥Survive as a couple with your partner♥", RoleId.Lover);
        public static RoleInfo badlover = new RoleInfo("Loverstor", Palette.ImpostorRed, $"<color=#FF00D1FF>♥Survive as a couple with your partner♥. </color><color=#FF1919FF>Kill the rest</color>", $"<color=#FF00D1FF>♥Survive as a couple with your partner♥. </color><color=#FF1919FF>Kill the rest</color>", RoleId.Lover);


        public static List<RoleInfo> allRoleInfos = new List<RoleInfo>() {
            impostor,
            mimic,
            painter,
            demon,
            janitor,
            ilusionist,
            manipulator,
            bomberman,
            chameleon,
            gambler,
            sorcerer,
            renegade,
            minion,
            bountyHunter,
            trapper,
            yinyanger,
            challenger,
            joker,
            rolethief,
            pyromaniac,
            treasureHunter,
            devourer,
            crewmate,
            captain,
            mechanic,
            sheriff,
            detective,
            forensic,
            timeTraveler,
            squire,
            cheater,
            fortuneTeller,
            hacker,
            sleuth,
            fink,
            welder,
            spiritualist,
            theChosenOne,
            vigilant,
            vigilantMira,
            performer,
            kid,
            hunter,
            jinx,
            lighter,
            blind,
            flash,
            bigchungus,
            lover,
            badlover,
            redplayer01,
            redplayer02,
            redplayer03,
            redplayer04,
            redplayer05,
            redplayer06,
            redplayer07,
            blueplayer01,
            blueplayer02,
            blueplayer03,
            blueplayer04,
            blueplayer05,
            blueplayer06,
            blueplayer07,
            blueplayer08,
            policeplayer01,
            policeplayer02,
            policeplayer03,
            policeplayer04,
            policeplayer05,
            thiefplayer01,
            thiefplayer02,
            thiefplayer03,
            thiefplayer04,
            thiefplayer05,
            thiefplayer06,
            thiefplayer07,
            thiefplayer08,
            thiefplayer09,
            thiefplayer10,
            greenKing,
            greenplayer01,
            greenplayer02,
            greenplayer03,
            greenplayer04,
            greenplayer05,
            greenplayer06,
            yellowKing,
            yellowplayer01,
            yellowplayer02,
            yellowplayer03,
            yellowplayer04,
            yellowplayer05,
            yellowplayer06,
            yellowplayer07
        };

        public static List<RoleInfo> getRoleInfoForPlayer(PlayerControl p) {
            List<RoleInfo> infos = new List<RoleInfo>();
            if (p == null) return infos;

            // Capture the Flag
            if (p == CaptureTheFlag.redplayer01) infos.Add(redplayer01);
            if (p == CaptureTheFlag.redplayer02) infos.Add(redplayer02);
            if (p == CaptureTheFlag.redplayer03) infos.Add(redplayer03);
            if (p == CaptureTheFlag.redplayer04) infos.Add(redplayer04);
            if (p == CaptureTheFlag.redplayer05) infos.Add(redplayer05);
            if (p == CaptureTheFlag.redplayer06) infos.Add(redplayer06);
            if (p == CaptureTheFlag.redplayer07) infos.Add(redplayer07);
            if (p == CaptureTheFlag.blueplayer01) infos.Add(blueplayer01);
            if (p == CaptureTheFlag.blueplayer02) infos.Add(blueplayer02);
            if (p == CaptureTheFlag.blueplayer03) infos.Add(blueplayer03);
            if (p == CaptureTheFlag.blueplayer04) infos.Add(blueplayer04);
            if (p == CaptureTheFlag.blueplayer05) infos.Add(blueplayer05);
            if (p == CaptureTheFlag.blueplayer06) infos.Add(blueplayer06);
            if (p == CaptureTheFlag.blueplayer07) infos.Add(blueplayer07);
            if (p == CaptureTheFlag.blueplayer08) infos.Add(blueplayer08);

            // Police and Thief
            if (p == PoliceAndThief.policeplayer01) infos.Add(policeplayer01);
            if (p == PoliceAndThief.policeplayer02) infos.Add(policeplayer02);
            if (p == PoliceAndThief.policeplayer03) infos.Add(policeplayer03);
            if (p == PoliceAndThief.policeplayer04) infos.Add(policeplayer04);
            if (p == PoliceAndThief.policeplayer05) infos.Add(policeplayer05);
            if (p == PoliceAndThief.thiefplayer01) infos.Add(thiefplayer01);
            if (p == PoliceAndThief.thiefplayer02) infos.Add(thiefplayer02);
            if (p == PoliceAndThief.thiefplayer03) infos.Add(thiefplayer03);
            if (p == PoliceAndThief.thiefplayer04) infos.Add(thiefplayer04);
            if (p == PoliceAndThief.thiefplayer05) infos.Add(thiefplayer05);
            if (p == PoliceAndThief.thiefplayer06) infos.Add(thiefplayer06);
            if (p == PoliceAndThief.thiefplayer07) infos.Add(thiefplayer07);
            if (p == PoliceAndThief.thiefplayer08) infos.Add(thiefplayer08);
            if (p == PoliceAndThief.thiefplayer09) infos.Add(thiefplayer09);
            if (p == PoliceAndThief.thiefplayer10) infos.Add(thiefplayer10);

            // King of the hill
            if (p == KingOfTheHill.greenKingplayer) infos.Add(greenKing);
            if (p == KingOfTheHill.greenplayer01) infos.Add(greenplayer01);
            if (p == KingOfTheHill.greenplayer02) infos.Add(greenplayer02);
            if (p == KingOfTheHill.greenplayer03) infos.Add(greenplayer03);
            if (p == KingOfTheHill.greenplayer04) infos.Add(greenplayer04);
            if (p == KingOfTheHill.greenplayer05) infos.Add(greenplayer05);
            if (p == KingOfTheHill.greenplayer06) infos.Add(greenplayer06);
            if (p == KingOfTheHill.yellowKingplayer) infos.Add(yellowKing);
            if (p == KingOfTheHill.yellowplayer01) infos.Add(yellowplayer01);
            if (p == KingOfTheHill.yellowplayer02) infos.Add(yellowplayer02);
            if (p == KingOfTheHill.yellowplayer03) infos.Add(yellowplayer03);
            if (p == KingOfTheHill.yellowplayer04) infos.Add(yellowplayer04);
            if (p == KingOfTheHill.yellowplayer05) infos.Add(yellowplayer05);
            if (p == KingOfTheHill.yellowplayer06) infos.Add(yellowplayer06);
            if (p == KingOfTheHill.yellowplayer07) infos.Add(yellowplayer07);


            // Impostor roles
            if (p == Mimic.mimic) infos.Add(mimic);
            if (p == Painter.painter) infos.Add(painter);
            if (p == Demon.demon) infos.Add(demon);
            if (p == Ilusionist.ilusionist) infos.Add(ilusionist);
            if (p == Janitor.janitor) infos.Add(janitor);
            if (p == Manipulator.manipulator) infos.Add(manipulator);
            if (p == Bomberman.bomberman) infos.Add(bomberman);
            if (p == Chameleon.chameleon) infos.Add(chameleon);
            if (p == Gambler.gambler) infos.Add(gambler);
            if (p == Sorcerer.sorcerer) infos.Add(sorcerer);

            // Rebels roles
            if (p == Renegade.renegade || (Renegade.formerRenegades != null && Renegade.formerRenegades.Any(x => x.PlayerId == p.PlayerId))) infos.Add(renegade);
            if (p == Minion.minion) infos.Add(minion);
            if (p == BountyHunter.bountyhunter) infos.Add(bountyHunter);
            if (p == Trapper.trapper) infos.Add(trapper);
            if (p == Yinyanger.yinyanger) infos.Add(yinyanger);
            if (p == Challenger.challenger) infos.Add(challenger);

            // Neutral roles
            if (p == Joker.joker) infos.Add(joker);
            if (p == RoleThief.rolethief) infos.Add(rolethief);
            if (p == Pyromaniac.pyromaniac) infos.Add(pyromaniac);
            if (p == TreasureHunter.treasureHunter) infos.Add(treasureHunter);
            if (p == Devourer.devourer) infos.Add(devourer);

            // Crewmate roles
            if (p == Captain.captain) infos.Add(captain);
            if (p == Mechanic.mechanic) infos.Add(mechanic);
            if (p == Sheriff.sheriff) infos.Add(sheriff);
            if (p == Detective.detective) infos.Add(detective);
            if (p == Forensic.forensic) infos.Add(forensic);
            if (p == TimeTraveler.timeTraveler) infos.Add(timeTraveler);
            if (p == Squire.squire) infos.Add(squire);
            if (p == Cheater.cheater) infos.Add(cheater);
            if (p == FortuneTeller.fortuneTeller) infos.Add(fortuneTeller);
            if (p == Hacker.hacker) infos.Add(hacker);
            if (p == Sleuth.sleuth) infos.Add(sleuth);
            if (p == Fink.fink) infos.Add(fink);
            if (p == Kid.kid) infos.Add(kid);
            if (p == Welder.welder) infos.Add(welder);
            if (p == Spiritualist.spiritualist) infos.Add(spiritualist);
            if (p == TheChosenOne.theChosenOne) infos.Add(theChosenOne);
            if (p == Vigilant.vigilant) infos.Add(vigilant);
            if (p == Vigilant.vigilantMira) infos.Add(vigilantMira);
            if (p == Performer.performer) infos.Add(performer);
            if (p == Hunter.hunter) infos.Add(hunter);
            if (p == Jinx.jinx) infos.Add(jinx);

            // Modifier
            if (p == Modifiers.lighter) infos.Add(lighter);
            if (p == Modifiers.blind) infos.Add(blind);
            if (p == Modifiers.flash) infos.Add(flash);
            if (p == Modifiers.bigchungus) infos.Add(bigchungus);
            if (p == Modifiers.lover1 || p == Modifiers.lover2) infos.Add(p.Data.Role.IsImpostor ? badlover : lover);

            // Default roles
            if (infos.Count == 0 && p.Data.Role.IsImpostor) infos.Add(impostor); // Just Impostor
            if (infos.Count == 0 && !p.Data.Role.IsImpostor) infos.Add(crewmate); // Just Crewmate

            return infos;
        }

        public static String GetRolesString(PlayerControl p, bool useColors) {
            string roleName;
            roleName = String.Join(" ", getRoleInfoForPlayer(p).Select(x => useColors ? Helpers.cs(x.color, x.name) : x.name).ToArray());
            if (roleName.Contains("Lover")) roleName.Replace("Lover", "");
            return roleName;
        }

        public class RoleFortuneTellerInfo
        {
            public Color color;
            public string name;
            public bool isGood;

            RoleFortuneTellerInfo(Color color, string name, bool isGood) {
                this.color = color;
                this.name = name;
                this.isGood = isGood;
            }

            public static RoleFortuneTellerInfo getFortuneTellerRoleInfoForPlayer(PlayerControl p) {
                string name = "";
                bool isGood = true;
                Color color = Color.white;

                if (Captain.captain != null && p == Captain.captain) {
                    name = "Captain";
                    color = Captain.color;
                }
                else if (Mechanic.mechanic != null && p == Mechanic.mechanic) {
                    name = "Mechanic";
                    color = Mechanic.color;
                }
                else if (Sheriff.sheriff != null && p == Sheriff.sheriff) {
                    name = "Sheriff";
                    color = Sheriff.color;
                }
                else if (Detective.detective != null && p == Detective.detective) {
                    name = "Detective";
                    color = Detective.color;
                }
                else if (Forensic.forensic != null && p == Forensic.forensic) {
                    name = "Forensic";
                    color = Forensic.color;
                }
                else if (TimeTraveler.timeTraveler != null && p == TimeTraveler.timeTraveler) {
                    name = "Time Traveler";
                    color = TimeTraveler.color;
                }
                else if (Squire.squire != null && p == Squire.squire) {
                    name = "Squire";
                    color = Squire.color;
                }
                else if (Cheater.cheater != null && p == Cheater.cheater) {
                    name = "Cheater";
                    color = Cheater.color;
                }
                else if (FortuneTeller.fortuneTeller != null && p == FortuneTeller.fortuneTeller) {
                    name = "Fortune Teller";
                    color = FortuneTeller.color;
                }
                else if (Hacker.hacker != null && p == Hacker.hacker) {
                    name = "Hacker";
                    color = Hacker.color;
                }
                else if (Sleuth.sleuth != null && p == Sleuth.sleuth) {
                    name = "Sleuth";
                    color = Sleuth.color;
                }
                else if (Fink.fink != null && p == Fink.fink) {
                    name = "Fink";
                    color = Fink.color;
                }
                else if (Kid.kid != null && p == Kid.kid) {
                    name = "Kid";
                    color = Kid.color;
                }
                else if (Welder.welder != null && p == Welder.welder) {
                    name = "Welder";
                    color = Welder.color;
                }
                else if (Spiritualist.spiritualist != null && p == Spiritualist.spiritualist) {
                    name = "Spiritualist";
                    color = Spiritualist.color;
                }
                else if (TheChosenOne.theChosenOne != null && p == TheChosenOne.theChosenOne) {
                    name = "The Chosen One";
                    color = TheChosenOne.color;
                }
                else if (Vigilant.vigilant != null && p == Vigilant.vigilant) {
                    name = "Vigilant";
                    color = Vigilant.color;
                }
                else if (Vigilant.vigilantMira != null && p == Vigilant.vigilantMira) {
                    name = "Vigilant";
                    color = Vigilant.color;
                }
                else if (Performer.performer != null && p == Performer.performer) {
                    name = "Performer";
                    color = Performer.color;
                }
                else if (Hunter.hunter != null && p == Hunter.hunter) {
                    name = "Hunter";
                    color = Hunter.color;
                }
                else if (Jinx.jinx != null && p == Jinx.jinx) {
                    name = "Jinx";
                    color = Jinx.color;
                }
                else if (Mimic.mimic != null && p == Mimic.mimic) {
                    name = "Mimic";
                    color = Mimic.color;
                    isGood = false;
                }
                else if (Painter.painter != null && p == Painter.painter) {
                    name = "Painter";
                    color = Painter.color;
                    isGood = false;
                }
                else if (Demon.demon != null && p == Demon.demon) {
                    name = "Demon";
                    color = Demon.color;
                    isGood = false;
                }
                else if (Ilusionist.ilusionist != null && p == Ilusionist.ilusionist) {
                    name = "Ilusionist";
                    color = Ilusionist.color;
                    isGood = false;
                }
                else if (Janitor.janitor != null && p == Janitor.janitor) {
                    name = "Janitor";
                    color = Janitor.color;
                    isGood = false;
                }
                else if (Manipulator.manipulator != null && p == Manipulator.manipulator) {
                    name = "Manipulator";
                    color = Manipulator.color;
                    isGood = false;
                }
                else if (Bomberman.bomberman != null && p == Bomberman.bomberman) {
                    name = "Bomberman";
                    color = Bomberman.color;
                    isGood = false;
                }
                else if (Chameleon.chameleon != null && p == Chameleon.chameleon) {
                    name = "Chameleon";
                    color = Palette.ImpostorRed;
                    isGood = false;
                }
                else if (Gambler.gambler != null && p == Gambler.gambler) {
                    name = "Gambler";
                    color = Palette.ImpostorRed;
                    isGood = false;
                }
                else if (Sorcerer.sorcerer != null && p == Sorcerer.sorcerer) {
                    name = "Sorcerer";
                    color = Palette.ImpostorRed;
                    isGood = false;
                }
                else if (Renegade.renegade != null && p == Renegade.renegade) {
                    name = "Renegade";
                    color = Renegade.color;
                    isGood = false;
                }
                else if (Minion.minion != null && p == Minion.minion) {
                    name = "Minion";
                    color = Minion.color;
                    isGood = false;
                }
                else if (BountyHunter.bountyhunter != null && p == BountyHunter.bountyhunter) {
                    name = "Bounty Hunter";
                    color = BountyHunter.color;
                    isGood = false;
                }
                else if (Trapper.trapper != null && p == Trapper.trapper) {
                    name = "Trapper";
                    color = Trapper.color;
                    isGood = false;
                }
                else if (Yinyanger.yinyanger != null && p == Yinyanger.yinyanger) {
                    name = "Yinyanger";
                    color = Yinyanger.color;
                    isGood = false;
                }
                else if (Challenger.challenger != null && p == Challenger.challenger) {
                    name = "Challenger";
                    color = Challenger.color;
                    isGood = false;
                }
                else if (Joker.joker != null && p == Joker.joker) {
                    name = "Joker";
                    color = Joker.color;
                    isGood = false;
                }
                else if (RoleThief.rolethief != null && p == RoleThief.rolethief) {
                    name = "Role Thief";
                    color = RoleThief.color;
                    isGood = false;
                }
                else if (Pyromaniac.pyromaniac != null && p == Pyromaniac.pyromaniac) {
                    name = "Pyromaniac";
                    color = Pyromaniac.color;
                    isGood = false;
                }
                else if (TreasureHunter.treasureHunter != null && p == TreasureHunter.treasureHunter) {
                    name = "Treasure Hunter";
                    color = TreasureHunter.color;
                    isGood = false;
                }
                else if (Devourer.devourer != null && p == Devourer.devourer) {
                    name = "Devourer";
                    color = Devourer.color;
                    isGood = false;
                }
                else if (p.Data.Role.IsImpostor) { // Just Impostor
                    name = "Impostor";
                    color = Palette.ImpostorRed;
                    isGood = false;
                }
                else { // Just Crewmate
                    name = "Crewmate";
                    color = Kid.color;
                }

                return new RoleFortuneTellerInfo(
                    color,
                    name,
                    isGood
                );
            }
        }
    }
}
