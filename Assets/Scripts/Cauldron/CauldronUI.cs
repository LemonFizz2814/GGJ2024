using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CauldronUI : MonoBehaviour
{
    [Header("Recipe Properties")]
    public List<Image> RecipeItems;

    public CauldronEvents Events;

    public void UpdateUI(int _numCorrectItemsInRecipe)
    {
        StopAllCoroutines();

        // Set Everything White
        for (int i = 0; i < RecipeItems.Count; i++)
        {
            //Debug.Log($"Green {i}: {(i < Events.GetNoCorrectItemsInRecipe())}, {Events.GetNoCorrectItemsInRecipe()}");
            RecipeItems[i].color = (i < _numCorrectItemsInRecipe) ? Color.green : Color.red;
        }

        StartCoroutine(SetBackToNormal());
    }

    IEnumerator SetBackToNormal()
    {
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < RecipeItems.Count; i++)
        {
            if (RecipeItems[i].color != Color.grey)
            {
                RecipeItems[i].color = Color.white;
            }
        }
    }

    public void PutObjectIn(int _index)
    {
        RecipeItems[_index].color = Color.grey;
    }
}
