using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUNING;
using UnityEngine;

namespace ONI_AsteroidBelt_102.AsBuilding.NoseconeConfigs
{
    internal class ConfigFile : IBuildingConfig
    {
        // Token: 0x06000C1E RID: 3102 RVA: 0x00042E7C File Offset: 0x0004107C
        public override string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }

        // Token: 0x06000C1F RID: 3103 RVA: 0x00042E84 File Offset: 0x00041084
        public override BuildingDef CreateBuildingDef()
        {
            string id = "LowTechNosecone";//
            int width = 5;
            int height = 2;
            string anim = "rocket_nosecone_default_kanim";
            int hitpoints = 1000;
            float construction_time = 60f;
            float[] hollow_TIER = new float[2] { 1200f, 600f };//
            string[] construction_materials = new string[]
            {
            "RefinedMetal",
            "Insulator"
            };
            float melting_point = 9999f;
            BuildLocationRule build_location_rule = BuildLocationRule.Anywhere;
            EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, width, height, anim, hitpoints, construction_time, hollow_TIER, construction_materials, melting_point, build_location_rule, BUILDINGS.DECOR.NONE, tier, 0.2f);
            BuildingTemplates.CreateRocketBuildingDef(buildingDef);
            buildingDef.AttachmentSlotTag = GameTags.Rocket;
            buildingDef.SceneLayer = Grid.SceneLayer.Building;
            buildingDef.OverheatTemperature = 2273.15f;
            buildingDef.Floodable = false;
            buildingDef.ObjectLayer = ObjectLayer.Building;
            buildingDef.ForegroundLayer = Grid.SceneLayer.Front;
            buildingDef.RequiresPowerInput = false;
            buildingDef.attachablePosition = new CellOffset(0, 0);
            buildingDef.CanMove = true;
            buildingDef.Cancellable = false;
            buildingDef.ShowInBuildMenu = false;
            return buildingDef;
        }

        // Token: 0x06000C20 RID: 3104 RVA: 0x00042F41 File Offset: 0x00041141
        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
            go.AddOrGet<LoopingSounds>();
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
            go.GetComponent<KPrefabID>().AddTag(GameTags.NoseRocketModule, false);
        }

        // Token: 0x06000C21 RID: 3105 RVA: 0x00042F81 File Offset: 0x00041181
        public override void DoPostConfigureComplete(GameObject go)
        {
            BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MINOR, 0f, 0f);
            go.GetComponent<ReorderableBuilding>().buildConditions.Add(new TopOnly());
        }

        // Token: 0x040006B7 RID: 1719
        public const string ID = "LowTechNosecone";//
    }
}
