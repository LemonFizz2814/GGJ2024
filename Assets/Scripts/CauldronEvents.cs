using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class CauldronEvents : MonoBehaviour
{
    [Header("Cauldron Properties")]
    public int MaxCauldronObjects;

    private List<string> ObjectsInCauldron = new List<string>();
    private List<string> SecretRecipe = new List<string>();

    private void Start()
    {
        // Get a list of all objects in the Scene with the Tag CauldronObjects
        // To randomly generate the recipe to escape
        List<GameObject> GrabbableObjectsInScene = new List<GameObject>(GameObject.FindGameObjectsWithTag("CauldronObject"));

        // Set the Secret Recipe for Success
        SecretRecipe = ChooseRecipe(GrabbableObjectsInScene);
    }

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

    // Randomly Select Cauldron Objects to create the secret recipe
    private List<string> ChooseRecipe(List<GameObject> ObjectsToChooseFrom)
    {
        List<string> result = new List<string>();

        // Select Random Objects for Secret Recipe
        for (int i = 0; i < MaxCauldronObjects; i++)
        {
            int NoObjects = ObjectsToChooseFrom.Count;
            int randomIndex = Random.Range(0, NoObjects);

            result.Add(ObjectsToChooseFrom[randomIndex].name);
            ObjectsToChooseFrom.Remove(ObjectsToChooseFrom[randomIndex]);
        }

        Debug.Log("The Secret Recipe is " + string.Join(", ", result));

        return result;
    }

    // For now this just removes everything
    private void RemoveCauldronItems()
    {
        ObjectsInCauldron.Clear();
    }

    private void CauldronEvent()
    {
        // Success
        bool areEqual = ObjectsInCauldron.OrderBy(t => t).SequenceEqual(SecretRecipe.OrderBy(t => t));

        if (areEqual)
        {
            Debug.Log("SUCCESS");
        }
    }
}
