using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ONI_AsteroidBelt_2.Common
{
    public static class PersistentGameObject
    {
        private static readonly GameObject gameObject;

        private static readonly Dictionary<string, List<GameObject>> Childs;

        static PersistentGameObject()
        {
            gameObject = new GameObject("PersistentGameObject");

            Childs = new Dictionary<string, List<GameObject>>();

            GameObject.DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// 设置子对象
        /// </summary>
        /// <param name="Child">子对象</param>
        public static void SetChild(GameObject Child)
        {
            if (Child == null)
                throw new ArgumentException("PersistentGameObject: SetChild get null");
            Child.transform.SetParent(gameObject.transform);
            if (Childs.ContainsKey(Child.name))
            {
                Childs[Child.name].Add(Child);
            }
            else
            {
                Childs[Child.name] = new List<GameObject>() { Child };
            }
        }

        /// <summary>
        /// 获取一个不会被销毁的对象
        /// </summary>
        /// <param name="name">对象名</param>
        /// <returns>对象</returns>
        public static GameObject GetObject(string name)
        {
            if (name == null)
                throw new ArgumentException("PersistentGameObject: GetObject get null");

            GameObject obj = new GameObject(name);
            SetChild(obj);
            return obj;
        }

        /// <summary>
        /// 尝试销毁相应的永久对象
        /// </summary>
        /// <param name="name">对象名字</param>
        /// <param name="immediate">是否立即清除</param>
        /// <returns>是否找到要清除的对象</returns>
        /// <exception cref="ArgumentException">传入名字是 null</exception>
        public static bool RemoveObject(string name, bool immediate = false)
        {
            if (name == null)
                throw new ArgumentException("PersistentGameObject: RemoveObject get null");

            if (!Childs.ContainsKey(name))
                return false;

            foreach (GameObject obj in Childs[name])
            {
                if (obj != null)
                    if (immediate)
                        GameObject.DestroyImmediate(obj);
                    else
                        GameObject.Destroy(obj);
            }

            Childs.Remove(name);
            return true;
        }
    }
}
