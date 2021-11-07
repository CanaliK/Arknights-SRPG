using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 视角控制(控制视角，发出主角的移动命令)
/// </summary>
public class PhysicsRaycaster : MonoBehaviour
{
    public GameObject MainObject;//跟随视角下的角色物体

    public MapControl mapControl;//地图控制脚本

    public bool IsOP = false;//判断点击是否为移动命令，是才执行移动操作
    public bool ShiYeType = true;//视角移动的方式（跟随或wasd）
    void Start()
    {

    }

    void Update()
    {
        if(ShiYeType)
        {
            //w键前进
            if (Input.GetKey(KeyCode.W))
            {
                this.gameObject.transform.Translate(new Vector3(0, 6 * Time.deltaTime, 3 * Time.deltaTime));
            }
            //s键后退
            if (Input.GetKey(KeyCode.S))
            {
                this.gameObject.transform.Translate(new Vector3(0, -6 * Time.deltaTime, -3 * Time.deltaTime));
            }   
            //a键后退
            if (Input.GetKey(KeyCode.A))
            {
                this.gameObject.transform.Translate(new Vector3(-5 * Time.deltaTime, 0, 0));
            }
            //d键后退
            if (Input.GetKey(KeyCode.D))
            {
                this.gameObject.transform.Translate(new Vector3(5 * Time.deltaTime, 0, 0));
            }
        }
        else
        //if (ShiYeType == 2 && MainObject != null) 
        {
            transform.position = Vector3.Lerp(transform.position, MainObject.transform.position + Vector3.up * 5f - MainObject.transform.forward, Time.deltaTime * 2f);

            transform.LookAt(transform);
        }

        if (IsOP == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.transform.Find("Obstacles1").gameObject.active)
                {
                    IsOP = false;
                    return;
                }
                mapControl.NewMainObject = hit.collider.gameObject;
                mapControl.IsChange = true;
            }
            IsOP = false;
        }
    }
}
