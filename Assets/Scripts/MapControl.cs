using Assets.Proclass;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

/// <summary>
/// ��ͼ���ƣ���Ҫ�������ɵ�ͼ�����ݲ����仯��ͼ��
/// </summary>
public class MapControl : MonoBehaviour
{
    public GameObject MapHolder;//��ͼ��Ԫ��Ԥ����
    Vector2 mapSize = new Vector2(25, 25);//��ͼ��С���ֽ׶ξ�������Ϊ��������Ȼ���ܻ���������λ�õĵط���bug
    public Transform MapGenerator;//��ͼê�������
    public PhysicsRaycaster physicsRaycaster;//�ӽǿ��ƽű�

    int maptype =1;//��ͼ��Ԫ�ĵ�ͼ���͵�����ֵ���������������ֵ��Σ�
    public bool IsChange = false;//��ͼ�Ƿ���Ҫ���б仯�������ƶ�ʱ��ͨ���������ű������ֵ���仯��ͼ��
    public GameObject NewMainObject;//�ƶ������λ�ã����룩
    //List<Mapdata> mapdata = new List<Mapdata>();
    List<MapUnityMsg> Maplist = new List<MapUnityMsg>();//���е�ͼ��Ԫ��ɵ�list����ŵ�ǰ��ͼ��Ϣ

    void Start()
    {
        GenerateMap();
        RefreshMap_4(NewMainObject);
    }

    /// <summary>
    /// ÿһ֡���鿴һ�Σ��Ƿ���Ҫ�仯��ͼ����Ҫ�ͱ仯
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
    /// ���ɵ�ͼ����������λ�ã�֮��϶�ҲҪ�������ˣ�
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
    /// ˢ�����񣨽�ɫ��ҰΪ����
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
    /// ˢ���ĸ񣨽�ɫ��ҰΪ�ģ�
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
