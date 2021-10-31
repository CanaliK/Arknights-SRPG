using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRaycaster : MonoBehaviour
{
    public GameObject MainObject;

    public MapControl mapControl;
    void Start()
    {

    }

    void Update()
    {
        if (MainObject != null)
        {
            transform.position = Vector3.Lerp(transform.position, MainObject.transform.position + Vector3.up * 4.3f - MainObject.transform.forward, Time.deltaTime * 2f);

            transform.LookAt(transform);
        }
        transform.LookAt(transform);

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                mapControl.NewMainObject = hit.collider.gameObject;
                mapControl.IsChange = true;
            }
        }
    }
}
