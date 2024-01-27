using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronUI : MonoBehaviour
{
    [Header("Recipe Properties")]
    public List<Image> RecipeItems;

    private CauldronEvents Events;

    // Get the script from the parent object
    private void Start()
    {
        Events = GetComponentInParent<CauldronEvents>();
    }

    private void Update()
    {
        // Change Images to the correct number of items
        if (Events.GetUpdateUI())
        {
            StopAllCoroutines();

            // Set Everything White
            for (int i = 0; i < RecipeItems.Count; i++)
            {
                RecipeItems[i].color = Color.red;
            }

            // Set Correct Things Green
            for (int i = 0; i < Events.GetNoCorrectItemsInRecipe(); i++)
            {
                RecipeItems[i].color = Color.green;
            }

            StartCoroutine(SetBackToNormal());

            Events.SetUpdateUI(false);
            Events.SetToPlayEvent(true);
        }
    }

    IEnumerator SetBackToNormal()
    {
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < RecipeItems.Count; i++)
        {
            RecipeItems[i].color = Color.white;
        }
    }

    public void PutObjectIn(int _index)
    {
        RecipeItems[_index].color = Color.grey;
    }
}
