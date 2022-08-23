using Newtonsoft.Json;
using ONI_AsteroidBelt_2.AsData.BuildingData;
using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsAttributes;
using ONI_AsteroidBelt_2.Common.AsLoader;
using PeterHan.PLib.Options;
using ProcGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsUI.PLibUI
{
    [ModInfo("", "", false)]
    [ConfigFile("config.json", true, false)]
    internal class PLibMenu
    {
        [Load]
        private static void Load()
        {
            new POptions().RegisterOptions(Entrance.Instance, typeof(PLibMenu));

            void act(object o) { PLibMenu.Reload(); }

            EventCentre.Subscribe(EventInventory.RefreshUIData, act);

            Reload();
        }

        public static PLibMenu Instance = POptions.ReadSettings<PLibMenu>() ?? new PLibMenu();

        public static void Reload()
        {
            Instance = POptions.ReadSettings<PLibMenu>() ?? new PLibMenu();

            HabitatData.Width = Instance.SpaceShipWidth;

            HabitatData.Height = Instance.SpaceShipHight;

            HabitatData.ZoneType = SwitchToSubWorldZoneType(Instance.InteriorzoneType);
        }

        [JsonIgnore]
        [Option("Select World", "", null)]
        public Action<object> CommonCarePackage
        {
            get
            {
                return new Action<object>((o) => { EventCentre.Trigger(EventInventory.OpenSelectWorldUI, null); });
            }
        }

        [Option("Width", " It will be laggy if habitat is too big", "SpaceShip")]
        [JsonProperty]
        [Limit(20, 100)]
        public int SpaceShipWidth { get; set; } = 40;

        [Option("Hight", " It will be laggy if habitat is too big", "SpaceShip")]
        [JsonProperty]
        [Limit(20, 100)]
        public int SpaceShipHight { get; set; } = 40;

        [Option("Background", "Get boring with the defult background?", "SpaceShip")]
        [JsonProperty]
        public ZoneType InteriorzoneType { get; set; } = ZoneType.RocketInterior;

        public enum ZoneType
        {
            FrozenWastes,
            CrystalCaverns,
            BoggyMarsh,
            Sandstone,
            ToxicJungle,
            MagmaCore,
            OilField,
            Space,
            Ocean,
            Rust,
            Forest,
            Radioactive,
            Swamp,
            Wasteland,
            RocketInterior,
            Metallic,
            Barren,
            Moo
        }

        private static SubWorld.ZoneType SwitchToSubWorldZoneType(ZoneType type)
        {
            switch (type)
            {
                case ZoneType.FrozenWastes:
                    return SubWorld.ZoneType.FrozenWastes;
                case ZoneType.CrystalCaverns:
                    return SubWorld.ZoneType.CrystalCaverns;
                case ZoneType.BoggyMarsh:
                    return SubWorld.ZoneType.BoggyMarsh;
                case ZoneType.Sandstone:
                    return SubWorld.ZoneType.Sandstone;
                case ZoneType.ToxicJungle:
                    return SubWorld.ZoneType.ToxicJungle;
                case ZoneType.MagmaCore:
                    return SubWorld.ZoneType.MagmaCore;
                case ZoneType.OilField:
                    return SubWorld.ZoneType.OilField;
                case ZoneType.Space:
                    return SubWorld.ZoneType.Space;
                case ZoneType.Ocean:
                    return SubWorld.ZoneType.Ocean;
                case ZoneType.Rust:
                    return SubWorld.ZoneType.Rust;
                case ZoneType.Forest:
                    return SubWorld.ZoneType.Forest;
                case ZoneType.Radioactive:
                    return SubWorld.ZoneType.Radioactive;
                case ZoneType.Swamp:
                    return SubWorld.ZoneType.Swamp;
                case ZoneType.Wasteland:
                    return SubWorld.ZoneType.Wasteland;
                case ZoneType.RocketInterior:
                    return SubWorld.ZoneType.RocketInterior;
                case ZoneType.Metallic:
                    return SubWorld.ZoneType.Metallic;
                case ZoneType.Barren:
                    return SubWorld.ZoneType.Barren;
                case ZoneType.Moo:
                    return SubWorld.ZoneType.Moo;
                default:
                    return SubWorld.ZoneType.RocketInterior;
            }
        }
    }
}
