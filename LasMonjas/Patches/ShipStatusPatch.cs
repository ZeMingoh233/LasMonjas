using HarmonyLib;
using static LasMonjas.LasMonjas;
using UnityEngine;

namespace LasMonjas.Patches
{

    [HarmonyPatch(typeof(ShipStatus))]
    public class ShipStatusPatch
    {

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.CalculateLightRadius))]
        public static bool Prefix(ref float __result, ShipStatus __instance, [HarmonyArgument(0)] GameData.PlayerInfo player) {
            ISystemType systemType = __instance.Systems.ContainsKey(SystemTypes.Electrical) ? __instance.Systems[SystemTypes.Electrical] : null;
            if (systemType == null) return true;
            SwitchSystem switchSystem = systemType.TryCast<SwitchSystem>();
            if (switchSystem == null) return true;

            float num = (float)switchSystem.Value / 255f;

            if (player == null || player.IsDead) // IsDead
                __result = __instance.MaxLightRadius;
            else if (Modifiers.blind != null && Modifiers.blind.PlayerId == player.PlayerId && Ilusionist.lightsOutTimer <= 0f) {// if player is Blind
                __result = Mathf.Lerp(__instance.MinLightRadius, __instance.MaxLightRadius, num) * PlayerControl.GameOptions.CrewLightMod * 0.75f;
            }
            else if (player.Role.IsImpostor
                || (Renegade.renegade != null && Renegade.renegade.PlayerId == player.PlayerId  && Ilusionist.lightsOutTimer <= 0f)
                || (Minion.minion != null && Minion.minion.PlayerId == player.PlayerId  && Ilusionist.lightsOutTimer <= 0f)) // Impostor, Renegade/Minion
                __result = __instance.MaxLightRadius * PlayerControl.GameOptions.ImpostorLightMod;
            else if (Modifiers.lighter != null && Modifiers.lighter.PlayerId == player.PlayerId && Ilusionist.lightsOutTimer <= 0f) {// if player is Lighter modifier
                __result = Mathf.Lerp(__instance.MaxLightRadius * 0.75f, __instance.MaxLightRadius * 2, num);
            }
            else if (Ilusionist.ilusionist != null && Ilusionist.lightsOutTimer > 0f) {
                float lerpValue = 1f;
                if (Ilusionist.lightsOutDuration - Ilusionist.lightsOutTimer < 0.5f) lerpValue = Mathf.Clamp01((Ilusionist.lightsOutDuration - Ilusionist.lightsOutTimer) * 2);
                else if (Ilusionist.lightsOutTimer < 0.5) lerpValue = Mathf.Clamp01(Ilusionist.lightsOutTimer * 2);
                __result = Mathf.Lerp(__instance.MinLightRadius, __instance.MaxLightRadius, 1 - lerpValue) * PlayerControl.GameOptions.CrewLightMod;
            }
            else
                __result = Mathf.Lerp(__instance.MinLightRadius, __instance.MaxLightRadius, num) * PlayerControl.GameOptions.CrewLightMod;
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.IsGameOverDueToDeath))]
        public static void Postfix2(ShipStatus __instance, ref bool __result) {
            __result = false;
        }

    }

}