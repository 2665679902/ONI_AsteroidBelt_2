using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.Common
{
    internal static class EventCentre
    {
        private static readonly Dictionary<int, System.Action<object>> EventDic = new Dictionary<int, System.Action<object>>();

        private static System.Action<object> GetEvent(int num)
        {
            if (EventDic.ContainsKey(num))
            {
                return EventDic[num];
            }
            else
            {
                EventDic.Add(num, null);
                return null;
            }
        }

        private static void SetEvent(int num, System.Action<object> action)
        {
            if (EventDic.ContainsKey(num))
                EventDic[num] += action ?? throw new ArgumentException("EventCentre: SetEvent get null");
            else
                EventDic.Add(num, action);
        }

        /// <summary>
        /// 尝试触发一个事件
        /// </summary>
        /// <param name="num">事件序号</param>
        /// <param name="message">事件消息</param>
        public static void Trigger(int num, object message)
        {
            GetEvent(num)?.Invoke(message);
        }

        /// <summary>
        /// 尝试订阅一个事件
        /// </summary>
        /// <param name="num">事件序号</param>
        /// <param name="action">行为</param>
        public static void Subscribe(int num, Action<object> action)
        {
            SetEvent(num, action);
        }

        /// <summary>
        /// 尝试取消订阅一个事件
        /// </summary>
        /// <param name="num">事件序号</param>
        /// <param name="action">如果为 null 则取消所有对此事件的监听</param>
        public static void Unsubscribe(int num, Action<object> action = null)
        {
            var target = GetEvent(num);

            if (action == null)
            {
                if (target == null)
                    return;
                EventDic[num] = null;
                EventDic.Remove(num);
            }

            if (target.GetInvocationList().Contains(action))
                target -= action;
            if (target != null)
                SetEvent(num, target);
            else if (EventDic.ContainsKey(num))
                EventDic.Remove(num);
        }

        //----------------------------------------------------

        /// <summary>
        /// 尝试触发一个事件
        /// </summary>
        /// <param name="Inventory">事件序号</param>
        /// <param name="message">事件消息</param>
        public static void Trigger(EventInventory Inventory, object message)
        {
            Trigger(Inventory.GetHashCode(), message);
        }

        /// <summary>
        /// 尝试订阅一个事件
        /// </summary>
        /// <param name="Inventory">事件序号</param>
        /// <param name="action">行为</param>
        public static void Subscribe(EventInventory Inventory, Action<object> action)
        {
            Subscribe(Inventory.GetHashCode(), action);
        }

        /// <summary>
        /// 尝试取消订阅一个事件
        /// </summary>
        /// <param name="Inventory">事件序号</param>
        /// <param name="action">如果为 null 则取消所有对此事件的监听</param>
        public static void Unsubscribe(EventInventory Inventory, Action<object> action = null)
        {
            Unsubscribe(Inventory.GetHashCode(), action);
        }
    }
}
