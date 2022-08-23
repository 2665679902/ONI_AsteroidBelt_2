using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.Common.AsAttributes
{
    /// <summary>
    /// 必须作用于无参数的静态函数
    /// </summary>
    public class LoadAttribute: Attribute
    {

        public Type AfterType { get;private set; }

        public LoadOpportunity Opportunity { get; private set; }

        /// <summary>
        /// 控制加载时机的一些函数
        /// </summary>
        /// <param name="AfterType">在什么函数之后加载，如果目标类型没有加载函数或不使用默认最后加载</param>
        /// <param name="opportunity">加载时机</param>
        public LoadAttribute(Type afterType = null, LoadOpportunity opportunity = LoadOpportunity.early )
        {
            AfterType = afterType;
            Opportunity = opportunity;
        }

        /// <summary>
        /// 加载的时机分组
        /// </summary>
        public enum LoadOpportunity
        {
            /// <summary>
            /// 在模组启动时加载
            /// </summary>
            early,

            /// <summary>
            /// 在游戏屏幕出现之前加载
            /// </summary>
            late,
        }
    }
}
