using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Script References")]
    public UIManager uiManager;
    public GameManager gameManager;
    [Space]
    [Header("Object references")]
    public Camera cam;
    public Rigidbody grabbedObject;
    public SpringJoint holdSpring;
    [Space]
    [Header("Variables")]
    public float grabDistance;
    public float throwSpeed;

    private FirstPersonController fpcPlayer;

    // private variables
    private bool canMove = true;
    private int layerMaskGrabbable = 1 << 6;
    private int layerMaskInteract = 1 << 7;
    //int layerMaskPullable = 1 << 7;

    void Start()
    {
        fpcPlayer = GetComponent<FirstPersonController>();
        DropObject();
    }

    void Update()
    {
        if (!canMove)
        {
            fpcPlayer.cameraCanMove = false;
            fpcPlayer.playerCanMove = false;
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, grabDistance, layerMaskGrabbable))
        {
            uiManager.ScaleCrosshair(2.5f);

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log($"Grabbed {hit.transform.name}");
                Debug.DrawRay(cam.transform.position, cam.transform.forward * grabDistance, Color.yellow, 0.5f);

                GrabObject(hit.transform.GetComponent<Rigidbody>());

                /*if (Physics.Raycast(cam.transform.position, cam.transform.forward * grabDistance, out hit, Mathf.Infinity, layerMaskPullable))
                {
                    Debug.Log($"Pulling {hit.transform.name}");
                    SetPullableObject(hit.transform.GetComponent<Pullable>());
                }*/
            }
            
        }
        else if (Physics.Raycast(cam.transform.position, cam.transform.forward * grabDistance, out hit, Mathf.Infinity, layerMaskInteract))
        {
            uiManager.ScaleCrosshair(2.5f);

            if (Input.GetMouseButtonDown(0))
            {
                Interact objInteract = hit.transform.gameObject.GetComponent<Interact>();
                objInteract.ActivateInteract();
                canMove = false;
            }
            //Debug.Log(hit.transform.name);
        }
        else
        {
            uiManager.ScaleCrosshair(1);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log($"Dropped");
            DropObject();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log($"Throw");
            if (grabbedObject != null)
            {
                grabbedObject.AddForce(cam.transform.forward * throwSpeed);
                DropObject();
            }
        }

        if (transform.position.y < -5)
        {
            SceneManager.LoadScene("Backrooms");
        }
    }

    /*public void SetPullableObject(Pullable _pullableScript)
    {
        _pullableScript.SetLerp(0.5f);
    }*/

    private void OnTriggerEnter(Collider other)
    {
        // game ending triggers
        if(other.CompareTag("GameWin"))
        {
            gameManager.GameWon();
        }
        if (other.CompareTag("Wizard"))
        {
            gameManager.GameOver("Gandalf the Gay caught you");
        }

        // joke triggers
        if (other.CompareTag("BananaPeel"))
        {
            gameManager.GameOver("You slipped on a banana peel");
        }
        if (other.CompareTag("WhoopeeCushion"))
        {
            Debug.LogError($"Played audio for whoopee cushon pleaseeeee");
        }
    }

    public void GrabObject(Rigidbody _object)
    {
        grabbedObject = _object;
        holdSpring.transform.position = grabbedObject.transform.position;

        // set grabbed object to rigid physics
        grabbedObject.drag = 100;
        grabbedObject.angularDrag = 10;

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

    public void SetCanMove(bool _canMove)
    {
        canMove = _canMove;
    }
}
