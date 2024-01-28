using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using static SoundManager;

public class CauldronEvents : MonoBehaviour
{
    [Header("Cauldron Properties")]
    public int MaxCauldronObjects;
    public Transform objectStorage;
    public Transform[] objectSpawn;
    public CauldronUI cauldronUI;
    public GameManager gameManager;
    public PlayerScript playerScript;
    public SoundManager soundManager;

    private List<string> ObjectsInCauldron = new List<string>();
    private List<GameObject> GameObjectsInCauldron = new List<GameObject>();
    private List<string> SecretRecipe = new List<string>();

    private int numCorrectItemsInRecipe;

    private void Start()
    {
        // Get a list of all objects in the Scene with the Tag CauldronObjects
        // To randomly generate the recipe to escape
        List<GameObject> GrabbableObjectsInScene = new List<GameObject>(GameObject.FindGameObjectsWithTag("CauldronObject"));

        // Set the Secret Recipe for Success
        SecretRecipe = ChooseRecipe(GrabbableObjectsInScene);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Add Item to cauldron
        if (other.gameObject.CompareTag("CauldronObject")) 
        {
            ObjectsInCauldron.Add(other.gameObject.name);
            GameObjectsInCauldron.Add(other.gameObject);
            Debug.Log(string.Join(",", ObjectsInCauldron));
            playerScript.DropObject();
            other.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.transform.position = objectStorage.position;

            cauldronUI.PutObjectIn(GameObjectsInCauldron.Count - 1);
            soundManager.PlaySound(Sounds.ItemInCauldron);
        }
    }
    private void Update()
    {
        // Only do things when cauldron has x number of items in it
        if (ObjectsInCauldron.Count == MaxCauldronObjects) 
        {
            CauldronRecipeCheck();

            numCorrectItemsInRecipe = ObjectsInCauldron.Intersect(SecretRecipe).Count();
            print("NoCorrectItemsInRecipe " + numCorrectItemsInRecipe);

            bool solved = (numCorrectItemsInRecipe == MaxCauldronObjects);

            cauldronUI.UpdateUI(numCorrectItemsInRecipe, solved);

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

    // Remove from ObjectsInCauldron that does not exist in SecretRecipe
    private void RemoveCauldronItems()
    {
        for(int i = 0; i < GameObjectsInCauldron.Count; i++)
        {
            GameObjectsInCauldron[i].transform.position = objectSpawn[i].position;
        }

        GameObjectsInCauldron.Clear();
        ObjectsInCauldron.Clear();
        //ObjectsInCauldron.RemoveAll(item => !SecretRecipe.Contains(item));
    }

    private void CauldronRecipeCheck()
    {
        // Success
        bool areEqual = ObjectsInCauldron.OrderBy(t => t).SequenceEqual(SecretRecipe.OrderBy(t => t));

        if (areEqual)
        {
            Debug.Log("SUCCESS");
            soundManager.PlaySound(Sounds.SolvedPuzzle);
            gameManager.CauldronSolved();
        }
        else
        {

            soundManager.PlaySound(Sounds.Incorrect);
        }
    }
}
