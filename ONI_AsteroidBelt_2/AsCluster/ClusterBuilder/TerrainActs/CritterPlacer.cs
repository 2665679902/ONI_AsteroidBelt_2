using ONI_AsteroidBelt_2.AsCluster.BiomeDescribe.ItemsDescribe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateClasses;
using static ONI_AsteroidBelt_2.AsCluster.ClusterBuilder.FormateTerrain;

namespace ONI_AsteroidBelt_102.AsWorldBuilder.Creator.Acts
{
    internal class CritterPlacer
    {
        public static void PlaceSpawnables(Sim.Cell[] cells, List<Prefab> pickupables, WorldList world, System.Random CreatorRandom)
        {
            foreach (var biome in world.Biomes)
            {
                var points = GetSpawnPoints(cells, biome.Cells);
                PlaceSpawnables(pickupables, biome.BiomeType.SpawnablesOnFloor, points.onFloor, CreatorRandom);
                PlaceSpawnables(pickupables, biome.BiomeType.SpawnablesInGround, points.inGround, CreatorRandom);
                PlaceSpawnables(pickupables, biome.BiomeType.SpawnablesInAir, points.inAir, CreatorRandom);
                PlaceSpawnables(pickupables, biome.BiomeType.SpawnablesOnCeil, points.onCeil, CreatorRandom);
                PlaceSpawnables(pickupables, biome.BiomeType.SpawnablesInLiquid, points.inLiquid, CreatorRandom);
            }
        }

        // Add spawnables to the GameSpawnData list
        private static void PlaceSpawnables(List<Prefab> spawnList, List<Critter> critters, ISet<Vector2I> spawnPoints, System.Random CreatorRandom)
        {
            if (critters == null || critters.Count == 0 || spawnPoints.Count == 0)
                return;
            foreach (var critter in critters)
            {
                int numcritters = (int)Math.Ceiling(critter.Possibility * spawnPoints.Count);//按比例计算要打印的动植物数量
                for (int i = 0; i < numcritters && spawnPoints.Count > 0; i++)
                {
                    var pos = spawnPoints.ElementAt(CreatorRandom.Next(0, spawnPoints.Count));//随机选一个格子放进去
                    spawnPoints.Remove(pos);//这个格子不要打印其他东西了
                    spawnList.Add(critter.GetPrefab(pos.x, pos.y));
                }
            }
        }


        private struct SpawnPoints
        {
            public ISet<Vector2I> onFloor;
            public ISet<Vector2I> onCeil;
            public ISet<Vector2I> inGround;
            public ISet<Vector2I> inAir;
            public ISet<Vector2I> inLiquid;
        }

        private static SpawnPoints GetSpawnPoints(Sim.Cell[] cells, IEnumerable<Vector2I> biomeCells)
        {
            var spawnPoints = new SpawnPoints()
            {
                onFloor = new HashSet<Vector2I>(),
                onCeil = new HashSet<Vector2I>(),
                inGround = new HashSet<Vector2I>(),
                inAir = new HashSet<Vector2I>(),
                inLiquid = new HashSet<Vector2I>(),
            };

            foreach (Vector2I pos in biomeCells)
            {
                int cell = Grid.PosToCell(pos);
                Element element = ElementLoader.elements[cells[cell].elementIdx];
                if (element.IsSolid && element.id != SimHashes.Katairite && element.id != SimHashes.Unobtanium && cells[cell].temperature < 373f)
                    spawnPoints.inGround.Add(pos);
                else if (element.IsGas)
                {
                    Element elementBelow = ElementLoader.elements[cells[Grid.CellBelow(cell)].elementIdx];
                    if (elementBelow.IsSolid)
                        spawnPoints.onFloor.Add(pos);
                    else
                        spawnPoints.inAir.Add(pos);
                }
                else if (element.IsLiquid)
                    spawnPoints.inLiquid.Add(pos);
            }

            return spawnPoints;
        }
    }
}
