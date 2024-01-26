using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronEvents : MonoBehaviour
{
    List<string> ObjectsInCauldron = new List<string>();

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CauldronObject")) 
        {
            ObjectsInCauldron.Add(collision.gameObject.name);
            Debug.Log(string.Join(",", ObjectsInCauldron));
            Destroy(collision.gameObject);
        }
    }
}
