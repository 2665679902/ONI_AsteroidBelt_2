using ONI_AsteroidBelt_2.AsData.AssetBundles;
using ONI_AsteroidBelt_2.AsData.CurrentCluster;
using ONI_AsteroidBelt_2.AsData.Strings;
using ONI_AsteroidBelt_2.AsUI.UGUI.SelectWorldUI.Data;
using ONI_AsteroidBelt_2.AsUI.UGUI.SelectWorldUI.States;
using ONI_AsteroidBelt_2.AsUI.UGUI.SelectWorldUI.ViewModel;
using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ONI_AsteroidBelt_2.AsData.CurrentCluster.ClusterInfo;
using static ONI_AsteroidBelt_2.AsData.CurrentCluster.ClusterInfoManager;

namespace ONI_AsteroidBelt_2.AsUI.UGUI.SelectWorldUI
{
    internal static class SelectWorldUIBuilder
    {
        [Load(typeof(Log))]
        private static void Load()
        {
            EventCentre.Subscribe(EventInventory.OpenSelectWorldUI, BuildUI);

            EventCentre.Subscribe(EventInventory.RefreshUIData,
                (o) =>
                {
                    string text = FileUtility.ReadText(@"Resources\ClusterData\first.json");
                    ClusterInfoManager.CurrentCluster = Utility.Deserialize<ClusterInfo>(text);
                });
        }

        private static void BuildUI(object o)
        {
            SelectWorldCanvas.SetActive(true);

            SelectWorldCanvas.ButtonEvents.HistoryButtonClick += HistoryButtonClick;

            SelectWorldCanvas.ButtonEvents.Load_ButtonClick += Load_ButtonClick;

            SelectWorldCanvas.ButtonEvents.NewButtonClick += NewButtonClick;

            SelectWorldCanvas.ButtonEvents.GetCoinButtonClick += GetCoinButtonClick;

            SelectWorldCanvas.ButtonEvents.SelectLeftButtonClick += SelectLeftButtonClick;

            SelectWorldCanvas.ButtonEvents.SelectMiddleButtonClick += SelectMiddleButtonClick;

            SelectWorldCanvas.ButtonEvents.SelectRightButtonClick += SelectRightButtonClick;

            SelectWorldCanvas.ButtonEvents.ConfirmButtonClick += ConfirmButtonClick;

            SelectWorldCanvas.ButtonEvents.CloseButtonClick += CloseButtonClick;

            BuildGetCoinScreen();
        }

        //------------------------------------------------------------------------------------

        private static StartNewState currentStartNewState = StartNewState.GetCoin;

        private static MainMenuState currentMainMenuState = MainMenuState.StartNew;

        private static ClusterInfo currentClusterInfo = new ClusterInfo();

        private static int currentCoin = 0;

        private static List<WorldDiscribe> currentWorlds;

        private static List<int> currentButtonTexts;

        //------------------------------------------------------------------------------------

        private static void HistoryButtonClick()
        {
            ReBuildHistoryScreen();
            //Log.Debug("HistoryButtonClick");
        }

        private static void Load_ButtonClick()
        {
            FileUtility.Swap(@"Resources\ClusterData\first.json", @"Resources\ClusterData\second.json");
            ReBuildHistoryScreen();
            //Log.Debug("Load_ButtonClick");
        }

        private static void NewButtonClick()
        {
            if (currentMainMenuState == MainMenuState.StartNew)
                return;

            BuildGetCoinScreen();
            //Log.Debug("NewButtonClick");
        }

        private static void GetCoinButtonClick()
        {
            currentStartNewState = StartNewState.SelectStartWorld;
            currentCoin = Utility.Random.Next(30, 60);
            currentClusterInfo = new ClusterInfo();
            currentWorlds = new List<WorldDiscribe>();
            currentButtonTexts = new List<int>();
            BuildSelectScreen("");
            //Log.Debug("GetCoinButtonClick");
        }

        private static void SelectLeftButtonClick()
        {
            LoadDataAfterSelect(0);
            //Log.Debug("SelectLeftButtonClick");
        }

        private static void SelectMiddleButtonClick()
        {
            LoadDataAfterSelect(1);
            //Log.Debug("SelectMiddleButtonClick");
        }

        private static void SelectRightButtonClick()
        {
            LoadDataAfterSelect(2);
            //Log.Debug("SelectRightButtonClick");
        }

        private static void ConfirmButtonClick()
        {
            FileUtility.WriteIn(@"Resources\ClusterData\second.json", Utility.Serialize(currentClusterInfo));
            FileUtility.Swap(@"Resources\ClusterData\first.json", @"Resources\ClusterData\second.json");
            App.instance.Restart();
        }

        private static void CloseButtonClick()
        {
            currentStartNewState = StartNewState.AtStart;
            currentMainMenuState = MainMenuState.StartNew;
            SelectWorldCanvas.BuildeGetCoinScreen("", "", "", "", "");
            SelectWorldCanvas.SetActive(false);
        }

        //----------------------------------- Act -----------------------------------


        //build history act

        private static void ReBuildHistoryScreen()
        {
            string ReadFile(string path)
            {
                var res = FileUtility.ReadText(path);
                ClusterInfo cluster = new ClusterInfo();
                if (res == null || res.Length < 10)
                    return $"⊙﹏⊙∥ {AsStrings.UI.UGUI.SelectWorldUI.Nothing}";

                return Utility.Deserialize<ClusterInfo>(res)?.RichText(false) ?? $"⊙﹏⊙∥ {AsStrings.UI.UGUI.SelectWorldUI.Nothing}";
            }

            currentStartNewState = StartNewState.AtStart;
            currentMainMenuState = MainMenuState.History;

            SelectWorldCanvas.BuildHistoryScreen(
                ReadFile(@"Resources\ClusterData\first.json"),
                ReadFile(@"Resources\ClusterData\second.json"),
                AsStrings.UI.UGUI.SelectWorldUI.Current,
                AsStrings.UI.UGUI.SelectWorldUI.Load);
        }

