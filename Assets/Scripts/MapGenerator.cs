using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapGenerator : MonoBehaviour
{
    public GameObject cubePrefab;
    public GameObject cubePrefab1;
    Vector2 mapSize = new Vector2(9, 9);
    public Transform mapHolder;

    GameObject MainObject;
    GameObject OldObject;
    public Camera Camera;

    public LayerMask LayeAbleCilke;

    void Start()
    {
        //GenerateMap();
    }

    private void Update()
    {
        //Camera.transform.position = Vector3.Lerp(Camera.transform.position, MainObject.transform.position + Vector3.up * 4.3f - MainObject.transform.forward, Time.deltaTime * 2f);

        //Camera.transform.LookAt(Camera.transform);

        //Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        //if (Physics.Raycast(ray, out hit, 5000, LayeAbleCilke.value)) 
        //{
        //    if (hit.collider.gameObject != MainObject && hit.collider.gameObject != OldObject)
        //    {
        //        MainObject.SetActive(false);
        //        OldObject.SetActive(true);

        //        Debug.Log(hit.collider.gameObject.name);
        //    }
        //}

        //if (Input.GetMouseButton(0))
        //{
        //    Ray ray = Camera.ScreenPointToRay(Input.mousePosition);//从摄像机发出到点击坐标的射线
        //    RaycastHit hitInfo;
        //    if (Physics.Raycast(ray, out hitInfo, LayeAbleCilke.value))
        //    {
        //        Debug.DrawLine(ray.origin, hitInfo.point);//划出射线，在scene视图中能看到由摄像机发射出的射线
        //        GameObject gameObj = hitInfo.collider.gameObject;
        //        if (gameObj.name.StartsWith("平面") == true)//当射线碰撞目标的name包含Cube，执行拾取操作
        //        {
        //            Debug.Log(gameObj.name);
        //        }
        //    }
        //} 
    }

    /// <summary>
    /// 生成地图
    /// </summary>
    private void GenerateMap()
    {
        Vector3 newPos = new Vector3(-mapSize.x / 2 + 0.5f, 0, -mapSize.y / 2 + .05f);
        GameObject spawnTile = Instantiate(cubePrefab, newPos, Quaternion.identity);
        spawnTile.transform.SetParent(mapHolder);
        //for (int i = 0; i < mapSize.x; i++)
        //{
        //    for (int j = 0; j < mapSize.y; j++)
        //    {
        //        Vector3 newPos = new Vector3(-mapSize.x / 2 + 0.5f + i, 0, -mapSize.y / 2 + .05f + j);
        //        GameObject spawnTile = Instantiate(cubePrefab, newPos, Quaternion.identity);
        //        spawnTile.transform.SetParent(mapHolder);

        //        Vector3 newPos1 = new Vector3(-mapSize.x / 2 + 0.5f + i, 0, -mapSize.y / 2 + .05f + j);
        //        GameObject spawnTile1 = Instantiate(cubePrefab1, newPos, Quaternion.identity);
        //        spawnTile1.transform.SetParent(mapHolder);
        //        spawnTile1.gameObject.SetActive(false);

        //        if (i == 4 && j == 4)
        //        {
        //            spawnTile.gameObject.SetActive(false);
        //            spawnTile1.gameObject.SetActive(true);
        //            MainObject = spawnTile1;
        //            OldObject = spawnTile;
        //        }
        //    }
        //}
    }
}
