using Assets.Proclass;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

/// <summary>
/// 地图控制（主要就是生成地图，根据操作变化地图）
/// </summary>
public class MapControl : MonoBehaviour
{
    public GameObject MapHolder;//地图单元的预制体
    Vector2 mapSize = new Vector2(25, 25);//地图大小，现阶段尽可能设为奇数，不然可能会生成主角位置的地方出bug
    public Transform MapGenerator;//地图锚点空物体
    public PhysicsRaycaster physicsRaycaster;//视角控制脚本

    int maptype =1;//地图单元的地图类型的索引值（决定方块是那种地形）
    public bool IsChange = false;//地图是否需要进行变化（主角移动时，通过从其他脚本传入该值来变化地图）
    public GameObject NewMainObject;//移动后的新位置（传入）
    //List<Mapdata> mapdata = new List<Mapdata>();
    List<MapUnityMsg> Maplist = new List<MapUnityMsg>();//所有地图单元组成的list，存放当前地图信息

    void Start()
    {
        GenerateMap();
        RefreshMap_4(NewMainObject);
    }

    /// <summary>
    /// 每一帧都查看一次，是否需要变化地图，需要就变化
    /// </summary>
    void Update()
    {
        if(IsChange)
        {
            physicsRaycaster.MainObject.transform.Find("MainRole1").gameObject.SetActive(false);
            if (maptype == 1)
            {
                physicsRaycaster.MainObject.transform.Find("MapType1").gameObject.SetActive(true);
            }
            if (maptype == 0)
            {
                physicsRaycaster.MainObject.transform.Find("MapType2").gameObject.SetActive(true);
            }

            if (NewMainObject.transform.Find("MapType1").gameObject.active == true)
            {
                NewMainObject.transform.Find("MapType1").gameObject.SetActive(false);
                maptype = 1;
            }
            if(NewMainObject.transform.Find("MapType2").gameObject.active == true)
            {
                NewMainObject.transform.Find("MapType2").gameObject.SetActive(false);
                maptype = 0;
            }
            NewMainObject.transform.Find("MainRole1").gameObject.SetActive(true);
            IsChange = false;

            RefreshMap_4(NewMainObject);

            physicsRaycaster.MainObject = NewMainObject;
        }
    }

