using ONI_AsteroidBelt_2.AsData.AssetBundles;
using ONI_AsteroidBelt_2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ONI_AsteroidBelt_2.AsUI.UGUI.SelectWorldUI.ViewModel
{
    internal static class SelectWorldCanvas
    {
        private static GameObject _canvas;

        /// <summary>
        /// 获取 SelectWorld 画布的激活状态
        /// </summary>
        public static void SetActive(bool active)
        {
            if (active)
            {
                //取出界面
                _canvas = AssetBundleManager.TryGetObject<GameObject>("SelectWorldCa");

                //生成界面
                _canvas = UnityEngine.Object.Instantiate(_canvas);
            }
            else
            {
                if (_canvas != null)
                    GameObject.Destroy(_canvas);
            }

        }

        /// <summary>
        /// 尝试在画布中获取子对象的相应组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="name">子对象的名字</param>
        /// <returns>相应组件 找不到返回 null</returns>
        public static T Get<T>(string name) where T : Component
        {
            return UGUI_Utils.FindComponent<T>(_canvas, name);
        }

        /// <summary>
        /// 为指定按钮绑定事件
        /// </summary>
        /// <param name="buttonName">子对象的名字</param>
        /// <param name="callBack">绑定的事件</param>
        public static void BuildButton(string buttonName, System.Action callBack)
        {
            UGUI_Utils.BuildButton(_canvas, buttonName, callBack);
        }

        /// <summary>
        /// 尝试在画布中获取子对象
        /// </summary>
        /// <param name="name">子对象的名字</param>
        /// <returns>相应子对象 找不到返回 null</returns>
        public static GameObject Get(string name)
        {
            return UGUI_Utils.FindChild(_canvas, name);
        }

        /// <summary>
        /// 设置一个对象的活跃状态
        /// </summary>
        /// <param name="go">对象</param>
        /// <param name="active">状态</param>
        private static void SetObjectActive(GameObject go, bool active = false)
        {
            if (go == null)
            {
                Log.Error($"SelectWorldCa try to inactivate a null object");
                return;
            }

            go.SetActive(active);
        }

        /// <summary>
        /// 设置一个对象的活跃状态
        /// </summary>
        /// <param name="name">对象名</param>
        /// <param name="active">状态</param>
        private static void SetObjectActive(string name, bool active = false)
        {
            SetObjectActive(Get(name), active);
        }

        /// <summary>
        /// 失活所有的屏幕
        /// </summary>
        private static void InactivateAllScreen()
        {
            SetObjectActive("HistoryScreeen");
            SetObjectActive("StartNewScreen");
            SetObjectActive("StartAnimationScreen");
            SetObjectActive("GetCoinScreen");
            SetObjectActive("SelectWorldScreen");
            SetObjectActive("ConfirmScreen");
        }

        /// <summary>
        /// 激活 HistoryScreen
        /// </summary>
        private static void HistoryScreenActive()
        {
            InactivateAllScreen();
            SetObjectActive("HistoryScreeen", true);
        }

        /// <summary>
        /// 激活 GetCoinScreen
        /// </summary>
        private static void GetCoinScreenActive()
        {
            InactivateAllScreen();
            SetObjectActive("StartNewScreen", true);
            SetObjectActive("GetCoinScreen", true);

        }

        /// <summary>
        /// 激活 SelectWorldScreen
        /// </summary>
        private static void SelectWorldScreenActive()
        {
            InactivateAllScreen();
            SetObjectActive("StartNewScreen", true);
            SetObjectActive("SelectWorldScreen", true);
        }

        /// <summary>
        /// 激活 ConfirmScreen
        /// </summary>
        private static void ConfirmScreenActive()
        {
            InactivateAllScreen();
            SetObjectActive("StartNewScreen", true);
            SetObjectActive("ConfirmScreen", true);
        }

        /// <summary>
        /// 生成 HistoryScreen
        /// </summary>
        /// <param name="History_1Text">上面的文本</param>
        /// <param name="History_2Text">下面的文本</param>
        public static void BuildHistoryScreen(string History_1Text, string History_2Text, string HistoryShowText, string Load_ButtonText)
        {
            Get<Text>("History_1Text").text = History_1Text;
            Get<Text>("History_1Text").resizeTextForBestFit = true;

            Get<Text>("History_2Text").text = History_2Text;
            Get<Text>("History_2Text").resizeTextForBestFit = true;

            Get<Text>("HistoryShowText").text = HistoryShowText;

            Get<Text>("Load_ButtonText").text = Load_ButtonText;
            HistoryScreenActive();
        }

        /// <summary>
        /// 生成 ConfirmScreen
        /// </summary>
        /// <param name="resultText">生成世界的结果</param>
        public static void BuildeConfirmScreen(string resultText)
        {
            Get<Text>("ConfirmText").text = resultText;
            ConfirmScreenActive();
        }

        /// <summary>
        /// 生成 SelectWorldScreen
        /// </summary>
        /// <param name="discribe">世界上方的描述</param>
        /// <param name="Pic1">图片1</param>
        /// <param name="But1">按钮1的需要的Coin</param>
        /// <param name="Pic2">图片2</param>
        /// <param name="But2">按钮2的需要的Coin</param>
        /// <param name="Pic3">图片3</param>
        /// <param name="But3">按钮3的需要的Coin</param>
        /// <param name="currentCoin">当前的Coin</param>
        public static void BuildeSelectWorldScreen(
            string discribe,
            Sprite Pic1, int But1,
            Sprite Pic2, int But2,
            Sprite Pic3, int But3,
            int currentCoin
            )
        {
            Get<Text>("CoinText").text = $"Coin: <color=orange> {currentCoin} </color>";

            Get<Text>("DisText").text = discribe;

            Get<Button>("SelectLeftButton").enabled = true;
            Get<Button>("SelectMiddleButton").enabled = true;
            Get<Button>("SelectRightButton").enabled = true;


            if (Pic1 != null)
            {
                Get<Image>("SelectLeftImage").sprite = Pic1;
                Get<Text>("SelectLeftText").text = But1 > 0 ? "+" + But1.ToString() : But1.ToString();
                if (-But1 > currentCoin)
                {
                    Get<Button>("SelectLeftButton").enabled = false;
                }
            }

            if (Pic2 != null)
            {
                Get<Image>("SelectMiddleImage").sprite = Pic2;
                Get<Text>("SelectMiddleText").text = But2 > 0 ? "+" + But2.ToString() : But2.ToString();
                if (-But2 > currentCoin)
                {
                    Get<Button>("SelectMiddleButton").enabled = false;
                }
            }

            if (Pic3 != null)
            {
                Get<Image>("SelectRightImage").sprite = Pic3;
                Get<Text>("SelectRightText").text = But3 > 0 ? "+" + But3.ToString() : But1.ToString();
                if (-But3 > currentCoin)
                {
                    Get<Button>("SelectRightButton").enabled = false;
                }
            }
            SelectWorldScreenActive();
        }

        /// <summary>
        /// 生成 GetCoinScreen
        /// </summary>
        /// <param name="GetCoinText">获取Coin的文本</param>
        /// <param name="GetCoinButtonText">获取Coin按钮的文本</param>
        public static void BuildeGetCoinScreen(string GetCoinText, string GetCoinButtonText, string CloseButtonText, string NewButtonText, string HistoryButtonText)
        {
            Get<Text>("GetCoinText").text = GetCoinText;

            Get<Text>("GetCoinButtonText").text = GetCoinButtonText;

            Get<Text>("CloseButtonText").text = CloseButtonText;

            Get<Text>("NewButtonText").text = NewButtonText;

            Get<Text>("HistoryButtonText").text = HistoryButtonText;

            GetCoinScreenActive();
        }

        public class ButtonEvent
        {
            private System.Action _historyButtonClick;
            public System.Action HistoryButtonClick
            {
                get => _historyButtonClick;
                set
                {
                    _historyButtonClick = value;
                    BuildButton("HistoryButton", _historyButtonClick);
                }
            }


            private System.Action _closeButtonClick;
            public System.Action CloseButtonClick
            {
                get => _closeButtonClick;
                set
                {
                    _closeButtonClick = value;
                    BuildButton("CloseButton", _closeButtonClick);
                }
            }

            private System.Action _newButtonClick;
            public System.Action NewButtonClick
            {
                get => _newButtonClick;
                set
                {
                    _newButtonClick = value;
                    BuildButton("NewButton", _newButtonClick);
                }
            }

            private System.Action _load_ButtonClick;
            public System.Action Load_ButtonClick
            {
                get => _load_ButtonClick;
                set
                {
                    _load_ButtonClick = value;
                    BuildButton("Load_Button", _load_ButtonClick);
                }
            }

            private System.Action _getCoinButtonClick;
            public System.Action GetCoinButtonClick
            {
                get => _getCoinButtonClick;
                set
                {
                    _getCoinButtonClick = value;
                    BuildButton("GetCoinButton", _getCoinButtonClick);
                }
            }

            private System.Action _selectLeftButtonClick;
            public System.Action SelectLeftButtonClick
            {
                get => _selectLeftButtonClick;
                set
                {
                    _selectLeftButtonClick = value;
                    BuildButton("SelectLeftButton", _selectLeftButtonClick);
                }
            }

            private System.Action _selectMiddleButtonClick;
            public System.Action SelectMiddleButtonClick
            {
                get => _selectMiddleButtonClick;
                set
                {
                    _selectMiddleButtonClick = value;
                    BuildButton("SelectMiddleButton", _selectMiddleButtonClick);
                }
            }

            private System.Action _selectRightButtonClick;
            public System.Action SelectRightButtonClick
            {
                get => _selectRightButtonClick;
                set
                {
                    _selectRightButtonClick = value;
                    BuildButton("SelectRightButton", _selectRightButtonClick);
                }
            }

            private System.Action _confirmButtonClick;
            public System.Action ConfirmButtonClick
            {
                get => _confirmButtonClick;
                set
                {
                    _confirmButtonClick = value;
                    BuildButton("ConfirmButton", _confirmButtonClick);
                }
            }
        }

        /// <summary>
        /// 记录了一系列的Button事件
        /// </summary>
        public static ButtonEvent ButtonEvents { get; } = new ButtonEvent();
    }
}
