﻿using TUNING;
using UnityEngine;

namespace ONI_AsteroidBelt_2.AsBuilding.HabitatConfigs
{
    internal class ConfigFile : IBuildingConfig
    {
        public override string[] GetDlcIds()
        {
            return DlcManager.AVAILABLE_EXPANSION1_ONLY;
        }


        public override BuildingDef CreateBuildingDef()
        {
            string id = "HabitatModuleHuge";//
            int width = 5;
            int height = 4;
            string anim = "rocket_habitat_medium_module_kanim";
            int hitpoints = 1000;
            float construction_time = 60f;
            float[] dense_TIER = TUNING.BUILDINGS.ROCKETRY_MASS_KG.DENSE_TIER3;
            string[] raw_METALS = MATERIALS.RAW_METALS;
            float melting_point = 9999f;
            BuildLocationRule build_location_rule = BuildLocationRule.Anywhere;
            EffectorValues tier = NOISE_POLLUTION.NOISY.TIER2;
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(id, width, height, anim, hitpoints, construction_time, dense_TIER, raw_METALS, melting_point, build_location_rule, TUNING.BUILDINGS.DECOR.NONE, tier, 0.2f);
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


        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            BuildingConfigManager.Instance.IgnoreDefaultKComponent(typeof(RequiresFoundation), prefab_tag);
            go.AddOrGet<LoopingSounds>();
            go.GetComponent<KPrefabID>().AddTag(RoomConstraints.ConstraintTags.IndustrialMachinery, false);
            go.GetComponent<KPrefabID>().AddTag(GameTags.LaunchButtonRocketModule, false);
            go.AddOrGet<AssignmentGroupController>().generateGroupOnStart = true;
            go.AddOrGet<PassengerRocketModule>().interiorReverbSnapshot = AudioMixerSnapshots.Get().MediumRocketInteriorReverbSnapshot;
            go.AddOrGet<ClustercraftExteriorDoor>().interiorTemplateName = "expansion1::interiors/habitat_huge";//
            go.AddOrGetDef<SimpleDoorController.Def>();
            go.AddOrGet<NavTeleporter>();
            go.AddOrGet<AccessControl>();
            go.AddOrGet<LaunchableRocketCluster>();
            go.AddOrGet<RocketCommandConditions>();
            go.AddOrGet<RocketProcessConditionDisplayTarget>();
            go.AddOrGet<CharacterOverlay>().shouldShowName = true;
            go.AddOrGet<BuildingAttachPoint>().points = new BuildingAttachPoint.HardPoint[]
            {
            new BuildingAttachPoint.HardPoint(new CellOffset(0, 4), GameTags.Rocket, null)
            };
            Storage storage = go.AddComponent<Storage>();
            storage.showInUI = false;
            storage.capacityKg = 10f;
            RocketConduitSender rocketConduitSender = go.AddComponent<RocketConduitSender>();
            rocketConduitSender.conduitStorage = storage;
            rocketConduitSender.conduitPortInfo = this.liquidInputPort;
            go.AddComponent<RocketConduitReceiver>().conduitPortInfo = this.liquidOutputPort;
            Storage storage2 = go.AddComponent<Storage>();
            storage2.showInUI = false;
            storage2.capacityKg = 1f;
            RocketConduitSender rocketConduitSender2 = go.AddComponent<RocketConduitSender>();
            rocketConduitSender2.conduitStorage = storage2;
            rocketConduitSender2.conduitPortInfo = this.gasInputPort;
            go.AddComponent<RocketConduitReceiver>().conduitPortInfo = this.gasOutputPort;
        }


        private void AttachPorts(GameObject go)
        {
            go.AddComponent<ConduitSecondaryInput>().portInfo = this.liquidInputPort;
            go.AddComponent<ConduitSecondaryOutput>().portInfo = this.liquidOutputPort;
            go.AddComponent<ConduitSecondaryInput>().portInfo = this.gasInputPort;
            go.AddComponent<ConduitSecondaryOutput>().portInfo = this.gasOutputPort;
        }


        public override void DoPostConfigureComplete(GameObject go)
        {
            BuildingTemplates.ExtendBuildingToRocketModuleCluster(go, null, ROCKETRY.BURDEN.MAJOR, 0f, 0f);
            Ownable ownable = go.AddOrGet<Ownable>();
            ownable.slotID = Db.Get().AssignableSlots.HabitatModule.Id;
            ownable.canBePublic = false;
            FakeFloorAdder fakeFloorAdder = go.AddOrGet<FakeFloorAdder>();
            fakeFloorAdder.floorOffsets = new CellOffset[]
            {
            new CellOffset(-2, -1),
            new CellOffset(-1, -1),
            new CellOffset(0, -1),
            new CellOffset(1, -1),
            new CellOffset(2, -1)
            };
            fakeFloorAdder.initiallyActive = false;
            go.AddOrGet<BuildingCellVisualizer>();
            go.GetComponent<ReorderableBuilding>().buildConditions.Add(new LimitOneCommandModule());
        }


        public override void DoPostConfigurePreview(BuildingDef def, GameObject go)
        {
            base.DoPostConfigurePreview(def, go);
            go.AddOrGet<BuildingCellVisualizer>();
            this.AttachPorts(go);
        }


        public override void DoPostConfigureUnderConstruction(GameObject go)
        {
            base.DoPostConfigureUnderConstruction(go);
            go.AddOrGet<BuildingCellVisualizer>();
            this.AttachPorts(go);
        }

        public const string ID = "HabitatModuleHuge";//

        private ConduitPortInfo gasInputPort = new ConduitPortInfo(ConduitType.Gas, new CellOffset(-2, 0));

        private ConduitPortInfo gasOutputPort = new ConduitPortInfo(ConduitType.Gas, new CellOffset(2, 0));

        private ConduitPortInfo liquidInputPort = new ConduitPortInfo(ConduitType.Liquid, new CellOffset(-2, 3));

        private ConduitPortInfo liquidOutputPort = new ConduitPortInfo(ConduitType.Liquid, new CellOffset(2, 3));
    }



}
