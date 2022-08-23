using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsAttributes;
using TUNING;

namespace ONI_AsteroidBelt_2.AsBuilding.HabitatConfigs
{
    internal class HabitatLoad
    {
        [Load(null, LoadAttribute.LoadOpportunity.late)]
        public static void Load()
        {
            EventCentre.Trigger(EventInventory.RefreshUIData, null);

            Log.Debug($"Set SpaceWith: {HabitatData.SpaceWidth}, {HabitatData.SpaceWidth}");

            ROCKETRY.ROCKET_INTERIOR_SIZE = new Vector2I(HabitatData.SpaceWidth, HabitatData.SpaceWidth);

            FileUtility.WriteIn(@"Resources\Habitat\habitat_huge.yaml", HabitatData.HabitatTample);

            FileUtility.LoadFile(@"Resources\Habitat", @"OxygenNotIncluded_Data\StreamingAssets\dlc\expansion1\templates\interiors");

        }

        public static void UnLoad()
        {
            FileUtility.Remove(@"Resources\Habitat");
        }
    }
}