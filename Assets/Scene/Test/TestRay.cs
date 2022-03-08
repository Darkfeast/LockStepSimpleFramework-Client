using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TestRay : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 input = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
            Vector3 origin = Camera.main.ScreenToWorldPoint(input);

            Vector3 dir = Camera.main.transform.position;
            float dis = 1000;
            // Debug.DrawLine(dir, origin, Color.cyan, 5);

            Ray r = new Ray(dir, origin-dir);
            Debug.DrawRay(dir,(origin)*dis,Color.cyan,5);   

            // RaycastHit2D[] hits = Physics2D.RaycastAll(origin, dir);
            RaycastHit[] hits = null;
            hits = Physics.RaycastAll(dir, origin-dir,dis);
            Debug.Log("L "+hits.Length);
            foreach (var v in hits)
            {
                Debug.Log(v.transform.name);
            }
            // }
            // else
            // {
            //     Debug.Log("ddd");
            // }
        }
    }
}