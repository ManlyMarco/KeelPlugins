﻿using BepInEx;
using BepInEx.Configuration;
using KeelPlugins.Utils;
using UnityEngine;

namespace LockOnPlugin.Core
{
    public abstract class LockOnPluginCore : BaseUnityPlugin
    {
        public const string GUID = "keelhauled.lockonplugin";
        public const string PluginName = "LockOnPlugin";

        private const string SECTION_HOTKEYS = "Keyboard shortcuts";
        private const string SECTION_GENERAL = "General";

        private const string DESCRIPTION_TRACKSPEED = "The speed at which the target is followed.";
        private const string DESCRIPTION_SCROLLMALES = "Choose whether to include males in the rotation when switching between characters using the hotkeys from the plugin.";
        private const string DESCRIPTION_LEASHLENGTH = "The amount of slack allowed when tracking.";
        private const string DESCRIPTION_AUTOLOCK = "Lock on automatically after switching characters.";
        private const string DESCRIPTION_SHOWINFOMSG = "Show various messages about the plugin on screen.";

        internal static ConfigEntry<float> TrackingSpeedNormal { get; set; }
        internal static ConfigEntry<bool> ScrollThroughMalesToo { get; set; }
        internal static ConfigEntry<bool> ShowInfoMsg { get; set; }
        internal static ConfigEntry<float> LockLeashLength { get; set; }
        internal static ConfigEntry<bool> AutoSwitchLock { get; set; }
        internal static ConfigEntry<KeyboardShortcut> LockOnKey { get; set; }
        internal static ConfigEntry<KeyboardShortcut> PrevCharaKey { get; set; }
        internal static ConfigEntry<KeyboardShortcut> NextCharaKey { get; set; }

        internal static GameObject bepinex;

        protected virtual void Awake()
        {
            bepinex = gameObject;
            Log.SetLogSource(Logger);

            TargetData.LoadData();

            TrackingSpeedNormal = Config.Bind(SECTION_GENERAL, "Tracking speed", 0.1f, new ConfigDescription(DESCRIPTION_TRACKSPEED, new AcceptableValueRange<float>(0.01f, 0.3f), new ConfigurationManagerAttributes { Order = 10 }));
            ScrollThroughMalesToo = Config.Bind(SECTION_GENERAL, "Scroll through males too", true, new ConfigDescription(DESCRIPTION_SCROLLMALES, null, new ConfigurationManagerAttributes { Order = 8 }));
            ShowInfoMsg = Config.Bind(SECTION_GENERAL, "Show info messages", false, new ConfigDescription(DESCRIPTION_SHOWINFOMSG, null, new ConfigurationManagerAttributes { Order = 6 }));
            LockLeashLength = Config.Bind(SECTION_GENERAL, "Leash length", 0f, new ConfigDescription(DESCRIPTION_LEASHLENGTH, new AcceptableValueRange<float>(0f, 0.5f), new ConfigurationManagerAttributes { Order = 9 }));
            AutoSwitchLock = Config.Bind(SECTION_GENERAL, "Auto switch lock", false, new ConfigDescription(DESCRIPTION_AUTOLOCK, null, new ConfigurationManagerAttributes { Order = 7 }));

            LockOnKey = Config.Bind(SECTION_HOTKEYS, "Lock on", new KeyboardShortcut(KeyCode.Mouse4), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 10 }));
            PrevCharaKey = Config.Bind(SECTION_HOTKEYS, "Select previous character", new KeyboardShortcut(KeyCode.None), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 8 }));
            NextCharaKey = Config.Bind(SECTION_HOTKEYS, "Select next character", new KeyboardShortcut(KeyCode.None), new ConfigDescription("", null, new ConfigurationManagerAttributes { Order = 9 }));
        }
    }
}
