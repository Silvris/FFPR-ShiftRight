using BepInEx.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFPR_ShiftRight
{
    public class Configuration
    {
        //BepInEx configs use ConfigEntry objects to bind to the config file
        private static string Group = "ShiftRight";
        public ConfigEntry<bool> configOne { get; set; }

        public Configuration(ConfigFile file)
        {
            //file is the ConfigFile that BasePlugin exposes, you can feed it to it with EntryPoint.Instance.Config
            configOne = file.Bind(new ConfigDefinition(Group, nameof(configOne)), true, new ConfigDescription("This is the description for the config entry."));
            //To limit the possible values for the ConfigEntry, you give the ConfigDescription constructor
            //a class inheriting AcceptableValueBase (normally AcceptableValueList or AcceptableValueRange)

            //To have a function run after the value is changed, subscribe it to the ConfigEntry.SettingChanged event
            configOne.SettingChanged += wc_SettingsChanged;
        }
        public static void wc_SettingsChanged(object sender, EventArgs e)
        {
            ModComponent.Log.LogInfo("Setting changed!");
        }
        //you access the value of the ConfigEntry by accessing ConfigEntry.Value, but it might be easier to set up properties to grab it for you
        public bool ConfigOne => configOne.Value;
    }
}
