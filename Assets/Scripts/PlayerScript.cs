using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Object references")]
    public Camera cam;
    public Rigidbody grabbedObject;
    public SpringJoint holdSpring;
    [Space]
    [Header("Variables")]
    public float grabDistance;
    public float throwSpeed;

    // private variables
    int layerMaskGrabbable = 1 << 6;
    //int layerMaskPullable = 1 << 7;

    void Start()
    {
        DropObject();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Debug.DrawRay(cam.transform.position, cam.transform.forward * grabDistance, Color.yellow, 0.5f);

            if (Physics.Raycast(cam.transform.position, cam.transform.forward * grabDistance, out hit, Mathf.Infinity, layerMaskGrabbable))
            {
                Debug.Log($"Grabbed {hit.transform.name}");
                GrabObject(hit.transform.GetComponent<Rigidbody>());
            }
            /*if (Physics.Raycast(cam.transform.position, cam.transform.forward * grabDistance, out hit, Mathf.Infinity, layerMaskPullable))
            {
                Debug.Log($"Pulling {hit.transform.name}");
                SetPullableObject(hit.transform.GetComponent<Pullable>());
            }*/
        }

        if(Input.GetMouseButtonUp(0))
        {
            Debug.Log($"Dropped");
            DropObject();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log($"Throw");
            if(grabbedObject != null)
            {
                grabbedObject.AddForce(cam.transform.forward * throwSpeed);
                DropObject();
            }
        }

        if(transform.position.y < -5)
        {
            SceneManager.LoadScene("Backrooms");
        }
    }

    /*public void SetPullableObject(Pullable _pullableScript)
    {
        _pullableScript.SetLerp(0.5f);
    }*/

    public void GrabObject(Rigidbody _object)
    {
        grabbedObject = _object;
        holdSpring.transform.position = grabbedObject.transform.position;

        // set grabbed object to rigid physics
        grabbedObject.drag = 100;
        grabbedObject.angularDrag = 100;

        holdSpring.connectedBody = _object;
    }

    public void DropObject()
    {
        // set previous object back to normal physics
        if (grabbedObject != null)
        {
            grabbedObject.drag = 1;
            grabbedObject.angularDrag = 1;

            grabbedObject = null;
            holdSpring.connectedBody = null;
        }
    }
}
