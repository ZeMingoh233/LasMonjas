using System;
using System.Collections.Generic;
using System.Text;
using BepInEx;
using BepInEx.IL2CPP;
using HarmonyLib;
using BepInEx.Logging;
using UnityEngine;

// Adapted from https://github.com/xxomega77xx/HatPack

namespace LasMonjas.Core
{
    class CustomHats
    {
        public static Material MagicShader;

        public struct AuthorData
        {
            public string AuthorName;
            public string HatName;
            public string FloorHatName;
            public string ClimbHatName;
            public string LeftImageName;
            public bool NoBounce;
            public bool altShader;
        }

        public static List<AuthorData> authorDatas = new List<AuthorData>()
        {
            new AuthorData {AuthorName = "Allul", HatName = "Monja", NoBounce = false},
            new AuthorData {AuthorName = "Allul", HatName = "Minion Monja", FloorHatName ="Minion Monja Climb", ClimbHatName = "Minion Monja Climb", LeftImageName = "Minion Monja", NoBounce = false},
            new AuthorData {AuthorName = "Sensei", HatName = "Cursed Monja", NoBounce = false},
            new AuthorData {AuthorName = "Sensei", HatName = "Abombg Man", NoBounce = false, altShader = true},
            new AuthorData {AuthorName = "Sensei", HatName = "Among Ass", NoBounce = false, altShader = true},
            new AuthorData {AuthorName = "Sensei", HatName = "Time To Duel", NoBounce = false, altShader = true},
            new AuthorData {AuthorName = "Sensei", HatName = "Medusa", NoBounce = false},
            new AuthorData {AuthorName = "Sensei", HatName = "Mega Hat", NoBounce = false, altShader = true},
            new AuthorData {AuthorName = "Sensei", HatName = "Over 9 Sus", NoBounce = false, altShader = true},
            new AuthorData {AuthorName = "Sensei", HatName = "Egyptian", NoBounce = false },
            new AuthorData {AuthorName = "Sensei", HatName = "Fortune Teller", NoBounce = false, altShader = true},
            new AuthorData {AuthorName = "Sensei", HatName = "Joker", NoBounce = false },
            new AuthorData {AuthorName = "Sensei", HatName = "SrCobra", NoBounce = false, altShader = true},
            new AuthorData {AuthorName = "Sensei", HatName = "The Eye", NoBounce = false, altShader = true},
            new AuthorData {AuthorName = "Sensei", HatName = "Alien", NoBounce = false, altShader = true},
            new AuthorData {AuthorName = "Sensei", HatName = "Dinoseto", NoBounce = false, altShader = true},
            new AuthorData {AuthorName = "Sensei", HatName = "Super Red Sus", NoBounce = false},
            new AuthorData {AuthorName = "Sensei", HatName = "Super Green Sus", NoBounce = false},
            new AuthorData {AuthorName = "Sensei", HatName = "Super Yellow Sus", NoBounce = false},
            new AuthorData {AuthorName = "Sensei", HatName = "Super Purple Sus", NoBounce = false},
            new AuthorData {AuthorName = "Sensei", HatName = "Chadsito", NoBounce = true, altShader = true},
            new AuthorData {AuthorName = "Sensei", HatName = "Scars", NoBounce = true, altShader = true},
            new AuthorData {AuthorName = "Sensei", HatName = "Sus Man", NoBounce = true},
            new AuthorData {AuthorName = "Sensei", HatName = "Take It Easy", NoBounce = true},
            new AuthorData {AuthorName = "Xago", HatName = "World Destroyer", NoBounce = true},
        };

        internal static Dictionary<int, AuthorData> IdToData = new Dictionary<int, AuthorData>();

        private static bool _customHatsLoaded = false;
        [HarmonyPatch(typeof(HatManager), nameof(HatManager.GetHatById))]
        public static class AddCustomHats
        {

            public static void Prefix(PlayerControl __instance) {

                if (!_customHatsLoaded) {
                    var allHats = HatManager.Instance.AllHats;

                    foreach (var data in authorDatas) {
                        HatID++;

                        if (data.FloorHatName != null && data.ClimbHatName != null && data.LeftImageName != null) {
                            if (data.NoBounce) {
                                if (data.altShader == true) {
                                    allHats.Add(CreateHat(GetSprite(data.HatName), data.AuthorName, GetSprite(data.ClimbHatName), GetSprite(data.FloorHatName), null, true, true));
                                }
                                else {
                                    allHats.Add(CreateHat(GetSprite(data.HatName), data.AuthorName, GetSprite(data.ClimbHatName), GetSprite(data.FloorHatName), GetSprite(data.LeftImageName), true, false));
                                }
                            }
                            else {
                                allHats.Add(CreateHat(GetSprite(data.HatName), data.AuthorName, GetSprite(data.ClimbHatName), GetSprite(data.FloorHatName), GetSprite(data.LeftImageName)));
                            }

                        }
                        else {
                            if (data.altShader == true) {
                                allHats.Add(CreateHat(GetSprite(data.HatName), data.AuthorName, null, null, null, false, true));
                            }
                            else {
                                allHats.Add(CreateHat(GetSprite(data.HatName), data.AuthorName));
                            }
                        }
                        IdToData.Add(HatManager.Instance.AllHats.Count - 1, data);

                        _customHatsLoaded = true;
                    }



                    _customHatsLoaded = true;
                }
            }

            public static Sprite GetSprite(string name)
                => AssetLoader.LoadAsset(name).Cast<GameObject>().GetComponent<SpriteRenderer>().sprite;

            public static int HatID = 0;
            /// <summary>
            /// Creates hat based on specified values
            /// </summary>
            /// <param name="sprite"></param>
            /// <param name="author"></param>
            /// <param name="climb"></param>
            /// <param name="floor"></param>
            /// <param name="leftimage"></param>
            /// <param name="bounce"></param>
            /// <param name="altshader"></param>
            /// <returns>HatBehaviour</returns>
            private static HatBehaviour CreateHat(Sprite sprite, string author, Sprite climb = null, Sprite floor = null, Sprite leftimage = null, bool bounce = false, bool altshader = false) {
                //Borrowed from Other Roles to get hats alt shaders to work
                if (MagicShader == null && DestroyableSingleton<HatManager>.InstanceExists) {
                    foreach (HatBehaviour h in DestroyableSingleton<HatManager>.Instance.AllHats) {
                        if (h.AltShader != null) {
                            MagicShader = h.AltShader;
                            break;
                        }
                    }
                }
                var newHat = ScriptableObject.CreateInstance<HatBehaviour>();
                newHat.name = $"{sprite.name} (by {author})";
                newHat.MainImage = sprite;
                newHat.ProductId = "hat_" + sprite.name.Replace(' ', '_');
                newHat.Order = 99 + HatID;
                newHat.InFront = true;
                newHat.NoBounce = bounce;
                newHat.FloorImage = floor;
                newHat.ClimbImage = climb;
                newHat.Free = true;
                newHat.LeftMainImage = leftimage;
                newHat.ChipOffset = new Vector2(-0.1f, 0.4f);
                if (altshader == true) { newHat.AltShader = MagicShader; }

                return newHat;
            }
        }
    }
}