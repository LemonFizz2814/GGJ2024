using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Camera cam;
    public SpringJoint holdSpring;
    public int grabDistance;
    int layerMask = 1 << 6;

    void Start()
    {
        SetGrabbedObject(null);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Debug.DrawRay(cam.transform.position, cam.transform.forward * grabDistance, Color.yellow, 0.5f);

            if (Physics.Raycast(cam.transform.position, cam.transform.forward * grabDistance, out hit, Mathf.Infinity, layerMask))
            {
                Debug.Log($"Grabbed {hit.transform.name}");
                SetGrabbedObject(hit.transform.GetComponent<Rigidbody>());
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log($"Dropped");
            SetGrabbedObject(null);
        }
    }

    public void SetGrabbedObject(Rigidbody _object)
    {
        holdSpring.connectedBody = _object;
    }
}
