using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.Common
{
    [Flags]
    internal enum EventInventory
    {
        /// <summary>
        /// 获取了鼠标点击、案件按下等事件 传输的值类型为 KInputController.KeyDef
        /// </summary>
        GetKeyEvent,

        /// <summary>
        /// 字符串加载的准备完成了
        /// </summary>
        StringLoadReady,

        /// <summary>
        /// Log获得数据
        /// </summary>
        LogGetData,

        /// <summary>
        /// 再次载入UI的数据以实时刷新系统状态
        /// </summary>
        RefreshUIData,

        /// <summary>
        /// 开启 debug 的 UI
        /// </summary>
        OpenDebugUI,

        /// <summary>
        /// 开启 选择世界 的 UI
        /// </summary>
        OpenSelectWorldUI


    }
}
