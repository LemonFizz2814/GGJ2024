using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldronEvents : MonoBehaviour
{
    [Header("Cauldron Properties")]
    public int MaxCauldronObjects;

    private List<string> ObjectsInCauldron = new List<string>();

    private void OnCollisionEnter(Collision collision)
    {
        // Add Item to cauldron
        if (collision.gameObject.CompareTag("CauldronObject")) 
        {
            ObjectsInCauldron.Add(collision.gameObject.name);
            Debug.Log(string.Join(",", ObjectsInCauldron));
            Destroy(collision.gameObject);
        }
    }
    private void Update()
    {
        // Only do things when cauldron has x number of items in it
        if (ObjectsInCauldron.Count == MaxCauldronObjects) 
        {
            // Play Event
            CauldronEvent();

            // Remove whatever needs to be removed
            RemoveCauldronItems();
        }
    }

    // For now this just removes everything
    private void RemoveCauldronItems()
    {
        ObjectsInCauldron.Clear();
    }

    private void CauldronEvent()
    {
        // Event 1
        if (ObjectsInCauldron.Contains("Teapot") && ObjectsInCauldron.Contains("Glove"))
        {
            Debug.Log("EXPLOSION");
        }
    }
}