    /// <summary>
    /// 生成地图（包括主角位置，之后肯定也要包括敌人）
    /// </summary>
    private void GenerateMap()
    {

        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                MapUnityMsg mapUnityMsg = new MapUnityMsg();
                Vector3 newPos = new Vector3(-mapSize.x / 2 + 0.5f + i, 0, -mapSize.y / 2 + 0.5f + j);
                GameObject spawnTile = Instantiate(MapHolder, newPos, Quaternion.identity);
                spawnTile.transform.SetParent(MapGenerator);
                mapUnityMsg.Mapunity = spawnTile;

                int type = Random.Range(0, 100);
                if (type < 70)
                {
                    mapUnityMsg.ACCnormal = "MapType1";
                    spawnTile.transform.Find("MapType1").gameObject.SetActive(true);
                }
                else if (type > 70 && type < 90) 
                {
                    mapUnityMsg.ACCnormal = "Obstacles1";
                    spawnTile.transform.Find("Obstacles1").gameObject.SetActive(true);
                }
                else
                {
                    mapUnityMsg.ACCnormal = "MapType2";
                    spawnTile.transform.Find("MapType2").gameObject.SetActive(true);
                }
                mapUnityMsg.Mappoint = new Vector3(-(mapSize.x-1) / 2 + i, 0, -(mapSize.y - 1) / 2 + j);

                if (i == (mapSize.x - 1) / 2 && j == (mapSize.y - 1) / 2)
                {
                    NewMainObject = spawnTile;
                    //mapUnityMsg.ACCnormal = "MainRole1";
                    spawnTile.transform.Find("MainRole1").gameObject.SetActive(true);
                    physicsRaycaster.MainObject = spawnTile;
                }
                Maplist.Add(mapUnityMsg); 
            }
        }
    }

    /// <summary>
    /// 刷新三格（角色视野为三）
    /// </summary>
    /// <param name="NewMainObject"></param>
    public void RefreshMap_3(GameObject NewMainObject)
    {
        Vector3 point = NewMainObject.transform.position;
        for (int i = 0; i < Maplist.Count; i++)
        {
            Maplist[i].Mapunity.transform.Find("MapType1").gameObject.SetActive(false);
            Maplist[i].Mapunity.transform.Find("MapType2").gameObject.SetActive(false);
            Maplist[i].Mapunity.transform.Find("MainRole1").gameObject.SetActive(false);
            Maplist[i].Mapunity.transform.Find("Obstacles1").gameObject.SetActive(false);
            Maplist[i].Mapunity.transform.Find("Shadow").gameObject.SetActive(true);

            if (Maplist[i].Mappoint.x == point.x - 3 && Maplist[i].Mappoint.z == point.z) 
            { 
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 2 && Maplist[i].Mappoint.z == point.z)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 2 && Maplist[i].Mappoint.z == point.z + 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 2 && Maplist[i].Mappoint.z == point.z - 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 1 && Maplist[i].Mappoint.z == point.z - 2)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 1 && Maplist[i].Mappoint.z == point.z + 2)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 1 && Maplist[i].Mappoint.z == point.z - 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 1 && Maplist[i].Mappoint.z == point.z + 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 1 && Maplist[i].Mappoint.z == point.z)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x && Maplist[i].Mappoint.z == point.z-3)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x && Maplist[i].Mappoint.z == point.z + 3)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x && Maplist[i].Mappoint.z == point.z -2)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x && Maplist[i].Mappoint.z == point.z + 2)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x && Maplist[i].Mappoint.z == point.z - 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x && Maplist[i].Mappoint.z == point.z + 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x && Maplist[i].Mappoint.z == point.z)
            {
                Maplist[i].Mapunity.transform.Find("Shadow").gameObject.SetActive(false);
                Maplist[i].Mapunity.transform.Find("MainRole1").gameObject.SetActive(true);
            }
            if (Maplist[i].Mappoint.x == point.x + 1 && Maplist[i].Mappoint.z == point.z)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 1 && Maplist[i].Mappoint.z == point.z - 2)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 1 && Maplist[i].Mappoint.z == point.z + 2)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 1 && Maplist[i].Mappoint.z == point.z - 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 1 && Maplist[i].Mappoint.z == point.z + 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 2 && Maplist[i].Mappoint.z == point.z - 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 2 && Maplist[i].Mappoint.z == point.z + 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 2 && Maplist[i].Mappoint.z == point.z)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 3 && Maplist[i].Mappoint.z == point.z)
            {
                MapUnityChang(Maplist[i]);
            }
        }
    }

    /// <summary>
    /// 刷新四格（角色视野为四）
    /// </summary>
    /// <param name="NewMainObject"></param>
    public void RefreshMap_4(GameObject NewMainObject)
    {
        Vector3 point = NewMainObject.transform.position;
        for (int i = 0; i < Maplist.Count; i++)
        {
            Maplist[i].Mapunity.transform.Find("MapType1").gameObject.SetActive(false);
            Maplist[i].Mapunity.transform.Find("MapType2").gameObject.SetActive(false);
            Maplist[i].Mapunity.transform.Find("MainRole1").gameObject.SetActive(false);
            Maplist[i].Mapunity.transform.Find("Obstacles1").gameObject.SetActive(false);
            Maplist[i].Mapunity.transform.Find("Shadow").gameObject.SetActive(true);


            if (Maplist[i].Mappoint.x == point.x - 4 && Maplist[i].Mappoint.z == point.z)
            {
                MapUnityChang(Maplist[i]);
            }

            if (Maplist[i].Mappoint.x == point.x - 3 && Maplist[i].Mappoint.z == point.z)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 3 && Maplist[i].Mappoint.z == point.z - 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 3 && Maplist[i].Mappoint.z == point.z + 1)
            {
                MapUnityChang(Maplist[i]);
            }

            if (Maplist[i].Mappoint.x == point.x - 2 && Maplist[i].Mappoint.z == point.z + 2) 
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 2 && Maplist[i].Mappoint.z == point.z + 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 2 && Maplist[i].Mappoint.z == point.z - 2)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 2 && Maplist[i].Mappoint.z == point.z - 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 2 && Maplist[i].Mappoint.z == point.z)
            {
                MapUnityChang(Maplist[i]);
            }
            
            if (Maplist[i].Mappoint.x == point.x - 1 && Maplist[i].Mappoint.z == point.z - 3)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 1 && Maplist[i].Mappoint.z == point.z + 3)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 1 && Maplist[i].Mappoint.z == point.z - 2)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 1 && Maplist[i].Mappoint.z == point.z + 2)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 1 && Maplist[i].Mappoint.z == point.z - 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 1 && Maplist[i].Mappoint.z == point.z + 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x - 1 && Maplist[i].Mappoint.z == point.z)
            {
                MapUnityChang(Maplist[i]);
            }


            if (Maplist[i].Mappoint.x == point.x && Maplist[i].Mappoint.z == point.z - 4)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x && Maplist[i].Mappoint.z == point.z + 4)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x && Maplist[i].Mappoint.z == point.z - 3)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x && Maplist[i].Mappoint.z == point.z + 3)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x && Maplist[i].Mappoint.z == point.z - 2)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x && Maplist[i].Mappoint.z == point.z + 2)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x && Maplist[i].Mappoint.z == point.z - 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x && Maplist[i].Mappoint.z == point.z + 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x && Maplist[i].Mappoint.z == point.z)
            {
                Maplist[i].Mapunity.transform.Find("Shadow").gameObject.SetActive(false);
                Maplist[i].Mapunity.transform.Find("MainRole1").gameObject.SetActive(true);
            }

            if (Maplist[i].Mappoint.x == point.x + 1 && Maplist[i].Mappoint.z == point.z)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 1 && Maplist[i].Mappoint.z == point.z - 2)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 1 && Maplist[i].Mappoint.z == point.z + 2)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 1 && Maplist[i].Mappoint.z == point.z - 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 1 && Maplist[i].Mappoint.z == point.z + 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 1 && Maplist[i].Mappoint.z == point.z + 3)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 1 && Maplist[i].Mappoint.z == point.z - 3)
            {
                MapUnityChang(Maplist[i]);
            }


            if (Maplist[i].Mappoint.x == point.x + 2 && Maplist[i].Mappoint.z == point.z - 2)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 2 && Maplist[i].Mappoint.z == point.z + 2)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 2 && Maplist[i].Mappoint.z == point.z - 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 2 && Maplist[i].Mappoint.z == point.z + 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 2 && Maplist[i].Mappoint.z == point.z)
            {
                MapUnityChang(Maplist[i]);
            }

            if (Maplist[i].Mappoint.x == point.x + 3 && Maplist[i].Mappoint.z == point.z -1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 3 && Maplist[i].Mappoint.z == point.z + 1)
            {
                MapUnityChang(Maplist[i]);
            }
            if (Maplist[i].Mappoint.x == point.x + 3 && Maplist[i].Mappoint.z == point.z)
            {
                MapUnityChang(Maplist[i]);
            }

            if (Maplist[i].Mappoint.x == point.x + 4 && Maplist[i].Mappoint.z == point.z)
            {
                MapUnityChang(Maplist[i]);
            }
        }
    }

    private void MapUnityChang(MapUnityMsg MapUnityMsg)
    {
        MapUnityMsg.Mapunity.transform.Find("MainRole1").gameObject.SetActive(false);
        MapUnityMsg.Mapunity.transform.Find("Shadow").gameObject.SetActive(false);
        MapUnityMsg.Mapunity.transform.Find("" + MapUnityMsg.ACCnormal + "").gameObject.SetActive(true);
    }
}
