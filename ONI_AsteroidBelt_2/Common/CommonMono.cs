using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ONI_AsteroidBelt_2.Common
{
    internal sealed class CommonMono : MonoBehaviour
    {
        public static GameObject MonoObject { get; set; }

        private static CommonMono instance;

        private static readonly object _lock = new object();

        public static CommonMono Instance
        {
            get
            {
                //确保多线程安全
                if (MonoObject == null)
                    lock (_lock)
                        if (MonoObject == null)
                        {
                            //进行自身的构造
                            MonoObject = PersistentGameObject.GetObject("MonoSingleCase");
                            //添加脚本
                            instance = MonoObject.AddComponent<CommonMono>();
                        }
                return instance;
            }
        }

        /// <summary>
        /// 物理更新函数，循环执行，0.02 秒执行一次（不受 FPS 帧率影响，时间可更改），所有和物理相关的更新都应在此函数处理
        /// </summary>
        public System.Action MonoFixedUpdate { get; set; }

        private void FixedUpdate()
        {
            try
            {
                MonoFixedUpdate?.Invoke();
            }
            catch (Exception e)
            {
                throw new Exception("MonoSingleCase -> FixedUpdate: get Exception", e);
            }
        }

        /// <summary>
        /// 更新函数，每帧执行一次，受 FPS 帧率影响
        /// </summary>
        public System.Action MonoUpdate { get; set; }

        private void Update()
        {
            try
            {
                MonoUpdate?.Invoke();
            }
            catch (Exception e)
            {
                throw new Exception("MonoSingleCase -> Update: get Exception", e);
            }
        }

        /// <summary>
        /// 稍后更新函数，在所有 Update 执行完后调用，帧间隔时间和 Update 一样
        /// </summary>
        public System.Action MonoLateUpdate { get; set; }

        private void LateUpdate()
        {
            try
            {
                MonoLateUpdate?.Invoke();
            }
            catch (Exception e)
            {
                throw new Exception("MonoSingleCase -> LateUpdate: get Exception", e);
            }
        }


        /// <summary>
        /// 在渲染和处理 GUI 事件时被调用，每帧都执行
        /// </summary>
        public System.Action MonoOnGUI { get; set; }

        private void OnGUI()
        {
            try
            {
                MonoOnGUI?.Invoke();
            }
            catch (Exception e)
            {
                throw new Exception("MonoSingleCase -> OnGUI: get Exception", e);
            }
        }

        public static void DestroyInstance()
        {
            instance = null;
            PersistentGameObject.RemoveObject("MonoSingleCase");
        }
    }
}
