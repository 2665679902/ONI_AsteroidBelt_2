using ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data;
using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_LogUI
{
    internal static class LogUI
    {
        [Load]
        private static void Load()
        {
            EventCentre.Subscribe(EventInventory.OpenDebugUI, StartBuild);

            EventCentre.Subscribe(EventInventory.LogGetData, WriteIn);

            EventCentre.Subscribe(EventInventory.GetKeyEvent, hotKet);

            //设置热键 Z + Shift
            void hotKet(object o)
            {
                if (o is KInputController.KeyDef keyDef)
                {
                    if(keyDef.mKeyCode == KKeyCode.Z && keyDef.mModifier == Modifier.Shift)
                    {
                        EventCentre.Trigger(EventInventory.OpenDebugUI, null);
                    }
                }
            }
        }

        private static bool isOpen = false;

        private static Rect windowRect;
        private static Rect DragWindowRect;
        private static Vector2 ScrollViewVector;
        private static GUISkin _skin;
        private static string Message = "";

        const float fpsMeasurePeriod = 0.5f;    //FPS测量间隔
        private static int m_FpsAccumulator = 0;   //帧数累计的数量
        private static float m_FpsNextPeriod = 0;  //FPS下一段的间隔
        private static int m_CurrentFps;   //当前的帧率
        const string display = "{0} FPS";   //显示的文字
        private static string CurrentFps = "";

        private static void CountFrame()
        {
            // 测量每一秒的平均帧率
            m_FpsAccumulator++;
            if (Time.realtimeSinceStartup > m_FpsNextPeriod)    //当前时间超过了下一次的计算时间
            {
                m_CurrentFps = (int)(m_FpsAccumulator / fpsMeasurePeriod);   //计算
                m_FpsAccumulator = 0;   //计数器归零
                m_FpsNextPeriod += fpsMeasurePeriod;    //再增加下一次的间隔
                CurrentFps = string.Format(display, m_CurrentFps); //处理一下文字显示
            }
        }

        /// <summary>
        /// 写入Log的数据
        /// </summary>
        /// <param name="o">数据【Common.LogAct.LogData】</param>
        private static void WriteIn(object o)
        {
            if(o is Common.LogAct.LogData data)
            {
                Message += "\n";

                Message += data.FormateToRichText();
            }
        }

        /// <summary>
        /// 关闭UI
        /// </summary>
        private static void CloseUI()
        {
            if (!isOpen)
                return;

            CommonMono.Instance.MonoOnGUI -= BuildUIWindow;

            CommonMono.Instance.MonoUpdate -= CountFrame;

            isOpen = false;
        }

        /// <summary>
        /// 初始化UI，并开始绘制
        /// </summary>
        /// <param name="o"></param>
        private static void StartBuild(object o)
        {
            if (!Log.IsAble || Log.Level >= Log.LogLevel.Error || isOpen)
                return;

            windowRect = new Rect(200, 200, 600, 800);

            DragWindowRect = new Rect(0, 0, windowRect.width - 50, windowRect.height - 100);

            ScrollViewVector = new Vector2();

            _skin = Data.KL_DefaultSkin;

            CommonMono.Instance.MonoOnGUI += BuildUIWindow;

            CommonMono.Instance.MonoUpdate += CountFrame;

            isOpen = true;
        }

        /// <summary>
        /// 绘制UI的函数
        /// </summary>
        private static void BuildUIWindow()
        {
            if(_skin.window.normal.background == null)
                _skin = Data.KL_DefaultSkin;

            GUI.skin = _skin;

            windowRect = GUI.Window(1000, windowRect, MainWindow, $"DeBug  {CurrentFps}");

            void MainWindow(int num)
            {
                GUI.DragWindow(DragWindowRect);

                GUILayout.Label("", GUILayout.MinHeight(20));

                ScrollViewVector = GUILayout.BeginScrollView(ScrollViewVector);

                GUILayout.TextArea(Message, GUILayout.MaxWidth(windowRect.width - 35));

                GUILayout.EndScrollView();

                GUILayout.BeginHorizontal();

                if (GUILayout.Button("Close", GUILayout.MinHeight(50)))
                {
                    CloseUI();
                    return;
                }

                GUILayout.Label("           ");

                if (GUILayout.Button("Open", GUILayout.MinHeight(50)))
                {
                    Log.OpenFile();
                    return;
                }

                GUILayout.EndHorizontal();
            }
        }
    }
}
