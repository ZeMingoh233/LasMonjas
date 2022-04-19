using HarmonyLib;
using LasMonjas.Core;

namespace LasMonjas.Patches
{
    [HarmonyPatch(typeof(MainMenuManager), nameof(MainMenuManager.Start))]
    public class MainMenuPatch
    {
        private static void Prefix(MainMenuManager __instance) {
            // Check the music option after loading main menu screen, so when you join the Lobby it starts playing if enabled
            MapOptions.checkMusic();
        }
    }

    [HarmonyPatch(typeof(Constants), nameof(Constants.ShouldHorseAround))]
    public static class ConstPatch
    {
        public static void Postfix(ref bool __result) {
            __result = LasMonjasPlugin.HorseMode.Value;
        }
    }
}