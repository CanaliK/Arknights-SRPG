using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Proclass
{
    public class MapUnityMsg
    {
        /// <summary>
        /// 储存生成的地图单元
        /// </summary>
        public GameObject Mapunity;

        /// <summary>
        /// 地图单元的坐标
        /// </summary>
        public Vector3 Mappoint { get; set; }

        /// <summary>
        /// 正常显示的地图单元名称
        /// </summary>
        public string ACCnormal { get; set; }
    }
}
