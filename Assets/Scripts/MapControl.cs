using Assets.Proclass;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MapControl : MonoBehaviour
{
    public GameObject MapHolder;
    Vector2 mapSize = new Vector2(9, 9);
    public Transform MapGenerator;
    public PhysicsRaycaster physicsRaycaster;

    public bool IsChange = false;
    public GameObject NewMainObject;
    List<Mapdata> mapdata = new List<Mapdata>();

    // Start is called before the first frame update
    void Start()
    {
        GenerateMap();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsChange)
        {
            physicsRaycaster.MainObject.transform.Find("MapType1").gameObject.SetActive(true);
            physicsRaycaster.MainObject.transform.Find("MapType2").gameObject.SetActive(false);

            NewMainObject.transform.Find("MapType1").gameObject.SetActive(false);
            NewMainObject.transform.Find("MapType2").gameObject.SetActive(true);
            IsChange = false;

            RefreshMap(NewMainObject);

            physicsRaycaster.MainObject = NewMainObject;
        }
    }

    private void GenerateMap()
    {
        
        for (int i = 0; i < mapSize.x; i++)
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                Vector3 newPos = new Vector3(-mapSize.x / 2 + 0.5f + i, 0, -mapSize.y / 2 + 0.5f + j);
                GameObject spawnTile = Instantiate(MapHolder, newPos, Quaternion.identity);
                spawnTile.transform.SetParent(MapGenerator);
                spawnTile.transform.Find("MapType1").gameObject.SetActive(true);

                Mapdata mapdata1 = new Mapdata();
                mapdata1.X = -4 + i;
                mapdata1.Y = -4 + j;
                mapdata.Add(mapdata1);

                if (i == 4 && j == 4)
                {
                    spawnTile.transform.Find("MapType1").gameObject.SetActive(false);
                    spawnTile.transform.Find("MapType2").gameObject.SetActive(true);
                    physicsRaycaster.MainObject = spawnTile;
                }
            }
        }
    }

    public void RefreshMap(GameObject NewMainObject)
    {
        Vector3 point = NewMainObject.transform.position;

        bool cheak1 = true;
        bool cheak2 = true;
        bool cheak3 = true;
        bool cheak4 = true;
        bool cheak5 = true;
        bool cheak6 = true;
        bool cheak7 = true;
        bool cheak8 = true;
        bool cheak9 = true;
        bool cheak10 = true;
        bool cheak11 = true;
        bool cheak12 = true;
        for (int i = 0; i < mapdata.Count; i++)
        {
            if (point.x - 2 == mapdata[i].X && point.z == mapdata[i].Y)
            {
                cheak1 = false;
            }
            if (point.x - 1 == mapdata[i].X && point.z == mapdata[i].Y)
            {
                cheak2 = false;
            }
            if (point.x - 1 == mapdata[i].X && point.z - 1 == mapdata[i].Y) 
            {
                cheak3 = false;
            }
            if (point.x - 1 == mapdata[i].X && point.z + 1 == mapdata[i].Y)
            {
                cheak4 = false;
            }
            if (point.x == mapdata[i].X && point.z - 2 == mapdata[i].Y)
            {
                cheak5 = false;
            }
            if (point.x == mapdata[i].X && point.z - 1 == mapdata[i].Y)
            {
                cheak6 = false;
            }
            if (point.x  == mapdata[i].X && point.z + 1 == mapdata[i].Y)
            {
                cheak7 = false;
            }
            if (point.x  == mapdata[i].X && point.z + 2 == mapdata[i].Y)
            {
                cheak8 = false;
            }
            if (point.x + 1 == mapdata[i].X && point.z - 1 == mapdata[i].Y)
            {
                cheak9 = false;
            }
            if (point.x + 1 == mapdata[i].X && point.z + 1 == mapdata[i].Y)
            {
                cheak10 = false;
            }
            if (point.x + 1 == mapdata[i].X && point.z == mapdata[i].Y)
            {
                cheak11 = false;
            }
            if (point.x + 2 == mapdata[i].X && point.z == mapdata[i].Y)
            {
                cheak12 = false;
            }
        }

        if (cheak1)
        {
            produceopbject(point.x - 2, point.z);
        }
        if (cheak2)
        {
            produceopbject(point.x - 1, point.z);
        }
        if (cheak3)
        {
            produceopbject(point.x - 1, point.z - 1);
        }
        if (cheak4)
        {
            produceopbject(point.x - 1, point.z + 1);
        }
        if (cheak5)
        {
            produceopbject(point.x, point.z - 2);
        }
        if (cheak6)
        {
            produceopbject(point.x, point.z - 1);
        }
        if (cheak7)
        {
            produceopbject(point.x, point.z + 1);
        }
        if (cheak8)
        {
            produceopbject(point.x, point.z + 2);
        }
        if (cheak9)
        {
            produceopbject(point.x + 1, point.z - 1);
        }
        if (cheak10)
        {
            produceopbject(point.x + 1, point.z + 1);
        }
        if (cheak11)
        {
            produceopbject(point.x + 1, point.z);
        }
        if (cheak12)
        {
            produceopbject(point.x + 2, point.z);
        }

        //CheakMaoOfmove(point, -2, 0);
        //CheakMaoOfmove(point, -1, 0);
        //CheakMaoOfmove(point, -1, -1);
        //CheakMaoOfmove(point, -1, 1);
        //CheakMaoOfmove(point, 0, -2);
        //CheakMaoOfmove(point, 0, -1);
        //CheakMaoOfmove(point, 0, 1);
        //CheakMaoOfmove(point, 0, 2);
        //CheakMaoOfmove(point, 1, -1);
        //CheakMaoOfmove(point, 1, 1);
        //CheakMaoOfmove(point, 1, 0);
        //CheakMaoOfmove(point, 2, 0);

    }

    //public void CheakMaoOfmove(Vector3 point,int x,int z)
    //{
    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        bool cheak = true;
    //        if ((int)(point.x + x) == (int)dt.Rows[i]["X"])
    //        {
    //            for (int k = 0; k < dt.Rows.Count; k++)
    //            {
    //                if ((int)(point.z + z) == (int)dt.Rows[k]["Z"]) 
    //                {
    //                    cheak = false;
    //                }
    //            }
    //        }
    //        if (cheak)
    //        {
    //            produceopbject(point.x - 2, point.z);
    //        }
    //    }
    //}

    public void produceopbject(float x, float z)
    {
        GameObject spawnTile = Instantiate(MapHolder, new Vector3(x, 0, z), Quaternion.identity);
        spawnTile.transform.SetParent(MapGenerator);
        spawnTile.transform.Find("MapType1").gameObject.SetActive(true);
        Mapdata mapdata1 = new Mapdata();
        mapdata1.X = (int)x;
        mapdata1.Y = (int)z;
        mapdata.Add(mapdata1);
    }
}
