using ProcGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsBuilding.HabitatConfigs
{
    internal class HabitatData
    {
        public static int Width { get { return AsData.BuildingData.HabitatData.Width; } }

        public static int Hight { get { return AsData.BuildingData.HabitatData.Height; } }

        public static int SpaceWidth { get { return Width < 25 ? 32 : Width + 6; } }

        public static int SpaceHight { get { return Hight < 25 ? 32 : Width + 6; } }

        public static string HabitatTample { get { return FormateHabitat(Width, Hight); } }

        public static SubWorld.ZoneType ZoneType { get { return AsData.BuildingData.HabitatData.ZoneType; } }

        private static string FormateHabitat(int x, int y)
        {
            StringBuilder result = new StringBuilder(
                $"name: habitat_medium\n" +
                $"info:\n" +
                $"  size:\n" +
                $"    X: {x}\n" +
                $"    Y: {y}\n" +
                $"  area: {x * y}\n" +
                $"cells:\n");

            for (int location_y = -1; location_y < y-1; location_y++)
            {
                for (int location_x = -1; location_x < x-1; location_x++)
                {
                    if (location_x == -1 || location_x == x - 2 || location_y == -1 || location_y == y - 2)
                    {
                        if (location_y == y - 2 && (x - location_x < 6 && x - location_x > 1))
                        {
                            continue;
                        }

                        if (location_y == -1 && !(location_x == -1 || location_x == x - 2))
                        {
                            result.Append(
                            $"- element: Diamond\n" +
                            $"  mass: 100\n" +
                            $"  temperature: 293.149994\n" +
                            $"  location_x: {location_x - (x / 2) + 1}\n" +
                            $"  location_y: {-location_y + (y / 2) - 1}\n");
                            continue;
                        }
                        result.Append(
                            $"- element: Steel\n" +
                            $"  mass: 100\n" +
                            $"  temperature: 293.149994\n" +
                            $"  location_x: {location_x - (x / 2) + 1}\n" +
                            $"  location_y: {-location_y + (y / 2) - 1}\n");
                    }
                    else
                    {
                        result.Append(
                            $"- element: Vacuum\n" +
                            $"  location_x: {location_x - (x / 2) + 1}\n" +
                            $"  location_y: {-location_y + (y / 2) - 1}\n");
                    }
                }
            }

            result.AppendLine("buildings:");

            Queue<string> ele = new Queue<string>();
            ele.Enqueue("RocketInteriorLiquidOutputPort");
            ele.Enqueue("RocketInteriorLiquidInputPort");
            ele.Enqueue("RocketInteriorGasOutputPort");
            ele.Enqueue("RocketInteriorGasInputPort");


            for (int location_y = -1; location_y < y-1; location_y++)
            {
                for (int location_x = -1; location_x < x - 1; location_x++)
                {
                    if (location_x == -1 || location_x == x - 2 || location_y == -1 || location_y == y - 2)
                    {
                        if (location_y == y - 2 && (x - location_x < 6 && x - location_x > 1))
                        {
                            result.Append(
                            $"- id: {ele.Dequeue()}\n" +
                            $"  location_x: {location_x - (x / 2) + 1}\n" +
                            $"  location_y: {-location_y + (y / 2) - 1}\n" +
                            $"  element: Steel\n" +
                            $"  temperature: 293.149994\n");
                            continue;
                        }

                        if (location_y == -1 && !(location_x == -1 || location_x == x - 2))
                        {
                            result.Append(
                            $"- id: RocketEnvelopeWindowTile\n" +
                            $"  location_x: {location_x - (x / 2) + 1}\n" +
                            $"  location_y: {-location_y + (y / 2) - 1}\n" +
                            $"  element: Diamond\n" +
                            $"  temperature: 293.149994\n");
                            continue;
                        }

                        result.Append(
                        $"- id: RocketWallTile\n" +
                        $"  location_x: {location_x - (x / 2) + 1}\n" +
                        $"  location_y: {-location_y + (y / 2) - 1}\n" +
                        $"  element: Steel\n" +
                        $"  temperature: 293.149994\n");
                    }

                    if (location_y == y - 3 && x - location_x == 6)
                    {
                        result.Append(
                        $"- id: RocketControlStation\n" +
                        $"  location_x: {location_x - (x / 2) + 1}\n" +
                        $"  location_y: {-location_y + (y / 2) - 1}\n" +
                        $"  element: Cuprite\n" +
                        $"  temperature: 293.149994\n");
                    }
                }
            }

            result.AppendLine(
                $"otherEntities:\n" +
                $"- id: ClustercraftInteriorDoor\n" +
                $"  location_x: {-(x / 2)}\n" +
                $"  location_y: {2 - y + (y / 2) }\n" +
                $"  element: Unobtanium\n" +
                $"  temperature: 294.149994\n" +
                $"  units: 1\n" +
                $"  type: Other\n"
                );

            return result.ToString();

        }
    }
}

