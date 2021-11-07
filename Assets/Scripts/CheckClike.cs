using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CheckClike : MonoBehaviour
{
    public PhysicsRaycaster physicsRaycaster;

    public Text btntext;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                physicsRaycaster.IsOP = false;
            }
            else
            {
                physicsRaycaster.IsOP = true;
            }
        }
    }

    public void ClikeNotUi()
    {
        if (physicsRaycaster.ShiYeType)
        {
            physicsRaycaster.ShiYeType = false;
            btntext.text = "取消锁定"; 
        }
        else
        {
            physicsRaycaster.ShiYeType = true;
            btntext.text = "跟随角色";
        }
    }
}
