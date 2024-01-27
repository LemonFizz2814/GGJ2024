using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public enum INTERACTTYPE
    {
        DYNAMITE
    }

    public INTERACTTYPE interact = INTERACTTYPE.DYNAMITE;

    virtual public void ActivateInteract()
    {
        Debug.Log("Object interacted");
        Destroy(gameObject);
    }
}
