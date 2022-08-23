using ONI_AsteroidBelt_2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ONI_AsteroidBelt_2.AsUI.UGUI
{
    internal class UGUI_Utils
    {
        /// <summary>
        /// 尝试在父对象中获取子对象
        /// </summary>
        /// <param name="ui">父对象</param>
        /// <param name="childName">子对象的名字</param>
        /// <returns>子对象 找不到返回 null</returns>
        public static GameObject FindChild(GameObject ui, string childName)
        {

            foreach (var t in ui.GetComponentsInChildren<Transform>(true))
            {
                if (t.name == childName)
                {
                    //Log.Debug($"Get child {childName}");
                    return t.gameObject;
                }
            }
            Log.Error($"can't find object: {childName}");
            return null;
        }

        /// <summary>
        /// 尝试在父对象中获取子对象的相应组件
        /// </summary>
        /// <typeparam name="T">组件类型</typeparam>
        /// <param name="ui">父对象</param>
        /// <param name="childName">子对象的名字</param>
        /// <returns>相应组件 找不到返回 null</returns>
        public static T FindComponent<T>(GameObject ui, string childName) where T : Component
        {

            var obj = FindChild(ui, childName);

            if (obj == null)
                return null;

            obj.TryGetComponent(out T res);

            if (res == null)
            {
                Log.Error($"can't find {typeof(T).Name}: {childName}");
                return null;
            }

            return res;
        }

        /// <summary>
        /// 为指定按钮绑定事件
        /// </summary>
        /// <param name="ui">父对象</param>
        /// <param name="buttonName">子对象的名字</param>
        /// <param name="callBack">绑定的事件</param>
        public static void BuildButton(GameObject ui, string buttonName, System.Action callBack)
        {
            Button res = FindComponent<Button>(ui, buttonName);
            if (res != null)
            {
                res.onClick.RemoveAllListeners();
                res.onClick.AddListener(() => { callBack?.Invoke(); });
                return;
            }
            Log.Error($"Can't find button: {buttonName}");

        }
    }
}
