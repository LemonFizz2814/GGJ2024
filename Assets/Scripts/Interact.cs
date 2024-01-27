using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public enum INTERACTTYPE
    {
        DYNAMITE,
        OPENABLE
    }

    public INTERACTTYPE interact = INTERACTTYPE.DYNAMITE;
    [SerializeField]private GameManager gm;

    private void Start()
    {
        if (gm = null)
        {
            gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        }
    }
    virtual public void ActivateInteract()
    {
        

        Debug.Log("Object interacted");
        Destroy(gameObject);
    }
}
