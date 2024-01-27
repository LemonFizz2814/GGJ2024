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

    private int NoCorrectItemsInRecipe;

    private bool UpdateUI = false;
    private bool ToPlayEvent = false;

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
            // Remove whatever needs to be removed
            RemoveCauldronItems();

            UpdateUI = true;

            NoCorrectItemsInRecipe = ObjectsInCauldron.Intersect(SecretRecipe).Count();
        }

        if (ToPlayEvent)
        {
            // Play Event
            CauldronEvent();
            ToPlayEvent = false;
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

    // Remove from ObjectsInCauldron that does not exist in SecretRecipe
    private void RemoveCauldronItems()
    {
        ObjectsInCauldron.RemoveAll(item => !SecretRecipe.Contains(item));
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

    public int GetNoCorrectItemsInRecipe()
    {
        return NoCorrectItemsInRecipe;
    }

    public bool GetUpdateUI()
    {
        return UpdateUI;
    }

    public void SetUpdateUI(bool NewValue)
    {
        UpdateUI = NewValue;
    }

    public bool GetToPlayEvent()
    {
        return ToPlayEvent;
    }

    public void SetToPlayEvent(bool NewValue)
    {
        ToPlayEvent = NewValue;
    }
}
