using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Proclass
{
    /// <summary>
    /// 地图组成单位属性
    /// </summary>
    public class MapUnityClass
    {
        string MunityName { get; set; }

        double Probability { get; set; }
        /// <summary>
        /// 暂定0为障碍物，1为普通地形
        /// </summary>
        int MUnityType { get; set; }

        int Level { get; set; }
    }
}
