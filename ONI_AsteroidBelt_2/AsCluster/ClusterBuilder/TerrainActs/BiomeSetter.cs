using Delaunay.Geo;
using ONI_AsteroidBelt_2.AsCluster.BiomeDescribe;
using ONI_AsteroidBelt_2.AsCluster.WorldDescribe;
using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsExtension;
using ProcGen;
using ProcGenGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VoronoiTree;
using ONI_AsteroidBelt_2.AsCluster.ClusterBuilder;

namespace ONI_AsteroidBelt_102.AsWorldBuilder.Creator.Acts
{
    internal class BiomeSetter
    {
        public static void SetBiomes(List<TerrainCell> overworldCells, FormateTerrain.WorldList world)
        {
            overworldCells.Clear();

            string SpaceBiome = "subworlds/space/Space";
            TagSet tags;
            List<Vector2> vertices;
            HashSet<Vector2I> total = new HashSet<Vector2I>();
            Polygon bounds;
            uint cellId = 0;

            for (int i = 0; i < world.Width; i++)
                for (int j = 0; j < world.Height; j++)
                    total.Add(new Vector2I(i, j));


            void CreateOverworldCell(string type, TagSet ts, Vector2 vector2)
            {
                ProcGen.Map.Cell cell = new ProcGen.Map.Cell(); // 生成格子

                cell.SetType(type);

                foreach (Tag tag in ts)
                    cell.tags.Add(tag);

                vertices = new List<Vector2>()
                {
                    vector2 + new Vector2(-1,-1),
                    vector2 + new Vector2(-1,1),
                    vector2 + new Vector2(1,-1),
                    vector2 + new Vector2(1,1),
                };
                bounds = new Polygon(vertices);
                Diagram.Site site = new Diagram.Site()
                {
                    id = cellId++,
                    poly = bounds
                };
                site.position = site.poly.Centroid();
                overworldCells.Add(new Klei.TerrainCellLogged(cell, site, new Dictionary<Tag, int>()));
            };

            foreach (var biome in  world.Biomes)
            {
                tags = new TagSet();

                if (world.World is StartWorld)
                {
                    tags = new TagSet
                    {
                        WorldGenTags.AtStart,
                        WorldGenTags.StartWorld,
                        WorldGenTags.StartLocation
                    };
                }

                List<Vector2I> biomeList = new List<Vector2I>(biome.Cells);

                foreach (var v in biomeList)
                {
                    CreateOverworldCell(biome.BiomeType.BackgroundSubworld, tags, v);
                }

                total.ExceptWith(biome.Cells);



            }

            Log.Debug("over");

            tags = new TagSet();

            foreach (var v in total)
            {
                CreateOverworldCell(SpaceBiome, tags, v);
            }

        }
    }
}