        //build GetCoin

        private static void BuildGetCoinScreen()
        {
            currentStartNewState = StartNewState.GetCoin;
            currentMainMenuState = MainMenuState.StartNew;

            SelectWorldCanvas.BuildeGetCoinScreen
                (AsStrings.UI.UGUI.SelectWorldUI.GetCoinText,
                AsStrings.UI.UGUI.SelectWorldUI.GetCoinButtonText,
                AsStrings.UI.UGUI.SelectWorldUI.Close,
                AsStrings.UI.UGUI.SelectWorldUI.StartNew,
                AsStrings.UI.UGUI.SelectWorldUI.History);
        }

        //build Select

        private static List<T> RandomGet<T>(T[] values, int num, T[] except = null)
        {
            if (values.Length < num)
                return new List<T>(values);

            List<T> sourselist = new List<T>(values);
            List<T> exceptlist = new List<T>(except);
            List<T> reslist = new List<T>();

            for (int i = 0; i < num; i++)
            {
                var one = sourselist[Utility.Random.Next(0, sourselist.Count())];

                sourselist.Remove(one);

                while (exceptlist.Contains(one))
                {
                    if (!sourselist.Any())
                        return reslist;

                    one = sourselist[Utility.Random.Next(0, sourselist.Count())];

                }
                reslist.Add(one);
            }

            return reslist;
        }

        private static void BuildSelectScreen(string lastDiscribe)
        {
            currentMainMenuState = MainMenuState.StartNew;

            void GetReady(List<WorldStrings> from, string discribe)
            {
                currentStartNewState = StartNewState.SelectInnerWorld;

                //挑选出所需要的世界
                var worlds = RandomGet(
                    from.ToArray(), 3,
                    GetString(currentClusterInfo.TotalWorlds).ToArray());

                //转化，并登记信息
                currentWorlds = new List<WorldDiscribe>();
                currentButtonTexts = new List<int>();
                foreach (var world in worlds)
                {
                    var one = WorldDiscribe.GetDefaultWorld(world);

                    var coin = Utility.Random.Next(one.MinCion, one.MaxCion + 1);

                    currentButtonTexts.Add(-coin);
                    currentWorlds.Add(one);
                }

                //构建界面
                SelectWorldCanvas.BuildeSelectWorldScreen(
                    discribe,
                    currentWorlds[0].Sprite, currentButtonTexts[0],
                    currentWorlds[1].Sprite, currentButtonTexts[1],
                    currentWorlds[2].Sprite, currentButtonTexts[2],
                    currentCoin
                    );
            }

            switch (currentStartNewState)
            {
                case StartNewState.SelectStartWorld:
                    GetReady(StartWorlds, AsStrings.UI.UGUI.SelectWorldUI.SelectStartWorldText);
                    break;
                case StartNewState.SelectInnerWorld:
                    GetReady(Worlds, lastDiscribe + "\n" + AsStrings.UI.UGUI.SelectWorldUI.SelectInnerWorldText);
                    break;
                default:
                    Log.Error("BuildSelectScreen 错误地进入了");
                    break;
            }
        }

        private static void LoadDataAfterSelect(int buttonIndex)
        {
            WorldInfo LoadResult()
            {
                int resCoin = currentButtonTexts[buttonIndex];

                var resWorld = currentWorlds[buttonIndex];

                currentCoin += resCoin;

                return new WorldInfo
                {
                    Name = resWorld.Name,
                    Magnification =
                    Utility.Random.NextDouble()
                    * (resWorld.MaxMagnification - resWorld.MinMagnification)
                    + resWorld.MinMagnification
                    + resCoin * (-0.1)
                };
            }

            if (currentClusterInfo.StartWorld.Name == null)
            {
                currentClusterInfo.StartWorld = LoadResult();
                BuildSelectScreen(currentWorlds[buttonIndex].Discribe);
                return;

            }
            else if (currentClusterInfo.InnerWorld_0.Name == null)
            {
                currentClusterInfo.InnerWorld_0 = LoadResult();
                BuildSelectScreen(currentWorlds[buttonIndex].Discribe);
                return;
            }
            else if (currentClusterInfo.InnerWorld_1.Name == null)
            {
                currentClusterInfo.InnerWorld_1 = LoadResult();
                BuildSelectScreen(currentWorlds[buttonIndex].Discribe);
                return;
            }

            currentClusterInfo.InnerWorld_2 = LoadResult();

            var namesUsed = new List<string>();
            foreach (var world in currentClusterInfo.TotalWorlds)
                namesUsed.Add(world.Name);

            currentClusterInfo.OuterWorldList = new List<WorldInfo>();

            foreach (var world in Worlds)
            {
                if (namesUsed.Contains(world.Name))
                    continue;

                var resWorld = WorldDiscribe.GetDefaultWorld(world);

                currentClusterInfo.OuterWorldList.Add(new WorldInfo()
                {
                    Name = world.Name,
                    Magnification =
                    Utility.Random.NextDouble()
                    * (resWorld.MaxMagnification - resWorld.MinMagnification)
                    + resWorld.MinMagnification
                });

            }

            currentClusterInfo.CoinLast = currentCoin;
            BuildConfirmScreen();
        }

        //build Confirm

        private static void BuildConfirmScreen()
        {
            SelectWorldCanvas.BuildeConfirmScreen(currentClusterInfo.RichText(true));
        }
    }
}
