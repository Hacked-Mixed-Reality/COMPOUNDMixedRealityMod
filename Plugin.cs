using System;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;
using Valve.VR;

namespace MixedRealityMod
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource _logger;

        private void Awake()
        {
            _logger = Logger;
            _logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            var harmony = new Harmony("mixedreality.patch.compound");
            harmony.PatchAll();
        }


        [HarmonyPatch(typeof(Valve.VR.SteamVR_ExternalCamera))]
        static class ExternalCameraPatches
        {
            [HarmonyPrefix]
            [HarmonyPatch("OnEnable")]
            public static bool OnEnable_Patch(ref SteamVR_ExternalCamera __instance)
            {
                _logger.LogInfo("Fix Black Headset");
                AccessTools.Method(null, "AutoEnableActionSet").Invoke(__instance, null);
                return false;
            }

        }
    }



}
