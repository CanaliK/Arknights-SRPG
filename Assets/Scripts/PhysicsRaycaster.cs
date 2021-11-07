using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �ӽǿ���(�����ӽǣ��������ǵ��ƶ�����)
/// </summary>
public class PhysicsRaycaster : MonoBehaviour
{
    public GameObject MainObject;//�����ӽ��µĽ�ɫ����

    public MapControl mapControl;//��ͼ���ƽű�

    public bool IsOP = false;//�жϵ���Ƿ�Ϊ�ƶ�����ǲ�ִ���ƶ�����
    public bool ShiYeType = true;//�ӽ��ƶ��ķ�ʽ�������wasd��
    void Start()
    {

    }

    void Update()
    {
        if(ShiYeType)
        {
            //w��ǰ��
            if (Input.GetKey(KeyCode.W))
            {
                this.gameObject.transform.Translate(new Vector3(0, 6 * Time.deltaTime, 3 * Time.deltaTime));
            }
            //s������
            if (Input.GetKey(KeyCode.S))
            {
                this.gameObject.transform.Translate(new Vector3(0, -6 * Time.deltaTime, -3 * Time.deltaTime));
            }   
            //a������
            if (Input.GetKey(KeyCode.A))
            {
                this.gameObject.transform.Translate(new Vector3(-5 * Time.deltaTime, 0, 0));
            }
            //d������
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
