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
    public string GameOverText;
    [SerializeField]private GameManager gm;

    private void Start()
    {
        if (gm == null)
        {
            gm = FindObjectOfType<GameManager>().GetComponent<GameManager>();
        }
    }
    virtual public void ActivateInteract()
    {
        switch (interact)
        {
            case INTERACTTYPE.DYNAMITE:
                gm.GameOver(GameOverText);
                break;

        }

        Debug.Log("Object interacted");
        Destroy(gameObject);
    }
}
